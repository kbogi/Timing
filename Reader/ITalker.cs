using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace Reader
{
    public delegate void MessageReceivedEventHandler(byte[] btAryBuffer);
    public delegate void ExceptionReceivedEventHandler(string strErr);

    interface ITalker
    {
        event MessageReceivedEventHandler MessageReceived;    // 接收到发来的消息

        event ExceptionReceivedEventHandler TcpExceptionReceived;    // 接收Tcp异常信息

        bool Connect(IPAddress ip, int port, out string strException);                 // 连接到服务端
        bool SendMessage(byte[] btAryBuffer);                 // 发送数据包
        void SignOut();                                       // 注销连接

        bool IsConnect();                                    //校验是否连接服务器
    }
}
