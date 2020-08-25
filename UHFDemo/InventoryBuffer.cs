using System;
using System.Collections.Generic;
using System.Data;

namespace UHFDemo
{
    class InventoryBuffer
    {
        public byte btRepeat;

        public List<byte> CustomizeSessionParameters;

        public List<byte> lAntenna;
        public int nIndexAntenna;
        public int nCommond;

        
        public int nTagCount;
        public int nDataCount; //执行一次命令所返回的标签记录条数
        public int nReadRate;
        public int nCurrentAnt;
        public List<int> lTotalRead;
        public DateTime dtStartInventory;
        public DateTime dtEndInventory;
        public int nMaxRSSI;
        public int nMinRSSI;

        public InventoryBuffer()
        {

            CustomizeSessionParameters = new List<byte>();
          
            btRepeat = 0x00;
            lAntenna = new List<byte>();
            nIndexAntenna = 0;
            nCommond = 0;

            nTagCount = 0;
            nReadRate = 0;
            lTotalRead = new List<int>();
            dtStartInventory = DateTime.Now;
            dtEndInventory = DateTime.Now;
            nMaxRSSI = 0;
            nMinRSSI = 0;

            
        }

        public void ClearInventoryPar()
        {
            btRepeat = 0x00;
            lAntenna.Clear();
            //bLoopInventory = false;
            nIndexAntenna = 0;
            nCommond = 0;
            CustomizeSessionParameters.Clear();
        }

        public void ClearInventoryResult()
        {
            nTagCount = 0;
            nReadRate = 0;
            lTotalRead.Clear();
            dtStartInventory = DateTime.Now;
            dtEndInventory = DateTime.Now;
            nMaxRSSI = 0;
            nMinRSSI = 0;
        }

        public void ClearInventoryRealResult()
        {
            nTagCount = 0;
            nReadRate = 0;
            lTotalRead.Clear();
            dtStartInventory = DateTime.Now;
            dtEndInventory = DateTime.Now;
            nMaxRSSI = 0;
            nMinRSSI = 0;
        }
    }
}
