using System;
using System.Net;

namespace Reader
{
    interface ITalker
    {
        event EventHandler<TransportDataEventArgs> EvRecvData;
        event EventHandler<ErrorReceivedEventArgs> EvException;

        bool Connect(IPAddress ip, int port, out string strException);                 // 连接到服务端
        bool SendMessage(byte[] btAryBuffer);                 // 发送数据包
        void SignOut();                                       // 注销连接

        bool IsConnect();                                    //校验是否连接服务器
    }
}
