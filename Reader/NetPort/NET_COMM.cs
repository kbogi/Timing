using System;
using System.Collections.Generic;

namespace Reader
{
    public enum NET_CMD
    {
        //通信命令码
        NET_MODULE_CMD_SET       = 0x01,     //配置网络中的模块
        NET_MODULE_CMD_GET       = 0x02,     //获取某个模块的配置
        NET_MODULE_CMD_RESET     = 0x03,     //复位模块
        NET_MODULE_CMD_SEARCH    = 0x04,     //搜索网络中的模块
        NET_MODULE_CMD_SET_BASE  = 0x05,     //配置模块的端口基本设置
        NET_MODULE_CMD_SET_PORT1 = 0x06,     //配置模块的端口1
        NET_MODULE_CMD_SET_PORT2 = 0x07,     //配置模块的端口2

        NET_MODULE_RESERVE = 0xFF,
        //应答命令码                 
        NET_MODULE_ACK_SET       = 0x81,     //回应配置命令码
        NET_MODULE_ACK_GET       = 0x82,     //回应获取命令码
        NET_MODULE_ACK_RESEST    = 0x83,     //回应复位命令码
        NET_MODULE_ACK_SEARCH    = 0x84,     //回应所搜命令码
        NET_MODULE_ACK_SET_BASE  = 0x85,     //配置模块的端口基本设置
        NET_MODULE_ACK_SET_PORT1 = 0x86,     //配置模块的端口1
        NET_MODULE_ACK_SET_PORT2 = 0x87,	 //配置模块的端口2
    }

    public enum NETPORT_TYPE
    {
        //模块标识
        TCP_SERVER = 0x00,       //模块作为TCP SERVER
        TCP_CLIENT = 0x01,       //模块作为TCP CLIENT
        UDP_SERVER = 0x02,       //模块作为UDP SERVER
        UDP_CLIENT = 0x03,		//模块作为UDP CLIENT
    }

    public enum NETPORT_Baudrate
    {
        //串口波特率: 300---921600bps
        B300    = 300,
        B600    = 600,
        B1200   = 1200,
        B2400   = 2400,
        B4800   = 4800,
        B9600   = 9600,
        B14400  = 14400,
        B19200  = 19200,
        B28400  = 28400,
        B57600  = 57600,
        B115200 = 115200,
        B230400 = 230400,
        B460800 = 460800,
        B921600 = 912600
    }

    public enum NETPORT_DataSize
    {
        //串口数据位: 5---8位
        Bits5 = 5,
        Bits6 = 6,
        Bits7 = 7,
        Bits8 = 8
    }

    public enum NETPORT_StopBits
    {
        //串口停止位: 1表示1个停止位; 2表示2个停止位
        None = 0,
        One = 1,
        Two = 2,
        //OnePointFive = 3
    }

    public enum NETPORT_Parity
    {
        //串口校验位: 0表示奇校验; 1表示偶校验; 2表示标志位(MARK,置1); 3表示空白位(SPACE,清0);
        Odd   = 0,
        Even  = 1,
        Mark  = 2,
        Space = 3, 
        None = 4
    }

    public class NET_COMM
    {
        private string NET_MODULE_FLAG = "NET_MODULE_COMM\0"; // Used to identify the communication _OLD
        private string CH9121_CFG_FLAG = "CH9121_CFG_FLAG\0";   // Used to identify the communication _new
        DeviceHWConfig HWCfg = null; 
        DevicePortConfig[] PortCfg = new DevicePortConfig[2]; 
        FoundDevice foundDevice = null;
        
        public byte[] message;
        public int writeIndex;
        public int readIndex;
        public int optIndex;

        public string Flag { get { return System.Text.Encoding.Default.GetString(getbytes(0, 15)); } } //[16] Communication identifier，"CH9121_CFG_FLAG"
        public string CmdString { get { return Enum.GetName(typeof(NET_CMD), getu8at(16)); } }         //Header
        public int Cmd { get { return getu8at(16); } }
        public string DevMac { get { return getMacAddr(getbytes(17, 6)); } }                           //[6] CH9121MAC Address
        public string PcMac { get { return getMacAddr(getbytes(23, 6)); } }                            //[6] PC 的 MAC Address
        public int Len { get { return getu8at(29); } }                                                 //Data Length

        public int Length { get { return writeIndex; } }

        //[NET_MODULE_DATA_LENGTH] Data area buffer

        //Network parameter structure
        public DeviceHWConfig HWConfig { 
            get
            {
                if (Cmd == (int)NET_CMD.NET_MODULE_ACK_GET 
                    || Cmd == (int)NET_CMD.NET_MODULE_ACK_SET 
                    || Cmd == (int)NET_CMD.NET_MODULE_RESERVE)
                {
                    if (HWCfg == null)
                    {
                        HWCfg = new DeviceHWConfig(getbytes(30, 74));
                    }
                }
                return HWCfg; 
            } 
        }

        //[2]Passthrough channel parameters
        public DevicePortConfig PortCfg_0
        {
            get 
            {
                if (Cmd == (int)NET_CMD.NET_MODULE_ACK_GET 
                    || Cmd == (int)NET_CMD.NET_MODULE_ACK_SET
                    || Cmd == (int)NET_CMD.NET_MODULE_RESERVE)
                {
                    if (PortCfg[0] == null)
                    {
                        PortCfg[0] = new DevicePortConfig(getbytes(104, 65));
                    }
                }
                return PortCfg[0];
            }
        }

        public DevicePortConfig PortCfg_1
        {
            get
            {
                if (Cmd == (int)NET_CMD.NET_MODULE_ACK_GET 
                    || Cmd == (int)NET_CMD.NET_MODULE_ACK_SET
                    || Cmd == (int)NET_CMD.NET_MODULE_RESERVE)
                {
                    if (PortCfg[1] == null)
                    {
                        PortCfg[1] = new DevicePortConfig(getbytes(169, 65));
                    }
                }
                return PortCfg[1];
            }
        }

        public FoundDevice FoundDev
        {
            get 
            {
                if (Cmd == (int)NET_CMD.NET_MODULE_ACK_SEARCH)
                {
                    if (foundDevice == null)
                    {
                        foundDevice = new FoundDevice(getbytes(30, Len+1));
                    }
                }
                return foundDevice;
            }
        }

        public NET_COMM()
        {
            writeIndex = 0;
            message = new byte[285];
        }

        public override string ToString()
        {
            return string.Format(
                $"\r\nFlag ={Flag}, " +
                $"\r\nCmd={CmdString} (0x{Cmd:x2}), " +
                $"\r\nDevMac={DevMac}, PcMac={PcMac}, " +
                $"\r\nLen={Len}, " +
                $"\r\nHWConfig-> {(HWConfig == null ? "null" : HWConfig.ToString())}," +
                $"\r\nPortCfg_0->{(PortCfg_0 == null ? "null" : PortCfg_0.ToString())}, " +
                $"\r\nPortCfg_1->{(PortCfg_1 == null ? "null" : PortCfg_1.ToString())}," +
                $"\r\nFoundDev={(FoundDev == null ? "null" : FoundDev.ToString())}");
        }

        public void setFlag()
        {
            byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
            setbytes(ch9121_cfg_flag);
        }

        string getMacAddr(byte[] bytes)
        {
            return ReaderUtils.ToHex(bytes, "", ":");
        }

        string getIpAddr(byte[] bytes)
        {
            return string.Format("{0}.{1}.{2}.{3}", bytes[0], bytes[1], bytes[2], bytes[3]);
        }

        private int getPortAt(int offset)
        {
            return ((message[offset] & 0xff) << 0)
              | ((message[offset + 1] & 0xff) << 8);
        }

        public void setPort(int val)
        {
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
        }

        int getLengthAt(int offset)
        {
            return ((message[offset] & 0xff) << 0)
              | ((message[offset + 1] & 0xff) << 8)
              | ((message[offset + 2] & 0xff) << 16)
              | ((message[offset + 3] & 0xff) << 24);
        }


        public void setLength(int val)
        {
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
            message[writeIndex++] = (byte)((val >> 16) & 0xff);
            message[writeIndex++] = (byte)((val >> 24) & 0xff);
        }

        public void setu8(int val)
        {
            message[writeIndex++] = (byte)(val & 0xff);
        }

        public void setu16(int val)
        {
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
        }

        void setu24(int val)
        {
            message[writeIndex++] = (byte)((val >> 16) & 0xff);
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
        }

        public void setu32(int val)
        {
            message[writeIndex++] = (byte)((val >> 24) & 0xff);
            message[writeIndex++] = (byte)((val >> 16) & 0xff);
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
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
            Array.Copy(array, start, message, writeIndex, length);
            writeIndex += length;
        }

        int getu8at(int offset)
        {
            return message[offset] & 0xff;
        }

        int getu16at(int offset)
        {
            return ((message[offset] & 0xff) << 8)
              | ((message[offset + 1] & 0xff) << 0);
        }

        int getu24at(int offset)
        {
            return ((message[offset] & 0xff) << 16)
              | ((message[offset + 1] & 0xff) << 8)
              | ((message[offset + 2] & 0xff) << 0);
        }

        int getu32at(int offset)
        {
            return ((message[offset] & 0xff) << 24)
              | ((message[offset + 1] & 0xff) << 16)
              | ((message[offset + 2] & 0xff) << 8)
              | ((message[offset + 3] & 0xff) << 0);
        }

        byte[] getbytes(int offset, int len)
        {
            byte[] bytes = new byte[len];
            Array.Copy(message, offset, bytes, 0, len);
            return bytes;
        }

        public void SetDefaultCOMM()
        {
            
        }
    }
}