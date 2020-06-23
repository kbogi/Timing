
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
            rawData = new byte[mergeData.Length];
            Array.Copy(mergeData, 0, rawData, 0, rawData.Length);
            writeIndex = 0;
            byte[] dev_hw_data = new byte[74];
            Array.Copy(rawData, writeIndex, dev_hw_data, 0, dev_hw_data.Length);
            dev_hw_cfg = new DEVICEHW_CONFIG(dev_hw_data);
            writeIndex += dev_hw_data.Length;


            dev_port_cfg = new DEVICEPORT_CONFIG[2];
            for (int i=0; i<2; i++)
            {
                byte[] dev_port_data = new byte[65];
                Array.Copy(rawData, writeIndex, dev_port_data, 0, dev_port_data.Length);
                dev_port_cfg[i] = new DEVICEPORT_CONFIG(dev_port_data);
                writeIndex += dev_port_data.Length;
                
            }
        }

        public void Update(NET_DEVICE_CONFIG mod_cfg)
        {
            if(!dev_hw_cfg.Modulename.Equals(mod_cfg.HW_CONFIG.Modulename))
                dev_hw_cfg.Modulename = mod_cfg.HW_CONFIG.Modulename;
            if (!dev_hw_cfg.DevIP.Equals(mod_cfg.HW_CONFIG.DevIP))
                dev_hw_cfg.DevIP = mod_cfg.HW_CONFIG.DevIP;
            if (!dev_hw_cfg.DevIPMask.Equals(mod_cfg.HW_CONFIG.DevIPMask))
                dev_hw_cfg.DevIPMask = mod_cfg.HW_CONFIG.DevIPMask;
            if (!dev_hw_cfg.DevGWIP.Equals(mod_cfg.HW_CONFIG.DevGWIP))
                dev_hw_cfg.DevGWIP = mod_cfg.HW_CONFIG.DevGWIP;
            if (!dev_hw_cfg.DhcpEnable == mod_cfg.HW_CONFIG.DhcpEnable)
                dev_hw_cfg.DhcpEnable = mod_cfg.HW_CONFIG.DhcpEnable;

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