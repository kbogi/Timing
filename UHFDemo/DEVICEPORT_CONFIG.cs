﻿using NPOI.Util;
using System;
using System.Collections;
using System.ComponentModel;
using System.Net;

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
        private int writeIndex;

        public DEVICEPORT_CONFIG(byte[] parseData)
		{
            this.data = new byte[65];
            Array.Copy(parseData, 0, data, 0, data.Length);
            readIndex = 0;

            bIndex = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bIndex={0}", bIndex);

            bPortEn = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bPortEn={0}", bPortEn);

            bNetMode = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bNetMode={0}", bNetMode);

            bRandSportFlag = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bRandSportFlag={0}", bRandSportFlag);

            wNetPort = GetPort();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG wNetPort={0}", wNetPort);

            bDesIP = new byte[4];
            Array.Copy(data, readIndex, bDesIP, 0, bDesIP.Length);
            readIndex += bDesIP.Length;
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bDesIP={0}", CCommondMethod.ToHex(bDesIP, "", "."));

            wDesPort = GetPort();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG wDesPort={0}", wDesPort);

            dBaudRate = GetBaudrate();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG dBaudRate={0}", dBaudRate);

            bDataSize = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bDataSize={0}", bDataSize);

            bStopBits = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bStopBits={0}", bStopBits);

            bParity = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bParity={0}", bParity);

            bPHYChangeHandle = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bPHYChangeHandle={0}", bPHYChangeHandle);

            dRxPktlength = GetBaudrate();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG dRxPktlength={0}", dRxPktlength);

            dRxPktTimeout = GetBaudrate();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG dRxPktTimeout={0}", dRxPktTimeout);

            bReConnectCnt = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bReConnectCnt={0}", bReConnectCnt);

            bResetCtrl = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bResetCtrl={0}", bResetCtrl);

            bDNSFlag = data[readIndex++];
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bDNSFlag={0}", bDNSFlag);

            szDomainname = new byte[20];
            Array.Copy(data, readIndex, szDomainname, 0, szDomainname.Length);
            readIndex += szDomainname.Length;
            //Console.WriteLine(" <---DEVICEPORT_CONFIG szDomainname={0}", CCommondMethod.ToHex(szDomainname, "", " "));

            bDNSHostIP = new byte[4];
            Array.Copy(data, readIndex, bDNSHostIP, 0, bDNSHostIP.Length);
            readIndex += bDNSHostIP.Length;
            //Console.WriteLine(" <---DEVICEPORT_CONFIG bDNSHostIP={0}", CCommondMethod.ToHex(bDNSHostIP, "", "."));

            wDNSHostPort = GetPort();
            //Console.WriteLine(" <---DEVICEPORT_CONFIG wDNSHostPort={0}", wDNSHostPort);

            breserved = new byte[8];
            Array.Copy(data, readIndex, breserved, 0, breserved.Length);
            readIndex += breserved.Length;
            //Console.WriteLine(" <---DEVICEPORT_CONFIG breserved={0}", CCommondMethod.ToHex(breserved, "", " "));
        }

        private ushort GetPort()
        {
            int val;
            val = 0
                | ((data[readIndex + 1] & 0xff) << 8)
                | ((data[readIndex] & 0xff) << 0);
            readIndex += 2;
            return Convert.ToUInt16(val);
        }
        private uint GetBaudrate()
        {
            int val;
            val = 0 
              | ((data[readIndex + 3] & 0xff) << 24)
              | ((data[readIndex + 2] & 0xff) << 16)
              | ((data[readIndex + 1] & 0xff) << 8)
              | ((data[readIndex + 0] & 0xff) << 0);
            readIndex += 4;
            return Convert.ToUInt32(val);
        }
        public byte[] UpdateDevCfgForSet()
        {
            byte[] setdata = new byte[65];
            writeIndex = 0;

            setdata[writeIndex++] = bIndex;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bIndex={0}", bIndex);
            setdata[writeIndex++] = bPortEn;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bPortEn={0}", bPortEn);
            setdata[writeIndex++] = bNetMode;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bNetMode={0}", bNetMode);
            setdata[writeIndex++] = bRandSportFlag;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bRandSportFlag={0}", bRandSportFlag);
            setdata[writeIndex++] = (byte)((wNetPort >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((wNetPort >> 8) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG wNetPort={0}", wNetPort);

            Array.Copy(bDesIP, 0, setdata, writeIndex, bDesIP.Length);
            writeIndex += bDesIP.Length;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bDesIP={0}", CCommondMethod.ToHex(bDesIP, "", "."));

            setdata[writeIndex++] = (byte)((wDesPort >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((wDesPort >> 8) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG wDesPort={0}", wDesPort);

            setdata[writeIndex++] = (byte)((dBaudRate >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((dBaudRate >> 8) & 0xff);
            setdata[writeIndex++] = (byte)((dBaudRate >> 16) & 0xff);
            setdata[writeIndex++] = (byte)((dBaudRate >> 24) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG dBaudRate={0}", dBaudRate);

            setdata[writeIndex++] = bDataSize;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bDataSize={0}", bDataSize);
            setdata[writeIndex++] = bStopBits;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bStopBits={0}", bStopBits);
            setdata[writeIndex++] = bParity;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bParity={0}", bParity);
            setdata[writeIndex++] = bPHYChangeHandle;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bPHYChangeHandle={0}", bPHYChangeHandle);

            setdata[writeIndex++] = (byte)((dRxPktlength >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktlength >> 8) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktlength >> 16) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktlength >> 24) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG dRxPktlength={0}", dRxPktlength);

            setdata[writeIndex++] = (byte)((dRxPktTimeout >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktTimeout >> 8) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktTimeout >> 16) & 0xff);
            setdata[writeIndex++] = (byte)((dRxPktTimeout >> 24) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG dRxPktTimeout={0}", dRxPktTimeout);

            setdata[writeIndex++] = bReConnectCnt;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bReConnectCnt={0}", bReConnectCnt);
            setdata[writeIndex++] = bResetCtrl;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bResetCtrl={0}", bResetCtrl);
            setdata[writeIndex++] = bDNSFlag;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bDNSFlag={0}", bDNSFlag);

            Array.Copy(szDomainname, 0, setdata, writeIndex, szDomainname.Length);
            writeIndex += szDomainname.Length;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG szDomainname={0}", CCommondMethod.ToHex(szDomainname, "", " "));

            Array.Copy(bDNSHostIP, 0, setdata, writeIndex, bDNSHostIP.Length);
            writeIndex += bDNSHostIP.Length;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG bDNSHostIP={0}", CCommondMethod.ToHex(bDNSHostIP, "", "."));

            setdata[writeIndex++] = (byte)((wDNSHostPort >> 0) & 0xff);
            setdata[writeIndex++] = (byte)((wDNSHostPort >> 8) & 0xff);
            //Console.WriteLine(" --> DEVICEPORT_CONFIG wDNSHostPort={0}", wDNSHostPort);

            Array.Copy(breserved, 0, setdata, writeIndex, breserved.Length);
            writeIndex += breserved.Length;
            //Console.WriteLine(" --> DEVICEPORT_CONFIG breserved={0}", CCommondMethod.ToHex(breserved, "", " "));

            return setdata;
        }
        public byte[] RawData
        {
            get { return data; }
        }
        public byte Index { 
            get => bIndex; 
            set => bIndex = value; 
        }
        public bool PortEn { 
            get
            {
                return (bPortEn == 0x01 ? true : false);
            }
            set
            {
                if(value == true)
                {
                    bPortEn = 0x01;
                }
                else
                {
                    bPortEn = 0x00;
                }
                //Console.WriteLine("set PortEn={0} -> {1}", value, bPortEn);
            }
        }
        public string NetMode {
            get 
            {
                return Enum.GetName(typeof(MODULE_TYPE), bNetMode);
            }
            set
            {
                if(MODULE_TYPE.TCP_SERVER.ToString().Equals(value))
                {
                    bNetMode = 0x00;
                }
                else if (MODULE_TYPE.TCP_CLIENT.ToString().Equals(value))
                {
                    bNetMode = 0x01;
                }
                else if (MODULE_TYPE.UDP_SERVER.ToString().Equals(value))
                {
                    bNetMode = 0x02;
                }
                else if (MODULE_TYPE.UDP_CLIENT.ToString().Equals(value))
                {
                    bNetMode = 0x03;
                }
                //Console.WriteLine("set bNetMode={0} -> {1}", value, bNetMode);
            }
        }

         public bool RandSportFlag { 
            get
            {
                if (bRandSportFlag == 0x01)
                    return true;
                else //if (bRandSportFlag == 0x00)
                    return false;
            }
            set
            {
                bRandSportFlag = (byte)(value == true ? 0x01 : 0x00);
            }
        }
        public ushort NetPort { 
            get
            {
                return wNetPort;
            }
            set
            { 
                wNetPort = value; 
            }
        }
        public string DesIP
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
              Convert.ToInt32(bDesIP[0]),
              Convert.ToInt32(bDesIP[1]),
              Convert.ToInt32(bDesIP[2]),
              Convert.ToInt32(bDesIP[3]));
            }

            set
            {
                byte[] ip = IPAddress.Parse(value).GetAddressBytes();
                Array.Copy(ip, 0, bDesIP, 0, ip.Length);
                //Console.WriteLine("chris: set dest ip {0} -> {1}", CCommondMethod.ToHex(bDesIP, "", "."), CCommondMethod.ToHex(ip, "", "."));
            }
        }
        public ushort DesPort { 
            get => wDesPort; 
            set => wDesPort = value; }
        public string BaudRate { 
            get
            {
                return Enum.GetName(typeof(BAUDRATE), dBaudRate);
            }
            set
            {
                dBaudRate = (uint)GetEnumValue(typeof(BAUDRATE), value);
            }
        }
        public static int GetEnumValue(Type enumType, string enumName)
        {
            System.Array values = System.Enum.GetValues(enumType);
            foreach (var value in values)
            {
                if (value.ToString().Equals(enumName))
                    return (int)value;
            }
            return 0;
        }

        public string DataSize {
            get
            {
                return Enum.GetName(typeof(DATABITS), bDataSize);
            }
            set
            {
                bDataSize = (byte)GetEnumValue(typeof(DATABITS), value);
            }
        }
        public string StopBits {
            get
            {
                return Enum.GetName(typeof(STOPBITS), bStopBits);
            }
            set
            {
                bStopBits = (byte)GetEnumValue(typeof(STOPBITS), value);
            }
        }
        public string Parity {
            get
            {
                return Enum.GetName(typeof(PARITY), bParity);
            }
            set
            {
                bParity = (byte)GetEnumValue(typeof(PARITY), value);
            }
        }
        public byte PHYChangeHandle { 
            get => bPHYChangeHandle; 
            set => bPHYChangeHandle = value; 
        }
        public uint RxPktlength { 
            get => dRxPktlength; 
            set => dRxPktlength = value;
        }
        public uint RxPktTimeout { 
            get => dRxPktTimeout; 
            set => dRxPktTimeout = value;
        }
        public byte ReConnectCnt { 
            get => bReConnectCnt; 
            set => bReConnectCnt = value; }
        public byte ResetCtrl { 
            get => bResetCtrl; 
            set => bResetCtrl = value; 
        }
        public byte DNSFlag {
            get => bDNSFlag; 
            set => bDNSFlag = value; }
        public byte[] DomainName { 
            get => szDomainname; 
            set => szDomainname = value; 
        }
        public byte[] DNSHostIP { 
            get => bDNSHostIP; 
            set => bDNSHostIP = value; 
        }
        public ushort DNSHostPort { 
            get => wDNSHostPort; 
            set => wDNSHostPort = value;
        }
        public byte[] Reserved { 
            get => breserved; 
            set => breserved = value; 
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