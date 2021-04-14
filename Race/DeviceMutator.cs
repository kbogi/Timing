using System;
using System.Data;
using Reader;

namespace Race {
    class DeviceMutator {

        private Device device;
        private Reader.ReaderMethod reader;
        TagDB tagdb = new TagDB();
        TagDB tagOpDb = new TagDB();
        TagMaskDB tagmaskDB = new TagMaskDB();
        JoharTagDB johardb = new JoharTagDB();
        private int WriteTagCount = 0;
        public double elapsedTime = 0.0; 
        DispatcherTimer dispatcherTimer = null;
        DispatcherTimer readratePerSecond = null;
        DateTime startInventoryTime;
        DateTime beforeCmdExecTime;
        private bool isJohar = false;
        bool isFastInv = false;
        bool doingFastInv = false;
        bool Inventorying = false;
        bool isRealInv = false;
        bool doingRealInv = false;
        bool isBufferInv = false;
        bool doingBufferInv = false;
        bool needGetBuffer = false;
        bool use_Phase = false;
        private int tagbufferCount = 0;
        public DeviceMutator(Device device, Reader.ReaderMethod reader) {
            this.device = device;
            this.reader = reader;
        }

        private void WriteLog(string message){
            Console.WriteLine(message);
        }
        private void WriteLog(string message, int error){
            Console.WriteLine(message);
        }

        private void RunLoopISO18000(int nLength) {
            // TODO
        }

        private void RefreshISO18000(byte btCmd) {
            // TODO
        }
        private void RefreshOpTag(byte btCmd) {
            // TODO
        }
        
        private void RefreshInventory(byte btCmd) {
            // TODO
        }
        
        private void RunLoopInventroy(){
            // TODO
        }

        private void RefreshInventoryReal(byte btCmd) {
            //TODO
        }
        private double CalculateElapsedTime()
        {
            TimeSpan elapsed = (DateTime.Now - startInventoryTime);
            // elapsed time + previous cached async read time
            double totalseconds = elapsedTime + elapsed.TotalSeconds;
            WriteLog(string.Format("{0} {1}", Math.Round(totalseconds, 2), "Sec"));
            if(doingBufferInv)
            {
                WriteLog(FormatLongToTimeStr((long)totalseconds));
            }
            return totalseconds;
        }

        private double CalculateExecTime()
        {
            return (DateTime.Now - beforeCmdExecTime).TotalMilliseconds;
        }
        
        private void RefreshFastSwitch(byte btCmd) {
            // TODO
        }

        private void RunLoopFastSwitch() {
            // TODO
        }
        private void RefreshReadSetting(byte btCmd)
        {
            // TODO
        }

        public void ApplyData(object sender, Reader.MessageTran msgTran)
        {
            Console.WriteLine("Packet {0} {1} {2}", msgTran.Cmd, msgTran.PacketType, msgTran.ReadId);
            
            if (msgTran.PacketType != 0xA0)
            {
                return;
            }
            switch (msgTran.Cmd)
            {
                case 0x66:
                    ProcessSetTempOutpower(msgTran);
                    break;
                case 0x69:
                    ProcessSetProfile(msgTran);
                    break;
                case 0x6A:
                    ProcessGetProfile(msgTran);
                    break;
                case 0x6c:
                    ProcessSetReaderAntGroup(msgTran);
                    break;
                case 0x6d:
                    ProcessGetReaderAntGroup(msgTran);
                    break;
                case 0x71:
                    ProcessSetUartBaudrate(msgTran);
                    break;
                case 0x72:
                    ProcessGetFirmwareVersion(msgTran);
                    break;
                case 0x73:
                    ProcessSetReadAddress(msgTran);
                    break;
                case 0x74:
                    ProcessSetWorkAntenna(msgTran);
                    break;
                case 0x75:
                    ProcessGetWorkAntenna(msgTran);
                    break;
                case 0x76:
                    ProcessSetOutputPower(msgTran);
                    break;
                case 0x97:
                case 0x77:
                    ProcessGetOutputPower(msgTran);
                    break;
                case 0x78:
                    ProcessSetFrequencyRegion(msgTran);
                    break;
                case 0x79:
                    ProcessGetFrequencyRegion(msgTran);
                    break;
                case 0x7A:
                    ProcessSetBeeperMode(msgTran);
                    break;
                case 0x7B:
                    ProcessGetReaderTemperature(msgTran);
                    break;
                case 0x7C:
                    ProcessSetDrmMode(msgTran);
                    break;
                case 0x7D:
                    ProcessGetDrmMode(msgTran);
                    break;
                case 0x7E:
                    ProcessGetImpedanceMatch(msgTran);
                    break;
                case 0x60:
                    ProcessReadGpioValue(msgTran);
                    break;
                case 0x61:
                    ProcessWriteGpioValue(msgTran);
                    break;
                case 0x62:
                    ProcessSetAntDetector(msgTran);
                    break;
                case 0x63:
                    ProcessGetAntDetector(msgTran);
                    break;
                case 0x67:
                    ProcessSetReaderIdentifier(msgTran);
                    break;
                case 0x68:
                    ProcessGetReaderIdentifier(msgTran);
                    break;
                case 0x80:
                    ProcessInventory(msgTran);
                    break;
                case 0x81:
                    ProcessReadTag(msgTran);
                    break;
                case 0x82:
                case 0x94:
                    ProcessWriteTag(msgTran);
                    break;
                case 0x83:
                    ProcessLockTag(msgTran);
                    break;
                case 0x84:
                    ProcessKillTag(msgTran);
                    break;
                case 0x85:
                    ProcessSetAccessEpcMatch(msgTran);
                    break;
                case 0x86:
                    ProcessGetAccessEpcMatch(msgTran);
                    break;

                case 0x89:
                case 0x8B:
                    ProcessInventoryReal(msgTran);
                    break;
                case 0x8A:
                    ProcessFastSwitch(msgTran);
                    break;
                case 0x8D:
                    ProcessSetMonzaStatus(msgTran);
                    break;
                case 0x8E:
                    ProcessGetMonzaStatus(msgTran);
                    break;
                case 0x90:
                    ProcessGetInventoryBuffer(msgTran);
                    break;
                case 0x91:
                    ProcessGetAndResetInventoryBuffer(msgTran);
                    break;
                case 0x92:
                    ProcessGetInventoryBufferTagCount(msgTran);
                    break;
                case 0x93:
                    ProcessResetInventoryBuffer(msgTran);
                    break;
                case 0x98:
                    ProcessTagMask(msgTran);
                    break;
                case 0xAA:
                    ProcessGetInternalVersion(msgTran);
                    break;
                case 0xb0:
                    ProcessInventoryISO18000(msgTran);
                    break;
                case 0xb1:
                    ProcessReadTagISO18000(msgTran);
                    break;
                case 0xb2:
                    ProcessWriteTagISO18000(msgTran);
                    break;
                case 0xb3:
                    ProcessLockTagISO18000(msgTran);
                    break;
                case 0xb4:
                    ProcessQueryISO18000(msgTran);
                    break;
                default:
                    break;
            }
        }

        private void ProcessSetTempOutpower(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", "Set Temp Out power");
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd, 0);
                    //FastInventory();
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = string.Format("{0}", "UnknowError");
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, "FailedCause", strErrorCode);
            WriteLog(strLog, 1);
        }

        private void ProcessSetReadAddress(Reader.MessageTran msgTran)
        {
            string strCmd = "Set reader's address";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessGetFirmwareVersion(MessageTran msgTran)
        {
            string strCmd = "Get Reader's firmware version";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                this.device.settings.btMajor = msgTran.AryData[0];
                this.device.settings.btMinor = msgTran.AryData[1];
                this.device.settings.btReadId = msgTran.ReadId;

                RefreshReadSetting(msgTran.Cmd);
                WriteLog(strCmd);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }
        
        private void ProcessGetInternalVersion(MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", "Get internal version");
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                this.device.settings.btInternalVersion = msgTran.AryData[0];
                string fwVersion = String.Format(
                        "{0}.{1}.{2}",
                        this.device.settings.btMajor,
                        this.device.settings.btMinor,
                        BitConverter.ToString(new byte[] { this.device.settings.btInternalVersion }));
                WriteLog(strCmd, 0);
                WriteLog(fwVersion, 0);
                return;
            }
            else
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
            WriteLog(strLog, 1);
        }

        private void ProcessSetUartBaudrate(Reader.MessageTran msgTran)
        {
            string strCmd = "Set Baudrate";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }


        private void ProcessGetReaderTemperature(Reader.MessageTran msgTran)
        {
            string strCmd = "Get reader internal temperature";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                this.device.settings.btReadId = msgTran.ReadId;
                this.device.settings.btPlusMinus = msgTran.AryData[0];
                this.device.settings.btTemperature = msgTran.AryData[1];

                RefreshReadSetting(msgTran.Cmd);
                WriteLog(strCmd);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessGetOutputPower(Reader.MessageTran msgTran)
        {
            string strCmd = "Get RF Output Power";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                this.device.settings.btReadId = msgTran.ReadId;
                this.device.settings.btOutputPower = msgTran.AryData[0];

                RefreshReadSetting(0x77);
                WriteLog(strCmd);
                return;
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessSetOutputPower(Reader.MessageTran msgTran)
        {
            string strCmd = "Set RF Output Power";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessGetWorkAntenna(Reader.MessageTran msgTran)
        {
            string strCmd = "Get working antenna";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01 || msgTran.AryData[0] == 0x02 || msgTran.AryData[0] == 0x03)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btWorkAntenna = msgTran.AryData[0];

                    RefreshReadSetting(0x75);
                    WriteLog(strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessSetWorkAntenna(Reader.MessageTran msgTran)
        {
            int intCurrentAnt = 0;
            intCurrentAnt = this.device.settings.btWorkAntenna + 1;
            string strCmd = "Set working antenna successfully, Current Ant: Ant" + intCurrentAnt.ToString();

            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    //Verify inventory operations
                    if (this.device.inventory)
                    {
                        RunLoopInventroy();
                    }
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);

            if(Inventorying)
            {
                if(isRealInv)
                {
                    stopRealInv();
                }
                else if (isFastInv)
                {
                    stopFastInv();
                }
                else if (isBufferInv)
                {
                    stopBufferInv();
                }
            }
        }
        private void ProcessGetDrmMode(Reader.MessageTran msgTran)
        {
            string strCmd = "Get DRM Status";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btDrmMode = msgTran.AryData[0];

                    RefreshReadSetting(0x7D);
                    WriteLog(strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessSetDrmMode(Reader.MessageTran msgTran)
        {
            string strCmd = "Set DRM Status";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private string GetFreqString(byte btFreq)
        {
            string strFreq = string.Empty;

            if (this.device.settings.btRegion == 4)
            {
                float nExtraFrequency = btFreq * this.device.settings.btUserDefineFrequencyInterval * 10;
                float nstartFrequency = ((float)this.device.settings.nUserDefineStartFrequency) / 1000;
                float nStart = nstartFrequency + nExtraFrequency / 1000;
                string strTemp = nStart.ToString("0.000");
                return strTemp;
            }
            else
            {
                if (btFreq < 0x07)
                {
                    float nStart = 865.00f + Convert.ToInt32(btFreq) * 0.5f;

                    string strTemp = nStart.ToString("0.00");

                    return strTemp;
                }
                else
                {
                    float nStart = 902.00f + (Convert.ToInt32(btFreq) - 7) * 0.5f;

                    string strTemp = nStart.ToString("0.00");

                    return strTemp;
                }
            }
        }

        private void ProcessGetFrequencyRegion(Reader.MessageTran msgTran)
            {
                string strCmd = string.Format("{0}", "Get Frequency Region");
                string strErrorCode = string.Empty;

                parseGetFrequencyRegion(msgTran.AryData);

                if (msgTran.AryData.Length == 3)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btRegion = msgTran.AryData[0];
                    this.device.settings.btFrequencyStart = msgTran.AryData[1];
                    this.device.settings.btFrequencyEnd = msgTran.AryData[2];

                    RefreshReadSetting(0x79);
                    WriteLog(strCmd, 0);
                    return;
                }
                else if (msgTran.AryData.Length == 6)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btRegion = msgTran.AryData[0];
                    this.device.settings.btUserDefineFrequencyInterval = msgTran.AryData[1];
                    this.device.settings.btUserDefineChannelQuantity = msgTran.AryData[2];
                    this.device.settings.nUserDefineStartFrequency = msgTran.AryData[3] * 256 * 256 + msgTran.AryData[4] * 256 + msgTran.AryData[5];
                    RefreshReadSetting(0x79);
                    WriteLog(strCmd, 0);
                    return;


                }
                else if (msgTran.AryData.Length == 1)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
                else
                {
                    strErrorCode = string.Format("{0}", "Unknown Error");
                }

                string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
                WriteLog(strLog, 1);
            }

        private void ProcessSetFrequencyRegion(Reader.MessageTran msgTran)
        {
            string strCmd = "Set RF frequency spectrum";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessSetBeeperMode(Reader.MessageTran msgTran)
        {
            string strCmd = "Set reader's buzzer hehavior";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessReadGpioValue(Reader.MessageTran msgTran)
        {
            string strCmd = "Get GPIO status";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                this.device.settings.btReadId = msgTran.ReadId;
                this.device.settings.btGpio1Value = msgTran.AryData[0];
                this.device.settings.btGpio2Value = msgTran.AryData[1];

                RefreshReadSetting(0x60);
                WriteLog(strCmd);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessWriteGpioValue(Reader.MessageTran msgTran)
        {
            string strCmd = "Set GPIO status";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessGetAntDetector(Reader.MessageTran msgTran)
        {
            string strCmd = "Get antenna detector threshold value";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                this.device.settings.btReadId = msgTran.ReadId;
                this.device.settings.btAntDetector = msgTran.AryData[0];

                RefreshReadSetting(0x63);
                WriteLog(strCmd);
                return;
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }
        private void ProcessGetMonzaStatus(Reader.MessageTran msgTran)
        {
            string strCmd = "Get current Impinj FastTID setting";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x8D)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btMonzaStatus = msgTran.AryData[0];

                    RefreshReadSetting(0x8E);
                    WriteLog(strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessSetMonzaStatus(Reader.MessageTran msgTran)
        {
            string strCmd = "Set Impinj FastTID function";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btAntDetector = msgTran.AryData[0];

                    WriteLog(strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }



            private void ProcessSetProfile(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", "Set profile");
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btLinkProfile = msgTran.AryData[0];

                    WriteLog( strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = string.Format("{0}", "Unknown Error");
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
            WriteLog( strLog, 1);
        }

        private void ProcessGetProfile(Reader.MessageTran msgTran)
        {
            string strCmd = "Get RF link profile";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if ((msgTran.AryData[0] >= 0xd0) && (msgTran.AryData[0] <= 0xd3))
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btLinkProfile = msgTran.AryData[0];

                    RefreshReadSetting(0x6A);
                    WriteLog( strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);;
        }

        private void ProcessSetReaderAntGroup(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0} {1}", "Set reader ant group", tagdb.AntGroup);
            string strErrorCode = "";

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog(strCmd, 0);

                    /*
                    if (m_setWorkAnt)
                    {
                        m_setWorkAnt = false;
                        byte btWorkAntenna = (byte)cmbWorkAnt.SelectedIndex;
                        if (btWorkAntenna >= 0x08)
                            btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                        reader.SetWorkAntenna(this.device.settings.btReadId, btWorkAntenna);
                        this.device.settings.btWorkAntenna = btWorkAntenna;
                        return;
                    }
                    if (m_getOutputPower)
                    {
                        reader.GetOutputPower(this.device.settings.btReadId);
                        return;
                    }
                    if (m_setOutputPower)
                    {
                            /*
                        if (tagdb.AntGroup == 0x00)
                        {
                            if (tb_outputpower_1.Text.Length != 0 || tb_outputpower_2.Text.Length != 0 || tb_outputpower_3.Text.Length != 0 || tb_outputpower_4.Text.Length != 0
                               || tb_outputpower_5.Text.Length != 0 || tb_outputpower_6.Text.Length != 0 || tb_outputpower_7.Text.Length != 0 || tb_outputpower_8.Text.Length != 0)
                            {
                                byte[] OutputPower = new byte[8];
                                OutputPower[0] = Convert.ToByte(tb_outputpower_1.Text);
                                OutputPower[1] = Convert.ToByte(tb_outputpower_2.Text);
                                OutputPower[2] = Convert.ToByte(tb_outputpower_3.Text);
                                OutputPower[3] = Convert.ToByte(tb_outputpower_4.Text);
                                OutputPower[4] = Convert.ToByte(tb_outputpower_5.Text);
                                OutputPower[5] = Convert.ToByte(tb_outputpower_6.Text);
                                OutputPower[6] = Convert.ToByte(tb_outputpower_7.Text);
                                OutputPower[7] = Convert.ToByte(tb_outputpower_8.Text);

                                reader.SetOutputPower(this.device.settings.btReadId, OutputPower);
                            }
                        }
                        else
                        {
                            if (tb_outputpower_9.Text.Length != 0 || tb_outputpower_10.Text.Length != 0 || tb_outputpower_11.Text.Length != 0 || tb_outputpower_12.Text.Length != 0
                               || tb_outputpower_13.Text.Length != 0 || tb_outputpower_14.Text.Length != 0 || tb_outputpower_15.Text.Length != 0 || tb_outputpower_16.Text.Length != 0)
                            {
                                byte[] OutputPower = new byte[8];
                                OutputPower[0] = Convert.ToByte(tb_outputpower_9.Text);
                                OutputPower[1] = Convert.ToByte(tb_outputpower_10.Text);
                                OutputPower[2] = Convert.ToByte(tb_outputpower_11.Text);
                                OutputPower[3] = Convert.ToByte(tb_outputpower_12.Text);
                                OutputPower[4] = Convert.ToByte(tb_outputpower_13.Text);
                                OutputPower[5] = Convert.ToByte(tb_outputpower_14.Text);
                                OutputPower[6] = Convert.ToByte(tb_outputpower_15.Text);
                                OutputPower[7] = Convert.ToByte(tb_outputpower_16.Text);

                                reader.SetOutputPower(this.device.settings.btReadId, OutputPower);
                            }
                        }
                        return;
                    }
                    if (doingRealInv)
                    {
                        reader.SetWorkAntenna(this.device.settings.btReadId, this.device.settings.btWorkAntenna);
                    }
                    else if(doingFastInv)
                    {
                        //cmdFastInventorySend(useAntG1); todo
                    }
                    else if(doingBufferInv)
                    {
                        reader.SetWorkAntenna(this.device.settings.btReadId, this.device.settings.btWorkAntenna);
                    }
                    return;
                    */
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = string.Format("{0}", "Unknow Error");
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
            WriteLog(strLog, 1);
        }

        private void ProcessGetReaderAntGroup(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", "Get reader ant group");
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    tagdb.AntGroup = msgTran.AryData[0];
                    if(tagdb.AntGroup==0x01)
                    {
                        /*
                        if(!antType16.Checked)
                            antType16.Checked = true;
                            */
                    }
                    if (device.getWorkAnt)
                    {
                        device.getWorkAnt = false;
                        reader.GetWorkAntenna(this.device.settings.btReadId);
                    }
                    WriteLog(strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = string.Format("{0}", "Unknow Error");
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
            WriteLog(strLog, 1);
        }



        private void ProcessGetReaderIdentifier(Reader.MessageTran msgTran)
        {
            string strCmd = "Get Reader Identifier";
            string strErrorCode = string.Empty;
            short i;
            string readerIdentifier = "";
            
            if (msgTran.AryData.Length == 12)
            {
                this.device.settings.btReadId = msgTran.ReadId;
                for (i = 0; i < 12; i ++)
                {
                    readerIdentifier = readerIdentifier + string.Format("{0:X2}", msgTran.AryData[i]) + " ";

                    
                }
                this.device.settings.btReaderIdentifier = readerIdentifier;
                RefreshReadSetting(0x68);
                
                WriteLog( strCmd);
                return;
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);
        }

        private void ProcessGetImpedanceMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "Measure Impedance of Antenna Port Match";
            string strErrorCode = string.Empty;
                  
            
            if (msgTran.AryData.Length == 1)
            {
                this.device.settings.btReadId = msgTran.ReadId;

                this.device.settings.btAntImpedance = msgTran.AryData[0];
                RefreshReadSetting(0x7E);
                
                WriteLog( strCmd);
                return;
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);;
        }
        
        private void ProcessSetReaderIdentifier(Reader.MessageTran msgTran)
        {
            string strCmd = "Set Reader Identifier";
            string strErrorCode = string.Empty;
            
            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog( strCmd);
                    return;
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);;
        }

        private void ProcessSetAntDetector(Reader.MessageTran msgTran)
        {
            string strCmd = "Set antenna detector threshold value";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    WriteLog( strCmd);

                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);;
        }

        private void ProcessFastSwitch(Reader.MessageTran msgTran)
        {
            string strCmd = "Real time inventory with fast ant switch";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
                if (isFastInv)
                {
                    //FastInventory(); todo
                }
                else
                {
                    stopFastInv();
                }
            }
            else if (msgTran.AryData.Length == 2)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[1]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode + "--" + "Antenna" + (msgTran.AryData[0] + 1);

                WriteLog( strCmd, 1);;
            }
            else if (msgTran.AryData.Length == 7)
            {
                if(doingFastInv)
                {
                    WriteLog(strCmd, 0);

                    tagdb.UpdateCmd8AExecuteSuccess(msgTran.AryData);
                    WriteLog(string.Format("readrate: {0}, tagreads: {1}, tagCount: {2}, tagDuration{3}, totalExecutionTime{4}",
                        tagdb.CmdReadRate.ToString(),
                        tagdb.TotalTagCounts.ToString(),
                        tagdb.CmdTotalRead.ToString(),
                        tagdb.CommandDuration.ToString(),
                        FormatLongToTimeStr(tagdb.TotalCommandTime)
                    ));

                    if (isFastInv)
                    {
                        // FastInventory();todo
                    }
                    else
                    {
                        stopFastInv();
                    }
                }
            }
            else
            {
                if (doingFastInv)
                {
                    parseInvTag(use_Phase, msgTran.AryData, 0x8a);
                }
            }
        }

        private void stopFastInv()
        {
            doingFastInv = false;
            Inventorying = false;
        }

        private void stopRealInv()
        {
            doingRealInv = false;
            Inventorying = false;
        }

        private void stopBufferInv()
        {
            doingBufferInv = false;
            Inventorying = false;
            // real stop bufferInv
        }

        private void ProcessInventoryReal(Reader.MessageTran msgTran)
        {
            string strCmd = "";
            if (msgTran.Cmd == 0x89)
            {
                strCmd = "Real time inventory";
            }
            if (msgTran.Cmd == 0x8B)
            {
                strCmd = "User define Session and Inventoried Flag inventory";
            }
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                if (this.device.fastExeCount != -1)
                {
                    if (this.device.fastExeCount > 1)
                    {
                        this.device.fastExeCount--;
                    }
                    else
                    {
                        isRealInv = false;
                    }
                }

                if (!isRealInv)
                {
                    stopRealInv();
                }
                if (doingRealInv)
                    RunLoopInventroy();

            }
            else if (msgTran.AryData.Length == 7)
            {
                WriteLog(strCmd, 0);

                tagdb.UpdateCmd89ExecuteSuccess(msgTran.AryData);
                WriteLog(string.Format("readrate: {0}, tagreads: {1}, tagCount: {2}, tagDuration{3}, totalExecutionTime{4}",
                    tagdb.CmdReadRate.ToString(),
                    tagdb.TotalTagCounts.ToString(),
                    tagdb.CmdTotalRead.ToString(),
                    tagdb.CommandDuration.ToString(),
                    FormatLongToTimeStr(tagdb.TotalCommandTime)
                ));

                if (this.device.fastExeCount != -1)
                {
                    if (this.device.fastExeCount > 1)
                    {
                        this.device.fastExeCount--;
                    }
                    else
                    {
                        isRealInv = false;
                    }
                }
                
                if (!isRealInv)
                {
                    stopRealInv();
                }
                if (doingRealInv)
                    RunLoopInventroy();
            }
            else
            {
                parseInvTag(use_Phase, msgTran.AryData, 0x89);
            }
        }

        private void ProcessInventory(Reader.MessageTran msgTran)
        {
            string strCmd = "Inventory";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 9)
            {
                WriteLog(strCmd, 0);
                tagdb.UpdateCmd80ExecuteSuccess(msgTran.AryData);
                WriteLog(string.Format("readrate: {0}, tagreads: {1}, tagCount: {2}, tagDuration{3}, totalExecutionTime{4}",
                    tagdb.CmdReadRate.ToString(),
                    tagdb.TotalTagCounts.ToString(),
                    tagdb.CmdTotalRead.ToString(),
                    tagdb.CommandDuration.ToString(),
                    FormatLongToTimeStr(tagdb.TotalCommandTime)
                ));

                if (this.device.fastExeCount != -1)
                {
                    if (this.device.fastExeCount > 1)
                    {
                        this.device.fastExeCount--;
                    }
                    else
                    {
                        isBufferInv = false;
                    }
                }

                if (!isBufferInv)
                {
                    stopBufferInv();
                }
                if (doingBufferInv)
                    RunLoopInventroy();
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);

            if (this.device.fastExeCount != -1)
            {
                if (this.device.fastExeCount > 1)
                {
                    this.device.fastExeCount--;
                }
                else
                {
                    isBufferInv = false;
                }
            }

            if (!isBufferInv)
            {
                stopBufferInv();
            }
            if (doingBufferInv)
                RunLoopInventroy();
        }

        private void SetMaxMinRSSI(int nRSSI)
        {
            if (tagdb.MinRSSI == 0 && tagdb.MinRSSI == 0)
            {
                tagdb.MaxRSSI = nRSSI;
                tagdb.MinRSSI = nRSSI;
            }
            else
            {
                if (tagdb.MaxRSSI < nRSSI)
                {
                    tagdb.MaxRSSI = nRSSI;
                }

                if (tagdb.MinRSSI > nRSSI)
                {
                    tagdb.MinRSSI = nRSSI;
                }
            }
        }

        private void ProcessGetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "Get buffered data without clearing";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);
                stopGetInventoryBuffer(false);
            }
            else
            {
                parseInvTag(false, msgTran.AryData, 0x90);
                WriteLog( strCmd);
            }
        }

        private void ProcessGetAndResetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "Get and clear buffered data";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                WriteLog(strCmd, 0);
                parseInvTag(false, msgTran.AryData, 0x91);
            }
        }

        private void stopGetInventoryBuffer(bool clearBuffer)
        {
            tagbufferCount = 0;
            needGetBuffer = false;
            if (clearBuffer)
            {
                WriteLog("Get and clear buffer");
            }
            else
            {
                WriteLog("Get buffer");
            }
            dispatcherTimer.Stop();
            readratePerSecond.Stop();
            elapsedTime = CalculateElapsedTime();

            tagdb.Repaint();
        }

        private void ProcessGetInventoryBufferTagCount(Reader.MessageTran msgTran)
        {
            string strCmd = "Query how many tags are buffered";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                int nTagCount = Convert.ToInt32(msgTran.AryData[0]) * 256 + Convert.ToInt32(msgTran.AryData[1]);
                string strLog1 = strCmd + " " + nTagCount.ToString();
                WriteLog(strLog1);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

            WriteLog( strCmd, 1);;
        }

        private void ProcessResetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "Clear buffer";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    RefreshInventory(0x93);
                    WriteLog( strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

            WriteLog( strCmd, 1);;
        }

        private void ProcessGetAccessEpcMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "Get selected tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x01)
                {
                    WriteLog("Unselected Tag");
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                if (msgTran.AryData[0] == 0x00)
                {
                    WriteLog(string.Format("{0} ({1}){2}",
                        strCmd,
                        Convert.ToInt32(msgTran.AryData[2]),
                        ReaderUtils.ByteArrayToString(msgTran.AryData, 2, Convert.ToInt32(msgTran.AryData[1]))), 0);
                    
                    return;
                }
                else
                {
                    strErrorCode = "Unknown Error";
                }
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

            WriteLog( strCmd, 1);
        }

        private void ProcessSetAccessEpcMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "Select/Deselect Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    WriteLog( strCmd);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

            WriteLog( strCmd, 1);;
        }

        private void ProcessReadTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Read Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                if (isJohar)
                {
                    parseJoharRead(msgTran.AryTranData);
                }
                else
                {
                    AddTagToTagOpDb(msgTran);

                    WriteLog(strCmd, 0);
                }
            }
        }

        private void ProcessWriteTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Write Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2])  - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                    return;
                }
                WriteTagCount++;

                AddTagToTagOpDb(msgTran);

                WriteLog( strCmd);
                if (WriteTagCount == (msgTran.AryData[0] * 256 + msgTran.AryData[1]))
                {
                    WriteTagCount = 0;
                }
            }
        }

        private void ProcessLockTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Lock Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);
                    return;
                }

                AddTagToTagOpDb(msgTran);

                WriteLog( strCmd);
            }
        }

        private void ProcessKillTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Kill Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                    return;
                }

                AddTagToTagOpDb(msgTran);
                WriteLog( strCmd);
            }
        }

        private void AddTagToTagOpDb(MessageTran msgTran)
        {
            tagOpDb.Add(new Tag(msgTran.AryData, msgTran.Cmd, tagdb.AntGroup));
        }

        private void ProcessInventoryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Inventory";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] != 0xFF)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                }                
            }
            else if (msgTran.AryData.Length == 9)
            {
                string strAntID = ReaderUtils.ByteArrayToString(msgTran.AryData, 0, 1);
                string strUID = ReaderUtils.ByteArrayToString(msgTran.AryData, 1, 8);

                //Add saved Tag List, no inventoried add recording, otherwise, the tag inventory number plus 1.
                DataRow[] drs = this.device.operateTagISO18000Buffer.dtTagTable.Select(string.Format("UID = '{0}'", strUID));
                if (drs.Length == 0)
                {
                    DataRow row = this.device.operateTagISO18000Buffer.dtTagTable.NewRow();
                    row[0] = strAntID;
                    row[1] = strUID;
                    row[2] = "1";
                    this.device.operateTagISO18000Buffer.dtTagTable.Rows.Add(row);
                    this.device.operateTagISO18000Buffer.dtTagTable.AcceptChanges();
                }
                else
                {
                    DataRow row = drs[0];
                    row.BeginEdit();
                    row[2] = (Convert.ToInt32(row[2]) + 1).ToString();
                    this.device.operateTagISO18000Buffer.dtTagTable.AcceptChanges();
                }
                
            }
            else if (msgTran.AryData.Length == 2)
            {
                this.device.operateTagISO18000Buffer.nTagCnt = Convert.ToInt32(msgTran.AryData[1]);
                RefreshISO18000(msgTran.Cmd);

                //WriteLog( strCmd);
            }
            else
            {
                strErrorCode = "Unknown Error";
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
        }

        private void ProcessReadTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Read Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                string strAntID = ReaderUtils.ByteArrayToString(msgTran.AryData, 0, 1);
                string strData = ReaderUtils.ByteArrayToString(msgTran.AryData, 1, msgTran.AryData.Length - 1);

                this.device.operateTagISO18000Buffer.btAntId = Convert.ToByte(strAntID);
                this.device.operateTagISO18000Buffer.strReadData = strData;

                RefreshISO18000(msgTran.Cmd);

                WriteLog( strCmd);
            }
        }

        private void ProcessWriteTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Write Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                //string strAntID = ReaderUtils.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strCnt = ReaderUtils.ByteArrayToString(msgTran.AryData, 1, 1);

                this.device.operateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                this.device.operateTagISO18000Buffer.btWriteLength = msgTran.AryData[1];

                //RefreshISO18000(msgTran.Cmd);
                string strLength = msgTran.AryData[1].ToString();
                string strLog = strCmd + ": " + "Successfully written" + strLength + "byte";
                WriteLog( strCmd);
                RunLoopISO18000(Convert.ToInt32(msgTran.AryData[1]));
            }
        }

        private void ProcessLockTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Permanent write protection";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);
            }
            else
            {
                //string strAntID = ReaderUtils.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = ReaderUtils.ByteArrayToString(msgTran.AryData, 1, 1);

                this.device.operateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                this.device.operateTagISO18000Buffer.btStatus = msgTran.AryData[1];

                //RefreshISO18000(msgTran.Cmd);
                string strLog = string.Empty; 
                switch (msgTran.AryData[1])
                {
                    case 0x00:
                        strLog = strCmd + ": " + "Successfully locked";
                        break;
                    case 0xFE:
                        strLog = strCmd + ": " + "It is already locked state";
                        break;
                    case 0xFF:
                        strLog = strCmd + ": " + "Unable to lock";
                        break;
                    default:
                        break;
                }

                WriteLog( strCmd);
                
            }
        }
        private void parseJoharRead(byte[] aryTranData)
                {
                    //Console.WriteLine("parseJoharRead totalread={0}, tagcount={1}", johardb.TotalTagCount, johardb.UniqueTagCount);
                    JoharTag tag = new JoharTag(aryTranData);
                    johardb.Add(tag);

                    WriteLog( string.Format(
                        "Johar count: {0}, unique count:{1}",
                        johardb.TotalTagCount.ToString(),
                        johardb.UniqueTagCount.ToString()
                    ));
                }

        private void ProcessQueryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Query Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);
            }
            else
            {
                //string strAntID = ReaderUtils.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = ReaderUtils.ByteArrayToString(msgTran.AryData, 1, 1);

                this.device.operateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                this.device.operateTagISO18000Buffer.btStatus = msgTran.AryData[1];

                RefreshISO18000(msgTran.Cmd);

                WriteLog( strCmd);
            }
        }

        private void setInvStoppedStatus()
        {
            dispatcherTimer.Stop();
            readratePerSecond.Stop();
            elapsedTime = CalculateElapsedTime();
            
            tagdb.Repaint();
        }

        private void ProcessTagMask(Reader.MessageTran msgTran)
        {
            //Set
            //Head	Len	Address	Cmd	ErrorCode	Check
            //0xA0    0x04         0x98
            //Clear
            //Head	Len	Address	Cmd	ErrorCode	Check
            //0xA0    0x04         0x98

            //Query
            //Head Len Add Cmd     Mask ID MaskQuantity   Target Action  Membank StartingMaskAdd MaskBitLen Mask    Truncate CC
            //0xA0         0x98

            //Head	Len	 Add	Cmd 	MaskQuantity	CC
            //0xA0  0x0B        0x98    0x00
            string strCmd = string.Format("{0}", "TagMask");
            string strErrorCode = string.Empty;
            if(msgTran.AryTranData[1] == 0x04)
            {
                //Error
                //clear mask result
                if (msgTran.AryData[0] == 0x10)
                {
                    strCmd += ": " + "ExecSuccess";
                    WriteLog(strCmd, 0);
                    return;
                }
                //query mask result, no mask case
                else if (msgTran.AryData[0] == 0x00)
                {
                    WriteLog(string.Format("{0} {1}", strCmd, "NoTagMask"), 0);
                    return;
                }
                else if (msgTran.AryData[1] == (byte)0x41)
                {
                    strErrorCode = "InvalidParameter";
                }
                else
                {
                    strErrorCode = string.Format("{0} {1:x2}", "Unknow Error", msgTran.AryData[1]);
                }
            }
            else
            {
                //Mask ID MaskQuantity Target Action Membank StartingMaskAdd MaskBitLen Mask Truncate
                if (msgTran.AryData.Length > 7)
                {
                    TagMask tagMask = new TagMask(msgTran.AryData);
                    Console.WriteLine(string.Format("tagmask={0}", tagMask.ToString()));

                    tagmaskDB.Add(tagMask);
                    return;
                }
                else
                {
                    strErrorCode = string.Format("{0} [{1}]", "Unknow Error", ReaderUtils.ToHex(msgTran.AryTranData, "", " "));
                }
            }
            string strLog = string.Format("{0}{1}: {2}", strCmd, "Failed cause", strErrorCode);
            WriteLog(strLog, 1);
        }

        public static String FormatLongToTimeStr(long ms)
        {
            int milliSecond = (int)(ms % 1000);
            int second = (int)(ms / 1000);
            int minute = 0;
            int hour = 0;

            if (second >= 60)
            {
                minute = second / 60;
                second = second % 60;
            }
            if (minute >= 60)
            {
                hour = minute / 60;
                minute = minute % 60;
            }
            return string.Format("{0:D4}-{1:D2}-{2:D2}-{3:D3}", hour, minute, second, milliSecond);
        }

        private void parseInvTag(bool readPhase, byte[] data, byte cmd)
        {
            Tag tag = new Tag(data, readPhase, cmd, tagdb.AntGroup);
            tagdb.Add(tag);
            SetMaxMinRSSI(Convert.ToInt32(tag.Rssi));
            WriteLog(string.Format("FastMaxRssi: {0}, FastMinRssi: {1}, totalReadCount: {2}, totalTagCount{3}",
                tagdb.MaxRSSI + "dBm",
                tagdb.MinRSSI + "dBm",
                tagdb.TotalReadCounts.ToString(),
                tagdb.TotalTagCounts.ToString()
            ));

            if (needGetBuffer)
            {
                tagbufferCount++;
                //Console.WriteLine("parseInvTag tagbufferCount={0}, {1}", tagbufferCount, tag.BufferTagCount);
                if (tagbufferCount >= tag.BufferTagCount)
                {
                    needGetBuffer = false;
                }
                if (!needGetBuffer)
                {
                    stopGetInventoryBuffer(false);
                }
            }
        }
        private void parseGetFrequencyRegion(byte[] data)
        {
            //Console.WriteLine("parseGetFrequencyRegion: {0}", ReaderUtils.ToHex(data, "", " "));
            if (tagdb != null)
                tagdb.UpdateRegionInfo(data);
            if (tagOpDb != null)
                tagOpDb.UpdateRegionInfo(data);
        }
    }
}
