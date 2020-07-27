using System;

namespace UHFDemo
{
    class NET_COMM
    {
        byte[] message;
        int maxLen = 285;
        // 16 + 1 + 6 + 6 + 1 + 255 = 279
        byte[] flag;     //[16]通信标识，因为都是用广播方式进行通信的，所以这里加一个固定值
        byte cmd;          //命令头
        byte[] mod_mac;        //[6]标识，标识是与某个模块在通信，若与所有的模块通信，则值0XFFFFFF,目标模块mac地址+
        byte[] pc_mac;   //[6]配置软件端的MAC
        byte len;          //数据区长度
        byte[] data;     //[255]数据区缓冲区
        int writeIndex;
        int readIndex;
        int optIndex;
        MODULE_SEARCH mod_search;
        NET_DEVICE_CONFIG net_dev_cfg;

        public byte[] Message { get { return message; } }

        public NET_COMM()
        {
            message = new byte[maxLen];
            writeIndex = 0;
        }

        public NET_COMM(byte[] parseData)
        {
            message = new byte[maxLen];
            writeIndex = 0;
            setbytes(parseData);

            toParseMessage(message);
        }

        public void UpdateMessage()
        {
            //Console.WriteLine("UpdateMessage");
            toParseMessage(message);
            net_dev_cfg = parseCfgData(data);
        }

        private void toParseMessage(byte[] message)
        {
            readIndex = 0;

            flag = new byte[16];
            Array.Copy(message, 0, flag, 0, flag.Length);
            readIndex += flag.Length;
            /* 1 [16]通信标识，因为都是用广播方式进行通信的，所以这里加一个固定值*/
            cmd = message[readIndex++];
            //Console.WriteLine(" <---NET_COMM cmd={0}", cmd);
            /* 2 命令头*/
            mod_mac = new byte[6];
            Array.Copy(message, readIndex, mod_mac, 0, mod_mac.Length);
            readIndex += mod_mac.Length;
            //Console.WriteLine(" <---NET_COMM mod_mac={0}", CCommondMethod.ToHex(mod_mac, "", ":"));
            /* 3 [6]标识，标识是与某个模块在通信，若与所有的模块通信，则值0XFFFFFF,目标模块mac地址+ */
            pc_mac = new byte[6];
            Array.Copy(message, readIndex, pc_mac, 0, pc_mac.Length);
            readIndex += pc_mac.Length;
            //Console.WriteLine(" <---NET_COMM pc_mac={0}", CCommondMethod.ToHex(pc_mac, "", ":"));
            /* 4 [6]配置软件端的MAC*/
            len = message[readIndex++];
            //Console.WriteLine(" <---NET_COMM len={0}", len);
            /* 5 数据区长度*/
            if (cmd == (byte)NET_ACK.NET_MODULE_ACK_SET)
                data = new byte[255];
            else
                data = new byte[len + 1];

            /* 6 [255]数据区缓冲区*/
            Array.Copy(message, readIndex, data, 0, data.Length);
            readIndex += data.Length;

            // 解析data
            if (cmd == (byte)NET_ACK.NET_MODULE_ACK_SEARCH)
            {
                mod_search = parseSearchData(data, mod_mac, pc_mac);
            }
            else if (cmd == (byte)NET_ACK.NET_MODULE_ACK_GET)
            {
                net_dev_cfg = parseCfgData(data);
            }
            else if (cmd == (byte)NET_ACK.NET_MODULE_ACK_SET)
            {
                net_dev_cfg = parseCfgData(data);
            }
            else if (cmd == (byte)NET_ACK.NET_MODULE_ACK_RESEST)
            {

            }
        }

        private NET_DEVICE_CONFIG parseCfgData(byte[] data)
        {
            NET_DEVICE_CONFIG cfg = new NET_DEVICE_CONFIG(data);
            return cfg;
        }

        private MODULE_SEARCH parseSearchData(byte[] parseData, byte[] mod_mac, byte[] pc_mac)
        {
            MODULE_SEARCH dev = new MODULE_SEARCH(parseData, mod_mac, pc_mac);
            return dev;
        }

        public String Flag
        {
            get { return System.Text.Encoding.Default.GetString(flag); }
            set { flag = System.Text.Encoding.Default.GetBytes(value); }
        }

        public byte Cmd
        {
            get { return cmd; }
            set { cmd = value; }
        }

        public String Mod_Mac
        {
            get { return CCommondMethod.ToHex(mod_mac, "", ":"); }
            set
            {
                string param_mod_mac = value.Replace(":", "").ToLower();
                mod_mac = CCommondMethod.FromHex(param_mod_mac);
            }
        }

        public String Pc_Mac
        {
            get 
            {
                return CCommondMethod.ToHex(pc_mac, "", ":"); 
            }
            set
            {
                string param_pc_mac = value.Replace(":", "").ToLower();
                pc_mac = CCommondMethod.FromHex(param_pc_mac);
            }
        }

        public byte Len
        {
            get { return len; }
            set { len = value; }
        }

        public byte[] Data
        {
            set { 
                data = new byte[value.Length];
                Array.Copy(value, 0, data, 0, value.Length);
            }
        }

        public MODULE_SEARCH ModSearch
        {
            get { return mod_search; }
        }

        public NET_DEVICE_CONFIG NetDevCfg
        {
            get { return net_dev_cfg; }
        }
        public void setu8(int val)
        {
            message[writeIndex++] = (byte)(val & 0xff);
        }

        void setu16(int val)
        {
            message[writeIndex++] = (byte)((val >> 8) & 0xff);
            message[writeIndex++] = (byte)((val >> 0) & 0xff);
        }

        void setu32(int val)
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
            Array.Copy(message, readIndex, destination, 0, length);
            readIndex += length;
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
    }
}
 