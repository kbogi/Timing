using System;

namespace UHFDemo
{
    public class MODULE_CFG
    {
        private byte[] data;

        byte[] module_name;      // [21]模块在网络中的名字
        byte type;                 //标识模块处于那模式(TCP/UDP server/client)
        byte[] src_ip;            // [4]模块本身的IP地址
        byte[] mask;              // [4]模块本身的子网掩码
        byte[] gateway;            // [4]模块对应的网关地址
        uint baud;                 //[4]模块的串口波特率
        byte[] other;             // [3]模块的串口其他配置(校验位、数据位、停止位)
        uint time_out;             //[4]模块的串口超时
        ushort src_port;            //[2]模块源端口
        byte[] dest_ip;           //[4] 目的IP地址
        ushort dest_port;           //[2]目的端口
        byte relink;               //重连次数
        byte clear_buff;			//是否清楚串口缓冲区

        int writeIndex;
        int readIndex;

        public MODULE_CFG(byte[] parseData)
        {
            this.data = new byte[parseData.Length];
            this.data = parseData;
            writeIndex = 0;
            module_name = new byte[21];
            Array.Copy(data, writeIndex, module_name, 0, module_name.Length);
            writeIndex += module_name.Length;

            type = data[writeIndex++];

            src_ip = new byte[4];
            Array.Copy(data, writeIndex, src_ip, 0, src_ip.Length);
            writeIndex += src_ip.Length;

            mask = new byte[4];
            Array.Copy(data, writeIndex, mask, 0, mask.Length);
            writeIndex += mask.Length;

            gateway = new byte[4];
            Array.Copy(data, writeIndex, gateway, 0, gateway.Length);
            writeIndex += gateway.Length;

            baud = (uint)getu32at(writeIndex);
            writeIndex += 4;

            other = new byte[3];
            Array.Copy(data, writeIndex, other, 0, other.Length);
            writeIndex += other.Length;

            time_out = (uint)getu32at(writeIndex);
            writeIndex += 4;

            src_port = (ushort)getu16at(writeIndex);
            writeIndex += 2;

            dest_ip = new byte[4];
            Array.Copy(data, writeIndex, dest_ip, 0, dest_ip.Length);
            writeIndex += dest_ip.Length;

            dest_port = (ushort)getu16at(writeIndex);
            writeIndex += 2;

            relink = data[writeIndex++];

            clear_buff = data[writeIndex++];
        }

        public string Module_Name
        {
            get { return System.Text.Encoding.Default.GetString(module_name); }
        }

        public string Type
        {
            get {
                string strType = "";
                switch(type)
                {
                    case (byte)MODULE_TYPE.NET_MODULE_TYPE_TCP_S:
                        strType = "TCP SERVER";
                        break;
                    case (byte)MODULE_TYPE.NET_MODULE_TYPE_TCP_C:
                        strType = "TCP CLIENT";
                        break;
                    case (byte)MODULE_TYPE.NET_MODULE_TYPE_UDP_S:
                        strType = "UDP SERVER";
                        break;
                    case (byte)MODULE_TYPE.NET_MODULE_TYPE_UDP_C:
                        strType = "UDP CLIENT";
                        break;
                }
                return strType; 
            }
        }

        public string Src_Ip
        {
            get 
            {
                return String.Format("{0}.{1}.{2}.{3}",
              Convert.ToInt32(src_ip[0]),
              Convert.ToInt32(src_ip[1]),
              Convert.ToInt32(src_ip[2]),
              Convert.ToInt32(src_ip[3]));
            }
        }

        public string Mask
        {
            get 
            {

                return String.Format("{0}.{1}.{2}.{3}",
            Convert.ToInt32(mask[0]),
            Convert.ToInt32(mask[1]),
            Convert.ToInt32(mask[2]),
            Convert.ToInt32(mask[3]));
            }
        }

        public string Gateway
        {
            get 
            {
                return String.Format("{0}.{1}.{2}.{3}",
                Convert.ToInt32(gateway[0]),
                Convert.ToInt32(gateway[1]),
                Convert.ToInt32(gateway[2]),
                Convert.ToInt32(gateway[3]));
            }
        }

        public uint Baud
        {
            get { return baud; }
        }

        public string Other
        {
            get { return CCommondMethod.ToHex(other, "", " "); }
        }

        public uint Timeout
        {
            get { return time_out; }
        }

        public ushort Src_Port
        {
            get { return src_port; }
        }

        public string Dest_Ip
        {
            get 
            {
                return String.Format("{0}.{1}.{2}.{3}",
                  Convert.ToInt32(dest_ip[0]),
                  Convert.ToInt32(dest_ip[1]),
                  Convert.ToInt32(dest_ip[2]),
                  Convert.ToInt32(dest_ip[3]));
            }
        }

        public ushort Dest_Port
        {
            get { return dest_port; }
        }

        public byte Relink
        {
            get { return relink; }
        }

        public byte ClearBuf
        {
            get { return clear_buff; }
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

        int getBytesToInt(int offset, byte[] data)
        {
            return ((data[offset] & 0xff) << 24)
              | ((data[offset + 1] & 0xff) << 16)
              | ((data[offset + 2] & 0xff) << 8)
              | ((data[offset + 3] & 0xff) << 0);
        }

    }
}