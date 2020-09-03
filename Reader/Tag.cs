using System;

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

        int bufferTagCount = 0;

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

        public Tag(byte[] tagData, byte cmd)
        {
            //Console.WriteLine("Tag tagLen={0}", tagData.Length);
            rawData = new byte[tagData.Length];
            Array.Copy(tagData, 0, rawData, 0, rawData.Length);
            switch (cmd)
            {
                case 0x89:
                case 0x8B:
                case 0x8A:
                    parseInvTagData();
                    break;
                case 0x90:
                case 0x91:
                    parseBufferTagData();
                    break;
                default:
                    break;
            }
        }

        public Tag(byte[] tagData, bool readPhase, byte cmd)
        {
            //Console.WriteLine("Tag cmd={0:X2}, readPhase={1}, tag[{2}] {3}", cmd, readPhase, tagData.Length, ReaderUtils.ToHex(tagData, "", " "));
            this.readPhase = readPhase;
            rawData = new byte[tagData.Length];
            Array.Copy(tagData, 0, rawData, 0, rawData.Length);
            switch (cmd)
            {
                case 0x89:
                case 0x8B:
                case 0x8A:
                    parseInvTagData();
                    break;
                case 0x90:
                case 0x91:
                    parseBufferTagData();
                    break;
                default:
                    break;
            }
        }

        private void parseBufferTagData()
        {
            //Console.WriteLine("parseBufferTagData={0}", ReaderUtils.ToHex(rawData, "", " "));
            //[TagCount][DataLen][Data][Rssi][FreqAnt][ReadCount]
            //[   2    ][  1    ][  N ][ 1  ][   1   ][   1     ]
            //Data:  PC(2字节) + EPC (根据标签规格 + CRC (2字节)) 
            int tagLen = rawData.Length;
            writeIndex = 0;
            //TagCount
            bufferTagCount = ReaderUtils.ToU16(rawData, ref writeIndex);
            //DataLen
            int dataLen = rawData[writeIndex++];

            //PC
            pc = new byte[2];
            Array.Copy(rawData, writeIndex, pc, 0, pc.Length);
            writeIndex += pc.Length;

            int epcLen = 0;
            epcLen = dataLen - 4;// pc + crc
            epc = new byte[epcLen];
            Array.Copy(rawData, writeIndex, epc, 0, epc.Length);
            writeIndex += epc.Length;

            crc = new byte[2];
            Array.Copy(rawData, writeIndex, crc, 0, crc.Length);
            writeIndex += crc.Length;

            rssiRaw = rawData[writeIndex++];

            freqAnt = rawData[writeIndex++];

            readcount = rawData[writeIndex++];

            phase = _noData;

            data = _noData;

            freq = (byte)((freqAnt & 0xFC) >> 2);
            antNo = (byte)(freqAnt & 0x03);
            //Console.WriteLine("#3 parseBufferTagData freq={0:x2}", freq);

            rssiH = (byte)((rssiRaw & 0x80) >> 7);
            rssi = (byte)(rssiRaw & 0x7F);
            //Console.WriteLine("#3 parseBufferTagData rssi={0:x2}", rssi);

            if (rssiH == 0x1) // rssiH == true, 5,6,7,8
            {
                antNo = (byte)(antNo + 0x05);
            }
            else
            {
                antNo = (byte)(antNo + 0x01);
            }
            //Console.WriteLine("#3 parseBufferTagData antNo={0:x2}", antNo);
        }

        private void parseInvTagData()
        {
            //Console.WriteLine("parseInvTagData={0}", ReaderUtils.ToHex(rawData, "", " "));
            //[FreqAnt][PC][EPC][Rssi][Phase]
            //[   1   ][2 ][ N ][ 1  ][   2 ]
            int tagLen = rawData.Length;
            writeIndex = 0;
            //FreqAnt
            freqAnt = rawData[writeIndex++];
            //Console.WriteLine("#3 parseInvTagData freqAnt={0:x2}", freqAnt);

            //PC
            pc = new byte[2];
            Array.Copy(rawData, writeIndex, pc, 0, pc.Length);
            writeIndex += pc.Length;
            //Console.WriteLine("#3 parseInvTagData PC({1})={0}", ReaderUtils.ToHex(pc, "", " "), pc.Length);

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
            //Console.WriteLine("#3 parseInvTagData EPC({1})={0}", ReaderUtils.ToHex(epc, "", " "), epcLen);

            rssiRaw = rawData[writeIndex++];
            //Console.WriteLine("#3 parseInvTagData rssiRaw={0:x2}", rssiRaw);

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
            //Console.WriteLine("#3 parseInvTagData phase({1})={0}", ReaderUtils.ToHex(phase, "", " "), phase.Length);

            crc = _noData;

            data = _noData;

            freq = (byte)((freqAnt & 0xFC) >> 2);
            antNo = (byte)(freqAnt & 0x03);
            //Console.WriteLine("#3 parseInvTagData freq={0:x2}", freq);

            rssiH = (byte)((rssiRaw & 0x80) >> 7);
            rssi = (byte)(rssiRaw & 0x7F);
            //Console.WriteLine("#3 parseInvTagData rssi={0:x2}", rssi);

            if (rssiH == 0x1) // rssiH == true, 5,6,7,8
            {
                antNo = (byte)(antNo + 0x05);
            }
            else
            {
                antNo = (byte)(antNo + 0x01);
            }
            //Console.WriteLine("#3 parseInvTagData antNo={0:x2}", antNo);

            readcount = 1;
        }

        public int BufferTagCount
        {
            get { return bufferTagCount; }
        }
        public int ReadCount
        {
            get { return readcount; }
            set { readcount = value; }
        }

        public string PC
        {
            get { return ReaderUtils.ToHex(pc, "", " "); }
        }

        public string EPC
        {
            get { return ReaderUtils.ToHex(epc, "", " "); }
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
            get { return ReaderUtils.ToHex(phase, "", " "); }
        }

        public string Antenna
        {
            get { return antNo.ToString("X2"); }
        }

        public string Data
        {
            get { return (data.Length == 0 ? "null" : ReaderUtils.ToHex(data, "", "")); }
        }

        public string CRC
        {
            get { return ReaderUtils.ToHex(crc, "", ""); }
        }
    }
    #endregion Tag
}