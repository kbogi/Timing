using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;
using System.Threading;

namespace Reader
{
    public class Talker:ITalker
    {
        public event MessageReceivedEventHandler MessageReceived;
        public event ExceptionReceivedEventHandler TcpExceptionReceived;

        //TcpClient client;
        Socket tcpClient;
        private Thread waitThread;

        private bool bIsConnect = false;
        IPAddress ipAddress;
        int nPort;
        int tryReconnectTimes = 0;
        bool reconnecting = false;
        private const int connectTimeout = 1000; // 连接超时时间

        public bool Connect(IPAddress ipAddress, int nPort, out string strException)
        {
            bool ret = false;
            if (tcpClient == null)
            {
                this.ipAddress = ipAddress;
                this.nPort = nPort;
            }

            if (tcpClient != null)
            {
                tcpClient.Close();
                tcpClient = null;
            }
            strException = String.Empty;

            tcpClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IAsyncResult ar = tcpClient.BeginConnect(ipAddress, nPort, null, null);
            bool success = ar.AsyncWaitHandle.WaitOne(connectTimeout);
            if (!success)
            {
                strException = "连接超时，未连接到指定服务器";
                ret = false;
            }
            else
            {
                try
                {
                    tcpClient.EndConnect(ar);
                    //开始KeppAlive检测
                    if (tcpClient != null)
                    {
                        tcpClient.IOControl(IOControlCode.KeepAliveValues, KeepAlive(1, 300, 300), null);//设置Keep-Alive参数
                    }

                    if (!IsConnect())
                    {
                        //建立线程收取服务器发送数据
                        ThreadStart stThead = new ThreadStart(ReceivedData);
                        waitThread = new Thread(stThead);
                        waitThread.IsBackground = true;
                        waitThread.Start();
                    }

                    bIsConnect = true;
                    ret = true;
                }
                catch (Exception e)
                {
                    strException = "连接异常： " + e.Message;
                    Thread.Sleep(connectTimeout);
                    ret = false;
                }
            }
            return ret;
        }

        private byte[] KeepAlive(int onOff, int keepAliveTime, int keepAliveInterval)
        {
            byte[] buffer = new byte[12];
            BitConverter.GetBytes(onOff).CopyTo(buffer, 0); // 是否启用Keep-Alive
            BitConverter.GetBytes(keepAliveTime).CopyTo(buffer, 4); // 多长时间后开始第一次探测（单位：毫秒）
            BitConverter.GetBytes(keepAliveInterval).CopyTo(buffer, 8); //探测时间间隔（单位：毫秒）
            return buffer;
        }

        private void ReceivedData()
        {
            while (true)
            {
                if (reconnecting)
                    continue;
                if (tcpClient.Poll(-1, SelectMode.SelectRead))
                {
                    try
                    {
                        byte[] btAryBuffer = new byte[4096 * 10];
                        int nLenRead = tcpClient.Receive(btAryBuffer);
                        if (nLenRead == 0)
                        {
                            Console.WriteLine("数据接收正常断开！！");
                            continue;
                        }
                        if (MessageReceived != null)
                        {
                            byte[] btAryReceiveData = new byte[nLenRead];

                            Array.Copy(btAryBuffer, btAryReceiveData, nLenRead);

                            MessageReceived(btAryReceiveData);
                        }
                    }
                    catch (Exception ex)
                    {
                        string exStr = String.Empty;
                        if (ex is SocketException)
                        {
                            SocketError err = ((SocketException)ex).SocketErrorCode;
                            exStr = String.Format("数据接收异常!!! [{0},{1}] {2}", ex.GetType().Name, err, ex.Message);
                            if (err.Equals(SocketError.ConnectionReset))
                            {
                                if (TcpExceptionReceived != null)
                                {
                                    TcpExceptionReceived(exStr);
                                }
                                Reconnect();
                            }
                            else
                            {
                                Console.WriteLine("SocketError={0}", err.ToString());
                            }
                        }
                    }
                }
            }
        }

        private void Reconnect()
        {
            reconnecting = true;
            new Thread(new ThreadStart(TryReconnect)).Start();
        }

        private void TryReconnect()
        {
            string exStr = String.Empty;
            while (reconnecting)
            {
                if (Connect(this.ipAddress, this.nPort, out string strException))
                {
                    exStr = String.Format("[{1}@{2}] 第[{0}]次重连成功!!! ", tryReconnectTimes, ipAddress.ToString(), nPort);
                    reconnecting = false;
                    tryReconnectTimes = 0;
                }
                else
                {
                    exStr = String.Format("[{1}@{2}] 第[{0}]次重连失败!!! , {3}", tryReconnectTimes++, ipAddress.ToString(), nPort, strException);
                }
                if (TcpExceptionReceived != null)
                {
                    TcpExceptionReceived(exStr);
                }
            }
        }

        public bool SendMessage(byte[] btAryBuffer)
        {
            try
            {
                lock (tcpClient)
                {
                    tcpClient.Send(btAryBuffer);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public void SignOut()
        {
            reconnecting = false;
            waitThread.Abort();
            bIsConnect = false;
        }

        public bool IsConnect()
        {
            return bIsConnect;
        }
    }
}
