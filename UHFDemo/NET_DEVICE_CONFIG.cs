
using System;

namespace UHFDemo
{
    public class NET_DEVICE_CONFIG
    {
        DEVICEHW_CONFIG dev_hw_cfg;
        DEVICEPORT_CONFIG[] dev_port_cfg;// 2
        private byte[] rawData;
        int writeIndex;

        public NET_DEVICE_CONFIG(byte[] mergeData)
        {
            //Console.WriteLine("chris: parse NET_DEVICE_CONFIG");
            rawData = new byte[mergeData.Length];
            Array.Copy(mergeData, 0, rawData, 0, rawData.Length);
            toParseData(rawData);
        }

        public void Update(NET_DEVICE_CONFIG mod_cfg)
        {
            //Console.WriteLine("#1 NET_DEVICE_CONFIG Update");
            writeIndex = 0;
            byte[] dev_hw_data = new byte[74];
            Array.Copy(mod_cfg.rawData, writeIndex, dev_hw_data, 0, dev_hw_data.Length);
            dev_hw_cfg.Update(dev_hw_data);
            dev_hw_cfg.UpdateForSet();
            writeIndex += dev_hw_data.Length;

            for (int i = 0; i < 2; i++)
            {
                //Console.WriteLine("chris: parse DEVICEPORT_CONFIG[{0}]", i);
                byte[] dev_port_data = new byte[65];
                Array.Copy(mod_cfg.rawData, writeIndex, dev_port_data, 0, dev_port_data.Length);
                dev_port_cfg[i].Update(dev_port_data);
                dev_port_cfg[i].UpdateDevCfgForSet();
                writeIndex += dev_port_data.Length;
            }
            //Console.WriteLine("################# end");
        }


        private void toParseData(byte[] mergeData)
        {
            writeIndex = 0;
            byte[] dev_hw_data = new byte[74];
            Array.Copy(mergeData, writeIndex, dev_hw_data, 0, dev_hw_data.Length);
            dev_hw_cfg = new DEVICEHW_CONFIG(dev_hw_data);
            writeIndex += dev_hw_data.Length;


            dev_port_cfg = new DEVICEPORT_CONFIG[2];
            for (int i = 0; i < 2; i++)
            {
                //Console.WriteLine("chris: parse DEVICEPORT_CONFIG[{0}]", i);
                byte[] dev_port_data = new byte[65];
                Array.Copy(mergeData, writeIndex, dev_port_data, 0, dev_port_data.Length);
                dev_port_cfg[i] = new DEVICEPORT_CONFIG(dev_port_data);
                writeIndex += dev_port_data.Length;
            }
        }

        public byte[] RawData
        {
            get { return rawData; }
        }

        public DEVICEHW_CONFIG HW_CONFIG
        {
            get { return dev_hw_cfg; }
        }

        public DEVICEPORT_CONFIG[] PORT_CONFIG
        {
            get { return dev_port_cfg; }
        }
    }
}