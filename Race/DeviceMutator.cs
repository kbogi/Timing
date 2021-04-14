using Util;
using System;
using System.Data;
using Reader;

namespace Race {
    class DeviceMutator {

        private Device device;
        public DeviceMutator(Device device) {
            this.device = device;
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
        
        private void RefreshReadSetting(byte btCmd) {
            // TODO
        }
        
        private void RefreshFastSwitch(byte btCmd) {
            // TODO
        }

        private void RunLoopFastSwitch() {
            // TODO
        }

        public void ApplyData(Reader.MessageTran msgTran)
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
                    WriteLog(lrtxtLog, strCmd, 0);
                    FastInventory();
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

                RefreshReadSetting((CMD)msgTran.Cmd);
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
            string strCmd = string.Format("{0}", FindResource("tipGetInternalVersion"));
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

            string strLog = string.Format("{0}{1}: {2}", strCmd, FindResource("FailedCause"), strErrorCode);
            WriteLog(lrtxtLog, strLog, 1);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);

            if (this.device.inventory)
            {
                this.device.inventoryBuffer.nCommond = 1;
                this.device.inventoryBuffer.dtEndInventory = DateTime.Now;
                RunLoopInventroy();
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog(strLog, 1);
        }

        private void ProcessGetFrequencyRegion(Reader.MessageTran msgTran)
            {
                string strCmd = string.Format("{0}", FindResource("tipGetFrequencyRegion"));
                string strErrorCode = string.Empty;

                parseGetFrequencyRegion(msgTran.AryData);

                if (msgTran.AryData.Length == 3)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btRegion = msgTran.AryData[0];
                    m_curSetting.btFrequencyStart = msgTran.AryData[1];
                    m_curSetting.btFrequencyEnd = msgTran.AryData[2];

                    RefreshReadSetting(CMD.cmd_get_frequency_region);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else if (msgTran.AryData.Length == 6)
                {
                    this.device.settings.btReadId = msgTran.ReadId;
                    this.device.settings.btRegion = msgTran.AryData[0];
                    this.device.settings.btUserDefineFrequencyInterval = msgTran.AryData[1];
                    this.device.settings.btUserDefineChannelQuantity = msgTran.AryData[2];
                    this.device.settings.nUserDefineStartFrequency = msgTran.AryData[3] * 256 * 256 + msgTran.AryData[4] * 256 + msgTran.AryData[5];
                    RefreshReadSetting(CMD.cmd_get_frequency_region);
                    WriteLog(strCmd, 0);
                    return;


                }
                else if (msgTran.AryData.Length == 1)
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
                else
                {
                    strErrorCode = string.Format("{0}", FindResource("UnknowError"));
                }

                string strLog = string.Format("{0}{1}: {2}", strCmd, FindResource("FailedCause"), strErrorCode);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    this.device.settings.btAntDetector = msgTran.AryData[0];

                    RefreshReadSetting(0x8E);
                    WriteLog(strCmd);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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

            private void ProcessSetProfile(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", FindResource("tipSetProfile"));
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
                strErrorCode = string.Format("{0}", FindResource("UnknowError"));
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, FindResource("FailedCause"), strErrorCode);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
            string strCmd = string.Format("{0} {1}", FindResource("tipSetReaderAntGroup"), tagdb.AntGroup);
            string strErrorCode;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);
                    if (m_setWorkAnt)
                    {
                        BeginInvoke(new ThreadStart(delegate() {
                            m_setWorkAnt = false;
                            byte btWorkAntenna = (byte)cmbWorkAnt.SelectedIndex;
                            if (btWorkAntenna >= 0x08)
                                btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                            reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                            m_curSetting.btWorkAntenna = btWorkAntenna;
                            return;
                        }));
                    }
                    if (m_getOutputPower)
                    {
                        reader.GetOutputPower(m_curSetting.btReadId);
                        return;
                    }
                    if (m_setOutputPower)
                    {
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

                                reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
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

                                reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                            }
                        }
                        return;
                    }
                    if (doingRealInv)
                    {
                        reader.SetWorkAntenna(m_curSetting.btReadId, m_curSetting.btWorkAntenna);
                    }
                    else if(doingFastInv)
                    {
                        cmdFastInventorySend(useAntG1);
                    }
                    else if(doingBufferInv)
                    {
                        reader.SetWorkAntenna(m_curSetting.btReadId, m_curSetting.btWorkAntenna);
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
                strErrorCode = string.Format("{0}", FindResource("UnknowError"));
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, FindResource("FailedCause"), strErrorCode);
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessGetReaderAntGroup(Reader.MessageTran msgTran)
        {
            string strCmd = string.Format("{0}", FindResource("tipGetReaderAntGroup"));
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    tagdb.AntGroup = msgTran.AryData[0];
                    if(tagdb.AntGroup==0x01)
                    {
                        BeginInvoke(new ThreadStart(delegate() {
                            if(!antType16.Checked)
                                antType16.Checked = true;
                        }));
                    }
                    if (m_getWorkAnt)
                    {
                        m_getWorkAnt = false;
                        reader.GetWorkAntenna(m_curSetting.btReadId);
                    }
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = ReaderUtils.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = string.Format("{0}", FindResource("UnknowError"));
            }

            string strLog = string.Format("{0}{1}: {2}", strCmd, FindResource("FailedCause"), strErrorCode);
            WriteLog(lrtxtLog, strLog, 1);
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
            WriteLog( strCmd, 1);;
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
                RefreshFastSwitch(0x01);
                RunLoopFastSwitch();
            }
            else if (msgTran.AryData.Length == 2)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[1]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode + "--" + "Antenna" + (msgTran.AryData[0] + 1);

                WriteLog( strCmd, 1);;
            }

            else if (msgTran.AryData.Length == 7)
            {
                this.device.switchTotal = Convert.ToInt32(msgTran.AryData[0]) * 255 * 255  + Convert.ToInt32(msgTran.AryData[1]) * 255  + Convert.ToInt32(msgTran.AryData[2]);
                this.device.switchTime = Convert.ToInt32(msgTran.AryData[3]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[4]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[5]) * 255 + Convert.ToInt32(msgTran.AryData[6]);

                this.device.inventoryBuffer.nDataCount = this.device.switchTotal;
                this.device.inventoryBuffer.nCommandDuration = this.device.switchTime;
                WriteLog( strCmd);
                RefreshFastSwitch(0x02);
                RunLoopFastSwitch();
            }

            /*else if (msgTran.AryData.Length == 8)
            {
                
                this.device.switchTotal = Convert.ToInt32(msgTran.AryData[0]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[1]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[2]) * 255 + Convert.ToInt32(msgTran.AryData[3]);
                this.device.switchTime = Convert.ToInt32(msgTran.AryData[4]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[5]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[6]) * 255 + Convert.ToInt32(msgTran.AryData[7]);

                this.device.inventoryBuffer.nDataCount = this.device.switchTotal;
                this.device.inventoryBuffer.nCommandDuration = this.device.switchTime;
                WriteLog( strCmd);
                RefreshFastSwitch(0x02);
                RunLoopFastSwitch();
            }*/
            else
            {
                this.device.total++;
                int nLength = msgTran.AryData.Length;
                int nEpcLength = nLength - 4;

                //Add inventory list
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, nEpcLength);                
                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 2);
                string strRSSI = msgTran.AryData[nLength - 1].ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nLength - 1]));
                byte btTemp = msgTran.AryData[0];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                this.device.inventoryBuffer.nCurrentAnt = (int)btAntId;
                string strAntId = btAntId.ToString();
                byte btFreq = (byte)(btTemp >> 2);
                
                string strFreq = GetFreqString(btFreq);

                DataRow[] drs = this.device.inventoryBuffer.dtTagTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                if (drs.Length == 0)
                {
                    DataRow row1 = this.device.inventoryBuffer.dtTagTable.NewRow();
                    row1[0] = strPC;
                    row1[2] = strEPC;
                    row1[4] = strRSSI;
                    row1[5] = "1";
                    row1[6] = strFreq;
                    row1[7] = "0";
                    row1[8] = "0";
                    row1[9] = "0";
                    row1[10] = "0";
                    switch(btAntId)
                    {
                        case 0x01:
                            {
                                row1[7] = "1";
                            }
                            break;
                        case 0x02:
                            {
                                row1[8] = "1";
                            }
                            break;
                        case 0x03:
                            {
                                row1[9] = "1";
                            }
                            break;
                        case 0x04:
                            {
                                row1[10] = "1";
                            }
                            break;
                        default:
                            break;
                    }

                    this.device.inventoryBuffer.dtTagTable.Rows.Add(row1);
                    this.device.inventoryBuffer.dtTagTable.AcceptChanges();
                }
                else
                {
                    foreach (DataRow dr in drs)
                    {
                        dr.BeginEdit();
                        int nTemp = 0;

                        dr[4] = strRSSI;
                        //dr[5] = (Convert.ToInt32(dr[5]) + 1).ToString();
                        nTemp = Convert.ToInt32(dr[5]);
                        dr[5] = (nTemp + 1).ToString();
                        dr[6] = strFreq;

                        switch(btAntId)
                        {
                            case 0x01:
                                {
                                    //dr[7] = (Convert.ToInt32(dr[7]) + 1).ToString();
                                    nTemp = Convert.ToInt32(dr[7]);
                                    dr[7] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x02:
                                {
                                    //dr[8] = (Convert.ToInt32(dr[8]) + 1).ToString();
                                    nTemp = Convert.ToInt32(dr[8]);
                                    dr[8] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x03:
                                {
                                    //dr[9] = (Convert.ToInt32(dr[9]) + 1).ToString();
                                    nTemp = Convert.ToInt32(dr[9]);
                                    dr[9] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x04:
                                {
                                    //dr[10] = (Convert.ToInt32(dr[10]) + 1).ToString();
                                    nTemp = Convert.ToInt32(dr[10]);
                                    dr[10] = (nTemp + 1).ToString();
                                }
                                break;
                            default:
                                break;
                        }

                        dr.EndEdit();
                    }
                    this.device.inventoryBuffer.dtTagTable.AcceptChanges();
                }

                this.device.inventoryBuffer.dtEndInventory = DateTime.Now;
                RefreshFastSwitch(0x00);
            }

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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
                RefreshInventoryReal(0x00);
                RunLoopInventroy();
            }
            else if (msgTran.AryData.Length == 7)
            {
                this.device.inventoryBuffer.nReadRate = Convert.ToInt32(msgTran.AryData[1]) * 256 + Convert.ToInt32(msgTran.AryData[2]);
                this.device.inventoryBuffer.nDataCount = Convert.ToInt32(msgTran.AryData[3]) * 256 * 256 * 256 + Convert.ToInt32(msgTran.AryData[4]) * 256 * 256 + Convert.ToInt32(msgTran.AryData[5]) * 256 + Convert.ToInt32(msgTran.AryData[6]);

                WriteLog( strCmd);
                RefreshInventoryReal(0x01);
                RunLoopInventroy();
            }
            else
            {
                this.device.total++;
                int nLength = msgTran.AryData.Length;
                int nEpcLength = nLength - 4;

                //Add inventory list
                //if (msgTran.AryData[3] == 0x00)
                //{
                //    MessageBox.Show("");
                //}
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, nEpcLength);
                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 2);
                string strRSSI = msgTran.AryData[nLength - 1].ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nLength - 1]));
                byte btTemp = msgTran.AryData[0];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                this.device.inventoryBuffer.nCurrentAnt = btAntId;
                string strAntId = btAntId.ToString();
            
                byte btFreq = (byte)(btTemp >> 2);
                string strFreq = GetFreqString(btFreq);
                
                //DataRow row = this.device.inventoryBuffer.dtTagDetailTable.NewRow();
                //row[0] = strEPC;
                //row[1] = strRSSI;
                //row[2] = strAntId;
                //row[3] = strFreq;

                //this.device.inventoryBuffer.dtTagDetailTable.Rows.Add(row);
                //this.device.inventoryBuffer.dtTagDetailTable.AcceptChanges();

                ////Add tag list
                //DataRow[] drsDetail = this.device.inventoryBuffer.dtTagDetailTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                //int nDetailCount = drsDetail.Length;
                DataRow[] drs = this.device.inventoryBuffer.dtTagTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                if (drs.Length == 0)
                {
                    DataRow row1 = this.device.inventoryBuffer.dtTagTable.NewRow();
                    row1[0] = strPC;
                    row1[2] = strEPC;
                    row1[4] = strRSSI;
                    row1[5] = "1";
                    row1[6] = strFreq;

                    this.device.inventoryBuffer.dtTagTable.Rows.Add(row1);
                    this.device.inventoryBuffer.dtTagTable.AcceptChanges();
                }
                else
                {
                    foreach (DataRow dr in drs)
                    {
                        dr.BeginEdit();

                        dr[4] = strRSSI;
                        dr[5] = (Convert.ToInt32(dr[5]) + 1).ToString();
                        dr[6] = strFreq;

                        dr.EndEdit();                       
                    }
                    this.device.inventoryBuffer.dtTagTable.AcceptChanges();
                }

                this.device.inventoryBuffer.dtEndInventory = DateTime.Now;
                RefreshInventoryReal(0x89);
            }
        }

        private void ProcessInventory(Reader.MessageTran msgTran)
        {
            string strCmd = "Inventory";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 9)
            {
                this.device.inventoryBuffer.nCurrentAnt = msgTran.AryData[0];
                this.device.inventoryBuffer.nTagCount = Convert.ToInt32(msgTran.AryData[1]) * 256 + Convert.ToInt32(msgTran.AryData[2]);
                this.device.inventoryBuffer.nReadRate = Convert.ToInt32(msgTran.AryData[3]) * 256 + Convert.ToInt32(msgTran.AryData[4]);
                int nTotalRead = Convert.ToInt32(msgTran.AryData[5]) * 256 * 256 * 256
                    + Convert.ToInt32(msgTran.AryData[6]) * 256 * 256
                    + Convert.ToInt32(msgTran.AryData[7]) * 256
                    + Convert.ToInt32(msgTran.AryData[8]);
                this.device.inventoryBuffer.nDataCount = nTotalRead;
                this.device.inventoryBuffer.lTotalRead.Add(nTotalRead);
                this.device.inventoryBuffer.dtEndInventory = DateTime.Now;

                RefreshInventory(0x80);
                WriteLog( strCmd);

                RunLoopInventroy();

                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "Unknown Error";
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;
            WriteLog( strCmd, 1);;

            RunLoopInventroy();
        }

        private void SetMaxMinRSSI(int nRSSI)
        {
            if (this.device.inventoryBuffer.nMaxRSSI < nRSSI)
            {
                this.device.inventoryBuffer.nMaxRSSI = nRSSI;
            }

            if (this.device.inventoryBuffer.nMinRSSI == 0)
            {
                this.device.inventoryBuffer.nMinRSSI = nRSSI;
            }
            else if (this.device.inventoryBuffer.nMinRSSI > nRSSI)
            {
                this.device.inventoryBuffer.nMinRSSI = nRSSI;
            }
        }

        private void ProcessGetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "Get buffered data without clearing";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nDataLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEpc = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strRSSI = msgTran.AryData[nDataLen - 3].ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nDataLen - 3]));
                byte btTemp = msgTran.AryData[nDataLen - 2];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                string strAntId = btAntId.ToString();
                string strReadCnr = msgTran.AryData[nDataLen - 1].ToString();


                Console.WriteLine("ChipData: {0}, {1}, {2}, {3}, {4}, {5}", strPC, strCRC, strEpc, strAntId, strRSSI, strReadCnr);

                DataRow row = this.device.inventoryBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEpc;
                row[3] = strAntId;
                row[4] = strRSSI;
                row[5] = strReadCnr;

                this.device.inventoryBuffer.dtTagTable.Rows.Add(row);
                this.device.inventoryBuffer.dtTagTable.AcceptChanges();

                RefreshInventory(0x90);
                WriteLog( strCmd);
            }
        }

        private void ProcessGetAndResetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "Get and clear buffered data";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nDataLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEpc = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strRSSI = msgTran.AryData[nDataLen - 3].ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nDataLen - 3]));
                byte btTemp = msgTran.AryData[nDataLen - 2];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                string strAntId = btAntId.ToString();
                string strReadCnr = msgTran.AryData[nDataLen - 1].ToString();
                
                Console.WriteLine("ChipData: {0}, {1}, {2}, {3}, {4}, {5}", strPC, strCRC, strEpc, strAntId, strRSSI, strReadCnr);

                DataRow row = this.device.inventoryBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEpc;
                row[3] = strAntId;
                row[4] = strRSSI;
                row[5] = strReadCnr;

                this.device.inventoryBuffer.dtTagTable.Rows.Add(row);
                this.device.inventoryBuffer.dtTagTable.AcceptChanges();

                RefreshInventory(0x91);
                WriteLog( strCmd);
            }
        }

        private void ProcessGetInventoryBufferTagCount(Reader.MessageTran msgTran)
        {
            string strCmd = "Query how many tags are buffered";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                this.device.inventoryBuffer.nTagCount = Convert.ToInt32(msgTran.AryData[0]) * 256 + Convert.ToInt32(msgTran.AryData[1]);

                RefreshInventory(0x92);
                string strLog1 = strCmd + " " + this.device.inventoryBuffer.nTagCount.ToString();
                WriteLog(strLog1);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                if (msgTran.AryData[0] == 0x00)
                {
                    this.device.operateTagBuffer.strAccessEpcMatch = CCommondMethod.ByteArrayToString(msgTran.AryData, 2, Convert.ToInt32(msgTran.AryData[1]));
                    
                    RefreshOpTag(0x86);
                    WriteLog( strCmd);
                    return;
                }
                else
                {
                    strErrorCode = "Unknown Error";
                }
            }

            string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

            WriteLog( strCmd, 1);;
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nDataLen = Convert.ToInt32(msgTran.AryData[nLen - 3]);
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - nDataLen - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 7 + nEpcLen, nDataLen);

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                DataRow row = this.device.operateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = nDataLen.ToString();
                row[5] = strAntId;
                row[6] = strReadCount;

                this.device.operateTagBuffer.dtTagTable.Rows.Add(row);
                this.device.operateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x81);
                WriteLog( strCmd);
            }
        }

        private void ProcessWriteTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Write Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2])  - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                    return;
                }

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = string.Empty;

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                DataRow row = this.device.operateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                this.device.operateTagBuffer.dtTagTable.Rows.Add(row);
                this.device.operateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x82);
                WriteLog( strCmd);
            }
        }

        private void ProcessLockTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Lock Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                    return;
                }

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = string.Empty;

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                DataRow row = this.device.operateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                this.device.operateTagBuffer.dtTagTable.Rows.Add(row);
                this.device.operateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x83);
                WriteLog( strCmd);
            }
        }

        private void ProcessKillTag(Reader.MessageTran msgTran)
        {
            string strCmd = "Kill Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                    return;
                }

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = string.Empty;

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                DataRow row = this.device.operateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                this.device.operateTagBuffer.dtTagTable.Rows.Add(row);
                this.device.operateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x84);
                WriteLog( strCmd);
            }
        }

        private void ProcessInventoryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Inventory";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] != 0xFF)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                    string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                    WriteLog( strCmd, 1);;
                }                
            }
            else if (msgTran.AryData.Length == 9)
            {
                string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                string strUID = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 8);

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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, msgTran.AryData.Length - 1);

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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);;
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strCnt = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

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

        private void ProcessQueryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "Query Tag";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "Failure, failure cause: " + strErrorCode;

                WriteLog( strCmd, 1);
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

                this.device.operateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                this.device.operateTagISO18000Buffer.btStatus = msgTran.AryData[1];

                RefreshISO18000(msgTran.Cmd);

                WriteLog( strCmd);
            }
        }
    }
}
