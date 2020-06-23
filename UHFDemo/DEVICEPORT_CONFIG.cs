using System;

namespace UHFDemo
{
    public class DEVICEPORT_CONFIG
    {
		// 65
		byte bIndex;                    /* 端口序号 */
		byte bPortEn;                  /* 端口启用标志 1：启用后 ；0：不启用 */
		byte bNetMode;             /* 网络工作模式: 0: TCP SERVER;1: TCP CLENT; 2: UDP SERVER 3：UDP CLIENT; */
		byte bRandSportFlag;           /* TCP 客户端模式下随即本地端口号，1：随机 0: 不随机*/
		ushort wNetPort;                /* [2]网络通讯端口号 */
		byte[] bDesIP;                /* [4]目的IP地址 */
		ushort wDesPort;                /* [2]工作于TCP Server模式时，允许外部连接的端口号 */
		uint dBaudRate;                /* [4]串口波特率: 300---921600bps */
		byte bDataSize;                /* 串口数据位: 5---8位 */
		byte bStopBits;                /* 串口停止位: 1表示1个停止位; 2表示2个停止位 */
		byte bParity;                  /* 串口校验位: 0表示奇校验; 1表示偶校验; 2表示标志位(MARK,置1); 3表示空白位(SPACE,清0);  */
		byte bPHYChangeHandle;     /* PHY断开，Socket动作，1：关闭Socket 2、不动作*/
		uint dRxPktlength;         /*[4] 串口RX数据打包长度，最大1024 */
		uint dRxPktTimeout;            /*[4] 串口RX数据打包转发的最大等待时间,单位为: 10ms,0则表示关闭超时功能 */
		byte bReConnectCnt;            /* 工作于TCP CLIENT时，连接TCP SERVER的最大重试次数*/
		byte bResetCtrl;               /* 串口复位操作: 0表示不清空串口数据缓冲区; 1表示连接时清空串口数据缓冲区 */
		byte bDNSFlag;             /* 域名功能启用标志，1：启用 2：不启用*/
		byte[] szDomainname;     /* [20]域名*/
		byte[] bDNSHostIP;            /* [4]DNS 主机*/
		ushort wDNSHostPort;            /*[2] DNS 端口*/
		byte[] breserved;         /*[8] 保留*/

		private byte[] data;
        private int readIndex;

        public DEVICEPORT_CONFIG(byte[] parseData)
		{
            this.data = new byte[65];
            Array.Copy(parseData, 0, data, 0, data.Length);
            readIndex = 0;

            bIndex = data[readIndex++];

            bPortEn = data[readIndex++];

            bNetMode = data[readIndex++];

            bRandSportFlag = data[readIndex++];

            wNetPort = (ushort)getu16();

            bDesIP = new byte[4];
            Array.Copy(data, readIndex, bDesIP, 0, bDesIP.Length);
            readIndex += bDesIP.Length;

            wDesPort = (ushort)getu16();

            dBaudRate = (uint)getu32();

            bDataSize = data[readIndex++];

            bStopBits = data[readIndex++];

            bParity = data[readIndex++];

            bPHYChangeHandle = data[readIndex++];

            dRxPktlength = (uint)getu32();

            dRxPktTimeout = (uint)getu32();

            bReConnectCnt = data[readIndex++];

            bResetCtrl = data[readIndex++];
            
            bDNSFlag = data[readIndex++];
            
            szDomainname = new byte[20];
            Array.Copy(data, readIndex, szDomainname, 0, szDomainname.Length);
            readIndex += szDomainname.Length;

            bDNSHostIP = new byte[4];
            Array.Copy(data, readIndex, bDNSHostIP, 0, bDNSHostIP.Length);
            readIndex += bDNSHostIP.Length;

            wDNSHostPort = (ushort)getu16();

            breserved = new byte[8];
            Array.Copy(data, readIndex, breserved, 0, breserved.Length);
            readIndex += breserved.Length;
        }

        public byte[] RawData
        {
            get { return data; }
        }

        int getu8()
        {
            return getu8at(readIndex++);
        }

        int getu16()
        {
            int val;
            val = getu16at(readIndex);
            readIndex += 2;
            return val;
        }

        short gets16()
        {
            short val;
            val = (short)getu16at(readIndex);
            readIndex += 2;
            return val;
        }

        int getu24()
        {
            int val;
            val = getu24at(readIndex);
            readIndex += 3;
            return val;
        }

        int getu32()
        {
            int val;
            val = getu32at(readIndex);
            readIndex += 4;
            return val;
        }

        void getbytes(byte[] destination, int length)
        {
            Array.Copy(data, readIndex, destination, 0, length);
            readIndex += length;
        }

        int getu8at(int offset)
        {
            return data[offset] & 0xff;
        }

        int getu16at(int offset)
        {
            return ((data[offset] & 0xff) << 8)
              | ((data[offset + 1] & 0xff) << 0);
        }

        int getu24at(int offset)
        {
            return ((data[offset] & 0xff) << 16)
              | ((data[offset + 1] & 0xff) << 8)
              | ((data[offset + 2] & 0xff) << 0);
        }

        int getu32at(int offset)
        {
            return ((data[offset] & 0xff) << 24)
              | ((data[offset + 1] & 0xff) << 16)
              | ((data[offset + 2] & 0xff) << 8)
              | ((data[offset + 3] & 0xff) << 0);
        }
    }
}