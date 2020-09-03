using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using System.IO;
using System.IO.Ports;
using Reader;
using System.Net.Sockets;
using System.Diagnostics;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using Windows.Devices.Enumeration;
using System.ComponentModel;
using Windows.Devices.Bluetooth.GenericAttributeProfile;
using Windows.Devices.Bluetooth;
using System.Runtime.InteropServices.WindowsRuntime;

namespace UHFDemo
{
    public partial class R2000UartDemo : Form
    {
        R2000UartDemo rootPage;
        public string SelectedBleDeviceId;
        public string SelectedBleDeviceName = "No device selected";

        private ReaderMethod reader;

        private ReaderSetting m_curSetting = new ReaderSetting();
        private OperateTagBuffer m_curOperateTagBuffer = new OperateTagBuffer();
        private OperateTagISO18000Buffer m_curOperateTagISO18000Buffer = new OperateTagISO18000Buffer();

        //实时盘存锁定操作
        private bool m_bLockTab = false;
        //ISO18000标签连续盘存标识
        private bool m_bContinue = false;
        //是否显示串口监控数据
        private bool m_bDisplayLog = false;
        //记录ISO18000标签循环写入次数
        private int m_nLoopTimes = 0;
        //记录ISO18000标签写入字符数
        private int m_nBytes = 0;
        //记录ISO18000标签已经循环写入次数
        private int m_nLoopedTimes = 0;

        private int m_nReceiveFlag = 0;
        private int m_FastExeCount;
        private volatile bool m_nRepeat2 = false;

        private volatile bool m_nRepeat12 = false;

        private bool m_getOutputPower = false;
        private bool m_setOutputPower = false;
        private bool m_setWorkAnt = false;
        private bool m_getWorkAnt = false;

        private CheckBox[] fast_inv_ants = null;
        private TextBox[] fast_inv_stays = null;
        private TextBox[] fast_inv_temp_pows = null;

        TagDB tagdb = null;
        bool isFastInv = false;
        bool doingFastInv = false;
        bool Inventorying = false;
        bool isRealInv = false;
        bool doingRealInv = false;
        bool isBufferInv = false;
        bool doingBufferInv = false;
        bool needGetBuffer = false;
        private int tagbufferCount = 0;

        private bool ReverseTarget = false;
        private int stayBTimes = 0;
        private bool invTargetB = false;

        bool useAntG1 = true;
        int FastExecTimes = 0;

        /// <summary>
        /// Define a variable for the inventory test log
        /// </summary>
        TextWriter transportLogFile = null;
        uint inventory_times = 0;

        RadioButton[] sessionArr = null;
        RadioButton[] targetArr = null;
        RadioButton[] selectFlagArr = null;

        int channels = 1;

        DispatcherTimer dispatcherTimer = null;
        DispatcherTimer readratePerSecond = null;

        DateTime startInventoryTime; // 记录盘存开始时间
        DateTime beforeCmdExecTime;
        public double elapsedTime = 0.0; // 盘点开始到此刻经过的时间

        List<string> antLists = null;

        public R2000UartDemo()
        {
            InitializeComponent();

            Text = string.Format("{0}{1}.{2}",
                "UHF RFID Reader Demo v",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor);

            DoubleBuffered = true;

            fast_inv_ants = new CheckBox[] { 
                chckbx_fast_inv_ant_1, chckbx_fast_inv_ant_2, chckbx_fast_inv_ant_3, chckbx_fast_inv_ant_4,
                chckbx_fast_inv_ant_5, chckbx_fast_inv_ant_6, chckbx_fast_inv_ant_7, chckbx_fast_inv_ant_8,
                chckbx_fast_inv_ant_9, chckbx_fast_inv_ant_10, chckbx_fast_inv_ant_11, chckbx_fast_inv_ant_12,
                chckbx_fast_inv_ant_13, chckbx_fast_inv_ant_14, chckbx_fast_inv_ant_15, chckbx_fast_inv_ant_16
            };

            fast_inv_stays = new TextBox[] { 
                txt_fast_inv_Stay_1, txt_fast_inv_Stay_2, txt_fast_inv_Stay_3, txt_fast_inv_Stay_4,
                txt_fast_inv_Stay_5, txt_fast_inv_Stay_6, txt_fast_inv_Stay_7, txt_fast_inv_Stay_8,
                txt_fast_inv_Stay_9, txt_fast_inv_Stay_10, txt_fast_inv_Stay_11, txt_fast_inv_Stay_12,
                txt_fast_inv_Stay_13,txt_fast_inv_Stay_14, txt_fast_inv_Stay_15, txt_fast_inv_Stay_16
            };

            fast_inv_temp_pows = new TextBox[] {
                tv_temp_pow_1, tv_temp_pow_2, tv_temp_pow_3, tv_temp_pow_4, tv_temp_pow_5, tv_temp_pow_6, tv_temp_pow_7, tv_temp_pow_8,
                tv_temp_pow_9, tv_temp_pow_10, tv_temp_pow_11, tv_temp_pow_12, tv_temp_pow_13, tv_temp_pow_14, tv_temp_pow_15, tv_temp_pow_16
            };

            bindInvAntTableEvents();

            sessionArr = new RadioButton[] { radio_btn_S0, radio_btn_S1, radio_btn_S2, radio_btn_S3 };
            targetArr = new RadioButton[] { radio_btn_target_A, radio_btn_target_B };
            selectFlagArr = new RadioButton[] { radio_btn_sl_00, radio_btn_sl_01, radio_btn_sl_02, radio_btn_sl_03 };

            radio_btn_S1.Checked = true;
            radio_btn_target_A.Checked = true;
            radio_btn_sl_00.Checked = true;

            initRealInvAnts();
            radio_btn_realtime_inv.Checked = true;
            
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Interval = TimeSpan.FromMilliseconds(50);
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);

            readratePerSecond = new DispatcherTimer();
            readratePerSecond.Interval = TimeSpan.FromMilliseconds(900);
            readratePerSecond.Tick += new EventHandler(readRatePerSec_Tick);
        }

        private void initRealInvAnts()
        {
            antLists = new List<string>();
            antLists.Add("天线1");
            combo_realtime_inv_ants.Items.AddRange(antLists.ToArray());
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lock(tagdb)
            {
                tagdb.Repaint();
            }
        }

        void readRatePerSec_Tick(object sender, EventArgs e)
        {
            if (led_totalread_count.Text.ToString() != "")
            {
                //Divide Total tag count at every 1 sec instant per difference value of
                //current time and start async read time
                UpdateReadRate(CalculateElapsedTime());
            }
        }

        /// <summary>
        /// Calculate total elapsed time between present time and start read command
        /// initiated
        /// </summary>
        /// <returns>Returns total time elapsed </returns>
        private double CalculateElapsedTime()
        {
            TimeSpan elapsed = (DateTime.Now - startInventoryTime);
            // elapsed time + previous cached async read time
            double totalseconds = elapsedTime + elapsed.TotalSeconds;
            label_totaltime.Text = Math.Round(totalseconds, 2) + "秒";
            if(doingBufferInv)
            {
                ledFast_total_execute_time.Text = FormatLongToTimeStr((long)totalseconds);
            }
            return totalseconds;
        }

        private double CalculateExecTime()
        {
            return (DateTime.Now - beforeCmdExecTime).TotalMilliseconds;
        }

        /// <summary>
        /// Display read rate per sec
        /// </summary>
        /// <param name="totalElapsedSeconds"> total elapsed time</param>
        private void UpdateReadRate(double totalElapsedSeconds)
        {
            long totalReads = 0;
            long tags = 0;
            if (Inventorying)
            {
                totalReads = tagdb.TotalReadCounts;
                tags = tagdb.TotalTagCounts;
            }
            label_totalread_count.Text = totalReads + "次";
            label_totaltag_count.Text = tags + "个";
            label_readrate.Text = Math.Round((totalReads / totalElapsedSeconds), 2) + "个/秒";
        }

        private void bindInvAntTableEvents()
        {
            foreach (CheckBox cb in fast_inv_ants)
            {
                cb.CheckedChanged += new EventHandler(fastInvAntChecked);
            }
        }

        private void fastInvAntChecked(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            int antNo = Convert.ToInt32(cb.Text) - 1;
            if (cb.Checked)
            {
                fast_inv_stays[antNo].Text = "1";
                fast_inv_temp_pows[antNo].Text = "20";
            }
            else
            {
                fast_inv_stays[antNo].Text = "0";
                fast_inv_temp_pows[antNo].Text = "0";
            }
        }

        private void R2000UartDemo_Load(object sender, EventArgs e)
        {
            //初始化访问读写器实例
            reader = new Reader.ReaderMethod();

            //回调函数
            reader.AnalyCallback = AnalyData;
            reader.SendCallback = SendData;
            reader.ReceiveCallback = RecvData;
            reader.ErrCallback = OnError;

            //设置界面元素有效性
            SetFormEnable(false);
            radio_btn_rs232.Checked = true;
            antType4.Checked = true;

            //初始化连接读写器默认配置
            RefreshComPorts();

            cmbBaudrate.SelectedIndex = 1; // 115200
            ipIpServer.IpAddressStr = "192.168.0.178";
            txtTcpPort.Text = "4001";


            combo_mast_id.SelectedIndex = 0;
            combo_menbank.SelectedIndex = 1;
            combo_action.SelectedIndex = 0;
            combo_session.SelectedIndex = 0;
            comboBox16.SelectedIndex = 0;

            cmbReturnLossFreq.SelectedIndex = 33;
            if (cbUserDefineFreq.Checked == true)
            {
                groupBox21.Enabled = false;
                groupBox23.Enabled = true;

            }
            else
            {
                groupBox21.Enabled = true;
                groupBox23.Enabled = false;
            };

            //保存盘点日志
            saveLog();

            GenerateColmnsDataGridForFastInv();
            tagdb = new TagDB();
            dgv_fast_inv_tags.DataSource = tagdb.TagList;

        }

        private void RefreshComPorts()
        {
            cmbComPort.Items.Clear();
            string[] portNames = SerialPort.GetPortNames();
            if (portNames != null && portNames.Length > 0)
            {
                cmbComPort.Items.AddRange(portNames);
            }
            cmbComPort.SelectedIndex = cmbComPort.Items.Count - 1;
        }

        private void SendData(object sender, byte[] data)
        {
            if (m_bDisplayLog)
            {
                string strLog = "Send: " + CCommondMethod.ToHex(data, "", " ");
                Console.WriteLine("-->  {0}", strLog);
                WriteLog(lrtxtDataTran, strLog, 0);
            }
        }

        private void RecvData(object sender, TransportDataEventArgs e)
        {
            if (m_bDisplayLog)
            {
                string strLog = e.Tx ? "Send: ":"Recv: " + CCommondMethod.ToHex(e.Data, "", " ");
                Console.WriteLine("<--  {0}", strLog);
                WriteLog(lrtxtDataTran, strLog, e.Tx ? 0 : 1);
            }
        }

        private void OnError(object sender, ErrorReceivedEventArgs e)
        {
            WriteLog(lrtxtLog, e.ErrStr, 1);
            if (radio_btn_tcp.Checked)
            {
                if (e.ErrStr.Contains("重连成功") && !btnInventory.Text.Equals("开始盘存"))
                {
                    BeginInvoke(new ThreadStart(delegate ()
                    {
                        FastInventory_Click(null, null);
                        FastInventory_Click(null, null);
                    }));
                }
            }
        }

        private void AnalyData(object sender, Reader.MessageTran msgTran)
        {
            m_nReceiveFlag = 0;
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
            string strCmd = "设置临时输出功率";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);
                    //Console.WriteLine("设置功率成功，开始循环快速盘存");
                    FastInventory();
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private delegate void WriteLogUnSafe(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType);
        public void WriteLog(CustomControl.LogRichTextBox logRichTxt, string strLog, int nType)
        {
            if (this.InvokeRequired)
            {
                WriteLogUnSafe InvokeWriteLog = new WriteLogUnSafe(WriteLog);
                this.Invoke(InvokeWriteLog, new object[] { logRichTxt, strLog, nType });
            }
            else
            {
                if (nType == 0)
                {
                    logRichTxt.AppendTextEx(strLog, Color.Indigo);
                }
                else
                {
                    logRichTxt.AppendTextEx(strLog, Color.Red);
                }

                if (ckClearOperationRec.Checked)
                {
                    if (logRichTxt.Lines.Length > 50)
                    {
                        logRichTxt.Clear();
                    }
                }

                logRichTxt.Select(logRichTxt.TextLength, 0);
                logRichTxt.ScrollToCaret();
            }
        }

        private delegate void RefreshOpTagUnsafe(byte btCmd);
        private void RefreshOpTag(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshOpTagUnsafe InvokeRefresh = new RefreshOpTagUnsafe(RefreshOpTag);
                this.Invoke(InvokeRefresh, new object[] { btCmd });
            }
            else
            {
                switch (btCmd)
                {
                    case 0x81:
                    case 0x82:
                    case 0x83:
                    case 0x84:
                        {
                            int nCount = ltvOperate.Items.Count;
                            int nLength = m_curOperateTagBuffer.dtTagTable.Rows.Count;

                            DataRow row = m_curOperateTagBuffer.dtTagTable.Rows[nLength - 1];

                            ListViewItem item = new ListViewItem();
                            item.Text = (nCount + 1).ToString();
                            item.SubItems.Add(row[0].ToString());
                            item.SubItems.Add(row[1].ToString());
                            item.SubItems.Add(row[2].ToString());
                            item.SubItems.Add(row[3].ToString());
                            item.SubItems.Add(row[4].ToString());
                            item.SubItems.Add(row[5].ToString());
                            item.SubItems.Add(row[6].ToString());

                            ltvOperate.Items.Add(item);
                        }
                        break;
                    case 0x86:
                        {
                            txtAccessEpcMatch.Text = m_curOperateTagBuffer.strAccessEpcMatch;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private delegate void RefreshReadSettingUnsafe(byte btCmd);
        private void RefreshReadSetting(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshReadSettingUnsafe InvokeRefresh = new RefreshReadSettingUnsafe(RefreshReadSetting);
                this.Invoke(InvokeRefresh, new object[] { btCmd });
            }
            else
            {
                htxtReadId.Text = string.Format("{0:X2}", m_curSetting.btReadId);
                switch (btCmd)
                {
                    case 0x6A:
                        if (m_curSetting.btLinkProfile == 0xd0)
                        {
                            rdbProfile0.Checked = true;
                        }
                        else if (m_curSetting.btLinkProfile == 0xd1)
                        {
                            rdbProfile1.Checked = true;
                        }
                        else if (m_curSetting.btLinkProfile == 0xd2)
                        {
                            rdbProfile2.Checked = true;
                        }
                        else if (m_curSetting.btLinkProfile == 0xd3)
                        {
                            rdbProfile3.Checked = true;
                        }
                        else
                        {
                        }

                        break;
                    case 0x68:
                        htbGetIdentifier.Text = m_curSetting.btReaderIdentifier;

                        break;
                    case 0x72:
                        {
                            txtFirmwareVersion.Text = m_curSetting.btMajor.ToString() + "." + m_curSetting.btMinor.ToString();
                        }
                        break;
                    case 0x75:
                        {
                            if (antType16.Checked && m_curSetting.btAntGroup == (byte)0x00)
                                cmbWorkAnt.SelectedIndex = m_curSetting.btWorkAntenna;
                            else
                                cmbWorkAnt.SelectedIndex = m_curSetting.btWorkAntenna + 0x08;
                        }
                        break;
                    case 0x77:
                        {
                            if (antType4.Checked)
                            {
                                if (m_curSetting.btOutputPower != 0 && m_curSetting.btOutputPowers == null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_2.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_3.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_4.Text = m_curSetting.btOutputPower.ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    tb_outputpower_2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    tb_outputpower_3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    tb_outputpower_4.Text = m_curSetting.btOutputPowers[3].ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }

                            }

                            if (antType1.Checked)
                            {
                                if (m_curSetting.btOutputPower != 0 && m_curSetting.btOutputPowers == null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPower.ToString();
                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                            }

                        }
                        break;
                    case 0x97:
                        {
                            if (antType8.Checked)
                            {

                                if (m_curSetting.btOutputPower != 0 && m_curSetting.btOutputPowers == null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_2.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_3.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_4.Text = m_curSetting.btOutputPower.ToString();


                                    tb_outputpower_5.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_6.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_7.Text = m_curSetting.btOutputPower.ToString();
                                    tb_outputpower_8.Text = m_curSetting.btOutputPower.ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    tb_outputpower_2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    tb_outputpower_3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    tb_outputpower_4.Text = m_curSetting.btOutputPowers[3].ToString();
                                    tb_outputpower_5.Text = m_curSetting.btOutputPowers[4].ToString();
                                    tb_outputpower_6.Text = m_curSetting.btOutputPowers[5].ToString();
                                    tb_outputpower_7.Text = m_curSetting.btOutputPowers[6].ToString();
                                    tb_outputpower_8.Text = m_curSetting.btOutputPowers[7].ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                            }
                            else if (antType16.Checked)
                            {
                                if (m_curSetting.btOutputPowers != null)
                                {
                                    tb_outputpower_1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    tb_outputpower_2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    tb_outputpower_3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    tb_outputpower_4.Text = m_curSetting.btOutputPowers[3].ToString();
                                    tb_outputpower_5.Text = m_curSetting.btOutputPowers[4].ToString();
                                    tb_outputpower_6.Text = m_curSetting.btOutputPowers[5].ToString();
                                    tb_outputpower_7.Text = m_curSetting.btOutputPowers[6].ToString();
                                    tb_outputpower_8.Text = m_curSetting.btOutputPowers[7].ToString();

                                    if (m_curSetting.btOutputPowers.Length >= 16)
                                    {
                                        tb_outputpower_9.Text = m_curSetting.btOutputPowers[8].ToString();
                                        tb_outputpower_10.Text = m_curSetting.btOutputPowers[9].ToString();
                                        tb_outputpower_11.Text = m_curSetting.btOutputPowers[10].ToString();
                                        tb_outputpower_12.Text = m_curSetting.btOutputPowers[11].ToString();
                                        tb_outputpower_13.Text = m_curSetting.btOutputPowers[12].ToString();
                                        tb_outputpower_14.Text = m_curSetting.btOutputPowers[13].ToString();
                                        tb_outputpower_15.Text = m_curSetting.btOutputPowers[14].ToString();
                                        tb_outputpower_16.Text = m_curSetting.btOutputPowers[15].ToString();
                                        m_curSetting.btOutputPowers = null;
                                    }
                                }
                            }
                        }
                        break;
                    case 0x79:
                        {
                            switch (m_curSetting.btRegion)
                            {
                                case 0x01:
                                    {
                                        cbUserDefineFreq.Checked = false;
                                        textStartFreq.Text = "";
                                        TextFreqInterval.Text = "";
                                        textFreqQuantity.Text = "";
                                        rdbRegionFcc.Checked = true;
                                        cmbFrequencyStart.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyStart) - 7;
                                        cmbFrequencyEnd.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyEnd) - 7;
                                    }
                                    break;
                                case 0x02:
                                    {
                                        cbUserDefineFreq.Checked = false;
                                        textStartFreq.Text = "";
                                        TextFreqInterval.Text = "";
                                        textFreqQuantity.Text = "";
                                        rdbRegionEtsi.Checked = true;
                                        cmbFrequencyStart.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyStart);
                                        cmbFrequencyEnd.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyEnd);
                                    }
                                    break;
                                case 0x03:
                                    {
                                        cbUserDefineFreq.Checked = false;
                                        textStartFreq.Text = "";
                                        TextFreqInterval.Text = "";
                                        textFreqQuantity.Text = "";
                                        rdbRegionChn.Checked = true;
                                        cmbFrequencyStart.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyStart) - 43;
                                        cmbFrequencyEnd.SelectedIndex = Convert.ToInt32(m_curSetting.btFrequencyEnd) - 43;
                                    }
                                    break;
                                case 0x04:
                                    {
                                        cbUserDefineFreq.Checked = true;
                                        rdbRegionChn.Checked = false;
                                        rdbRegionEtsi.Checked = false;
                                        rdbRegionFcc.Checked = false;
                                        cmbFrequencyStart.SelectedIndex = -1;
                                        cmbFrequencyEnd.SelectedIndex = -1;
                                        textStartFreq.Text = m_curSetting.nUserDefineStartFrequency.ToString();
                                        TextFreqInterval.Text = Convert.ToString(m_curSetting.btUserDefineFrequencyInterval * 10);
                                        textFreqQuantity.Text = m_curSetting.btUserDefineChannelQuantity.ToString();
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    case 0x7B:
                        {
                            string strTemperature = string.Empty;
                            if (m_curSetting.btPlusMinus == 0x0)
                            {
                                strTemperature = "-" + m_curSetting.btTemperature.ToString() + "℃";
                            }
                            else
                            {
                                strTemperature = m_curSetting.btTemperature.ToString() + "℃";
                            }
                            txtReaderTemperature.Text = strTemperature;
                        }
                        break;
                    case 0x7D:
                        {
                            /*
                            if (m_curSetting.btDrmMode == 0x00)
                            {
                                rdbDrmModeClose.Checked = true;
                            }
                            else
                            {
                                rdbDrmModeOpen.Checked = true;
                            }
                             * */
                        }
                        break;
                    case 0x7E:
                        {
                            textReturnLoss.Text = m_curSetting.btAntImpedance.ToString() + " dB";
                        }
                        break;


                    case 0x8E:
                        {
                            if (m_curSetting.btMonzaStatus == 0x8D)
                            {
                                rdbMonzaOn.Checked = true;
                            }
                            else
                            {
                                rdbMonzaOff.Checked = true;
                            }
                        }
                        break;
                    case 0x60:
                        {
                            if (m_curSetting.btGpio1Value == 0x00)
                            {
                                rdbGpio1Low.Checked = true;
                            }
                            else
                            {
                                rdbGpio1High.Checked = true;
                            }

                            if (m_curSetting.btGpio2Value == 0x00)
                            {
                                rdbGpio2Low.Checked = true;
                            }
                            else
                            {
                                rdbGpio2High.Checked = true;
                            }
                        }
                        break;
                    case 0x63:
                        {
                            tbAntDectector.Text = m_curSetting.btAntDetector.ToString();
                        }
                        break;
                    case 0x98:
                        getMaskInitStatus();
                        break;
                    default:
                        break;
                }
            }
        }

        private void getMaskInitStatus()
        {

            byte[] maskValue = new byte[m_curSetting.btsGetTagMask.Length - 8];
            for (int i = 0; i < maskValue.Length; i++)
            {
                maskValue[i] = m_curSetting.btsGetTagMask[i + 7];
            }
            CCommondMethod.ByteArrayToString(maskValue, 0, maskValue.Length);
            ListViewItem item = new ListViewItem();
            item.Text = m_curSetting.btsGetTagMask[0].ToString();
            if (m_curSetting.btsGetTagMask[2] == 0)
            {
                item.SubItems.Add("S0");
            }
            else if (m_curSetting.btsGetTagMask[2] == 1)
            {
                item.SubItems.Add("S1");
            }
            else if (m_curSetting.btsGetTagMask[2] == 2)
            {
                item.SubItems.Add("S2");
            }
            else if (m_curSetting.btsGetTagMask[2] == 3)
            {
                item.SubItems.Add("S3");
            }
            else
            {
                item.SubItems.Add("SL");
            }

            item.SubItems.Add("0x0" + m_curSetting.btsGetTagMask[3].ToString());
            if (m_curSetting.btsGetTagMask[4] == 0)
            {
                item.SubItems.Add("Reserve");
            }
            else if (m_curSetting.btsGetTagMask[4] == 1)
            {
                item.SubItems.Add("EPC");
            }
            else if (m_curSetting.btsGetTagMask[4] == 2)
            {
                item.SubItems.Add("TID");
            }
            else
            {
                item.SubItems.Add("USER");
            }
            item.SubItems.Add(CCommondMethod.ByteArrayToString(new byte[] { m_curSetting.btsGetTagMask[5] }, 0, 1).ToString());
            item.SubItems.Add(CCommondMethod.ByteArrayToString(new byte[] { m_curSetting.btsGetTagMask[6] }, 0, 1).ToString());
            item.SubItems.Add(CCommondMethod.ByteArrayToString(maskValue, 0, maskValue.Length).ToString());
            listView2.Items.Add(item);
        }
        private void RunLoopInventroy()
        {
            BeginInvoke(new ThreadStart(delegate() {
                if (doingRealInv)
                {
                    cmdRealInventorySend();
                }
                else if (doingBufferInv)
                {
                    cmdCachedInventorySend();
                }
                else
                {
                    Console.WriteLine("RunLoopInventroy ...");
                }
            }));
        }

        private void cmdGetAndResetInventoryBuffer()
        {
            beforeCmdExecTime = DateTime.Now;
            reader.GetAndResetInventoryBuffer(m_curSetting.btReadId);
        }

        private void cmdGetInventoryBuffer()
        {
            beforeCmdExecTime = DateTime.Now;
            reader.GetInventoryBuffer(m_curSetting.btReadId);
        }

        private void cmdCachedInventorySend()
        {
            RefreshInventoryInfo();
            beforeCmdExecTime = DateTime.Now;
            reader.Inventory(m_curSetting.btReadId, Convert.ToByte(txtRepeat.Text));
        }

        private void cmdRealInventorySend()
        {
            RefreshInventoryInfo();
            beforeCmdExecTime = DateTime.Now;
            int writeIndex = 0;
            byte[] data = new byte[256];
            data[writeIndex++] = 0xA0;
            data[writeIndex++] = 0x03;
            data[writeIndex++] = m_curSetting.btReadId;
            if(cb_customized_session_target.Checked)
            {
                data[writeIndex++] = 0x8B; // cmd
                data[writeIndex++] = getParamSession(); // session
                if (ReverseTarget)
                {
                    if (invTargetB && stayBTimes > 1)
                    {
                        stayBTimes--;
                        data[writeIndex++] = 0x01; // Target B
                    }
                    else
                    {
                        stayBTimes = Convert.ToInt32(tb_fast_inv_staytargetB_times.Text);
                        data[writeIndex++] = (byte)(invTargetB == false ? 0x00 : 0x01); // Target
                        invTargetB = !invTargetB;
                    }
                }
                else
                {
                    data[writeIndex++] = getParamTarget(); // Target
                }

                if (cb_use_selectFlags_tempPows.Checked)
                {
                    data[writeIndex++] = getParamSelectFlag(); // SL
                }
                if(cb_use_Phase.Checked)
                    data[writeIndex++] = cb_use_Phase.Checked ? (byte)0x01 : (byte)0x00; // Phase
                if (cb_use_powerSave.Checked)
                {
                    data[writeIndex++] = Convert.ToByte(txtPowerSave.Text); // PowerSave
                }
            }
            else
            {
                data[writeIndex++] = 0x89;
            }
            data[writeIndex++] = Convert.ToByte(txtRepeat.Text);//Repeat
            int msgLen = writeIndex + 1;
            data[1] = (byte)(msgLen - 2); // len

            byte[] checkdata = new byte[msgLen-1];
            Array.Copy(data, 0, checkdata, 0, checkdata.Length);
            data[writeIndex] = reader.CheckValue(checkdata);
            Array.Resize(ref data, msgLen);

            //Console.WriteLine("Send: {0}", CCommondMethod.ToHex(data, "", " "));
            reader.SendMessage(data);
        }

        private void cmdFastInventorySend(bool antG1)
        {
            beforeCmdExecTime = DateTime.Now;
            BeginInvoke(new ThreadStart(delegate () {
                //Console.WriteLine("cmdFastInventorySend [G{0}] 开始快速盘存  ##{1}##", useAntG1 ? "1" : "2", m_FastExeCount);
                int writeIndex = 0;
                byte[] rawData = new byte[256];
                rawData[writeIndex++] = 0xA0; // hdr

                rawData[writeIndex++] = 0x03; // len minLen = 3
                rawData[writeIndex++] = m_curSetting.btReadId; // addr

                rawData[writeIndex++] = 0x8A; // cmd
                if(antType1.Checked)
                {
                    for (int i = 0; i < 4; i++)
                    {

                        rawData[writeIndex++] = (byte)(Convert.ToInt32(fast_inv_ants[i].Text) - 1);
                        if (i == 0)
                        {
                            rawData[writeIndex++] = Convert.ToByte(fast_inv_stays[i].Text);
                        }
                        else
                        {
                            rawData[writeIndex++] = 0x00;
                        }
                    }
                    rawData[writeIndex++] = Convert.ToByte(this.txtInterval.Text); // Interval, 0 ms

                    rawData[writeIndex++] = Convert.ToByte(txtRepeat.Text); // Repeat
                }
                if (antType4.Checked)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        rawData[writeIndex++] = (byte)(Convert.ToInt32(fast_inv_ants[i].Text) - 1);
                        rawData[writeIndex++] = Convert.ToByte(fast_inv_stays[i].Text);
                    }

                    rawData[writeIndex++] = Convert.ToByte(this.txtInterval.Text); // Interval, 0 ms

                    rawData[writeIndex++] = Convert.ToByte(txtRepeat.Text); // Repeat
                }
                else if (antType16.Checked || antType8.Checked)
                {
                    // data
                    if (antG1)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            rawData[writeIndex++] = (byte)(Convert.ToInt32(fast_inv_ants[i].Text) - 1);
                            rawData[writeIndex++] = Convert.ToByte(fast_inv_stays[i].Text);
                        }
                    }
                    else
                    {
                        for (int i = 8; i < 16; i++)
                        {
                            rawData[writeIndex++] = (byte)(Convert.ToInt32(fast_inv_ants[i].Text) - 8);
                            rawData[writeIndex++] = Convert.ToByte(fast_inv_stays[i].Text);
                        }
                    }
                    //Console.WriteLine("antType8/16 end [G{0}]", useAntG1 ? "1" : "2");
                    rawData[writeIndex++] = Convert.ToByte(this.txtInterval.Text); // Interval, 0 ms

                    if (cb_customized_session_target.Checked)
                    {
                        if (cb_use_selectFlags_tempPows.Checked)
                        {
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_1.Text); // Reserve
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_2.Text);
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_3.Text);
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_4.Text);


                            rawData[writeIndex++] = getParamSelectFlag();//SL
                            rawData[writeIndex++] = getParamSession(); // session
                            rawData[writeIndex++] = (byte)(invTargetB == false ? 0x00 : 0x01); // Target
                            rawData[writeIndex++] = cb_use_Phase.Checked ? (byte)0x01 : (byte)0x00; // Phase
                            if (antG1) //Temp Power > 20
                            {
                                for (int i = 0; i < 8; i++)
                                {
                                    rawData[writeIndex++] = Convert.ToByte(fast_inv_temp_pows[i].Text);
                                }
                            }
                            else
                            {
                                for (int i = 8; i < 16; i++)
                                {
                                    rawData[writeIndex++] = Convert.ToByte(fast_inv_temp_pows[i].Text);
                                }
                            }
                        }
                        else
                        {
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_1.Text); // Reserve
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_2.Text);
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_3.Text);
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_4.Text);
                            rawData[writeIndex++] = Convert.ToByte(this.tb_fast_inv_reserved_5.Text);

                            rawData[writeIndex++] = getParamSession(); // session

                            if (ReverseTarget)
                            {
                                if (invTargetB && stayBTimes > 1)
                                {
                                    stayBTimes--;
                                    rawData[writeIndex++] = 0x01; // Target B
                                }
                                else
                                {
                                    stayBTimes = Convert.ToInt32(tb_fast_inv_staytargetB_times.Text);
                                    rawData[writeIndex++] = (byte)(invTargetB == false ? 0x00 : 0x01); // Target

                                    invTargetB = !invTargetB;
                                }
                            }
                            else
                            {
                                rawData[writeIndex++] = getParamTarget(); // Target
                            }

                            rawData[writeIndex++] = Convert.ToByte(txtOptimize.Text, 16); // Optimize

                            rawData[writeIndex++] = Convert.ToByte(txtOngoing.Text, 16);//Ongoing

                            rawData[writeIndex++] = Convert.ToByte(txtTargetQuantity.Text);//Target Quantity

                            rawData[writeIndex++] = cb_use_Phase.Checked ? (byte)0x01 : (byte)0x00; // Phase
                        }
                    }


                    rawData[writeIndex++] = Convert.ToByte(txtRepeat.Text); // Repeat
                }

                int msgLen = writeIndex + 1;
                rawData[1] = (byte)(msgLen - 2); // except hdr+len
                //Console.WriteLine("快速盘存 writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

                byte[] checkData = new byte[msgLen - 1];
                Array.Copy(rawData, 0, checkData, 0, checkData.Length);
                rawData[writeIndex] = reader.CheckValue(checkData); // check

                Array.Resize(ref rawData, msgLen);

                //Console.WriteLine("快速盘存: {0}", CCommondMethod.ToHex(rawData, "", " "));
                int nResult = reader.SendMessage(rawData);
            }));
        }

        public void FastInventory()
        {
            BeginInvoke(new ThreadStart(delegate() {
                //Console.WriteLine("-----------------RunLoopFastSwitch");
                if (antType16.Checked)
                {
                    if (useAntG1)
                    {
                        if (checkFastInvAntG2Count())
                        {
                            cmdSwitchAntG2();
                        }
                        else
                        {
                            if (m_FastExeCount == -1)
                            {
                                RefreshInventoryInfo();
                                cmdFastInventorySend(useAntG1);
                            }
                            else
                            {
                                if (m_FastExeCount > 1)
                                {
                                    m_FastExeCount--;
                                    RefreshInventoryInfo();
                                    cmdFastInventorySend(useAntG1);
                                }
                                else
                                {
                                    stopFastInv();
                                }
                            }
                        }
                    }
                    else
                    {
                        RefreshInventoryInfo();
                        if (m_FastExeCount == -1)
                        {
                            if (checkFastInvAntG1Count())
                            {
                                cmdSwitchAntG1();
                            }
                            else
                            {
                                cmdFastInventorySend(useAntG1);
                            }
                        }
                        else
                        {
                            if (m_FastExeCount > 1)
                            {
                                m_FastExeCount--;
                                if (checkFastInvAntG1Count())
                                {
                                    cmdSwitchAntG1();
                                }
                                else
                                {
                                    cmdFastInventorySend(useAntG1);
                                }
                            }
                            else
                            {
                                stopFastInv();
                            }
                        }
                    }
                }
                else
                {
                    if (m_FastExeCount == -1)
                    {
                        //Console.WriteLine("m_FastExeCount < 0 ，循环");
                        RefreshInventoryInfo();
                        cmdFastInventorySend(useAntG1);
                    }
                    else
                    {
                        if (m_FastExeCount > 1)
                        {
                            m_FastExeCount--;
                            RefreshInventoryInfo();
                            //Console.WriteLine("循环次数=" + m_FastExeCount + ", 开始下一次");
                            cmdFastInventorySend(useAntG1);
                        }
                        else
                        {
                            btnInventory.BackColor = Color.WhiteSmoke;
                            btnInventory.ForeColor = Color.DarkBlue;
                            btnInventory.Text = "开始盘存";
                            //Console.WriteLine("循环次数=0，结束");
                            stopFastInv();
                        }
                    }
                }
            }));
        }

        private delegate void RefreshISO18000Unsafe(byte btCmd);
        private void RefreshISO18000(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshISO18000Unsafe InvokeRefreshISO18000 = new RefreshISO18000Unsafe(RefreshISO18000);
                this.Invoke(InvokeRefreshISO18000, new object[] { btCmd });
            }
            else
            {
                switch (btCmd)
                {
                    case 0xb0:
                        {
                            ltvTagISO18000.Items.Clear();
                            int nLength = m_curOperateTagISO18000Buffer.dtTagTable.Rows.Count;
                            int nIndex = 1;
                            foreach (DataRow row in m_curOperateTagISO18000Buffer.dtTagTable.Rows)
                            {
                                ListViewItem item = new ListViewItem();
                                item.Text = nIndex.ToString();
                                item.SubItems.Add(row[1].ToString());
                                item.SubItems.Add(row[0].ToString());
                                item.SubItems.Add(row[2].ToString());
                                ltvTagISO18000.Items.Add(item);

                                nIndex++;
                            }

                            //txtTagCountISO18000.Text = m_curOperateTagISO18000Buffer.dtTagTable.Rows.Count.ToString();

                            if (m_bContinue)
                            {
                                reader.InventoryISO18000(m_curSetting.btReadId);
                            }
                            else
                            {
                                WriteLog(lrtxtLog, "停止盘存", 0);
                            }
                        }
                        break;
                    case 0xb1:
                        {
                            htxtReadData18000.Text = m_curOperateTagISO18000Buffer.strReadData;
                        }
                        break;
                    case 0xb2:
                        {
                            //txtWriteLength.Text = m_curOperateTagISO18000Buffer.btWriteLength.ToString();
                        }
                        break;
                    case 0xb3:
                        {
                            //switch(m_curOperateTagISO18000Buffer.btStatus)
                            //{
                            //    case 0x00:
                            //        MessageBox.Show("该字节成功锁定");
                            //        break;
                            //    case 0xFE:
                            //        MessageBox.Show("该字节已是锁定状态");
                            //        break;
                            //    case 0xFF:
                            //        MessageBox.Show("该字节无法锁定");
                            //        break;
                            //    default:
                            //        break;
                            //}
                        }
                        break;
                    case 0xb4:
                        {
                            switch (m_curOperateTagISO18000Buffer.btStatus)
                            {
                                case 0x00:
                                    txtStatus.Text = "该字节未锁定";
                                    break;
                                case 0xFE:
                                    txtStatus.Text = "该字节已是锁定状态";
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        private delegate void RunLoopISO18000Unsafe(int nLength);
        private void RunLoopISO18000(int nLength)
        {
            if (this.InvokeRequired)
            {
                RunLoopISO18000Unsafe InvokeRunLoopISO18000 = new RunLoopISO18000Unsafe(RunLoopISO18000);
                this.Invoke(InvokeRunLoopISO18000, new object[] { nLength });
            }
            else
            {
                //判断写入是否正确
                if (nLength == m_nBytes)
                {
                    m_nLoopedTimes++;
                    txtLoopTimes.Text = m_nLoopedTimes.ToString();
                }
                //判断是否循环结束
                m_nLoopTimes--;
                if (m_nLoopTimes > 0)
                {
                    WriteTagISO18000();
                }
            }
        }

        private void connectType_CheckedChanged(object sender, EventArgs e)
        {
            if (radio_btn_rs232.Checked)
            {
                grb_rs232.Visible = true;
                grb_tcp.Visible = false;
                btnConnect.Enabled = true;
            }
            else if (radio_btn_tcp.Checked)
            {
                grb_rs232.Visible = false;
                grb_tcp.Visible = true;
                btnConnect.Enabled = true;
            }
        }

        private void SetFormEnable(bool bIsEnable)
        {
            gbConnectType.Enabled = (!bIsEnable);
            gbCmdReaderAddress.Enabled = bIsEnable;
            gbCmdVersion.Enabled = bIsEnable;
            gbCmdBaudrate.Enabled = bIsEnable;
            gbCmdTemperature.Enabled = bIsEnable;
            gbCmdOutputPower.Enabled = bIsEnable;
            gbCmdAntenna.Enabled = bIsEnable;
            //gbCmdDrm.Enabled = bIsEnable;
            gbCmdRegion.Enabled = bIsEnable;
            gbCmdBeeper.Enabled = bIsEnable;
            gbCmdReadGpio.Enabled = bIsEnable;
            gbCmdAntDetector.Enabled = bIsEnable;
            gbReturnLoss.Enabled = bIsEnable;
            gbProfile.Enabled = bIsEnable;

            btnResetReader.Enabled = bIsEnable;

            gbCmdOperateTag.Enabled = bIsEnable;

            btnInventoryISO18000.Enabled = bIsEnable;
            btnClear.Enabled = bIsEnable;
            gbISO1800ReadWrite.Enabled = bIsEnable;
            gbISO1800LockQuery.Enabled = bIsEnable;

            gbCmdManual.Enabled = bIsEnable;

            tab_6c_Tags_Test.Enabled = bIsEnable;

            gbMonza.Enabled = bIsEnable;
            lbChangeBaudrate.Enabled = bIsEnable;
            cmbSetBaudrate.Enabled = bIsEnable;
            btnSetUartBaudrate.Enabled = bIsEnable;
            btReaderSetupRefresh.Enabled = bIsEnable;

            btRfSetup.Enabled = bIsEnable;
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            if(btnConnect.Text.Equals("连接读写器"))
            {
                ConnectReader();
            }
            else if (btnConnect.Text.Equals("断开连接"))
            {
                btnConnect.Text = "连接读写器";
                DisconnectReader();
            }
        }

        private void DisconnectReader()
        {
            Inventorying = false;
            isFastInv = false;
            isBufferInv = false;
            isRealInv = false;
            doingFastInv = false;
            doingRealInv = false;
            doingBufferInv = false;
            setInvStoppedStatus();

            if (radio_btn_rs232.Checked)
            {
                //处理串口断开连接读写器
                reader.CloseCom();

                //处理界面元素是否有效
                SetFormEnable(false);
            }
            else if(radio_btn_tcp.Checked)
            {
                //处理断开Tcp连接读写器
                reader.SignOut();

                //处理界面元素是否有效
                SetFormEnable(false);
            }
        }

        private void ConnectReader()
        {
            if(radio_btn_rs232.Checked)
            {
                //处理串口连接读写器
                string strException = string.Empty;
                string strComPort = cmbComPort.Text;
                int nBaudrate = Convert.ToInt32(cmbBaudrate.Text);

                int nRet = reader.OpenCom(strComPort, nBaudrate, out strException);
                if (nRet != 0)
                {
                    string strLog = "连接读写器失败，失败原因： " + strException;
                    WriteLog(lrtxtLog, strLog, 1);

                    return;
                }
                else
                {
                    string strLog = "连接读写器 " + strComPort + "@" + nBaudrate.ToString();
                    WriteLog(lrtxtLog, strLog, 0);
                    btnConnect.Text = "断开连接";
                }

                //处理界面元素是否有效
                SetFormEnable(true);
            }
            else if(radio_btn_tcp.Checked)
            {
                try
                {
                    //处理Tcp连接读写器
                    string strException = string.Empty;
                    IPAddress ipAddress = IPAddress.Parse(ipIpServer.IpAddressStr);
                    int nPort = Convert.ToInt32(txtTcpPort.Text);

                    int nRet = reader.ConnectServer(ipAddress, nPort, out strException);
                    if (nRet != 0)
                    {
                        string strLog = "连接读写器失败，失败原因： " + strException;
                        WriteLog(lrtxtLog, strLog, 1);

                        return;
                    }
                    else
                    {
                        string strLog = "连接读写器 " + ipIpServer.IpAddressStr + "@" + nPort.ToString();
                        WriteLog(lrtxtLog, strLog, 0);
                        btnConnect.Text = "断开连接";
                    }

                    //处理界面元素是否有效
                    SetFormEnable(true);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnResetReader_Click(object sender, EventArgs e)
        {
            int nRet = reader.Reset(m_curSetting.btReadId);
            if (nRet != 0)
            {
                string strLog = "复位读写器失败";
                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                string strLog = "复位读写器";
                m_curSetting.btReadId = (byte)0xFF;
                WriteLog(lrtxtLog, strLog, 0);
            }
        }

        private void btnSetReadAddress_Click(object sender, EventArgs e)
        {
            try
            {
                if (htxtReadId.Text.Length != 0)
                {
                    string strTemp = htxtReadId.Text.Trim();
                    reader.SetReaderAddress(m_curSetting.btReadId, Convert.ToByte(strTemp, 16));
                    m_curSetting.btReadId = Convert.ToByte(strTemp, 16);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ProcessSetReadAddress(Reader.MessageTran msgTran)
        {
            string strCmd = "设置读写器地址";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnGetFirmwareVersion_Click(object sender, EventArgs e)
        {
            reader.GetFirmwareVersion(m_curSetting.btReadId);
        }

        private void ProcessGetFirmwareVersion(Reader.MessageTran msgTran)
        {
            string strCmd = "取得读写器版本号";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                m_curSetting.btMajor = msgTran.AryData[0];
                m_curSetting.btMinor = msgTran.AryData[1];
                m_curSetting.btReadId = msgTran.ReadId;

                RefreshReadSetting(msgTran.Cmd);
                WriteLog(lrtxtLog, strCmd, 0);

                cmdGetInternalVersion();

                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        #region GetInternalVersion
        private void cmdGetInternalVersion()
        {
            // minLen = addr + cmd + check = 3
            // [hdr][len][addr][cmd][data][check]
            // 0xA0 len addr 0xAA 
            int index = 0;
            byte[] rawData = new byte[256];
            rawData[index++] = 0xA0;
            rawData[index++] = 3;
            rawData[index++] = m_curSetting.btReadId;
            rawData[index++] = 0xAA;

            int msgLen = index + 1;
            rawData[1] = (byte)(msgLen - 2); // update len, except hdr+len

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[index++] = reader.CheckValue(checkData);

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            int nResult = reader.SendMessage(sendData);
        }

        private void ProcessGetInternalVersion(MessageTran msgTran)
        {
            string strCmd = "取得读写器内部版本号";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                m_curSetting.btInternalVersion = msgTran.AryData[0];
                BeginInvoke(new ThreadStart(delegate () {
                    txtFirmwareVersion.Text = String.Format("{0}.{1}.{2}", m_curSetting.btMajor, m_curSetting.btMinor, BitConverter.ToString(new byte[] { m_curSetting.btInternalVersion }));
                }));
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }
        #endregion GetInternalVersion

        private void btnSetUartBaudrate_Click(object sender, EventArgs e)
        {
            if (cmbSetBaudrate.SelectedIndex != -1)
            {
                reader.SetUartBaudrate(m_curSetting.btReadId, cmbSetBaudrate.SelectedIndex + 3);
                m_curSetting.btIndexBaudrate = Convert.ToByte(cmbSetBaudrate.SelectedIndex);
            }
        }

        private void ProcessSetUartBaudrate(Reader.MessageTran msgTran)
        {
            string strCmd = "设置波特率";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnGetReaderTemperature_Click(object sender, EventArgs e)
        {
            reader.GetReaderTemperature(m_curSetting.btReadId);
        }

        private void ProcessGetReaderTemperature(Reader.MessageTran msgTran)
        {
            string strCmd = "取得读写器温度";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btPlusMinus = msgTran.AryData[0];
                m_curSetting.btTemperature = msgTran.AryData[1];

                RefreshReadSetting(msgTran.Cmd);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnGetOutputPower_Click(object sender, EventArgs e)
        {
            if (antType16.Checked)
            {
                m_getOutputPower = true;
                m_curSetting.btAntGroup = 0x00;
                reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                reader.GetOutputPower(m_curSetting.btReadId);
            }
            else if (antType8.Checked)
            {
                reader.GetOutputPower(m_curSetting.btReadId);
            }
            else if (antType4.Checked || antType1.Checked)
            {
                reader.GetOutputPowerFour(m_curSetting.btReadId);
            }
        }

        private void ProcessGetOutputPower(Reader.MessageTran msgTran)
        {
            string strCmd = "取得输出功率";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btOutputPower = msgTran.AryData[0];

                RefreshReadSetting(0x77);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 8)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                if (antType16.Checked && m_getOutputPower)
                {
                    if (m_curSetting.btAntGroup == (byte)0x00)
                    {
                        m_curSetting.btOutputPowers = msgTran.AryData;
                        m_curSetting.btAntGroup = 0x01;
                        reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                    }
                    else
                    {
                        byte[] btPowers = new byte[m_curSetting.btOutputPowers.Length + msgTran.AryData.Length];
                        Array.Copy(m_curSetting.btOutputPowers, 0, btPowers, 0, m_curSetting.btOutputPowers.Length);
                        Array.Copy(msgTran.AryData, 0, btPowers, m_curSetting.btOutputPowers.Length, msgTran.AryData.Length);
                        m_curSetting.btOutputPowers = btPowers;
                        m_getOutputPower = false;
                    }
                }
                else
                {
                    m_curSetting.btOutputPowers = msgTran.AryData;
                }

                RefreshReadSetting(0x97);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 4)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btOutputPowers = msgTran.AryData;

                RefreshReadSetting(0x77);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetOutputPower_Click(object sender, EventArgs e)
        {
            try
            {
                if (antType16.Checked)
                {
                    m_setOutputPower = true;
                    m_curSetting.btAntGroup = 0x00;
                    reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                }
                else if (antType8.Checked)
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

                        //m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                        reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                        // m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                    }
                }
                else if (antType4.Checked)
                {
                    if (tb_outputpower_1.Text.Length != 0 || tb_outputpower_2.Text.Length != 0 || tb_outputpower_3.Text.Length != 0 || tb_outputpower_4.Text.Length != 0)
                    {
                        byte[] OutputPower = new byte[4];
                        OutputPower[0] = Convert.ToByte(tb_outputpower_1.Text);
                        OutputPower[1] = Convert.ToByte(tb_outputpower_2.Text);
                        OutputPower[2] = Convert.ToByte(tb_outputpower_3.Text);
                        OutputPower[3] = Convert.ToByte(tb_outputpower_4.Text);
                        //m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                        reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                        // m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                    }
                }
                else if (antType1.Checked)
                {
                    if (tb_outputpower_1.Text.Length != 0)
                    {
                        byte[] OutputPower = new byte[1];
                        OutputPower[0] = Convert.ToByte(tb_outputpower_1.Text);
                        //m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                        reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                        // m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ProcessSetOutputPower(Reader.MessageTran msgTran)
        {
            string strCmd = "设置输出功率";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    if (antType16.Checked && m_setOutputPower)
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            m_curSetting.btAntGroup = 0x01;
                            reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        }
                        else
                            m_setOutputPower = false;
                    }
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnGetWorkAntenna_Click(object sender, EventArgs e)
        {
            m_getWorkAnt = true;
            reader.GetReaderAntGroup(m_curSetting.btReadId);
        }

        private void ProcessGetWorkAntenna(Reader.MessageTran msgTran)
        {
            string strCmd = "取得工作天线";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01 || msgTran.AryData[0] == 0x02 || msgTran.AryData[0] == 0x03
                    || msgTran.AryData[0] == 0x04 || msgTran.AryData[0] == 0x05 || msgTran.AryData[0] == 0x06 || msgTran.AryData[0] == 0x07)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btWorkAntenna = msgTran.AryData[0];

                    RefreshReadSetting(0x75);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetWorkAntenna_Click(object sender, EventArgs e)
        {
            if (cmbWorkAnt.SelectedIndex != -1)
            {
                m_setWorkAnt = true;
                byte btWorkAntenna = (byte)cmbWorkAnt.SelectedIndex;
                if (btWorkAntenna >= 0x08)
                    m_curSetting.btAntGroup = 0x01;
                else
                    m_curSetting.btAntGroup = 0x00;
                reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
            }
        }

        private void ProcessSetWorkAntenna(Reader.MessageTran msgTran)
        {
            int intCurrentAnt = 0;
            if (antType16.Checked && m_curSetting.btAntGroup == (byte)0x01)
                intCurrentAnt = m_curSetting.btWorkAntenna + 9;
            else
                intCurrentAnt = m_curSetting.btWorkAntenna + 1;
            string strCmd = "设置工作天线成功,当前工作天线: 天线" + intCurrentAnt.ToString();

            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);
                    if(Inventorying)
                        RunLoopInventroy();
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
            if(Inventorying)
            {
                if(radio_btn_realtime_inv.Checked)
                {
                    stopRealInv();
                }
                else if (radio_btn_fast_inv.Checked)
                {
                    stopFastInv();
                }
                else if (radio_btn_cache_inv.Checked)
                {
                    stopBufferInv();
                }
            }
        }

        private void btnGetDrmMode_Click(object sender, EventArgs e)
        {
            reader.GetDrmMode(m_curSetting.btReadId);
        }

        private void ProcessGetDrmMode(Reader.MessageTran msgTran)
        {
            string strCmd = "取得DRM模式";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btDrmMode = msgTran.AryData[0];

                    RefreshReadSetting(0x7D);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetDrmMode_Click(object sender, EventArgs e)
        {
            byte btDrmMode = 0xFF;
            /*
            if (rdbDrmModeClose.Checked)
            {
                btDrmMode = 0x00;
            }
            else if (rdbDrmModeOpen.Checked)
            {
                btDrmMode = 0x01;
            }
            else
            {
                return;
            }
             */

            reader.SetDrmMode(m_curSetting.btReadId, btDrmMode);
            m_curSetting.btDrmMode = btDrmMode;
        }

        private void ProcessSetDrmMode(Reader.MessageTran msgTran)
        {
            string strCmd = "设置DRM模式";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void rdbRegionFcc_CheckedChanged(object sender, EventArgs e)
        {
            cmbFrequencyStart.SelectedIndex = -1;
            cmbFrequencyEnd.SelectedIndex = -1;
            cmbFrequencyStart.Items.Clear();
            cmbFrequencyEnd.Items.Clear();

            float nStart = 902.00f;
            for (int nloop = 0; nloop < 53; nloop++)
            {
                string strTemp = nStart.ToString("0.00");
                cmbFrequencyStart.Items.Add(strTemp);
                cmbFrequencyEnd.Items.Add(strTemp);

                nStart += 0.5f;
            }
        }

        private void rdbRegionEtsi_CheckedChanged(object sender, EventArgs e)
        {
            cmbFrequencyStart.SelectedIndex = -1;
            cmbFrequencyEnd.SelectedIndex = -1;
            cmbFrequencyStart.Items.Clear();
            cmbFrequencyEnd.Items.Clear();

            float nStart = 865.00f;
            for (int nloop = 0; nloop < 7; nloop++)
            {
                string strTemp = nStart.ToString("0.00");
                cmbFrequencyStart.Items.Add(strTemp);
                cmbFrequencyEnd.Items.Add(strTemp);

                nStart += 0.5f;
            }
        }

        private void rdbRegionChn_CheckedChanged(object sender, EventArgs e)
        {
            cmbFrequencyStart.SelectedIndex = -1;
            cmbFrequencyEnd.SelectedIndex = -1;
            cmbFrequencyStart.Items.Clear();
            cmbFrequencyEnd.Items.Clear();

            float nStart = 920.00f;
            for (int nloop = 0; nloop < 11; nloop++)
            {
                string strTemp = nStart.ToString("0.00");
                cmbFrequencyStart.Items.Add(strTemp);
                cmbFrequencyEnd.Items.Add(strTemp);

                nStart += 0.5f;
            }
        }

        private string GetFreqString(byte btFreq)
        {
            string strFreq = string.Empty;

            if (m_curSetting.btRegion == 4)
            {
                float nExtraFrequency = btFreq * m_curSetting.btUserDefineFrequencyInterval * 10;
                float nstartFrequency = ((float)m_curSetting.nUserDefineStartFrequency) / 1000;
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

        private void btnGetFrequencyRegion_Click(object sender, EventArgs e)
        {
            reader.GetFrequencyRegion(m_curSetting.btReadId);
        }

        private void ProcessGetFrequencyRegion(Reader.MessageTran msgTran)
        {
            string strCmd = "取得射频规范";
            string strErrorCode = string.Empty;

            parseGetFrequencyRegion(msgTran.AryData);

            if (msgTran.AryData.Length == 3)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btRegion = msgTran.AryData[0];
                m_curSetting.btFrequencyStart = msgTran.AryData[1];
                m_curSetting.btFrequencyEnd = msgTran.AryData[2];

                RefreshReadSetting(0x79);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 6)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btRegion = msgTran.AryData[0];
                m_curSetting.btUserDefineFrequencyInterval = msgTran.AryData[1];
                m_curSetting.btUserDefineChannelQuantity = msgTran.AryData[2];
                m_curSetting.nUserDefineStartFrequency = msgTran.AryData[3] * 256 * 256 + msgTran.AryData[4] * 256 + msgTran.AryData[5];
                RefreshReadSetting(0x79);
                WriteLog(lrtxtLog, strCmd, 0);
                return;


            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetFrequencyRegion_Click(object sender, EventArgs e)
        {
            try
            {
                if (cbUserDefineFreq.Checked == true)
                {
                    int nStartFrequency = Convert.ToInt32(textStartFreq.Text);
                    int nFrequencyInterval = Convert.ToInt32(TextFreqInterval.Text);
                    nFrequencyInterval = nFrequencyInterval / 10;
                    byte btChannelQuantity = Convert.ToByte(textFreqQuantity.Text);
                    reader.SetUserDefineFrequency(m_curSetting.btReadId, nStartFrequency, (byte)nFrequencyInterval, btChannelQuantity);
                    m_curSetting.btRegion = 4;
                    m_curSetting.nUserDefineStartFrequency = nStartFrequency;
                    m_curSetting.btUserDefineFrequencyInterval = (byte)nFrequencyInterval;
                    m_curSetting.btUserDefineChannelQuantity = btChannelQuantity;
                }
                else
                {
                    byte btRegion = 0x00;
                    byte btStartFreq = 0x00;
                    byte btEndFreq = 0x00;

                    int nStartIndex = cmbFrequencyStart.SelectedIndex;
                    int nEndIndex = cmbFrequencyEnd.SelectedIndex;
                    if (nEndIndex < nStartIndex)
                    {
                        MessageBox.Show("频谱范围不符合规范，请参考通讯协议");
                        return;
                    }

                    if (rdbRegionFcc.Checked)
                    {
                        btRegion = 0x01;
                        btStartFreq = Convert.ToByte(nStartIndex + 7);
                        btEndFreq = Convert.ToByte(nEndIndex + 7);
                    }
                    else if (rdbRegionEtsi.Checked)
                    {
                        btRegion = 0x02;
                        btStartFreq = Convert.ToByte(nStartIndex);
                        btEndFreq = Convert.ToByte(nEndIndex);
                    }
                    else if (rdbRegionChn.Checked)
                    {
                        btRegion = 0x03;
                        btStartFreq = Convert.ToByte(nStartIndex + 43);
                        btEndFreq = Convert.ToByte(nEndIndex + 43);
                    }
                    else
                    {
                        return;
                    }

                    reader.SetFrequencyRegion(m_curSetting.btReadId, btRegion, btStartFreq, btEndFreq);
                    m_curSetting.btRegion = btRegion;
                    m_curSetting.btFrequencyStart = btStartFreq;
                    m_curSetting.btFrequencyEnd = btEndFreq;
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessSetFrequencyRegion(Reader.MessageTran msgTran)
        {
            string strCmd = "设置射频规范";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetBeeperMode_Click(object sender, EventArgs e)
        {
            byte btBeeperMode = 0xFF;

            if (rdbBeeperModeSlient.Checked)
            {
                btBeeperMode = 0x00;
            }
            else if (rdbBeeperModeInventory.Checked)
            {
                btBeeperMode = 0x01;
            }
            else if (rdbBeeperModeTag.Checked)
            {
                btBeeperMode = 0x02;
            }
            else
            {
                return;
            }

            reader.SetBeeperMode(m_curSetting.btReadId, btBeeperMode);
            m_curSetting.btBeeperMode = btBeeperMode;
        }

        private void ProcessSetBeeperMode(Reader.MessageTran msgTran)
        {
            string strCmd = "设置蜂鸣器模式";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnReadGpioValue_Click(object sender, EventArgs e)
        {
            reader.ReadGpioValue(m_curSetting.btReadId);
        }

        private void ProcessReadGpioValue(Reader.MessageTran msgTran)
        {
            string strCmd = "读取GPIO状态";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btGpio1Value = msgTran.AryData[0];
                m_curSetting.btGpio2Value = msgTran.AryData[1];

                RefreshReadSetting(0x60);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnWriteGpio3Value_Click(object sender, EventArgs e)
        {
            byte btGpioValue = 0xFF;

            if (rdbGpio3Low.Checked)
            {
                btGpioValue = 0x00;
            }
            else if (rdbGpio3High.Checked)
            {
                btGpioValue = 0x01;
            }
            else
            {
                return;
            }

            reader.WriteGpioValue(m_curSetting.btReadId, 0x03, btGpioValue);
            m_curSetting.btGpio3Value = btGpioValue;
        }

        private void btnWriteGpio4Value_Click(object sender, EventArgs e)
        {
            byte btGpioValue = 0xFF;

            if (rdbGpio4Low.Checked)
            {
                btGpioValue = 0x00;
            }
            else if (rdbGpio4High.Checked)
            {
                btGpioValue = 0x01;
            }
            else
            {
                return;
            }

            reader.WriteGpioValue(m_curSetting.btReadId, 0x04, btGpioValue);
            m_curSetting.btGpio4Value = btGpioValue;
        }

        private void ProcessWriteGpioValue(Reader.MessageTran msgTran)
        {
            string strCmd = "设置GPIO状态";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnGetAntDetector_Click(object sender, EventArgs e)
        {
            reader.GetAntDetector(m_curSetting.btReadId);
        }

        private void ProcessGetAntDetector(Reader.MessageTran msgTran)
        {
            string strCmd = "读取天线连接检测阈值";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                m_curSetting.btAntDetector = msgTran.AryData[0];

                RefreshReadSetting(0x63);
                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessGetMonzaStatus(Reader.MessageTran msgTran)
        {
            string strCmd = "读取Impinj Monza快速读TID功能";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x8D)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btMonzaStatus = msgTran.AryData[0];

                    RefreshReadSetting(0x8E);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessSetMonzaStatus(Reader.MessageTran msgTran)
        {
            string strCmd = "设置Impinj Monza快速读TID功能";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btAntDetector = msgTran.AryData[0];

                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessSetProfile(Reader.MessageTran msgTran)
        {
            string strCmd = "设置射频通讯链路配置";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btLinkProfile = msgTran.AryData[0];

                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessGetProfile(Reader.MessageTran msgTran)
        {
            string strCmd = "读取射频通讯链路配置";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if ((msgTran.AryData[0] >= 0xd0) && (msgTran.AryData[0] <= 0xd3))
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btLinkProfile = msgTran.AryData[0];

                    RefreshReadSetting(0x6A);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessSetReaderAntGroup(Reader.MessageTran msgTran)
        {
            string strCmd = "设置天线组 " + m_curSetting.btAntGroup;
            string strErrorCode;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);
                    if (m_setWorkAnt)
                    {
                        m_setWorkAnt = false;
                        byte btWorkAntenna = (byte)cmbWorkAnt.SelectedIndex;
                        if (btWorkAntenna >= 0x08)
                            btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                        reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                        m_curSetting.btWorkAntenna = btWorkAntenna;
                        return;
                    }
                    if (m_getOutputPower)
                    {
                        //当前切换天线组是为了获取功率
                        reader.GetOutputPower(m_curSetting.btReadId);
                        return;
                    }
                    if (m_setOutputPower)
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
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
                    else
                    {
                        // 其他情况
                        Console.WriteLine("设置天线组{0}成功, 但不做其他事情", useAntG1?"1":"2");
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
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }
        
        private byte getParamSelectFlag()
        {
            for (int i = 0; i < selectFlagArr.Length; i++)
            {
                if (selectFlagArr[i].Checked)
                    return (byte)i;
            }
            return 0x00;//default SL00
        }

        private byte getParamTarget()
        {
            for (int i = 0; i < targetArr.Length; i++)
            {
                if (targetArr[i].Checked)
                    return (byte)i;
            }
            return 0x00;//default target A
        }

        private byte getParamSession()
        {
            for(int i = 0; i < sessionArr.Length; i++)
            {
                if (sessionArr[i].Checked)
                    return (byte)i;
            }
            return 0x01;//default S1
        }

        private void ProcessGetReaderAntGroup(Reader.MessageTran msgTran)
        {
            string strCmd = "获取天线组";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x00 || msgTran.AryData[0] == 0x01)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    m_curSetting.btAntGroup = msgTran.AryData[0];
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
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessGetReaderIdentifier(Reader.MessageTran msgTran)
        {
            string strCmd = "读取读写器识别标记";
            string strErrorCode = string.Empty;
            short i;
            string readerIdentifier = "";

            if (msgTran.AryData.Length == 12)
            {
                m_curSetting.btReadId = msgTran.ReadId;
                for (i = 0; i < 12; i++)
                {
                    readerIdentifier = readerIdentifier + string.Format("{0:X2}", msgTran.AryData[i]) + " ";


                }
                m_curSetting.btReaderIdentifier = readerIdentifier;
                RefreshReadSetting(0x68);

                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessGetImpedanceMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "测量天线端口阻抗匹配";
            string strErrorCode = string.Empty;


            if (msgTran.AryData.Length == 1)
            {
                m_curSetting.btReadId = msgTran.ReadId;

                m_curSetting.btAntImpedance = msgTran.AryData[0];
                RefreshReadSetting(0x7E);

                WriteLog(lrtxtLog, strCmd, 0);
                return;
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }



        private void ProcessSetReaderIdentifier(Reader.MessageTran msgTran)
        {
            string strCmd = "设置读写器识别标记";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }


        private void btnSetAntDetector_Click(object sender, EventArgs e)
        {
            try
            {
                if (tbAntDectector.Text.Length != 0)
                {
                    reader.SetAntDetector(m_curSetting.btReadId, Convert.ToByte(tbAntDectector.Text));
                    m_curSetting.btAntDetector = Convert.ToByte(tbAntDectector.Text);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ProcessSetAntDetector(Reader.MessageTran msgTran)
        {
            string strCmd = "设置天线连接检测阈值";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    m_curSetting.btReadId = msgTran.ReadId;
                    WriteLog(lrtxtLog, strCmd, 0);

                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void ProcessFastSwitch(Reader.MessageTran msgTran)
        {
            string strCmd = "快速天线盘存";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
                if (isFastInv)
                {
                    FastInventory();
                }
                else
                {
                    stopFastInv();
                }
            }
            else if (msgTran.AryData.Length == 2)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[1]);
                Console.WriteLine("Return ant NO : " + (msgTran.AryData[0] + 1));
                string strLog = strCmd + "失败，失败原因： " + strErrorCode + "--" + "天线" + (msgTran.AryData[0] + 1);
                Console.WriteLine("快速天线盘存失败 {0}", strLog);
                WriteLog(lrtxtLog, strLog, 1);
            }
            else if (msgTran.AryData.Length == 7)
            {
                if(doingFastInv)
                {
                    //Console.WriteLine("快速天线盘存结束");
                    WriteLog(lrtxtLog, strCmd, 0);
                    BeginInvoke(new ThreadStart(delegate () {
                        tagdb.UpdateCmd8AExecuteSuccess(msgTran.AryData);
                        led_cmd_readrate.Text = tagdb.CmdReadRate.ToString(); // 单次盘存速率
                        led_cmd_total_tagreads.Text = tagdb.CmdTotalRead.ToString(); // 单次读取次数
                        led_cmd_execute_duration.Text = tagdb.CommandDuration.ToString(); // 单次执行时间
                        ledFast_total_execute_time.Text = FormatLongToTimeStr(tagdb.TotalCommandTime); //总执行时间
                    }));

                    if (isFastInv)
                    {
                        FastInventory();
                    }
                    else
                    {
                        stopFastInv();
                    }
                }
                else
                {
                    Console.WriteLine("快速盘存成功, 并且停止盘存: {0}", FastExecTimes);
                }
            }
            else
            {
                if (doingFastInv)
                {
                    parseInvTag(cb_use_Phase.Checked, msgTran.AryData, 0x8a);
                }
                else 
                {
                    Console.WriteLine("解析标签: {0}", FastExecTimes);
                }
            }
        }

        private void stopFastInv()
        {
            doingFastInv = false;
            Inventorying = false;
            Console.WriteLine("真正停止快速盘存");
            if (!useAntG1)
            {
                Console.WriteLine("真正停止快速盘存 V1, 并且切换回到天线组1");
                cmdSwitchAntG1();
            }
            
            BeginInvoke(new ThreadStart(delegate {
                setInvStoppedStatus();
            }));
        }

        private void stopRealInv()
        {
            doingRealInv = false;
            Inventorying = false;
            Console.WriteLine("真正停止实时盘存");
            if (!useAntG1)
            {
                Console.WriteLine("真正停止实时盘存, 并且切换回到天线组1");
                cmdSwitchAntG1();
            }

            BeginInvoke(new ThreadStart(delegate {
                setInvStoppedStatus();
            }));
        }

        private void stopBufferInv()
        {
            doingBufferInv = false;
            Inventorying = false;
            Console.WriteLine("真正停止缓存盘存");
            if (!useAntG1)
            {
                Console.WriteLine("真正停止缓存盘存, 并且切换回到天线组1");
                cmdSwitchAntG1();
            }

            BeginInvoke(new ThreadStart(delegate {
                setInvStoppedStatus();
            }));
        }

        private void ProcessInventoryReal(Reader.MessageTran msgTran)
        {
            string strCmd = "";
            if (msgTran.Cmd == 0x89)
            {
                strCmd = "实时盘存";
            }
            if (msgTran.Cmd == 0x8B)
            {
                strCmd = "自定义Session和Inventoried Flag盘存";
            }
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;
                WriteLog(lrtxtLog, strLog, 1);

                if (m_FastExeCount != -1)
                {
                    if (m_FastExeCount > 1)
                    {

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
                WriteLog(lrtxtLog, strCmd, 0);
                BeginInvoke(new ThreadStart(delegate() {
                    tagdb.UpdateCmd89ExecuteSuccess(msgTran.AryData);
                    led_cmd_readrate.Text = tagdb.CmdReadRate.ToString(); // 单次盘存速率
                    led_cmd_execute_duration.Text = tagdb.CommandDuration.ToString();// 单次盘存时间
                    ledFast_total_execute_time.Text = FormatLongToTimeStr(tagdb.TotalCommandTime); // 总盘存时间
                    led_cmd_total_tagreads.Text = tagdb.CmdTotalRead.ToString(); // 单次读取次数
                }));

                if (m_FastExeCount != -1)
                {
                    if (m_FastExeCount > 1)
                    {

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
            else // 解析标签
            {
                parseInvTag(cb_use_Phase.Checked, msgTran.AryData, 0x89);
            }
        }

        private void ProcessInventory(Reader.MessageTran msgTran)
        {
            string strCmd = "盘存标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 9)
            {
                WriteLog(lrtxtLog, strCmd, 0);
                BeginInvoke(new ThreadStart(delegate () {
                    tagdb.UpdateCmd80ExecuteSuccess(msgTran.AryData);
                    led_cmd_total_tagreads.Text = tagdb.CmdTotalRead.ToString();
                    led_cmd_execute_duration.Text = CalculateExecTime().ToString();
                    led_cmd_readrate.Text = tagdb.CmdReadRate.ToString();
                    txtTotalTagCount.Text =tagdb.TotalTagCounts.ToString();
                    led_totalread_count.Text = tagdb.TotalReadCounts.ToString();
                    
                }));

                if (m_FastExeCount != -1)
                {
                    if (m_FastExeCount > 1)
                    {

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
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);

            if (m_FastExeCount != -1)
            {
                if (m_FastExeCount > 1)
                {

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
            string strCmd = "读取缓存";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;
                WriteLog(lrtxtLog, strLog, 1);
                stopGetInventoryBuffer(false);
            }
            else
            {
                WriteLog(lrtxtLog, strCmd, 0);
                parseInvTag(false, msgTran.AryData, 0x90);
            }
        }

        private void ProcessGetAndResetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "读取清空缓存";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;
                WriteLog(lrtxtLog, strLog, 1);
                stopGetInventoryBuffer(true);
            }
            else
            {
                WriteLog(lrtxtLog, strCmd, 0);
                parseInvTag(false, msgTran.AryData, 0x91);
            }
        }

        private void stopGetInventoryBuffer(bool clearBuffer)
        {
            BeginInvoke(new ThreadStart(delegate() {
                lock(tagdb)
                {
                    Console.WriteLine("stopGetInventoryBuffer tagbufferCount={0}", tagbufferCount);
                    tagbufferCount = 0;
                    needGetBuffer = false;
                    if (clearBuffer)
                    {
                        btnGetAndClearBuffer.Text = "获取并清空缓存";
                    }
                    else
                    {
                        btnGetBuffer.Text = "获取缓存";
                    }
                    dispatcherTimer.Stop();
                    readratePerSecond.Stop();
                    elapsedTime = CalculateElapsedTime();

                    tagdb.Repaint();
                }
            }));
        }

        private void btnGetInventoryBufferTagCount_Click(object sender, EventArgs e)
        {
            reader.GetInventoryBufferTagCount(m_curSetting.btReadId);
        }

        private void ProcessGetInventoryBufferTagCount(Reader.MessageTran msgTran)
        {
            string strCmd = "缓存标签数量";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 2)
            {
                int nTagCount = Convert.ToInt32(msgTran.AryData[0]) * 256 + Convert.ToInt32(msgTran.AryData[1]);

                string strLog1 = strCmd + " " + nTagCount;
                WriteLog(lrtxtLog, strLog1, 0);
                return;
            }
            else if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;

            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnResetInventoryBuffer_Click(object sender, EventArgs e)
        {
            reader.ResetInventoryBuffer(m_curSetting.btReadId);
        }

        private void ProcessResetInventoryBuffer(Reader.MessageTran msgTran)
        {
            string strCmd = "清空缓存";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;

            WriteLog(lrtxtLog, strLog, 1);
        }

        private void cbAccessEpcMatch_CheckedChanged(object sender, EventArgs e)
        {
            if (ckAccessEpcMatch.Checked)
            {
                reader.GetAccessEpcMatch(m_curSetting.btReadId);
            }
            else
            {
                m_curOperateTagBuffer.strAccessEpcMatch = "";
                txtAccessEpcMatch.Text = "";
                reader.CancelAccessEpcMatch(m_curSetting.btReadId, 0x01);
            }
        }

        private void ProcessGetAccessEpcMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "取得选定标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x01)
                {
                    WriteLog(lrtxtLog, "未选定标签", 0);
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
                    m_curOperateTagBuffer.strAccessEpcMatch = CCommondMethod.ByteArrayToString(msgTran.AryData, 2, Convert.ToInt32(msgTran.AryData[1]));

                    RefreshOpTag(0x86);
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = "未知错误";
                }
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;

            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnSetAccessEpcMatch_Click(object sender, EventArgs e)
        {
            string[] reslut = CCommondMethod.StringToStringArray(cmbSetAccessEpcMatch.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("请选择EPC号");
                return;
            }

            byte[] btAryEpc = CCommondMethod.StringArrayToByteArray(reslut, reslut.Length);

            m_curOperateTagBuffer.strAccessEpcMatch = cmbSetAccessEpcMatch.Text;
            txtAccessEpcMatch.Text = cmbSetAccessEpcMatch.Text;
            ckAccessEpcMatch.Checked = true;
            reader.SetAccessEpcMatch(m_curSetting.btReadId, 0x00, Convert.ToByte(btAryEpc.Length), btAryEpc);
        }

        private void ProcessSetAccessEpcMatch(Reader.MessageTran msgTran)
        {
            string strCmd = "选定/取消选定标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == 0x10)
                {
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                }
            }
            else
            {
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;

            WriteLog(lrtxtLog, strLog, 1);
        }

        private void btnReadTag_Click(object sender, EventArgs e)
        {
            try
            {
                byte btMemBank = 0x00;
                byte btWordAdd = 0x00;
                byte btWordCnt = 0x00;

                if (rdbReserved.Checked)
                {
                    btMemBank = 0x00;
                }
                else if (rdbEpc.Checked)
                {
                    btMemBank = 0x01;
                }
                else if (rdbTid.Checked)
                {
                    btMemBank = 0x02;
                }
                else if (rdbUser.Checked)
                {
                    btMemBank = 0x03;
                }
                else
                {
                    MessageBox.Show("请选择读标签区域");
                    return;
                }

                if (txtWordAdd.Text.Length != 0)
                {
                    btWordAdd = Convert.ToByte(txtWordAdd.Text);
                }
                else
                {
                    MessageBox.Show("请选择读标签起始地址");
                    return;
                }

                if (txtWordCnt.Text.Length != 0)
                {
                    btWordCnt = Convert.ToByte(txtWordCnt.Text);
                }
                else
                {
                    MessageBox.Show("请选择读标签长度");
                    return;
                }

                string[] reslut = CCommondMethod.StringToStringArray(htxtReadAndWritePwd.Text.ToUpper(), 2);

                if (reslut != null && reslut.GetLength(0) != 4)
                {
                    MessageBox.Show("密码必须是空或者4个字节");
                    return;
                }
                byte[] btAryPwd = null;

                if (reslut != null)
                {
                    btAryPwd = CCommondMethod.StringArrayToByteArray(reslut, 4);
                }

                m_curOperateTagBuffer.dtTagTable.Clear();
                ltvOperate.Items.Clear();
                reader.ReadTag(m_curSetting.btReadId, btMemBank, btWordAdd, btWordCnt, btAryPwd);
                WriteLog(lrtxtLog, "读标签", 1);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void ProcessReadTag(Reader.MessageTran msgTran)
        {
            string strCmd = "读标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                // get raw data
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

                if (johar_cb.Checked)
                {
                    parseJoharRead(msgTran.AryTranData);
                }
                else
                {
                    DataRow row = m_curOperateTagBuffer.dtTagTable.NewRow();
                    row[0] = strPC;
                    row[1] = strCRC;
                    row[2] = strEPC;
                    row[3] = strData;
                    row[4] = nDataLen.ToString();
                    row[5] = strAntId;
                    row[6] = strReadCount;

                    m_curOperateTagBuffer.dtTagTable.Rows.Add(row);
                    m_curOperateTagBuffer.dtTagTable.AcceptChanges();
                    RefreshOpTag(0x81);
                    WriteLog(lrtxtLog, strCmd, 0);
                }
            }
        }

        private void btnWriteTag_Click(object sender, EventArgs e)
        {
            try
            {
                byte btMemBank = 0x00;
                byte btWordAdd = 0x00;
                byte btWordCnt = 0x00;
                byte btCmd = 0x00;
                if (radioButton1.Checked)
                {
                    btCmd = 0x82;
                }

                if (radioButton2.Checked)
                {
                    btCmd = 0x94;
                }

                if (rdbReserved.Checked)
                {
                    btMemBank = 0x00;
                }
                else if (rdbEpc.Checked)
                {
                    btMemBank = 0x01;
                }
                else if (rdbTid.Checked)
                {
                    btMemBank = 0x02;
                }
                else if (rdbUser.Checked)
                {
                    btMemBank = 0x03;
                }
                else
                {
                    MessageBox.Show("请选择读标签区域");
                    return;
                }

                if (txtWordAdd.Text.Length != 0)
                {
                    btWordAdd = Convert.ToByte(txtWordAdd.Text);
                }
                else
                {
                    MessageBox.Show("请选择读标签起始地址");
                    return;
                }

                if (txtWordCnt.Text.Length != 0)
                {
                    btWordCnt = Convert.ToByte(txtWordCnt.Text);
                }
                else
                {
                    MessageBox.Show("请选择读标签长度");
                    return;
                }

                string[] reslut = CCommondMethod.StringToStringArray(htxtReadAndWritePwd.Text.ToUpper(), 2);

                if (reslut == null)
                {
                    MessageBox.Show("输入字符无效");
                    return;
                }
                else if (reslut.GetLength(0) < 4)
                {
                    MessageBox.Show("至少输入4个字节");
                    return;
                }
                byte[] btAryPwd = CCommondMethod.StringArrayToByteArray(reslut, 4);

                reslut = CCommondMethod.StringToStringArray(htxtWriteData.Text.ToUpper(), 2);

                if (reslut == null)
                {
                    MessageBox.Show("输入字符无效");
                    return;
                }
                byte[] btAryWriteData = CCommondMethod.StringArrayToByteArray(reslut, reslut.Length);
                btWordCnt = Convert.ToByte(reslut.Length / 2 + reslut.Length % 2);

                txtWordCnt.Text = btWordCnt.ToString();

                m_curOperateTagBuffer.dtTagTable.Clear();
                ltvOperate.Items.Clear();
                reader.WriteTag(m_curSetting.btReadId, btAryPwd, btMemBank, btWordAdd, btWordCnt, btAryWriteData, btCmd);
                WriteLog(lrtxtLog, "写标签", 0);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private int WriteTagCount = 0;
        private void ProcessWriteTag(Reader.MessageTran msgTran)
        {
            string strCmd = "写标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                    WriteLog(lrtxtLog, strLog, 1);
                    return;
                }
                WriteTagCount++;


                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strData = string.Empty;

                byte byTemp = msgTran.AryData[nLen - 2];
                byte byAntId = (byte)((byTemp & 0x03) + 1);
                string strAntId = byAntId.ToString();

                string strReadCount = msgTran.AryData[nLen - 1].ToString();

                DataRow row = m_curOperateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                m_curOperateTagBuffer.dtTagTable.Rows.Add(row);
                m_curOperateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x82);
                WriteLog(lrtxtLog, strCmd, 0);
                if (WriteTagCount == (msgTran.AryData[0] * 256 + msgTran.AryData[1]))
                {
                    WriteTagCount = 0;
                }
            }
        }

        private void btnLockTag_Click(object sender, EventArgs e)
        {
            byte btMemBank = 0x00;
            byte btLockType = 0x00;

            if (rdbAccessPwd.Checked)
            {
                btMemBank = 0x04;
            }
            else if (rdbKillPwd.Checked)
            {
                btMemBank = 0x05;
            }
            else if (rdbEpcMermory.Checked)
            {
                btMemBank = 0x03;
            }
            else if (rdbTidMemory.Checked)
            {
                btMemBank = 0x02;
            }
            else if (rdbUserMemory.Checked)
            {
                btMemBank = 0x01;
            }
            else
            {
                MessageBox.Show("请选择保护区域");
                return;
            }

            if (rdbFree.Checked)
            {
                btLockType = 0x00;
            }
            else if (rdbFreeEver.Checked)
            {
                btLockType = 0x02;
            }
            else if (rdbLock.Checked)
            {
                btLockType = 0x01;
            }
            else if (rdbLockEver.Checked)
            {
                btLockType = 0x03;
            }
            else
            {
                MessageBox.Show("请选择保护类型");
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtLockPwd.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 4)
            {
                MessageBox.Show("至少输入4个字节");
                return;
            }

            byte[] btAryPwd = CCommondMethod.StringArrayToByteArray(reslut, 4);

            m_curOperateTagBuffer.dtTagTable.Clear();
            ltvOperate.Items.Clear();
            reader.LockTag(m_curSetting.btReadId, btAryPwd, btMemBank, btLockType);
        }

        private void ProcessLockTag(Reader.MessageTran msgTran)
        {
            string strCmd = "锁定标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                    WriteLog(lrtxtLog, strLog, 1);
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

                DataRow row = m_curOperateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                m_curOperateTagBuffer.dtTagTable.Rows.Add(row);
                m_curOperateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x83);
                WriteLog(lrtxtLog, strCmd, 0);
            }
        }

        private void btnKillTag_Click(object sender, EventArgs e)
        {
            string[] reslut = CCommondMethod.StringToStringArray(htxtKillPwd.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 4)
            {
                MessageBox.Show("至少输入4个字节");
                return;
            }

            byte[] btAryPwd = CCommondMethod.StringArrayToByteArray(reslut, 4);

            m_curOperateTagBuffer.dtTagTable.Clear();
            ltvOperate.Items.Clear();
            reader.KillTag(m_curSetting.btReadId, btAryPwd);
        }

        private void ProcessKillTag(Reader.MessageTran msgTran)
        {
            string strCmd = "销毁标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                int nLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                if (msgTran.AryData[nLen - 3] != 0x10)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[nLen - 3]);
                    string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                    WriteLog(lrtxtLog, strLog, 1);
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

                DataRow row = m_curOperateTagBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEPC;
                row[3] = strData;
                row[4] = string.Empty;
                row[5] = strAntId;
                row[6] = strReadCount;

                m_curOperateTagBuffer.dtTagTable.Rows.Add(row);
                m_curOperateTagBuffer.dtTagTable.AcceptChanges();

                RefreshOpTag(0x84);
                WriteLog(lrtxtLog, strCmd, 0);
            }
        }

        private void btnInventoryISO18000_Click(object sender, EventArgs e)
        {
            if (Inventorying)
            {
                MessageBox.Show("正在盘点...");
                return;
            }
            if (m_bContinue)
            {
                m_bContinue = false;
                btnInventoryISO18000.BackColor = Color.WhiteSmoke;
                btnInventoryISO18000.ForeColor = Color.Indigo;
                btnInventoryISO18000.Text = "开始盘存";
            }
            else
            {
                //判断EPC盘存是否正在进行
                if (Inventorying)
                {
                    if (MessageBox.Show("EPC C1G2标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        Console.WriteLine("btnInventoryISO18000_Click m_bContinue={0}", m_bContinue);
                        return;
                    }
                }

                m_curOperateTagISO18000Buffer.ClearBuffer();
                ltvTagISO18000.Items.Clear();
                m_bContinue = true;
                btnInventoryISO18000.BackColor = Color.Indigo;
                btnInventoryISO18000.ForeColor = Color.White;
                btnInventoryISO18000.Text = "停止盘存";

                string strCmd = "盘存标签";
                WriteLog(lrtxtLog, strCmd, 0);

                reader.InventoryISO18000(m_curSetting.btReadId);
            }
        }

        private void ProcessInventoryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "盘存标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] != 0xFF)
                {
                    strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                    string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                    WriteLog(lrtxtLog, strLog, 1);
                }
            }
            else if (msgTran.AryData.Length == 9)
            {
                string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                string strUID = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 8);

                //增加保存标签列表，原未盘存则增加记录，否则将标签盘存数量加1
                DataRow[] drs = m_curOperateTagISO18000Buffer.dtTagTable.Select(string.Format("UID = '{0}'", strUID));
                if (drs.Length == 0)
                {
                    DataRow row = m_curOperateTagISO18000Buffer.dtTagTable.NewRow();
                    row[0] = strAntID;
                    row[1] = strUID;
                    row[2] = "1";
                    m_curOperateTagISO18000Buffer.dtTagTable.Rows.Add(row);
                    m_curOperateTagISO18000Buffer.dtTagTable.AcceptChanges();
                }
                else
                {
                    DataRow row = drs[0];
                    row.BeginEdit();
                    row[2] = (Convert.ToInt32(row[2]) + 1).ToString();
                    m_curOperateTagISO18000Buffer.dtTagTable.AcceptChanges();
                }

            }
            else if (msgTran.AryData.Length == 2)
            {
                m_curOperateTagISO18000Buffer.nTagCnt = Convert.ToInt32(msgTran.AryData[1]);
                RefreshISO18000(msgTran.Cmd);

                //WriteLog(lrtxtLog, strCmd, 0);
            }
            else
            {
                strErrorCode = "未知错误";
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
        }

        private void btnReadTagISO18000_Click(object sender, EventArgs e)
        {
            if (htxtReadUID.Text.Length == 0)
            {
                MessageBox.Show("请输入UID");
                return;
            }
            if (htxtReadStartAdd.Text.Length == 0)
            {
                MessageBox.Show("请输入读取起始地址");
                return;
            }
            if (txtReadLength.Text.Length == 0)
            {
                MessageBox.Show("请输入读取长度");
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtReadUID.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 8)
            {
                MessageBox.Show("至少输入8个字节");
                return;
            }
            byte[] btAryUID = CCommondMethod.StringArrayToByteArray(reslut, 8);

            reader.ReadTagISO18000(m_curSetting.btReadId, btAryUID, Convert.ToByte(htxtReadStartAdd.Text, 16), Convert.ToByte(txtReadLength.Text, 16));
        }

        private void ProcessReadTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "读取标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                string strData = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, msgTran.AryData.Length - 1);

                m_curOperateTagISO18000Buffer.btAntId = Convert.ToByte(strAntID);
                m_curOperateTagISO18000Buffer.strReadData = strData;

                RefreshISO18000(msgTran.Cmd);

                WriteLog(lrtxtLog, strCmd, 0);
            }
        }

        private void btnWriteTagISO18000_Click(object sender, EventArgs e)
        {
            try
            {
                m_nLoopedTimes = 0;
                if (txtLoop.Text.Length == 0)
                    m_nLoopTimes = 0;
                else
                    m_nLoopTimes = Convert.ToInt32(txtLoop.Text);

                WriteTagISO18000();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void WriteTagISO18000()
        {
            if (htxtReadUID.Text.Length == 0)
            {
                MessageBox.Show("请输入UID");
                return;
            }
            if (htxtWriteStartAdd.Text.Length == 0)
            {
                MessageBox.Show("请输入写入地址");
                return;
            }
            if (htxtWriteData18000.Text.Length == 0)
            {
                MessageBox.Show("请输入写入数据");
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtReadUID.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 8)
            {
                MessageBox.Show("至少输入8个字节");
                return;
            }
            byte[] btAryUID = CCommondMethod.StringArrayToByteArray(reslut, 8);

            byte btStartAdd = Convert.ToByte(htxtWriteStartAdd.Text, 16);

            //string[] reslut = CCommondMethod.StringToStringArray(htxtWriteData18000.Text.ToUpper(), 2);
            string strTemp = cleanString(htxtWriteData18000.Text);
            reslut = CCommondMethod.StringToStringArray(strTemp.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }

            byte[] btAryData = CCommondMethod.StringArrayToByteArray(reslut, reslut.Length);

            //byte btLength = Convert.ToByte(txtWriteLength.Text, 16);
            byte btLength = Convert.ToByte(reslut.Length);
            txtWriteLength.Text = String.Format("{0:X}", btLength);
            m_nBytes = reslut.Length;

            reader.WriteTagISO18000(m_curSetting.btReadId, btAryUID, btStartAdd, btLength, btAryData);
        }

        private string cleanString(string newStr)
        {
            string tempStr = newStr.Replace('\r', ' ');
            return tempStr.Replace('\n', ' ');
        }


        private void ProcessWriteTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "写入标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strCnt = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

                m_curOperateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                m_curOperateTagISO18000Buffer.btWriteLength = msgTran.AryData[1];

                //RefreshISO18000(msgTran.Cmd);
                string strLength = msgTran.AryData[1].ToString();
                string strLog = strCmd + ": " + "成功写入" + strLength + "字节";
                WriteLog(lrtxtLog, strLog, 0);
                RunLoopISO18000(Convert.ToInt32(msgTran.AryData[1]));
            }
        }

        private void btnLockTagISO18000_Click(object sender, EventArgs e)
        {
            if (htxtReadUID.Text.Length == 0)
            {
                MessageBox.Show("请输入UID");
                return;
            }
            if (htxtLockAdd.Text.Length == 0)
            {
                MessageBox.Show("请输入写保护地址");
                return;
            }

            //确认写保护提示
            if (MessageBox.Show("是否确定对该地址永久写保护?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
            {
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtReadUID.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 8)
            {
                MessageBox.Show("至少输入8个字节");
                return;
            }
            byte[] btAryUID = CCommondMethod.StringArrayToByteArray(reslut, 8);

            byte btStartAdd = Convert.ToByte(htxtLockAdd.Text, 16);

            reader.LockTagISO18000(m_curSetting.btReadId, btAryUID, btStartAdd);
        }

        private void ProcessLockTagISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "永久写保护";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

                m_curOperateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                m_curOperateTagISO18000Buffer.btStatus = msgTran.AryData[1];

                //RefreshISO18000(msgTran.Cmd);
                string strLog = string.Empty;
                switch (msgTran.AryData[1])
                {
                    case 0x00:
                        strLog = strCmd + ": " + "成功锁定";
                        break;
                    case 0xFE:
                        strLog = strCmd + ": " + "已是锁定状态";
                        break;
                    case 0xFF:
                        strLog = strCmd + ": " + "无法锁定";
                        break;
                    default:
                        break;
                }

                WriteLog(lrtxtLog, strLog, 0);

            }
        }

        private void btnQueryTagISO18000_Click(object sender, EventArgs e)
        {
            if (htxtReadUID.Text.Length == 0)
            {
                MessageBox.Show("请输入UID");
                return;
            }
            if (htxtQueryAdd.Text.Length == 0)
            {
                MessageBox.Show("请输入查询地址");
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtReadUID.Text.ToUpper(), 2);

            if (reslut == null)
            {
                MessageBox.Show("输入字符无效");
                return;
            }
            else if (reslut.GetLength(0) < 8)
            {
                MessageBox.Show("至少输入8个字节");
                return;
            }
            byte[] btAryUID = CCommondMethod.StringArrayToByteArray(reslut, 8);

            byte btStartAdd = Convert.ToByte(htxtQueryAdd.Text, 16);

            reader.QueryTagISO18000(m_curSetting.btReadId, btAryUID, btStartAdd);
        }

        private void ProcessQueryISO18000(Reader.MessageTran msgTran)
        {
            string strCmd = "查询标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
            }
            else
            {
                //string strAntID = CCommondMethod.ByteArrayToString(msgTran.AryData, 0, 1);
                //string strStatus = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 1);

                m_curOperateTagISO18000Buffer.btAntId = msgTran.AryData[0];
                m_curOperateTagISO18000Buffer.btStatus = msgTran.AryData[1];

                RefreshISO18000(msgTran.Cmd);

                WriteLog(lrtxtLog, strCmd, 0);
            }
        }

        private void htxtSendData_Leave(object sender, EventArgs e)
        {
            if (htxtSendData.TextLength == 0)
            {
                return;
            }

            string[] reslut = CCommondMethod.StringToStringArray(htxtSendData.Text.ToUpper(), 2);
            byte[] btArySendData = CCommondMethod.StringArrayToByteArray(reslut, reslut.Length);

            byte btCheckData = reader.CheckValue(btArySendData);
            htxtCheckData.Text = string.Format(" {0:X2}", btCheckData);
        }

        private void btnSendData_Click(object sender, EventArgs e)
        {
            if (htxtSendData.TextLength == 0)
            {
                return;
            }

            string strData = htxtSendData.Text + htxtCheckData.Text;

            string[] reslut = CCommondMethod.StringToStringArray(strData.ToUpper(), 2);
            byte[] btArySendData = CCommondMethod.StringArrayToByteArray(reslut, reslut.Length);

            reader.SendMessage(btArySendData);
        }

        private void btnClearData_Click(object sender, EventArgs e)
        {
            htxtSendData.Text = "";
            htxtCheckData.Text = "";
        }

        private void lrtxtDataTran_DoubleClick(object sender, EventArgs e)
        {
            lrtxtDataTran.Text = "";
        }

        private void lrtxtLog_DoubleClick(object sender, EventArgs e)
        {
            lrtxtLog.Text = "";
        }

        private void tabCtrMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region Johar
            if (tabCtrMain.SelectedTab.Name.Equals("johar_tabPage"))
            {
                johar_cb.Enabled = false;
                johar_session_s0_rb.Checked = true;
                johar_target_A_rb.Checked = true;
                johar_readmode_mode3.Checked = true;
                johar_cmd_interval_cb.SelectedIndex = 2;

                if (johardb == null)
                {
                    johardb = new JoharTagDB();
                    johar_tag_dgv.DataSource = johardb.TagList;
                }
            }
            else
            {
                if (johar_use_btn.Text.Equals("停用"))
                {
                    johar_use_btn.Text = "启用";
                    enableReadJohar(false);
                    johar_cb.Checked = false;
                    clearSelect();
                }
            }
            #endregion Johar

            #region Net Configure
            if (tabCtrMain.SelectedTab.Name.Equals("net_configure_tabPage"))
            {
                dev_dgv.AutoGenerateColumns = true;

                if (net_db == null)
                {
                    net_db = new NetCfgDB();
                }
                if (net_card_dict == null)
                {
                    net_card_dict = new Dictionary<string, NetCardSearch>();
                }
                NetRefreshNetCard();
                LoadNetConfigViews();
                UpdateUdpServerStatus("not start");
            }
            else
            {
                StopNetUdpServer();
            }
            #endregion Net Configure

            if (m_bLockTab)
            {
                tabCtrMain.SelectTab(1);
            }
            int nIndex = tabCtrMain.SelectedIndex;

            if (nIndex == 2)
            {
                lrtxtDataTran.Select(lrtxtDataTran.TextLength, 0);
                lrtxtDataTran.ScrollToCaret();
            }
        }

        private void txtTcpPort_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)ConsoleKey.Backspace)
            {
                e.Handled = false;
            }
        }

        private void txtOutputPower_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)ConsoleKey.Backspace)
            {
                e.Handled = false;
            }
        }

        private void txtChannel_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)ConsoleKey.Backspace)
            {
                e.Handled = false;
            }
        }

        private void txtWordAdd_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)ConsoleKey.Backspace)
            {
                e.Handled = false;
            }
        }

        private void txtWordCnt_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.Handled = true;
            if ((e.KeyChar >= '0' && e.KeyChar <= '9') || e.KeyChar == (char)ConsoleKey.Backspace)
            {
                e.Handled = false;
            }
        }

        private void cmbSetAccessEpcMatch_DropDown(object sender, EventArgs e)
        {
            cmbSetAccessEpcMatch.Items.Clear();
        }

        private void ShowListView(ListView ltvListView, DataRow[] drSelect)
        {
            //ltvListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            int nItemCount = ltvListView.Items.Count;
            int nIndex = 1;

            foreach (DataRow row in drSelect)
            {
                ListViewItem item = new ListViewItem();
                item.Text = (nItemCount + nIndex).ToString();
                item.SubItems.Add(row[0].ToString());

                string strTemp = (Convert.ToInt32(row[1].ToString()) - 129).ToString() + "dBm";
                item.SubItems.Add(strTemp);
                byte byTemp = Convert.ToByte(row[1]);
                if (byTemp > 0x50)
                {
                    item.BackColor = Color.PowderBlue;
                }
                else if (byTemp < 0x30)
                {
                    item.BackColor = Color.LemonChiffon;
                }

                item.SubItems.Add(row[2].ToString());
                item.SubItems.Add(row[3].ToString());

                ltvListView.Items.Add(item);
                ltvListView.Items[nIndex - 1].EnsureVisible();
                nIndex++;
            }
        }

        private void ltvTagISO18000_DoubleClick(object sender, EventArgs e)
        {
            //if (ltvTagISO18000.SelectedItems.Count == 1)
            //{
            //    ListViewItem item = ltvTagISO18000.SelectedItems[0];
            //    string strUID = item.SubItems[1].Text;
            //    htxtReadUID.Text = strUID;
            //}
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            htxtReadUID.Text = "";
            htxtReadStartAdd.Text = "";
            txtReadLength.Text = "";
            htxtReadData18000.Text = "";
            htxtWriteStartAdd.Text = "";
            txtWriteLength.Text = "";
            htxtWriteData18000.Text = "";
            htxtLockAdd.Text = "";
            htxtQueryAdd.Text = "";
            txtStatus.Text = "";
            txtLoop.Text = "1";
            ltvTagISO18000.Items.Clear();
        }

        private void ltvTagISO18000_Click(object sender, EventArgs e)
        {
            if (ltvTagISO18000.SelectedItems.Count == 1)
            {
                ListViewItem item = ltvTagISO18000.SelectedItems[0];
                string strUID = item.SubItems[1].Text;
                htxtReadUID.Text = strUID;
            }
        }

        private void ckDisplayLog_CheckedChanged(object sender, EventArgs e)
        {
            if (ckDisplayLog.Checked)
            {
                m_bDisplayLog = true;
            }
            else
            {
                m_bDisplayLog = false;
            }
        }

        private void startRealtimeInv()
        {
            int antId = combo_realtime_inv_ants.SelectedIndex;
            Console.WriteLine("startRealtimeInv 天线{0}, 天线组{1}", antId, m_curSetting.btAntGroup);
            if(antId >= 8)
            {
                m_curSetting.btWorkAntenna = (byte)(antId - 8);
                cmdSwitchAntG2();
            }
            else
            {
                useAntG1 = true;
                m_curSetting.btAntGroup = 0x00;
                m_curSetting.btWorkAntenna = (byte)antId;
                reader.SetWorkAntenna(m_curSetting.btReadId, m_curSetting.btWorkAntenna);
            }
        }

        private void FastInventory_Click(object sender, EventArgs e)
        {
            if (btnInventory.Text.Equals("开始盘存"))
            {
                if (Inventorying)
                {
                    MessageBox.Show("正在盘点...");
                    return;
                }

                // 检查是否选择天线
                if (!checkFastInvAnt())
                {
                    MessageBox.Show("请至少选择一个天线至少轮询一次，重复次数至少一次。");
                    btnInventory.BackColor = Color.WhiteSmoke;
                    btnInventory.ForeColor = Color.DarkBlue;
                    btnInventory.Text = "开始盘存";
                    return;
                }

                try
                {
                    if (Convert.ToInt32(mInventoryExeCount.Text) == 0)
                    {
                        MessageBox.Show("无效参数运行次数不能为0!");
                        return;
                    }

                    btnInventory.BackColor = Color.DarkBlue;
                    btnInventory.ForeColor = Color.White;
                    btnInventory.Text = "停止盘存";

                    isFastInv = true;
                    doingFastInv = true;
                    Inventorying = true;

                    invTargetB = false;

                    m_FastExeCount = Convert.ToInt32(mInventoryExeCount.Text);

                    m_curSetting.btAntGroup = 0x00;

                    if (antType16.Checked)
                    {
                        if (checkFastInvAntG1Count())
                        {
                            Console.WriteLine("FastInv Start G1");
                            RefreshInventoryInfo();
                            cmdFastInventorySend(useAntG1);
                        }
                        else if (checkFastInvAntG2Count())
                        {
                            Console.WriteLine("FastInv Start G2");
                            cmdSwitchAntG2();
                        }
                        else
                        {
                            useAntG1 = true;
                            FastExecTimes = 0;
                            isFastInv = false;
                            stopFastInv();
                            MessageBox.Show("No Antenna select!");
                        }
                    }
                    else
                    {
                        useAntG1 = true;
                        RefreshInventoryInfo();
                        cmdFastInventorySend(useAntG1);
                    }
                    startInventoryTime = DateTime.Now;
                    dispatcherTimer.Start();
                    readratePerSecond.Start();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (btnInventory.Text.Equals("停止盘存"))
            {
                //默认循环发送命令
                if (Inventorying)
                {
                    SetInvStopingStatus();
                    isFastInv = false;
                }
            }
        }

        private void SetInvStopingStatus()
        {
            btnInventory.BackColor = Color.LightBlue;
            btnInventory.ForeColor = Color.Red;
            btnInventory.Text = "正在停止...";
        }

        private void setInvStoppedStatus()
        {
            dispatcherTimer.Stop();
            readratePerSecond.Stop();
            elapsedTime = CalculateElapsedTime();

            btnInventory.BackColor = Color.WhiteSmoke;
            btnInventory.ForeColor = Color.DarkBlue;
            btnInventory.Text = "开始盘存";
        }

        private bool checkFastInvAntG1Count()
        {
            for (int i = 0; i < 8; i++)
            {
                if (fast_inv_ants[i].Enabled)
                {
                    if (fast_inv_ants[i].Checked)
                    {
                        useAntG1 = true;
                        return true;
                    }
                }
            }
            useAntG1 = false;
            return false;
        }

        private bool checkFastInvAntG2Count()
        {
            for (int i = 8; i < 16; i++)
            {
                if (fast_inv_ants[i].Enabled)
                {
                    if (fast_inv_ants[i].Checked)
                    {
                        useAntG1 = false;
                        return true;
                    }
                }
            }
            useAntG1 = true;
            return false;
        }

        private bool checkFastInvAnt()
        {
            int antCount = 0;
            for(int i = 0; i < 16; i++)
            {
                if(fast_inv_ants[i].Enabled)
                {
                    if (fast_inv_ants[i].Checked)
                    {
                        //Console.WriteLine("天线{0} stay={1}", fast_inv_ants[i].Text, fast_inv_stays[i].Text);
                    }
                    else
                    {
                        //Console.WriteLine("天线 {0} Not Use but Visible", fast_inv_ants[i].Text);
                    }
                    antCount++;
                }
                else
                {
                    //Console.WriteLine("天线{0} Not Use", fast_inv_ants[i].Text);
                }
            }
            if (antCount > 0)
                return true;
            return false;
        }

        private void buttonFastFresh_Click(object sender, EventArgs e)
        {
            BeginInvoke(new ThreadStart(delegate() {
                startInventoryTime = DateTime.Now;
                elapsedTime = 0.0;
                lock (tagdb)
                {
                    tagdb.Clear();
                    tagdb.Repaint();

                    led_cmd_total_tagreads.Text = tagdb.TotalTagCounts.ToString();
                    led_cmd_readrate.Text = tagdb.CmdReadRate.ToString();
                    led_cmd_execute_duration.Text = tagdb.CommandDuration.ToString();
                    led_totalread_count.Text = tagdb.TotalReadCounts.ToString();
                    ledFast_total_execute_time.Text = tagdb.TotalCommandTime.ToString();
                    txtFastMinRssi.Text = tagdb.MinRSSI.ToString();
                    txtFastMaxRssi.Text = tagdb.MaxRSSI.ToString();
                    txtTotalTagCount.Text = tagdb.TotalTagCounts.ToString();

                    label_readrate.Text = tagdb.CmdReadRate.ToString();
                    label_totalread_count.Text = tagdb.TotalReadCounts.ToString();
                    label_totaltag_count.Text = tagdb.TotalTagCounts.ToString();
                    label_totaltime.Text = tagdb.TotalCommandTime.ToString();
                }
            }));
        }

        private void pageFast4AntMode_Enter(object sender, EventArgs e)
        {
            //buttonFastFresh_Click(sender, e);
        }

        private void button7_Click(object sender, EventArgs e)
        {
            txtFirmwareVersion.Text = "";
            htxtReadId.Text = "";
            htbSetIdentifier.Text = "";
            txtReaderTemperature.Text = "";
            //txtOutputPower.Text = "";
            tb_outputpower_1.Text = "";
            tb_outputpower_2.Text = "";
            tb_outputpower_3.Text = "";
            tb_outputpower_4.Text = "";

            htbGetIdentifier.Text = "";
        }

        private void btGetMonzaStatus_Click(object sender, EventArgs e)
        {
            reader.GetMonzaStatus(m_curSetting.btReadId);
        }

        private void btSetMonzaStatus_Click(object sender, EventArgs e)
        {
            byte btMonzaStatus = 0xFF;

            if (rdbMonzaOn.Checked)
            {
                btMonzaStatus = 0x8D;
            }
            else if (rdbMonzaOff.Checked)
            {
                btMonzaStatus = 0x00;
            }
            else
            {
                return;
            }

            reader.SetMonzaStatus(m_curSetting.btReadId, btMonzaStatus);
            m_curSetting.btMonzaStatus = btMonzaStatus;
        }

        private void btGetIdentifier_Click(object sender, EventArgs e)
        {
            reader.GetReaderIdentifier(m_curSetting.btReadId);
        }

        private void btSetIdentifier_Click(object sender, EventArgs e)
        {
            try
            {
                string strTemp = htbSetIdentifier.Text.Trim();


                string[] result = CCommondMethod.StringToStringArray(strTemp.ToUpper(), 2);

                if (result == null)
                {
                    MessageBox.Show("输入字符无效");
                    return;
                }
                else if (result.GetLength(0) != 12)
                {
                    MessageBox.Show("请输入12个字节");
                    return;
                }
                byte[] readerIdentifier = CCommondMethod.StringArrayToByteArray(result, 12);


                reader.SetReaderIdentifier(m_curSetting.btReadId, readerIdentifier);
                //m_curSetting.btReadId = Convert.ToByte(strTemp, 16);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btReaderSetupRefresh_Click(object sender, EventArgs e)
        {
            htxtReadId.Text = "";
            htbGetIdentifier.Text = "";
            htbSetIdentifier.Text = "";
            txtFirmwareVersion.Text = "";
            txtReaderTemperature.Text = "";
            rdbGpio1High.Checked = false;
            rdbGpio1Low.Checked = false;
            rdbGpio2High.Checked = false;
            rdbGpio2Low.Checked = false;
            rdbGpio3High.Checked = false;
            rdbGpio3Low.Checked = false;
            rdbGpio4High.Checked = false;
            rdbGpio4Low.Checked = false;

            rdbBeeperModeSlient.Checked = false;
            rdbBeeperModeInventory.Checked = false;
            rdbBeeperModeTag.Checked = false;

            cmbSetBaudrate.SelectedIndex = -1;
        }

        private void btRfSetup_Click(object sender, EventArgs e)
        {
            //txtOutputPower.Text = "";
            tb_outputpower_1.Text = "";
            tb_outputpower_2.Text = "";
            tb_outputpower_3.Text = "";
            tb_outputpower_4.Text = "";

            cmbFrequencyStart.SelectedIndex = -1;
            cmbFrequencyEnd.SelectedIndex = -1;
            tbAntDectector.Text = "";

            //rdbDrmModeOpen.Checked = false;
            //rdbDrmModeClose.Checked = false;

            rdbMonzaOn.Checked = false;
            rdbMonzaOff.Checked = false;
            rdbRegionFcc.Checked = false;
            rdbRegionEtsi.Checked = false;
            rdbRegionChn.Checked = false;

            textReturnLoss.Text = "";
            cmbWorkAnt.SelectedIndex = -1;
            textStartFreq.Text = "";
            TextFreqInterval.Text = "";
            textFreqQuantity.Text = "";

            rdbProfile0.Checked = false;
            rdbProfile1.Checked = false;
            rdbProfile2.Checked = false;
            rdbProfile3.Checked = false;
        }
        private void cbRealSession_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (cbRealSession.Checked == true)
            {
                label97.Enabled = true;
                label98.Enabled = true;
                cmbSession.Enabled = true;
                cmbTarget.Enabled = true;
            }
            else
            {
                label97.Enabled = false;
                label98.Enabled = false;
                cmbSession.Enabled = false;
                cmbTarget.Enabled = false;

                m_session_sl_cb.Checked = false;
            } */
        }

        private void btReturnLoss_Click(object sender, EventArgs e)
        {
            if (cmbReturnLossFreq.SelectedIndex != -1)
            {
                reader.MeasureReturnLoss(m_curSetting.btReadId, Convert.ToByte(cmbReturnLossFreq.SelectedIndex));
            }
        }

        private void cbUserDefineFreq_CheckedChanged(object sender, EventArgs e)
        {
            if (cbUserDefineFreq.Checked == true)
            {
                groupBox21.Enabled = false;
                groupBox23.Enabled = true;

            }
            else
            {
                groupBox21.Enabled = true;
                groupBox23.Enabled = false;
            }
        }

        private void btSetProfile_Click(object sender, EventArgs e)
        {
            byte btSelectedProfile = 0xFF;

            if (rdbProfile0.Checked)
            {
                btSelectedProfile = 0xD0;
            }
            else if (rdbProfile1.Checked)
            {
                btSelectedProfile = 0xD1;
            }
            else if (rdbProfile2.Checked)
            {
                btSelectedProfile = 0xD2;
            }
            else if (rdbProfile3.Checked)
            {
                btSelectedProfile = 0xD3;
            }
            else
            {
                return;
            }

            reader.SetRadioProfile(m_curSetting.btReadId, btSelectedProfile);
        }

        private void btGetProfile_Click(object sender, EventArgs e)
        {
            reader.GetRadioProfile(m_curSetting.btReadId);
        }

        private void tabCtrMain_Click(object sender, EventArgs e)
        {
            if ((m_curSetting.btRegion < 1) || (m_curSetting.btRegion > 4)) //如果是自定义的频谱则需要先提取自定义频率信息
            {
                reader.GetFrequencyRegion(m_curSetting.btReadId);
                Thread.Sleep(5);

            }
        }

        private void ProcessTagMask(Reader.MessageTran msgTran)
        {
            string strCmd = "操作过滤";
            string strErrorCode = string.Empty;
            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == (byte)0x10)
                {
                    strCmd += ": 命令执行成功!";
                    WriteLog(lrtxtLog, strCmd, 0);
                    return;
                }
                else if (msgTran.AryData[1] == (byte)0x41)
                {
                    strErrorCode = "无效的参数错误";
                }
                else
                {
                    strErrorCode = "未知错误";
                }
            }
            else
            {
                if (msgTran.AryData.Length > 7)
                {
                    m_curSetting.btsGetTagMask = msgTran.AryData;
                    RefreshReadSetting(msgTran.Cmd);
                    WriteLog(lrtxtLog, "查询过滤设置成功", 0);
                    return;
                }
            }
            string strLog = strCmd + "失败，失败原因: " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (combo_mast_id.SelectedIndex == -1 || combo_session.SelectedIndex == -1 || combo_action.SelectedIndex == -1 || combo_menbank.SelectedIndex == -1)
                {
                    MessageBox.Show("Target Action Membank must be selected");
                    return;
                }
                byte btMaskNo = (byte)(combo_mast_id.SelectedIndex + 1);
                byte btTarget = (byte)combo_session.SelectedIndex;
                byte btAction = (byte)combo_action.SelectedIndex;
                byte btMembank = (byte)combo_menbank.SelectedIndex;

                string strMaskValue = hexTextBox_mask.Text.Trim();
                string[] maskValue = CCommondMethod.StringToStringArray(strMaskValue.ToUpper(), 2);


                byte btStartAddress = Convert.ToByte(startAddr.Text);
                int intStartAdd = Convert.ToInt32(startAddr.Text);
                byte btMaskLen = Convert.ToByte(bitLen.Text);
                int intMaskLen = Convert.ToInt32(bitLen.Text);

                byte[] btsMaskValue = CCommondMethod.StringArrayToByteArray(maskValue, maskValue.Length);

                if (intStartAdd <= 0 || intStartAdd > 255 || intMaskLen <= 0 || intMaskLen > 255)
                {
                    MessageBox.Show("Mask Length and start address must be 1-255");
                    return;
                }

                if (intMaskLen < (btsMaskValue.Length - 1) * 8 + 1 || intMaskLen > btsMaskValue.Length * 8)
                {
                    MessageBox.Show("Mask Length is invaild!");
                    return;
                }

                reader.setTagMask((byte)0xFF, btMaskNo, btTarget, btAction, btMembank, btStartAddress, btMaskLen, btsMaskValue);
                //m_curSetting.btReadId = Convert.ToByte(strTemp, 16);

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox16.SelectedIndex == -1)
            {
                MessageBox.Show("MaskNO must be selected");
                return;
            }
            byte btMaskNo = (byte)comboBox16.SelectedIndex;
            reader.clearTagMask((byte)0xFF, btMaskNo);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView2.Items.Clear();
            reader.getTagMask((byte)0xFF);
        }

        private void saveFastData()
        {
            string strDestinationFile = string.Empty;
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "CSV Files (*.csv)|*.csv";
                strDestinationFile = "InventoryResult"
                        + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + @".csv";
                saveFileDialog1.FileName = strDestinationFile;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    strDestinationFile = saveFileDialog1.FileName;
                    TextWriter tw = new StreamWriter(strDestinationFile, true, Encoding.Default);
                    StringBuilder sb = new StringBuilder();
                    //writing the header
                    int columnCount = dgv_fast_inv_tags.Columns.Count;

                    for (int count = 0; count < columnCount; count++)
                    {
                        string colHeader = dgv_fast_inv_tags.Columns[count].HeaderText;
                        sb.Append(colHeader + ", ");
                    }
                    tw.WriteLine(sb.ToString());

                    //writing the data
                    TagRecord rda = null;
                    for (int rowCount = 0; rowCount <= tagdb.TagList.Count - 1; rowCount++)
                    {
                        rda = tagdb.TagList[rowCount];
                        textWrite(tw, rda, rowCount + 1);
                    }
                    tw.Close();
                    MessageBox.Show("数据导出成功！");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// For readability sake in the text file.
        /// </summary>
        /// <param name="tw"></param>
        /// <param name="rda"></param>
        private void textWrite(TextWriter tw, TagRecord rda, int rowNumber)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(rda.SerialNumber + ", ");

            sb.Append(rda.ReadCount + ", ");

            sb.Append(rda.PC + ", ");

            sb.Append(rda.EPC + ", ");

            sb.Append(rda.Antenna + ", ");

            sb.Append(rda.Rssi + ", ");

            sb.Append(rda.Freq + ", ");

            sb.Append(rda.Phase);

            sb.Append(rda.Data + ", ");


            //sb.Append(rda.Protocol + ", ");

            //sb.Append(rda.TimeStamp.ToString("MM-dd-yyyy HH:mm:ss:fff") + ", ");

            tw.Write(sb.ToString());
            tw.WriteLine();
        }


        //save tag as excel

        public DataTable ListViewToDataTable(ListView listView)
        {
            DataTable table = new DataTable();

            foreach (ColumnHeader header in listView.Columns)
            {
                table.Columns.Add(header.Text, typeof(string));
            }

            foreach (ListViewItem item in listView.Items)
            {
                DataRow row = table.NewRow();
                //处理行
                for (int i = 0; i < item.SubItems.Count; i++)
                {
                    //MessageBox.Show(item.SubItems[i].Text);
                    row[i] = item.SubItems[i].Text;
                }

                table.Rows.Add(row);
            }
            return table;
        }


        //////////////////////////////////////////////////////////////////////////
        public void SaveToFile(MemoryStream ms, string fileName)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Create, FileAccess.Write))
            {
                byte[] data = ms.ToArray();

                fs.Write(data, 0, data.Length);
                fs.Flush();

                data = null;
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            saveFastData();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_1.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_1.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_1.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_1.Text = "";
                }
            }

            int channels = 1;

            if (antType4.Checked)
            {
                channels = 4;
            }

            if (antType8.Checked)
            {
                channels = 8;
            }

            if (antType16.Checked)
            {
                channels = 16;
            }

            if(channels >= 4)
            {
                tb_outputpower_2.Text = tb_outputpower_1.Text;
                tb_outputpower_3.Text = tb_outputpower_1.Text;
                tb_outputpower_4.Text = tb_outputpower_1.Text;

                if(channels >= 8)
                {
                    tb_outputpower_5.Text = tb_outputpower_1.Text;
                    tb_outputpower_6.Text = tb_outputpower_1.Text;
                    tb_outputpower_7.Text = tb_outputpower_1.Text;
                    tb_outputpower_8.Text = tb_outputpower_1.Text;

                    if (channels >= 16)
                    {
                        tb_outputpower_9.Text = tb_outputpower_1.Text;
                        tb_outputpower_10.Text = tb_outputpower_1.Text;
                        tb_outputpower_11.Text = tb_outputpower_1.Text;
                        tb_outputpower_12.Text = tb_outputpower_1.Text;

                        tb_outputpower_13.Text = tb_outputpower_1.Text;
                        tb_outputpower_14.Text = tb_outputpower_1.Text;
                        tb_outputpower_15.Text = tb_outputpower_1.Text;
                        tb_outputpower_16.Text = tb_outputpower_1.Text;
                    }
                }
            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_2.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_2.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_2.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_2.Text = "";
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_3.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_3.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_3.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_3.Text = "";
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_4.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_4.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_4.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_4.Text = "";
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_5.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_5.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_5.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_5.Text = "";
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_6.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_6.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_6.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_6.Text = "";
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_7.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_7.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_7.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_7.Text = "";
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (tb_outputpower_8.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(tb_outputpower_8.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        tb_outputpower_8.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    tb_outputpower_8.Text = "";
                }
            }
        }

        private void antType_CheckedChanged(object sender, EventArgs e)
        {
            Console.WriteLine("antType_CheckedChanged");
            RadioButton btn = (RadioButton)sender;
            if (!btn.Checked)
                return;
            switch (btn.Name)
            {
                case "antType1":
                    channels = 1;
                    antType1_CheckedChanged(sender, e);
                    break;
                case "antType4":
                    channels = 4;
                    antType4_CheckedChanged(sender, e);
                    break;
                case "antType8":
                    channels = 8;
                    antType8_CheckedChanged(sender, e);
                    break;
                case "antType16":
                    channels = 16;
                    antType16_CheckedChanged(sender, e);
                    break;
            }
            InventoryTypeChanged(null, null);
        }

        private void antType1_CheckedChanged(object sender, EventArgs e)
        {
            if (antType1.Checked)
            {
                // output power 
                tb_outputpower_2.Enabled = false;
                tb_outputpower_3.Enabled = false;
                tb_outputpower_4.Enabled = false;
                tb_outputpower_5.Enabled = false;
                tb_outputpower_6.Enabled = false;
                tb_outputpower_7.Enabled = false;
                tb_outputpower_8.Enabled = false;
                tb_outputpower_9.Enabled = false;
                tb_outputpower_10.Enabled = false;
                tb_outputpower_11.Enabled = false;
                tb_outputpower_12.Enabled = false;
                tb_outputpower_13.Enabled = false;
                tb_outputpower_14.Enabled = false;
                tb_outputpower_15.Enabled = false;
                tb_outputpower_16.Enabled = false;

                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1"});
                this.cmbWorkAnt.SelectedIndex = 0;
            }
        }

        private void antType4_CheckedChanged(object sender, EventArgs e)
        {
            if (antType4.Checked)
            {
                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1",
                "天线 2",
                "天线 3",
                "天线 4"});
                this.cmbWorkAnt.SelectedIndex = 0;

                // output power 
                tb_outputpower_2.Enabled = true;
                tb_outputpower_3.Enabled = true;
                tb_outputpower_4.Enabled = true;
                tb_outputpower_5.Enabled = false;
                tb_outputpower_6.Enabled = false;
                tb_outputpower_7.Enabled = false;
                tb_outputpower_8.Enabled = false;
                tb_outputpower_9.Enabled = false;
                tb_outputpower_10.Enabled = false;
                tb_outputpower_11.Enabled = false;
                tb_outputpower_12.Enabled = false;
                tb_outputpower_13.Enabled = false;
                tb_outputpower_14.Enabled = false;
                tb_outputpower_15.Enabled = false;
                tb_outputpower_16.Enabled = false;
            }
        }

        private void antType8_CheckedChanged(object sender, EventArgs e)
        {
            if (antType8.Checked)
            {
                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1",
                "天线 2",
                "天线 3",
                "天线 4",
                "天线 5",
                "天线 6",
                "天线 7",
                "天线 8"});
                this.cmbWorkAnt.SelectedIndex = 0;

                // output power 
                tb_outputpower_2.Enabled = true;
                tb_outputpower_3.Enabled = true;
                tb_outputpower_4.Enabled = true;
                tb_outputpower_5.Enabled = true;
                tb_outputpower_6.Enabled = true;
                tb_outputpower_7.Enabled = true;
                tb_outputpower_8.Enabled = true;
                tb_outputpower_9.Enabled = false;
                tb_outputpower_10.Enabled = false;
                tb_outputpower_11.Enabled = false;
                tb_outputpower_12.Enabled = false;
                tb_outputpower_13.Enabled = false;
                tb_outputpower_14.Enabled = false;
                tb_outputpower_15.Enabled = false;
                tb_outputpower_16.Enabled = false;
            }
        }

        private void antType16_CheckedChanged(object sender, EventArgs e)
        {
            if (antType16.Checked)
            {
                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1",
                "天线 2",
                "天线 3",
                "天线 4",
                "天线 5",
                "天线 6",
                "天线 7",
                "天线 8",
                "天线 9",
                "天线 10",
                "天线 11",
                "天线 12",
                "天线 13",
                "天线 14",
                "天线 15",
                "天线 16"});
                this.cmbWorkAnt.SelectedIndex = 0;

                // output power 
                tb_outputpower_2.Enabled = true;
                tb_outputpower_3.Enabled = true;
                tb_outputpower_4.Enabled = true;
                tb_outputpower_5.Enabled = true;
                tb_outputpower_6.Enabled = true;
                tb_outputpower_7.Enabled = true;
                tb_outputpower_8.Enabled = true;
                tb_outputpower_9.Enabled = true;
                tb_outputpower_10.Enabled = true;
                tb_outputpower_11.Enabled = true;
                tb_outputpower_12.Enabled = true;
                tb_outputpower_13.Enabled = true;
                tb_outputpower_14.Enabled = true;
                tb_outputpower_15.Enabled = true;
                tb_outputpower_16.Enabled = true;
            }
        }

        private void cb_customized_session_target_CheckedChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("cb_customized_session_target_CheckedChanged");
            if (cb_customized_session_target.Checked)
            {
                // 允许测试反转AB
                cb_fast_inv_reverse_target.Visible = true;
                tb_fast_inv_staytargetB_times.Visible = true;

                if(radio_btn_fast_inv.Checked) // 快速盘点
                {
                    tb_fast_inv_reserved_5.Visible = false;
                    grb_sessions.Visible = true; // Session
                    grb_targets.Visible = true; // Target
                    grb_Reserve.Visible = true;

                    cb_use_Phase.Visible = true;
                    cb_use_selectFlags_tempPows.Visible = true;
                    cb_use_optimize.Visible = true;
                    //cb_use_Phase_CheckedChanged(null, null);
                    //cb_use_selectFlags_tempPows_CheckedChanged(null, null);
                    //cb_use_optimize_CheckedChanged(null, null);
                }
                else
                {
                    grb_sessions.Visible = true;
                    grb_targets.Visible = true;

                    cb_use_Phase.Visible = true; // Phase
                    cb_use_selectFlags_tempPows.Visible = true;
                    cb_use_powerSave.Visible = true;
                }
            }
            else
            {
                // 禁止测试反转AB
                cb_fast_inv_reverse_target.Checked = false;
                cb_fast_inv_reverse_target.Visible = false;
                tb_fast_inv_staytargetB_times.Visible = false;

                if (radio_btn_fast_inv.Checked) // 快速盘点
                {
                    cb_use_Phase.Checked = false; // Phase
                    cb_use_Phase.Visible = false;

                    cb_use_selectFlags_tempPows.Checked = false;
                    cb_use_selectFlags_tempPows.Visible = false;
                    tb_fast_inv_reserved_5.Visible = true;
                    grb_sessions.Visible = false;
                    grb_targets.Visible = false;
                    grb_Reserve.Visible = false;

                    cb_use_optimize.Checked = false;
                    cb_use_optimize.Visible = false;
                    grb_Optimize.Visible = false;
                }
                else
                {
                    cb_use_Phase.Checked = false; // Phase
                    cb_use_Phase.Visible = false; 

                    cb_use_selectFlags_tempPows.Checked = false;
                    cb_use_selectFlags_tempPows.Visible = false;
                    grb_selectFlags.Visible = false;
                    grb_sessions.Visible = false;
                    grb_targets.Visible = false;

                    cb_use_powerSave.Checked = false;
                    cb_use_powerSave.Visible = false;
                    grb_powerSave.Visible = false;
                }
            }
        }

        private void btnSaveData_Click(object sender, EventArgs e)
        {
            string strLog = lrtxtDataTran.Text;
            string path = Application.StartupPath + @"\Log.txt";
            StreamWriter sWriter = File.CreateText(path);
            sWriter.Write(strLog);
            sWriter.Flush();
            sWriter.Close();
            MessageBox.Show("保存成功：" + path);

            lrtxtDataTran.Text = "";
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

        #region Net Configure
        UdpClient netClient;
        IPEndPoint netEndpoint;
        Thread netRecvthread = null;
        static bool netStarted = false;
        bool udpServerRunning = false;
        bool netCmdStarted = false;
        bool searchDev = false;
        bool searching = false;

        string NET_MODULE_FLAG = "NET_MODULE_COMM\0"; // 用来标识通信_old
        string CH9121_CFG_FLAG = "CH9121_CFG_FLAG\0";	// 用来标识通信_new

        NetCfgDB net_db;
        Dictionary<string, NetCardSearch> net_card_dict;
        string cur_desc = string.Empty;

        private void net_refresh_netcard_btn_Click(object sender, EventArgs e)
        {
            net_pc_ip_label.Text = "";
            net_pc_mac_label.Text = "";
            net_pc_mask_label.Text = "";
            NetRefreshNetCard();
        }

        private void StopNetUdpServer()
        {
            netStarted = false;
        }

        private void StartNetUdpServer()
        {
            if (net_card_dict.Count == 0)
            {
                MessageBox.Show("请先刷新网卡!", "提示", MessageBoxButtons.OK);
                UpdateUdpServerStatus("start failed");
                return;
            }
            UpdateUdpServerStatus("starting");
            string desc = net_card_combox.SelectedItem.ToString();
            cur_desc = desc;
            string pc_ip = net_card_dict[cur_desc].PC_IP;
            int port = 60000;
            Console.WriteLine("cur_desc={0}, cur_pc_ip={1}@{2}", cur_desc, pc_ip, port);

            if (!netStarted && netClient == null)
            {
                netClient = new UdpClient();  //不指定地址和端口

                Socket updSocket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
                updSocket.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, 1);
                //updSocket.Bind(new IPEndPoint(IPAddress.Any, port));
                updSocket.Bind(new IPEndPoint(IPAddress.Parse(pc_ip), port));
                //socket.Bind(new IPEndPoint(IPAddress.Parse("169.254.202.67"), 6000));
                netClient.Client = updSocket;

                netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息 广播地址
                netClient.Client.SendTimeout = 5000; // 设置超时
                netClient.Client.ReceiveTimeout = 5000; // 设置超时时间
                netClient.Client.ReceiveBufferSize = 2 * 1024;

                netStarted = true;
                udpServerRunning = true;

                netRecvthread = new Thread(new ThreadStart(NetRecvThread));
                netRecvthread.IsBackground = true;
                netRecvthread.Start();
                UpdateUdpServerStatus("running");
            }
            else
            {
                Console.WriteLine("already started ####");
                UpdateUdpServerStatus("running");
            }
        }

        private void NetRecvThread()
        {
            while (netStarted)
            {
                if (netCmdStarted)
                {
                    if (netClient.Available > 0)
                    {
                        try
                        {
                            byte[] buf = netClient.Receive(ref netEndpoint);
                            string msg = CCommondMethod.ToHex(buf, "", " ");
                            //Console.WriteLine("#2 Recv:{0}", msg);
                            parseRecvData(buf);
                        }
                        catch (SocketException e)
                        {
                            Console.WriteLine("Error: {0}", e.SocketErrorCode);
                            netCmdStarted = false;
                            enableNetConfigUI(true);
                            MessageBox.Show("操作超时!", "提示", MessageBoxButtons.OK);
                        }
                    }
                }
            }
            if (netRecvthread.IsAlive)
            {
                Console.WriteLine("NetRecvThread isAlive");
            }
            netClient.Close();
            Console.WriteLine("netClient close");
            netClient = null;
            udpServerRunning = false;
            UpdateUdpServerStatus("stoped");
        }

        private void parseRecvData(byte[] buf)
        {
            //Console.WriteLine("###### parseRecvData");
            NET_COMM recv = new NET_COMM(buf);
            if (recv.Cmd == (byte)NET_ACK.NET_MODULE_ACK_SEARCH)
            {
                recv.Pc_Mac = net_card_dict[cur_desc].PC_MAC;
                recv.ModSearch.PcMac = net_card_dict[cur_desc].PC_MAC;
                //Console.WriteLine("###### parseRecvData mac={0} -> {1}", recv.Pc_Mac, recv.ModSearch.PcMac);
                bool added = net_db.Add(recv.Mod_Mac, recv.ModSearch);
                if(added)
                {
                    UpdateNetSearch(recv);
                    BeginInvoke(new ThreadStart(delegate() {
                        net_search_cnt_label.Text = net_db.TotalCounts  + "";
                    }));
                }
                return;
            }
            else if (recv.Cmd == (byte)NET_ACK.NET_MODULE_ACK_GET)
            {
                net_db.Add(recv.Mod_Mac, recv.NetDevCfg);
                UpdateDevCfgUI(recv);
            }
            else if (recv.Cmd == (byte)NET_ACK.NET_MODULE_ACK_SET)
            {
                Thread.Sleep(1500);
                MessageBox.Show("保存设置成功!", "提示", MessageBoxButtons.OK);
                net_db.Add(recv.Mod_Mac, recv.NetDevCfg);
                UpdateDevCfgUI(recv);
            }
            else if (recv.Cmd == (byte)NET_ACK.NET_MODULE_ACK_RESEST)
            {
                MessageBox.Show("恢复出厂设置成功!", "提示", MessageBoxButtons.OK);
                clearDevCfgViews();
            }
            
            netCmdStarted = false;
            enableNetConfigUI(true);
        }

        delegate void UpdateNetSearchDelegate(NET_COMM recv);
        private void UpdateNetSearch(NET_COMM recv)
        {
            UpdateNetSearchDelegate d = new UpdateNetSearchDelegate(UpdateNetSearch);
            if (net_search_btn.InvokeRequired)
            {
                this.Invoke(d, recv);
            }
            else
            {
                int index = dev_dgv.Rows.Add();
                dev_dgv.Rows[index].Cells[ModName.Name].Value = recv.ModSearch.ModName;
                dev_dgv.Rows[index].Cells[ModIp.Name].Value = recv.ModSearch.ModIp;
                dev_dgv.Rows[index].Cells[ModMac.Name].Value = recv.ModSearch.ModMac;
                dev_dgv.Rows[index].Cells[ModVer.Name].Value = recv.ModSearch.ModVer;
                dev_dgv.Rows[index].Cells[PcMac.Name].Value = recv.ModSearch.PcMac;
            }
        }

        delegate void UpdateUdpServerStatusDelegate(string status);
        private void UpdateUdpServerStatus(string status)
        {
            UpdateUdpServerStatusDelegate d = new UpdateUdpServerStatusDelegate(UpdateUdpServerStatus);
            if (net_search_btn.InvokeRequired)
            {
                this.Invoke(d, status);
            }
            else
            {
                string stat = "["+ netStarted + ", " + udpServerRunning + "]";
                if (status.Equals("not start"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务未启动";
                    net_udpserver_status_label.ForeColor = Color.Black;
                }
                else if (status.Equals("starting"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务正在启动...";
                    net_udpserver_status_label.ForeColor = Color.Blue;
                }
                else if (status.Equals("start failed"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务启动失败.";
                    net_udpserver_status_label.ForeColor = Color.Blue;
                }
                else if (status.Equals("running"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务运行中...";
                    net_udpserver_status_label.ForeColor = Color.Green;
                }
                else if (status.Equals("stoping"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务正在停止...";
                    net_udpserver_status_label.ForeColor = Color.Blue;
                }
                else if (status.Equals("stoped"))
                {
                    net_udpserver_status_label.Text = stat + "网口配置服务已停止";
                    net_udpserver_status_label.ForeColor = Color.Red;
                }
                else
                {
                    net_udpserver_status_label.Text = stat + "未知状态: " + status;
                    net_udpserver_status_label.ForeColor = Color.Red;
                }
            }
        }

        delegate void UpdateDevCfgUIDelegate(NET_COMM recv);
        private void UpdateDevCfgUI(NET_COMM recv)
        {
            UpdateDevCfgUIDelegate d = new UpdateDevCfgUIDelegate(UpdateDevCfgUI);
            if (net_search_btn.InvokeRequired)
            {
                this.Invoke(d, recv);
            }
            else
            {
                net_base_info_lb.Items.Clear();
                net_base_info_lb.Items.Add("---- 基础设置 ----");
                net_base_info_lb.Items.Add("设备类型: " + recv.NetDevCfg.HW_CONFIG.DevType);
                net_base_info_lb.Items.Add("设备子类型: " + recv.NetDevCfg.HW_CONFIG.AuxDevType);
                net_base_info_lb.Items.Add("设备序号: " + recv.NetDevCfg.HW_CONFIG.Index);
                net_base_info_lb.Items.Add("设备硬件版本号: " + recv.NetDevCfg.HW_CONFIG.DevHardwareVer);
                net_base_info_lb.Items.Add("设备软件版本号: " + recv.NetDevCfg.HW_CONFIG.DevSoftwareVer);
                net_base_info_lb.Items.Add("模块名: " + recv.NetDevCfg.HW_CONFIG.Modulename);
                net_base_info_lb.Items.Add("模块网络MAC地址: " + recv.NetDevCfg.HW_CONFIG.DevMAC);
                net_base_info_lb.Items.Add("模块IP地址: " + recv.NetDevCfg.HW_CONFIG.DevIP);
                net_base_info_lb.Items.Add("模块网关IP: " + recv.NetDevCfg.HW_CONFIG.DevGWIP);
                net_base_info_lb.Items.Add("模块子网掩码: " + recv.NetDevCfg.HW_CONFIG.DevIPMask);
                net_base_info_lb.Items.Add("DHCP 使能: " + recv.NetDevCfg.HW_CONFIG.DhcpEnable);
                net_base_info_lb.Items.Add("WEB网页地址: " + recv.NetDevCfg.HW_CONFIG.WebPort);
                net_base_info_lb.Items.Add("用户名同模块名: " + recv.NetDevCfg.HW_CONFIG.Username);
                net_base_info_lb.Items.Add("密码使能: " + recv.NetDevCfg.HW_CONFIG.PassWordEn);
                net_base_info_lb.Items.Add("密码: " + recv.NetDevCfg.HW_CONFIG.PassWord);
                net_base_info_lb.Items.Add("固件升级标志: " + recv.NetDevCfg.HW_CONFIG.UpdateFlag);
                net_base_info_lb.Items.Add("串口协商进入配置模式使能: " + recv.NetDevCfg.HW_CONFIG.ComcfgEn);
                net_base_info_lb.Items.Add("保留: " + recv.NetDevCfg.HW_CONFIG.Reserved);
                net_base_info_lb.Items.Add("---- Port 1 ----------------------------------------------------------------------------------");
                net_base_info_lb.Items.Add("端口序号: " + recv.NetDevCfg.PORT_CONFIG[1].Index);
                net_base_info_lb.Items.Add("端口启用标志: " + recv.NetDevCfg.PORT_CONFIG[1].PortEn);
                net_base_info_lb.Items.Add("网络工作模式: " + recv.NetDevCfg.PORT_CONFIG[1].NetMode);
                net_base_info_lb.Items.Add("TCP 客户端模式下随即本地端口号: " + recv.NetDevCfg.PORT_CONFIG[1].RandSportFlag);
                net_base_info_lb.Items.Add("网络通讯端口号: " + recv.NetDevCfg.PORT_CONFIG[1].NetPort);
                net_base_info_lb.Items.Add("目的IP地址: " + recv.NetDevCfg.PORT_CONFIG[1].DesIP);
                net_base_info_lb.Items.Add("工作于TCP Server模式时，允许外部连接的端口号: " + recv.NetDevCfg.PORT_CONFIG[1].DesPort);
                net_base_info_lb.Items.Add("串口波特率: " + recv.NetDevCfg.PORT_CONFIG[1].BaudRate);
                net_base_info_lb.Items.Add("串口数据位: " + recv.NetDevCfg.PORT_CONFIG[1].DataSize);
                net_base_info_lb.Items.Add("串口停止位: " + recv.NetDevCfg.PORT_CONFIG[1].StopBits);
                net_base_info_lb.Items.Add("串口校验位: " + recv.NetDevCfg.PORT_CONFIG[1].Parity);
                net_base_info_lb.Items.Add("PHY断开，Socket动作: " + recv.NetDevCfg.PORT_CONFIG[1].PHYChangeHandle);
                net_base_info_lb.Items.Add("串口RX数据打包长度(<1024): " + recv.NetDevCfg.PORT_CONFIG[1].RxPktlength);
                net_base_info_lb.Items.Add("串口RX数据打包转发的最大等待时间(10ms): " + recv.NetDevCfg.PORT_CONFIG[1].RxPktTimeout);
                net_base_info_lb.Items.Add("工作于TCP CLIENT时，连接TCP SERVER的最大重试次数: " + recv.NetDevCfg.PORT_CONFIG[1].ReConnectCnt);
                net_base_info_lb.Items.Add("串口复位操作: " + (recv.NetDevCfg.PORT_CONFIG[1].ResetCtrl == true ? "清空缓存" : "不清空缓存"));
                net_base_info_lb.Items.Add("域名功能启用标志: " + recv.NetDevCfg.PORT_CONFIG[1].DNSFlag);
                net_base_info_lb.Items.Add("域名: " + recv.NetDevCfg.PORT_CONFIG[1].DomainName);
                net_base_info_lb.Items.Add("DNS主机: " + recv.NetDevCfg.PORT_CONFIG[1].DNSHostIP);
                net_base_info_lb.Items.Add("DNS 端口: " + recv.NetDevCfg.PORT_CONFIG[1].DNSHostPort);
                net_base_info_lb.Items.Add("保留: " + recv.NetDevCfg.PORT_CONFIG[1].Reserved);
                net_base_info_lb.Items.Add("---- 心跳 ----------------------------------------------------------------------------------");
                net_base_info_lb.Items.Add("端口序号: " + recv.NetDevCfg.PORT_CONFIG[0].Index);
                net_base_info_lb.Items.Add("端口启用标志: " + recv.NetDevCfg.PORT_CONFIG[0].PortEn);
                net_base_info_lb.Items.Add("网络工作模式: " + recv.NetDevCfg.PORT_CONFIG[0].NetMode);
                net_base_info_lb.Items.Add("TCP 客户端模式下随即本地端口号: " + recv.NetDevCfg.PORT_CONFIG[0].RandSportFlag);
                net_base_info_lb.Items.Add("网络通讯端口号: " + recv.NetDevCfg.PORT_CONFIG[0].NetPort);
                net_base_info_lb.Items.Add("目的IP地址: " + recv.NetDevCfg.PORT_CONFIG[0].DesIP);
                net_base_info_lb.Items.Add("工作于TCP Server模式时，允许外部连接的端口号: " + recv.NetDevCfg.PORT_CONFIG[0].DesPort);
                net_base_info_lb.Items.Add("串口波特率: " + recv.NetDevCfg.PORT_CONFIG[0].BaudRate);
                net_base_info_lb.Items.Add("串口数据位: " + recv.NetDevCfg.PORT_CONFIG[0].DataSize);
                net_base_info_lb.Items.Add("串口停止位: " + recv.NetDevCfg.PORT_CONFIG[0].StopBits);
                net_base_info_lb.Items.Add("串口校验位: " + recv.NetDevCfg.PORT_CONFIG[0].Parity);
                net_base_info_lb.Items.Add("PHY断开，Socket动作: " + recv.NetDevCfg.PORT_CONFIG[0].PHYChangeHandle);
                net_base_info_lb.Items.Add("串口RX数据打包长度(<1024): " + recv.NetDevCfg.PORT_CONFIG[0].RxPktlength);
                net_base_info_lb.Items.Add("串口RX数据打包转发的最大等待时间(10ms): USE FOR Heartbeat Interval");
                net_base_info_lb.Items.Add("######################## 心跳间隔(50ms): " + recv.NetDevCfg.PORT_CONFIG[0].RxPktTimeout);
                net_base_info_lb.Items.Add("工作于TCP CLIENT时，连接TCP SERVER的最大重试次数: " + recv.NetDevCfg.PORT_CONFIG[0].ReConnectCnt);
                net_base_info_lb.Items.Add("串口复位操作: " + (recv.NetDevCfg.PORT_CONFIG[0].ResetCtrl == true ? "清空" : "不清空"));
                net_base_info_lb.Items.Add("域名功能启用标志: " + recv.NetDevCfg.PORT_CONFIG[0].DNSFlag);
                net_base_info_lb.Items.Add("域名:  USE FOR Heartbeat Content");
                net_base_info_lb.Items.Add("######################## 心跳内容（< 20 Bytes）: " + recv.NetDevCfg.PORT_CONFIG[0].DomainName);
                net_base_info_lb.Items.Add("DNS主机: " + recv.NetDevCfg.PORT_CONFIG[0].DNSHostIP);
                net_base_info_lb.Items.Add("DNS 端口: " + recv.NetDevCfg.PORT_CONFIG[0].DNSHostPort);
                net_base_info_lb.Items.Add("保留: " + recv.NetDevCfg.PORT_CONFIG[0].Reserved);

                // 串口协商进入配置模式使能
                net_base_comcfgEn_cb.Checked = recv.NetDevCfg.HW_CONFIG.ComcfgEn;
                // 目标模块mac地址
                net_base_mod_mac_tb.Text = recv.Mod_Mac;
                // [21]模块名
                net_base_mod_name_tb.Text = recv.NetDevCfg.HW_CONFIG.Modulename;
                // DHCP 使能 
                net_base_dhcp_enable_cb.Checked = recv.NetDevCfg.HW_CONFIG.DhcpEnable;
                //[4]模块IP地址
                net_base_mod_ip_tb.Text = recv.NetDevCfg.HW_CONFIG.DevIP;
                //模块子网掩码
                net_base_mod_mask_tb.Text = recv.NetDevCfg.HW_CONFIG.DevIPMask;
                //模块网关IP
                net_base_mod_gateway_tb.Text = recv.NetDevCfg.HW_CONFIG.DevGWIP;

                // 端口0
                int port_1_index = 1;
                //端口序号
                // 端口启用标志
                net_port_1_enable_cb.Checked = recv.NetDevCfg.PORT_CONFIG[port_1_index].PortEn;
                //网络工作模式
                net_port_1_net_mode_cbo.SelectedItem = recv.NetDevCfg.PORT_CONFIG[port_1_index].NetMode;
                // TCP 客户端模式下随即本地端口号
                net_port_1_rand_port_flag_cb.Checked = recv.NetDevCfg.PORT_CONFIG[port_1_index].RandSportFlag;
                // 网络通讯端口号
                net_port_1_local_net_port_tb.Text = "" + recv.NetDevCfg.PORT_CONFIG[port_1_index].NetPort;
                // 目的IP地址
                net_port_1_dest_ip_tb.Text = recv.NetDevCfg.PORT_CONFIG[port_1_index].DesIP;
                // 工作于TCP Server模式时，允许外部连接的端口号
                net_port_1_dest_port_tb.Text = "" + recv.NetDevCfg.PORT_CONFIG[port_1_index].DesPort;
                // 串口波特率
                net_port_1_baudrate_cbo.SelectedItem = recv.NetDevCfg.PORT_CONFIG[port_1_index].BaudRate;
                net_port_1_databits_cbo.SelectedItem = recv.NetDevCfg.PORT_CONFIG[port_1_index].DataSize;
                net_port_1_stopbits_cbo.SelectedItem = recv.NetDevCfg.PORT_CONFIG[port_1_index].StopBits;
                net_port_1_parity_bit_cbo.SelectedItem = recv.NetDevCfg.PORT_CONFIG[port_1_index].Parity;
                // PHY断开，Socket动作
                net_port_1_phyChangeHandle_cb.Checked = recv.NetDevCfg.PORT_CONFIG[port_1_index].PHYChangeHandle;
                // 串口RX数据打包长度
                net_port_1_rx_pkg_size_tb.Text = String.Format("{0}", recv.NetDevCfg.PORT_CONFIG[port_1_index].RxPktlength);
                // 串口RX数据打包转发的最大等待时间
                net_port_1_rx_pkg_timeout_tb.Text = String.Format("{0}", recv.NetDevCfg.PORT_CONFIG[port_1_index].RxPktTimeout);
                // 工作于TCP CLIENT时，连接TCP SERVER的最大重试次数
                net_port_1_reconnectcnt_tb.Text = String.Format("{0}", recv.NetDevCfg.PORT_CONFIG[port_1_index].ReConnectCnt);
                // 串口复位操作
                net_port_1_resetctrl_cb.Checked = recv.NetDevCfg.PORT_CONFIG[port_1_index].ResetCtrl;
                // 域名功能启用标志
                net_port_1_dns_flag.Checked = recv.NetDevCfg.PORT_CONFIG[port_1_index].DNSFlag;
                // 域名
                net_port_1_dns_domain_tb.Text = recv.NetDevCfg.PORT_CONFIG[port_1_index].DomainName;
                // DNS主机
                net_port_1_dns_host_ip_tb.Text = string.Format("{0}", recv.NetDevCfg.PORT_CONFIG[port_1_index].DNSHostIP);
                // DNS端口
                net_port_1_dns_host_port_tb.Text = string.Format("{0}", recv.NetDevCfg.PORT_CONFIG[port_1_index].DNSHostPort);
                // 保留

                // 心跳包
                int port_2_index = 0;
                net_use_heartbeat_cb.Checked = recv.NetDevCfg.PORT_CONFIG[port_2_index].PortEn;
                net_heartbeat_content_tb.Text = recv.NetDevCfg.PORT_CONFIG[port_2_index].DomainName;
                net_heartbeat_interval_tb.Text = "" + recv.NetDevCfg.PORT_CONFIG[port_2_index].RxPktTimeout;

            }
        }

        private void net_search_btn_Click(object sender, EventArgs e)
        {
            if(net_search_btn.Text.Equals("搜索设备"))
            {
                if (!netStarted)
                {
                    if (udpServerRunning)
                    {
                        while (udpServerRunning)
                        {
                            Thread.Sleep(200);
                            Console.WriteLine("waiting for udpserver running done.");
                        }
                    }
                }
                StartNetUdpServer();

                //// 清空原来的数据
                //net_db.Clear();
                //dev_dgv.Rows.Clear();

                if (!CheckNetConfigStatus())
                    return;

                NET_COMM comm_cmd = new NET_COMM();
                byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
                comm_cmd.setbytes(ch9121_cfg_flag);
                comm_cmd.setu8((byte)NET_CMD.NET_MODULE_CMD_SEARCH); // 设置cmd

                netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息 广播地址
                byte[] message = comm_cmd.Message;
                int ret = netClient.Send(message, message.Length, netEndpoint);

                if (net_search_size.SelectedItem == null)
                    net_search_size.SelectedIndex = 1;
                string s = net_search_size.SelectedItem.ToString();
                int searchCnt = Convert.ToInt32(s);
                BeginInvoke(new ThreadStart(delegate () {
                    enableNetConfigUI(false);
                    if (searchCnt == -1)
                    {
                        net_search_btn.Enabled = true;
                    }
                    net_search_btn.Text = "正在搜索...";
                }));
                
                new Thread(new ThreadStart(delegate {
                    netCmdStarted = true;
                    searchDev = true;
                    searching = true;
                    do
                    {
                        try
                        {
                            netClient.Send(message, message.Length, netEndpoint);
                            //Console.WriteLine("[{0}]Sending message ...", searchCnt);
                            if (searchCnt == 1)
                            {
                                searchDev = false;
                            }
                            else
                            {
                                Thread.Sleep(3000);
                            }
                            if (searchCnt != -1)
                                searchCnt--;
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Searching Error: " + ex.Message);
                            searchCnt = 1;
                            searchDev = false;
                            searching = false;
                        }
                    } while (searchDev);
                    searching = false;
                    BeginInvoke(new ThreadStart(delegate () {
                        enableNetConfigUI(true);
                        netCmdStarted = false;
                        net_search_btn.Text = "搜索设备";
                    }));

                    Console.WriteLine("Searching done...");
                })).Start();
            }
            else
            {
                searchDev = false;
            }
        }

    delegate void enableNetConfigUIDelegate(bool enable);
        private void enableNetConfigUI(bool enable)
        {
            enableNetConfigUIDelegate d = new enableNetConfigUIDelegate(enableNetConfigUI);
            if (net_search_btn.InvokeRequired)
            {
                this.Invoke(d, enable);
            }
            else
            {
                net_search_btn.Enabled = enable;
                net_getCfg_btn.Enabled = enable;
                net_setCfg_btn.Enabled = enable;
                net_reset_btn.Enabled = enable;
                net_refresh_netcard_btn.Enabled = enable;
                net_card_combox.Enabled = enable;
                net_reset_default.Enabled = enable;
                net_load_cfg_btn.Enabled = enable;
                net_save_cfg_btn.Enabled = enable;
            }
        }

        private bool CheckNetConfigStatus()
        {
            if (!netStarted && netClient != null)
                StartNetUdpServer();

            if (!netStarted)
            {
                Console.WriteLine("未启动NetUDPClient");
                return false;
            }
            if (netCmdStarted)
            {
                MessageBox.Show("正在执行操作!", "提示", MessageBoxButtons.OK);
                return false;
            }
            return true;
        }

        private void net_getCfg_btn_Click(object sender, EventArgs e)
        {
            string mod_mac;
            if (dev_dgv.CurrentRow == null)
            {
                MessageBox.Show("请先在列表中选择设备!", "提示", MessageBoxButtons.OK);
                return;
            }
            mod_mac = dev_dgv.CurrentRow.Cells[ModMac.Name].Value.ToString();
            if (!net_db.IndexSearch.ContainsKey(mod_mac))
            {
                MessageBox.Show("不在列表!", "获取配置失败", MessageBoxButtons.OK);
            }
            NetGetCfg(net_db.IndexSearch[mod_mac]);
        }

        private void NetGetCfg(MODULE_SEARCH search)
        {
            if(!net_card_dict[cur_desc].PC_MAC.Equals(search.PcMac))
            {
                MessageBox.Show(String.Format("请选择与主机mac: {0} 对应的网卡", search.PcMac), "NetGetCfg Error", MessageBoxButtons.OK);
                return;
            }
            if (!CheckNetConfigStatus())
                StartNetUdpServer();
            NET_COMM comm_cmd = new NET_COMM();
            byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
            comm_cmd.setbytes(ch9121_cfg_flag);
            comm_cmd.setu8((byte)NET_CMD.NET_MODULE_CMD_GET); // 设置cmd

            string param_mod_mac = search.ModMac.Replace(":", "").ToLower();

            byte[] b_mod_mac = CCommondMethod.FromHex(param_mod_mac);
            comm_cmd.setbytes(b_mod_mac); // 设置mod_mac

            byte[] message = comm_cmd.Message;

            netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            int ret = netClient.Send(message, message.Length, netEndpoint);
            netCmdStarted = true;
            enableNetConfigUI(false);
        }

        private bool CheckIP(string ipStr)
        {
            IPAddress ip;
            if (IPAddress.TryParse(ipStr, out ip))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckMacAddr(string macAddr)
        {
            string ma = "([A-Fa-f0-9]{2}:){5}[A-Fa-f0-9]{2}";

            Regex r = new Regex(ma, RegexOptions.IgnoreCase);

            Match m = r.Match(macAddr.Trim());
            if (m.Success)
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        private void net_setCfg_btn_Click(object sender, EventArgs e)
        {
            if (!CheckNetConfigStatus())
                StartNetUdpServer();
            string mod_mac = net_base_mod_mac_tb.Text.ToString();
            string mod_ip = net_base_mod_ip_tb.Text.ToString();
            string mod_mask = net_base_mod_mask_tb.Text.ToString();
            string mod_gateway = net_base_mod_gateway_tb.Text.ToString();
            string pc_mac = net_pc_mac_label.Text.ToString();

            if (!net_db.IndexNetDevCfg.ContainsKey(mod_mac))
            {
                MessageBox.Show("请先在列表中选择设备", "提示", MessageBoxButtons.OK);
                return;
            }

            if(!CheckMacAddr(mod_mac))
            {
                MessageBox.Show("Mod Mac地址格式错误, eg: 11:22:33:44:55:66", "Mac地址", MessageBoxButtons.OK);
                return;
            }

            if (!CheckIP(mod_ip))
            {
                MessageBox.Show("IP地址格式错误, eg: 192.168.1.200", "IP地址", MessageBoxButtons.OK);
                return;
            }
            if (!CheckIP(mod_mask))
            {
                MessageBox.Show("Mask地址格式错误, eg: 255.255.255.0", "IP地址", MessageBoxButtons.OK);
                return;
            }
            if (!CheckIP(mod_gateway))
            {

                MessageBox.Show("网关地址格式错误, eg: 192.168.1.1", "IP地址", MessageBoxButtons.OK);
                return;
            }

            if (!CheckMacAddr(pc_mac))
            {
                MessageBox.Show("Pc Mac地址格式错误, eg: 11:22:33:44:55:66", "Mac地址", MessageBoxButtons.OK);
                return;
            }

            byte[] rawdata = NewNetComm();

            netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            int ret = netClient.Send(rawdata, rawdata.Length, netEndpoint);

            netCmdStarted = true;
            enableNetConfigUI(false);
        }

        private byte[] NewNetComm()
        {
            string mod_mac = net_base_mod_mac_tb.Text.ToString();
            string pc_mac = net_pc_mac_label.Text.ToString();
            string dev_net_ip = net_base_mod_ip_tb.Text.ToString();
            string dev_gateway_ip = net_base_mod_gateway_tb.Text.ToString();
            string dev_mask = net_base_mod_mask_tb.Text.ToString();
            int writeIndex = 0;
            byte[] rawdata = new byte[285];

            #region NET_COMM
            // [16] 用来标识通信_new
            string flag = "CH9121_CFG_FLAG\0";
            byte[] bflag = Encoding.Default.GetBytes(flag);
            Array.Copy(bflag, 0, rawdata, writeIndex, bflag.Length);
            writeIndex += bflag.Length;
            //Console.WriteLine("flag={0}", CCommondMethod.ToHex(bflag, "", " "));

            // CMD
            byte cmd = (byte)NET_CMD.NET_MODULE_CMD_SET;
            rawdata[writeIndex++] = cmd; // 设置cmd
            //Console.WriteLine("cmd={0}", cmd);

            // mod_mac [6]
            byte[] b_mod_mac = CCommondMethod.FromHex(mod_mac.Replace(":", ""));// 设置mod_mac
            Array.Copy(b_mod_mac, 0, rawdata, writeIndex, b_mod_mac.Length);
            writeIndex += b_mod_mac.Length;
            //Console.WriteLine("mod_mac={0}", CCommondMethod.ToHex(b_mod_mac, "", ":"));

            // pc_mac [6]
            byte[] b_pc_mac = CCommondMethod.FromHex(pc_mac.Replace(":", "")); // 设置pc_mac
            Array.Copy(b_pc_mac, 0, rawdata, writeIndex, b_pc_mac.Length);
            writeIndex += b_pc_mac.Length;
            //Console.WriteLine("pc_mac={0}", CCommondMethod.ToHex(b_pc_mac, "", ":"));

            // len在后面才算
            int lenIndex = writeIndex;
            byte blen = 0x0;
            rawdata[writeIndex++] = blen;

            #region HW_CFG
            //DEVICEHW_CONFIG hw_cfg = new DEVICEHW_CONFIG();
            // dev_type
            rawdata[writeIndex++] = 0x21;
            // dev_sub_type
            rawdata[writeIndex++] = 0x21;
            // dev_id
            rawdata[writeIndex++] = 0x01;
            // dev_hw_ver
            rawdata[writeIndex++] = 0x02;
            // dev_sw_ver
            rawdata[writeIndex++] = 0x03;

            // dev_name [21] 模块名
            byte[] bdev_name = Encoding.Default.GetBytes(net_base_mod_name_tb.Text.ToString());
            Array.Copy(bdev_name, 0, rawdata, writeIndex, bdev_name.Length);
            writeIndex += bdev_name.Length;
            //Console.WriteLine("dev_name={0}", CCommondMethod.ToHex(bdev_name, "", " "));

            int dev_last_len = 21 - bdev_name.Length;
            byte[] dev_name_last = new byte[dev_last_len];
            Array.Copy(dev_name_last, 0, rawdata, writeIndex, dev_name_last.Length);
            writeIndex += dev_name_last.Length;
            //Console.WriteLine("dev_name_last={0}", CCommondMethod.ToHex(dev_name_last, "", " "));

            // dev_net_mac [6]
            string dev_net_mac = "02:03:04:05:06:07";
            byte[] b_dev_net_mac = CCommondMethod.FromHex(dev_net_mac.Replace(":", ""));
            Array.Copy(b_dev_net_mac, 0, rawdata, writeIndex, b_dev_net_mac.Length);
            writeIndex += b_dev_net_mac.Length;
            //Console.WriteLine("dev_net_mac={0}", CCommondMethod.ToHex(b_dev_net_mac, "", ":"));

            // dev_net_ip [4]
            byte[] b_dev_net_ip = IPAddress.Parse(dev_net_ip).GetAddressBytes();
            Array.Copy(b_dev_net_ip, 0, rawdata, writeIndex, b_dev_net_ip.Length);
            writeIndex += b_dev_net_ip.Length;
            //Console.WriteLine("dev_net_ip={0}", CCommondMethod.ToHex(b_dev_net_ip, "", "."));

            // dev_gateway_ip [4]
            byte[] b_dev_gateway_ip = IPAddress.Parse(dev_gateway_ip).GetAddressBytes();
            Array.Copy(b_dev_gateway_ip, 0, rawdata, writeIndex, b_dev_gateway_ip.Length);
            writeIndex += b_dev_gateway_ip.Length;
            //Console.WriteLine("dev_gateway_ip={0}", CCommondMethod.ToHex(b_dev_gateway_ip, "", "."));

            // dev_mask [4]
            byte[] b_dev_mask = IPAddress.Parse(dev_mask).GetAddressBytes();
            Array.Copy(b_dev_mask, 0, rawdata, writeIndex, b_dev_mask.Length);
            writeIndex += b_dev_mask.Length;
            //Console.WriteLine("dev_mask={0}", CCommondMethod.ToHex(b_dev_mask, "", "."));

            // dev_dhcp_enable
            rawdata[writeIndex++] = (byte)(net_base_dhcp_enable_cb.Checked == true ? 0x01 : 0x00);

            // dev_web_port 0050 -> 80
            byte[] bdev_web_port = new byte[2] { 0x50, 0x00 };
            Array.Copy(bdev_web_port, 0, rawdata, writeIndex, bdev_web_port.Length);
            writeIndex += bdev_web_port.Length;
            //Console.WriteLine("dev_web_port={0}", CCommondMethod.ToHex(bdev_web_port, "", " "));

            // dev_user_name 
            byte[] bdev_user_name = new byte[8];
            Array.Copy(bdev_user_name, 0, rawdata, writeIndex, bdev_user_name.Length);
            writeIndex += bdev_user_name.Length;
            //Console.WriteLine("dev_user_name={0}", CCommondMethod.ToHex(bdev_user_name, "", " "));

            // dev_pw_enable
            rawdata[writeIndex++] = 0x00;

            // dev_pw
            byte[] b_dev_pw = new byte[8];
            Array.Copy(b_dev_pw, 0, rawdata, writeIndex, b_dev_pw.Length);
            writeIndex += b_dev_pw.Length;
            //Console.WriteLine("dev_pw={0}", CCommondMethod.ToHex(b_dev_pw, "", " "));

            // dev_update_flag
            rawdata[writeIndex++] = 0x00;

            // dev_com_enable
            rawdata[writeIndex++] = (byte)(net_base_comcfgEn_cb.Checked == true ? 0x01 : 0x00);

            // dev_reserved
            byte[] b_dev_reserved = new byte[8];
            Array.Copy(b_dev_reserved, 0, rawdata, writeIndex, b_dev_reserved.Length);
            writeIndex += b_dev_reserved.Length;
            //Console.WriteLine("dev_reserved={0}", CCommondMethod.ToHex(b_dev_reserved, "", " "));
            #endregion HW_CFG

            #region HeartBeat
            // 心跳包 port index = 0x00
            //DEVICEPORT_CONFIG dev_port_1 = new DEVICEPORT_CONFIG();
            // port_id
            rawdata[writeIndex++] = 0x00;
            // port_enable
            rawdata[writeIndex++] = (byte)(net_use_heartbeat_cb.Checked == true ? 0x01 : 0x00);

            // port_net_mode
            rawdata[writeIndex++] = 0x01; // 0x00 TCP_Client
            // port_port_rand_enable
            rawdata[writeIndex++] = 0x01;
            // port_net_port
            byte[] bport2_net_port = new byte[2] { 0xB8, 0x0B }; // 3000
            Array.Copy(bport2_net_port, 0, rawdata, writeIndex, bport2_net_port.Length);
            writeIndex += bport2_net_port.Length;
            // port_dest_ip
            string port2_dest_ip = "192.168.0.100";
            byte[] b_port2_dest_ip = IPAddress.Parse(port2_dest_ip).GetAddressBytes();
            Array.Copy(b_port2_dest_ip, 0, rawdata, writeIndex, b_port2_dest_ip.Length);
            writeIndex += b_port2_dest_ip.Length;
            // port_dest_port 07D0 -> 2000
            byte[] bport2_dest_port = new byte[2] { 0xD0, 0xD0 };
            Array.Copy(bport2_dest_port, 0, rawdata, writeIndex, bport2_dest_port.Length);
            writeIndex += bport2_dest_port.Length;
            // port_baudrate
            int port2_baudrate = 115200;
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 24) & 0xff);
            // port_datasize
            rawdata[writeIndex++] = 0x08;
            // port_stopbit
            rawdata[writeIndex++] = 0x01;
            // port_parity
            rawdata[writeIndex++] = 0x04;
            /* PHY断开，Socket动作，1：关闭Socket 2、不动作*/
            // port_phy_disconnect
            rawdata[writeIndex++] = 0x01;
            // port_package_size
            int port2_package_size = 1024;
            rawdata[writeIndex++] = (byte)((port2_package_size >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 24) & 0xff);
            // port_package_timeout
            int port2_package_timeout = 0;
            if (net_use_heartbeat_cb.Checked)
            {
                port2_package_timeout = Convert.ToInt32(net_heartbeat_interval_tb.Text.ToString());
            }
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 24) & 0xff);
            // connect_count
            rawdata[writeIndex++] = 0x00;
            // port_reset_ctrl
            rawdata[writeIndex++] = 0x00;
            // port_dns_enable
            rawdata[writeIndex++] = 0x00;
            // port_domain 心跳内容
            byte[] heartbeatContent = new byte[20];
            string str_heartbeatContent = "";
            if(net_use_heartbeat_cb.Checked)
            {
                str_heartbeatContent = net_heartbeat_content_tb.Text.ToString();
            }
            byte[] bheartbeatContent = Encoding.Default.GetBytes(str_heartbeatContent);
            Array.Copy(bheartbeatContent, 0, heartbeatContent, 0, bheartbeatContent.Length);
            //Console.WriteLine("dev_name={0}", CCommondMethod.ToHex(bdev_name, "", " "));

            int heartbeatContent_last_len = 20 - bheartbeatContent.Length;
            if (heartbeatContent_last_len > 0)
            {
                byte[] heartbeatContent_last = new byte[heartbeatContent_last_len];
                Array.Copy(heartbeatContent_last, 0, heartbeatContent, bheartbeatContent.Length, heartbeatContent_last.Length);
                //Console.WriteLine("dev_name_last={0}", CCommondMethod.ToHex(dev_name_last, "", " "));
            }
            Array.Copy(heartbeatContent, 0, rawdata, writeIndex, heartbeatContent.Length);
            writeIndex += heartbeatContent.Length;
            // port_host_ip
            string port2_host_ip = "0.0.0.0";
            byte[] b_port2_host_ip = IPAddress.Parse(port2_host_ip).GetAddressBytes();
            Array.Copy(b_port2_host_ip, 0, rawdata, writeIndex, b_port2_host_ip.Length);
            writeIndex += b_port2_host_ip.Length;
            // port_dns_port
            int port2_dns_port = 0;
            rawdata[writeIndex++] = (byte)((port2_dns_port >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_dns_port >> 8) & 0xff);

            byte[] b_port2_reserved = new byte[8];
            Array.Copy(b_port2_reserved, 0, rawdata, writeIndex, b_port2_reserved.Length);
            writeIndex += b_port2_reserved.Length;
            #endregion HeartBeat

            #region PORT_1
            // Port 0x01 端口1
            string str_port_1_net_mode = net_port_1_net_mode_cbo.SelectedItem.ToString();
            //DEVICEPORT_CONFIG dev_port_2 = new DEVICEPORT_CONFIG();
            // port_id
            rawdata[writeIndex++] = 0x01;
            // port_enable
            rawdata[writeIndex++] = (byte)(net_port_1_enable_cb.Checked == true ? 0x01 : 0x00);

            // port_net_mode
            byte port_1_net_mode;
            if (MODULE_TYPE.TCP_SERVER.ToString().Equals(str_port_1_net_mode))
            {
                port_1_net_mode = 0x00;
            }
            else if (MODULE_TYPE.TCP_CLIENT.ToString().Equals(str_port_1_net_mode))
            {
                port_1_net_mode = 0x01;
            }
            else if (MODULE_TYPE.UDP_SERVER.ToString().Equals(str_port_1_net_mode))
            {
                port_1_net_mode = 0x02;
            }
            else //if (MODULE_TYPE.UDP_CLIENT.ToString().Equals(str_port_1_net_mode))
            {
                port_1_net_mode = 0x03;
            }
            rawdata[writeIndex++] = port_1_net_mode;

            // port_port_rand_enable
            rawdata[writeIndex++] = (byte)(net_port_1_rand_port_flag_cb.Checked == true ? 0x01 : 0x00); ;
            // port_net_port 0fa1 -> 4001 本地端口
            int net_port = Convert.ToInt32(net_port_1_local_net_port_tb.Text.ToString());
            byte[] bport_net_port = new byte[2] {
                (byte)((net_port >> 0) & 0xff), // 0xa1
                (byte)((net_port >> 8) & 0xff) // 0x0f
            };
            Array.Copy(bport_net_port, 0, rawdata, writeIndex, bport_net_port.Length);
            writeIndex += bport_net_port.Length;
            //Console.WriteLine("port_net_port={0}", CCommondMethod.ToHex(bport_net_port, "", " "));

            // port_dest_ip 
            string port_dest_ip = net_port_1_dest_ip_tb.Text.ToString();
            byte[] b_port_dest_ip = IPAddress.Parse(port_dest_ip).GetAddressBytes();
            Array.Copy(b_port_dest_ip, 0, rawdata, writeIndex, b_port_dest_ip.Length);
            writeIndex += b_port_dest_ip.Length;
            //Console.WriteLine("b_port_dest_ip={0}", CCommondMethod.ToHex(b_port_dest_ip, "", " "));

            // port_dest_port 03e8 -> 1000 目标端口
            int des_net_port = Convert.ToInt32(net_port_1_dest_port_tb.Text.ToString());
            byte[] bport_dest_port = new byte[2] {
                (byte)((net_port >> 0) & 0xff), // 0xe8
                (byte)((net_port >> 8) & 0xff) // 0x03
            };
            Array.Copy(bport_dest_port, 0, rawdata, writeIndex, bport_dest_port.Length);
            writeIndex += bport_dest_port.Length;
            //Console.WriteLine("bport_dest_port={0}", CCommondMethod.ToHex(bport_dest_port, "", " "));

            // port_baudrate
            uint baudrate = (uint)GetEnumValue(typeof(BAUDRATE), net_port_1_baudrate_cbo.SelectedItem.ToString());
            rawdata[writeIndex++] = (byte)((baudrate >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 24) & 0xff);

            // port_datasize
            rawdata[writeIndex++] = (byte)GetEnumValue(typeof(DATABITS), net_port_1_databits_cbo.SelectedItem.ToString());
            // port_stopbit
            rawdata[writeIndex++] = (byte)GetEnumValue(typeof(STOPBITS), net_port_1_stopbits_cbo.SelectedItem.ToString());
            // port_parity
            rawdata[writeIndex++] = (byte)GetEnumValue(typeof(PARITY), net_port_1_parity_bit_cbo.SelectedItem.ToString()); ;
            // port_phy_disconnect
            rawdata[writeIndex++] = (byte)(net_port_1_phyChangeHandle_cb.Checked == true ? 0x01 : 0x00);
            // port_package_size
            int package_size = Convert.ToInt32(net_port_1_rx_pkg_size_tb.Text.ToString());
            rawdata[writeIndex++] = (byte)((package_size >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 24) & 0xff);
            // port_package_timeout
            int package_timeout = Convert.ToInt32(net_port_1_rx_pkg_timeout_tb.Text.ToString()); ;
            rawdata[writeIndex++] = (byte)((package_timeout >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 24) & 0xff);
            // connect_count
            rawdata[writeIndex++] = Convert.ToByte(net_port_1_reconnectcnt_tb.Text.ToString());
            // port_reset_ctrl
            rawdata[writeIndex++] = (byte)(net_port_1_resetctrl_cb.Checked == true ? 0x01 : 0x00);
            // port_dns_enable
            rawdata[writeIndex++] = (byte)(net_port_1_dns_flag.Checked == true ? 0x01 : 0x00); ;

            // port_domain
            byte[] bport_domain = new byte[20];
            string domain_name = net_port_1_dns_domain_tb.Text.ToString();
            byte[] bdomain_name = Encoding.Default.GetBytes(domain_name);
            Array.Copy(bdomain_name, 0, bport_domain, 0, bdomain_name.Length);
            //Console.WriteLine("dev_name={0}", CCommondMethod.ToHex(bdev_name, "", " "));
            int domain_name_last_len = 20 - bdomain_name.Length;
            if (domain_name_last_len > 0)
            {
                byte[] domain_name_last = new byte[domain_name_last_len];
                Array.Copy(domain_name_last, 0, bport_domain, bdomain_name.Length, domain_name_last.Length);
                //Console.WriteLine("dev_name_last={0}", CCommondMethod.ToHex(dev_name_last, "", " "));
            }
            Array.Copy(bport_domain, 0, rawdata, writeIndex, bport_domain.Length);
            writeIndex += bport_domain.Length;

            // port_host_ip
            string port_host_ip = net_port_1_dns_host_ip_tb.Text.ToString();
            byte[] b_port_host_ip = IPAddress.Parse(port_host_ip).GetAddressBytes();
            Array.Copy(b_port_host_ip, 0, rawdata, writeIndex, b_port_host_ip.Length);
            writeIndex += b_port_host_ip.Length;
            //Console.WriteLine("b_port_host_ip={0}", CCommondMethod.ToHex(b_port_host_ip, "", " "));

            // port_dns_port
            int port_dns_port = Convert.ToInt32(net_port_1_dns_host_port_tb.Text.ToString());
            rawdata[writeIndex++] = (byte)((port_dns_port >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port_dns_port >> 8) & 0xff);

            byte[] b_port_reserved = new byte[8];
            Array.Copy(b_port_reserved, 0, rawdata, writeIndex, b_port_reserved.Length);
            writeIndex += b_port_reserved.Length;
            //Console.WriteLine("b_port_reserved={0}", CCommondMethod.ToHex(b_port_reserved, "", " "));
            #endregion PORT_1
            //NET_DEVICE_CONFIG dev_cfg = new NET_DEVICE_CONFIG();

            int len = writeIndex - 30;
            rawdata[lenIndex] = (byte)len;
            #endregion NET_COMM

            return rawdata;
        }

        public static int GetEnumValue(Type enumType, string enumName)
        {
            System.Array values = System.Enum.GetValues(enumType);
            foreach (var value in values)
            {
                if (value.ToString().Equals(enumName))
                    return (int)value;
            }
            return 0;
        }


        private void NetSetCfg(string mod_mac, MODULE_SEARCH mod_search)
        {
            int setindex = 0;
            if (!CheckNetConfigStatus())
                return;
            NET_COMM comm_cmd = new NET_COMM();
            byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
            comm_cmd.setbytes(ch9121_cfg_flag);
            comm_cmd.setu8((byte)NET_CMD.NET_MODULE_CMD_SET); // 设置cmd
            setindex++;

            string param_mod_mac = mod_search.ModMac.Replace(":", "").ToLower();
            byte[] b_mod_mac = CCommondMethod.FromHex(param_mod_mac);
            comm_cmd.setbytes(b_mod_mac); // 设置mod_mac
            setindex += b_mod_mac.Length;

            //string param_pc_mac = mod_search.PcMac.Replace(":", "").ToLower();
            string param_pc_mac = net_pc_mac_label.Text.Replace(":", "").ToLower();

            byte[] b_pc_mac = CCommondMethod.FromHex(param_pc_mac);
            comm_cmd.setbytes(b_pc_mac); // 设置pc_mac
            setindex += b_pc_mac.Length;

            int len = net_db.IndexNetDevCfg[mod_mac].RawData.Length - 1;
            comm_cmd.setu8(len);
            setindex++;

            comm_cmd.setbytes(net_db.IndexNetDevCfg[mod_mac].HW_CONFIG.RawData);
            setindex += net_db.IndexNetDevCfg[mod_mac].HW_CONFIG.RawData.Length;

            comm_cmd.setbytes(net_db.IndexNetDevCfg[mod_mac].PORT_CONFIG[0].RawData);
            setindex += net_db.IndexNetDevCfg[mod_mac].PORT_CONFIG[0].RawData.Length;

            comm_cmd.setbytes(net_db.IndexNetDevCfg[mod_mac].PORT_CONFIG[1].RawData);
            setindex += net_db.IndexNetDevCfg[mod_mac].PORT_CONFIG[1].RawData.Length;

            netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            int ret = netClient.Send(comm_cmd.Message, comm_cmd.Message.Length, netEndpoint);


            netCmdStarted = true;
            enableNetConfigUI(false);
        }

        private void NetSetCfgForLoadCfg(byte[] data)
        {
            Console.WriteLine("NetSetCfgForLoadCfg ...");
            int setindex = 0;
            NET_COMM comm_cmd = new NET_COMM();
            byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
            comm_cmd.setbytes(ch9121_cfg_flag);
            comm_cmd.setu8((byte)NET_CMD.NET_MODULE_CMD_SET); // 设置cmd
            setindex++;
            string mod_mac = net_base_mod_mac_tb.Text.ToString();
            if(mod_mac.Trim().Length == 0)
            {
                mod_mac = "00:00:00:00:00:00";
            }
            string param_mod_mac = mod_mac.Replace(":", "").ToLower();
            byte[] b_mod_mac = CCommondMethod.FromHex(param_mod_mac);
            comm_cmd.setbytes(b_mod_mac); // 设置mod_mac
            setindex += b_mod_mac.Length;

            string pc_mac = net_pc_mac_label.Text.ToString();
            if (pc_mac.Trim().Length == 0)
            {
                pc_mac = "00:00:00:00:00:00";
            }
            string param_pc_mac = pc_mac.Replace(":", "").ToLower();

            byte[] b_pc_mac = CCommondMethod.FromHex(param_pc_mac);
            comm_cmd.setbytes(b_pc_mac); // 设置pc_mac
            setindex += b_pc_mac.Length;

            int len = data.Length - 1;
            comm_cmd.setu8(len);
            setindex++;

            comm_cmd.setbytes(data);
            setindex += data.Length;

            comm_cmd.UpdateMessage();

            UpdateDevCfgUI(comm_cmd);

            //netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            //int ret = netClient.Send(comm_cmd.Message, comm_cmd.Message.Length, netEndpoint);

            //netCmdStarted = true;
            //enableNetConfigUI(false);
        }

        private void net_reset_btn_Click(object sender, EventArgs e)
        {
            if (!CheckNetConfigStatus())
                StartNetUdpServer();
            string mod_mac = net_base_mod_mac_tb.Text;
            string pc_mac = net_pc_mac_label.Text.Replace(":", "").ToLower();
            if (!net_db.IndexSearch.ContainsKey(mod_mac))
            {
                MessageBox.Show("请先在列表中选择设备", "提示", MessageBoxButtons.OK);
                return;
            }
            NetResetCfg(mod_mac);
        }

        private void NetSetDefaultCFG(string mod_mac, string pc_mac)
        {
            int writeIndex = 0;
            if (!CheckNetConfigStatus())
                return;
            byte[] rawdata = new byte[285];

            string flag = "CH9121_CFG_FLAG\0";	// 用来标识通信_new
            byte[] bflag = Encoding.Default.GetBytes(flag);
            Array.Copy(bflag, 0, rawdata, writeIndex, bflag.Length);
            writeIndex += bflag.Length;
            //Console.WriteLine("flag={0}", CCommondMethod.ToHex(bflag, "", " "));

            byte cmd = (byte)NET_CMD.NET_MODULE_CMD_SET;
            rawdata[writeIndex++] = cmd; // 设置cmd
            //Console.WriteLine("cmd={0}", cmd);

            // mod_mac [6]
            byte[] b_mod_mac = CCommondMethod.FromHex(mod_mac.Replace(":", ""));// 设置mod_mac
            Array.Copy(b_mod_mac, 0, rawdata, writeIndex, b_mod_mac.Length);
            writeIndex += b_mod_mac.Length;
            //Console.WriteLine("mod_mac={0}", CCommondMethod.ToHex(b_mod_mac, "", ":"));

            // pc_mac [6]
            byte[] b_pc_mac = CCommondMethod.FromHex(pc_mac.Replace(":", "")); // 设置pc_mac
            Array.Copy(b_pc_mac, 0, rawdata, writeIndex, b_pc_mac.Length);
            writeIndex += b_pc_mac.Length;
            //Console.WriteLine("pc_mac={0}", CCommondMethod.ToHex(b_pc_mac, "", ":"));

            // len在后面才算
            int lenIndex = writeIndex;
            byte blen = 0x0;
            rawdata[writeIndex++] = blen;

            //DEVICEHW_CONFIG hw_cfg = new DEVICEHW_CONFIG();
            // dev_type
            rawdata[writeIndex++] = 0x21;
            // dev_sub_type
            rawdata[writeIndex++] = 0x21;
            // dev_id
            rawdata[writeIndex++] = 0x01;
            // dev_hw_ver
            rawdata[writeIndex++] = 0x02;
            // dev_sw_ver
            rawdata[writeIndex++] = 0x03;

            // dev_name [21]
            string dev_name = "ro board";
            byte[] bdev_name = Encoding.Default.GetBytes(dev_name);
            Array.Copy(bdev_name, 0, rawdata, writeIndex, bdev_name.Length);
            writeIndex += bdev_name.Length;
            //Console.WriteLine("dev_name={0}", CCommondMethod.ToHex(bdev_name, "", " "));

            int dev_last_len = 21 - bdev_name.Length;
            byte[] dev_name_last = new byte[dev_last_len];
            Array.Copy(dev_name_last, 0, rawdata, writeIndex, dev_name_last.Length);
            writeIndex += dev_name_last.Length;
            //Console.WriteLine("dev_name_last={0}", CCommondMethod.ToHex(dev_name_last, "", " "));

            // dev_net_mac [6]
            string dev_net_mac = "02:03:04:05:06:07";
            byte[] b_dev_net_mac = CCommondMethod.FromHex(dev_net_mac.Replace(":",""));
            Array.Copy(b_dev_net_mac, 0, rawdata, writeIndex, b_dev_net_mac.Length);
            writeIndex += b_dev_net_mac.Length;
            //Console.WriteLine("dev_net_mac={0}", CCommondMethod.ToHex(b_dev_net_mac, "", ":"));

            // dev_net_ip [4]
            string dev_net_ip = "192.168.0.178";
            byte[] b_dev_net_ip = IPAddress.Parse(dev_net_ip).GetAddressBytes();
            Array.Copy(b_dev_net_ip, 0, rawdata, writeIndex, b_dev_net_ip.Length);
            writeIndex += b_dev_net_ip.Length;
            //Console.WriteLine("dev_net_ip={0}", CCommondMethod.ToHex(b_dev_net_ip, "", "."));

            // dev_gateway_ip [4]
            string dev_gateway_ip = "192.168.0.1";
            byte[] b_dev_gateway_ip = IPAddress.Parse(dev_gateway_ip).GetAddressBytes();
            Array.Copy(b_dev_gateway_ip, 0, rawdata, writeIndex, b_dev_gateway_ip.Length);
            writeIndex += b_dev_gateway_ip.Length;
            //Console.WriteLine("dev_gateway_ip={0}", CCommondMethod.ToHex(b_dev_gateway_ip, "", "."));

            // dev_mask [4]
            string dev_mask = "255.255.255.0";
            byte[] b_dev_mask = IPAddress.Parse(dev_mask).GetAddressBytes();
            Array.Copy(b_dev_mask, 0, rawdata, writeIndex, b_dev_mask.Length);
            writeIndex += b_dev_mask.Length;
            //Console.WriteLine("dev_mask={0}", CCommondMethod.ToHex(b_dev_mask, "", "."));

            // dev_dhcp_enable
            rawdata[writeIndex++] = 0x00;

            // dev_web_port
            byte[] bdev_web_port = new byte[2] { 0x50, 0x00 };
            Array.Copy(bdev_web_port, 0, rawdata, writeIndex, bdev_web_port.Length);
            writeIndex += bdev_web_port.Length;
            //Console.WriteLine("dev_web_port={0}", CCommondMethod.ToHex(bdev_web_port, "", " "));

            // dev_user_name
            byte[] bdev_user_name = new byte[8];
            Array.Copy(bdev_user_name, 0, rawdata, writeIndex, bdev_user_name.Length);
            writeIndex += bdev_user_name.Length;
            //Console.WriteLine("dev_user_name={0}", CCommondMethod.ToHex(bdev_user_name, "", " "));

            // dev_pw_enable
            rawdata[writeIndex++] = 0x00;

            // dev_pw
            byte[] b_dev_pw = new byte[8];
            Array.Copy(b_dev_pw, 0, rawdata, writeIndex, b_dev_pw.Length);
            writeIndex += b_dev_pw.Length;
            //Console.WriteLine("dev_pw={0}", CCommondMethod.ToHex(b_dev_pw, "", " "));

            // dev_update_flag
            rawdata[writeIndex++] = 0x00;

            // dev_com_enable
            rawdata[writeIndex++] = 0x00;

            // dev_reserved
            byte[] b_dev_reserved = new byte[8];
            Array.Copy(b_dev_reserved, 0, rawdata, writeIndex, b_dev_reserved.Length);
            writeIndex += b_dev_reserved.Length;
            //Console.WriteLine("dev_reserved={0}", CCommondMethod.ToHex(b_dev_reserved, "", " "));

            //DEVICEPORT_CONFIG dev_port_1 = new DEVICEPORT_CONFIG();
            // port_id
            rawdata[writeIndex++] = 0x00;
            // port_enable
            rawdata[writeIndex++] = 0x00;
            // port_net_mode
            rawdata[writeIndex++] = 0x02;
            // port_port_rand_enable
            rawdata[writeIndex++] = 0x01;
            // port_net_port
            byte[] bport_net_port = new byte[2] { 0xb8, 0x0b };
            Array.Copy(bport_net_port, 0, rawdata, writeIndex, bport_net_port.Length);
            writeIndex += bport_net_port.Length;
            //Console.WriteLine("port_net_port={0}", CCommondMethod.ToHex(bport_net_port, "", " "));

            // port_dest_ip
            string port_dest_ip = "192.168.0.100";
            byte[] b_port_dest_ip = IPAddress.Parse(port_dest_ip).GetAddressBytes();
            Array.Copy(b_port_dest_ip, 0, rawdata, writeIndex, b_port_dest_ip.Length);
            writeIndex += b_port_dest_ip.Length;
            //Console.WriteLine("b_port_dest_ip={0}", CCommondMethod.ToHex(b_port_dest_ip, "", " "));

            // port_dest_port
            byte[] bport_dest_port = new byte[2] { 0xd0, 0x07 };
            Array.Copy(bport_dest_port, 0, rawdata, writeIndex, bport_dest_port.Length);
            writeIndex += bport_dest_port.Length;
            //Console.WriteLine("bport_dest_port={0}", CCommondMethod.ToHex(bport_dest_port, "", " "));

            // port_baudrate
            int baudrate = 115200;
            rawdata[writeIndex++] = (byte)((baudrate >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((baudrate >> 24) & 0xff);
            // port_datasize
            rawdata[writeIndex++] = 0x08;
            // port_stopbit
            rawdata[writeIndex++] = 0x01;
            // port_parity
            rawdata[writeIndex++] = 0x04;
            // port_phy_disconnect
            rawdata[writeIndex++] = 0x00;
            // port_package_size
            int package_size = 1024;
            rawdata[writeIndex++] = (byte)((package_size >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((package_size >> 24) & 0xff);
            // port_package_timeout
            int package_timeout = 0;
            rawdata[writeIndex++] = (byte)((package_timeout >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((package_timeout >> 24) & 0xff);
            // connect_count
            rawdata[writeIndex++] = 0x00;
            // port_reset_ctrl
            rawdata[writeIndex++] = 0x00;
            // port_dns_enable
            rawdata[writeIndex++] = 0x00;
            // port_domain
            byte[] bport_domain = new byte[20];
            Array.Copy(bport_domain, 0, rawdata, writeIndex, bport_domain.Length);
            writeIndex += bport_domain.Length;
            // port_host_ip
            string port_host_ip = "0.0.0.0";
            byte[] b_port_host_ip = IPAddress.Parse(port_host_ip).GetAddressBytes();
            Array.Copy(b_port_host_ip, 0, rawdata, writeIndex, b_port_host_ip.Length);
            writeIndex += b_port_host_ip.Length;
            //Console.WriteLine("b_port_host_ip={0}", CCommondMethod.ToHex(b_port_host_ip, "", " "));

            // port_dns_port
            int port_dns_port = 0;
            rawdata[writeIndex++] = (byte)((port_dns_port >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port_dns_port >> 8) & 0xff);

            byte[] b_port_reserved = new byte[8];
            Array.Copy(b_port_reserved, 0, rawdata, writeIndex, b_port_reserved.Length);
            writeIndex += b_port_reserved.Length;
            //Console.WriteLine("b_port_reserved={0}", CCommondMethod.ToHex(b_port_reserved, "", " "));


            //DEVICEPORT_CONFIG dev_port_2 = new DEVICEPORT_CONFIG();
            // port_id
            rawdata[writeIndex++] = 0x01;
            // port_enable
            rawdata[writeIndex++] = 0x01;
            // port_net_mode
            rawdata[writeIndex++] = 0x00; // 0x00 TCP_Server
            // port_port_rand_enable
            rawdata[writeIndex++] = 0x00;
            // port_net_port
            byte[] bport2_net_port = new byte[2] { 0xA1, 0x0F }; // 4001
            Array.Copy(bport2_net_port, 0, rawdata, writeIndex, bport2_net_port.Length);
            writeIndex += bport2_net_port.Length;
            // port_dest_ip
            string port2_dest_ip = "192.168.0.101";
            byte[] b_port2_dest_ip = IPAddress.Parse(port2_dest_ip).GetAddressBytes();
            Array.Copy(b_port2_dest_ip, 0, rawdata, writeIndex, b_port2_dest_ip.Length);
            writeIndex += b_port2_dest_ip.Length;
            // port_dest_port
            byte[] bport2_dest_port = new byte[2] { 0xe8, 0x03 };
            Array.Copy(bport2_dest_port, 0, rawdata, writeIndex, bport2_dest_port.Length);
            writeIndex += bport2_dest_port.Length;
            // port_baudrate
            int port2_baudrate = 115200;
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_baudrate >> 24) & 0xff);
            // port_datasize
            rawdata[writeIndex++] = 0x08;
            // port_stopbit
            rawdata[writeIndex++] = 0x01;
            // port_parity
            rawdata[writeIndex++] = 0x04;
            /* PHY断开，Socket动作，1：关闭Socket 2、不动作*/
            // port_phy_disconnect
            rawdata[writeIndex++] = 0x00;
            // port_package_size
            int port2_package_size = 1024;
            rawdata[writeIndex++] = (byte)((port2_package_size >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_size >> 24) & 0xff);
            // port_package_timeout
            int port2_package_timeout = 0;
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 8) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 16) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_package_timeout >> 24) & 0xff);
            // connect_count
            rawdata[writeIndex++] = 0x00;
            // port_reset_ctrl
            rawdata[writeIndex++] = 0x00;
            // port_dns_enable
            rawdata[writeIndex++] = 0x00;
            // port_domain
            byte[] bport2_domain = new byte[20];
            Array.Copy(bport2_domain, 0, rawdata, writeIndex, bport2_domain.Length);
            writeIndex += bport2_domain.Length;
            // port_host_ip
            string port2_host_ip = "0.0.0.0";
            byte[] b_port2_host_ip = IPAddress.Parse(port2_host_ip).GetAddressBytes();
            Array.Copy(b_port2_host_ip, 0, rawdata, writeIndex, b_port2_host_ip.Length);
            writeIndex += b_port2_host_ip.Length;
            // port_dns_port
            int port2_dns_port = 0;
            rawdata[writeIndex++] = (byte)((port2_dns_port >> 0) & 0xff);
            rawdata[writeIndex++] = (byte)((port2_dns_port >> 8) & 0xff);

            byte[] b_port2_reserved = new byte[8];
            Array.Copy(b_port2_reserved, 0, rawdata, writeIndex, b_port2_reserved.Length);
            writeIndex += b_port2_reserved.Length;

            //NET_DEVICE_CONFIG dev_cfg = new NET_DEVICE_CONFIG();

            int len = writeIndex - 30; ;
            rawdata[lenIndex] = (byte)len;

            netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            int ret = netClient.Send(rawdata, rawdata.Length, netEndpoint);

            netCmdStarted = true;
            enableNetConfigUI(false);
        }

        private void NetResetCfg(string mod_mac)
        {
            if (!CheckNetConfigStatus())
                return;
            NET_COMM comm_cmd = new NET_COMM();
            byte[] ch9121_cfg_flag = System.Text.Encoding.Default.GetBytes(CH9121_CFG_FLAG);
            comm_cmd.setbytes(ch9121_cfg_flag);
            comm_cmd.setu8((byte)NET_CMD.NET_MODULE_CMD_RESET); // 设置cmd

            string param_mod_mac = mod_mac.Replace(":", "").ToLower();
            byte[] b_mod_mac = CCommondMethod.FromHex(param_mod_mac);
            comm_cmd.setbytes(b_mod_mac); // 设置mod_mac

            byte[] message = comm_cmd.Message;
            netEndpoint = new IPEndPoint(IPAddress.Parse("255.255.255.255"), 50000); // 目的地址信息
            int ret = netClient.Send(message, message.Length, netEndpoint);

            netCmdStarted = true;
            enableNetConfigUI(false);
        }

        private void LoadNetConfigViews()
        {
            net_port_1_net_mode_cbo.Items.Clear();
            net_port_1_net_mode_cbo.Items.AddRange(Enum.GetNames(typeof(MODULE_TYPE)));
            net_port_1_baudrate_cbo.Items.Clear();
            net_port_1_baudrate_cbo.Items.AddRange(Enum.GetNames(typeof(BAUDRATE)));
            net_port_1_databits_cbo.Items.Clear();
            net_port_1_databits_cbo.Items.AddRange(Enum.GetNames(typeof(DATABITS)));
            net_port_1_stopbits_cbo.Items.Clear();
            net_port_1_stopbits_cbo.Items.AddRange(Enum.GetNames(typeof(STOPBITS)));
            net_port_1_parity_bit_cbo.Items.Clear();
            net_port_1_parity_bit_cbo.Items.AddRange(Enum.GetNames(typeof(PARITY)));
        }

        private void NetRefreshNetCard()
        {
            net_card_combox.Items.Clear();
            net_card_dict.Clear();

            //SELECT* FROM Win32_NetworkAdapter where PhysicalAdapter = TRUE and MACAddress>‘’ //只查询有MAC的物理网卡，不包含虚拟网卡

            ManagementObjectSearcher s = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration where IPenabled=true");

            // ManagementObjectSearcher s = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapter");
            if (s.Get().Count == 0)
            {
                MessageBox.Show("没有找到网卡");
                return;
            }
            foreach (ManagementObject bb in s.Get())
            {
                PropertyDataCollection p = bb.Properties;
                //Console.WriteLine(String.Format("Description={0}", bb.GetPropertyValue("Description")));
                //Console.WriteLine(String.Format("DNSHostName={0}", bb.GetPropertyValue("DNSHostName")));
                //String[] ipAddr = (String[])bb.GetPropertyValue("IPAddress");
                //Console.WriteLine(String.Format("IPAddress={0}", String.Join(", ", ipAddr)));
                //String[] subNet = (String[])bb.GetPropertyValue("IPSubnet");
                //Console.WriteLine(String.Format("IPSubnet={0}", String.Join(", ", subNet)));
                //Console.WriteLine(String.Format("MACAddress={0}", bb.GetPropertyValue("MACAddress")));

                String desc = bb.GetPropertyValue("Description").ToString();
                //if (!desc.Contains("vmware") && !desc.Contains("virtual") &&
                //                    !desc.Contains("VMware") && !desc.Contains("Virtual"))
                {
                    String[] ipAddr = (String[])bb.GetPropertyValue("IPAddress");
                    String pcIpaddr = String.Join(", ", ipAddr, 0, (ipAddr.Length > 1 ? 1 : 0));
                    String[] subNet = (String[])bb.GetPropertyValue("IPSubnet");
                    String pcMask = String.Join("", subNet, 0, (subNet.Length > 1 ? 1 : 0));
                    String pcMac = bb.GetPropertyValue("MACAddress").ToString();
                    net_card_combox.Items.Add(desc);
                    NetCardSearch net_card_search = new NetCardSearch();
                    net_card_search.PC_IP = pcIpaddr;
                    net_card_search.PC_MAC = pcMac;
                    net_card_search.PC_MASK = pcMask;
                    if (!net_card_dict.ContainsKey(desc))
                    {
                        Console.WriteLine("add " + desc);
                        net_card_dict.Add(desc, net_card_search);
                    }
                }
            }

            if (net_card_combox.Items.Count > 0)
                net_card_combox.SelectedIndex = 0;
        }

        private void dev_dgv_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            string mod_mac = dev_dgv.Rows[e.RowIndex].Cells[ModMac.Name].Value.ToString();
            if (!net_db.IndexSearch.ContainsKey(mod_mac))
            {
                MessageBox.Show("不在列表!", "获取配置失败", MessageBoxButtons.OK);
                return;
            }
            NetGetCfg(net_db.IndexSearch[mod_mac]);
        }

        private void net_clear_btn_Click(object sender, EventArgs e)
        {
            BeginInvoke(new ThreadStart(delegate() {
                net_db.Clear();
                dev_dgv.Rows.Clear();
            }));
            clearDevCfgViews();
        }

        private void clearDevCfgViews()
        {
            BeginInvoke(new ThreadStart(delegate() {
                net_base_mod_mac_tb.Text = "";
                net_base_mod_name_tb.Text = "";
                net_base_dhcp_enable_cb.Checked = false;
                net_base_mod_ip_tb.Text = "";
                net_base_mod_mask_tb.Text = "";
                net_base_mod_gateway_tb.Text = "";

                net_port_1_enable_cb.Checked = false;
                net_port_1_rand_port_flag_cb.Checked = false;
                net_port_1_parity_bit_cbo.SelectedIndex =  -1;
                net_port_1_stopbits_cbo.SelectedIndex = -1;
                net_port_1_databits_cbo.SelectedIndex = -1;
                net_port_1_baudrate_cbo.SelectedIndex = -1;
                net_port_1_dest_port_tb.Text = "";
                net_port_1_dest_ip_tb.Text = "";
                // 启用域名
                net_port_1_local_net_port_tb.Text = "";
                net_port_1_net_mode_cbo.SelectedIndex = -1;

                net_heartbeat_interval_tb.Text = "";
                net_heartbeat_content_tb.Text = "";
                net_base_info_lb.Items.Clear();

                net_port_1_rx_pkg_size_tb.Text = "";
                net_port_1_rx_pkg_timeout_tb.Text = "";
                net_port_1_resetctrl_cb.Checked = false;
                net_port_1_reconnectcnt_tb.Text = "";
                net_port_1_dns_host_ip_tb.Text = "";
                net_port_1_dns_host_port_tb.Text = "";
                net_port_1_phyChangeHandle_cb.Checked = false;
                net_base_comcfgEn_cb.Checked = false;
            }));
        }

        private void net_card_combox_SelectedIndexChanged(object sender, EventArgs e)
        {
            string desc = net_card_combox.SelectedItem.ToString();
            cur_desc = desc;
            Console.WriteLine("net_card_combox_SelectedIndexChanged cur_desc-> " + cur_desc);
            if (net_card_dict.ContainsKey(desc))
            {
                net_pc_ip_label.Text = "Ip: " + net_card_dict[desc].PC_IP;
                net_pc_mac_label.Text = net_card_dict[desc].PC_MAC;
                net_pc_mask_label.Text = "Mask: " + net_card_dict[desc].PC_MASK;
            }
            StopNetUdpServer();
            if(netStarted && udpServerRunning)
                UpdateUdpServerStatus("stoping");

            clearDevCfgViews();
        }

        private void old_net_port_link_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://drive.263.net/link/41OTclS6USY4fTc/";
            Process.Start(url);
        }

        private void net_port_config_tool_linkLabel_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = "https://drive.263.net/link/cq8UK5i03uk1huN/";
            Process.Start(url);
        }

        private void net_port_net_mode_cb_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (net_port_1_net_mode_cbo.SelectedIndex == -1)
                return;
            //Console.WriteLine(" ... {0}", net_port_1_net_mode_cbo.SelectedItem.ToString());
            if (net_port_1_net_mode_cbo.SelectedItem.Equals(MODULE_TYPE.TCP_SERVER.ToString()))
            {
                EnablePort0_TcpClient(false);
            }
            else
            {
                EnablePort0_TcpClient(true);
            }
        }

        private void EnablePort0_TcpClient(bool flag)
        {
            //Console.WriteLine("EnablePort0_TCPClient ... ");
            // TcpClient 随机端口
            net_port_1_rand_port_flag_cb.Enabled = flag;
            // TcpClient 重连次数
            net_port_1_reconnectcnt_tb.Enabled = flag;
            // TcpClient 目标IP
            net_port_1_dest_ip_tb.Enabled = flag;
            // TcpClient 目标端口
            net_port_1_dest_port_tb.Enabled = flag;
            // TcpClient 域名启用
            net_port_1_dns_flag.Enabled = flag;
            net_port_1_dns_domain_tb.Enabled = flag;
            net_port_1_dns_host_ip_tb.Enabled = flag;
            net_port_1_dns_host_port_tb.Enabled = flag;
            //Console.WriteLine("EnablePort0_TCPClient end... ");
        }

        private void net_reset_default_Click(object sender, EventArgs e)
        {
            if (!CheckNetConfigStatus())
                StartNetUdpServer();
            string mod_mac = net_base_mod_mac_tb.Text;
            string pc_mac = net_pc_mac_label.Text.Replace(":", "").ToLower();

            if (!net_db.IndexNetDevCfg.ContainsKey(mod_mac))
            {
                MessageBox.Show("请先在列表中选择设备", "提示", MessageBoxButtons.OK);
                return;
            }

            if (!CheckMacAddr(mod_mac))
            {
                MessageBox.Show("Mod Mac地址格式错误, eg: 11:22:33:44:55:66", "Mac地址", MessageBoxButtons.OK);
                return;
            }
            if (!CheckMacAddr(mod_mac))
            {
                MessageBox.Show("Pc Mac地址格式错误, eg: 11:22:33:44:55:66", "Mac地址", MessageBoxButtons.OK);
                return;
            }
            NetSetDefaultCFG(mod_mac, pc_mac);
        }


        private void net_port_1_dns_flag_CheckStateChanged(object sender, EventArgs e)
        {
            Console.WriteLine("############### net_port_1_dns_flag_CheckedChanged {0}", sender.ToString());
            CheckBox cb = (CheckBox)sender;
            if (cb.Checked)
            {
                //if (!net_port_1_net_mode_cbo.SelectedItem.Equals(MODULE_TYPE.TCP_SERVER.ToString()))
                net_port_1_dns_domain_tb.Enabled = true;
            }
            else
            {
                net_port_1_dns_domain_tb.Enabled = false;
            }
        }

        private void net_save_cfg_btn_Click(object sender, EventArgs e)
        {
            string mod_mac = net_base_mod_mac_tb.Text.ToString();
            if(mod_mac.Trim().Length == 0)
            {
                MessageBox.Show("请先加载配置！");
                return;
            }
            //if(net_db.IndexNetDevCfg.ContainsKey(mod_mac))
            //{
            byte[] rawdata = NewNetComm();
            if (rawdata.Length < 285)
            {
                MessageBox.Show("请保存完整的配置文件！");
                return;
            }
            NET_COMM nc = new NET_COMM(rawdata);
                nc.UpdateMessage();
                saveNetCfg(nc.NetDevCfg);
            //}
            //else
            //{
            //    MessageBox.Show("没有需要保存配置文件的设备！");
            //}
        }

        private void saveNetCfg(NET_DEVICE_CONFIG cfg)
        {
            try
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "NetPortConfigure (.cfg)|*.cfg";
                saveFileDialog1.Title = "Select a File to save net port cfg";
                string strDestinationFile = "NetPortConfigure"
                    + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + @".cfg";
                saveFileDialog1.FileName = strDestinationFile;
                //saveFileDialog1.InitialDirectory = "D://";
                // Show the Dialog.
                // If the user clicked OK in the dialog and
                // a .txt file was selected, open it.
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                    writer.AutoFlush = true;
                    writer.WriteLine(CCommondMethod.ToHex(cfg.RawData, "", " "));
                    writer.Flush();
                    writer.Close();
                    MessageBox.Show("保存成功！");
                }
                else
                {
                    MessageBox.Show("未保存配置文件！");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Save NetCfg: " + ex.Message);
            }
        }

        private void net_load_cfg_btn_Click(object sender, EventArgs e)
        {
            
            //if (net_db.IndexNetDevCfg.ContainsKey(mod_mac))
            //{
            byte[] data = loadNetCfg();
            if (data == null)
            {
                MessageBox.Show("未加载配置文件!");
                return;
            }
            NetSetCfgForLoadCfg(data);
            MessageBox.Show("加载成功！!");
            //}
            //else
            //{
            //    MessageBox.Show("没有需要加载配置文件的设备！");
            //}
        }


        private byte[] loadNetCfg()
        {
            try
            {
                OpenFileDialog openLoadSaveConfigFileDialog = new OpenFileDialog();
                openLoadSaveConfigFileDialog.Filter = "NetPortConfigure (.cfg)|*.cfg";
                openLoadSaveConfigFileDialog.Title = "Select a configuration file to load";
                openLoadSaveConfigFileDialog.InitialDirectory = Directory.GetParent(System.Reflection.Assembly.GetExecutingAssembly().Location).ToString();
                openLoadSaveConfigFileDialog.RestoreDirectory = true;
                if (DialogResult.OK == openLoadSaveConfigFileDialog.ShowDialog())
                {
                    FileStream fs = new FileStream(openLoadSaveConfigFileDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                    StreamReader sr = new StreamReader(fs);   //选择编码方式
                    StringBuilder cfgStr = new StringBuilder();
                    while (sr.EndOfStream != true)
                    {
                        cfgStr.Append(sr.ReadLine());
                    }
                    //Console.WriteLine("cfgStr={0}", cfgStr.Replace(" ", ""));
                    
                    return CCommondMethod.FromHex(cfgStr.Replace(" ", "").ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Load NetCfg: " + ex.Message);
            }
            return null;
        }

        #endregion Net Configure

        #region Johar
        /////
        /// SEN_DATA[23:0] -> EPC 0x06, 0x07
        /// delta1 = User 0x08
        /// 芯片版本兼容数据 -> User 0x09
        /// 
        /// 1.获取原始数据
        /// 2.得到 SEN_DATA[23:0]
        /// 3.传感数据验证
        /// 4.获取校准参数 user 0x08
        /// 5.获取温度数据
        /// 
        bool joharReadingStarted = false;
        bool joharReading = false;
        JoharTagDB johardb = null;
        int joharCmdInterval = 100;

        private void johar_read_btn_Click(object sender, EventArgs e)
        {
            if (johar_read_btn.Text.Equals("开始"))
            {
                johar_read_btn.Text = "停止";
                if (johar_use_btn.Text.Equals("启用"))
                {
                    johar_use_btn.Text = "停用";
                    enableReadJohar(true);
                    johar_cb.Checked = true;
                    selectJohar();
                }
                joharCmdInterval = Convert.ToInt32(johar_cmd_interval_cb.SelectedItem.ToString());
                jorharEnableView(false);
                new Thread(new ThreadStart(readingJohar)).Start();
            }
            else if (johar_read_btn.Text.Equals("停止"))
            {
                joharReadingStarted = false;
                while(joharReading)
                {
                    Thread.Sleep(200);
                }
                jorharEnableView(true);
                johar_read_btn.Text = "开始";
            } 
        }

        private void jorharEnableView(bool enable)
        {
            johar_use_btn.Enabled = enable;
            johar_settings_gb.Enabled = enable;
            johar_cmd_interval_cb.Enabled = enable;
        }

        private void johar_use_btn_Click(object sender, EventArgs e)
        {
            if (johar_use_btn.Text.Equals("启用"))
            {
                johar_use_btn.Text = "停用";
                enableReadJohar(true);
                johar_cb.Checked = true;
                selectJohar();
            }
            else if (johar_use_btn.Text.Equals("停用"))
            {
                johar_use_btn.Text = "启用";
                enableReadJohar(false);
                johar_cb.Checked = false;
                clearSelect();
            }
        }

        private void johar_clear_btn_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate ()
            {
                johardb.Clear();
                johar_totalread_label.Text = johardb.TotalTagCount.ToString();
                johar_tagcount_label.Text = johardb.UniqueTagCount.ToString();
            }));
        }

        private void enableReadJohar(bool flag)
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x8C; // cmd 8D - 保存到flash

            // data
            if (true == flag)
            {
                rawData[writeIndex++] = 0x90; // TempSel
            }
            else
            {
                rawData[writeIndex++] = 0x00; // TempSel
            }

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // update len, except hdr+len
            Console.WriteLine("enableReadJohar writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            int nResult = reader.SendMessage(sendData);
        }

        private void selectJohar()
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = 0xFF;// m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x98; // cmd

            // data
            // function
            rawData[writeIndex++] = 0x01;
            // target
            rawData[writeIndex++] = 0x07;
            // action
            rawData[writeIndex++] = 0x04;
            // membank
            rawData[writeIndex++] = 0x01;
            // StartingMaskAdd
            rawData[writeIndex++] = 0x01;
            // MaskBitLen
            rawData[writeIndex++] = 0x01;
            // Mask
            rawData[writeIndex++] = 0x00;
            // Truncate
            rawData[writeIndex++] = 0x00;

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // update len, except hdr+len
            Console.WriteLine("selectJohar writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            int nResult = reader.SendMessage(sendData);
        }

        private void clearSelect()
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x98; // cmd

            // data
            rawData[writeIndex++] = 0x00;

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // update len, except hdr+len
            Console.WriteLine("clearSelect writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            int nResult = reader.SendMessage(sendData);
        }

        private void sendReadJoharMessage()
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x81; // cmd

            // data
            rawData[writeIndex++] = 0x00; // reserved
            rawData[writeIndex++] = 0x00;

            rawData[writeIndex++] = 0x00; // Tid
            rawData[writeIndex++] = 0x00;

            rawData[writeIndex++] = 0x08; // User 08, 09
            rawData[writeIndex++] = 0x02;

            rawData[writeIndex++] = 0x00; // Access Password
            rawData[writeIndex++] = 0x00;
            rawData[writeIndex++] = 0x00;
            rawData[writeIndex++] = 0x00;

            rawData[writeIndex++] = getJoharSession(); // session

            rawData[writeIndex++] = getJoharTarget(); // Target

            rawData[writeIndex++] = getJoharReadMode(); // ReadMode

            rawData[writeIndex++] = 0x05; // TimeOut default: 5ms

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // except hdr+len
            //Console.WriteLine("sendReadJoharMessage writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            int nResult = reader.SendMessage(sendData);
        }

        private void readingJohar()
        {
            joharReadingStarted = true;
            joharReading = true;
            while (joharReadingStarted)
            {
                sendReadJoharMessage();
                Thread.Sleep(joharCmdInterval<100?100:joharCmdInterval);
            }
            joharReading = false;
        }

        private void parseJoharRead(byte[] aryTranData)
        {
            this.BeginInvoke(new ThreadStart(delegate()
            {
                //Console.WriteLine("parseJoharRead totalread={0}, tagcount={1}", johardb.TotalTagCount, johardb.UniqueTagCount);
                JoharTag tag = new JoharTag(aryTranData);
                johardb.Add(tag);
                johar_totalread_label.Text = johardb.TotalTagCount.ToString();
                johar_tagcount_label.Text = johardb.UniqueTagCount.ToString();
            }));
        }

        private byte getJoharReadMode()
        {
            if (johar_readmode_mode1.Checked)
            {
                return 0x00;
            }
            else if (johar_readmode_mode2.Checked)
            {
                return 0x01;
            }
            else if (johar_readmode_mode3.Checked)
            {
                return 0x02;
            }
            return 0x02; // default Mode3
        }

        private byte getJoharTarget()
        {
            if (johar_target_A_rb.Checked)
            {
                return 0x00;
            }
            else if (johar_target_B_rb.Checked)
            {
                return 0x01;
            }
            return 0x00; // default A
        }

        private byte getJoharSession()
        {
            if (johar_session_s0_rb.Checked)
            {
                return 0x00;
            }
            else if (johar_session_s1_rb.Checked)
            {
                return 0x01;
            }
            else if (johar_session_s2_rb.Checked)
            {
                return 0x02;
            }
            else if (johar_session_s3_rb.Checked)
            {
                return 0x02;
            }
            return 0x01; // default s1
        }

        #endregion Johar

        #region FastInventory_8A_v2

        private void RefreshInventoryInfo()
        {
            if (inventory_times > 0)
                saveInventoryToLog(useAntG1, inventory_times);
            inventory_times++;
        }

        private void parseInvTag(bool readPhase, byte[] data, byte cmd)
        {
            BeginInvoke(new ThreadStart(delegate ()
            {
                lock (tagdb)
                {
                    Tag tag = new Tag(data, readPhase, cmd);
                    tagdb.Add(tag);
                    SetMaxMinRSSI(Convert.ToInt32(tag.Rssi));
                    txtFastMaxRssi.Text = tagdb.MaxRSSI + "dBm";
                    txtFastMinRssi.Text = tagdb.MinRSSI + "dBm";
                    led_totalread_count.Text = tagdb.TotalReadCounts.ToString(); //总读取次数（包含重复）
                    txtTotalTagCount.Text = tagdb.TotalTagCounts.ToString(); // 总读取标签数

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
            }));
        }

        private void cmdSwitchAntG1()
        {
            Console.WriteLine("切换到天线组1");
            useAntG1 = true;
            m_curSetting.btAntGroup = 0x00;
            cmdSwitchAntGroup(m_curSetting.btAntGroup);
        }

        private void cmdSwitchAntG2()
        {
            Console.WriteLine("切换到天线组2");
            useAntG1 = false;
            m_curSetting.btAntGroup = 0x01;
            cmdSwitchAntGroup(m_curSetting.btAntGroup);
        }

        private void cmdSwitchAntGroup(byte groupid)
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x6C; // cmd

            rawData[writeIndex++] = groupid; // groupId G1=0x00, g2=0x01

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // except hdr+len
            //Console.WriteLine("cmdSwitchAntGroup writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            //Console.WriteLine("cmdSwitchAntGroup: {0}", CCommondMethod.ToHex(sendData, "", " "));
            int nResult = reader.SendMessage(sendData);
            //Console.WriteLine("cmdSwitchAntGroup: [{0}] {1}", nResult, CCommondMethod.ToHex(sendData, "", " "));
        }

        private void GenerateColmnsDataGridForFastInv()
        {
            dgv_fast_inv_tags.AutoGenerateColumns = false;
            dgv_fast_inv_tags.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgv_fast_inv_tags.BackgroundColor = Color.White;

            SerialNumber_fast_inv.DataPropertyName = "SerialNumber";
            SerialNumber_fast_inv.HeaderText = "#";

            PC_fast_inv.DataPropertyName = "PC";
            PC_fast_inv.HeaderText = "PC";

            EPC_fast_inv.DataPropertyName = "EPC";
            EPC_fast_inv.HeaderText = "EPC";
            EPC_fast_inv.MinimumWidth = 120;
            EPC_fast_inv.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ReadCount_fast_inv.DataPropertyName = "ReadCount";
            //ReadCount_real_inv.HeaderText = "ReadCount";
            ReadCount_fast_inv.HeaderText = "识别次数";

            Rssi_fast_inv.DataPropertyName = "Rssi";
            Rssi_fast_inv.HeaderText = "Rssi(dBm)";

            Freq_fast_inv.DataPropertyName = "Freq";
            //Freq_fast_inv.HeaderText = "Freq(MHz)";
            Freq_fast_inv.HeaderText = "载波频率(MHz)";

            Phase_fast_inv.DataPropertyName = "Phase";
            Phase_fast_inv.HeaderText = "Phase";
            Phase_fast_inv.Visible = false;

            Antenna_fast_inv.DataPropertyName = "Antenna";
            Antenna_fast_inv.HeaderText = "Ant";

            Data_fast_inv.DataPropertyName = "Data";
            Data_fast_inv.HeaderText = "Data";
            Data_fast_inv.MinimumWidth = 120;
            Data_fast_inv.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            Data_fast_inv.Visible = false;
            //dgv_fast_inv_tags.DataSource = tagdb.TagList;
        }
        #endregion FastInventory_8A_v2

        private void cmdGetFrequencyRegion()
        {
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x79; // cmd

            // data
            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // except hdr+len
            //Console.WriteLine("cmdGetFrequencyRegion writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            Console.WriteLine("cmdGetFrequencyRegion: {0}", CCommondMethod.ToHex(sendData, "", " "));
            int nResult = reader.SendMessage(sendData);
        }

        private void R2000UartDemo_FormClosing(object sender, FormClosingEventArgs e)
        {
            #region FastInventory_8A_v2
            if (null != transportLogFile)
            {
                transportLogFile.Close();
            }
            #endregion FastInventory_8A_v2
        }

        private void saveLog()
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text Files (.txt)|*.txt";
            saveFileDialog1.Title = "Select a File to save transport layer logging";
            string strDestinationFile = "InventoryTesting-log"
                /*+ DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") */+ @".txt";
            saveFileDialog1.FileName = strDestinationFile;
            saveFileDialog1.InitialDirectory = Application.StartupPath;
            // Show the Dialog.
            // If the user clicked OK in the dialog and
            // a .txt file was selected, open it.
            if (true)//saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                StreamWriter writer = new StreamWriter(saveFileDialog1.FileName);
                writer.AutoFlush = true;
                transportLogFile = writer;
                // Todo: add callback
            }
            //if (null != transportLogFile)
            //{
            //    transportLogFile.Close();
            //}
        }

        private void saveInventoryToLog(bool useG1, uint times)
        {
            //Console.WriteLine("SaveInventoryToLog [G{0}] {1}", useG1 ? "1":"2", times);
            this.BeginInvoke(new ThreadStart(delegate ()
            {
                if (transportLogFile != null)
                {
                    lock (tagdb)
                    {
                        if (Inventorying)
                        {
                            transportLogFile.Write(String.Format("{0},{1}ms,{2},{3},[{4}], {5}",
                            DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"),
                            "第" + times + "次盘点耗时" + tagdb.CommandDuration,
                            "使用天线组" + (useG1 ? "1" : "2"),
                            (ReverseTarget ? ("使用Target" + (invTargetB ? "B," : "A,")) : ""),
                            tagdb.CmdTotalRead,
                            tagdb.CmdUniqueTagCount));

                            transportLogFile.WriteLine();
                            transportLogFile.Flush();
                        }
                        tagdb.CmdUniqueTagCount = 0;
                    }
                }
            }));
        }

        private void parseGetFrequencyRegion(byte[] data)
        {
            Console.WriteLine("parseGetFrequencyRegion: {0}", CCommondMethod.ToHex(data, "", " "));
            if (tagdb != null)
                tagdb.UpdateRegionInfo(data);
        }

        private void cb_fast_inv_check_all_ant_CheckedChanged(object sender, EventArgs e)
        {
            bool check = cb_fast_inv_check_all_ant.Checked;
            foreach (CheckBox cb in fast_inv_ants)
            {
                cb.Checked = check;
            }
        }

        private void btn_refresh_comports_Click(object sender, EventArgs e)
        {
            RefreshComPorts();
        }

        private void cb_fast_inv_reverse_target_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_fast_inv_reverse_target.Checked)
            {
                ReverseTarget = true;
                invTargetB = false;
                stayBTimes = Convert.ToInt32(tb_fast_inv_staytargetB_times.Text);
            }
            else
            {
                ReverseTarget = false;
            }
        }

        private void cb_use_selectFlags_tempPows_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_use_selectFlags_tempPows.Checked)
            {
                if (radio_btn_fast_inv.Checked)
                {
                    cb_use_optimize.Checked = false;
                    tb_fast_inv_reserved_5.Visible = false; // reserver 5 disable
                    grb_selectFlags.Visible = true;//SL

                    grb_temp_pow_ants_g1.Visible = true;
                    if(channels > 8)
                        grb_temp_pow_ants_g2.Visible = true;
                    for (int i = 0; i < channels; i++)
                    {
                        fast_inv_temp_pows[i].Enabled = true;
                    }
                }
                else
                {
                    grb_selectFlags.Visible = true;
                }
            }
            else
            {
                if (radio_btn_fast_inv.Checked)
                {
                    tb_fast_inv_reserved_5.Visible = true;
                    grb_selectFlags.Visible = false;//SL

                    grb_temp_pow_ants_g1.Visible = false;
                    grb_temp_pow_ants_g2.Visible = false;
                }
                else
                {
                    cb_use_powerSave.Checked = false;
                    grb_selectFlags.Visible = false;
                }
            }
        }

        private void cb_use_Phase_CheckedChanged(object sender, EventArgs e)
        {
            Phase_fast_inv.Visible = cb_use_Phase.Checked;
            if(radio_btn_realtime_inv.Checked && cb_use_Phase.Checked)
            {
                cb_use_selectFlags_tempPows.Checked = true;
            }
        }

        private void InventoryTypeChanged(object sender, EventArgs e)
        {
            //Console.WriteLine("InventoryTypeChanged channels={0}", channels);
            if(radio_btn_fast_inv.Checked)
            {
                grb_multi_ant.Visible = true;
                grb_cache_inv.Visible = false;
                grb_inventory_cfg.Visible = true;

                cb_use_powerSave.Checked = false;
                cb_use_powerSave.Visible = false;
                grb_real_inv_ants.Visible = false;

                grb_Interval.Visible = true;//Interval
                grb_Reserve.Visible = false;

                cb_customized_session_target.Checked = false; 
                cb_use_selectFlags_tempPows.Checked = false;
                cb_use_selectFlags_tempPows.Text = "无人零售配置";
                cb_use_selectFlags_tempPows.Visible = false;
                grb_selectFlags.Visible = false;//SL

                grb_sessions.Visible = false;//Session
                grb_targets.Visible = false;//Target
                grb_temp_pow_ants_g1.Visible = false;//Power
                grb_temp_pow_ants_g2.Visible = false;

                cb_use_powerSave.Checked = false;
                grb_powerSave.Visible = false;

                grb_ants_g1.Visible = true;//Antenna 
                if (channels > 8)
                {
                    grb_ants_g2.Visible = true;
                }
                else
                {
                    grb_ants_g2.Visible = false;
                }
                for(int i = 0; i < 16; i++)
                {
                    if(i < channels)
                    {
                        if(i==0)
                        {
                            fast_inv_ants[i].Checked = true;
                        }
                        fast_inv_ants[i].Visible = true;
                        fast_inv_stays[i].Visible = true;
                        fast_inv_temp_pows[i].Visible = true;
                    }
                    else
                    {
                        fast_inv_ants[i].Visible = false;
                        fast_inv_stays[i].Visible = false;
                        fast_inv_temp_pows[i].Visible = false;
                    }
                }

                cb_use_optimize.Checked = false;
                cb_use_optimize.Visible = false;
                grb_Optimize.Visible = false;
                grb_Ongoing.Visible = false;
                grb_TargetQuantity.Visible = false;

                cb_use_Phase.Checked = false;
                cb_use_Phase.Visible = false;//Phase
                grb_Repeat.Visible = true;//Repeat
                cb_customized_session_target_CheckedChanged(null, null);
            }
            else if(radio_btn_realtime_inv.Checked)
            {
                grb_multi_ant.Visible = false;
                grb_cache_inv.Visible = false;
                grb_inventory_cfg.Visible = true;

                grb_ants_g1.Visible = false;//Antenna
                grb_ants_g2.Visible = false;
                grb_temp_pow_ants_g1.Visible = false;//Power
                grb_temp_pow_ants_g2.Visible = false;

                grb_real_inv_ants.Visible = true;
                antLists.Clear();
                for (int i=1; i <= channels; i++)
                {
                    antLists.Add("天线" + i);
                }
                combo_realtime_inv_ants.Items.Clear();
                combo_realtime_inv_ants.Items.AddRange(antLists.ToArray());
                combo_realtime_inv_ants.SelectedIndex = 0;

                grb_Interval.Visible = false;//Interval
                grb_Reserve.Visible = false;

                cb_customized_session_target.Checked = false;
                cb_use_selectFlags_tempPows.Checked = false;
                cb_use_selectFlags_tempPows.Text = "SL";
                grb_selectFlags.Visible = false;

                grb_sessions.Visible = false;
                grb_targets.Visible = false;

                cb_use_powerSave.Checked = false;
                grb_powerSave.Visible = false;

                cb_use_optimize.Checked = false;
                cb_use_optimize.Visible = false;
                grb_Optimize.Visible = false;
                grb_Ongoing.Visible = false;
                grb_TargetQuantity.Visible = false;

                cb_use_Phase.Checked = false;
                cb_use_Phase.Visible = false;//Phase

                grb_Repeat.Visible = true;//Repeat
                cb_customized_session_target_CheckedChanged(null, null);
            }
            else if (radio_btn_cache_inv.Checked)
            {
                grb_multi_ant.Visible = false;
                grb_cache_inv.Visible = true;

                grb_real_inv_ants.Visible = true;
                antLists.Clear();
                for (int i = 1; i <= channels; i++)
                {
                    antLists.Add("天线" + i);
                }
                combo_realtime_inv_ants.Items.Clear();
                combo_realtime_inv_ants.Items.AddRange(antLists.ToArray());
                combo_realtime_inv_ants.SelectedIndex = 0;

                grb_inventory_cfg.Visible = false;
                grb_Interval.Visible = false;
                grb_Reserve.Visible = false;
                grb_selectFlags.Visible = false;
                grb_sessions.Visible = false;
                grb_targets.Visible = false;
                grb_Optimize.Visible = false;
                grb_Ongoing.Visible = false;
                grb_TargetQuantity.Visible = false;
                grb_powerSave.Visible = false;
                grb_Repeat.Visible = true;

                grb_ants_g1.Visible = false;
                grb_ants_g2.Visible = false;
                grb_temp_pow_ants_g1.Visible = false;
                grb_temp_pow_ants_g2.Visible = false;
            }
        }

        private void cb_use_powerSave_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_use_powerSave.Checked)
            {
                cb_use_selectFlags_tempPows.Checked = true;
                grb_powerSave.Visible = true;
            }
            else
            {
                grb_powerSave.Visible = false;
            }
        }

        private void cb_use_optimize_CheckedChanged(object sender, EventArgs e)
        {
            if(cb_use_optimize.Checked)
            {
                cb_use_selectFlags_tempPows.Checked = false;
                grb_Optimize.Visible = true;
                grb_Ongoing.Visible = true;
                grb_TargetQuantity.Visible = true;
            }
            else
            {
                grb_Optimize.Visible = false;
                grb_Ongoing.Visible = false;
                grb_TargetQuantity.Visible = false;
            }
        }

        private void btnInventory_Click_1(object sender, EventArgs e)
        {
            if(radio_btn_fast_inv.Checked)
            {
                FastInventory_Click(sender, e);
            }
            else if (radio_btn_realtime_inv.Checked)
            {
                RealTimeInventory_Click(sender, e);
            }
            else if (radio_btn_cache_inv.Checked)
            {
                CachedInventory_Click(sender, e);
            }
        }

        private void CachedInventory_Click(object sender, EventArgs e)
        {
            if (btnInventory.Text.Equals("开始盘存"))
            {
                if (Inventorying)
                {
                    MessageBox.Show("正在盘点...");
                    return;
                }

                try
                {
                    if (mInventoryExeCount.Text.Length == 0)
                    {
                        MessageBox.Show("请输入循环次数");
                        return;
                    }

                    btnInventory.BackColor = Color.DarkBlue;
                    btnInventory.ForeColor = Color.White;
                    btnInventory.Text = "停止盘存";

                    isBufferInv = true;
                    doingBufferInv = true;
                    Inventorying = true;
                    startInventoryTime = DateTime.Now;
                    dispatcherTimer.Start();
                    readratePerSecond.Start();
                    tagbufferCount = 0;
                    m_FastExeCount = Convert.ToInt32(mInventoryExeCount.Text);
                    startCachedInv();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (btnInventory.Text.Equals("停止盘存"))
            {
                SetInvStopingStatus();
                isBufferInv = false;
            }
        }

        private void startCachedInv()
        {
            int antId = combo_realtime_inv_ants.SelectedIndex;
            Console.WriteLine("startCachedInv 天线{0}, 天线组{1}", antId, m_curSetting.btAntGroup);
            if (antId >= 8)
            {
                m_curSetting.btWorkAntenna = (byte)(antId - 8);
                cmdSwitchAntG2();
            }
            else
            {
                useAntG1 = true;
                m_curSetting.btAntGroup = 0x00;
                m_curSetting.btWorkAntenna = (byte)antId;
                reader.SetWorkAntenna(m_curSetting.btReadId, m_curSetting.btWorkAntenna);
            }
            Console.WriteLine("## startCachedInv 天线{0:X2}, 天线组{1}", m_curSetting.btWorkAntenna, m_curSetting.btAntGroup);
        }

        private void RealTimeInventory_Click(object sender, EventArgs e)
        {
            if (btnInventory.Text.Equals("开始盘存"))
            {
                if (Inventorying)
                {
                    MessageBox.Show("正在盘点...");
                    return;
                }

                try
                {
                    if (mInventoryExeCount.Text.Length == 0)
                    {
                        MessageBox.Show("请输入循环次数");
                        return;
                    }

                    btnInventory.BackColor = Color.DarkBlue;
                    btnInventory.ForeColor = Color.White;
                    btnInventory.Text = "停止盘存";

                    isRealInv = true;
                    doingRealInv = true;
                    Inventorying = true;
                    startInventoryTime = DateTime.Now;
                    dispatcherTimer.Start();
                    readratePerSecond.Start();
                    m_FastExeCount = Convert.ToInt32(mInventoryExeCount.Text);
                    startRealtimeInv();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else if (btnInventory.Text.Equals("停止盘存"))
            {
                if (Inventorying)
                {
                    SetInvStopingStatus();
                    isRealInv = false;
                }
            }
        }

        private void btnGetBuffer_Click(object sender, EventArgs e)
        {
            if (btnGetBuffer.Text.Equals("获取缓存"))
            {
                btnGetBuffer.Text = "正在获取缓存";
                tagbufferCount = 0;
                needGetBuffer = true;
                startInventoryTime = DateTime.Now;
                dispatcherTimer.Start();
                readratePerSecond.Start();
                //Console.WriteLine("btnGetBuffer_Click startInventoryTime={0}", startInventoryTime.ToString("yyyy-MM-dd hh:mm:ss ffff"));
                cmdGetInventoryBuffer();
            }
            else if (btnGetBuffer.Text.Equals("正在获取缓存"))
            {
                stopGetInventoryBuffer(false);
            }
        }

        private void btnGetAndClearBuffer_Click(object sender, EventArgs e)
        {
            if (btnGetAndClearBuffer.Text.Equals("获取并清空缓存"))
            {
                btnGetAndClearBuffer.Text = "正在获取缓存";
                tagbufferCount = 0;
                needGetBuffer = true;
                startInventoryTime = DateTime.Now;
                dispatcherTimer.Start();
                readratePerSecond.Start();
                //Console.WriteLine("btnGetAndClearBuffer_Click startInventoryTime={0}", startInventoryTime.ToString("yyyy-MM-dd hh:mm:ss ffff"));
                cmdGetAndResetInventoryBuffer();
            }
            else if (btnGetAndClearBuffer.Text.Equals("正在获取缓存"))
            {
                stopGetInventoryBuffer(true);
            }
        }

        private void btnClearBuffer_Click(object sender, EventArgs e)
        {
            reader.ResetInventoryBuffer(m_curSetting.btReadId);
        }

        private void btnGetBufferTagCount_Click(object sender, EventArgs e)
        {
            reader.GetInventoryBufferTagCount(m_curSetting.btReadId);
        }
    }

    public enum NotifyType
    {
        StatusMessage,
        ErrorMessage
    };

    public class NetCardSearch
    {
        string pc_ip = String.Empty;
        string pc_mac = string.Empty;
        string pc_mask = String.Empty;
        public NetCardSearch()
        {
        }

        public string PC_IP { get { return pc_ip; } set { pc_ip = value; } }
        public string PC_MAC { get { return pc_mac; } set { pc_mac = value; } }
        public string PC_MASK { get { return pc_mask; } set { pc_mask = value; } }
    }
}
