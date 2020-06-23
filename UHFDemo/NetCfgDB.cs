using System;
using System.Collections.Generic;

namespace UHFDemo
{
    public class NetCfgDB
    {
        Dictionary<string, MODULE_SEARCH> modMacIndexSearch = new Dictionary<string, MODULE_SEARCH>();
        Dictionary<string, NET_DEVICE_CONFIG> modMacIndexNetDevCfg = new Dictionary<string, NET_DEVICE_CONFIG>();

        public Dictionary<string, MODULE_SEARCH> IndexSearch
        {
            get { return modMacIndexSearch; }
        }

        public Dictionary<string, NET_DEVICE_CONFIG> IndexNetDevCfg
        {
            get { return modMacIndexNetDevCfg; }
        }

        public void Add(string mod_mac, MODULE_SEARCH mod_search)
        {
            if (!modMacIndexSearch.ContainsKey(mod_mac))
                modMacIndexSearch.Add(mod_mac, mod_search);
            else
                modMacIndexSearch[mod_mac].Update(mod_search);
        }

        public void Add(string mod_mac, NET_DEVICE_CONFIG mod_cfg)
        {
            if (!modMacIndexNetDevCfg.ContainsKey(mod_mac))
                modMacIndexNetDevCfg.Add(mod_mac, mod_cfg);
            else
                modMacIndexNetDevCfg[mod_mac].Update(mod_cfg);
        }

        public void Clear()
        {
            modMacIndexSearch.Clear();
            modMacIndexNetDevCfg.Clear();
        }
    }
}