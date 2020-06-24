using System;
using System.Net;

namespace UHFDemo
{
    public class DEVICEHW_CONFIG
    {
        // 74
		byte bDevType;                 /* 设备类型,具体见设备类型表 */
		byte bAuxDevType;                  /* 设备子类型 */
		byte bIndex;                       /* 设备序号 */
		byte bDevHardwareVer;              /* 设备硬件版本号 */
		byte bDevSoftwareVer;              /* 设备软件版本号 */
		byte[] szModulename;         /* [21]模块名*/
		byte[] bDevMAC;               /* [6]模块网络MAC地址 */
		byte[] bDevIP;                    /* [4]模块IP地址*/
		byte[] bDevGWIP;              /* [4]模块网关IP */
		byte[] bDevIPMask;                /* [4]模块子网掩码 */
		byte bDhcpEnable;                  /* DHCP 使能，是否启用DHCP,1:启用，0：不启用*/
		ushort wWebPort;                    /* [2]WEB网页地址 */
		byte[] szUsername;                /* [8]用户名同模块名*/
		byte bPassWordEn;                  /*密码使能 1：使能 0： 禁用*/
		byte[] szPassWord;                /* [8]密码*/
		byte bUpdateFlag;                  /* 固件升级标志，1：升级 0：不升级*/
		byte bComcfgEn;                    /*串口协商进入配置模式使能，1：使能 0:不使能 */
		byte[] breserved;             /* [8]保留*/
		private byte[] data;

		int readIndex;
        int writeIndex;


        public DEVICEHW_CONFIG(byte[] parseData)
		{
			this.data = new byte[parseData.Length];
            Array.Copy(parseData, 0, data, 0, data.Length);
			readIndex = 0;

			bDevType = data[readIndex++];

			bAuxDevType = data[readIndex++];

			bIndex = data[readIndex++];

			bDevHardwareVer = data[readIndex++];

			bDevSoftwareVer = data[readIndex++];

			szModulename = new byte[21];
			Array.Copy(data, readIndex, szModulename, 0, szModulename.Length);
			readIndex += szModulename.Length;

			bDevMAC = new byte[6];
			Array.Copy(data, readIndex, bDevMAC, 0, bDevMAC.Length);
			readIndex += bDevMAC.Length;

			bDevIP = new byte[4];
			Array.Copy(data, readIndex, bDevIP, 0, bDevIP.Length);
			readIndex += bDevIP.Length;

			bDevGWIP = new byte[4];
			Array.Copy(data, readIndex, bDevGWIP, 0, bDevGWIP.Length);
			readIndex += bDevGWIP.Length;

			bDevIPMask = new byte[4];
			Array.Copy(data, readIndex, bDevIPMask, 0, bDevIPMask.Length);
			readIndex += bDevIPMask.Length;

			bDhcpEnable = data[readIndex++];

            wWebPort = GetPort();

			szUsername = new byte[8];
			Array.Copy(data, readIndex, szUsername, 0, szUsername.Length);
			readIndex += szUsername.Length;

			bPassWordEn = data[readIndex++];

            szPassWord = new byte[8];
            Array.Copy(data, readIndex, szPassWord, 0, szPassWord.Length);
            readIndex += szPassWord.Length;

            bUpdateFlag = data[readIndex++];

            bComcfgEn = data[readIndex++];

            breserved = new byte[8];
			Array.Copy(data, readIndex, breserved, 0, breserved.Length);
			readIndex += breserved.Length;
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
        public byte[] UpdateForSet()
        {
            byte[] setdata = new byte[74];
            readIndex = 0;

            setdata[readIndex++] = bDevType;

            setdata[readIndex++] = bAuxDevType;

            setdata[readIndex++] = bIndex;

            setdata[readIndex++] = bDevHardwareVer;

            setdata[readIndex++] = bDevSoftwareVer;

            Array.Copy(szModulename, 0, setdata, readIndex, szModulename.Length);
            readIndex += szModulename.Length;

            Array.Copy(bDevMAC, 0, setdata, readIndex, bDevMAC.Length);
            readIndex += bDevMAC.Length;

            Array.Copy(bDevIP, 0, setdata, readIndex, bDevIP.Length);
            readIndex += bDevIP.Length;

            Array.Copy(bDevGWIP, 0, setdata, readIndex, bDevGWIP.Length);
            readIndex += bDevGWIP.Length;

            Array.Copy(bDevIPMask, 0, setdata, readIndex, bDevIPMask.Length);
            readIndex += bDevIPMask.Length;

            setdata[readIndex++] = bDhcpEnable;

            setdata[readIndex++] = (byte)((wWebPort >> 8) & 0xff);
            setdata[readIndex++] = (byte)((wWebPort >> 0) & 0xff);

            Array.Copy(szUsername, 0, setdata, readIndex, szUsername.Length);
            readIndex += szUsername.Length;

            setdata[readIndex++] = bPassWordEn;

            Array.Copy(szPassWord, 0, setdata, readIndex, szPassWord.Length);
            readIndex += szPassWord.Length;

            setdata[readIndex++] = bUpdateFlag;

            setdata[readIndex++] = bComcfgEn;

            Array.Copy(breserved, 0, setdata, readIndex, breserved.Length);
            readIndex += breserved.Length;

            return setdata;
        }

        public byte[] RawData
        {
            get { return data; }
        }

        // 设备类型
        public string DevType
        {
            get
            {
                string strType = "";
                switch (bDevType)
                {
                    case (byte)MODULE_TYPE.TCP_SERVER:
                        strType = "TCP SERVER";
                        break;
                    case (byte)MODULE_TYPE.TCP_CLIENT:
                        strType = "TCP CLIENT";
                        break;
                    case (byte)MODULE_TYPE.UDP_SERVER:
                        strType = "UDP SERVER";
                        break;
                    case (byte)MODULE_TYPE.UDP_CLIENT:
                        strType = "UDP CLIENT";
                        break;
                    default:
                        strType = Convert.ToString(bDevType);
                        break;
                }
                return strType;
            }
        }

        // 设备子类型
        public string AuxDevType
        {
            get
            {
                return Convert.ToString(bAuxDevType);
            }
        }

        // 设备序号
        public string Index
        {
            get
            {
                return Convert.ToString(bIndex);
            }
        }

        // 设备硬件版本号
        public string DevHardwareVer
        {
            get
            {
                return Convert.ToString(bDevHardwareVer);
            }
        }

        // 设备软件版本号
        public string DevSoftwareVer
        {
            get
            {
                return Convert.ToString(bDevSoftwareVer);
            }
        }

        // 模块名
        public string Modulename
        {
            get
            {
                return System.Text.Encoding.Default.GetString(szModulename);
            }

            set 
            {
                byte[] name = System.Text.Encoding.Default.GetBytes(value);
                byte[] new_mod_name = new byte[szModulename.Length];
                Array.Copy(name, 0, new_mod_name, 0, name.Length<=21?name.Length:21);
                Array.Copy(new_mod_name, 0, szModulename, 0, new_mod_name.Length);
            }
        }
        // 模块网络MAC地址
        public string DevMAC
        {
            get
            {
                return CCommondMethod.ToHex(bDevMAC, "", ":");
            }
        }
        // 模块IP地址
        public string DevIP
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
              Convert.ToInt32(bDevIP[0]),
              Convert.ToInt32(bDevIP[1]),
              Convert.ToInt32(bDevIP[2]),
              Convert.ToInt32(bDevIP[3]));
            }

            set 
            {
                byte[] ip = IPAddress.Parse(value).GetAddressBytes();
                Array.Copy(ip, 0, bDevIP, 0, ip.Length);
            }
        }
        // 模块网关IP
        public string DevGWIP
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
              Convert.ToInt32(bDevGWIP[0]),
              Convert.ToInt32(bDevGWIP[1]),
              Convert.ToInt32(bDevGWIP[2]),
              Convert.ToInt32(bDevGWIP[3]));
            }

            set
            {
                byte[] ip = IPAddress.Parse(value).GetAddressBytes();
                Array.Copy(ip, 0, bDevGWIP, 0, bDevGWIP.Length);
            }
        }
        // 模块子网掩码
        public string DevIPMask
        {
            get
            {
                return String.Format("{0}.{1}.{2}.{3}",
              Convert.ToInt32(bDevIPMask[0]),
              Convert.ToInt32(bDevIPMask[1]),
              Convert.ToInt32(bDevIPMask[2]),
              Convert.ToInt32(bDevIPMask[3]));
            }

            set
            {
                byte[] ip = IPAddress.Parse(value).GetAddressBytes();
                Array.Copy(ip, 0, bDevIPMask, 0, bDevIPMask.Length);
            }
        }
        // DHCP 使能，是否启用DHCP,1:启用，0：不启用
        public bool DhcpEnable
        {
            get
            {
                return bDhcpEnable==0x00?false:true;
            }

            set 
            {
                bDhcpEnable = (byte)(value==true?0x01:0x00);
            }
        }
        // WEB网页地址
        public ushort WebPort
        {
            get
            {
                return wWebPort;
            }
        }
        // 用户名同模块名
        public string Username
        {
            get
            {
                return System.Text.Encoding.Default.GetString(szUsername);
            }
        }// 密码使能 1：使能 0： 禁用
        public string PassWordEn
        {
            get
            {
                return Convert.ToString(bPassWordEn);
            }
        }
        // 密码
        public string PassWord
        {
            get
            {
                return System.Text.Encoding.Default.GetString(szPassWord);
            }
        }
        // 固件升级标志，1：升级 0：不升级
        public string UpdateFlag
        {
            get
            {
                return Convert.ToString(bUpdateFlag);
            }
        }
        // 串口协商进入配置模式使能，1：使能 0:不使能
        public string ComcfgEn
        {
            get
            {
                return Convert.ToString(bComcfgEn);
            }
        }// 保留
        public string Reserved
        {
            get
            {
                return CCommondMethod.ToHex(breserved, "", " ");
            }
        }

        public void setu8(int val)
        {
            data[writeIndex++] = (byte)(val & 0xff);
        }

        void setu16(int val)
        {
            data[writeIndex++] = (byte)((val >> 8) & 0xff);
            data[writeIndex++] = (byte)((val >> 0) & 0xff);
        }

        void setu32(int val)
        {
            data[writeIndex++] = (byte)((val >> 24) & 0xff);
            data[writeIndex++] = (byte)((val >> 16) & 0xff);
            data[writeIndex++] = (byte)((val >> 8) & 0xff);
            data[writeIndex++] = (byte)((val >> 0) & 0xff);
        }

        public void setbytes(byte[] array)
        {
            if (array != null)
            {
                setbytes(array, 0, array.Length);
            }
        }

        void setbytes(byte[] array, int start, int length)
        {
            Array.Copy(array, start, data, writeIndex, length);
            writeIndex += length;
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