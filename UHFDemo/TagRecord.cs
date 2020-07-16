using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace UHFDemo
{
    #region Tag
    public class Tag
    {
        int writeIndex = 0;
        int readIndex = 0;
        int optIndex = 0;

        byte[] _noData = new byte[0];
        private byte[] rawData;
        private byte freqAnt;
        private byte[] pc; // 2
        private byte[] epc; // N
        private byte rssiRaw;
        private byte[] phase; // 2
        private byte[] crc; // 2

        private byte[] data;

        byte antNo;
        byte freq;
        byte rssiH;
        byte rssi;

        byte region;
        byte startFreq;
        byte endFreq;
        byte freqSpace;
        byte freqQuantity;

        private int readcount = 0;
        /// <summary>
        /// Time that search started
        /// </summary>
        internal DateTime _baseTime;
        /// <summary>
        /// Time tag was read, in milliseconds since start of search
        /// </summary>
        internal int _readOffset = 0;
        private bool readPhase = false;

        public Tag(byte[] tagData)
        {
            //Console.WriteLine("Tag tagLen={0}", tagData.Length);
            rawData = new byte[tagData.Length];
            Array.Copy(tagData, 0, rawData, 0, rawData.Length);

            parseFastInvV2TagData();
        }

        public Tag(byte[] tagData, bool readPhase)
        {
            //Console.WriteLine("Tag readPhase tagLen={0}", tagData.Length);
            this.readPhase = readPhase;
            rawData = new byte[tagData.Length];
            Array.Copy(tagData, 0, rawData, 0, rawData.Length);

            //Console.WriteLine("Tag readPhase rawData={0}", CCommondMethod.ToHex(rawData, "", " "));
            parseFastInvV2TagData();
        }

        private void parseFastInvV2TagData()
        {
            //Console.WriteLine("parseFastInvV2TagData tagLen={0}", rawData.Length);
            int tagLen = rawData.Length;
            writeIndex = 0;
            freqAnt = rawData[writeIndex++];
            //Console.WriteLine("#3 parseFastInvV2TagData freqAnt={0:x2}", freqAnt);

            pc = new byte[2];
            Array.Copy(rawData, writeIndex, pc, 0, pc.Length);
            writeIndex += pc.Length;
            //Console.WriteLine("#3 parseFastInvV2TagData PC({1})={0}", CCommondMethod.ToHex(pc, "", " "), pc.Length);

            int epcLen = 0;
            if (readPhase)
            {
                epcLen = tagLen - 6;// freqAnt + pc + rssi + phase
            }
            else
            {
                epcLen = tagLen - 4;// freqAnt + pc + rssi
            }
            epc = new byte[epcLen];
            Array.Copy(rawData, writeIndex, epc, 0, epc.Length);
            writeIndex += epc.Length;
            //Console.WriteLine("#3 parseFastInvV2TagData EPC({1})={0}", CCommondMethod.ToHex(epc, "", " "), epcLen);

            rssiRaw = rawData[writeIndex++];
            //Console.WriteLine("#3 parseFastInvV2TagData rssiRaw={0:x2}", rssiRaw);

            if (readPhase)
            {
                phase = new byte[2];
                Array.Copy(rawData, writeIndex, phase, 0, phase.Length);
            }
            else
            {
                phase = _noData;
            }
            writeIndex += phase.Length;
            //Console.WriteLine("#3 parseFastInvV2TagData phase({1})={0}", CCommondMethod.ToHex(phase, "", " "), phase.Length);

            crc = _noData;

            data = _noData;

            freq = (byte)((freqAnt & 0xFC) >> 2);
            antNo = (byte)(freqAnt & 0x03);
            //Console.WriteLine("#3 parseFastInvV2TagData freq={0:x2}", freq);

            rssiH = (byte)((rssiRaw & 0x80) >> 7);
            rssi = (byte)(rssiRaw & 0x7F);
            //Console.WriteLine("#3 parseFastInvV2TagData rssi={0:x2}", rssi);

            if (rssiH == 0x1) // rssiH == true, 5,6,7,8
            {
                antNo = (byte)(antNo + 0x05);
            }
            else
            {
                antNo = (byte)(antNo + 0x01);
            }
            //Console.WriteLine("#3 parseFastInvV2TagData antNo={0:x2}", antNo);

            readcount = 1;
        }

        public int ReadCount
        {
            get { return readcount; }
            set { readcount = value; }
        }

        public string PC
        {
            get { return CCommondMethod.ToHex(pc, "", " "); }
        }

        public string EPC
        {
            get { return CCommondMethod.ToHex(epc, "", " "); }
        }

        public string Rssi
        {
            //get { return rssi.ToString("X2"); }
            get { return (rssi - 129).ToString(); }
        }

        public string Freq
        {
            //get { return freq.ToString("X2"); }
            get { return String.Format("{0:N2}", (865.0 + 0.5 * freq).ToString("0.00")); }
        }

        public byte FreqByte
        {
            get { return freq; }
        }

        public string Phase
        {
            get { return CCommondMethod.ToHex(phase, "", " "); }
        }

        public string Antenna
        {
            get { return antNo.ToString("X2"); }
        }

        public string Data
        {
            get { return (data.Length == 0 ? "null" : CCommondMethod.ToHex(data, "", "")); }
        }

        public string CRC
        {
            get { return CCommondMethod.ToHex(crc, "", ""); }
        }
    }
    #endregion Tag
    //public class SortableBindingList<T> : BindingList<T>
    //{
    //    protected override bool SupportsSortingCore
    //    {
    //        get
    //        {
    //            return true;
    //        }
    //    }

    //    protected override bool IsSortedCore
    //    {
    //        get
    //        {
    //            return _isSorted;
    //        }
    //    }
    //    private bool _isSorted = false;

    //    protected override PropertyDescriptor SortPropertyCore
    //    {
    //        get
    //        {
    //            return _sortProperty;
    //        }
    //    }
    //    private PropertyDescriptor _sortProperty;

    //    protected override ListSortDirection SortDirectionCore
    //    {
    //        get
    //        {
    //            return _sortDirection;
    //        }
    //    }
    //    private ListSortDirection _sortDirection;

    //    protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
    //    {
    //        if (null != prop.PropertyType.GetInterface("IComparable"))
    //        {
    //            List<T> itemsList = (List<T>)this.Items;
    //            Comparison<T> comparer = GetComparer(prop);
    //            itemsList.Sort(comparer);
    //            if (direction == ListSortDirection.Descending)
    //            {
    //                itemsList.Reverse();
    //            }
    //            _isSorted = true;
    //            _sortProperty = prop;
    //            _sortDirection = direction;
    //            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
    //        }
    //    }

    //    protected virtual Comparison<T> GetComparer(PropertyDescriptor prop)
    //    {
    //        throw new NotImplementedException();
    //    }

    //    protected override void RemoveSortCore() { }

    //    protected override void OnListChanged(ListChangedEventArgs e)
    //    {
    //        if (null != SortPropertyCore)
    //        {
    //            if (!_insideListChangedHandler)
    //            {
    //                _insideListChangedHandler = true;
    //                ApplySortCore(SortPropertyCore, SortDirectionCore);
    //                _insideListChangedHandler = false;
    //            }
    //        }
    //        base.OnListChanged(e);
    //    }
    //    private bool _insideListChangedHandler = false;
    //}
    public class TagRecordBindingList : SortableBindingList<TagRecord>
    {
        protected override Comparison<TagRecord> GetComparer(PropertyDescriptor prop)
        {
            Comparison<TagRecord> comparer = null;
            switch (prop.Name)
            {
                case "SerialNumber":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return (int)(a.SerialNumber - b.SerialNumber);
                    });
                    break;
                case "ReadCount":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return a.ReadCount - b.ReadCount;
                    });
                    break;
                case "EPC":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.EPC, b.EPC);
                    });
                    break;
                case "Rssi":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.Rssi, b.Rssi);
                    });
                    break;
                case "Phase":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.Phase, b.Phase);
                    });
                    break;
                case "Freq":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.Freq, b.Freq);
                    });
                    break;
                case "Antenna":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.Antenna, b.Antenna);
                    });
                    break;
                case "Data":
                    comparer = new Comparison<TagRecord>(delegate (TagRecord a, TagRecord b)
                    {
                        return String.Compare(a.Data, b.Data);
                    });
                    break;
            }
            return comparer;
        }
    }

    #region TagDB
    public class TagDB
    {
        private TagRecordBindingList _tagList = new TagRecordBindingList();

        /// <summary>
        /// EPC index into tag list
        /// </summary>
        private Dictionary<string, TagRecord> EpcIndex = new Dictionary<string, TagRecord>();

        static long UniqueTagCounts = 0;
        static long TotalReadCounts = 0;
        static uint TotalCommandTimes = 0;

        #region 0x79
        byte region;
        byte startFreq;
        byte endFreq;
        byte freqSpace;
        byte freqQuantity;
        #endregion 0x79

        public BindingList<TagRecord> TagList
        {
            get { return _tagList; }
        }
        public long UniqueTagCount
        {
            get { return UniqueTagCounts; }
        }
        public long TotalTagCount
        {
            get { return TotalReadCounts; }
        }

        public uint TotalCommandTime { 
            get { return TotalCommandTimes; } 
        }

        public void UpdateTotalCommandTime(uint commandDuration)
        {
            TotalCommandTimes += commandDuration;
        }

        public void Clear()
        {
            EpcIndex.Clear();
            UniqueTagCounts = 0;
            TotalReadCounts = 0;
            TotalCommandTimes = 0;
            _tagList.Clear();
            // Clear doesn't fire notifications on its own
            _tagList.ResetBindings();
        }

        public void Add(Tag addData)
        {
            lock (new Object())
            {
                string key = null;
                key = addData.EPC;

                UniqueTagCounts = 0;
                TotalReadCounts = 0;

                if (!EpcIndex.ContainsKey(key))
                {
                    //Console.WriteLine("Add key={0}", key);
                    TagRecord value = new TagRecord(addData);
                    value.SerialNumber = (uint)EpcIndex.Count + 1;
                    value.Region = region;
                    value.StartFreq = startFreq;
                    value.EndFreq = endFreq;
                    value.FreqSpace = freqSpace;
                    value.FreqQuantity = freqQuantity;

                    _tagList.Add(value);
                    EpcIndex.Add(key, value);
                    //Call this method to calculate total tag reads and unique tag read counts 
                    UpdateTagCountTextBox(EpcIndex);
                }
                else
                {
                    EpcIndex[key].Update(addData);
                    UpdateTagCountTextBox(EpcIndex);
                }
            }
        }

        //Calculate total tag reads and unique tag reads.
        public void UpdateTagCountTextBox(Dictionary<string, TagRecord> EpcIndex)
        {
            UniqueTagCounts += EpcIndex.Count;
            TagRecord[] dataRecord = new TagRecord[EpcIndex.Count];
            EpcIndex.Values.CopyTo(dataRecord, 0);
            TotalReadCounts = 0;
            for (int i = 0; i < dataRecord.Length; i++)
            {
                TotalReadCounts += dataRecord[i].ReadCount;
            }
        }

        #region 0x79
        public void UpdateRegionInfo(byte[] data)
        {
            int readIndex = 0;
            region = data[readIndex++];
            switch (region)
            {
                case 0x01:
                case 0x02:
                case 0x03:
                    startFreq = data[readIndex++];
                    endFreq = data[readIndex++];
                    freqSpace = 0x05;
                    freqQuantity = (byte)((endFreq - startFreq) / freqSpace);
                    break;
                case 0x04:
                    freqSpace = data[readIndex++];
                    freqQuantity = data[readIndex++];
                    startFreq = data[readIndex++];
                    endFreq = (byte)(startFreq + (freqSpace * 10) * freqQuantity);
                    break;
                default:
                    MessageBox.Show("未定义频段");
                    break;
            }
        }
        #endregion 0x79
    }
    #endregion TagDB
    public class TagRecord : INotifyPropertyChanged
    {
        protected Tag RawRead = null;
        protected bool dataChecked = false;
        protected UInt32 serialNo = 0;

        #region 0x79
        byte region;
        byte startFreq;
        byte endFreq;
        byte freqSpace;
        byte freqQuantity;
        #endregion 0x79

        public TagRecord(Tag newData)
        {
            lock (new Object())
            {
                RawRead = newData;
            }
        }
        /// <summary>
        /// Merge new tag read with existing one
        /// </summary>
        /// <param name="data">New tag read</param>
        public void Update(Tag mergeData)
        {
            //Console.WriteLine("Update {0}", mergeData.EPC);
            mergeData.ReadCount += ReadCount;
            RawRead = mergeData;
            RawRead.ReadCount = mergeData.ReadCount;

            OnPropertyChanged(null);
        }

        public UInt32 SerialNumber
        {
            get { return serialNo; }
            set { serialNo = value; }
        }

        public int ReadCount
        {
            get { return RawRead.ReadCount; }
        }

        public string EPC
        {
            get { return RawRead.EPC; }
        }

        public string Rssi
        {
            get { return RawRead.Rssi; }
        }

        public string Freq
        {
            get {
                //Console.WriteLine("Freq {0:X2}", RawRead.FreqByte);
                //Console.WriteLine("Region {0:X2} [{1:X2} to {2:X2}] space={3:X2} quantity={4:X2}", 
                //    region, startFreq, endFreq, freqSpace, freqQuantity);
                string strFreq;
                switch (region)
                {
                    case 0x01:
                    case 0x02:
                    case 0x03:
                        if (RawRead.FreqByte < 0x07)
                            strFreq = (865 + RawRead.FreqByte * 0.5).ToString("0.00");
                        else
                        {
                            strFreq = (902 + (RawRead.FreqByte - 7) * 0.5).ToString("0.00");
                        }
                        break;
                    case 0x04:
                        strFreq = ((startFreq + (freqSpace * 10) * RawRead.FreqByte) / 1000).ToString("0.00"); 
                        break;
                    default:
                        strFreq = "0.00";
                        MessageBox.Show("未定义频段");
                        break;
                }

                return strFreq;
            }
        }

        public string Phase
        {
            get { return RawRead.Phase; }
        }

        public string Antenna
        {
            get { return RawRead.Antenna; }
        }

        public string Data
        {
            get { return RawRead.Data; }
        }

        public byte Region
        {
            set { region = value; }
        }
        public byte StartFreq
        {
            set { startFreq = value; }
        }
        public byte EndFreq
        {
            set { endFreq = value; }
        }
        public byte FreqSpace
        {
            set { freqSpace = value; }
        }
        public byte FreqQuantity
        {
            set { freqQuantity = value; }
        }

        //public bool Checked
        //{
        //    get { return dataChecked; }
        //    set
        //    {
        //        dataChecked = value;
        //    }
        //}

        #region INotifyPropertyChanged Members

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string name)
        {
            PropertyChangedEventArgs td = new PropertyChangedEventArgs(name);
            try
            {
                if (null != PropertyChanged)
                {
                    PropertyChanged(this, td);
                }
            }
            finally
            {
                td = null;
            }
        }

        #endregion
    }

    #region RfMessage
    //public class RfMessage
    //{
    //    private byte[] rawData;
    //    int writeIndex = 0;
    //    int readIndex = 0;
    //    int optIndex = 0;
    //    byte hdr;
    //    byte len;
    //    byte addr;
    //    byte cmd;
    //    byte[] data;
    //    byte check;
    //    bool readPhase = false;

    //    public bool ReadPhase
    //    {
    //        get { return readPhase; }
    //        set
    //        {
    //            Console.WriteLine("RfMessage set readPhase to {0}", value);
    //            readPhase = value;
    //        }
    //    }

    //    public RfMessage(byte[] data)
    //    {
    //        rawData = new byte[data.Length];
    //        Array.Copy(data, 0, rawData, 0, rawData.Length);

    //        Console.WriteLine("RfMessage {0}, readPhase={1}", CCommondMethod.ToHex(rawData, "", " "), readPhase);
    //        parseMessage();
    //    }

    //    public RfMessage(byte[] data, bool readPhase)
    //    {
    //        this.ReadPhase = readPhase;
    //        rawData = new byte[data.Length];
    //        Array.Copy(data, 0, rawData, 0, rawData.Length);

    //        Console.WriteLine("RfMessage {0}, readPhase={1}", CCommondMethod.ToHex(rawData, "", " "), readPhase);
    //        parseMessage();
    //    }

    //    private void parseMessage()
    //    {
    //        Console.WriteLine("parseMessage");
    //        int msgLen = rawData.Length;
    //        writeIndex = 0;
    //        hdr = rawData[writeIndex++];
    //        Console.WriteLine("#1 RfMessage [{1}]hdr={0:x2}", hdr, writeIndex);

    //        len = rawData[writeIndex++];
    //        Console.WriteLine("#1 RfMessage [{1}]len={0:x2}", len, writeIndex);

    //        addr = rawData[writeIndex++];
    //        Console.WriteLine("#1 RfMessage [{1}]addr={0:x2}", addr, writeIndex);

    //        cmd = rawData[writeIndex++];
    //        Console.WriteLine("#1 RfMessage [{1}]cmd={0:x2}", cmd, writeIndex);

    //        data = new byte[msgLen - 5]; // hdr + len + addr + cmd + check
    //        Array.Copy(rawData, writeIndex, data, 0, data.Length);
    //        writeIndex += data.Length;
    //        Console.WriteLine("#1 RfMessage [{1}]data={0:x2}", CCommondMethod.ToHex(data, "", " "), writeIndex);

    //        check = rawData[writeIndex++];
    //        Console.WriteLine("#1 RfMessage [{1}]check={0:x2}", check, writeIndex);

    //        parseData();
    //    }


    //    private void parseData()
    //    {
    //        switch (cmd)
    //        {
    //            case 0x8A:
    //                parseCmd8AData();
    //                break;
    //            default:
    //                Console.WriteLine("#1 parseData by default!");
    //                break;
    //        }
    //    }

    //    private void parseCmd8AData()
    //    {
    //        switch (len)
    //        {
    //            // failed
    //            case 0x04:
    //                ErrorHandle();
    //                break;
    //            // success
    //            case 0x0A:
    //                // totalRead commandDuration
    //                FastInvV2Success();
    //                break;
    //            // antenna detect
    //            case 0x05:
    //                //天线未连接
    //                // antId ErrorCode
    //                AntennaDetectError();
    //                break;
    //            // get tags
    //            default:
    //                parseTag(readPhase);
    //                break;
    //        }

    //    }

    //    private void parseTag(bool readPhase)
    //    {
    //        Console.WriteLine("parseTag {0}", readPhase);
    //        Tag tag = new Tag(data, readPhase);

    //    }

    //    private void ErrorHandle()
    //    {
    //        // ErrorCode
    //        Console.WriteLine("Errorcode={0}", CCommondMethod.ToHex(data, "0x", " "));
    //    }

    //    private void FastInvV2Success()
    //    {
    //        byte[] bTotalRead = new byte[3];
    //        Array.Copy(data, 0, bTotalRead, 0, bTotalRead.Length);
    //        byte[] bCommandDuration = new byte[4];
    //        Array.Copy(data, 0, bCommandDuration, 0, bCommandDuration.Length);
    //        uint totalRead = CCommondMethod.ToU32(bTotalRead, 0);
    //        uint commandDuration = CCommondMethod.ToU32(bCommandDuration, 0); ;
    //        Console.WriteLine("totalRead={0}, commandDuration={1}", totalRead, commandDuration);
    //    }

    //    private void AntennaDetectError()
    //    {
    //        Console.WriteLine("antId={0}, ErrCode={1}", data[0], data[1]);
    //    }
    //}
    #endregion RfMessage
}