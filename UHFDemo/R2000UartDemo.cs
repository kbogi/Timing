using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Threading;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.IO;
using System.IO.Ports;
using NPOI.Util;
using Reader;
using System.Net.Sockets;
using System.Diagnostics;
using System.Net.NetworkInformation;
using System.Management;
using System.Text.RegularExpressions;
using System.Windows.Threading;
using System.Collections;

namespace UHFDemo
{
    public partial class R2000UartDemo : Form
    {
        private Reader.ReaderMethod reader;

        private ReaderSetting m_curSetting = new ReaderSetting();
        private InventoryBuffer m_curInventoryBuffer = new InventoryBuffer();
        private OperateTagBuffer m_curOperateTagBuffer = new OperateTagBuffer();
        private OperateTagISO18000Buffer m_curOperateTagISO18000Buffer = new OperateTagISO18000Buffer();

        //盘存操作前，需要先设置工作天线，用于标识当前是否在执行盘存操作
        private bool m_bInventory = false;
        //标识是否统计命令执行时间，当前仅盘存命令需要统计时间
        private bool m_bReckonTime = false;
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
        //实时盘存次数
        private int m_nTotal = 0;
        //列表更新频率
        private int m_nRealRate = 20;
        //记录快速轮询天线参数
        private byte[] m_btAryData = new byte[18];
        //记录快速轮询第二组天线参数
        private byte[] m_btAryData_group2 = new byte[18];
        //4 ant
        private byte[] m_btAryData_4 = new byte[10];
        //记录快速轮询总次数
        private int m_nSwitchTotal = 0;
        private int m_nSwitchTime = 0;

        private int m_nReceiveFlag = 0;

        //A,B Inventory Count
        private int m_AStatusInventoryCount;
        private int m_BStatusInventoryCount;

        private DateTime m_InventoryStarTime;
        private volatile int m_ConsumTime;

        private volatile bool m_runLoopTest = false;
        private volatile int m_intervalTimeTest;
        private FileInfo m_FileInfo;
        private String m_FilePath;

        private int m_writeCount = 0;
        private int m_TestTagCount;
        private int m_ErrorCount = 0;
        private int m_ErrorTagCount = 0;

        private DateTime m_startConsumTime;


        private int m_FastInterval;
        private int m_FastExeCount;
        private int m_FastCountStart;

        private volatile int m_FastSessionPowerInternal;
        private volatile int m_FastSessionCount;

        private volatile int m_NewExeTimer;

        private volatile bool m_nPhaseOpened = false;

        private volatile bool m_nRepeat1 = false;
        private volatile bool m_nRepeat2 = false;

        private volatile bool m_nRepeat12 = false;

        private volatile bool m_nIsFastEnd = false;

        private bool m_getOutputPower = false;
        private bool m_setOutputPower = false;
        private bool m_setWorkAnt = false;
        private bool m_getWorkAnt = false;

        private CheckBox[] fast_inv_ants = null;
        private TextBox[] fast_inv_stays = null;

        public R2000UartDemo()
        {
            InitializeComponent();
            this.Text = string.Format("{0}{1}.{2}",
                "UHF RFID Reader Demo v",
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major,
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Minor);

            this.DoubleBuffered = true;


            lvRealList.SmallImageList = sortImageList;
            lvFastList.SmallImageList = sortImageList;
            lvBufferList.SmallImageList = sortImageList;

            this.columnHeader37.ImageIndex = 0;
            this.columnHeader38.ImageIndex = 0;

            this.columnHeader31.ImageIndex = 0;
            this.columnHeader32.ImageIndex = 0;

            this.columnHeader49.ImageIndex = 0;
            this.columnHeader52.ImageIndex = 0;

            this.refreshFastListView();
            this.m_new_fast_inventory_session.SelectedIndex = 1;
            this.m_new_fast_inventory_flag.SelectedIndex = 0;

            this.columnHeader37.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;

            cmbx_realinv_workant.Items.AddRange(new string[] { "天线1", "天线2", "天线3", "天线4", "天线5", "天线6", "天线7", "天线8", "天线9", "天线10", "天线11", "天线12", "天线13", "天线14", "天线15", "天线16" });
            cmbx_realinv_workant.SelectedIndex = 0;

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

            antChkbs = new CheckBox[] {
                cb_fast_inv_v2_ant_1, cb_fast_inv_v2_ant_2, cb_fast_inv_v2_ant_3, cb_fast_inv_v2_ant_4,
                cb_fast_inv_v2_ant_5, cb_fast_inv_v2_ant_6, cb_fast_inv_v2_ant_7, cb_fast_inv_v2_ant_8,
                cb_fast_inv_v2_ant_9, cb_fast_inv_v2_ant_10, cb_fast_inv_v2_ant_11, cb_fast_inv_v2_ant_12,
                cb_fast_inv_v2_ant_13, cb_fast_inv_v2_ant_14, cb_fast_inv_v2_ant_15, cb_fast_inv_v2_ant_16
            };
            antStays = new TextBox[] {
                tb_fast_inv_v2_stay_1, tb_fast_inv_v2_stay_2, tb_fast_inv_v2_stay_3, tb_fast_inv_v2_stay_4,
                tb_fast_inv_v2_stay_5, tb_fast_inv_v2_stay_6, tb_fast_inv_v2_stay_7, tb_fast_inv_v2_stay_8,
                tb_fast_inv_v2_stay_9, tb_fast_inv_v2_stay_10, tb_fast_inv_v2_stay_11, tb_fast_inv_v2_stay_12,
                tb_fast_inv_v2_stay_13, tb_fast_inv_v2_stay_14, tb_fast_inv_v2_stay_15, tb_fast_inv_v2_stay_16
            };
            antPowers = new TextBox[] {
                tb_fast_inv_v2_power_1, tb_fast_inv_v2_power_2, tb_fast_inv_v2_power_3, tb_fast_inv_v2_power_4,
                tb_fast_inv_v2_power_5, tb_fast_inv_v2_power_6, tb_fast_inv_v2_power_7, tb_fast_inv_v2_power_8,
                tb_fast_inv_v2_power_9, tb_fast_inv_v2_power_10, tb_fast_inv_v2_power_11, tb_fast_inv_v2_power_12,
                tb_fast_inv_v2_power_13, tb_fast_inv_v2_power_14, tb_fast_inv_v2_power_15, tb_fast_inv_v2_power_16
            };
        }

        private void R2000UartDemo_Load(object sender, EventArgs e)
        {
            //初始化访问读写器实例
            reader = new Reader.ReaderMethod();

            //回调函数
            reader.AnalyCallback = AnalyData;
            reader.ReceiveCallback = ReceiveData;
            reader.SendCallback = SendData;
            reader.TcpErrCallback = TcpExcption;

            //设置界面元素有效性
            gbRS232.Enabled = false;
            gbTcpIp.Enabled = false;
            SetFormEnable(false);
            rdbRS232.Checked = true;
            antType4.Checked = true;



            //初始化连接读写器默认配置
            string[] portNames = SerialPort.GetPortNames();
            if (portNames != null && portNames.Length > 0)
            {
                cmbComPort.Items.AddRange(portNames);
                cmbComPort.SelectedIndex = cmbComPort.Items.Count - 1;
            }
            cmbBaudrate.SelectedIndex = 1;
            ipIpServer.IpAddressStr = "192.168.0.178";
            txtTcpPort.Text = "4001";


            comboBox12.SelectedIndex = 0;
            comboBox13.SelectedIndex = 1;
            comboBox14.SelectedIndex = 0;
            comboBox15.SelectedIndex = 0;
            comboBox16.SelectedIndex = 0;

            m_session_sl.SelectedIndex = 0;


            rdbInventoryRealTag_CheckedChanged(sender, e);
            cmbSession.SelectedIndex = 0;
            cmbTarget.SelectedIndex = 0;
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

            //init session time 
            //string time = string.Empty;
            for (int nloop = 0; nloop < 256; nloop++)
            {
                string strTemp = (nloop / 10.0).ToString("f1") + "s";
                this.mSessionExeTime.Items.Add(strTemp);
                //this.mSessionExeTime.Items.Add(strTemp);
            }
            this.mSessionExeTime.SelectedIndex = 0;


            //ListView settings
            this.lvRealList.ListViewItemSorter = new ListViewColumnSorter();
            ListViewHelper lvRealHelper = new ListViewHelper();
            lvRealHelper.addSortColumn(0);
            lvRealHelper.addSortColumn(1);
            this.lvRealList.ColumnClick += new ColumnClickEventHandler(lvRealHelper.ListView_ColumnClick);

            this.lvBufferList.ListViewItemSorter = new ListViewColumnSorter();
            ListViewHelper lvBufferHelper = new ListViewHelper();
            lvBufferHelper.addSortColumn(0);
            lvBufferHelper.addSortColumn(3);
            this.lvBufferList.ColumnClick += new ColumnClickEventHandler(lvBufferHelper.ListView_ColumnClick);

            this.lvFastList.ListViewItemSorter = new ListViewColumnSorter();
            ListViewHelper lvFastHelper = new ListViewHelper();
            lvFastHelper.addSortColumn(0);
            lvFastHelper.addSortColumn(1);
            this.lvFastList.ColumnClick += new ColumnClickEventHandler(lvFastHelper.ListView_ColumnClick);


            //build file
            m_FilePath = Application.StartupPath + "\\testlog.txt";
            m_FileInfo = new FileInfo(m_FilePath);
            if (!m_FileInfo.Exists)
            {
                m_FileInfo.Create();
            }
            else
            {
                StreamWriter write = new StreamWriter(m_FilePath);
                write.Write("");
                write.Flush();
                write.Dispose();
            }

            this.mFastSessionTimer.Tick += new EventHandler(this.mFastSessionTimer_Tick);

            this.mSendFastSwitchTimer.Tick += new EventHandler(this.SendFastSwitchTimer_Tick);

            //mFastSessionSelect.SelectedIndex = 0;

            #region FastInventory_8A_v2
            fastInventoryLoadCfg();
            GenerateColmnsForDataGrid();
            fast_inv_v2_sl_gb.Visible = false;
            fast_inv_v2_cb.Visible = false;
            tagdb = new TagDB();
            tags_dgv.DataSource = tagdb.TagList;
            #endregion FastInventory_8A_v2

            autoInventoryrb.Visible = false;
        }

        private void TcpExcption(string strErr)
        {
            WriteLog(lrtxtLog, strErr, 1);
            if (strErr.Contains("重连成功") && !btFastInventory.Text.Equals("开始盘存"))
            {
                BeginInvoke(new ThreadStart(delegate ()
                {
                    btFastInventory_Click(null, null);
                    btFastInventory_Click(null, null);
                }));
            }
        }

        private void ReceiveData(byte[] btAryReceiveData)
        {

            if (m_bDisplayLog)
            {
                string strLog = CCommondMethod.ByteArrayToString(btAryReceiveData, 0, btAryReceiveData.Length);

                WriteLog(lrtxtDataTran, strLog, 1);
            }
        }

        private void SendData(byte[] btArySendData)
        {
            if (m_bDisplayLog)
            {
                string strLog = CCommondMethod.ByteArrayToString(btArySendData, 0, btArySendData.Length);

                WriteLog(lrtxtDataTran, strLog, 0);
            }
        }

        private void AnalyData(Reader.MessageTran msgTran)
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
                    RunLoopFastSwitch();
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

        private delegate void RefreshInventoryUnsafe(byte btCmd);
        private void RefreshInventory(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshInventoryUnsafe InvokeRefresh = new RefreshInventoryUnsafe(RefreshInventory);
                this.Invoke(InvokeRefresh, new object[] { btCmd });
            }
            else
            {
                switch (btCmd)
                {
                    case 0x80:
                        {
                            ledBuffer1.Text = m_curInventoryBuffer.nTagCount.ToString();
                            ledBuffer2.Text = m_curInventoryBuffer.nReadRate.ToString();

                            TimeSpan ts = m_curInventoryBuffer.dtEndInventory - m_curInventoryBuffer.dtStartInventory;
                            ledBuffer5.Text = FormatLongToTimeStr(ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds);
                            int nTotalRead = 0;
                            foreach (int nTemp in m_curInventoryBuffer.lTotalRead)
                            {
                                nTotalRead += nTemp;
                            }
                            ledBuffer4.Text = nTotalRead.ToString();
                            int commandDuration = 0;
                            if (m_curInventoryBuffer.nReadRate > 0)
                            {
                                commandDuration = m_curInventoryBuffer.nDataCount * 1000 / m_curInventoryBuffer.nReadRate;
                            }
                            ledBuffer3.Text = commandDuration.ToString();
                            int currentAntDisplay = 0;
                            currentAntDisplay = m_curInventoryBuffer.nCurrentAnt + 1;

                        }
                        break;
                    case 0x90:
                    case 0x91:
                        {
                            int nCount = lvBufferList.Items.Count;
                            int nLength = m_curInventoryBuffer.dtTagTable.Rows.Count;
                            DataRow row = m_curInventoryBuffer.dtTagTable.Rows[nLength - 1];

                            ListViewItem item = new ListViewItem();
                            item.Text = (nCount + 1).ToString();
                            item.SubItems.Add(row[0].ToString());
                            item.SubItems.Add(row[1].ToString());
                            item.SubItems.Add(row[2].ToString());
                            item.SubItems.Add(row[3].ToString());

                            string strTemp = (Convert.ToInt32(row[4].ToString()) - 129).ToString() + "dBm";
                            item.SubItems.Add(strTemp);
                            byte byTemp = Convert.ToByte(row[4]);
                            /*   if (byTemp > 0x50)
                               {
                                   item.BackColor = Color.PowderBlue;
                               }
                               else if (byTemp < 0x30)
                               {
                                   item.BackColor = Color.LemonChiffon;
                               } */

                            item.SubItems.Add(row[5].ToString());

                            lvBufferList.Items.Add(item);
                            //lvBufferList.Items[nCount].EnsureVisible();

                            labelBufferTagCount.Text = "标签列表： " + m_curInventoryBuffer.nTagCount.ToString() + "个";

                        }
                        break;
                    case 0x92:
                        {

                        }
                        break;
                    case 0x93:
                        {

                        }
                        break;
                    default:
                        break;
                }
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

        private delegate void RefreshInventoryRealUnsafe(byte btCmd);
        private void RefreshInventoryReal(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshInventoryRealUnsafe InvokeRefresh = new RefreshInventoryRealUnsafe(RefreshInventoryReal);
                this.Invoke(InvokeRefresh, new object[] { btCmd });
            }
            else
            {
                switch (btCmd)
                {
                    case 0x89:
                    case 0x8B:
                        {
                            int nTagCount = m_curInventoryBuffer.dtTagTable.Rows.Count;
                            int nTotalRead = m_nTotal;// m_curInventoryBuffer.dtTagDetailTable.Rows.Count;
                            TimeSpan ts = m_curInventoryBuffer.dtEndInventory - m_curInventoryBuffer.dtStartInventory;
                            int nTotalTime = (int)(ts.Ticks / 10000);
                            int nCaculatedReadRate = 0;
                            int nCommandDuation = 0;

                            if (m_curInventoryBuffer.nReadRate == 0) //读写器没有返回速度前软件测速度
                            {
                                if (nTotalTime > 0)
                                {
                                    nCaculatedReadRate = (nTotalRead * 1000 / nTotalTime);
                                }
                            }
                            else
                            {
                                nCommandDuation = m_curInventoryBuffer.nDataCount * 1000 / m_curInventoryBuffer.nReadRate;
                                nCaculatedReadRate = m_curInventoryBuffer.nReadRate;
                            }

                            //列表用变量
                            int nEpcCount = 0;
                            int nEpcLength = m_curInventoryBuffer.dtTagTable.Rows.Count;

                            ledReal1.Text = nTagCount.ToString();
                            ledReal2.Text = nCaculatedReadRate.ToString();

                            ledReal5.Text = FormatLongToTimeStr(nTotalTime);
                            ledReal3.Text = nTotalRead.ToString();
                            ledReal4.Text = nCommandDuation.ToString();  //实际的命令执行时间
                            tbRealMaxRssi.Text = (m_curInventoryBuffer.nMaxRSSI - 129).ToString() + "dBm";
                            tbRealMinRssi.Text = (m_curInventoryBuffer.nMinRSSI - 129).ToString() + "dBm";
                            lbRealTagCount.Text = "标签EPC号列表（不重复）： " + nTagCount.ToString() + "个";

                            nEpcCount = lvRealList.Items.Count;


                            if (nEpcCount < nEpcLength)
                            {
                                DataRow row = m_curInventoryBuffer.dtTagTable.Rows[nEpcLength - 1];

                                ListViewItem item = new ListViewItem();
                                item.Text = (nEpcCount + 1).ToString();
                                item.SubItems.Add(row[2].ToString());
                                item.SubItems.Add(row[0].ToString());
                                //item.SubItems.Add(row[5].ToString());
                                if (antType16.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "/" + row[8].ToString() + "/" + row[9].ToString() + "/" + row[10].ToString() + "/"
                                    + row[11].ToString() + "/" + row[12].ToString() + "/" + row[13].ToString() + "/" + row[14].ToString() + "/"
                                    + row[15].ToString() + "/" + row[16].ToString() + "/" + row[17].ToString() + "/" + row[18].ToString() + "/"
                                    + row[19].ToString() + "/" + row[20].ToString() + "/" + row[21].ToString() + "/" + row[22].ToString());
                                }
                                else if (antType8.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10] + "  /  "
                                    + row[11].ToString() + "  /  " + row[12].ToString() + "  /  " + row[13].ToString() + "  /  " + row[14]);
                                }
                                else if (antType4.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10]);
                                }
                                else if (antType1.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString());
                                }
                                item.SubItems.Add((Convert.ToInt32(row[4]) - 129).ToString() + "dBm");
                                item.SubItems.Add(row[6].ToString());

                                //set Item backagroud color.
                                //item.BackColor = Color.Red;

                                lvRealList.Items.Add(item);

                                //do not location the scrolling bar in the bottom.
                                //lvRealList.Items[nEpcCount].EnsureVisible();
                            }

                            //else
                            //{
                            //    int nIndex = 0;
                            //    foreach (DataRow row in m_curInventoryBuffer.dtTagTable.Rows)
                            //    {
                            //        ListViewItem item = ltvInventoryEpc.Items[nIndex];
                            //        item.SubItems[3].Text = row[5].ToString();
                            //        nIndex++;
                            //    }
                            //}

                            //更新列表中读取的次数
                            if (m_nTotal % m_nRealRate == 1)
                            {
                                int nIndex = 0;
                                foreach (DataRow row in m_curInventoryBuffer.dtTagTable.Rows)
                                {
                                    ListViewItem item;
                                    item = lvRealList.Items[nIndex];
                                    //item.SubItems[3].Text = row[5].ToString();
                                    if (antType16.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "/" + row[8].ToString() + "/" + row[9].ToString() + "/" + row[10].ToString() + "/"
                                    + row[11].ToString() + "/" + row[12].ToString() + "/" + row[13].ToString() + "/" + row[14].ToString() + "/"
                                    + row[15].ToString() + "/" + row[16].ToString() + "/" + row[17].ToString() + "/" + row[18].ToString() + "/"
                                    + row[19].ToString() + "/" + row[20].ToString() + "/" + row[21].ToString() + "/" + row[22].ToString());
                                    }
                                    else if (antType8.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10] + "  /  "
                                       + row[11].ToString() + "  /  " + row[12].ToString() + "  /  " + row[13].ToString() + "  /  " + row[14]);
                                    }
                                    else if (antType4.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10]);
                                    }
                                    else if (antType1.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString());
                                    }
                                    item.SubItems[4].Text = (Convert.ToInt32(row[4]) - 129).ToString() + "dBm";
                                    item.SubItems[5].Text = row[6].ToString();

                                    nIndex++;
                                }
                            }

                            //if (ltvInventoryEpc.SelectedIndices.Count != 0)
                            //{
                            //    int nDetailCount = ltvInventoryTag.Items.Count;
                            //    int nDetailLength = m_curInventoryBuffer.dtTagDetailTable.Rows.Count;

                            //    foreach (int nIndex in ltvInventoryEpc.SelectedIndices)
                            //    {
                            //        ListViewItem itemEpc = ltvInventoryEpc.Items[nIndex];
                            //        DataRow row = m_curInventoryBuffer.dtTagDetailTable.Rows[nDetailLength - 1];
                            //        if (itemEpc.SubItems[1].Text == row[0].ToString())
                            //        {
                            //            ListViewItem item = new ListViewItem();
                            //            item.Text = (nDetailCount + 1).ToString();
                            //            item.SubItems.Add(row[0].ToString());

                            //            string strTemp = (Convert.ToInt32(row[1].ToString()) - 129).ToString() + "dBm";
                            //            item.SubItems.Add(strTemp);
                            //            byte byTemp = Convert.ToByte(row[1]);
                            //            if (byTemp > 0x50)
                            //            {
                            //                item.BackColor = Color.PowderBlue;
                            //            }
                            //            else if (byTemp < 0x30)
                            //            {
                            //                item.BackColor = Color.LemonChiffon;
                            //            }

                            //            item.SubItems.Add(row[2].ToString());
                            //            item.SubItems.Add(row[3].ToString());

                            //            ltvInventoryTag.Items.Add(item);
                            //            ltvInventoryTag.Items[nDetailCount].EnsureVisible();
                            //        }
                            //    }
                            //}
                            //else
                            //{
                            //    int nDetailCount = ltvInventoryTag.Items.Count;
                            //    int nDetailLength = m_curInventoryBuffer.dtTagDetailTable.Rows.Count;

                            //    DataRow row = m_curInventoryBuffer.dtTagDetailTable.Rows[nDetailLength - 1];
                            //    ListViewItem item = new ListViewItem();
                            //    item.Text = (nDetailCount + 1).ToString();
                            //    item.SubItems.Add(row[0].ToString());

                            //    string strTemp = (Convert.ToInt32(row[1].ToString()) - 129).ToString() + "dBm";
                            //    item.SubItems.Add(strTemp);
                            //    byte byTemp = Convert.ToByte(row[1]);
                            //    if (byTemp > 0x50)
                            //    {
                            //        item.BackColor = Color.PowderBlue;
                            //    }
                            //    else if (byTemp < 0x30)
                            //    {
                            //        item.BackColor = Color.LemonChiffon;
                            //    }

                            //    item.SubItems.Add(row[2].ToString());
                            //    item.SubItems.Add(row[3].ToString());

                            //    ltvInventoryTag.Items.Add(item);
                            //    ltvInventoryTag.Items[nDetailCount].EnsureVisible();
                            //}


                        }
                        break;


                    case 0x00:
                    case 0x01:
                        {
                            m_bLockTab = false;

                            //TimeSpan ts = m_curInventoryBuffer.dtEndInventory - m_curInventoryBuffer.dtStartInventory;
                            //int nTotalTime = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;

                            //ledReal5.Text = nTotalTime.ToString();

                        }
                        break;
                    case 0x02:
                        m_curInventoryBuffer.dtEndInventory = DateTime.Now;
                        TimeSpan tsp = DateTime.Now - m_InventoryStarTime;
                        int consume = tsp.Minutes * 60 * 1000 + tsp.Seconds * 1000 + tsp.Milliseconds;

                        if ((this.m_ConsumTime != 0) && (this.m_ConsumTime < consume))
                        {
                            //RefreshInventoryReal(0x02);
                            m_bInventory = false;
                            m_curInventoryBuffer.bLoopInventory = false;
                            m_curInventoryBuffer.bLoopInventoryReal = false;
                            btRealTimeInventory.BackColor = Color.WhiteSmoke;
                            btRealTimeInventory.ForeColor = Color.DarkBlue;
                            btRealTimeInventory.Text = "开始盘存";
                            timerInventory.Enabled = false;
                            totalTime.Enabled = false;
                            //return;
                        }

                        break;
                    default:
                        break;
                }
            }
        }



        private delegate void RefreshFastSwitchUnsafe(byte btCmd);
        private void RefreshFastSwitch(byte btCmd)
        {
            if (this.InvokeRequired)
            {
                RefreshFastSwitchUnsafe InvokeRefreshFastSwitch = new RefreshFastSwitchUnsafe(RefreshFastSwitch);
                this.Invoke(InvokeRefreshFastSwitch, new object[] { btCmd });
            }
            else
            {
                switch (btCmd)
                {
                    case 0x00:
                        {
                            int nTagCount = m_curInventoryBuffer.dtTagTable.Rows.Count;
                            int nTotalRead = m_nTotal;// m_curInventoryBuffer.dtTagDetailTable.Rows.Count;
                            TimeSpan ts = m_curInventoryBuffer.dtEndInventory - m_curInventoryBuffer.dtStartInventory;
                            int nTotalTime = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;

                            ledFast1.Text = nTagCount.ToString(); //标签总数
                            if (m_curInventoryBuffer.nCommandDuration > 0)
                            {
                                ledFast2.Text = (m_curInventoryBuffer.nDataCount * 1000 / m_curInventoryBuffer.nCommandDuration).ToString(); //读标签速度
                            }
                            else
                            {
                                ledFast2.Text = "0";
                            }

                            if (m_nIsFastEnd && !m_nRepeat12 && this.mDynamicPoll.Checked)
                            {
                                this.m_NewExeTimer += m_curInventoryBuffer.nCommandDuration;

                                Console.WriteLine("execute time:" + this.m_NewExeTimer.ToString());
                            }
                            else if (m_nIsFastEnd && m_nRepeat12 && this.mDynamicPoll.Checked)
                            {
                                this.m_NewExeTimer += m_curInventoryBuffer.nCommandDuration;
                                //this.m_NewExeTimer += Convert.ToInt32(this.m_fast_session_power_time.Text);
                                ledFast3.Text = this.m_NewExeTimer.ToString(); //命令执行时间
                                this.m_NewExeTimer = 0;
                                m_curInventoryBuffer.nCommandDuration = 0;
                                Console.WriteLine("execute time:" + this.m_NewExeTimer.ToString());
                            }
                            else
                            {
                                ledFast3.Text = m_curInventoryBuffer.nCommandDuration.ToString(); //命令执行时间
                            }



                            //ledFast5.Text = nTotalTime.ToString(); //命令累计执行时间
                            ledFast5.Text = FormatLongToTimeStr(nTotalTime);
                            ledFast4.Text = nTotalRead.ToString();

                            txtFastMaxRssi.Text = (m_curInventoryBuffer.nMaxRSSI - 129).ToString() + "dBm";
                            txtFastMinRssi.Text = (m_curInventoryBuffer.nMinRSSI - 129).ToString() + "dBm";
                            txtFastTagList.Text = "标签EPC号列表（不重复）： " + nTagCount.ToString() + "个";


                            //DataRow[] drss = m_curInventoryBuffer.dtTagTable.Select();
                            //Console.WriteLine("=================开始打印=================");
                            //for (int i = 0; i < drss.Length; i++)
                            //{
                            //    Console.WriteLine("EPC: {0}   ---     次数: {1}", drss[i][2], drss[i][7]);
                            //}
                            //Console.WriteLine("-----------------结束打印-----------------");

                            //形成列表
                            int nEpcCount = lvFastList.Items.Count;
                            int nEpcLength = m_curInventoryBuffer.dtTagTable.Rows.Count;
                            //Console.WriteLine("nEpcCount: {0}   ===nEpcLength:{1}", nEpcCount, nEpcLength);
                            if (nEpcCount < nEpcLength)
                            {
                                DataRow row = m_curInventoryBuffer.dtTagTable.Rows[nEpcLength - 1];

                                ListViewItem item = new ListViewItem();
                                item.Text = (nEpcCount + 1).ToString();
                                item.SubItems.Add(row[2].ToString());
                                item.SubItems.Add(row[0].ToString());
                                //item.SubItems.Add(row[5].ToString());
                                if (antType16.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "/" + row[8].ToString() + "/" + row[9].ToString() + "/" + row[10].ToString() + "/"
                                    + row[11].ToString() + "/" + row[12].ToString() + "/" + row[13].ToString() + "/" + row[14].ToString() + "/"
                                    + row[15].ToString() + "/" + row[16].ToString() + "/" + row[17].ToString() + "/" + row[18].ToString() + "/"
                                    + row[19].ToString() + "/" + row[20].ToString() + "/" + row[21].ToString() + "/" + row[22].ToString());
                                }
                                else if (antType8.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10] + "  /  "
                                    + row[11].ToString() + "  /  " + row[12].ToString() + "  /  " + row[13].ToString() + "  /  " + row[14]);
                                }
                                else if (antType4.Checked)
                                {
                                    item.SubItems.Add(row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10]);
                                }
                                item.SubItems.Add((Convert.ToInt32(row[4]) - 129).ToString() + "dBm");
                                item.SubItems.Add(row[23].ToString());
                                item.SubItems.Add(row[6].ToString());

                                lvFastList.Items.Add(item);
                                //lvFastList.Items[nEpcCount].EnsureVisible();
                            }

                            //更新列表中读取的次数
                            if (m_nTotal % m_nRealRate == 1)
                            {
                                int nIndex = 0;
                                foreach (DataRow row in m_curInventoryBuffer.dtTagTable.Rows)
                                {
                                    ListViewItem item = lvFastList.Items[nIndex];
                                    //item.SubItems[3].Text = row[5].ToString();
                                    if (antType16.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "/" + row[8].ToString() + "/" + row[9].ToString() + "/" + row[10].ToString() + "/"
                                        + row[11].ToString() + "/" + row[12].ToString() + "/" + row[13].ToString() + "/" + row[14].ToString() + "/"
                                        + row[15].ToString() + "/" + row[16].ToString() + "/" + row[17].ToString() + "/" + row[18].ToString() + "/"
                                        + row[19].ToString() + "/" + row[20].ToString() + "/" + row[21].ToString() + "/" + row[22].ToString());
                                    }
                                    else if (antType8.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10] + "  /  "
                                       + row[11].ToString() + "  /  " + row[12].ToString() + "  /  " + row[13].ToString() + "  /  " + row[14]);
                                    }
                                    else if (antType4.Checked)
                                    {
                                        item.SubItems[3].Text = (row[7].ToString() + "  /  " + row[8].ToString() + "  /  " + row[9].ToString() + "  /  " + row[10]);
                                    }
                                    item.SubItems[4].Text = (Convert.ToInt32(row[4]) - 129).ToString() + "dBm";
                                    //item.SubItems[5].Text = row[6].ToString();

                                    if (m_nPhaseOpened)
                                    {
                                        item.SubItems[5].Text = row[23].ToString();
                                        item.SubItems[6].Text = row[6].ToString();
                                    }
                                    else
                                    {
                                        item.SubItems[6].Text = row[6].ToString();
                                    }

                                    nIndex++;
                                }
                            }

                        }
                        break;
                    case 0x01:
                        {

                        }
                        break;
                    case 0x02:
                        {

                            //ledFast1.Text.Text = m_nSwitchTime.ToString();
                            //ledFast1.Text.Text = m_nSwitchTotal.ToString();
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
                                    textBox1.Text = m_curSetting.btOutputPower.ToString();
                                    textBox2.Text = m_curSetting.btOutputPower.ToString();
                                    textBox3.Text = m_curSetting.btOutputPower.ToString();
                                    textBox4.Text = m_curSetting.btOutputPower.ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    textBox1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    textBox2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    textBox3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    textBox4.Text = m_curSetting.btOutputPowers[3].ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }

                            }

                            if (antType1.Checked)
                            {
                                if (m_curSetting.btOutputPower != 0 && m_curSetting.btOutputPowers == null)
                                {
                                    textBox1.Text = m_curSetting.btOutputPower.ToString();
                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    textBox1.Text = m_curSetting.btOutputPowers[0].ToString();
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
                                    textBox1.Text = m_curSetting.btOutputPower.ToString();
                                    textBox2.Text = m_curSetting.btOutputPower.ToString();
                                    textBox3.Text = m_curSetting.btOutputPower.ToString();
                                    textBox4.Text = m_curSetting.btOutputPower.ToString();


                                    textBox7.Text = m_curSetting.btOutputPower.ToString();
                                    textBox8.Text = m_curSetting.btOutputPower.ToString();
                                    textBox9.Text = m_curSetting.btOutputPower.ToString();
                                    textBox10.Text = m_curSetting.btOutputPower.ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                                else if (m_curSetting.btOutputPowers != null)
                                {
                                    textBox1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    textBox2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    textBox3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    textBox4.Text = m_curSetting.btOutputPowers[3].ToString();
                                    textBox7.Text = m_curSetting.btOutputPowers[4].ToString();
                                    textBox8.Text = m_curSetting.btOutputPowers[5].ToString();
                                    textBox9.Text = m_curSetting.btOutputPowers[6].ToString();
                                    textBox10.Text = m_curSetting.btOutputPowers[7].ToString();

                                    m_curSetting.btOutputPower = 0;
                                    m_curSetting.btOutputPowers = null;
                                }
                            }
                            else if (antType16.Checked)
                            {
                                if (m_curSetting.btOutputPowers != null)
                                {
                                    textBox1.Text = m_curSetting.btOutputPowers[0].ToString();
                                    textBox2.Text = m_curSetting.btOutputPowers[1].ToString();
                                    textBox3.Text = m_curSetting.btOutputPowers[2].ToString();
                                    textBox4.Text = m_curSetting.btOutputPowers[3].ToString();
                                    textBox7.Text = m_curSetting.btOutputPowers[4].ToString();
                                    textBox8.Text = m_curSetting.btOutputPowers[5].ToString();
                                    textBox9.Text = m_curSetting.btOutputPowers[6].ToString();
                                    textBox10.Text = m_curSetting.btOutputPowers[7].ToString();

                                    if (m_curSetting.btOutputPowers.Length >= 16)
                                    {
                                        tb_dbm_9.Text = m_curSetting.btOutputPowers[8].ToString();
                                        tb_dbm_10.Text = m_curSetting.btOutputPowers[9].ToString();
                                        tb_dbm_11.Text = m_curSetting.btOutputPowers[10].ToString();
                                        tb_dbm_12.Text = m_curSetting.btOutputPowers[11].ToString();
                                        tb_dbm_13.Text = m_curSetting.btOutputPowers[12].ToString();
                                        tb_dbm_14.Text = m_curSetting.btOutputPowers[13].ToString();
                                        tb_dbm_15.Text = m_curSetting.btOutputPowers[14].ToString();
                                        tb_dbm_16.Text = m_curSetting.btOutputPowers[15].ToString();
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

            /**
            if (m_curSetting.btsGetTagMask[1] == (byte)0xFF)
            {
                comboBox7.SelectedIndex = 5;
            }
            else
            {
                comboBox7.SelectedIndex = m_curSetting.btsGetTagMask[1];
            }

            if (m_curSetting.btsGetTagMask[2] == (byte)0xFF)
            {
                comboBox5.SelectedIndex = 8;
            }
            else
            {
                comboBox5.SelectedIndex = m_curSetting.btsGetTagMask[2];
            }

            if (m_curSetting.btsGetTagMask[3] == (byte)0xFF)
            {
                comboBox4.SelectedIndex = 4;
            }
            else
            {
                comboBox4.SelectedIndex = m_curSetting.btsGetTagMask[3];
            }
           hexTextBox6.Text = CCommondMethod.ByteArrayToString(new byte[] { m_curSetting.btsGetTagMask[4] },0,1);
           hexTextBox5.Text = CCommondMethod.ByteArrayToString(new byte[] { m_curSetting.btsGetTagMask[5] },0, 1);
           byte[] maskValue = new byte[m_curSetting.btsGetTagMask.Length - 8];
           for (int i = 0; i < maskValue.Length; i++ )
           {
               maskValue[i] = m_curSetting.btsGetTagMask[i + 6];
           }
           hexTextBox4.Text = CCommondMethod.ByteArrayToString(maskValue,0,maskValue.Length); */
        }

        private delegate void RunLoopInventoryUnsafe();
        private void RunLoopInventroy()
        {
            if (this.InvokeRequired)
            {
                RunLoopInventoryUnsafe InvokeRunLoopInventory = new RunLoopInventoryUnsafe(RunLoopInventroy);
                this.Invoke(InvokeRunLoopInventory, new object[] { });
            }
            else
            {
                //校验盘存是否所有天线均完成
                if (m_curInventoryBuffer.nIndexAntenna < m_curInventoryBuffer.lAntenna.Count - 1 || m_curInventoryBuffer.nCommond == 0)
                {
                    if (m_curInventoryBuffer.nCommond == 0)
                    {
                        if (m_curInventoryBuffer.bLoopInventoryReal)
                        {
                            //m_bLockTab = true;
                            //btnInventory.Enabled = false;
                            if (m_curInventoryBuffer.bLoopCustomizedSession)//自定义Session和Inventoried Flag 
                            {
                                reader.CustomizedInventoryV2(m_curSetting.btReadId, m_curInventoryBuffer.CustomizeSessionParameters.ToArray());
                            }
                            else //实时盘存
                            {
                                reader.InventoryReal(m_curSetting.btReadId, m_curInventoryBuffer.btRepeat);
                            }
                        }
                        else
                        {
                            m_curInventoryBuffer.nCommond = 1;
                            if (m_curInventoryBuffer.bLoopInventory)
                                reader.Inventory(m_curSetting.btReadId, m_curInventoryBuffer.btRepeat);
                        }
                    }
                    else
                    {
                        m_curInventoryBuffer.nCommond = 0;
                        m_curInventoryBuffer.nIndexAntenna++;

                        byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                        if (btWorkAntenna >= 0x08 && m_curSetting.btAntGroup == (byte)0x00)
                        {
                            //切换天线组
                            m_curSetting.btAntGroup = 0x01;
                            reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        }
                        else
                        {
                            if (btWorkAntenna >= 0x08)
                            {
                                btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                            }
                            m_curSetting.btWorkAntenna = btWorkAntenna;
                            reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                        }
                    }
                }
                //校验是否循环盘存
                else if (m_curInventoryBuffer.bLoopInventory)
                {
                    m_curInventoryBuffer.nIndexAntenna = 0;
                    m_curInventoryBuffer.nCommond = 0;

                    if (antType16.Checked)
                    {
                        //切换天线组
                        byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                        if (btWorkAntenna >= (byte)0x08 && m_curSetting.btAntGroup == (byte)0x00)
                        {
                            m_curSetting.btAntGroup = 0x01;
                            reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        }
                        else if (btWorkAntenna < (byte)0x08 && m_curSetting.btAntGroup == (byte)0x01)
                        {
                            m_curSetting.btAntGroup = 0x00;
                            reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        }
                        else
                        {
                            if (btWorkAntenna >= 0x08)
                                btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                            reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                            m_curSetting.btWorkAntenna = btWorkAntenna;
                        }
                    }
                    else
                    {
                        byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                        reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                        m_curSetting.btWorkAntenna = btWorkAntenna;
                    }
                }
            }
        }

        private delegate void SendFastSwitchUnsafe();
        private void SendFastSwitch()
        {
            if (this.InvokeRequired)
            {
                SendFastSwitchUnsafe InvokeSendFastSwitch = new SendFastSwitchUnsafe(SendFastSwitch);
                this.Invoke(InvokeSendFastSwitch, new object[] { });
            }
            else
            {
                this.mSendFastSwitchTimer.Enabled = true;
                this.mSendFastSwitchTimer.Start();
            }
        }

        private delegate void RunLoopFastSwitchUnsafe();
        private void RunLoopFastSwitch()
        {

            if (this.InvokeRequired)
            {
                RunLoopFastSwitchUnsafe InvokeRunLoopFastSwitch = new RunLoopFastSwitchUnsafe(RunLoopFastSwitch);
                this.Invoke(InvokeRunLoopFastSwitch, new object[] { });
            }
            else
            {
                //Console.WriteLine("-----------------RunLoopFastSwitch");
                if (mDynamicPoll.Checked && !m_nRepeat2 && !m_nRepeat1 && !m_nRepeat12)
                {
                    if (!antType16.Checked || m_curSetting.btAntGroup == (byte)0x01)
                        m_nRepeat1 = true;
                    else if (antType16.Checked)
                    {
                        m_curSetting.btAntGroup = (byte)0x01;
                        reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        return;
                    }


                    if (antType4.Checked)
                    {
                        m_btAryData_4[m_btAryData_4.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_4);
                    }
                    else if (antType8.Checked)
                    {
                        m_btAryData[m_btAryData.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                    }
                    else if (antType16.Checked)
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            m_btAryData[m_btAryData.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                        }
                        else
                        {
                            m_btAryData_group2[m_btAryData_group2.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_group2);
                        }
                    }
                }
                else if (mDynamicPoll.Checked && !m_nRepeat2 && m_nRepeat1 && !m_nRepeat12)
                {
                    m_nRepeat2 = true;

                    reader.SetTempOutpower(m_curSetting.btReadId, Convert.ToByte(m_new_fast_inventory_power2.Text));

                }
                else if (mDynamicPoll.Checked && m_nRepeat2 && m_nRepeat1 && !m_nRepeat12)
                {
                    if (!antType16.Checked || m_curSetting.btAntGroup == (byte)0x01)
                        m_nRepeat12 = true;
                    else if (antType16.Checked)
                    {
                        m_curSetting.btAntGroup = (byte)0x01;
                        reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                        return;
                    }

                    if (antType4.Checked)
                    {
                        m_btAryData_4[m_btAryData_4.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat2.Text);
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_4);
                    }
                    else if (antType8.Checked)
                    {
                        m_btAryData[m_btAryData.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat2.Text);
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                    }
                    else if (antType16.Checked)
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            m_btAryData[m_btAryData.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                        }
                        else
                        {
                            m_btAryData_group2[m_btAryData_group2.Length - 1] = Convert.ToByte(m_new_fast_inventory_repeat1.Text);
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_group2);
                        }
                    }
                }
                else
                {
                    if (!antType16.Checked || m_curSetting.btAntGroup == (byte)0x01)
                    {
                        if (m_FastExeCount > 0)
                        {
                            m_FastExeCount--;
                        }
                        this.m_writeCount++;
                        if (this.m_TestTagCount != m_curInventoryBuffer.dtTagTable.Rows.Count)
                        {
                            this.m_ErrorCount++;
                            this.m_ErrorTagCount += m_curInventoryBuffer.dtTagTable.Rows.Count;
                        }
                        StreamWriter writer = new StreamWriter(m_FilePath, true);
                        writer.WriteLine(this.m_writeCount.ToString() + " " + "本次读取的标签数量是：" + m_curInventoryBuffer.dtTagTable.Rows.Count + " " + "耗时："
                            + ((long)(DateTime.Now - m_startConsumTime).TotalMilliseconds).ToString());
                        writer.Flush();
                        writer.Dispose();

                        if (m_FastExeCount >= 0)
                            m_curInventoryBuffer.dtTagTable.Clear();

                        if (m_curInventoryBuffer.bLoopInventory)
                        {
                            if (m_FastExeCount > 0)
                            {
                                //Console.WriteLine("循环次数=" + m_FastExeCount + ", 开始下一次");
                                mFastSessionTimer.Enabled = true;
                                mFastSessionTimer.Start();
                            }
                            else if (m_FastExeCount == 0)
                            {
                                m_bInventory = false;
                                m_curInventoryBuffer.bLoopInventory = false;
                                m_curInventoryBuffer.bLoopInventoryReal = false;
                                btFastInventory.BackColor = Color.WhiteSmoke;
                                btFastInventory.ForeColor = Color.DarkBlue;
                                btFastInventory.Text = "开始盘存";
                                //Console.WriteLine("循环次数=0，结束");

                            }
                            else
                            {
                                //Console.WriteLine("m_FastExeCount < 0 ，循环");
                                mFastSessionTimer.Enabled = true;
                                mFastSessionTimer.Start();
                            }

                        }
                    }
                    else
                    {
                        m_curSetting.btAntGroup = (byte)0x01;
                        //Console.WriteLine("-----------设置天线组" + m_curSetting.btAntGroup);
                        reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                    }
                }
            }
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

        private void rdbRS232_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbRS232.Checked)
            {
                gbRS232.Enabled = true;
                btnDisconnectRs232.Enabled = false;

                //设置按钮字体颜色
                btnConnectRs232.ForeColor = Color.Indigo;
                SetButtonBold(btnConnectRs232);
                if (btnConnectTcp.Font.Bold)
                {
                    SetButtonBold(btnConnectTcp);
                }

                gbTcpIp.Enabled = false;
            }
        }

        private void rdbTcpIp_CheckedChanged(object sender, EventArgs e)
        {
            if (rdbTcpIp.Checked)
            {
                gbTcpIp.Enabled = true;
                btnDisconnectTcp.Enabled = false;

                //设置按钮字体颜色
                btnConnectTcp.ForeColor = Color.Indigo;
                if (btnConnectRs232.Font.Bold)
                {
                    SetButtonBold(btnConnectRs232);
                }
                SetButtonBold(btnConnectTcp);

                gbRS232.Enabled = false;
            }
        }

        private void SetButtonBold(Button btnBold)
        {
            Font oldFont = btnBold.Font;
            Font newFont = new Font(oldFont, oldFont.Style ^ FontStyle.Bold);
            btnBold.Font = newFont;
        }

        private void SetRadioButtonBold(CheckBox ckBold)
        {
            Font oldFont = ckBold.Font;
            Font newFont = new Font(oldFont, oldFont.Style ^ FontStyle.Bold);
            ckBold.Font = newFont;
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

        private void btnConnectRs232_Click(object sender, EventArgs e)
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
            }

            //处理界面元素是否有效
            SetFormEnable(true);


            btnConnectRs232.Enabled = false;
            btnDisconnectRs232.Enabled = true;

            //设置按钮字体颜色
            btnConnectRs232.ForeColor = Color.Black;
            btnDisconnectRs232.ForeColor = Color.Indigo;
            SetButtonBold(btnConnectRs232);
            SetButtonBold(btnDisconnectRs232);
        }

        private void btnDisconnectRs232_Click(object sender, EventArgs e)
        {
            //处理串口断开连接读写器
            reader.CloseCom();

            //处理界面元素是否有效
            SetFormEnable(false);
            btnConnectRs232.Enabled = true;
            btnDisconnectRs232.Enabled = false;

            //设置按钮字体颜色
            btnConnectRs232.ForeColor = Color.Indigo;
            btnDisconnectRs232.ForeColor = Color.Black;
            SetButtonBold(btnConnectRs232);
            SetButtonBold(btnDisconnectRs232);
        }

        private void btnConnectTcp_Click(object sender, EventArgs e)
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
                }

                //处理界面元素是否有效
                SetFormEnable(true);
                btnConnectTcp.Enabled = false;
                btnDisconnectTcp.Enabled = true;

                //设置按钮字体颜色
                btnConnectTcp.ForeColor = Color.Black;
                btnDisconnectTcp.ForeColor = Color.Indigo;
                SetButtonBold(btnConnectTcp);
                SetButtonBold(btnDisconnectTcp);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void btnDisconnectTcp_Click(object sender, EventArgs e)
        {
            //处理断开Tcp连接读写器
            reader.SignOut();

            //处理界面元素是否有效
            SetFormEnable(false);
            btnConnectTcp.Enabled = true;
            btnDisconnectTcp.Enabled = false;

            //设置按钮字体颜色
            btnConnectTcp.ForeColor = Color.Indigo;
            btnDisconnectTcp.ForeColor = Color.Black;
            SetButtonBold(btnConnectTcp);
            SetButtonBold(btnDisconnectTcp);
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
                    if (textBox1.Text.Length != 0 || textBox2.Text.Length != 0 || textBox3.Text.Length != 0 || textBox4.Text.Length != 0
                       || textBox7.Text.Length != 0 || textBox8.Text.Length != 0 || textBox9.Text.Length != 0 || textBox10.Text.Length != 0)
                    {
                        byte[] OutputPower = new byte[8];
                        OutputPower[0] = Convert.ToByte(textBox1.Text);
                        OutputPower[1] = Convert.ToByte(textBox2.Text);
                        OutputPower[2] = Convert.ToByte(textBox3.Text);
                        OutputPower[3] = Convert.ToByte(textBox4.Text);
                        OutputPower[4] = Convert.ToByte(textBox7.Text);
                        OutputPower[5] = Convert.ToByte(textBox8.Text);
                        OutputPower[6] = Convert.ToByte(textBox9.Text);
                        OutputPower[7] = Convert.ToByte(textBox10.Text);

                        //m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                        reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                        // m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                    }
                }
                else if (antType4.Checked)
                {
                    if (textBox1.Text.Length != 0 || textBox2.Text.Length != 0 || textBox3.Text.Length != 0 || textBox4.Text.Length != 0)
                    {
                        byte[] OutputPower = new byte[4];
                        OutputPower[0] = Convert.ToByte(textBox1.Text);
                        OutputPower[1] = Convert.ToByte(textBox2.Text);
                        OutputPower[2] = Convert.ToByte(textBox3.Text);
                        OutputPower[3] = Convert.ToByte(textBox4.Text);
                        //m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                        reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                        // m_curSetting.btOutputPower = Convert.ToByte(txtOutputPower.Text);
                    }
                }
                else if (antType1.Checked)
                {
                    if (textBox1.Text.Length != 0)
                    {
                        byte[] OutputPower = new byte[1];
                        OutputPower[0] = Convert.ToByte(textBox1.Text);
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
            m_bInventory = false;
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
            //fixed
            m_curInventoryBuffer.dtEndInventory = DateTime.Now;
            TimeSpan tsp = DateTime.Now - m_InventoryStarTime;
            int consume = tsp.Minutes * 60 * 1000 + tsp.Seconds * 1000 + tsp.Milliseconds;

            if ((this.m_ConsumTime != 0) && (this.m_ConsumTime < consume))
            {
                RefreshInventoryReal(0x02);
                return;
            }
            //fixed 

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

                    //校验是否盘存操作
                    if (m_bInventory)
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
                strErrorCode = "未知错误";
            }

            string strLog = strCmd + "失败，失败原因： " + strErrorCode;
            WriteLog(lrtxtLog, strLog, 1);

            if (m_bInventory)
            {
                m_curInventoryBuffer.nCommond = 1;
                m_curInventoryBuffer.dtEndInventory = DateTime.Now;
                RunLoopInventroy();
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

            #region FastInventory_8A_v2
            if (fast_inv_v2_cb.Checked)
            {
                parseMessage(msgTran.AryTranData);
                return;
            }
            #endregion FastInventory_8A_v2

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

            #region FastInventory_8A_v2
            if (fast_inv_v2_cb.Checked)
            {
                parseFastInventoryV2(msgTran.AryTranData);
                return;
            }
            #endregion FastInventory_8A_v2

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
                            if (textBox1.Text.Length != 0 || textBox2.Text.Length != 0 || textBox3.Text.Length != 0 || textBox4.Text.Length != 0
                               || textBox7.Text.Length != 0 || textBox8.Text.Length != 0 || textBox9.Text.Length != 0 || textBox10.Text.Length != 0)
                            {
                                byte[] OutputPower = new byte[8];
                                OutputPower[0] = Convert.ToByte(textBox1.Text);
                                OutputPower[1] = Convert.ToByte(textBox2.Text);
                                OutputPower[2] = Convert.ToByte(textBox3.Text);
                                OutputPower[3] = Convert.ToByte(textBox4.Text);
                                OutputPower[4] = Convert.ToByte(textBox7.Text);
                                OutputPower[5] = Convert.ToByte(textBox8.Text);
                                OutputPower[6] = Convert.ToByte(textBox9.Text);
                                OutputPower[7] = Convert.ToByte(textBox10.Text);

                                reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                            }
                        }
                        else
                        {
                            if (tb_dbm_9.Text.Length != 0 || tb_dbm_10.Text.Length != 0 || tb_dbm_11.Text.Length != 0 || tb_dbm_12.Text.Length != 0
                               || tb_dbm_13.Text.Length != 0 || tb_dbm_14.Text.Length != 0 || tb_dbm_15.Text.Length != 0 || tb_dbm_16.Text.Length != 0)
                            {
                                byte[] OutputPower = new byte[8];
                                OutputPower[0] = Convert.ToByte(tb_dbm_9.Text);
                                OutputPower[1] = Convert.ToByte(tb_dbm_10.Text);
                                OutputPower[2] = Convert.ToByte(tb_dbm_11.Text);
                                OutputPower[3] = Convert.ToByte(tb_dbm_12.Text);
                                OutputPower[4] = Convert.ToByte(tb_dbm_13.Text);
                                OutputPower[5] = Convert.ToByte(tb_dbm_14.Text);
                                OutputPower[6] = Convert.ToByte(tb_dbm_15.Text);
                                OutputPower[7] = Convert.ToByte(tb_dbm_16.Text);

                                reader.SetOutputPower(m_curSetting.btReadId, OutputPower);
                            }
                        }
                        return;
                    }
                    if (m_curInventoryBuffer.bLoopInventoryReal)
                    {
                        byte btWorkAntenna;
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                        }
                        else
                        {
                            btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                            if (btWorkAntenna >= 0x08)
                                btWorkAntenna = (byte)((btWorkAntenna & 0xFF) - 0x08);
                        }
                        reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                        m_curSetting.btWorkAntenna = btWorkAntenna;
                        return;
                    }
                    if (mDynamicPoll.Checked)
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            //Console.WriteLine("轮询模式------设置天线组0成功, 开始设置功率:" + Convert.ToByte(m_new_fast_inventory_power1.Text));
                            reader.SetTempOutpower(m_curSetting.btReadId, Convert.ToByte(m_new_fast_inventory_power1.Text));
                        }
                        else
                        {
                            //Console.WriteLine("轮询模式------设置天线组1成功, 开始快速盘存");
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_group2);
                        }
                    }
                    else
                    {
                        if (m_curSetting.btAntGroup == (byte)0x00)
                        {
                            //Console.WriteLine("设置天线组0成功, 开始快速盘存");
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                        }
                        else
                        {
                            //Console.WriteLine("设置天线组1成功, 开始快速盘存");
                            reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_group2);
                        }
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

        private void rdbInventoryTag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbOperateTag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rdbInventoryRealTag_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void rbdFastSwitchInventory_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnInventory_Click(object sender, EventArgs e)
        {
            /*try
            {                
                if (rbdFastSwitchInventory.Checked)
                {
                }
                else
                {
                    m_curInventoryBuffer.ClearInventoryPar();

                    if (txtChannel.Text.Length == 0)
                    {
                        MessageBox.Show("请输入跳频次数");
                        return;
                    }
                    m_curInventoryBuffer.btChannel = Convert.ToByte(txtChannel.Text);

                    if (ckWorkAntenna1.Checked)
                    {
                        m_curInventoryBuffer.lAntenna.Add(0x00);
                    }
                    if (ckWorkAntenna2.Checked)
                    {
                        m_curInventoryBuffer.lAntenna.Add(0x01);
                    }
                    if (ckWorkAntenna3.Checked)
                    {
                        m_curInventoryBuffer.lAntenna.Add(0x02);
                    }
                    if (ckWorkAntenna4.Checked)
                    {
                        m_curInventoryBuffer.lAntenna.Add(0x03);
                    }
                    if (m_curInventoryBuffer.lAntenna.Count == 0)
                    {
                        MessageBox.Show("请至少选择一个天线");
                        return;
                    }
                }                

                //默认循环发送命令
                if (m_curInventoryBuffer.bLoopInventory)
                {
                    m_bInventory = false;
                    m_curInventoryBuffer.bLoopInventory = false;
                    btnInventory.BackColor = Color.WhiteSmoke;
                    btnInventory.ForeColor = Color.Indigo;
                    btnInventory.Text = "开始盘存";
                    return;
                }
                else
                {
                    //ISO 18000-6B盘存是否正在运行
                    if (m_bContinue)
                    {
                        if (MessageBox.Show("ISO 18000-6B标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            btnInventoryISO18000_Click(sender, e);
                            return;
                        }
                    }

                    m_bInventory = true; 
                    m_curInventoryBuffer.bLoopInventory = true;
                    btnInventory.BackColor = Color.Indigo;
                    btnInventory.ForeColor = Color.White;
                    btnInventory.Text = "停止盘存";
                }

                if (rdbInventoryRealTag.Checked)
                {
                    m_curInventoryBuffer.bLoopInventoryReal = true;
                }

                m_curInventoryBuffer.ClearInventoryRealResult();
                ltvInventoryEpc.Items.Clear();
                ltvInventoryTag.Items.Clear();
                m_nTotal = 0;
                if (rbdFastSwitchInventory.Checked)
                {
                    if (cmbAntSelect1.SelectedIndex == -1)
                    {
                        m_btAryData[0] = 0xFF;
                    }
                    else
                    {
                        m_btAryData[0] = Convert.ToByte(cmbAntSelect1.SelectedIndex);
                    }
                    if (txtStayA.Text.Length == 0)
                    {
                        m_btAryData[1] = 0x00;
                    }
                    else
                    {
                        m_btAryData[1] = Convert.ToByte(txtStayA.Text);
                    }

                    if (cmbAntSelect2.SelectedIndex == -1)
                    {
                        m_btAryData[2] = 0xFF;
                    }
                    else
                    {
                        m_btAryData[2] = Convert.ToByte(cmbAntSelect2.SelectedIndex);
                    }
                    if (txtStayB.Text.Length == 0)
                    {
                        m_btAryData[3] = 0x00;
                    }
                    else
                    {
                        m_btAryData[3] = Convert.ToByte(txtStayB.Text);
                    }

                    if (cmbAntSelect3.SelectedIndex == -1)
                    {
                        m_btAryData[4] = 0xFF;
                    }
                    else
                    {
                        m_btAryData[4] = Convert.ToByte(cmbAntSelect3.SelectedIndex);
                    }
                    if (txtStayC.Text.Length == 0)
                    {
                        m_btAryData[5] = 0x00;
                    }
                    else
                    {
                        m_btAryData[5] = Convert.ToByte(txtStayC.Text);
                    }

                    if (cmbAntSelect4.SelectedIndex == -1)
                    {
                        m_btAryData[6] = 0xFF;
                    }
                    else
                    {
                        m_btAryData[6] = Convert.ToByte(cmbAntSelect4.SelectedIndex);
                    }
                    if (txtStayD.Text.Length == 0)
                    {
                        m_btAryData[7] = 0x00;
                    }
                    else
                    {
                        m_btAryData[7] = Convert.ToByte(txtStayD.Text);
                    }

                    if (txtInterval.Text.Length == 0)
                    {
                        m_btAryData[8] = 0x00;
                    }
                    else
                    {
                        m_btAryData[8] = Convert.ToByte(txtInterval.Text);
                    }

                    if (txtRepeat.Text.Length == 0)
                    {
                        m_btAryData[9] = 0x00;
                    }
                    else
                    {
                        m_btAryData[9] = Convert.ToByte(txtRepeat.Text);
                    }

                    m_nSwitchTotal = 0;
                    m_nSwitchTime = 0;
                    reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                }
                else
                {
                    byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                    reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                }                
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }     */
        }

        private void ProcessFastSwitch(Reader.MessageTran msgTran)
        {
            string strCmd = "快速天线盘存";
            string strErrorCode = string.Empty;

            #region FastInventory_8A_v2
            if (fast_inv_v2_cb.Checked)
            {
                parseFastInventoryV2(msgTran.AryTranData);
                return;
            }
            #endregion FastInventory_8A_v2

            if (msgTran.AryData.Length == 1)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[0]);
                string strLog = strCmd + "失败，失败原因： " + strErrorCode;

                WriteLog(lrtxtLog, strLog, 1);
                //Console.WriteLine("快速天线盘存失败");
                RefreshFastSwitch(0x8A);
                RunLoopFastSwitch();
                /*
                this.m_FastSessionCount++;

                Console.WriteLine("FastSession:" + this.m_FastSessionCount.ToString());

                if (this.m_FastSessionCount == 1)
                {
                    this.SendFastSwitch();
                }
                else if (this.m_FastSessionCount == 2)
                {
                    RunLoopFastSwitch();
                } */

            }
            else if (msgTran.AryData.Length == 2)
            {
                strErrorCode = CCommondMethod.FormatErrorCode(msgTran.AryData[1]);
                Console.WriteLine("Return ant NO : " + (msgTran.AryData[0] + 1));
                string strLog = strCmd + "失败，失败原因： " + strErrorCode + "--" + "天线" + (msgTran.AryData[0] + 1);

                WriteLog(lrtxtLog, strLog, 1);


                /*
                this.m_FastSessionCount++;

                if (this.m_FastSessionCount == 1)
                {
                    this.SendFastSwitch();
                }
                else if (this.m_FastSessionCount == 2)
                {
                    RunLoopFastSwitch();
                } */

            }

            else if (msgTran.AryData.Length == 7)
            {
                //Console.WriteLine("快速天线盘存结束");
                m_nSwitchTotal = Convert.ToInt32(msgTran.AryData[0]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[1]) * 255 + Convert.ToInt32(msgTran.AryData[2]);
                m_nSwitchTime = Convert.ToInt32(msgTran.AryData[3]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[4]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[5]) * 255 + Convert.ToInt32(msgTran.AryData[6]);

                m_curInventoryBuffer.nDataCount = m_nSwitchTotal;
                m_curInventoryBuffer.nCommandDuration = m_nSwitchTime;
                WriteLog(lrtxtLog, strCmd, 0);
                m_nIsFastEnd = true;
                RefreshFastSwitch(0x00);

                //Console.WriteLine("当前天线组" + m_curSetting.btAntGroup + "，继续下一步, 时间:" + m_nSwitchTime);
                RunLoopFastSwitch();
                /*
                this.m_FastSessionCount++;

                if (this.m_FastSessionCount == 1)
                {
                    this.SendFastSwitch();
                }
                else if (this.m_FastSessionCount == 2)
                {
                    RunLoopFastSwitch();
                } */
            }

            /*else if (msgTran.AryData.Length == 8)
            {
                
                m_nSwitchTotal = Convert.ToInt32(msgTran.AryData[0]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[1]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[2]) * 255 + Convert.ToInt32(msgTran.AryData[3]);
                m_nSwitchTime = Convert.ToInt32(msgTran.AryData[4]) * 255 * 255 * 255 + Convert.ToInt32(msgTran.AryData[5]) * 255 * 255 + Convert.ToInt32(msgTran.AryData[6]) * 255 + Convert.ToInt32(msgTran.AryData[7]);

                m_curInventoryBuffer.nDataCount = m_nSwitchTotal;
                m_curInventoryBuffer.nCommandDuration = m_nSwitchTime;
                WriteLog(lrtxtLog, strCmd, 0);
                RefreshFastSwitch(0x02);
                RunLoopFastSwitch();
            }*/
            else
            {
                m_nTotal++;
                int nLength = msgTran.AryData.Length;

                int nEpcLength = nLength - 4;
                if (m_nPhaseOpened)
                {
                    nEpcLength = nLength - 6;
                }

                //Add inventory list
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, nEpcLength);
                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 2);
                string strRSSI = string.Empty;

                if (m_nPhaseOpened)
                {
                    SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nLength - 3] & 0x7F));
                    strRSSI = (msgTran.AryData[nLength - 3] & 0x7F).ToString();
                }
                else
                {
                    SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nLength - 1] & 0x7F));
                    strRSSI = (msgTran.AryData[nLength - 1] & 0x7F).ToString();
                }

                byte btTemp = msgTran.AryData[0];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                string strPhase = string.Empty;
                if (m_nPhaseOpened)
                {
                    if ((msgTran.AryData[nLength - 3] & 0x80) != 0) btAntId += 4;
                    strPhase = CCommondMethod.ByteArrayToString(msgTran.AryData, nLength - 2, 2);
                }
                else
                {
                    if ((msgTran.AryData[nLength - 1] & 0x80) != 0) btAntId += 4;
                }

                m_curInventoryBuffer.nCurrentAnt = (int)btAntId;
                string strAntId = btAntId.ToString();
                byte btFreq = (byte)(btTemp >> 2);

                string strFreq = GetFreqString(btFreq);

                DataRow[] drs = m_curInventoryBuffer.dtTagTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                if (drs.Length == 0)
                {
                    DataRow row1 = m_curInventoryBuffer.dtTagTable.NewRow();
                    row1[0] = strPC;
                    row1[2] = strEPC;
                    row1[4] = strRSSI;
                    row1[5] = "1";
                    row1[6] = strFreq;
                    row1[7] = "0";
                    row1[8] = "0";
                    row1[9] = "0";
                    row1[10] = "0";
                    row1[11] = "0";
                    row1[12] = "0";
                    row1[13] = "0";
                    row1[14] = "0";
                    row1[15] = "0";
                    row1[16] = "0";
                    row1[17] = "0";
                    row1[18] = "0";
                    row1[19] = "0";
                    row1[20] = "0";
                    row1[21] = "0";
                    row1[22] = "0";
                    row1[23] = strPhase;
                    switch (btAntId)
                    {
                        case 0x01:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[7] = "1";
                            else
                                row1[15] = "1";
                            break;
                        case 0x02:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[8] = "1";
                            else
                                row1[16] = "1";
                            break;
                        case 0x03:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[9] = "1";
                            else
                                row1[17] = "1";
                            break;
                        case 0x04:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[10] = "1";
                            else
                                row1[18] = "1";
                            break;
                        case 0x05:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[11] = "1";
                            else
                                row1[19] = "1";
                            break;
                        case 0x06:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[12] = "1";
                            else
                                row1[20] = "1";
                            break;
                        case 0x07:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[13] = "1";
                            else
                                row1[21] = "1";
                            break;
                        case 0x08:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[14] = "1";
                            else
                                row1[22] = "1";
                            break;
                        default:
                            break;
                    }

                    m_curInventoryBuffer.dtTagTable.Rows.Add(row1);
                    m_curInventoryBuffer.dtTagTable.AcceptChanges();
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
                        //Console.WriteLine("单次累计此时: " + (nTemp + 1));
                        dr[5] = (nTemp + 1).ToString();
                        dr[6] = strFreq;

                        switch (btAntId)
                        {
                            case 0x01:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[7]);
                                    dr[7] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[15]);
                                    dr[15] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x02:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[8]);
                                    dr[8] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[16]);
                                    dr[16] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x03:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[9]);
                                    dr[9] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[17]);
                                    dr[17] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x04:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[10]);
                                    dr[10] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[18]);
                                    dr[18] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x05:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[11]);
                                    dr[11] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[19]);
                                    dr[19] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x06:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[12]);
                                    dr[12] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[20]);
                                    dr[20] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x07:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[13]);
                                    dr[13] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[21]);
                                    dr[21] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x08:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[14]);
                                    dr[14] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[22]);
                                    dr[22] = (nTemp + 1).ToString();
                                }
                                break;
                            default:
                                break;
                        }

                        dr.EndEdit();
                    }
                    m_curInventoryBuffer.dtTagTable.AcceptChanges();
                }

                m_curInventoryBuffer.dtEndInventory = DateTime.Now;
                m_nIsFastEnd = false;
                RefreshFastSwitch(0x00);
            }

        }

        private void ProcessInventoryReal(Reader.MessageTran msgTran)
        {

            m_curInventoryBuffer.dtEndInventory = DateTime.Now;
            TimeSpan ts = DateTime.Now - m_InventoryStarTime;
            int consume = ts.Minutes * 60 * 1000 + ts.Seconds * 1000 + ts.Milliseconds;

            if ((this.m_ConsumTime != 0) && (this.m_ConsumTime < consume))
            {
                RefreshInventoryReal(0x02);
                return;
            }

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

                m_curInventoryBuffer.dtEndInventory = DateTime.Now;

                RefreshInventoryReal(0x89);
                RunLoopInventroy();
            }
            else if (msgTran.AryData.Length == 7)
            {
                m_curInventoryBuffer.nReadRate = Convert.ToInt32(msgTran.AryData[1]) * 256 + Convert.ToInt32(msgTran.AryData[2]);
                m_curInventoryBuffer.nDataCount = Convert.ToInt32(msgTran.AryData[3]) * 256 * 256 * 256 + Convert.ToInt32(msgTran.AryData[4]) * 256 * 256 + Convert.ToInt32(msgTran.AryData[5]) * 256 + Convert.ToInt32(msgTran.AryData[6]);

                m_curInventoryBuffer.dtEndInventory = DateTime.Now;

                WriteLog(lrtxtLog, strCmd, 0);
                RefreshInventoryReal(0x89);
                RunLoopInventroy();
            }
            else
            {
                m_nTotal++;
                int nLength = msgTran.AryData.Length;
                int nEpcLength = nLength - 4;
                string strEPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, nEpcLength);
                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 1, 2);
                //string strPC = CCommondMethod.ByteArrayToString(new byte[]{msgTran.ReadId},0,1);
                string strRSSI = (msgTran.AryData[nLength - 1] & 0x7F).ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nLength - 1] & 0x7F));
                byte btTemp = msgTran.AryData[0];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                if ((msgTran.AryData[nLength - 1] & 0x80) != 0) btAntId += 4;
                m_curInventoryBuffer.nCurrentAnt = (int)btAntId;
                string strAntId = btAntId.ToString();
                byte btFreq = (byte)(btTemp >> 2);


                string strFreq = GetFreqString(btFreq);

                //DataRow row = m_curInventoryBuffer.dtTagDetailTable.NewRow();
                //row[0] = strEPC;
                //row[1] = strRSSI;
                //row[2] = strAntId;
                //row[3] = strFreq;

                //m_curInventoryBuffer.dtTagDetailTable.Rows.Add(row);
                //m_curInventoryBuffer.dtTagDetailTable.AcceptChanges();

                ////增加标签表
                //DataRow[] drsDetail = m_curInventoryBuffer.dtTagDetailTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                //int nDetailCount = drsDetail.Length;
                DataRow[] drs = m_curInventoryBuffer.dtTagTable.Select(string.Format("COLEPC = '{0}'", strEPC));
                if (drs.Length == 0)
                {
                    DataRow row1 = m_curInventoryBuffer.dtTagTable.NewRow();
                    row1[0] = strPC;
                    row1[2] = strEPC;
                    row1[4] = strRSSI;
                    row1[5] = "1";
                    row1[6] = strFreq;
                    row1[7] = "0";
                    row1[8] = "0";
                    row1[9] = "0";
                    row1[10] = "0";
                    row1[11] = "0";
                    row1[12] = "0";
                    row1[13] = "0";
                    row1[14] = "0";
                    row1[15] = "0";
                    row1[16] = "0";
                    row1[17] = "0";
                    row1[18] = "0";
                    row1[19] = "0";
                    row1[20] = "0";
                    row1[21] = "0";
                    row1[22] = "0";
                    switch (btAntId)
                    {
                        case 0x01:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[7] = "1";
                            else
                                row1[15] = "1";
                            break;
                        case 0x02:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[8] = "1";
                            else
                                row1[16] = "1";
                            break;
                        case 0x03:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[9] = "1";
                            else
                                row1[17] = "1";
                            break;
                        case 0x04:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[10] = "1";
                            else
                                row1[18] = "1";
                            break;
                        case 0x05:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[11] = "1";
                            else
                                row1[19] = "1";
                            break;
                        case 0x06:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[12] = "1";
                            else
                                row1[20] = "1";
                            break;
                        case 0x07:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[13] = "1";
                            else
                                row1[21] = "1";
                            break;
                        case 0x08:
                            if (m_curSetting.btAntGroup == (byte)0x00)
                                row1[14] = "1";
                            else
                                row1[22] = "1";
                            break;
                        default:
                            break;
                    }

                    m_curInventoryBuffer.dtTagTable.Rows.Add(row1);
                    m_curInventoryBuffer.dtTagTable.AcceptChanges();
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

                        switch (btAntId)
                        {
                            case 0x01:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[7]);
                                    dr[7] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[15]);
                                    dr[15] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x02:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[8]);
                                    dr[8] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[16]);
                                    dr[16] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x03:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[9]);
                                    dr[9] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[17]);
                                    dr[17] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x04:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[10]);
                                    dr[10] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[18]);
                                    dr[18] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x05:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[11]);
                                    dr[11] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[19]);
                                    dr[19] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x06:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[12]);
                                    dr[12] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[20]);
                                    dr[20] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x07:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[13]);
                                    dr[13] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[21]);
                                    dr[21] = (nTemp + 1).ToString();
                                }
                                break;
                            case 0x08:
                                if (m_curSetting.btAntGroup == (byte)0x00)
                                {
                                    nTemp = Convert.ToInt32(dr[14]);
                                    dr[14] = (nTemp + 1).ToString();
                                }
                                else
                                {
                                    nTemp = Convert.ToInt32(dr[22]);
                                    dr[22] = (nTemp + 1).ToString();
                                }
                                break;
                            default:
                                break;
                        }

                        dr.EndEdit();
                    }
                    m_curInventoryBuffer.dtTagTable.AcceptChanges();
                }
                RefreshInventoryReal(0x89);
            }
        }



        private void ProcessInventory(Reader.MessageTran msgTran)
        {
            string strCmd = "盘存标签";
            string strErrorCode = string.Empty;

            if (msgTran.AryData.Length == 9)
            {
                m_curInventoryBuffer.nCurrentAnt = msgTran.AryData[0];
                m_curInventoryBuffer.nTagCount = Convert.ToInt32(msgTran.AryData[1]) * 256 + Convert.ToInt32(msgTran.AryData[2]);
                m_curInventoryBuffer.nReadRate = Convert.ToInt32(msgTran.AryData[3]) * 256 + Convert.ToInt32(msgTran.AryData[4]);
                int nTotalRead = Convert.ToInt32(msgTran.AryData[5]) * 256 * 256 * 256
                    + Convert.ToInt32(msgTran.AryData[6]) * 256 * 256
                    + Convert.ToInt32(msgTran.AryData[7]) * 256
                    + Convert.ToInt32(msgTran.AryData[8]);
                m_curInventoryBuffer.nDataCount = nTotalRead;
                m_curInventoryBuffer.lTotalRead.Add(nTotalRead);
                m_curInventoryBuffer.dtEndInventory = DateTime.Now;

                RefreshInventory(0x80);
                WriteLog(lrtxtLog, strCmd, 0);

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

            RunLoopInventroy();
        }

        private void btnGetInventoryBuffer_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.dtTagTable.Rows.Clear();

            reader.GetInventoryBuffer(m_curSetting.btReadId);
        }

        private void SetMaxMinRSSI(int nRSSI)
        {
            if (m_curInventoryBuffer.nMaxRSSI < nRSSI)
            {
                m_curInventoryBuffer.nMaxRSSI = nRSSI;
            }

            if (m_curInventoryBuffer.nMinRSSI == 0)
            {
                m_curInventoryBuffer.nMinRSSI = nRSSI;
            }
            else if (m_curInventoryBuffer.nMinRSSI > nRSSI)
            {
                m_curInventoryBuffer.nMinRSSI = nRSSI;
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
            }
            else
            {
                int nDataLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEpc = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);

                string strRSSI = (msgTran.AryData[nDataLen - 3] & 0x7F).ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nDataLen - 3] & 0x7F));
                byte btTemp = msgTran.AryData[nDataLen - 2];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                if ((msgTran.AryData[nDataLen - 3] & 0x80) != 0) btAntId += 4;
                m_curInventoryBuffer.nCurrentAnt = (int)btAntId;
                string strAntId = btAntId.ToString();
                string strReadCnr = msgTran.AryData[nDataLen - 1].ToString();

                DataRow row = m_curInventoryBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEpc;
                row[3] = strAntId;
                row[4] = strRSSI;
                row[5] = strReadCnr;

                m_curInventoryBuffer.dtTagTable.Rows.Add(row);
                m_curInventoryBuffer.dtTagTable.AcceptChanges();

                RefreshInventory(0x90);
                WriteLog(lrtxtLog, strCmd, 0);
            }
        }

        private void btnGetAndResetInventoryBuffer_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.dtTagTable.Rows.Clear();

            reader.GetAndResetInventoryBuffer(m_curSetting.btReadId);
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
            }
            else
            {
                int nDataLen = msgTran.AryData.Length;
                int nEpcLen = Convert.ToInt32(msgTran.AryData[2]) - 4;

                string strPC = CCommondMethod.ByteArrayToString(msgTran.AryData, 3, 2);
                string strEpc = CCommondMethod.ByteArrayToString(msgTran.AryData, 5, nEpcLen);
                string strCRC = CCommondMethod.ByteArrayToString(msgTran.AryData, 5 + nEpcLen, 2);
                string strRSSI = (msgTran.AryData[nDataLen - 3] & 0x7F).ToString();
                SetMaxMinRSSI(Convert.ToInt32(msgTran.AryData[nDataLen - 3] & 0x7F));
                byte btTemp = msgTran.AryData[nDataLen - 2];
                byte btAntId = (byte)((btTemp & 0x03) + 1);
                if ((msgTran.AryData[nDataLen - 3] & 0x80) != 0) btAntId += 4;
                m_curInventoryBuffer.nCurrentAnt = (int)btAntId;
                string strAntId = btAntId.ToString();

                string strReadCnr = msgTran.AryData[nDataLen - 1].ToString();

                DataRow row = m_curInventoryBuffer.dtTagTable.NewRow();
                row[0] = strPC;
                row[1] = strCRC;
                row[2] = strEpc;
                row[3] = strAntId;
                row[4] = strRSSI;
                row[5] = strReadCnr;

                m_curInventoryBuffer.dtTagTable.Rows.Add(row);
                m_curInventoryBuffer.dtTagTable.AcceptChanges();

                RefreshInventory(0x91);
                WriteLog(lrtxtLog, strCmd, 0);
            }
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
                m_curInventoryBuffer.nTagCount = Convert.ToInt32(msgTran.AryData[0]) * 256 + Convert.ToInt32(msgTran.AryData[1]);

                RefreshInventory(0x92);
                string strLog1 = strCmd + " " + m_curInventoryBuffer.nTagCount.ToString();
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
                    RefreshInventory(0x93);
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
            if (m_bContinue)
            {
                m_bContinue = false;
                m_curInventoryBuffer.bLoopInventoryReal = false;
                btnInventoryISO18000.BackColor = Color.WhiteSmoke;
                btnInventoryISO18000.ForeColor = Color.Indigo;
                btnInventoryISO18000.Text = "开始盘存";
            }
            else
            {
                //判断EPC盘存是否正在进行
                if (m_curInventoryBuffer.bLoopInventory)
                {
                    if (MessageBox.Show("EPC C1G2标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                    {
                        return;
                    }
                    else
                    {
                        btnInventory_Click(sender, e);
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
            DataRow[] drs = m_curInventoryBuffer.dtTagTable.Select();
            foreach (DataRow row in drs)
            {
                cmbSetAccessEpcMatch.Items.Add(row[2].ToString());
            }
        }


        private void btnClearInventoryRealResult_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.ClearInventoryRealResult();



            lvRealList.Items.Clear();
            //ltvInventoryTag.Items.Clear();
        }

        private void ltvInventoryEpc_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ltvInventoryTag.Items.Clear();
            DataRow[] drs;

            if (lvRealList.SelectedItems.Count == 0)
            {
                drs = m_curInventoryBuffer.dtTagDetailTable.Select();
                //ShowListView(ltvInventoryTag, drs);
            }
            else
            {
                foreach (ListViewItem itemEpc in lvRealList.SelectedItems)
                {
                    //ListViewItem itemEpc = ltvInventoryEpc.Items[nIndex];
                    string strEpc = itemEpc.SubItems[1].Text;

                    drs = m_curInventoryBuffer.dtTagDetailTable.Select(string.Format("COLEPC = '{0}'", strEpc));
                    //ShowListView(ltvInventoryTag, drs);
                }
            }
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

        private void btRealTimeInventory_Click(object sender, EventArgs e)
        {
            try
            {
                this.m_ConsumTime = Convert.ToInt32(this.customizedExeTime.Text);

                m_curInventoryBuffer.ClearInventoryPar();

                if (textRealRound.Text.Length == 0)
                {
                    MessageBox.Show("请输入循环次数");
                    return;
                }
                m_curInventoryBuffer.btRepeat = Convert.ToByte(textRealRound.Text);

                if (this.sessionInventoryrb.Checked == true)
                {
                    if (cmbSession.SelectedIndex == -1)
                    {
                        MessageBox.Show("请输入Session ID");
                        return;
                    }
                    if (cmbTarget.SelectedIndex == -1)
                    {
                        MessageBox.Show("请输入Inventoried Flag");
                        return;
                    }
                    m_curInventoryBuffer.bLoopCustomizedSession = true;
                    m_curInventoryBuffer.btSession = (byte)cmbSession.SelectedIndex;
                    m_curInventoryBuffer.btTarget = (byte)cmbTarget.SelectedIndex;

                    m_curInventoryBuffer.CustomizeSessionParameters.Add((byte)cmbSession.SelectedIndex);
                    m_curInventoryBuffer.CustomizeSessionParameters.Add((byte)cmbTarget.SelectedIndex);

                    if (m_session_sl_cb.Checked)
                    {
                        m_curInventoryBuffer.CustomizeSessionParameters.Add((byte)m_session_sl.SelectedIndex);
                        //m_curInventoryBuffer.CustomizeSessionParameters.Add((byte)mSessionExeTime.SelectedIndex);
                    }

                    if (m_session_q_cb.Checked)
                    {
                        byte startQ = Convert.ToByte(m_session_start_q.Text);
                        byte minQ = Convert.ToByte(m_session_min_q.Text);
                        byte maxQ = Convert.ToByte(m_session_max_q.Text);
                        if (startQ < 0 || minQ < 0 || maxQ < 0 || startQ > 15 || minQ > 15 || maxQ > 15)
                        {
                            MessageBox.Show("Start Q,Min Q,Max Q must be 0-15");
                            return;
                        }
                        m_curInventoryBuffer.CustomizeSessionParameters.Add(startQ);
                        m_curInventoryBuffer.CustomizeSessionParameters.Add(minQ);
                        m_curInventoryBuffer.CustomizeSessionParameters.Add(maxQ);

                    }
                    m_curInventoryBuffer.CustomizeSessionParameters.Add(Convert.ToByte(textRealRound.Text));

                }
                else
                {
                    m_curInventoryBuffer.bLoopCustomizedSession = false;
                }

                //默认循环发送命令
                if (m_curInventoryBuffer.bLoopInventory)
                {
                    m_bInventory = false;
                    m_curInventoryBuffer.bLoopInventory = false;
                    m_curInventoryBuffer.bLoopInventoryReal = false;
                    btRealTimeInventory.BackColor = Color.WhiteSmoke;
                    btRealTimeInventory.ForeColor = Color.DarkBlue;
                    btRealTimeInventory.Text = "开始盘存";
                    timerInventory.Enabled = false;

                    totalTime.Enabled = false;
                    return;
                }
                else
                {
                    //ISO 18000-6B盘存是否正在运行
                    if (m_bContinue)
                    {
                        if (MessageBox.Show("ISO 18000-6B标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            btnInventoryISO18000_Click(sender, e);
                            return;
                        }
                    }

                    m_bInventory = true;
                    m_curInventoryBuffer.bLoopInventory = true;
                    btRealTimeInventory.BackColor = Color.DarkBlue;
                    btRealTimeInventory.ForeColor = Color.White;
                    btRealTimeInventory.Text = "停止盘存";
                }

                m_curInventoryBuffer.bLoopInventoryReal = true;

                m_curInventoryBuffer.ClearInventoryRealResult();
                lvRealList.Items.Clear();
                lvRealList.Items.Clear();
                tbRealMaxRssi.Text = "0";
                tbRealMinRssi.Text = "0";
                m_nTotal = 0;

                this.m_InventoryStarTime = DateTime.Now;
                if(cmbx_realinv_workant.SelectedIndex > -1)
                {
                    m_curInventoryBuffer.lAntenna.Add((byte)(cmbx_realinv_workant.SelectedIndex));
                }
                else
                    m_curInventoryBuffer.lAntenna.Add(0x00);

                m_curInventoryBuffer.nIndexAntenna = 0;
                byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                if (btWorkAntenna >= (byte)0x08)
                    m_curSetting.btAntGroup = 0x01;
                else
                    m_curSetting.btAntGroup = 0x00;
                reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);

                timerInventory.Enabled = true;

                totalTime.Enabled = true;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void btRealFresh_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.ClearInventoryRealResult();

            lvRealList.Items.Clear();
            lvRealList.Items.Clear();
            ledReal1.Text = "0";
            ledReal2.Text = "0";
            ledReal3.Text = "0";
            ledReal4.Text = "0";
            ledReal5.Text = "0";
            tbRealMaxRssi.Text = "0";
            tbRealMinRssi.Text = "0";
            textRealRound.Text = "1";
            lbRealTagCount.Text = "标签列表：";
        }

        private void btBufferInventory_Click(object sender, EventArgs e)
        {
            try
            {
                m_curInventoryBuffer.ClearInventoryPar();

                if (textReadRoundBuffer.Text.Length == 0)
                {
                    MessageBox.Show("请输入循环次数");
                    return;
                }
                m_curInventoryBuffer.btRepeat = Convert.ToByte(textReadRoundBuffer.Text);

                if (cbBufferWorkant1.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x00);
                }
                if (cbBufferWorkant2.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x01);
                }
                if (cbBufferWorkant3.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x02);
                }
                if (cbBufferWorkant4.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x03);
                }
                if (checkBox1.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x04);
                }
                if (checkBox2.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x05);
                }
                if (checkBox3.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x06);
                }
                if (checkBox4.Checked)
                {
                    m_curInventoryBuffer.lAntenna.Add(0x07);
                }
                if (m_curInventoryBuffer.lAntenna.Count == 0)
                {
                    MessageBox.Show("请至少选择一个天线");
                    return;
                }


                //默认循环发送命令
                if (m_curInventoryBuffer.bLoopInventory)
                {
                    m_bInventory = false;
                    m_curInventoryBuffer.bLoopInventory = false;
                    m_curInventoryBuffer.bLoopInventoryReal = false;
                    btBufferInventory.BackColor = Color.WhiteSmoke;
                    btBufferInventory.ForeColor = Color.DarkBlue;
                    btBufferInventory.Text = "开始盘存";

                    //this.totalTime.Enabled = false;
                    return;
                }
                else
                {
                    //ISO 18000-6B盘存是否正在运行
                    if (m_bContinue)
                    {
                        if (MessageBox.Show("ISO 18000-6B标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            btnInventoryISO18000_Click(sender, e);
                            return;
                        }
                    }

                    m_bInventory = true;
                    m_curInventoryBuffer.bLoopInventory = true;
                    btBufferInventory.BackColor = Color.DarkBlue;
                    btBufferInventory.ForeColor = Color.White;
                    btBufferInventory.Text = "停止盘存";
                }


                m_curInventoryBuffer.ClearInventoryRealResult();
                lvBufferList.Items.Clear();
                lvBufferList.Items.Clear();
                m_nTotal = 0;

                //this.totalTime.Enabled = true;

                byte btWorkAntenna = m_curInventoryBuffer.lAntenna[m_curInventoryBuffer.nIndexAntenna];
                reader.SetWorkAntenna(m_curSetting.btReadId, btWorkAntenna);
                m_curSetting.btWorkAntenna = btWorkAntenna;

            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btGetBuffer_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.dtTagTable.Rows.Clear();
            lvBufferList.Items.Clear();
            reader.GetInventoryBuffer(m_curSetting.btReadId);
        }

        private void btGetClearBuffer_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.dtTagTable.Rows.Clear();
            lvBufferList.Items.Clear();
            reader.GetAndResetInventoryBuffer(m_curSetting.btReadId);
        }

        private void btClearBuffer_Click(object sender, EventArgs e)
        {
            reader.ResetInventoryBuffer(m_curSetting.btReadId);
            btBufferFresh_Click(sender, e);

        }

        private void btQueryBuffer_Click(object sender, EventArgs e)
        {
            reader.GetInventoryBufferTagCount(m_curSetting.btReadId);
        }

        private void btBufferFresh_Click(object sender, EventArgs e)
        {
            m_curInventoryBuffer.ClearInventoryRealResult();
            lvBufferList.Items.Clear();
            lvBufferList.Items.Clear();
            ledBuffer1.Text = "0";
            ledBuffer2.Text = "0";
            ledBuffer3.Text = "0";
            ledBuffer4.Text = "0";
            ledBuffer5.Text = "0";

            textReadRoundBuffer.Text = "1";
            cbBufferWorkant1.Checked = true;
            cbBufferWorkant2.Checked = false;
            cbBufferWorkant3.Checked = false;
            cbBufferWorkant4.Checked = false;
            labelBufferTagCount.Text = "标签列表：";
        }

        private void btFastInventory_Click(object sender, EventArgs e)
        {
            // 检查是否选择天线
            if (!checkFastInvAnt())
            {
                MessageBox.Show("请至少选择一个天线至少轮询一次，重复次数至少一次。");
                m_bInventory = false;
                m_curInventoryBuffer.bLoopInventory = false;
                m_curInventoryBuffer.bLoopInventoryReal = false;
                btFastInventory.BackColor = Color.WhiteSmoke;
                btFastInventory.ForeColor = Color.DarkBlue;
                btFastInventory.Text = "开始盘存";
                return;
            }

            try
            {
                if (Convert.ToInt32(mFastExeCount.Text) == 0)
                {
                    MessageBox.Show("无效参数运行次数不能为0!");
                    return;
                }

                //默认循环发送命令
                if (m_curInventoryBuffer.bLoopInventory)
                {
                    m_bInventory = false;
                    m_curInventoryBuffer.bLoopInventory = false;
                    m_curInventoryBuffer.bLoopInventoryReal = false;
                    btFastInventory.BackColor = Color.WhiteSmoke;
                    btFastInventory.ForeColor = Color.DarkBlue;
                    btFastInventory.Text = "开始盘存";

                    return;
                }
                else
                {
                    //ISO 18000-6B盘存是否正在运行
                    if (m_bContinue)
                    {
                        if (MessageBox.Show("ISO 18000-6B标签正在盘存，是否停止?", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Cancel)
                        {
                            return;
                        }
                        else
                        {
                            btnInventoryISO18000_Click(sender, e);
                            return;
                        }
                    }

                    m_bInventory = true;
                    m_curInventoryBuffer.bLoopInventory = true;
                    btFastInventory.BackColor = Color.DarkBlue;
                    btFastInventory.ForeColor = Color.White;
                    btFastInventory.Text = "停止盘存";
                }

                this.m_FastSessionCount = 0;

                mFastSessionTimer.Interval = Convert.ToInt32(mFastIntervalTime.Text) + 1;

                //this.mSendFastSwitchTimer.Interval = Convert.ToInt32(m_fast_session_power_time.Text);

                m_FastExeCount = Convert.ToInt32(mFastExeCount.Text);

                m_curInventoryBuffer.ClearInventoryRealResult();
                lvFastList.Items.Clear();

                if (antType8.Checked || antType16.Checked)
                {
                    if (m_new_fast_inventory.Checked)
                    {
                        // 
                        // [antennas AB...GH][interval][reserved][session][target][optimize][ongoing][target quantity][phase][repeat]
                        // [        16      ][    1   ][    5   ][    1  ][    1 ][    1   ][    1  ][    1          ][  1  ][   1  ]
                        m_btAryData = new byte[29];
                        int antIndex = 0;
                        for(int i = 0; i < 16;)
                        {
                            if(fast_inv_ants[antIndex].Enabled)
                            {
                                if(fast_inv_ants[antIndex].Checked)
                                {
                                    m_btAryData[i++] = (byte)antIndex;
                                    m_btAryData[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                }
                                else
                                {
                                    m_btAryData[i++] = (byte)antIndex;
                                    m_btAryData[i++] = 0x00;
                                }
                            }
                            else
                            {
                                Console.WriteLine("#antIndex={0}, Stay={1}", antIndex, fast_inv_stays[antIndex].Text);
                                m_btAryData[i++] = 0xFF;
                                m_btAryData[i++] = 0x00;
                            }
                            antIndex++;
                        }

                        m_btAryData[16] = Convert.ToByte(this.txtInterval.Text);
                        m_btAryData[17] = Convert.ToByte(this.tb_fast_inv_reserved_1.Text);
                        m_btAryData[18] = Convert.ToByte(tb_fast_inv_reserved_2.Text);
                        m_btAryData[19] = Convert.ToByte(this.tb_fast_inv_reserved_3.Text);
                        m_btAryData[20] = Convert.ToByte(this.tb_fast_inv_reserved_4.Text);
                        m_btAryData[21] = Convert.ToByte(this.tb_fast_inv_reserved_5.Text);

                        m_btAryData[22] = Convert.ToByte(m_new_fast_inventory_session.SelectedIndex);
                        m_btAryData[23] = Convert.ToByte(m_new_fast_inventory_flag.SelectedIndex);
                        m_btAryData[24] = Convert.ToByte(m_new_fast_inventory_optimized.Text, 16);
                        m_btAryData[25] = Convert.ToByte(m_new_fast_inventory_continue.Text, 16);
                        m_btAryData[26] = Convert.ToByte(m_new_fast_inventory_target_count.Text);
                        m_btAryData[27] = m_phase_value.Checked ? (byte)0x01 : (byte)0x00;
                        if (txtRepeat.Text.Length == 0)
                        {
                            m_btAryData[28] = 0x00;
                        }
                        else
                        {
                            m_btAryData[28] = Convert.ToByte(txtRepeat.Text);
                        }

                        if (antType16.Checked)
                        {
                            m_btAryData_group2 = new byte[29];
                            for (int i = 0; i < 16;)
                            {
                                if (fast_inv_ants[antIndex].Enabled)
                                {
                                    if (fast_inv_ants[antIndex].Checked)
                                    {
                                        m_btAryData_group2[i++] = (byte)(antIndex - 8);
                                        m_btAryData_group2[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                    }
                                    else
                                    {
                                        m_btAryData_group2[i++] = (byte)antIndex;
                                        m_btAryData_group2[i++] = 0x00;
                                    }
                                }
                                else
                                {
                                    m_btAryData_group2[i++] = 0xFF;
                                    m_btAryData_group2[i++] = 0x00;
                                }
                                antIndex++;
                            }
                            m_btAryData_group2[16] = Convert.ToByte(this.txtInterval.Text);
                            m_btAryData_group2[17] = Convert.ToByte(this.tb_fast_inv_reserved_1.Text);
                            m_btAryData_group2[18] = Convert.ToByte(this.tb_fast_inv_reserved_2.Text);
                            m_btAryData_group2[19] = Convert.ToByte(this.tb_fast_inv_reserved_3.Text);
                            m_btAryData_group2[20] = Convert.ToByte(this.tb_fast_inv_reserved_4.Text);
                            m_btAryData_group2[21] = Convert.ToByte(this.tb_fast_inv_reserved_5.Text);

                            m_btAryData_group2[22] = Convert.ToByte(m_new_fast_inventory_session.SelectedIndex);
                            m_btAryData_group2[23] = Convert.ToByte(m_new_fast_inventory_flag.SelectedIndex);
                            m_btAryData_group2[24] = Convert.ToByte(m_new_fast_inventory_optimized.Text, 16);
                            m_btAryData_group2[25] = Convert.ToByte(m_new_fast_inventory_continue.Text, 16);
                            m_btAryData_group2[26] = Convert.ToByte(m_new_fast_inventory_target_count.Text);
                            m_btAryData_group2[27] = m_phase_value.Checked ? (byte)0x01 : (byte)0x00;

                            if (txtRepeat.Text.Length == 0)
                            {
                                m_btAryData_group2[28] = 0x00;
                            }
                            else
                            {
                                m_btAryData_group2[28] = Convert.ToByte(txtRepeat.Text);
                            }
                        }
                    }
                    else
                    {
                        // 
                        // [antennas AB...GH][interval][repeat]
                        // [        16      ][    1   ][   1  ]
                        m_btAryData = new byte[18];
                        int antIndex = 0;
                        for (int i = 0; i < 16;)
                        {
                            if (fast_inv_ants[antIndex].Enabled)
                            {
                                if (fast_inv_ants[antIndex].Checked)
                                {
                                    m_btAryData[i++] = (byte)antIndex;
                                    m_btAryData[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                }
                                else
                                {
                                    m_btAryData[i++] = (byte)antIndex;
                                    m_btAryData[i++] = 0x00;
                                }
                            }
                            else
                            {
                                m_btAryData[i++] = 0xFF;
                                m_btAryData[i++] = 0x00;
                            }
                            antIndex++;
                        }
                        m_btAryData[16] = Convert.ToByte(this.txtInterval.Text);
                        if (txtRepeat.Text.Length == 0)
                        {
                            m_btAryData[17] = 0x00;
                        }
                        else
                        {
                            m_btAryData[17] = Convert.ToByte(txtRepeat.Text);
                        }
                        if (antType16.Checked)
                        {
                            m_btAryData_group2 = new byte[18];
                            for (int i = 0; i < 16;)
                            {
                                if (fast_inv_ants[antIndex].Enabled)
                                {
                                    if (fast_inv_ants[antIndex].Checked)
                                    {
                                        m_btAryData_group2[i++] = (byte)(antIndex - 8);
                                        m_btAryData_group2[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                    }
                                    else
                                    {
                                        m_btAryData_group2[i++] = (byte)antIndex;
                                        m_btAryData_group2[i++] = 0x00;
                                    }
                                }
                                else
                                {
                                    m_btAryData_group2[i++] = 0xFF;
                                    m_btAryData_group2[i++] = 0x00;
                                }
                                antIndex++;
                            }
                            m_btAryData_group2[16] = Convert.ToByte(this.txtInterval.Text);
                            if (txtRepeat.Text.Length == 0)
                            {
                                m_btAryData_group2[17] = 0x00;
                            }
                            else
                            {
                                m_btAryData_group2[17] = Convert.ToByte(txtRepeat.Text);
                            }
                        }
                    }
                }

                if (antType4.Checked)
                {
                    if (m_new_fast_inventory.Checked)
                    {
                        // 
                        // [antennas AB...GH][interval][reserved][session][target][optimize][ongoing][target quantity][phase][repeat]
                        // [        16      ][    1   ][    5   ][    1  ][    1 ][    1   ][    1  ][    1          ][  1  ][   1  ]
                        m_btAryData_4 = new byte[29];
                        int antIndex = 0;
                        for (int i = 0; i < 16;)
                        {
                            if (fast_inv_ants[antIndex].Enabled)
                            {
                                if (fast_inv_ants[antIndex].Checked)
                                {
                                    m_btAryData_4[i++] = (byte)antIndex;
                                    m_btAryData_4[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                }
                                else
                                {
                                    m_btAryData_4[i++] = (byte)antIndex;
                                    m_btAryData_4[i++] = 0x00;
                                }
                            }
                            else
                            {
                                m_btAryData_4[i++] = 0xFF;
                                m_btAryData_4[i++] = 0x00;
                            }
                            antIndex++;
                        }
                        if (txtInterval.Text.Length == 0)
                        {
                            m_btAryData_4[16] = 0x00;
                        }
                        else
                        {
                            m_btAryData_4[16] = Convert.ToByte(txtInterval.Text);
                        }

                        m_btAryData_4[17] = Convert.ToByte(this.tb_fast_inv_reserved_1.Text);
                        m_btAryData_4[18] = Convert.ToByte(this.tb_fast_inv_reserved_2.Text);
                        m_btAryData_4[19] = Convert.ToByte(this.tb_fast_inv_reserved_3.Text);
                        m_btAryData_4[20] = Convert.ToByte(this.tb_fast_inv_reserved_4.Text);
                        m_btAryData_4[21] = Convert.ToByte(this.tb_fast_inv_reserved_5.Text);

                        m_btAryData_4[22] = Convert.ToByte(m_new_fast_inventory_session.SelectedIndex);
                        m_btAryData_4[23] = Convert.ToByte(m_new_fast_inventory_flag.SelectedIndex);
                        m_btAryData_4[24] = Convert.ToByte(m_new_fast_inventory_optimized.Text, 16);
                        m_btAryData_4[25] = Convert.ToByte(m_new_fast_inventory_continue.Text, 16);
                        m_btAryData_4[26] = Convert.ToByte(m_new_fast_inventory_target_count.Text);
                        m_btAryData_4[27] = m_phase_value.Checked ? (byte)0x01 : (byte)0x00;

                        if (txtRepeat.Text.Length == 0)
                        {
                            m_btAryData_4[28] = 0x00;
                        }
                        else
                        {
                            m_btAryData_4[28] = Convert.ToByte(txtRepeat.Text);
                        }
                    }
                    else
                    {
                        // 
                        // [antennas ABCD][interval][repeat]
                        // [      8      ][    1   ][   1  ]
                        m_btAryData_4 = new byte[10];

                        int antIndex = 0;
                        for (int i = 0; i < 8;)
                        {
                            if (fast_inv_ants[antIndex].Enabled)
                            {
                                if (fast_inv_ants[antIndex].Checked)
                                {
                                    m_btAryData_4[i++] = (byte)antIndex;
                                    m_btAryData_4[i++] = Convert.ToByte(fast_inv_stays[antIndex].Text);
                                }
                                else
                                {
                                    m_btAryData_4[i++] = (byte)antIndex;
                                    m_btAryData_4[i++] = 0x00;
                                }
                            }
                            else
                            {
                                m_btAryData_4[i++] = 0xFF;
                                m_btAryData_4[i++] = 0x00;
                            }
                            antIndex++;
                        }
                        if (txtInterval.Text.Length == 0)
                        {
                            m_btAryData_4[8] = 0x00;
                        }
                        else
                        {
                            m_btAryData_4[8] = Convert.ToByte(txtInterval.Text);
                        }

                        if (txtRepeat.Text.Length == 0)
                        {
                            m_btAryData_4[9] = 0x00;
                        }
                        else
                        {
                            m_btAryData_4[9] = Convert.ToByte(txtRepeat.Text);
                        }
                    }
                }

                m_nTotal = 0;

                m_nSwitchTotal = 0;
                m_nSwitchTime = 0;
                m_startConsumTime = DateTime.Now;
                m_curSetting.btAntGroup = 0;

                if (mDynamicPoll.Checked)
                {
                    m_nRepeat2 = false;
                    m_nRepeat12 = false;
                    m_nRepeat1 = false;
                    //Console.WriteLine("轮询模式-----第一次开始设置功率");
                    reader.SetTempOutpower(m_curSetting.btReadId, Convert.ToByte(m_new_fast_inventory_power1.Text));
                }
                else
                {
                    if (antType4.Checked)
                    {
                        //Console.WriteLine("第一次开始四天线快速盘存");
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_4);
                    }
                    else if (antType8.Checked)
                    {
                        //Console.WriteLine("第一次开始八天线快速盘存");
                        reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                    }
                    else if (antType16.Checked)
                    {
                        //Console.WriteLine("第一次开始16天线, 设置天线组" + m_curSetting.btAntGroup);
                        reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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
            m_curInventoryBuffer.ClearInventoryRealResult();
            lvFastList.Items.Clear();
            lvFastList.Items.Clear();
            ledFast1.Text = "0";
            ledFast2.Text = "0";
            ledFast3.Text = "0";
            ledFast4.Text = "0";
            ledFast5.Text = "0";
            txtFastMinRssi.Text = "";
            txtFastMaxRssi.Text = "";
            txtFastTagList.Text = "标签列表：";

            m_new_fast_inventory.Checked = false;

            mDynamicPoll.Checked = false;

            // Todo: refresh antenna
            txtInterval.Text = "0";
            txtRepeat.Text = "1";

            mFastExeCount.Text = "1";
            mFastIntervalTime.Text = "0";

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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

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
            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

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

        private void timerInventory_Tick(object sender, EventArgs e)
        {
            m_nReceiveFlag++;
            if (m_nReceiveFlag >= 5)
            {
                RunLoopInventroy();
                m_nReceiveFlag = 0;
            }
        }

        private void totalTimeDisplay(object sender, EventArgs e)
        {
            TimeSpan sp = DateTime.Now - m_curInventoryBuffer.dtStartInventory;
            int totalTime = (int)(sp.Ticks / 10000);

            ledReal5.Text = FormatLongToTimeStr(totalTime);
            //RefreshInventoryReal(0x00);
        }



        private void tabEpcTest_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void lrtxtLog_TextChanged(object sender, EventArgs e)
        {

        }

        private void button6_Click(object sender, EventArgs e)
        {

        }

        private void cbRealWorkant1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void cmbAntSelect3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click_1(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

        private void ProcessTagMask(Reader.MessageTran msgTran)
        {
            string strCmd = "操作过滤";
            string strErrorCode = string.Empty;
            if (msgTran.AryData.Length == 1)
            {
                if (msgTran.AryData[0] == (byte)0x10)
                {
                    WriteLog(lrtxtLog, "命令执行成功！", 0);
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
                if (comboBox12.SelectedIndex == -1 || comboBox15.SelectedIndex == -1 || comboBox14.SelectedIndex == -1 || comboBox13.SelectedIndex == -1)
                {
                    MessageBox.Show("Target Action Membank must be selected");
                    return;
                }
                byte btMaskNo = (byte)(comboBox12.SelectedIndex + 1);
                byte btTarget = (byte)comboBox15.SelectedIndex;
                byte btAction = (byte)comboBox14.SelectedIndex;
                byte btMembank = (byte)comboBox13.SelectedIndex;

                string strMaskValue = hexTextBox9.Text.Trim();
                string[] maskValue = CCommondMethod.StringToStringArray(strMaskValue.ToUpper(), 2);


                byte btStartAddress = Convert.ToByte(textBox11.Text);
                int intStartAdd = Convert.ToInt32(textBox11.Text);
                byte btMaskLen = Convert.ToByte(textBox12.Text);
                int intMaskLen = Convert.ToInt32(textBox12.Text);

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

        private void button8_Click(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            Encoder encoder = Encoding.UTF8.GetEncoder();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (txt_format_rb.Checked)
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Title = "Save an text File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    //String strHead = "---------------------------------------------------------------------------------------------------------------------------\r\n";

                    DataTable table = ListViewToDataTable(lvRealList);
                    String title = String.Empty;
                    foreach (ColumnHeader header in lvRealList.Columns)
                    {
                        title += header.Text + "\t";
                    }
                    title += "\r\n";
                    byte[] byteTitile = System.Text.Encoding.UTF8.GetBytes(title);
                    fs.Write(byteTitile, 0, byteTitile.Length);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        String strData = String.Empty;
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j != table.Columns.Count - 1)
                            {
                                strData += row[j].ToString() + "\t";
                            }
                            else
                            {
                                strData += row[j].ToString() + "\t\r\n";
                            }
                        }
                        Char[] charData = strData.ToString().ToArray();
                        Byte[] byData = new byte[charData.Length];
                        encoder.GetBytes(charData, 0, charData.Length, byData, 0, true);
                        fs.Write(byData, 0, byData.Length);
                    }
                    fs.Close();
                    MessageBox.Show("数据导出成功！");
                }
            }
            else if (excel_format_rb.Checked)
            {
                saveFileDialog1.Filter = "97-2003文档（*.xls）|*.xls|2007文档(*.xlsx)|*.xlsx";
                saveFileDialog1.Title = "Save an excel File";
                saveFileDialog1.ShowDialog();

                DataTable tmp = ListViewToDataTable(lvRealList);

                if (saveFileDialog1.FileName != "")
                {
                    string suffix = saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf(".") + 1, saveFileDialog1.FileName.Length - saveFileDialog1.FileName.LastIndexOf(".") - 1);
                    if (suffix == "xls")
                    {
                        RenderToExcel(tmp, saveFileDialog1.FileName);
                    }
                    else
                    {
                        TableToExcelForXLSX(tmp, saveFileDialog1.FileName);
                    }
                }
            }
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

        /// <summary>
        /// 导出数据到excel2003中
        /// </summary>
        /// <param name="table"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        public bool RenderToExcel(DataTable table, string filename)
        {
            MemoryStream ms = new MemoryStream();
            using (table)
            {
                NPOI.HSSF.UserModel.HSSFWorkbook workbook = new HSSFWorkbook();

                ISheet sheet = workbook.CreateSheet();

                IRow headerRow = sheet.CreateRow(0);
                // handling header. 
                foreach (DataColumn column in table.Columns)
                    headerRow.CreateCell(column.Ordinal).SetCellValue(column.Caption);//If Caption not set, returns the ColumnName value 

                // handling value. 
                int rowIndex = 1;

                foreach (DataRow row in table.Rows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);

                    foreach (DataColumn column in table.Columns)
                    {
                        dataRow.CreateCell(column.Ordinal).SetCellValue(row[column].ToString());
                    }
                    //  proBar.progressBar1.Value = proBar.progressBar1.Value+1;

                    rowIndex++;
                }
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                try
                {
                    SaveToFile(ms, filename);
                    MessageBox.Show("数据导出成功！");
                    return true;
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                    return false;
                }
            }
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

        /// <summary>
        /// 导出数据到excel2007中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool TableToExcelForXLSX(DataTable dt, string file)
        {
            XSSFWorkbook xssfworkbook = new XSSFWorkbook();
            ISheet sheet = xssfworkbook.CreateSheet("Test");

            //表头   
            IRow row = sheet.CreateRow(0);
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                ICell cell = row.CreateCell(i);
                cell.SetCellValue(dt.Columns[i].ColumnName);
            }

            //数据   
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                IRow row1 = sheet.CreateRow(i + 1);
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    ICell cell = row1.CreateCell(j);
                    cell.SetCellValue(dt.Rows[i][j].ToString());
                }
            }

            //转为字节数组   
            MemoryStream stream = new MemoryStream();
            xssfworkbook.Write(stream);
            var buf = stream.ToArray();

            //保存为Excel文件  
            try
            {
                using (FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write))
                {
                    fs.Write(buf, 0, buf.Length);
                    fs.Flush();
                }
                MessageBox.Show("数据导出成功！");
                return true;
            }

            catch (SystemException ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
        }

        //save tag as execel


        private void button6_Click_1(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            Encoder encoder = Encoding.UTF8.GetEncoder();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (txt_format_buffer_rb.Checked)
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Title = "Save an text File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    //String strHead = "---------------------------------------------------------------------------------------------------------------------------\r\n";

                    DataTable table = ListViewToDataTable(lvBufferList);
                    String title = String.Empty;
                    foreach (ColumnHeader header in lvBufferList.Columns)
                    {
                        title += header.Text + "\t";
                    }
                    title += "\r\n";
                    byte[] byteTitile = System.Text.Encoding.UTF8.GetBytes(title);
                    fs.Write(byteTitile, 0, byteTitile.Length);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        String strData = String.Empty;
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j != table.Columns.Count - 1)
                            {
                                strData += row[j].ToString() + "\t";
                            }
                            else
                            {
                                strData += row[j].ToString() + "\t\r\n";
                            }
                        }
                        Char[] charData = strData.ToString().ToArray();
                        Byte[] byData = new byte[charData.Length];
                        encoder.GetBytes(charData, 0, charData.Length, byData, 0, true);
                        fs.Write(byData, 0, byData.Length);
                    }
                    fs.Close();
                    MessageBox.Show("数据导出成功！");
                }
            }
            else if (excel_format_buffer_rb.Checked)
            {
                saveFileDialog1.Filter = "97-2003文档（*.xls）|*.xls|2007文档(*.xlsx)|*.xlsx";
                saveFileDialog1.Title = "Save an excel File";
                saveFileDialog1.ShowDialog();

                DataTable tmp = ListViewToDataTable(lvBufferList);

                if (saveFileDialog1.FileName != "")
                {
                    string suffix = saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf(".") + 1, saveFileDialog1.FileName.Length - saveFileDialog1.FileName.LastIndexOf(".") - 1);
                    if (suffix == "xls")
                    {
                        RenderToExcel(tmp, saveFileDialog1.FileName);
                    }
                    else
                    {
                        TableToExcelForXLSX(tmp, saveFileDialog1.FileName);
                    }
                }
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            // Displays a SaveFileDialog so the user can save the Image
            // assigned to Button2.
            Encoder encoder = Encoding.UTF8.GetEncoder();
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            if (txt_format_fast_rb.Checked)
            {
                saveFileDialog1.Filter = "txt files (*.txt)|*.txt";
                saveFileDialog1.Title = "Save an text File";
                saveFileDialog1.ShowDialog();

                // If the file name is not an empty string open it for saving.
                if (saveFileDialog1.FileName != "")
                {
                    // Saves the Image via a FileStream created by the OpenFile method.
                    System.IO.FileStream fs =
                       (System.IO.FileStream)saveFileDialog1.OpenFile();
                    // Saves the Image in the appropriate ImageFormat based upon the
                    // File type selected in the dialog box.
                    // NOTE that the FilterIndex property is one-based.
                    //String strHead = "---------------------------------------------------------------------------------------------------------------------------\r\n";

                    DataTable table = ListViewToDataTable(lvFastList);
                    String title = String.Empty;
                    foreach (ColumnHeader header in lvFastList.Columns)
                    {
                        title += header.Text + "\t";
                    }
                    title += "\r\n";
                    byte[] byteTitile = System.Text.Encoding.UTF8.GetBytes(title);
                    fs.Write(byteTitile, 0, byteTitile.Length);
                    for (int i = 0; i < table.Rows.Count; i++)
                    {
                        DataRow row = table.Rows[i];
                        String strData = String.Empty;
                        for (int j = 0; j < table.Columns.Count; j++)
                        {
                            if (j != table.Columns.Count - 1)
                            {
                                strData += row[j].ToString() + "\t";
                            }
                            else
                            {
                                strData += row[j].ToString() + "\t\r\n";
                            }
                        }
                        Char[] charData = strData.ToString().ToArray();
                        Byte[] byData = new byte[charData.Length];
                        encoder.GetBytes(charData, 0, charData.Length, byData, 0, true);
                        fs.Write(byData, 0, byData.Length);
                    }
                    fs.Close();
                    MessageBox.Show("数据导出成功！");
                }
            }
            else if (excel_format_fast_rb.Checked)
            {
                saveFileDialog1.Filter = "97-2003文档（*.xls）|*.xls|2007文档(*.xlsx)|*.xlsx";
                saveFileDialog1.Title = "Save an excel File";
                saveFileDialog1.ShowDialog();

                DataTable tmp = ListViewToDataTable(lvFastList);

                if (saveFileDialog1.FileName != "")
                {
                    string suffix = saveFileDialog1.FileName.Substring(saveFileDialog1.FileName.LastIndexOf(".") + 1, saveFileDialog1.FileName.Length - saveFileDialog1.FileName.LastIndexOf(".") - 1);
                    if (suffix == "xls")
                    {
                        RenderToExcel(tmp, saveFileDialog1.FileName);
                    }
                    else
                    {
                        TableToExcelForXLSX(tmp, saveFileDialog1.FileName);
                    }
                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox1.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox1.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox1.Text = "";
                }
            }


            if (antType4.Checked)
            {
                textBox2.Text = textBox1.Text;
                textBox3.Text = textBox1.Text;
                textBox4.Text = textBox1.Text;
            }

            if (antType8.Checked)
            {
                textBox2.Text = textBox1.Text;
                textBox3.Text = textBox1.Text;
                textBox4.Text = textBox1.Text;

                textBox7.Text = textBox1.Text;
                textBox8.Text = textBox1.Text;
                textBox9.Text = textBox1.Text;
                textBox10.Text = textBox1.Text;

            }

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            if (textBox2.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox2.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox2.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox2.Text = "";
                }
            }
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            if (textBox3.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox3.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox3.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox3.Text = "";
                }
            }
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            if (textBox4.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox4.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox4.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox4.Text = "";
                }
            }
        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {
            if (textBox7.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox7.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox7.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox7.Text = "";
                }
            }
        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {
            if (textBox8.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox8.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox8.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox8.Text = "";
                }
            }
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            if (textBox9.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox9.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox9.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox9.Text = "";
                }
            }
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            if (textBox10.Text.Length == 0)
            {

            }
            else
            {
                try
                {

                    int tmp = Convert.ToInt16(textBox10.Text);
                    if (tmp > 33 || tmp < 0)
                    {
                        MessageBox.Show("参数异常!");
                        textBox10.Text = "";
                        return;
                    }
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    textBox10.Text = "";
                }
            }
        }

        private void antType_CheckedChanged(object sender, EventArgs e)
        {
            int channels = 1;
            RadioButton btn = (RadioButton)sender;
            if (!btn.Checked)
                return;
            switch (btn.Name)
            {
                case "antType1":
                    channels = 1;
                    antType1_CheckedChanged(sender, e);
                    fast_inv_v2_channel_1.Checked = true;
                    cmbx_realinv_workant.Items.Clear();
                    cmbx_realinv_workant.Items.Add("天线1");
                    cmbx_realinv_workant.SelectedIndex = 0;
                    break;
                case "antType4":
                    channels = 4;
                    antType4_CheckedChanged(sender, e);
                    fast_inv_v2_channel_4.Checked = true;
                    cmbx_realinv_workant.Items.Clear();
                    cmbx_realinv_workant.Items.AddRange(new String[] { "天线1", "天线2", "天线3", "天线4" });
                    cmbx_realinv_workant.SelectedIndex = 0;
                    break;
                case "antType8":
                    channels = 8;
                    antType8_CheckedChanged(sender, e);
                    fast_inv_v2_channel_8.Checked = true;
                    cmbx_realinv_workant.Items.Clear();
                    cmbx_realinv_workant.Items.AddRange(new String[] { "天线1", "天线2", "天线3", "天线4", "天线5", "天线6", "天线7", "天线8" });
                    cmbx_realinv_workant.SelectedIndex = 0;
                    break;
                case "antType16":
                    channels = 16;
                    antType16_CheckedChanged(sender, e);
                    fast_inv_v2_channel_16.Checked = true;

                    cmbx_realinv_workant.Items.Clear();
                    cmbx_realinv_workant.Items.AddRange(new String[] { 
                        "天线1", "天线2", "天线3", "天线4", "天线5", "天线6", "天线7", "天线8",
                        "天线9", "天线10", "天线11", "天线12", "天线13", "天线14", "天线15", "天线16"
                    });
                    cmbx_realinv_workant.SelectedIndex = 0;
                    break;
            }

            for (int i = 0; i < 16; i++)
            {
                if (i < channels)
                {
                    fast_inv_ants[i].Visible = true;
                    fast_inv_ants[i].Enabled = true;
                    if(i == 0) // 默认选1
                        fast_inv_ants[i].Checked = true;

                    fast_inv_stays[i].Visible = true;
                    fast_inv_stays[i].Enabled = true;
                }
                else
                {
                    fast_inv_ants[i].Visible = false;
                    fast_inv_ants[i].Enabled = false;
                    fast_inv_ants[i].Checked = false;

                    fast_inv_stays[i].Visible = false;
                    fast_inv_stays[i].Enabled = false;

                }
            }
        }

        private void antType1_CheckedChanged(object sender, EventArgs e)
        {
            if (antType1.Checked)
            {
                label_fast_inv_ant_title_c2.Visible = false;
                label_fast_inv_stay_title_c2.Visible = false;

                //disable fast ant switch inventory.
                btFastInventory.Enabled = false;
                //disable fast ant switch inventory.
                // output power 
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                tb_dbm_9.Enabled = false;
                tb_dbm_10.Enabled = false;
                tb_dbm_11.Enabled = false;
                tb_dbm_12.Enabled = false;
                tb_dbm_13.Enabled = false;
                tb_dbm_14.Enabled = false;
                tb_dbm_15.Enabled = false;
                tb_dbm_16.Enabled = false;

                columnHeader40.Text = "识别次数";

                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1"});
                this.cmbWorkAnt.SelectedIndex = 0;


                //select ant
                cbBufferWorkant2.Checked = false;
                cbBufferWorkant3.Checked = false;
                cbBufferWorkant4.Checked = false;

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;


                //init selelct ant


                cbBufferWorkant1.Enabled = false;
                cbBufferWorkant2.Enabled = false;
                cbBufferWorkant3.Enabled = false;
                cbBufferWorkant4.Enabled = false;

                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;
            }
        }

        private void antType4_CheckedChanged(object sender, EventArgs e)
        {
            if (antType4.Checked)
            {
                label_fast_inv_ant_title_c2.Visible = false;
                label_fast_inv_stay_title_c2.Visible = false;

                //Enable fast ant switch inventory.
                btFastInventory.Enabled = true;
                //Enable fast ant switch inventory.

                //set fast 4 ant
                columnHeader34.Text = "识别次数(ANT1/2/3/4)";
                //init selelct ant
                columnHeader40.Text = "识别次数(ANT1/2/3/4)";

                //set work ant
                this.cmbWorkAnt.Items.Clear();
                this.cmbWorkAnt.Items.AddRange(new object[] {
                "天线 1",
                "天线 2",
                "天线 3",
                "天线 4"});
                this.cmbWorkAnt.SelectedIndex = 0;

                // output power 
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox7.Enabled = false;
                textBox8.Enabled = false;
                textBox9.Enabled = false;
                textBox10.Enabled = false;
                tb_dbm_9.Enabled = false;
                tb_dbm_10.Enabled = false;
                tb_dbm_11.Enabled = false;
                tb_dbm_12.Enabled = false;
                tb_dbm_13.Enabled = false;
                tb_dbm_14.Enabled = false;
                tb_dbm_15.Enabled = false;
                tb_dbm_16.Enabled = false;

                cbBufferWorkant1.Enabled = true;
                cbBufferWorkant2.Enabled = true;
                cbBufferWorkant3.Enabled = true;
                cbBufferWorkant4.Enabled = true;

                checkBox1.Enabled = false;
                checkBox2.Enabled = false;
                checkBox3.Enabled = false;
                checkBox4.Enabled = false;

                //select ant
                cbBufferWorkant2.Checked = false;
                cbBufferWorkant3.Checked = false;
                cbBufferWorkant4.Checked = false;

                checkBox1.Checked = false;
                checkBox2.Checked = false;
                checkBox3.Checked = false;
                checkBox4.Checked = false;

                /*
                cmbAntSelect2.SelectedIndex = 8;
                cmbAntSelect3.SelectedIndex = 8;
                cmbAntSelect4.SelectedIndex = 8;

                comboBox1.SelectedIndex = 8;
                comboBox2.SelectedIndex = 8;
                comboBox3.SelectedIndex = 8;
                comboBox4.SelectedIndex = 8;
                 */
            }
        }

        private void antType8_CheckedChanged(object sender, EventArgs e)
        {
            if (antType8.Checked)
            {
                label_fast_inv_ant_title_c2.Visible = false;
                label_fast_inv_stay_title_c2.Visible = false;

                //Enable fast ant switch inventory.
                btFastInventory.Enabled = true;
                //Enable fast ant switch inventory.

                //set fast 8 ant
                columnHeader34.Text = "识别次数(ANT1/2/3/4/5/6/7/8)";
                columnHeader40.Text = "识别次数(ANT1/2/3/4/5/6/7/8)";
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
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                tb_dbm_9.Enabled = false;
                tb_dbm_10.Enabled = false;
                tb_dbm_11.Enabled = false;
                tb_dbm_12.Enabled = false;
                tb_dbm_13.Enabled = false;
                tb_dbm_14.Enabled = false;
                tb_dbm_15.Enabled = false;
                tb_dbm_16.Enabled = false;

                cbBufferWorkant1.Enabled = true;
                cbBufferWorkant2.Enabled = true;
                cbBufferWorkant3.Enabled = true;
                cbBufferWorkant4.Enabled = true;

                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void antType16_CheckedChanged(object sender, EventArgs e)
        {
            if (antType16.Checked)
            {
                label_fast_inv_ant_title_c2.Visible = true;
                label_fast_inv_stay_title_c2.Visible = true;

                //Enable fast ant switch inventory.
                btFastInventory.Enabled = true;
                //Enable fast ant switch inventory.

                //set fast 16 ant
                columnHeader34.Text = "识别次数(ANT1/2/3/4/5/6/7/8/9/10/11/12/13/14/15/16)";
                columnHeader40.Text = "识别次数(ANT1/2/3/4/5/6/7/8/9/10/11/12/13/14/15/16)";
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
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox7.Enabled = true;
                textBox8.Enabled = true;
                textBox9.Enabled = true;
                textBox10.Enabled = true;
                tb_dbm_9.Enabled = true;
                tb_dbm_10.Enabled = true;
                tb_dbm_11.Enabled = true;
                tb_dbm_12.Enabled = true;
                tb_dbm_13.Enabled = true;
                tb_dbm_14.Enabled = true;
                tb_dbm_15.Enabled = true;
                tb_dbm_16.Enabled = true;

                cbBufferWorkant1.Enabled = true;
                cbBufferWorkant2.Enabled = true;
                cbBufferWorkant3.Enabled = true;
                cbBufferWorkant4.Enabled = true;

                checkBox1.Enabled = true;
                checkBox2.Enabled = true;
                checkBox3.Enabled = true;
                checkBox4.Enabled = true;
            }
        }

        private void label125_Click(object sender, EventArgs e)
        {

        }

        private void m_session_q_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (m_session_q_cb.Checked)
            {
                m_session_sl_cb.Checked = true;
                m_session_start_q.Enabled = true;
                m_session_min_q.Enabled = true;
                m_session_max_q.Enabled = true;
                m_min_q_content.Enabled = true;
                m_start_q_content.Enabled = true;
                m_max_q_content.Enabled = true;
            }
            else
            {
                m_session_start_q.Enabled = false;
                m_session_min_q.Enabled = false;
                m_session_max_q.Enabled = false;
                m_min_q_content.Enabled = false;
                m_start_q_content.Enabled = false;
                m_max_q_content.Enabled = false;
            }
        }

        private void m_session_sl_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (m_session_sl_cb.Checked)
            {
                sessionInventoryrb.Checked = true;
                //cbRealSession.Checked = true;
                Duration.Enabled = true;
                m_session_sl.Enabled = true;
                m_sl_content.Enabled = true;
                mSessionExeTime.Enabled = true;
            }
            else
            {
                Duration.Enabled = false;
                m_session_q_cb.Checked = false;
                m_session_sl.Enabled = false;
                m_sl_content.Enabled = false;
                mSessionExeTime.Enabled = false;
            }
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void autoInventoryrb_CheckedChanged(object sender, EventArgs e)
        {
            if (autoInventoryrb.Checked)
            {
                label97.Enabled = false;
                label98.Enabled = false;
                cmbSession.Enabled = false;
                cmbTarget.Enabled = false;

                m_session_sl_cb.Checked = false;
            }
        }

        private void sessionInventoryrb_CheckedChanged(object sender, EventArgs e)
        {
            if (sessionInventoryrb.Checked)
            {
                label97.Enabled = true;
                label98.Enabled = true;
                cmbSession.Enabled = true;
                cmbTarget.Enabled = true;
            }
        }

        private void SendFastSwitchTimer_Tick(object sender, EventArgs e)
        {
            this.mSendFastSwitchTimer.Enabled = false;
            this.mSendFastSwitchTimer.Stop();
            if (antType4.Checked)
            {
                reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_4);
            }
            else if (antType8.Checked)
            {
                reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
            }
        }

        private void mFastSessionTimer_Tick(object sender, EventArgs e)
        {
            mFastSessionTimer.Enabled = false;
            mFastSessionTimer.Stop();
            m_startConsumTime = DateTime.Now;

            //lvFastList.Items.Clear();
            this.m_FastSessionCount = 0;

            m_curSetting.btAntGroup = 0;
            if (mDynamicPoll.Checked)
            {
                m_nRepeat2 = false;
                m_nRepeat12 = false;
                m_nRepeat1 = false;
                if (antType16.Checked)
                {
                    //Console.WriteLine("定时器时间到，轮询模式------16天线设置天线组" + m_curSetting.btAntGroup);
                    reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                }
                else
                {
                    //Console.WriteLine("定时器时间到，轮询模式------设置功率" + Convert.ToByte(m_new_fast_inventory_power1.Text));
                    reader.SetTempOutpower(m_curSetting.btReadId, Convert.ToByte(m_new_fast_inventory_power1.Text));
                }
            }
            else
            {
                if (antType4.Checked)
                {
                    //Console.WriteLine("定时器时间到，四天线快速盘存");
                    reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData_4);
                }
                else if (antType8.Checked)
                {
                    //Console.WriteLine("定时器时间到，八天线天线快速盘存");
                    reader.FastSwitchInventory(m_curSetting.btReadId, m_btAryData);
                }
                else if (antType16.Checked)
                {
                    //Console.WriteLine("定时器时间到，十六天线天线开始设置天线组" + m_curSetting.btAntGroup);
                    reader.SetReaderAntGroup(m_curSetting.btReadId, m_curSetting.btAntGroup);
                }
            }

        }

        private void lvRealList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void mFastSession_CheckedChanged(object sender, EventArgs e)
        {
            /*
            if (mFastSession.Checked)
            {
                mFastSessionSelect.Enabled = true;
            }
            else
            {
                mFastSessionSelect.Enabled = false;
            }*/
        }

        private void label61_Click(object sender, EventArgs e)
        {

        }

        private void mPower4_TextChanged(object sender, EventArgs e)
        {

        }

        private void m_new_fast_inventory_CheckedChanged(object sender, EventArgs e)
        {
            if (m_new_fast_inventory.Checked)
            {
                this.m_new_fast_inventory_flag.Enabled = true;
                this.m_new_fast_inventory_session.Enabled = true;
                this.m_phase_value.Enabled = true;

                tb_fast_inv_reserved_1.Enabled = true;
                tb_fast_inv_reserved_2.Enabled = true;
                tb_fast_inv_reserved_3.Enabled = true;
                tb_fast_inv_reserved_4.Enabled = true;
                tb_fast_inv_reserved_5.Enabled = true;

                mReserve.Enabled = true;

                m_new_fast_inventory_optimized.Enabled = true;
                mOpitimized.Enabled = true;

                m_new_fast_inventory_continue.Enabled = true;
                mContiue.Enabled = true;

                m_new_fast_inventory_target_count.Enabled = true;
                mTargetQuantity.Enabled = true;

            }
            else
            {
                this.m_new_fast_inventory_flag.Enabled = false;
                this.m_new_fast_inventory_session.Enabled = false;
                this.m_phase_value.Enabled = false;
                this.m_phase_value.Checked = false;

                tb_fast_inv_reserved_1.Enabled = false;
                tb_fast_inv_reserved_2.Enabled = false;
                tb_fast_inv_reserved_3.Enabled = false;
                tb_fast_inv_reserved_4.Enabled = false;
                tb_fast_inv_reserved_5.Enabled = false;

                tb_fast_inv_reserved_1.Text = "0";
                tb_fast_inv_reserved_2.Text = "0";
                tb_fast_inv_reserved_3.Text = "0";
                tb_fast_inv_reserved_4.Text = "0";
                tb_fast_inv_reserved_5.Text = "0";

                mReserve.Enabled = false;

                m_new_fast_inventory_optimized.Enabled = false;
                m_new_fast_inventory_optimized.Text = "0";
                mOpitimized.Enabled = false;

                m_new_fast_inventory_continue.Enabled = false;
                m_new_fast_inventory_continue.Text = "0";
                mContiue.Enabled = false;

                m_new_fast_inventory_target_count.Enabled = false;
                m_new_fast_inventory_target_count.Text = "0";
                mTargetQuantity.Enabled = false;
            }
        }

        private void m_phase_value_CheckedChanged(object sender, EventArgs e)
        {
            this.refreshFastListView();
            if (m_phase_value.Checked)
            {
                m_nPhaseOpened = true;
            }
            else
            {
                m_nPhaseOpened = false;
            }
        }

        private void refreshFastListView()
        {
            if (this.m_phase_value.Checked)
            {
                this.columnHeader31.Width = 53;
                this.columnHeader32.Width = 390;
                this.columnHeader33.Width = 61;
                this.columnHeader34.Width = 211;
                this.columnHeader35.Width = 89;
                this.columnHeader356.Width = 65;
                this.columnHeader36.Width = 117;
            }
            else
            {
                this.columnHeader31.Width = 56;
                this.columnHeader32.Width = 420;
                this.columnHeader33.Width = 65;
                this.columnHeader34.Width = 226;
                this.columnHeader35.Width = 96;
                this.columnHeader356.Width = 0;
                this.columnHeader36.Width = 125;
            }
        }

        private void mDynamicPoll_CheckedChanged(object sender, EventArgs e)
        {
            if (mDynamicPoll.Checked)
            {
                mRepeat1.Enabled = true;
                mRepeat2.Enabled = true;
                mRepeatPower1.Enabled = true;
                mRepeatPower2.Enabled = true;

                m_new_fast_inventory_repeat1.Enabled = true;
                m_new_fast_inventory_repeat2.Enabled = true;
                m_new_fast_inventory_power1.Enabled = true;
                m_new_fast_inventory_power2.Enabled = true;
            }
            else
            {
                mRepeat1.Enabled = false;
                mRepeat2.Enabled = false;
                mRepeatPower1.Enabled = false;
                mRepeatPower2.Enabled = false;

                m_new_fast_inventory_repeat1.Enabled = false;
                m_new_fast_inventory_repeat1.Text = "1";
                m_new_fast_inventory_repeat2.Enabled = false;
                m_new_fast_inventory_repeat2.Text = "1";
                m_new_fast_inventory_power1.Enabled = false;
                m_new_fast_inventory_power1.Text = "26";
                m_new_fast_inventory_power2.Enabled = false;
                m_new_fast_inventory_power2.Text = "28";
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
                            netClient.SendAsync(message, message.Length, netEndpoint);
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
        string curChannel = "0";
        CheckBox[] antChkbs = null;
        TextBox[] antStays = null;
        TextBox[] antPowers = null;
        static TagDB tagdb = null;
        bool useAntG1 = true;
        int FastExecTimes = 0;
        bool FastInvV2Reading = false;

        /// <summary>
        /// Define a variable for the inventory test log
        /// </summary>
        TextWriter transportLogFile = null;
        uint inventory_times = 0;

        private void fast_inventory_v2_clear_btn_Click(object sender, EventArgs e)
        {
            this.BeginInvoke(new ThreadStart(delegate ()
            {
                tagdb.Clear();
                fast_inv_v2_totalread_label.Text = tagdb.TotalTagCount + "次";
                fast_inv_v2_totaltagcount_label.Text = tagdb.UniqueTagCount + "个";
                fast_inv_v2_cmd_totalread_label.Text = "0 个";
                fast_inv_v2_cmd_time_label.Text = "0 ms";
                fast_inv_v2_cmd_exec_rate.Text = "0 个/s";
                fast_inv_v2_total_cmd_time_label.Text = tagdb.TotalCommandTime + "ms";
            }));
        }


        private void cb_fast_inv_v2_check_all_ant_CheckedChanged(object sender, EventArgs e)
        {
            bool check = cb_fast_inv_v2_check_all_ant.Checked;
            foreach (CheckBox cb in antChkbs)
            {
                if (cb.Enabled)
                    cb.Checked = check;
            }
        }

        private bool checkFastInvV2Ant()
        {
            int antCount = 0;
            for (int i = 0; i < 16; i++)
            {
                if (antChkbs[i].Enabled)
                    antCount++;
            }
            if (antCount > 0)
                return true;
            return false;
        }

        private void fast_inventory_v2_start_btn_Click(object sender, EventArgs e)
        {
            if(fast_inventory_v2_start_btn.Text.Equals("停止"))
            {
                FastInvV2Reading = false;
                fast_inventory_v2_start_btn.Text = "开始";
                return;
            }
            if (!checkFastInvV2Ant())
            {
                MessageBox.Show("请至少选择一个天线");
                fast_inventory_v2_start_btn.Text = "开始";
                return;
            }

            fast_inventory_v2_start_btn.Text = "停止";

            foreach (CheckBox cb in antChkbs)
            {
                cb.ForeColor = Color.Black;
            }

            FastExecTimes = Convert.ToInt32(tb_fast_inv2_exec_times.Text.ToString());

            cmdGetFrequencyRegion();
            if (!savelog_cb.Checked)
            {
                savelog_cb.Checked = true;
            }

            //startInvV2();
        }

        private void startInvV2()
        {
            FastInvV2Reading = true;
            if (checkAntG1Count())
            {
                cmdFastInventoryV2Send(useAntG1);
            }
            else if (checkAntG2Count())
            {
                cmdSwitchAntG2();
            }
            else
            {
                useAntG1 = true;
                FastExecTimes = 0;
                FastInvV2Reading = false;
                stopInvV2();
                MessageBox.Show("No Antenna select!");
            }
        }

        private void stopInvV2()
        {
            Console.WriteLine("stopInvV2 useAntG1={0}", useAntG1);
            BeginInvoke(new ThreadStart(delegate {
                fast_inventory_v2_start_btn.Text = "开始";
            }));
        }

        private bool checkAntG1Count()
        {
            for (int i = 0; i<8; i++)
            {
                if(antChkbs[i].Enabled)
                {
                    if (antChkbs[i].Checked)
                    {
                        useAntG1 = true;
                        return true;
                    }
                }
            }
            useAntG1 = false;
            return false;
        }

        private bool checkAntG2Count()
        {
            for (int i = 8; i < 16; i++)
            {
                if (antChkbs[i].Enabled)
                {
                    if (antChkbs[i].Checked)
                    {
                        useAntG1 = false;
                        return true;
                    }
                }
            }
            useAntG1 = true;
            return false;
        }

        private List<Antenna> getFastInventoryAntList()
        {
            List<Antenna> antList = new List<Antenna>();
            for (int antNo = 0; antNo < 16; antNo++)
            {
                antList.Add(new Antenna(antNo, antStays[antNo].Text, antPowers[antNo].Text));
            }
            return antList;
        }

        private void parseFastInventoryV2(byte[] data)
        {
            bool ReadPhase = fast_inv_v2_phase_cb.Checked;

            byte[] rawData;
            rawData = new byte[data.Length];
            Array.Copy(data, 0, rawData, 0, rawData.Length);
            parseMessage(rawData);
        }

        private void parseMessage(byte[] rawData )
        {
            byte hdr;
            byte len;
            byte addr;
            byte cmd;
            byte[] data;
            byte check;

            //Console.WriteLine("parseMessage");
            int msgLen = rawData.Length;
            int writeIndex = 0;
            hdr = rawData[writeIndex++];
            //Console.WriteLine("#1 RfMessage [{1}]hdr={0:x2}", hdr, writeIndex);

            len = rawData[writeIndex++];
            //Console.WriteLine("#1 RfMessage [{1}]len={0:x2}", len, writeIndex);

            addr = rawData[writeIndex++];
            //Console.WriteLine("#1 RfMessage [{1}]addr={0:x2}", addr, writeIndex);

            cmd = rawData[writeIndex++];
            //Console.WriteLine("#1 RfMessage [{1}]cmd={0:x2}", cmd, writeIndex);

            data = new byte[msgLen - 5]; // hdr + len + addr + cmd + check
            Array.Copy(rawData, writeIndex, data, 0, data.Length);
            writeIndex += data.Length;
            //Console.WriteLine("#1 RfMessage [{1}]data={0:x2}", CCommondMethod.ToHex(data, "", " "), writeIndex);

            check = rawData[writeIndex++];
            //Console.WriteLine("#1 RfMessage [{1}]check={0:x2}", check, writeIndex);

            switch (cmd)
            {
                case 0x6C:
                    parseCmd6CData(len, data);
                    break;
                case 0x8A:
                    parseCmd8AData(len, data);
                    break;
                case 0x79:
                    parseGetFrequencyRegion(data);
                    startInvV2();
                    break;
                default:
                    Console.WriteLine("#1 parseData by default!");
                    break;
            }
        }

        private void parseCmd6CData(byte len, byte[] data)
        {
            switch (len)
            {
                case 0x04:
                    if (FastInvV2Reading)
                        cmdFastInventoryV2Send(useAntG1);
                    break;
                default:
                    Console.WriteLine("parseCmd6CData={0}", CCommondMethod.ToHex(data, "", " "));
                    break;
            }

        }

        private void parseCmd8AData(byte len, byte[] data)
        {
            switch (len)
            {
                // failed
                case 0x04:
                    ErrorHandle(data);
                    break;
                // success
                case 0x0A:
                    // totalRead commandDuration
                    FastInvV2Success(data);
                    break;
                // antenna detect
                case 0x05:
                    //天线未连接
                    // antId ErrorCode
                    AntennaDetectError(data);
                    break;
                // get tags
                default:
                    //Console.WriteLine("$$$$ parseCmd8AData data[{1}]={0}", CCommondMethod.ToHex(data, "", " "), data.Length);
                    bool readPhase = fast_inv_v2_phase_cb.Checked;
                    if (data.Length > 2)// pc
                        parseTag(readPhase, data);
                    break;
            }

        }

        private void parseTag(bool readPhase, byte[] data)
        {
            //Console.WriteLine("parseTag {0}", readPhase);
            this.BeginInvoke(new ThreadStart(delegate ()
            {
                lock(tagdb)
                {
                    Tag tag = new Tag(data, readPhase);
                    tagdb.Add(tag);
                }
            }));

            this.BeginInvoke(new ThreadStart(delegate ()
            {
                lock (tagdb)
                {
                    fast_inv_v2_totalread_label.Text = tagdb.TotalTagCount + "次";
                    fast_inv_v2_totaltagcount_label.Text = tagdb.UniqueTagCount + "个";
                }
            }));
        }

        private void ErrorHandle(byte[] data)
        {
            // ErrorCode
            Console.WriteLine("ErrorCode={0}\r\n", CCommondMethod.ToHex(data, "0x", " "));
            MessageBox.Show("ErrorCode: {0}" + CCommondMethod.ToHex(data, "0x", " "));
        }

        private void FastInvV2Success(byte[] data)
        {
            //Console.WriteLine("FastInvV2Success len={0}, data={1}", data.Length, CCommondMethod.ToHex(data, "", " "));
            int readIndex = 0;
            byte[] bTotalRead = new byte[3];
            Array.Copy(data, 0, bTotalRead, 0, bTotalRead.Length);
            readIndex += bTotalRead.Length;

            byte[] bCommandDuration = new byte[4];
            Array.Copy(data, readIndex, bCommandDuration, 0, bCommandDuration.Length);
            readIndex += bCommandDuration.Length;

            byte[] temp = new byte[4];
            temp[0] = 0x00;
            Array.Copy(bTotalRead, 0, temp, 1, bTotalRead.Length);
            uint totalRead = CCommondMethod.ToU32(temp, 0);
            uint commandDuration = CCommondMethod.ToU32(bCommandDuration, 0); ;
            //Console.WriteLine("totalRead={0}, commandDuration={1}\r\n", totalRead, commandDuration);

            this.BeginInvoke(new ThreadStart(delegate ()
            {
                lock (tagdb)
                {
                    tagdb.UpdateTotalCommandTime(commandDuration);

                    fast_inv_v2_cmd_totalread_label.Text = totalRead + "个";
                    if(totalRead ==0)
                    {
                        fast_inv_v2_cmd_totalread_label.ForeColor = Color.Red;
                    }
                    else
                        fast_inv_v2_cmd_totalread_label.ForeColor = Color.Black;
                    fast_inv_v2_cmd_time_label.Text = commandDuration + "ms";

                    fast_inv_v2_cmd_exec_rate.Text = commandDuration==0?"0 个/s":((totalRead * 1000) / commandDuration).ToString() + " 个/s";
                    fast_inv_v2_total_cmd_time_label.Text = tagdb.TotalCommandTime + "ms";
                }
            }));

            this.BeginInvoke(new ThreadStart(delegate() 
            {
                if(transportLogFile!=null)
                {
                    transportLogFile.Write(String.Format("{0} [{1}] 本次读取的标签数量是：{2} 个 耗时： {3} ms",
                    DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt"), inventory_times++, totalRead, commandDuration));

                    transportLogFile.WriteLine();
                    transportLogFile.Flush();
                }
            }));

            if(!useAntG1)
            {
                if (FastExecTimes != -1)
                {
                    if (FastExecTimes > 1)
                    {
                        FastExecTimes--;
                        //Console.WriteLine("--> FastExecTimes={0}", FastExecTimes);
                    }
                    else
                    {
                        FastInvV2Reading = false;
                        stopInvV2();
                    }
                }
            }

            if(FastInvV2Reading)
            {
                if (useAntG1)
                {
                    if (checkAntG2Count())
                    {
                        cmdSwitchAntG2();
                    }
                    else
                    {
                        if (FastExecTimes != -1)
                        {
                            if (FastExecTimes > 1)
                            {
                                FastExecTimes--;
                                //Console.WriteLine("# --> FastExecTimes={0}", FastExecTimes);
                            }
                            else
                            {
                                FastInvV2Reading = false;
                                stopInvV2();
                            }
                        }
                        if (FastInvV2Reading)
                            cmdFastInventoryV2Send(useAntG1);
                    }
                }
                else
                {
                    if (checkAntG1Count())
                    {
                        cmdSwitchAntG1();
                    }
                    else
                    {
                        cmdFastInventoryV2Send(useAntG1);
                    }
                }
            }
            else
            {
                if(!useAntG1)
                {
                    //Console.WriteLine("FastInvV2Reading={0}", FastInvV2Reading);
                    cmdSwitchAntG1();
                }
            }
        }

        private void cmdSwitchAntG1()
        {
            //Console.WriteLine("切换到天线组1");
            useAntG1 = true;
            cmdSwitchAntGroup(0x00);
        }

        private void cmdSwitchAntG2()
        {
            //Console.WriteLine("切换到天线组2");
            useAntG1 = false;
            cmdSwitchAntGroup(0x01);
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

        private void AntennaDetectError(byte[] data)
        {
            //Console.WriteLine("antId={0}, ErrCode={1}\r\n", data[0], data[1]);
            byte antId = data[0];
            antChkbs[antId].ForeColor = Color.Red;
        }

        private void cmdFastInventoryV2Send(bool antG1)
        {
            //Console.WriteLine("cmdFastInventoryV2Send 天线组{0}", antG1 == true ? "1" : "2");
            // ToDo: 固件版本>=8.2.9
            //ValidateFwVersion();
            int writeIndex = 0;
            byte[] rawData = new byte[256];
            rawData[writeIndex++] = 0xA0; // hdr

            rawData[writeIndex++] = 0x03; // len minLen = 3

            rawData[writeIndex++] = m_curSetting.btReadId; // addr

            rawData[writeIndex++] = 0x8A; // cmd

            // data
           if(antG1)
            {
                for(int i = 0; i < 8; i++)
                {
                    if (antChkbs[i].Checked)
                    {
                        rawData[writeIndex++] = (byte)(Convert.ToInt32(antChkbs[i].Text) - 1);
                        rawData[writeIndex++] = Convert.ToByte(antStays[i].Text);
                    }
                    else
                    {
                        rawData[writeIndex++] = (byte)(Convert.ToInt32(antChkbs[i].Text) - 1);
                        rawData[writeIndex++] = 0x00;
                    }
                }
            }
           else
            {
                for (int i = 8; i < 16; i++)
                {
                    if (antChkbs[i].Checked)
                    {
                        rawData[writeIndex++] = (byte)(Convert.ToInt32(antChkbs[i].Text) - 8);
                        rawData[writeIndex++] = Convert.ToByte(antStays[i].Text);
                    }
                    else
                    {
                        rawData[writeIndex++] = (byte)(Convert.ToInt32(antChkbs[i].Text) - 8);
                        rawData[writeIndex++] = 0x00;
                    }
                }
            }

            rawData[writeIndex++] = getFastInventoryV2Interval(); // Interval, 0 ms
            
            rawData[writeIndex++] = 0xDE; // Reserve
            rawData[writeIndex++] = 0xDE;
            rawData[writeIndex++] = 0x00;
            rawData[writeIndex++] = 0x00;

            rawData[writeIndex++] = getFastInventoryV2SelectFlag(); // SL

            rawData[writeIndex++] = getFastInventoryV2Session(); // session

            rawData[writeIndex++] = getFastInventoryV2Target(); // Target

            rawData[writeIndex++] = getFastInventoryV2Phase(); // Phase

            if(antG1)
            {
                for (int i = 0; i < 8; i++)
                {
                    if (antChkbs[i].Checked)
                    {
                        rawData[writeIndex++] = Convert.ToByte(antPowers[i].Text);
                    }
                    else
                    {
                        rawData[writeIndex++] = 0x00;
                    }
                }
            }
            else
            {
                for (int i = 8; i < 16; i++)
                {
                    if (antChkbs[i].Checked)
                    {
                        rawData[writeIndex++] = Convert.ToByte(antPowers[i].Text);
                    }
                    else
                    {
                        rawData[writeIndex++] = 0x00;
                    }
                }
            }

            rawData[writeIndex++] = getFastInventoryV2Repeat(); // Repeat

            int msgLen = writeIndex + 1;
            rawData[1] = (byte)(msgLen - 2); // except hdr+len
            //Console.WriteLine("sendReadJoharMessage writeIndex={0}, msgLen={0}, len={2}", writeIndex, msgLen, rawData[1]);

            byte[] checkData = new byte[msgLen - 1];
            Array.Copy(rawData, 0, checkData, 0, checkData.Length);
            rawData[writeIndex] = reader.CheckValue(checkData); // check

            byte[] sendData = new byte[msgLen];
            Array.Copy(rawData, 0, sendData, 0, msgLen);
            //Console.WriteLine("fastInventoryV2Send: {0}", CCommondMethod.ToHex(sendData, "", " "));
            int nResult = reader.SendMessage(sendData);
            //Console.WriteLine("fastInventoryV2Send: [{0}] {1}", nResult, CCommondMethod.ToHex(sendData, "", " "));
        }

        private byte getFastInventoryV2Repeat()
        {
            string repeat = fast_inv_v2_repeat_tb.Text.Trim();
            //Console.WriteLine("repeat={0}", repeat);
            return Convert.ToByte(repeat);
        }

        private byte getFastInventoryV2SelectFlag()
        {
            if (fast_inv_v2_sl_00_rb.Checked)
            {
                return 0x00;
            }
            else if (fast_inv_v2_sl_01_rb.Checked)
            {
                return 0x01;
            }
            else if (fast_inv_v2_sl_02_rb.Checked)
            {
                return 0x02;
            }
            else if (fast_inv_v2_sl_03_rb.Checked)
            {
                return 0x03;
            }
            return 0x01; // default s1
        }

        private byte getFastInventoryV2Interval()
        {
            string interval = fast_inv_v2_interval_tb.Text.Trim();
            //Console.WriteLine("interval={0}", interval);
            return Convert.ToByte(interval);
        }

        private byte getFastInventoryV2Phase()
        {
            if(fast_inv_v2_phase_cb.Checked)
            {
                return 0x01;
            }
            else
            {
                return 0x00;
            }
        }

        private byte getFastInventoryV2Target()
        {
            if (fast_inv_v2_target_A_rb.Checked)
            {
                return 0x00;
            }
            else if (fast_inv_v2_target_B_rb.Checked)
            {
                return 0x01;
            }
            return 0x00; // default A
        }

        private byte getFastInventoryV2Session()
        {
            if (fast_inv_v2_S0_rb.Checked)
            {
                return 0x00;
            }
            else if (fast_inv_v2_S1_rb.Checked)
            {
                return 0x01;
            }
            else if (fast_inv_v2_S2_rb.Checked)
            {
                return 0x02;
            }
            else if (fast_inv_v2__S3_rb.Checked)
            {
                return 0x03;
            }
            return 0x01; // default s1
        }

        private void fastInventoryLoadCfg()
        {
            fast_inv_v2_S1_rb.Checked = true;
            fast_inv_v2_target_A_rb.Checked = true;
            fast_inv_v2_sl_00_rb.Checked = true;
            fast_inv_v2_interval_tb.Text = "0";
            fast_inv_v2_repeat_tb.Text = "1";
            fast_inv_v2_phase_cb.Checked = false;
        }

        private void fast_inv_v2_channel_1_CheckedChanged(object sender, EventArgs e)
        {
            label_fast_inv_v2_ant_title_c2.Visible = false;
            label_fast_inv_v2_stay_title_c2.Visible = false;
            label_fast_inv_v2_power_title_c2.Visible = false;

            channelChange((RadioButton)sender);
        }

        private void fast_inv_v2_channel_4_CheckedChanged(object sender, EventArgs e)
        {
            label_fast_inv_v2_ant_title_c2.Visible = false;
            label_fast_inv_v2_stay_title_c2.Visible = false;
            label_fast_inv_v2_power_title_c2.Visible = false;
            channelChange((RadioButton)sender);
        }

        private void fast_inv_v2_channel_8_CheckedChanged(object sender, EventArgs e)
        {
            label_fast_inv_v2_ant_title_c2.Visible = false;
            label_fast_inv_v2_stay_title_c2.Visible = false;
            label_fast_inv_v2_power_title_c2.Visible = false;
            channelChange((RadioButton)sender);
        }

        private void fast_inv_v2_channel_16_CheckedChanged(object sender, EventArgs e)
        {
            label_fast_inv_v2_ant_title_c2.Visible = true;
            label_fast_inv_v2_stay_title_c2.Visible = true;
            label_fast_inv_v2_power_title_c2.Visible = true;
            channelChange((RadioButton)sender);
        }

        private void channelChange(RadioButton rb)
        {
            //Console.WriteLine("{0}:{1}:{2}", rb.Name, rb.Text, rb.Checked);
            if(rb.Name.Equals(curChannel) || !rb.Checked)
            {
                return;
            }
            curChannel = rb.Name;
            
            int channel = Convert.ToInt32(rb.Text);

            for(int i=0; i<16; i++)
            {
                if(i<channel)
                {
                    antChkbs[i].Visible = true;
                    antChkbs[i].Enabled = true;
                    antChkbs[i].Checked = (i < 1 ? true : false);//默认选1

                    antStays[i].Visible = true;
                    antStays[i].Enabled = true;
                    antStays[i].Text = "1";

                    antPowers[i].Visible = true;
                    antPowers[i].Enabled = true;
                    antPowers[i].Text = "30";
                }
                else
                {
                    antChkbs[i].Visible = false;
                    antChkbs[i].Enabled = false;
                    antChkbs[i].Checked = false;

                    antStays[i].Visible = false;
                    antStays[i].Enabled = false;

                    antPowers[i].Visible = false;
                    antPowers[i].Enabled = false;
                }
            }
        }

        private void GenerateColmnsForDataGrid()
        {
            tags_dgv.AutoGenerateColumns = false;
            tags_dgv.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            mSerialNumberColumn.DataPropertyName = "SerialNumber";
            mSerialNumberColumn.HeaderText = "#";

            mEpcColumn.DataPropertyName = "EPC";
            mEpcColumn.HeaderText = "EPC";

            mReadCountColumn.DataPropertyName = "ReadCount";
            mReadCountColumn.HeaderText = "ReadCount";

            mRssiColumn.DataPropertyName = "Rssi";
            mRssiColumn.HeaderText = "Rssi(dBm)";

            mFreqColumn.DataPropertyName = "Freq";
            mFreqColumn.HeaderText = "Freq(MHz)";

            mPhaseColumn.DataPropertyName = "Phase";
            mPhaseColumn.HeaderText = "Phase";
            mPhaseColumn.Visible = false;

            mAntennaColumn.DataPropertyName = "Antenna";
            mAntennaColumn.HeaderText = "Ant";

            mDataColumn.DataPropertyName = "Data";
            mDataColumn.HeaderText = "Data";
            mDataColumn.Visible = false;

            //tags_dgv.DataSource = tagdb.TagList;
        }


        private void cmd_8a_v2_tabPage_Leave(object sender, EventArgs e)
        {
            Console.WriteLine("cmd_8a_v2_tabPage_Leave");
            if (!FastInvV2Reading)
            {
                fast_inv_v2_cb.Checked = false;
            }
        }

        private void cmd_8a_v2_tabPage_Enter(object sender, EventArgs e)
        {
            Console.WriteLine("cmd_8a_v2_tabPage_Enter");
            if(!FastInvV2Reading)
            {
                fast_inv_v2_cb.Checked = true;
            }

            if (tagdb == null)
            {
                tagdb = new TagDB();
                tags_dgv.DataSource = tagdb.TagList;
            }
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

        private void fast_inv_v2_phase_cb_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = (CheckBox)sender;
            mPhaseColumn.Visible = cb.Checked;
        }

        private void savelog_cb_CheckedChanged(object sender, EventArgs e)
        {
            saveLog();
            if (savelog_cb.Checked)
                savelog_cb.Enabled = false;
        }

        private void saveLog()
        {
            if (savelog_cb.Checked)
            {
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text Files (.txt)|*.txt";
                saveFileDialog1.Title = "Select a File to save transport layer logging";
                string strDestinationFile = "FastInventoryTesting-log"
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
                else
                {
                    savelog_cb.Checked = false;
                }
            }
            else
            {
                if (null != transportLogFile)
                {
                    transportLogFile.Close();
                }
            }
        }

        private void parseGetFrequencyRegion(byte[] data)
        {
            Console.WriteLine("parseGetFrequencyRegion: {0}", CCommondMethod.ToHex(data, "", " "));
            if(tagdb!=null)
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
    }

    class Antenna
    {
        int antNo; // A - H, 0 - 7
        int ant;
        int stay;
        int power;

        public Antenna(int antNo, string stay, string power)
        {
            this.antNo = antNo;
            this.ant = antNo >= 8 ? (antNo -8):antNo;
            this.stay = Convert.ToInt32(stay);
            this.power = Convert.ToInt32(power);
        }

        public int No
        {
            get { return antNo; }
        }

        public byte Ant
        {
            get { return (byte)ant; }
        }

        public byte Stay
        {
            get { return (byte)stay; }
        }

        public byte Power
        {
            get { return (byte)power; }
        }
    }

    public class NetCardSearch
    {
        string pc_ip = String.Empty;
        string pc_mac = String.Empty;
        string pc_mask = String.Empty;
        public NetCardSearch()
        {
        }

        public string PC_IP { get { return pc_ip; } set { pc_ip = value; } }
        public string PC_MAC { get { return pc_mac; } set { pc_mac = value; } }
        public string PC_MASK { get { return pc_mask; } set { pc_mask = value; } }
    }
}
