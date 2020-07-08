using System.Windows.Forms;
namespace UHFDemo
{
    partial class R2000UartDemo
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        // add ms
        public static long wasteTime = 0;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(R2000UartDemo));
            this.tabCtrMain = new System.Windows.Forms.TabControl();
            this.PagReaderSetting = new System.Windows.Forms.TabPage();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.groupBox24 = new System.Windows.Forms.GroupBox();
            this.antType16 = new System.Windows.Forms.RadioButton();
            this.antType8 = new System.Windows.Forms.RadioButton();
            this.antType4 = new System.Windows.Forms.RadioButton();
            this.antType1 = new System.Windows.Forms.RadioButton();
            this.btReaderSetupRefresh = new System.Windows.Forms.Button();
            this.gbCmdReadGpio = new System.Windows.Forms.GroupBox();
            this.groupBox11 = new System.Windows.Forms.GroupBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.label33 = new System.Windows.Forms.Label();
            this.rdbGpio3High = new System.Windows.Forms.RadioButton();
            this.rdbGpio3Low = new System.Windows.Forms.RadioButton();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.label32 = new System.Windows.Forms.Label();
            this.rdbGpio4High = new System.Windows.Forms.RadioButton();
            this.rdbGpio4Low = new System.Windows.Forms.RadioButton();
            this.btnWriteGpio4Value = new System.Windows.Forms.Button();
            this.btnWriteGpio3Value = new System.Windows.Forms.Button();
            this.groupBox10 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.label30 = new System.Windows.Forms.Label();
            this.rdbGpio1High = new System.Windows.Forms.RadioButton();
            this.rdbGpio1Low = new System.Windows.Forms.RadioButton();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.label31 = new System.Windows.Forms.Label();
            this.rdbGpio2High = new System.Windows.Forms.RadioButton();
            this.rdbGpio2Low = new System.Windows.Forms.RadioButton();
            this.btnReadGpioValue = new System.Windows.Forms.Button();
            this.gbCmdBeeper = new System.Windows.Forms.GroupBox();
            this.btnSetBeeperMode = new System.Windows.Forms.Button();
            this.rdbBeeperModeTag = new System.Windows.Forms.RadioButton();
            this.rdbBeeperModeInventory = new System.Windows.Forms.RadioButton();
            this.rdbBeeperModeSlient = new System.Windows.Forms.RadioButton();
            this.gbCmdTemperature = new System.Windows.Forms.GroupBox();
            this.btnGetReaderTemperature = new System.Windows.Forms.Button();
            this.txtReaderTemperature = new System.Windows.Forms.TextBox();
            this.gbCmdVersion = new System.Windows.Forms.GroupBox();
            this.btnGetFirmwareVersion = new System.Windows.Forms.Button();
            this.txtFirmwareVersion = new System.Windows.Forms.TextBox();
            this.btnResetReader = new System.Windows.Forms.Button();
            this.gbCmdBaudrate = new System.Windows.Forms.GroupBox();
            this.htbGetIdentifier = new CustomControl.HexTextBox();
            this.htbSetIdentifier = new CustomControl.HexTextBox();
            this.btSetIdentifier = new System.Windows.Forms.Button();
            this.btGetIdentifier = new System.Windows.Forms.Button();
            this.gbCmdReaderAddress = new System.Windows.Forms.GroupBox();
            this.htxtReadId = new CustomControl.HexTextBox();
            this.btnSetReadAddress = new System.Windows.Forms.Button();
            this.gbTcpIp = new System.Windows.Forms.GroupBox();
            this.btnDisconnectTcp = new System.Windows.Forms.Button();
            this.txtTcpPort = new System.Windows.Forms.TextBox();
            this.btnConnectTcp = new System.Windows.Forms.Button();
            this.ipIpServer = new CustomControl.IpAddressTextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.gbRS232 = new System.Windows.Forms.GroupBox();
            this.btnSetUartBaudrate = new System.Windows.Forms.Button();
            this.btnDisconnectRs232 = new System.Windows.Forms.Button();
            this.cmbSetBaudrate = new System.Windows.Forms.ComboBox();
            this.lbChangeBaudrate = new System.Windows.Forms.Label();
            this.btnConnectRs232 = new System.Windows.Forms.Button();
            this.cmbBaudrate = new System.Windows.Forms.ComboBox();
            this.cmbComPort = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbConnectType = new System.Windows.Forms.GroupBox();
            this.rdbTcpIp = new System.Windows.Forms.RadioButton();
            this.rdbRS232 = new System.Windows.Forms.RadioButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.gbReturnLoss = new System.Windows.Forms.GroupBox();
            this.label110 = new System.Windows.Forms.Label();
            this.label109 = new System.Windows.Forms.Label();
            this.cmbReturnLossFreq = new System.Windows.Forms.ComboBox();
            this.label108 = new System.Windows.Forms.Label();
            this.textReturnLoss = new System.Windows.Forms.TextBox();
            this.btReturnLoss = new System.Windows.Forms.Button();
            this.gbProfile = new System.Windows.Forms.GroupBox();
            this.btGetProfile = new System.Windows.Forms.Button();
            this.btSetProfile = new System.Windows.Forms.Button();
            this.rdbProfile3 = new System.Windows.Forms.RadioButton();
            this.rdbProfile2 = new System.Windows.Forms.RadioButton();
            this.rdbProfile1 = new System.Windows.Forms.RadioButton();
            this.rdbProfile0 = new System.Windows.Forms.RadioButton();
            this.btRfSetup = new System.Windows.Forms.Button();
            this.gbMonza = new System.Windows.Forms.GroupBox();
            this.label14 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.rdbMonzaOff = new System.Windows.Forms.RadioButton();
            this.btSetMonzaStatus = new System.Windows.Forms.Button();
            this.btGetMonzaStatus = new System.Windows.Forms.Button();
            this.rdbMonzaOn = new System.Windows.Forms.RadioButton();
            this.gbCmdAntDetector = new System.Windows.Forms.GroupBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.tbAntDectector = new System.Windows.Forms.TextBox();
            this.btnGetAntDetector = new System.Windows.Forms.Button();
            this.btnSetAntDetector = new System.Windows.Forms.Button();
            this.gbCmdAntenna = new System.Windows.Forms.GroupBox();
            this.label107 = new System.Windows.Forms.Label();
            this.cmbWorkAnt = new System.Windows.Forms.ComboBox();
            this.btnGetWorkAntenna = new System.Windows.Forms.Button();
            this.btnSetWorkAntenna = new System.Windows.Forms.Button();
            this.gbCmdRegion = new System.Windows.Forms.GroupBox();
            this.cbUserDefineFreq = new System.Windows.Forms.CheckBox();
            this.groupBox23 = new System.Windows.Forms.GroupBox();
            this.label106 = new System.Windows.Forms.Label();
            this.label105 = new System.Windows.Forms.Label();
            this.label104 = new System.Windows.Forms.Label();
            this.label103 = new System.Windows.Forms.Label();
            this.label86 = new System.Windows.Forms.Label();
            this.label75 = new System.Windows.Forms.Label();
            this.textFreqQuantity = new System.Windows.Forms.TextBox();
            this.TextFreqInterval = new System.Windows.Forms.TextBox();
            this.textStartFreq = new System.Windows.Forms.TextBox();
            this.groupBox21 = new System.Windows.Forms.GroupBox();
            this.label37 = new System.Windows.Forms.Label();
            this.label36 = new System.Windows.Forms.Label();
            this.cmbFrequencyEnd = new System.Windows.Forms.ComboBox();
            this.label13 = new System.Windows.Forms.Label();
            this.cmbFrequencyStart = new System.Windows.Forms.ComboBox();
            this.label12 = new System.Windows.Forms.Label();
            this.rdbRegionChn = new System.Windows.Forms.RadioButton();
            this.rdbRegionEtsi = new System.Windows.Forms.RadioButton();
            this.rdbRegionFcc = new System.Windows.Forms.RadioButton();
            this.btnGetFrequencyRegion = new System.Windows.Forms.Button();
            this.btnSetFrequencyRegion = new System.Windows.Forms.Button();
            this.gbCmdOutputPower = new System.Windows.Forms.GroupBox();
            this.label151 = new System.Windows.Forms.Label();
            this.label152 = new System.Windows.Forms.Label();
            this.label153 = new System.Windows.Forms.Label();
            this.label154 = new System.Windows.Forms.Label();
            this.tb_dbm_16 = new System.Windows.Forms.TextBox();
            this.tb_dbm_15 = new System.Windows.Forms.TextBox();
            this.tb_dbm_14 = new System.Windows.Forms.TextBox();
            this.tb_dbm_13 = new System.Windows.Forms.TextBox();
            this.label147 = new System.Windows.Forms.Label();
            this.label148 = new System.Windows.Forms.Label();
            this.label149 = new System.Windows.Forms.Label();
            this.label150 = new System.Windows.Forms.Label();
            this.tb_dbm_12 = new System.Windows.Forms.TextBox();
            this.tb_dbm_11 = new System.Windows.Forms.TextBox();
            this.tb_dbm_10 = new System.Windows.Forms.TextBox();
            this.tb_dbm_9 = new System.Windows.Forms.TextBox();
            this.label115 = new System.Windows.Forms.Label();
            this.label114 = new System.Windows.Forms.Label();
            this.label113 = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label15 = new System.Windows.Forms.Label();
            this.btnGetOutputPower = new System.Windows.Forms.Button();
            this.btnSetOutputPower = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.pageEpcTest = new System.Windows.Forms.TabPage();
            this.tabEpcTest = new System.Windows.Forms.TabControl();
            this.pageRealMode = new System.Windows.Forms.TabPage();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel5 = new System.Windows.Forms.Panel();
            this.customizedExeTime = new System.Windows.Forms.TextBox();
            this.label127 = new System.Windows.Forms.Label();
            this.Duration = new System.Windows.Forms.Label();
            this.mSessionExeTime = new System.Windows.Forms.ComboBox();
            this.btRealTimeInventory = new System.Windows.Forms.Button();
            this.label84 = new System.Windows.Forms.Label();
            this.textRealRound = new System.Windows.Forms.TextBox();
            this.sessionInventoryrb = new System.Windows.Forms.RadioButton();
            this.autoInventoryrb = new System.Windows.Forms.RadioButton();
            this.m_session_q_cb = new System.Windows.Forms.CheckBox();
            this.m_session_sl_cb = new System.Windows.Forms.CheckBox();
            this.m_session_max_q = new System.Windows.Forms.TextBox();
            this.m_session_min_q = new System.Windows.Forms.TextBox();
            this.m_session_start_q = new System.Windows.Forms.TextBox();
            this.m_max_q_content = new System.Windows.Forms.Label();
            this.m_min_q_content = new System.Windows.Forms.Label();
            this.m_start_q_content = new System.Windows.Forms.Label();
            this.m_session_sl = new System.Windows.Forms.ComboBox();
            this.m_sl_content = new System.Windows.Forms.Label();
            this.cmbTarget = new System.Windows.Forms.ComboBox();
            this.label98 = new System.Windows.Forms.Label();
            this.cmbSession = new System.Windows.Forms.ComboBox();
            this.label97 = new System.Windows.Forms.Label();
            this.excel_format_rb = new System.Windows.Forms.RadioButton();
            this.txt_format_rb = new System.Windows.Forms.RadioButton();
            this.button8 = new System.Windows.Forms.Button();
            this.groupBox20 = new System.Windows.Forms.GroupBox();
            this.cbRealWorkant16 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant15 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant14 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant13 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant9 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant12 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant11 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant10 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant8 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant7 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant6 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant5 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant1 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant4 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant3 = new System.Windows.Forms.CheckBox();
            this.cbRealWorkant2 = new System.Windows.Forms.CheckBox();
            this.label19 = new System.Windows.Forms.Label();
            this.tbRealMinRssi = new System.Windows.Forms.TextBox();
            this.tbRealMaxRssi = new System.Windows.Forms.TextBox();
            this.btRealFresh = new System.Windows.Forms.Button();
            this.label70 = new System.Windows.Forms.Label();
            this.label74 = new System.Windows.Forms.Label();
            this.lbRealTagCount = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ledReal3 = new LxControl.LxLedControl();
            this.comboBox6 = new System.Windows.Forms.ComboBox();
            this.ledReal5 = new LxControl.LxLedControl();
            this.ledReal2 = new LxControl.LxLedControl();
            this.ledReal4 = new LxControl.LxLedControl();
            this.label53 = new System.Windows.Forms.Label();
            this.label66 = new System.Windows.Forms.Label();
            this.label67 = new System.Windows.Forms.Label();
            this.label68 = new System.Windows.Forms.Label();
            this.label69 = new System.Windows.Forms.Label();
            this.ledReal1 = new LxControl.LxLedControl();
            this.lvRealList = new System.Windows.Forms.ListView();
            this.columnHeader37 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader38 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader39 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader40 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader41 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader42 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pageBufferedMode = new System.Windows.Forms.TabPage();
            this.excel_format_buffer_rb = new System.Windows.Forms.RadioButton();
            this.txt_format_buffer_rb = new System.Windows.Forms.RadioButton();
            this.button6 = new System.Windows.Forms.Button();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.btClearBuffer = new System.Windows.Forms.Button();
            this.btQueryBuffer = new System.Windows.Forms.Button();
            this.btGetClearBuffer = new System.Windows.Forms.Button();
            this.btGetBuffer = new System.Windows.Forms.Button();
            this.panel10 = new System.Windows.Forms.Panel();
            this.btBufferInventory = new System.Windows.Forms.Button();
            this.label85 = new System.Windows.Forms.Label();
            this.textReadRoundBuffer = new System.Windows.Forms.TextBox();
            this.panel8 = new System.Windows.Forms.Panel();
            this.checkBox4 = new System.Windows.Forms.CheckBox();
            this.checkBox3 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.cbBufferWorkant1 = new System.Windows.Forms.CheckBox();
            this.cbBufferWorkant4 = new System.Windows.Forms.CheckBox();
            this.cbBufferWorkant2 = new System.Windows.Forms.CheckBox();
            this.cbBufferWorkant3 = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ledBuffer4 = new LxControl.LxLedControl();
            this.comboBox11 = new System.Windows.Forms.ComboBox();
            this.ledBuffer5 = new LxControl.LxLedControl();
            this.ledBuffer2 = new LxControl.LxLedControl();
            this.ledBuffer3 = new LxControl.LxLedControl();
            this.label92 = new System.Windows.Forms.Label();
            this.label93 = new System.Windows.Forms.Label();
            this.label94 = new System.Windows.Forms.Label();
            this.label95 = new System.Windows.Forms.Label();
            this.label96 = new System.Windows.Forms.Label();
            this.ledBuffer1 = new LxControl.LxLedControl();
            this.btBufferFresh = new System.Windows.Forms.Button();
            this.labelBufferTagCount = new System.Windows.Forms.Label();
            this.lvBufferList = new System.Windows.Forms.ListView();
            this.columnHeader49 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader50 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader51 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader52 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader53 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader54 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader16 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.pageFast4AntMode = new System.Windows.Forms.TabPage();
            this.txt_format_fast_rb = new System.Windows.Forms.RadioButton();
            this.excel_format_fast_rb = new System.Windows.Forms.RadioButton();
            this.button7 = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox25 = new System.Windows.Forms.GroupBox();
            this.ledFast1 = new LxControl.LxLedControl();
            this.label58 = new System.Windows.Forms.Label();
            this.ledFast4 = new LxControl.LxLedControl();
            this.ledFast2 = new LxControl.LxLedControl();
            this.label55 = new System.Windows.Forms.Label();
            this.label56 = new System.Windows.Forms.Label();
            this.ledFast3 = new LxControl.LxLedControl();
            this.label57 = new System.Windows.Forms.Label();
            this.label54 = new System.Windows.Forms.Label();
            this.ledFast5 = new LxControl.LxLedControl();
            this.groupBox28 = new System.Windows.Forms.GroupBox();
            this.m_new_fast_inventory_power2 = new System.Windows.Forms.TextBox();
            this.m_new_fast_inventory_power1 = new System.Windows.Forms.TextBox();
            this.m_new_fast_inventory_repeat2 = new System.Windows.Forms.TextBox();
            this.m_new_fast_inventory_repeat1 = new System.Windows.Forms.TextBox();
            this.mRepeatPower1 = new System.Windows.Forms.Label();
            this.mRepeatPower2 = new System.Windows.Forms.Label();
            this.mRepeat2 = new System.Windows.Forms.Label();
            this.mRepeat1 = new System.Windows.Forms.Label();
            this.mDynamicPoll = new System.Windows.Forms.CheckBox();
            this.label132 = new System.Windows.Forms.Label();
            this.mFastExeCount = new System.Windows.Forms.TextBox();
            this.label131 = new System.Windows.Forms.Label();
            this.mFastIntervalTime = new System.Windows.Forms.TextBox();
            this.groupBox27 = new System.Windows.Forms.GroupBox();
            this.m_new_fast_inventory_target_count = new System.Windows.Forms.TextBox();
            this.mTargetQuantity = new System.Windows.Forms.Label();
            this.m_new_fast_inventory_continue = new System.Windows.Forms.TextBox();
            this.mContiue = new System.Windows.Forms.Label();
            this.m_new_fast_inventory_optimized = new System.Windows.Forms.TextBox();
            this.mOpitimized = new System.Windows.Forms.Label();
            this.m_phase_value = new System.Windows.Forms.CheckBox();
            this.m_new_fast_inventory_flag = new System.Windows.Forms.ComboBox();
            this.m_new_fast_inventory_session = new System.Windows.Forms.ComboBox();
            this.m_new_fast_inventory = new System.Windows.Forms.CheckBox();
            this.label73 = new System.Windows.Forms.Label();
            this.txtInterval = new System.Windows.Forms.TextBox();
            this.label72 = new System.Windows.Forms.Label();
            this.mReserve = new System.Windows.Forms.Label();
            this.txtRepeat = new System.Windows.Forms.TextBox();
            this.mPower1 = new System.Windows.Forms.TextBox();
            this.mPower2 = new System.Windows.Forms.TextBox();
            this.mPower5 = new System.Windows.Forms.TextBox();
            this.mPower4 = new System.Windows.Forms.TextBox();
            this.mPower3 = new System.Windows.Forms.TextBox();
            this.groupBox26 = new System.Windows.Forms.GroupBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btFastInventory = new System.Windows.Forms.Button();
            this.label122 = new System.Windows.Forms.Label();
            this.txtPStay = new System.Windows.Forms.TextBox();
            this.label130 = new System.Windows.Forms.Label();
            this.label133 = new System.Windows.Forms.Label();
            this.cmbAntSelect16 = new System.Windows.Forms.ComboBox();
            this.txtLStay = new System.Windows.Forms.TextBox();
            this.cmbAntSelect13 = new System.Windows.Forms.ComboBox();
            this.label134 = new System.Windows.Forms.Label();
            this.label135 = new System.Windows.Forms.Label();
            this.txtOStay = new System.Windows.Forms.TextBox();
            this.cmbAntSelect9 = new System.Windows.Forms.ComboBox();
            this.label136 = new System.Windows.Forms.Label();
            this.label137 = new System.Windows.Forms.Label();
            this.label138 = new System.Windows.Forms.Label();
            this.label139 = new System.Windows.Forms.Label();
            this.cmbAntSelect12 = new System.Windows.Forms.ComboBox();
            this.txtNStay = new System.Windows.Forms.TextBox();
            this.cmbAntSelect15 = new System.Windows.Forms.ComboBox();
            this.txtKStay = new System.Windows.Forms.TextBox();
            this.label140 = new System.Windows.Forms.Label();
            this.txtIStay = new System.Windows.Forms.TextBox();
            this.label141 = new System.Windows.Forms.Label();
            this.label142 = new System.Windows.Forms.Label();
            this.txtMStay = new System.Windows.Forms.TextBox();
            this.cmbAntSelect10 = new System.Windows.Forms.ComboBox();
            this.label143 = new System.Windows.Forms.Label();
            this.cmbAntSelect14 = new System.Windows.Forms.ComboBox();
            this.label144 = new System.Windows.Forms.Label();
            this.label145 = new System.Windows.Forms.Label();
            this.label146 = new System.Windows.Forms.Label();
            this.cmbAntSelect11 = new System.Windows.Forms.ComboBox();
            this.txtJStay = new System.Windows.Forms.TextBox();
            this.label123 = new System.Windows.Forms.Label();
            this.textBox16 = new System.Windows.Forms.TextBox();
            this.label124 = new System.Windows.Forms.Label();
            this.label120 = new System.Windows.Forms.Label();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.txtDStay = new System.Windows.Forms.TextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label64 = new System.Windows.Forms.Label();
            this.label116 = new System.Windows.Forms.Label();
            this.textBox15 = new System.Windows.Forms.TextBox();
            this.cmbAntSelect1 = new System.Windows.Forms.ComboBox();
            this.label121 = new System.Windows.Forms.Label();
            this.label48 = new System.Windows.Forms.Label();
            this.label65 = new System.Windows.Forms.Label();
            this.label119 = new System.Windows.Forms.Label();
            this.cmbAntSelect4 = new System.Windows.Forms.ComboBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.txtCStay = new System.Windows.Forms.TextBox();
            this.label59 = new System.Windows.Forms.Label();
            this.txtAStay = new System.Windows.Forms.TextBox();
            this.label117 = new System.Windows.Forms.Label();
            this.label62 = new System.Windows.Forms.Label();
            this.textBox13 = new System.Windows.Forms.TextBox();
            this.cmbAntSelect2 = new System.Windows.Forms.ComboBox();
            this.label118 = new System.Windows.Forms.Label();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.label61 = new System.Windows.Forms.Label();
            this.label63 = new System.Windows.Forms.Label();
            this.label60 = new System.Windows.Forms.Label();
            this.cmbAntSelect3 = new System.Windows.Forms.ComboBox();
            this.txtBStay = new System.Windows.Forms.TextBox();
            this.comboBox7 = new System.Windows.Forms.ComboBox();
            this.txtFastMaxRssi = new System.Windows.Forms.TextBox();
            this.txtFastMinRssi = new System.Windows.Forms.TextBox();
            this.buttonFastFresh = new System.Windows.Forms.Button();
            this.label22 = new System.Windows.Forms.Label();
            this.lvFastList = new System.Windows.Forms.ListView();
            this.columnHeader31 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader32 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader33 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader34 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader35 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader356 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader36 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.label49 = new System.Windows.Forms.Label();
            this.txtFastTagList = new System.Windows.Forms.Label();
            this.pageAcessTag = new System.Windows.Forms.TabPage();
            this.ltvOperate = new System.Windows.Forms.ListView();
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader9 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader10 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader11 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader12 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader13 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader14 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader15 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbCmdOperateTag = new System.Windows.Forms.GroupBox();
            this.groupBox16 = new System.Windows.Forms.GroupBox();
            this.btnKillTag = new System.Windows.Forms.Button();
            this.htxtKillPwd = new CustomControl.HexTextBox();
            this.label29 = new System.Windows.Forms.Label();
            this.groupBox15 = new System.Windows.Forms.GroupBox();
            this.htxtLockPwd = new CustomControl.HexTextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.groupBox19 = new System.Windows.Forms.GroupBox();
            this.rdbUserMemory = new System.Windows.Forms.RadioButton();
            this.rdbTidMemory = new System.Windows.Forms.RadioButton();
            this.rdbEpcMermory = new System.Windows.Forms.RadioButton();
            this.rdbKillPwd = new System.Windows.Forms.RadioButton();
            this.rdbAccessPwd = new System.Windows.Forms.RadioButton();
            this.groupBox18 = new System.Windows.Forms.GroupBox();
            this.rdbLockEver = new System.Windows.Forms.RadioButton();
            this.rdbFreeEver = new System.Windows.Forms.RadioButton();
            this.rdbLock = new System.Windows.Forms.RadioButton();
            this.rdbFree = new System.Windows.Forms.RadioButton();
            this.btnLockTag = new System.Windows.Forms.Button();
            this.groupBox14 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.htxtWriteData = new CustomControl.HexTextBox();
            this.txtWordCnt = new System.Windows.Forms.TextBox();
            this.label27 = new System.Windows.Forms.Label();
            this.btnWriteTag = new System.Windows.Forms.Button();
            this.btnReadTag = new System.Windows.Forms.Button();
            this.txtWordAdd = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.htxtReadAndWritePwd = new CustomControl.HexTextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.groupBox17 = new System.Windows.Forms.GroupBox();
            this.rdbUser = new System.Windows.Forms.RadioButton();
            this.rdbTid = new System.Windows.Forms.RadioButton();
            this.rdbEpc = new System.Windows.Forms.RadioButton();
            this.rdbReserved = new System.Windows.Forms.RadioButton();
            this.label24 = new System.Windows.Forms.Label();
            this.groupBox13 = new System.Windows.Forms.GroupBox();
            this.label23 = new System.Windows.Forms.Label();
            this.btnSetAccessEpcMatch = new System.Windows.Forms.Button();
            this.cmbSetAccessEpcMatch = new System.Windows.Forms.ComboBox();
            this.txtAccessEpcMatch = new System.Windows.Forms.TextBox();
            this.ckAccessEpcMatch = new System.Windows.Forms.CheckBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.listView2 = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader7 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader8 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.groupBox22 = new System.Windows.Forms.GroupBox();
            this.button3 = new System.Windows.Forms.Button();
            this.groupBox12 = new System.Windows.Forms.GroupBox();
            this.label111 = new System.Windows.Forms.Label();
            this.comboBox16 = new System.Windows.Forms.ComboBox();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox9 = new System.Windows.Forms.GroupBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.textBox11 = new System.Windows.Forms.TextBox();
            this.hexTextBox9 = new CustomControl.HexTextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.comboBox12 = new System.Windows.Forms.ComboBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label71 = new System.Windows.Forms.Label();
            this.label99 = new System.Windows.Forms.Label();
            this.label100 = new System.Windows.Forms.Label();
            this.label101 = new System.Windows.Forms.Label();
            this.label102 = new System.Windows.Forms.Label();
            this.comboBox13 = new System.Windows.Forms.ComboBox();
            this.comboBox14 = new System.Windows.Forms.ComboBox();
            this.comboBox15 = new System.Windows.Forms.ComboBox();
            this.button1 = new System.Windows.Forms.Button();
            this.PagISO18000 = new System.Windows.Forms.TabPage();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnInventoryISO18000 = new System.Windows.Forms.Button();
            this.ltvTagISO18000 = new System.Windows.Forms.ListView();
            this.columnHeader27 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader25 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader26 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader28 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.gbISO1800LockQuery = new System.Windows.Forms.GroupBox();
            this.txtStatus = new System.Windows.Forms.TextBox();
            this.htxtQueryAdd = new CustomControl.HexTextBox();
            this.label46 = new System.Windows.Forms.Label();
            this.htxtLockAdd = new CustomControl.HexTextBox();
            this.label47 = new System.Windows.Forms.Label();
            this.btnQueryTagISO18000 = new System.Windows.Forms.Button();
            this.btnLockTagISO18000 = new System.Windows.Forms.Button();
            this.gbISO1800ReadWrite = new System.Windows.Forms.GroupBox();
            this.txtLoopTimes = new System.Windows.Forms.TextBox();
            this.label44 = new System.Windows.Forms.Label();
            this.txtLoop = new System.Windows.Forms.TextBox();
            this.label40 = new System.Windows.Forms.Label();
            this.htxtWriteData18000 = new CustomControl.HexTextBox();
            this.txtWriteLength = new System.Windows.Forms.TextBox();
            this.htxtReadData18000 = new CustomControl.HexTextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.btnWriteTagISO18000 = new System.Windows.Forms.Button();
            this.label51 = new System.Windows.Forms.Label();
            this.label52 = new System.Windows.Forms.Label();
            this.txtReadLength = new System.Windows.Forms.TextBox();
            this.htxtWriteStartAdd = new CustomControl.HexTextBox();
            this.label50 = new System.Windows.Forms.Label();
            this.htxtReadStartAdd = new CustomControl.HexTextBox();
            this.label42 = new System.Windows.Forms.Label();
            this.label43 = new System.Windows.Forms.Label();
            this.btnReadTagISO18000 = new System.Windows.Forms.Button();
            this.label41 = new System.Windows.Forms.Label();
            this.htxtReadUID = new CustomControl.HexTextBox();
            this.PagTranDataLog = new System.Windows.Forms.TabPage();
            this.btnSaveData = new System.Windows.Forms.Button();
            this.gbCmdManual = new System.Windows.Forms.GroupBox();
            this.label16 = new System.Windows.Forms.Label();
            this.htxtSendData = new CustomControl.HexTextBox();
            this.btnClearData = new System.Windows.Forms.Button();
            this.label17 = new System.Windows.Forms.Label();
            this.btnSendData = new System.Windows.Forms.Button();
            this.htxtCheckData = new CustomControl.HexTextBox();
            this.lrtxtDataTran = new CustomControl.LogRichTextBox();
            this.net_configure_tabPage = new System.Windows.Forms.TabPage();
            this.port_setting_tabcontrol = new System.Windows.Forms.TabControl();
            this.net_port_0_tabPage = new System.Windows.Forms.TabPage();
            this.net_port_1_enable_cb = new System.Windows.Forms.CheckBox();
            this.net_port_1_rand_port_flag_cb = new System.Windows.Forms.CheckBox();
            this.label169 = new System.Windows.Forms.Label();
            this.net_port_1_parity_bit_cbo = new System.Windows.Forms.ComboBox();
            this.label168 = new System.Windows.Forms.Label();
            this.net_port_1_stopbits_cbo = new System.Windows.Forms.ComboBox();
            this.label167 = new System.Windows.Forms.Label();
            this.net_port_1_databits_cbo = new System.Windows.Forms.ComboBox();
            this.label166 = new System.Windows.Forms.Label();
            this.net_port_1_baudrate_cbo = new System.Windows.Forms.ComboBox();
            this.net_port_1_dest_port_tb = new System.Windows.Forms.TextBox();
            this.label155 = new System.Windows.Forms.Label();
            this.label129 = new System.Windows.Forms.Label();
            this.net_port_1_dest_ip_tb = new System.Windows.Forms.TextBox();
            this.net_port_1_ip_domain_select_cbo = new System.Windows.Forms.ComboBox();
            this.label128 = new System.Windows.Forms.Label();
            this.net_port_1_local_net_port_tb = new System.Windows.Forms.TextBox();
            this.label126 = new System.Windows.Forms.Label();
            this.label125 = new System.Windows.Forms.Label();
            this.net_port_1_net_mode_cbo = new System.Windows.Forms.ComboBox();
            this.net_port_1_tabPage = new System.Windows.Forms.TabPage();
            this.net_port_2_enable_cb = new System.Windows.Forms.CheckBox();
            this.net_port_2_rand_port_flag_cb = new System.Windows.Forms.CheckBox();
            this.label170 = new System.Windows.Forms.Label();
            this.net_port_2_parity_bit_cbo = new System.Windows.Forms.ComboBox();
            this.label171 = new System.Windows.Forms.Label();
            this.net_port_2_stopbits_cbo = new System.Windows.Forms.ComboBox();
            this.label172 = new System.Windows.Forms.Label();
            this.net_port_2_databits_cbo = new System.Windows.Forms.ComboBox();
            this.label173 = new System.Windows.Forms.Label();
            this.net_port_2_baudrate_cbo = new System.Windows.Forms.ComboBox();
            this.net_port_2_dest_port_tb = new System.Windows.Forms.TextBox();
            this.label174 = new System.Windows.Forms.Label();
            this.label175 = new System.Windows.Forms.Label();
            this.net_port_2_dest_ip_tb = new System.Windows.Forms.TextBox();
            this.net_port_2_ip_domain_select_cbo = new System.Windows.Forms.ComboBox();
            this.label176 = new System.Windows.Forms.Label();
            this.net_port_2_local_net_port_tb = new System.Windows.Forms.TextBox();
            this.label177 = new System.Windows.Forms.Label();
            this.label178 = new System.Windows.Forms.Label();
            this.net_port_2_net_mode_cbo = new System.Windows.Forms.ComboBox();
            this.net_port_config_tool_linkLabel = new System.Windows.Forms.LinkLabel();
            this.label165 = new System.Windows.Forms.Label();
            this.label164 = new System.Windows.Forms.Label();
            this.old_net_port_link = new System.Windows.Forms.LinkLabel();
            this.label163 = new System.Windows.Forms.Label();
            this.net_clear_btn = new System.Windows.Forms.Button();
            this.net_base_settings_gb = new System.Windows.Forms.GroupBox();
            this.net_base_mod_mac_tb = new System.Windows.Forms.TextBox();
            this.label157 = new System.Windows.Forms.Label();
            this.net_base_dhcp_enable_cb = new System.Windows.Forms.CheckBox();
            this.net_base_mod_gateway_tb = new System.Windows.Forms.TextBox();
            this.net_base_mod_mask_tb = new System.Windows.Forms.TextBox();
            this.net_base_mod_ip_tb = new System.Windows.Forms.TextBox();
            this.net_base_mod_name_tb = new System.Windows.Forms.TextBox();
            this.label161 = new System.Windows.Forms.Label();
            this.label160 = new System.Windows.Forms.Label();
            this.label158 = new System.Windows.Forms.Label();
            this.label156 = new System.Windows.Forms.Label();
            this.dev_dgv = new System.Windows.Forms.DataGridView();
            this.mod_check_Column = new System.Windows.Forms.DataGridViewCheckBoxColumn();
            this.ModName = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModIp = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.ModVer = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PcMac = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.label159 = new System.Windows.Forms.Label();
            this.net_card_combox = new System.Windows.Forms.ComboBox();
            this.groupBox30 = new System.Windows.Forms.GroupBox();
            this.label162 = new System.Windows.Forms.Label();
            this.net_pc_mask_label = new System.Windows.Forms.Label();
            this.net_pc_mac_label = new System.Windows.Forms.Label();
            this.net_pc_ip_label = new System.Windows.Forms.Label();
            this.net_reset_btn = new System.Windows.Forms.Button();
            this.net_setCfg_btn = new System.Windows.Forms.Button();
            this.net_getCfg_btn = new System.Windows.Forms.Button();
            this.net_search_btn = new System.Windows.Forms.Button();
            this.net_refresh_netcard_btn = new System.Windows.Forms.Button();
            this.label35 = new System.Windows.Forms.Label();
            this.ckDisplayLog = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.button4 = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.checkBox5 = new System.Windows.Forms.CheckBox();
            this.checkBox6 = new System.Windows.Forms.CheckBox();
            this.checkBox7 = new System.Windows.Forms.CheckBox();
            this.checkBox8 = new System.Windows.Forms.CheckBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.label76 = new System.Windows.Forms.Label();
            this.label77 = new System.Windows.Forms.Label();
            this.label78 = new System.Windows.Forms.Label();
            this.groupBox8 = new System.Windows.Forms.GroupBox();
            this.comboBox9 = new System.Windows.Forms.ComboBox();
            this.lxLedControl9 = new LxControl.LxLedControl();
            this.lxLedControl10 = new LxControl.LxLedControl();
            this.lxLedControl11 = new LxControl.LxLedControl();
            this.lxLedControl12 = new LxControl.LxLedControl();
            this.label79 = new System.Windows.Forms.Label();
            this.label80 = new System.Windows.Forms.Label();
            this.label81 = new System.Windows.Forms.Label();
            this.label82 = new System.Windows.Forms.Label();
            this.label83 = new System.Windows.Forms.Label();
            this.lxLedControl13 = new LxControl.LxLedControl();
            this.listView1 = new System.Windows.Forms.ListView();
            this.columnHeader43 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader44 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader45 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader46 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader47 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader48 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.comboBox10 = new System.Windows.Forms.ComboBox();
            this.label87 = new System.Windows.Forms.Label();
            this.label88 = new System.Windows.Forms.Label();
            this.label89 = new System.Windows.Forms.Label();
            this.label90 = new System.Windows.Forms.Label();
            this.label91 = new System.Windows.Forms.Label();
            this.ckClearOperationRec = new System.Windows.Forms.CheckBox();
            this.lrtxtLog = new CustomControl.LogRichTextBox();
            this.lxLedControl14 = new LxControl.LxLedControl();
            this.lxLedControl15 = new LxControl.LxLedControl();
            this.lxLedControl16 = new LxControl.LxLedControl();
            this.lxLedControl17 = new LxControl.LxLedControl();
            this.lxLedControl18 = new LxControl.LxLedControl();
            this.timerInventory = new System.Windows.Forms.Timer(this.components);
            this.totalTime = new System.Windows.Forms.Timer(this.components);
            this.mFastSessionTimer = new System.Windows.Forms.Timer(this.components);
            this.sortImageList = new System.Windows.Forms.ImageList(this.components);
            this.mSendFastSwitchTimer = new System.Windows.Forms.Timer(this.components);
            this.tabCtrMain.SuspendLayout();
            this.PagReaderSetting.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.groupBox24.SuspendLayout();
            this.gbCmdReadGpio.SuspendLayout();
            this.groupBox11.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.groupBox10.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.gbCmdBeeper.SuspendLayout();
            this.gbCmdTemperature.SuspendLayout();
            this.gbCmdVersion.SuspendLayout();
            this.gbCmdBaudrate.SuspendLayout();
            this.gbCmdReaderAddress.SuspendLayout();
            this.gbTcpIp.SuspendLayout();
            this.gbRS232.SuspendLayout();
            this.gbConnectType.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.gbReturnLoss.SuspendLayout();
            this.gbProfile.SuspendLayout();
            this.gbMonza.SuspendLayout();
            this.gbCmdAntDetector.SuspendLayout();
            this.gbCmdAntenna.SuspendLayout();
            this.gbCmdRegion.SuspendLayout();
            this.groupBox23.SuspendLayout();
            this.groupBox21.SuspendLayout();
            this.gbCmdOutputPower.SuspendLayout();
            this.pageEpcTest.SuspendLayout();
            this.tabEpcTest.SuspendLayout();
            this.pageRealMode.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel5.SuspendLayout();
            this.groupBox20.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal1)).BeginInit();
            this.pageBufferedMode.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel8.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer1)).BeginInit();
            this.pageFast4AntMode.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox25.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast5)).BeginInit();
            this.groupBox28.SuspendLayout();
            this.groupBox27.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.pageAcessTag.SuspendLayout();
            this.gbCmdOperateTag.SuspendLayout();
            this.groupBox16.SuspendLayout();
            this.groupBox15.SuspendLayout();
            this.groupBox19.SuspendLayout();
            this.groupBox18.SuspendLayout();
            this.groupBox14.SuspendLayout();
            this.groupBox17.SuspendLayout();
            this.groupBox13.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.groupBox22.SuspendLayout();
            this.groupBox12.SuspendLayout();
            this.groupBox9.SuspendLayout();
            this.PagISO18000.SuspendLayout();
            this.gbISO1800LockQuery.SuspendLayout();
            this.gbISO1800ReadWrite.SuspendLayout();
            this.PagTranDataLog.SuspendLayout();
            this.gbCmdManual.SuspendLayout();
            this.net_configure_tabPage.SuspendLayout();
            this.port_setting_tabcontrol.SuspendLayout();
            this.net_port_0_tabPage.SuspendLayout();
            this.net_port_1_tabPage.SuspendLayout();
            this.net_base_settings_gb.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dev_dgv)).BeginInit();
            this.groupBox30.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.groupBox8.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl17)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl18)).BeginInit();
            this.SuspendLayout();
            // 
            // tabCtrMain
            // 
            this.tabCtrMain.Controls.Add(this.PagReaderSetting);
            this.tabCtrMain.Controls.Add(this.pageEpcTest);
            this.tabCtrMain.Controls.Add(this.PagISO18000);
            this.tabCtrMain.Controls.Add(this.PagTranDataLog);
            this.tabCtrMain.Controls.Add(this.net_configure_tabPage);
            this.tabCtrMain.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabCtrMain.Location = new System.Drawing.Point(0, 0);
            this.tabCtrMain.Name = "tabCtrMain";
            this.tabCtrMain.SelectedIndex = 0;
            this.tabCtrMain.Size = new System.Drawing.Size(1018, 581);
            this.tabCtrMain.TabIndex = 0;
            this.tabCtrMain.SelectedIndexChanged += new System.EventHandler(this.tabCtrMain_SelectedIndexChanged);
            this.tabCtrMain.Click += new System.EventHandler(this.tabCtrMain_Click);
            // 
            // PagReaderSetting
            // 
            this.PagReaderSetting.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PagReaderSetting.Controls.Add(this.tabControl1);
            this.PagReaderSetting.Location = new System.Drawing.Point(4, 22);
            this.PagReaderSetting.Name = "PagReaderSetting";
            this.PagReaderSetting.Padding = new System.Windows.Forms.Padding(3);
            this.PagReaderSetting.Size = new System.Drawing.Size(1010, 555);
            this.PagReaderSetting.TabIndex = 0;
            this.PagReaderSetting.Text = "读写器设置";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabControl1.Location = new System.Drawing.Point(3, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1004, 547);
            this.tabControl1.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage1.Controls.Add(this.groupBox24);
            this.tabPage1.Controls.Add(this.btReaderSetupRefresh);
            this.tabPage1.Controls.Add(this.gbCmdReadGpio);
            this.tabPage1.Controls.Add(this.gbCmdBeeper);
            this.tabPage1.Controls.Add(this.gbCmdTemperature);
            this.tabPage1.Controls.Add(this.gbCmdVersion);
            this.tabPage1.Controls.Add(this.btnResetReader);
            this.tabPage1.Controls.Add(this.gbCmdBaudrate);
            this.tabPage1.Controls.Add(this.gbCmdReaderAddress);
            this.tabPage1.Controls.Add(this.gbTcpIp);
            this.tabPage1.Controls.Add(this.gbRS232);
            this.tabPage1.Controls.Add(this.gbConnectType);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(996, 521);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "基本参数设置";
            // 
            // groupBox24
            // 
            this.groupBox24.Controls.Add(this.antType16);
            this.groupBox24.Controls.Add(this.antType8);
            this.groupBox24.Controls.Add(this.antType4);
            this.groupBox24.Controls.Add(this.antType1);
            this.groupBox24.Location = new System.Drawing.Point(165, 15);
            this.groupBox24.Name = "groupBox24";
            this.groupBox24.Size = new System.Drawing.Size(267, 44);
            this.groupBox24.TabIndex = 16;
            this.groupBox24.TabStop = false;
            this.groupBox24.Text = "读写器类型";
            // 
            // antType16
            // 
            this.antType16.AutoSize = true;
            this.antType16.Location = new System.Drawing.Point(202, 17);
            this.antType16.Name = "antType16";
            this.antType16.Size = new System.Drawing.Size(59, 16);
            this.antType16.TabIndex = 3;
            this.antType16.TabStop = true;
            this.antType16.Text = "16天线";
            this.antType16.UseVisualStyleBackColor = true;
            this.antType16.CheckedChanged += new System.EventHandler(this.antType16_CheckedChanged);
            // 
            // antType8
            // 
            this.antType8.AutoSize = true;
            this.antType8.Location = new System.Drawing.Point(140, 17);
            this.antType8.Name = "antType8";
            this.antType8.Size = new System.Drawing.Size(53, 16);
            this.antType8.TabIndex = 2;
            this.antType8.TabStop = true;
            this.antType8.Text = "8天线";
            this.antType8.UseVisualStyleBackColor = true;
            this.antType8.CheckedChanged += new System.EventHandler(this.antType8_CheckedChanged);
            // 
            // antType4
            // 
            this.antType4.AutoSize = true;
            this.antType4.Location = new System.Drawing.Point(79, 17);
            this.antType4.Name = "antType4";
            this.antType4.Size = new System.Drawing.Size(53, 16);
            this.antType4.TabIndex = 1;
            this.antType4.TabStop = true;
            this.antType4.Text = "4天线";
            this.antType4.UseVisualStyleBackColor = true;
            this.antType4.CheckedChanged += new System.EventHandler(this.antType4_CheckedChanged);
            // 
            // antType1
            // 
            this.antType1.AutoSize = true;
            this.antType1.Location = new System.Drawing.Point(19, 17);
            this.antType1.Name = "antType1";
            this.antType1.Size = new System.Drawing.Size(53, 16);
            this.antType1.TabIndex = 0;
            this.antType1.TabStop = true;
            this.antType1.Text = "1天线";
            this.antType1.UseVisualStyleBackColor = true;
            this.antType1.CheckedChanged += new System.EventHandler(this.antType1_CheckedChanged);
            // 
            // btReaderSetupRefresh
            // 
            this.btReaderSetupRefresh.Location = new System.Drawing.Point(857, 474);
            this.btReaderSetupRefresh.Name = "btReaderSetupRefresh";
            this.btReaderSetupRefresh.Size = new System.Drawing.Size(89, 23);
            this.btReaderSetupRefresh.TabIndex = 15;
            this.btReaderSetupRefresh.Text = "刷新界面";
            this.btReaderSetupRefresh.UseVisualStyleBackColor = true;
            this.btReaderSetupRefresh.Click += new System.EventHandler(this.btReaderSetupRefresh_Click);
            // 
            // gbCmdReadGpio
            // 
            this.gbCmdReadGpio.Controls.Add(this.groupBox11);
            this.gbCmdReadGpio.Controls.Add(this.groupBox10);
            this.gbCmdReadGpio.ForeColor = System.Drawing.Color.Black;
            this.gbCmdReadGpio.Location = new System.Drawing.Point(463, 120);
            this.gbCmdReadGpio.Name = "gbCmdReadGpio";
            this.gbCmdReadGpio.Size = new System.Drawing.Size(519, 233);
            this.gbCmdReadGpio.TabIndex = 12;
            this.gbCmdReadGpio.TabStop = false;
            this.gbCmdReadGpio.Text = "读写GPIO";
            // 
            // groupBox11
            // 
            this.groupBox11.BackColor = System.Drawing.Color.Transparent;
            this.groupBox11.Controls.Add(this.groupBox6);
            this.groupBox11.Controls.Add(this.groupBox7);
            this.groupBox11.Controls.Add(this.btnWriteGpio4Value);
            this.groupBox11.Controls.Add(this.btnWriteGpio3Value);
            this.groupBox11.ForeColor = System.Drawing.Color.Black;
            this.groupBox11.Location = new System.Drawing.Point(16, 121);
            this.groupBox11.Name = "groupBox11";
            this.groupBox11.Size = new System.Drawing.Size(485, 98);
            this.groupBox11.TabIndex = 13;
            this.groupBox11.TabStop = false;
            this.groupBox11.Text = "写GPIO";
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.label33);
            this.groupBox6.Controls.Add(this.rdbGpio3High);
            this.groupBox6.Controls.Add(this.rdbGpio3Low);
            this.groupBox6.Location = new System.Drawing.Point(56, 17);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(245, 29);
            this.groupBox6.TabIndex = 4;
            this.groupBox6.TabStop = false;
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(31, 11);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(41, 12);
            this.label33.TabIndex = 6;
            this.label33.Text = "GPIO3:";
            // 
            // rdbGpio3High
            // 
            this.rdbGpio3High.AutoSize = true;
            this.rdbGpio3High.Location = new System.Drawing.Point(78, 10);
            this.rdbGpio3High.Name = "rdbGpio3High";
            this.rdbGpio3High.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio3High.TabIndex = 6;
            this.rdbGpio3High.TabStop = true;
            this.rdbGpio3High.Text = "高";
            this.rdbGpio3High.UseVisualStyleBackColor = true;
            // 
            // rdbGpio3Low
            // 
            this.rdbGpio3Low.AutoSize = true;
            this.rdbGpio3Low.Location = new System.Drawing.Point(161, 10);
            this.rdbGpio3Low.Name = "rdbGpio3Low";
            this.rdbGpio3Low.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio3Low.TabIndex = 0;
            this.rdbGpio3Low.TabStop = true;
            this.rdbGpio3Low.Text = "低";
            this.rdbGpio3Low.UseVisualStyleBackColor = true;
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.label32);
            this.groupBox7.Controls.Add(this.rdbGpio4High);
            this.groupBox7.Controls.Add(this.rdbGpio4Low);
            this.groupBox7.Location = new System.Drawing.Point(56, 54);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(245, 30);
            this.groupBox7.TabIndex = 4;
            this.groupBox7.TabStop = false;
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(30, 12);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(41, 12);
            this.label32.TabIndex = 9;
            this.label32.Text = "GPIO4:";
            // 
            // rdbGpio4High
            // 
            this.rdbGpio4High.AutoSize = true;
            this.rdbGpio4High.Location = new System.Drawing.Point(77, 10);
            this.rdbGpio4High.Name = "rdbGpio4High";
            this.rdbGpio4High.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio4High.TabIndex = 1;
            this.rdbGpio4High.TabStop = true;
            this.rdbGpio4High.Text = "高";
            this.rdbGpio4High.UseVisualStyleBackColor = true;
            // 
            // rdbGpio4Low
            // 
            this.rdbGpio4Low.AutoSize = true;
            this.rdbGpio4Low.Location = new System.Drawing.Point(161, 10);
            this.rdbGpio4Low.Name = "rdbGpio4Low";
            this.rdbGpio4Low.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio4Low.TabIndex = 2;
            this.rdbGpio4Low.TabStop = true;
            this.rdbGpio4Low.Text = "低";
            this.rdbGpio4Low.UseVisualStyleBackColor = true;
            // 
            // btnWriteGpio4Value
            // 
            this.btnWriteGpio4Value.Location = new System.Drawing.Point(378, 61);
            this.btnWriteGpio4Value.Name = "btnWriteGpio4Value";
            this.btnWriteGpio4Value.Size = new System.Drawing.Size(90, 23);
            this.btnWriteGpio4Value.TabIndex = 5;
            this.btnWriteGpio4Value.Text = "写GPIO4";
            this.btnWriteGpio4Value.UseVisualStyleBackColor = true;
            this.btnWriteGpio4Value.Click += new System.EventHandler(this.btnWriteGpio4Value_Click);
            // 
            // btnWriteGpio3Value
            // 
            this.btnWriteGpio3Value.Location = new System.Drawing.Point(378, 24);
            this.btnWriteGpio3Value.Name = "btnWriteGpio3Value";
            this.btnWriteGpio3Value.Size = new System.Drawing.Size(90, 23);
            this.btnWriteGpio3Value.TabIndex = 10;
            this.btnWriteGpio3Value.Text = "写GPIO3";
            this.btnWriteGpio3Value.UseVisualStyleBackColor = true;
            this.btnWriteGpio3Value.Click += new System.EventHandler(this.btnWriteGpio3Value_Click);
            // 
            // groupBox10
            // 
            this.groupBox10.Controls.Add(this.groupBox4);
            this.groupBox10.Controls.Add(this.groupBox5);
            this.groupBox10.Controls.Add(this.btnReadGpioValue);
            this.groupBox10.ForeColor = System.Drawing.Color.Black;
            this.groupBox10.Location = new System.Drawing.Point(17, 17);
            this.groupBox10.Name = "groupBox10";
            this.groupBox10.Size = new System.Drawing.Size(485, 98);
            this.groupBox10.TabIndex = 12;
            this.groupBox10.TabStop = false;
            this.groupBox10.Text = "读GPIO";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.label30);
            this.groupBox4.Controls.Add(this.rdbGpio1High);
            this.groupBox4.Controls.Add(this.rdbGpio1Low);
            this.groupBox4.Location = new System.Drawing.Point(53, 16);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(247, 30);
            this.groupBox4.TabIndex = 11;
            this.groupBox4.TabStop = false;
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(31, 14);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(41, 12);
            this.label30.TabIndex = 0;
            this.label30.Text = "GPIO1:";
            // 
            // rdbGpio1High
            // 
            this.rdbGpio1High.AutoSize = true;
            this.rdbGpio1High.Location = new System.Drawing.Point(78, 12);
            this.rdbGpio1High.Name = "rdbGpio1High";
            this.rdbGpio1High.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio1High.TabIndex = 1;
            this.rdbGpio1High.TabStop = true;
            this.rdbGpio1High.Text = "高";
            this.rdbGpio1High.UseVisualStyleBackColor = true;
            // 
            // rdbGpio1Low
            // 
            this.rdbGpio1Low.AutoSize = true;
            this.rdbGpio1Low.Location = new System.Drawing.Point(163, 12);
            this.rdbGpio1Low.Name = "rdbGpio1Low";
            this.rdbGpio1Low.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio1Low.TabIndex = 2;
            this.rdbGpio1Low.TabStop = true;
            this.rdbGpio1Low.Text = "低";
            this.rdbGpio1Low.UseVisualStyleBackColor = true;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.label31);
            this.groupBox5.Controls.Add(this.rdbGpio2High);
            this.groupBox5.Controls.Add(this.rdbGpio2Low);
            this.groupBox5.Location = new System.Drawing.Point(53, 55);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(247, 30);
            this.groupBox5.TabIndex = 6;
            this.groupBox5.TabStop = false;
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(30, 14);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(41, 12);
            this.label31.TabIndex = 3;
            this.label31.Text = "GPIO2:";
            // 
            // rdbGpio2High
            // 
            this.rdbGpio2High.AutoSize = true;
            this.rdbGpio2High.Location = new System.Drawing.Point(77, 12);
            this.rdbGpio2High.Name = "rdbGpio2High";
            this.rdbGpio2High.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio2High.TabIndex = 4;
            this.rdbGpio2High.TabStop = true;
            this.rdbGpio2High.Text = "高";
            this.rdbGpio2High.UseVisualStyleBackColor = true;
            // 
            // rdbGpio2Low
            // 
            this.rdbGpio2Low.AutoSize = true;
            this.rdbGpio2Low.Location = new System.Drawing.Point(163, 12);
            this.rdbGpio2Low.Name = "rdbGpio2Low";
            this.rdbGpio2Low.Size = new System.Drawing.Size(35, 16);
            this.rdbGpio2Low.TabIndex = 5;
            this.rdbGpio2Low.TabStop = true;
            this.rdbGpio2Low.Text = "低";
            this.rdbGpio2Low.UseVisualStyleBackColor = true;
            // 
            // btnReadGpioValue
            // 
            this.btnReadGpioValue.Location = new System.Drawing.Point(377, 62);
            this.btnReadGpioValue.Name = "btnReadGpioValue";
            this.btnReadGpioValue.Size = new System.Drawing.Size(90, 23);
            this.btnReadGpioValue.TabIndex = 0;
            this.btnReadGpioValue.Text = "读GPIO";
            this.btnReadGpioValue.UseVisualStyleBackColor = true;
            this.btnReadGpioValue.Click += new System.EventHandler(this.btnReadGpioValue_Click);
            // 
            // gbCmdBeeper
            // 
            this.gbCmdBeeper.Controls.Add(this.btnSetBeeperMode);
            this.gbCmdBeeper.Controls.Add(this.rdbBeeperModeTag);
            this.gbCmdBeeper.Controls.Add(this.rdbBeeperModeInventory);
            this.gbCmdBeeper.Controls.Add(this.rdbBeeperModeSlient);
            this.gbCmdBeeper.ForeColor = System.Drawing.Color.Black;
            this.gbCmdBeeper.Location = new System.Drawing.Point(463, 359);
            this.gbCmdBeeper.Name = "gbCmdBeeper";
            this.gbCmdBeeper.Size = new System.Drawing.Size(519, 109);
            this.gbCmdBeeper.TabIndex = 11;
            this.gbCmdBeeper.TabStop = false;
            this.gbCmdBeeper.Text = "蜂鸣器状态";
            // 
            // btnSetBeeperMode
            // 
            this.btnSetBeeperMode.Location = new System.Drawing.Point(394, 39);
            this.btnSetBeeperMode.Name = "btnSetBeeperMode";
            this.btnSetBeeperMode.Size = new System.Drawing.Size(90, 23);
            this.btnSetBeeperMode.TabIndex = 3;
            this.btnSetBeeperMode.Text = "设置 ";
            this.btnSetBeeperMode.UseVisualStyleBackColor = true;
            this.btnSetBeeperMode.Click += new System.EventHandler(this.btnSetBeeperMode_Click);
            // 
            // rdbBeeperModeTag
            // 
            this.rdbBeeperModeTag.AutoSize = true;
            this.rdbBeeperModeTag.Location = new System.Drawing.Point(16, 69);
            this.rdbBeeperModeTag.Name = "rdbBeeperModeTag";
            this.rdbBeeperModeTag.Size = new System.Drawing.Size(431, 16);
            this.rdbBeeperModeTag.TabIndex = 2;
            this.rdbBeeperModeTag.TabStop = true;
            this.rdbBeeperModeTag.Text = "每读到一张标签鸣响(适用于少量标签，响应次数严格按照标签被识别次数。)";
            this.rdbBeeperModeTag.UseVisualStyleBackColor = true;
            // 
            // rdbBeeperModeInventory
            // 
            this.rdbBeeperModeInventory.AutoSize = true;
            this.rdbBeeperModeInventory.Location = new System.Drawing.Point(16, 42);
            this.rdbBeeperModeInventory.Name = "rdbBeeperModeInventory";
            this.rdbBeeperModeInventory.Size = new System.Drawing.Size(83, 16);
            this.rdbBeeperModeInventory.TabIndex = 1;
            this.rdbBeeperModeInventory.TabStop = true;
            this.rdbBeeperModeInventory.Text = "盘存后鸣响";
            this.rdbBeeperModeInventory.UseVisualStyleBackColor = true;
            // 
            // rdbBeeperModeSlient
            // 
            this.rdbBeeperModeSlient.AutoSize = true;
            this.rdbBeeperModeSlient.Location = new System.Drawing.Point(17, 20);
            this.rdbBeeperModeSlient.Name = "rdbBeeperModeSlient";
            this.rdbBeeperModeSlient.Size = new System.Drawing.Size(47, 16);
            this.rdbBeeperModeSlient.TabIndex = 0;
            this.rdbBeeperModeSlient.TabStop = true;
            this.rdbBeeperModeSlient.Text = "安静";
            this.rdbBeeperModeSlient.UseVisualStyleBackColor = true;
            // 
            // gbCmdTemperature
            // 
            this.gbCmdTemperature.Controls.Add(this.btnGetReaderTemperature);
            this.gbCmdTemperature.Controls.Add(this.txtReaderTemperature);
            this.gbCmdTemperature.ForeColor = System.Drawing.Color.Black;
            this.gbCmdTemperature.Location = new System.Drawing.Point(463, 65);
            this.gbCmdTemperature.Name = "gbCmdTemperature";
            this.gbCmdTemperature.Size = new System.Drawing.Size(519, 49);
            this.gbCmdTemperature.TabIndex = 10;
            this.gbCmdTemperature.TabStop = false;
            this.gbCmdTemperature.Text = "工作温度监控";
            // 
            // btnGetReaderTemperature
            // 
            this.btnGetReaderTemperature.Location = new System.Drawing.Point(393, 17);
            this.btnGetReaderTemperature.Name = "btnGetReaderTemperature";
            this.btnGetReaderTemperature.Size = new System.Drawing.Size(90, 23);
            this.btnGetReaderTemperature.TabIndex = 0;
            this.btnGetReaderTemperature.Text = "读取";
            this.btnGetReaderTemperature.UseVisualStyleBackColor = true;
            this.btnGetReaderTemperature.Click += new System.EventHandler(this.btnGetReaderTemperature_Click);
            // 
            // txtReaderTemperature
            // 
            this.txtReaderTemperature.Location = new System.Drawing.Point(106, 17);
            this.txtReaderTemperature.Name = "txtReaderTemperature";
            this.txtReaderTemperature.ReadOnly = true;
            this.txtReaderTemperature.Size = new System.Drawing.Size(120, 21);
            this.txtReaderTemperature.TabIndex = 1;
            this.txtReaderTemperature.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // gbCmdVersion
            // 
            this.gbCmdVersion.Controls.Add(this.btnGetFirmwareVersion);
            this.gbCmdVersion.Controls.Add(this.txtFirmwareVersion);
            this.gbCmdVersion.ForeColor = System.Drawing.Color.Black;
            this.gbCmdVersion.Location = new System.Drawing.Point(463, 15);
            this.gbCmdVersion.Name = "gbCmdVersion";
            this.gbCmdVersion.Size = new System.Drawing.Size(519, 44);
            this.gbCmdVersion.TabIndex = 9;
            this.gbCmdVersion.TabStop = false;
            this.gbCmdVersion.Text = "固件版本";
            // 
            // btnGetFirmwareVersion
            // 
            this.btnGetFirmwareVersion.Location = new System.Drawing.Point(394, 14);
            this.btnGetFirmwareVersion.Name = "btnGetFirmwareVersion";
            this.btnGetFirmwareVersion.Size = new System.Drawing.Size(90, 23);
            this.btnGetFirmwareVersion.TabIndex = 0;
            this.btnGetFirmwareVersion.Text = "读取 ";
            this.btnGetFirmwareVersion.UseVisualStyleBackColor = true;
            this.btnGetFirmwareVersion.Click += new System.EventHandler(this.btnGetFirmwareVersion_Click);
            // 
            // txtFirmwareVersion
            // 
            this.txtFirmwareVersion.Location = new System.Drawing.Point(105, 16);
            this.txtFirmwareVersion.Name = "txtFirmwareVersion";
            this.txtFirmwareVersion.ReadOnly = true;
            this.txtFirmwareVersion.Size = new System.Drawing.Size(121, 21);
            this.txtFirmwareVersion.TabIndex = 1;
            this.txtFirmwareVersion.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnResetReader
            // 
            this.btnResetReader.Location = new System.Drawing.Point(10, 465);
            this.btnResetReader.Name = "btnResetReader";
            this.btnResetReader.Size = new System.Drawing.Size(434, 41);
            this.btnResetReader.TabIndex = 8;
            this.btnResetReader.Text = "重启读写器";
            this.btnResetReader.UseVisualStyleBackColor = true;
            this.btnResetReader.Click += new System.EventHandler(this.btnResetReader_Click);
            // 
            // gbCmdBaudrate
            // 
            this.gbCmdBaudrate.Controls.Add(this.htbGetIdentifier);
            this.gbCmdBaudrate.Controls.Add(this.htbSetIdentifier);
            this.gbCmdBaudrate.Controls.Add(this.btSetIdentifier);
            this.gbCmdBaudrate.Controls.Add(this.btGetIdentifier);
            this.gbCmdBaudrate.ForeColor = System.Drawing.Color.Black;
            this.gbCmdBaudrate.Location = new System.Drawing.Point(10, 359);
            this.gbCmdBaudrate.Name = "gbCmdBaudrate";
            this.gbCmdBaudrate.Size = new System.Drawing.Size(434, 96);
            this.gbCmdBaudrate.TabIndex = 7;
            this.gbCmdBaudrate.TabStop = false;
            this.gbCmdBaudrate.Text = "读写器识别标识(12字节)";
            // 
            // htbGetIdentifier
            // 
            this.htbGetIdentifier.Location = new System.Drawing.Point(34, 22);
            this.htbGetIdentifier.Name = "htbGetIdentifier";
            this.htbGetIdentifier.ReadOnly = true;
            this.htbGetIdentifier.Size = new System.Drawing.Size(228, 21);
            this.htbGetIdentifier.TabIndex = 13;
            this.htbGetIdentifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // htbSetIdentifier
            // 
            this.htbSetIdentifier.Location = new System.Drawing.Point(34, 61);
            this.htbSetIdentifier.Name = "htbSetIdentifier";
            this.htbSetIdentifier.Size = new System.Drawing.Size(228, 21);
            this.htbSetIdentifier.TabIndex = 12;
            this.htbSetIdentifier.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btSetIdentifier
            // 
            this.btSetIdentifier.Location = new System.Drawing.Point(314, 60);
            this.btSetIdentifier.Name = "btSetIdentifier";
            this.btSetIdentifier.Size = new System.Drawing.Size(90, 23);
            this.btSetIdentifier.TabIndex = 1;
            this.btSetIdentifier.Text = "设置";
            this.btSetIdentifier.UseVisualStyleBackColor = true;
            this.btSetIdentifier.Click += new System.EventHandler(this.btSetIdentifier_Click);
            // 
            // btGetIdentifier
            // 
            this.btGetIdentifier.Location = new System.Drawing.Point(314, 21);
            this.btGetIdentifier.Name = "btGetIdentifier";
            this.btGetIdentifier.Size = new System.Drawing.Size(90, 23);
            this.btGetIdentifier.TabIndex = 0;
            this.btGetIdentifier.Text = "读取";
            this.btGetIdentifier.UseVisualStyleBackColor = true;
            this.btGetIdentifier.Click += new System.EventHandler(this.btGetIdentifier_Click);
            // 
            // gbCmdReaderAddress
            // 
            this.gbCmdReaderAddress.Controls.Add(this.htxtReadId);
            this.gbCmdReaderAddress.Controls.Add(this.btnSetReadAddress);
            this.gbCmdReaderAddress.ForeColor = System.Drawing.Color.Black;
            this.gbCmdReaderAddress.Location = new System.Drawing.Point(10, 287);
            this.gbCmdReaderAddress.Name = "gbCmdReaderAddress";
            this.gbCmdReaderAddress.Size = new System.Drawing.Size(434, 66);
            this.gbCmdReaderAddress.TabIndex = 5;
            this.gbCmdReaderAddress.TabStop = false;
            this.gbCmdReaderAddress.Text = "读写器RS-485地址(HEX)";
            // 
            // htxtReadId
            // 
            this.htxtReadId.Location = new System.Drawing.Point(114, 25);
            this.htxtReadId.Name = "htxtReadId";
            this.htxtReadId.Size = new System.Drawing.Size(121, 21);
            this.htxtReadId.TabIndex = 2;
            this.htxtReadId.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnSetReadAddress
            // 
            this.btnSetReadAddress.Location = new System.Drawing.Point(314, 26);
            this.btnSetReadAddress.Name = "btnSetReadAddress";
            this.btnSetReadAddress.Size = new System.Drawing.Size(90, 23);
            this.btnSetReadAddress.TabIndex = 1;
            this.btnSetReadAddress.Text = "设置 ";
            this.btnSetReadAddress.UseVisualStyleBackColor = true;
            this.btnSetReadAddress.Click += new System.EventHandler(this.btnSetReadAddress_Click);
            // 
            // gbTcpIp
            // 
            this.gbTcpIp.Controls.Add(this.btnDisconnectTcp);
            this.gbTcpIp.Controls.Add(this.txtTcpPort);
            this.gbTcpIp.Controls.Add(this.btnConnectTcp);
            this.gbTcpIp.Controls.Add(this.ipIpServer);
            this.gbTcpIp.Controls.Add(this.label4);
            this.gbTcpIp.Controls.Add(this.label3);
            this.gbTcpIp.Location = new System.Drawing.Point(10, 197);
            this.gbTcpIp.Name = "gbTcpIp";
            this.gbTcpIp.Size = new System.Drawing.Size(434, 84);
            this.gbTcpIp.TabIndex = 3;
            this.gbTcpIp.TabStop = false;
            this.gbTcpIp.Text = "TCP/IP";
            // 
            // btnDisconnectTcp
            // 
            this.btnDisconnectTcp.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDisconnectTcp.Location = new System.Drawing.Point(314, 51);
            this.btnDisconnectTcp.Name = "btnDisconnectTcp";
            this.btnDisconnectTcp.Size = new System.Drawing.Size(90, 23);
            this.btnDisconnectTcp.TabIndex = 3;
            this.btnDisconnectTcp.Text = "断开读写器";
            this.btnDisconnectTcp.UseVisualStyleBackColor = true;
            this.btnDisconnectTcp.Click += new System.EventHandler(this.btnDisconnectTcp_Click);
            // 
            // txtTcpPort
            // 
            this.txtTcpPort.Location = new System.Drawing.Point(114, 52);
            this.txtTcpPort.Name = "txtTcpPort";
            this.txtTcpPort.Size = new System.Drawing.Size(120, 21);
            this.txtTcpPort.TabIndex = 1;
            this.txtTcpPort.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnConnectTcp
            // 
            this.btnConnectTcp.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnectTcp.Location = new System.Drawing.Point(314, 19);
            this.btnConnectTcp.Name = "btnConnectTcp";
            this.btnConnectTcp.Size = new System.Drawing.Size(90, 23);
            this.btnConnectTcp.TabIndex = 2;
            this.btnConnectTcp.Text = "连接读写器";
            this.btnConnectTcp.UseVisualStyleBackColor = true;
            this.btnConnectTcp.Click += new System.EventHandler(this.btnConnectTcp_Click);
            // 
            // ipIpServer
            // 
            this.ipIpServer.IpAddressStr = "";
            this.ipIpServer.Location = new System.Drawing.Point(114, 20);
            this.ipIpServer.Name = "ipIpServer";
            this.ipIpServer.Size = new System.Drawing.Size(120, 21);
            this.ipIpServer.TabIndex = 0;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(56, 56);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 12);
            this.label4.TabIndex = 1;
            this.label4.Text = "端口号:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(44, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 0;
            this.label3.Text = "读写器IP:";
            // 
            // gbRS232
            // 
            this.gbRS232.Controls.Add(this.btnSetUartBaudrate);
            this.gbRS232.Controls.Add(this.btnDisconnectRs232);
            this.gbRS232.Controls.Add(this.cmbSetBaudrate);
            this.gbRS232.Controls.Add(this.lbChangeBaudrate);
            this.gbRS232.Controls.Add(this.btnConnectRs232);
            this.gbRS232.Controls.Add(this.cmbBaudrate);
            this.gbRS232.Controls.Add(this.cmbComPort);
            this.gbRS232.Controls.Add(this.label2);
            this.gbRS232.Controls.Add(this.label1);
            this.gbRS232.Location = new System.Drawing.Point(10, 65);
            this.gbRS232.Name = "gbRS232";
            this.gbRS232.Size = new System.Drawing.Size(434, 124);
            this.gbRS232.TabIndex = 2;
            this.gbRS232.TabStop = false;
            this.gbRS232.Text = "RS-232";
            // 
            // btnSetUartBaudrate
            // 
            this.btnSetUartBaudrate.Location = new System.Drawing.Point(315, 93);
            this.btnSetUartBaudrate.Name = "btnSetUartBaudrate";
            this.btnSetUartBaudrate.Size = new System.Drawing.Size(90, 23);
            this.btnSetUartBaudrate.TabIndex = 1;
            this.btnSetUartBaudrate.Text = "设置";
            this.btnSetUartBaudrate.UseVisualStyleBackColor = true;
            this.btnSetUartBaudrate.Click += new System.EventHandler(this.btnSetUartBaudrate_Click);
            // 
            // btnDisconnectRs232
            // 
            this.btnDisconnectRs232.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnDisconnectRs232.Location = new System.Drawing.Point(314, 49);
            this.btnDisconnectRs232.Name = "btnDisconnectRs232";
            this.btnDisconnectRs232.Size = new System.Drawing.Size(90, 23);
            this.btnDisconnectRs232.TabIndex = 3;
            this.btnDisconnectRs232.Text = "断开读写器";
            this.btnDisconnectRs232.UseVisualStyleBackColor = true;
            this.btnDisconnectRs232.Click += new System.EventHandler(this.btnDisconnectRs232_Click);
            // 
            // cmbSetBaudrate
            // 
            this.cmbSetBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetBaudrate.FormattingEnabled = true;
            this.cmbSetBaudrate.Items.AddRange(new object[] {
            "38400",
            "115200"});
            this.cmbSetBaudrate.Location = new System.Drawing.Point(114, 94);
            this.cmbSetBaudrate.Name = "cmbSetBaudrate";
            this.cmbSetBaudrate.Size = new System.Drawing.Size(121, 20);
            this.cmbSetBaudrate.TabIndex = 0;
            // 
            // lbChangeBaudrate
            // 
            this.lbChangeBaudrate.AutoSize = true;
            this.lbChangeBaudrate.Location = new System.Drawing.Point(33, 98);
            this.lbChangeBaudrate.Name = "lbChangeBaudrate";
            this.lbChangeBaudrate.Size = new System.Drawing.Size(71, 12);
            this.lbChangeBaudrate.TabIndex = 0;
            this.lbChangeBaudrate.Text = "设置波特率:";
            // 
            // btnConnectRs232
            // 
            this.btnConnectRs232.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnConnectRs232.Location = new System.Drawing.Point(314, 15);
            this.btnConnectRs232.Name = "btnConnectRs232";
            this.btnConnectRs232.Size = new System.Drawing.Size(90, 23);
            this.btnConnectRs232.TabIndex = 2;
            this.btnConnectRs232.Text = "连接读写器";
            this.btnConnectRs232.UseVisualStyleBackColor = true;
            this.btnConnectRs232.Click += new System.EventHandler(this.btnConnectRs232_Click);
            // 
            // cmbBaudrate
            // 
            this.cmbBaudrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBaudrate.FormattingEnabled = true;
            this.cmbBaudrate.Items.AddRange(new object[] {
            "38400",
            "115200"});
            this.cmbBaudrate.Location = new System.Drawing.Point(113, 50);
            this.cmbBaudrate.Name = "cmbBaudrate";
            this.cmbBaudrate.Size = new System.Drawing.Size(121, 20);
            this.cmbBaudrate.TabIndex = 1;
            // 
            // cmbComPort
            // 
            this.cmbComPort.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbComPort.FormattingEnabled = true;
            this.cmbComPort.Location = new System.Drawing.Point(113, 16);
            this.cmbComPort.Name = "cmbComPort";
            this.cmbComPort.Size = new System.Drawing.Size(121, 20);
            this.cmbComPort.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(32, 54);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 12);
            this.label2.TabIndex = 1;
            this.label2.Text = "串口波特率:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(56, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "串口号:";
            // 
            // gbConnectType
            // 
            this.gbConnectType.Controls.Add(this.rdbTcpIp);
            this.gbConnectType.Controls.Add(this.rdbRS232);
            this.gbConnectType.Location = new System.Drawing.Point(10, 15);
            this.gbConnectType.Name = "gbConnectType";
            this.gbConnectType.Size = new System.Drawing.Size(147, 44);
            this.gbConnectType.TabIndex = 1;
            this.gbConnectType.TabStop = false;
            this.gbConnectType.Text = "连接方式";
            // 
            // rdbTcpIp
            // 
            this.rdbTcpIp.AutoSize = true;
            this.rdbTcpIp.Location = new System.Drawing.Point(78, 16);
            this.rdbTcpIp.Name = "rdbTcpIp";
            this.rdbTcpIp.Size = new System.Drawing.Size(59, 16);
            this.rdbTcpIp.TabIndex = 1;
            this.rdbTcpIp.TabStop = true;
            this.rdbTcpIp.Text = "TCP/IP";
            this.rdbTcpIp.UseVisualStyleBackColor = true;
            this.rdbTcpIp.CheckedChanged += new System.EventHandler(this.rdbTcpIp_CheckedChanged);
            // 
            // rdbRS232
            // 
            this.rdbRS232.AutoSize = true;
            this.rdbRS232.Location = new System.Drawing.Point(19, 17);
            this.rdbRS232.Name = "rdbRS232";
            this.rdbRS232.Size = new System.Drawing.Size(53, 16);
            this.rdbRS232.TabIndex = 0;
            this.rdbRS232.TabStop = true;
            this.rdbRS232.Text = "RS232";
            this.rdbRS232.UseVisualStyleBackColor = true;
            this.rdbRS232.CheckedChanged += new System.EventHandler(this.rdbRS232_CheckedChanged);
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage2.Controls.Add(this.gbReturnLoss);
            this.tabPage2.Controls.Add(this.gbProfile);
            this.tabPage2.Controls.Add(this.btRfSetup);
            this.tabPage2.Controls.Add(this.gbMonza);
            this.tabPage2.Controls.Add(this.gbCmdAntDetector);
            this.tabPage2.Controls.Add(this.gbCmdAntenna);
            this.tabPage2.Controls.Add(this.gbCmdRegion);
            this.tabPage2.Controls.Add(this.gbCmdOutputPower);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(996, 521);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "射频参数设置";
            // 
            // gbReturnLoss
            // 
            this.gbReturnLoss.BackColor = System.Drawing.Color.Transparent;
            this.gbReturnLoss.Controls.Add(this.label110);
            this.gbReturnLoss.Controls.Add(this.label109);
            this.gbReturnLoss.Controls.Add(this.cmbReturnLossFreq);
            this.gbReturnLoss.Controls.Add(this.label108);
            this.gbReturnLoss.Controls.Add(this.textReturnLoss);
            this.gbReturnLoss.Controls.Add(this.btReturnLoss);
            this.gbReturnLoss.ForeColor = System.Drawing.Color.Black;
            this.gbReturnLoss.Location = new System.Drawing.Point(15, 69);
            this.gbReturnLoss.Name = "gbReturnLoss";
            this.gbReturnLoss.Size = new System.Drawing.Size(472, 46);
            this.gbReturnLoss.TabIndex = 19;
            this.gbReturnLoss.TabStop = false;
            this.gbReturnLoss.Text = "测量天线端口回波损耗(Return Loss)";
            // 
            // label110
            // 
            this.label110.AutoSize = true;
            this.label110.Location = new System.Drawing.Point(156, 22);
            this.label110.Name = "label110";
            this.label110.Size = new System.Drawing.Size(11, 12);
            this.label110.TabIndex = 15;
            this.label110.Text = "@";
            // 
            // label109
            // 
            this.label109.AutoSize = true;
            this.label109.Location = new System.Drawing.Point(250, 22);
            this.label109.Name = "label109";
            this.label109.Size = new System.Drawing.Size(23, 12);
            this.label109.TabIndex = 14;
            this.label109.Text = "MHz";
            // 
            // cmbReturnLossFreq
            // 
            this.cmbReturnLossFreq.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbReturnLossFreq.FormattingEnabled = true;
            this.cmbReturnLossFreq.Items.AddRange(new object[] {
            "865.00",
            "865.50",
            "866.00",
            "866.50",
            "867.00",
            "867.50",
            "868.00",
            "902.00",
            "902.50",
            "903.00",
            "903.50",
            "904.00",
            "904.50",
            "905.00",
            "905.50",
            "906.00",
            "906.50",
            "907.00",
            "907.50",
            "908.00",
            "908.50",
            "909.00",
            "909.50",
            "910.00",
            "910.50",
            "911.00",
            "911.50",
            "912.00",
            "912.50",
            "913.00",
            "913.50",
            "914.00",
            "914.50",
            "915.00",
            "915.50",
            "916.00",
            "916.50",
            "917.00",
            "917.50",
            "918.00",
            "918.50",
            "919.00",
            "919.50",
            "920.00",
            "920.50",
            "921.00",
            "921.50",
            "922.00",
            "922.50",
            "923.00",
            "923.50",
            "924.00",
            "924.50",
            "925.00",
            "925.50",
            "926.00",
            "926.50",
            "927.00",
            "927.50",
            "928.00"});
            this.cmbReturnLossFreq.Location = new System.Drawing.Point(173, 18);
            this.cmbReturnLossFreq.Name = "cmbReturnLossFreq";
            this.cmbReturnLossFreq.Size = new System.Drawing.Size(71, 20);
            this.cmbReturnLossFreq.TabIndex = 13;
            // 
            // label108
            // 
            this.label108.AutoSize = true;
            this.label108.Location = new System.Drawing.Point(50, 22);
            this.label108.Name = "label108";
            this.label108.Size = new System.Drawing.Size(23, 12);
            this.label108.TabIndex = 12;
            this.label108.Text = "RL:";
            // 
            // textReturnLoss
            // 
            this.textReturnLoss.Location = new System.Drawing.Point(79, 18);
            this.textReturnLoss.Name = "textReturnLoss";
            this.textReturnLoss.ReadOnly = true;
            this.textReturnLoss.Size = new System.Drawing.Size(71, 21);
            this.textReturnLoss.TabIndex = 11;
            this.textReturnLoss.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btReturnLoss
            // 
            this.btReturnLoss.Location = new System.Drawing.Point(369, 17);
            this.btReturnLoss.Name = "btReturnLoss";
            this.btReturnLoss.Size = new System.Drawing.Size(90, 23);
            this.btReturnLoss.TabIndex = 10;
            this.btReturnLoss.Text = "测量";
            this.btReturnLoss.UseVisualStyleBackColor = true;
            this.btReturnLoss.Click += new System.EventHandler(this.btReturnLoss_Click);
            // 
            // gbProfile
            // 
            this.gbProfile.Controls.Add(this.btGetProfile);
            this.gbProfile.Controls.Add(this.btSetProfile);
            this.gbProfile.Controls.Add(this.rdbProfile3);
            this.gbProfile.Controls.Add(this.rdbProfile2);
            this.gbProfile.Controls.Add(this.rdbProfile1);
            this.gbProfile.Controls.Add(this.rdbProfile0);
            this.gbProfile.ForeColor = System.Drawing.Color.Black;
            this.gbProfile.Location = new System.Drawing.Point(15, 414);
            this.gbProfile.Name = "gbProfile";
            this.gbProfile.Size = new System.Drawing.Size(967, 73);
            this.gbProfile.TabIndex = 18;
            this.gbProfile.TabStop = false;
            this.gbProfile.Text = "射频通讯链路 ";
            // 
            // btGetProfile
            // 
            this.btGetProfile.Location = new System.Drawing.Point(734, 29);
            this.btGetProfile.Name = "btGetProfile";
            this.btGetProfile.Size = new System.Drawing.Size(90, 23);
            this.btGetProfile.TabIndex = 9;
            this.btGetProfile.Text = "读取 ";
            this.btGetProfile.UseVisualStyleBackColor = true;
            this.btGetProfile.Click += new System.EventHandler(this.btGetProfile_Click);
            // 
            // btSetProfile
            // 
            this.btSetProfile.Location = new System.Drawing.Point(859, 29);
            this.btSetProfile.Name = "btSetProfile";
            this.btSetProfile.Size = new System.Drawing.Size(90, 23);
            this.btSetProfile.TabIndex = 8;
            this.btSetProfile.Text = "设置 ";
            this.btSetProfile.UseVisualStyleBackColor = true;
            this.btSetProfile.Click += new System.EventHandler(this.btSetProfile_Click);
            // 
            // rdbProfile3
            // 
            this.rdbProfile3.AutoSize = true;
            this.rdbProfile3.Location = new System.Drawing.Point(347, 45);
            this.rdbProfile3.Name = "rdbProfile3";
            this.rdbProfile3.Size = new System.Drawing.Size(299, 16);
            this.rdbProfile3.TabIndex = 3;
            this.rdbProfile3.TabStop = true;
            this.rdbProfile3.Text = "配置3                 Tari 6.25uS; FM0 400KHz;";
            this.rdbProfile3.UseVisualStyleBackColor = true;
            // 
            // rdbProfile2
            // 
            this.rdbProfile2.AutoSize = true;
            this.rdbProfile2.Location = new System.Drawing.Point(65, 45);
            this.rdbProfile2.Name = "rdbProfile2";
            this.rdbProfile2.Size = new System.Drawing.Size(227, 16);
            this.rdbProfile2.TabIndex = 2;
            this.rdbProfile2.TabStop = true;
            this.rdbProfile2.Text = "配置2  Tari 25uS; Miller 4 300KHz;";
            this.rdbProfile2.UseVisualStyleBackColor = true;
            // 
            // rdbProfile1
            // 
            this.rdbProfile1.AutoSize = true;
            this.rdbProfile1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rdbProfile1.ForeColor = System.Drawing.Color.Black;
            this.rdbProfile1.Location = new System.Drawing.Point(347, 20);
            this.rdbProfile1.Name = "rdbProfile1";
            this.rdbProfile1.Size = new System.Drawing.Size(311, 16);
            this.rdbProfile1.TabIndex = 1;
            this.rdbProfile1.TabStop = true;
            this.rdbProfile1.Text = "配置1(推荐且为默认)   Tari 25uS; Miller 4 250KHz";
            this.rdbProfile1.UseVisualStyleBackColor = true;
            // 
            // rdbProfile0
            // 
            this.rdbProfile0.AutoSize = true;
            this.rdbProfile0.Location = new System.Drawing.Point(65, 20);
            this.rdbProfile0.Name = "rdbProfile0";
            this.rdbProfile0.Size = new System.Drawing.Size(185, 16);
            this.rdbProfile0.TabIndex = 0;
            this.rdbProfile0.TabStop = true;
            this.rdbProfile0.Text = "配置0  Tari 25uS; FM0 40KHz";
            this.rdbProfile0.UseVisualStyleBackColor = true;
            // 
            // btRfSetup
            // 
            this.btRfSetup.Location = new System.Drawing.Point(874, 493);
            this.btRfSetup.Name = "btRfSetup";
            this.btRfSetup.Size = new System.Drawing.Size(90, 23);
            this.btRfSetup.TabIndex = 17;
            this.btRfSetup.Text = "刷新界面";
            this.btRfSetup.UseVisualStyleBackColor = true;
            this.btRfSetup.Click += new System.EventHandler(this.btRfSetup_Click);
            // 
            // gbMonza
            // 
            this.gbMonza.Controls.Add(this.label14);
            this.gbMonza.Controls.Add(this.label11);
            this.gbMonza.Controls.Add(this.rdbMonzaOff);
            this.gbMonza.Controls.Add(this.btSetMonzaStatus);
            this.gbMonza.Controls.Add(this.btGetMonzaStatus);
            this.gbMonza.Controls.Add(this.rdbMonzaOn);
            this.gbMonza.ForeColor = System.Drawing.Color.Black;
            this.gbMonza.Location = new System.Drawing.Point(15, 196);
            this.gbMonza.Name = "gbMonza";
            this.gbMonza.Size = new System.Drawing.Size(967, 62);
            this.gbMonza.TabIndex = 15;
            this.gbMonza.TabStop = false;
            this.gbMonza.Text = "Impinj Monza 标签快速读取TID功能";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label14.Location = new System.Drawing.Point(48, 40);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(227, 12);
            this.label14.TabIndex = 10;
            this.label14.Text = "2.若标签不支持快速读TID请关闭此功能。";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label11.Location = new System.Drawing.Point(12, 18);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(371, 12);
            this.label11.TabIndex = 8;
            this.label11.Text = "说明: 1.只有Impinj Monza系列标签的部分型号支持快速读TID功能。";
            // 
            // rdbMonzaOff
            // 
            this.rdbMonzaOff.AutoSize = true;
            this.rdbMonzaOff.Location = new System.Drawing.Point(524, 27);
            this.rdbMonzaOff.Name = "rdbMonzaOff";
            this.rdbMonzaOff.Size = new System.Drawing.Size(47, 16);
            this.rdbMonzaOff.TabIndex = 3;
            this.rdbMonzaOff.TabStop = true;
            this.rdbMonzaOff.Text = "关闭";
            this.rdbMonzaOff.UseVisualStyleBackColor = true;
            // 
            // btSetMonzaStatus
            // 
            this.btSetMonzaStatus.Location = new System.Drawing.Point(859, 24);
            this.btSetMonzaStatus.Name = "btSetMonzaStatus";
            this.btSetMonzaStatus.Size = new System.Drawing.Size(90, 23);
            this.btSetMonzaStatus.TabIndex = 2;
            this.btSetMonzaStatus.Text = "设置";
            this.btSetMonzaStatus.UseVisualStyleBackColor = true;
            this.btSetMonzaStatus.Click += new System.EventHandler(this.btSetMonzaStatus_Click);
            // 
            // btGetMonzaStatus
            // 
            this.btGetMonzaStatus.Location = new System.Drawing.Point(734, 24);
            this.btGetMonzaStatus.Name = "btGetMonzaStatus";
            this.btGetMonzaStatus.Size = new System.Drawing.Size(90, 23);
            this.btGetMonzaStatus.TabIndex = 1;
            this.btGetMonzaStatus.Text = "读取";
            this.btGetMonzaStatus.UseVisualStyleBackColor = true;
            this.btGetMonzaStatus.Click += new System.EventHandler(this.btGetMonzaStatus_Click);
            // 
            // rdbMonzaOn
            // 
            this.rdbMonzaOn.AutoSize = true;
            this.rdbMonzaOn.Location = new System.Drawing.Point(413, 27);
            this.rdbMonzaOn.Name = "rdbMonzaOn";
            this.rdbMonzaOn.Size = new System.Drawing.Size(47, 16);
            this.rdbMonzaOn.TabIndex = 0;
            this.rdbMonzaOn.TabStop = true;
            this.rdbMonzaOn.Text = "打开";
            this.rdbMonzaOn.UseVisualStyleBackColor = true;
            // 
            // gbCmdAntDetector
            // 
            this.gbCmdAntDetector.Controls.Add(this.label7);
            this.gbCmdAntDetector.Controls.Add(this.label6);
            this.gbCmdAntDetector.Controls.Add(this.label5);
            this.gbCmdAntDetector.Controls.Add(this.label10);
            this.gbCmdAntDetector.Controls.Add(this.label8);
            this.gbCmdAntDetector.Controls.Add(this.tbAntDectector);
            this.gbCmdAntDetector.Controls.Add(this.btnGetAntDetector);
            this.gbCmdAntDetector.Controls.Add(this.btnSetAntDetector);
            this.gbCmdAntDetector.ForeColor = System.Drawing.Color.Black;
            this.gbCmdAntDetector.Location = new System.Drawing.Point(15, 118);
            this.gbCmdAntDetector.Name = "gbCmdAntDetector";
            this.gbCmdAntDetector.Size = new System.Drawing.Size(967, 72);
            this.gbCmdAntDetector.TabIndex = 13;
            this.gbCmdAntDetector.TabStop = false;
            this.gbCmdAntDetector.Text = "天线检测灵敏度";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label7.Location = new System.Drawing.Point(48, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(389, 12);
            this.label7.TabIndex = 9;
            this.label7.Text = "2.为保护设备，检测到回波损耗大于此阈值将报错并停止读写标签操作。";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label6.Location = new System.Drawing.Point(48, 56);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(347, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "3.此阈值越大对天线端口阻抗匹配要求越高，设为0关闭此功能。";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.CornflowerBlue;
            this.label5.Location = new System.Drawing.Point(12, 16);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(383, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "说明: 1.读写标签时系统自动测量天线端口的回波损耗(Return Loss)。";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(480, 36);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(83, 12);
            this.label10.TabIndex = 6;
            this.label10.Text = "回波损耗阈值:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(646, 36);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(17, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "dB";
            // 
            // tbAntDectector
            // 
            this.tbAntDectector.Location = new System.Drawing.Point(569, 32);
            this.tbAntDectector.Name = "tbAntDectector";
            this.tbAntDectector.Size = new System.Drawing.Size(71, 21);
            this.tbAntDectector.TabIndex = 4;
            this.tbAntDectector.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnGetAntDetector
            // 
            this.btnGetAntDetector.Location = new System.Drawing.Point(734, 31);
            this.btnGetAntDetector.Name = "btnGetAntDetector";
            this.btnGetAntDetector.Size = new System.Drawing.Size(90, 23);
            this.btnGetAntDetector.TabIndex = 3;
            this.btnGetAntDetector.Text = "读取 ";
            this.btnGetAntDetector.UseVisualStyleBackColor = true;
            this.btnGetAntDetector.Click += new System.EventHandler(this.btnGetAntDetector_Click);
            // 
            // btnSetAntDetector
            // 
            this.btnSetAntDetector.Location = new System.Drawing.Point(859, 31);
            this.btnSetAntDetector.Name = "btnSetAntDetector";
            this.btnSetAntDetector.Size = new System.Drawing.Size(90, 23);
            this.btnSetAntDetector.TabIndex = 2;
            this.btnSetAntDetector.Text = "设置 ";
            this.btnSetAntDetector.UseVisualStyleBackColor = true;
            this.btnSetAntDetector.Click += new System.EventHandler(this.btnSetAntDetector_Click);
            // 
            // gbCmdAntenna
            // 
            this.gbCmdAntenna.Controls.Add(this.label107);
            this.gbCmdAntenna.Controls.Add(this.cmbWorkAnt);
            this.gbCmdAntenna.Controls.Add(this.btnGetWorkAntenna);
            this.gbCmdAntenna.Controls.Add(this.btnSetWorkAntenna);
            this.gbCmdAntenna.ForeColor = System.Drawing.Color.Black;
            this.gbCmdAntenna.Location = new System.Drawing.Point(15, 12);
            this.gbCmdAntenna.Name = "gbCmdAntenna";
            this.gbCmdAntenna.Size = new System.Drawing.Size(472, 46);
            this.gbCmdAntenna.TabIndex = 14;
            this.gbCmdAntenna.TabStop = false;
            this.gbCmdAntenna.Text = "切换当前工作天线";
            // 
            // label107
            // 
            this.label107.AutoSize = true;
            this.label107.Location = new System.Drawing.Point(47, 22);
            this.label107.Name = "label107";
            this.label107.Size = new System.Drawing.Size(59, 12);
            this.label107.TabIndex = 7;
            this.label107.Text = "天线端口:";
            // 
            // cmbWorkAnt
            // 
            this.cmbWorkAnt.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWorkAnt.FormattingEnabled = true;
            this.cmbWorkAnt.Items.AddRange(new object[] {
            "天线 1",
            "天线 2",
            "天线 3",
            "天线 4"});
            this.cmbWorkAnt.Location = new System.Drawing.Point(112, 18);
            this.cmbWorkAnt.Name = "cmbWorkAnt";
            this.cmbWorkAnt.Size = new System.Drawing.Size(84, 20);
            this.cmbWorkAnt.TabIndex = 6;
            // 
            // btnGetWorkAntenna
            // 
            this.btnGetWorkAntenna.Location = new System.Drawing.Point(234, 16);
            this.btnGetWorkAntenna.Name = "btnGetWorkAntenna";
            this.btnGetWorkAntenna.Size = new System.Drawing.Size(90, 23);
            this.btnGetWorkAntenna.TabIndex = 5;
            this.btnGetWorkAntenna.Text = "读取 ";
            this.btnGetWorkAntenna.UseVisualStyleBackColor = true;
            this.btnGetWorkAntenna.Click += new System.EventHandler(this.btnGetWorkAntenna_Click);
            // 
            // btnSetWorkAntenna
            // 
            this.btnSetWorkAntenna.Location = new System.Drawing.Point(343, 16);
            this.btnSetWorkAntenna.Name = "btnSetWorkAntenna";
            this.btnSetWorkAntenna.Size = new System.Drawing.Size(90, 23);
            this.btnSetWorkAntenna.TabIndex = 4;
            this.btnSetWorkAntenna.Text = "设置 ";
            this.btnSetWorkAntenna.UseVisualStyleBackColor = true;
            this.btnSetWorkAntenna.Click += new System.EventHandler(this.btnSetWorkAntenna_Click);
            // 
            // gbCmdRegion
            // 
            this.gbCmdRegion.Controls.Add(this.cbUserDefineFreq);
            this.gbCmdRegion.Controls.Add(this.groupBox23);
            this.gbCmdRegion.Controls.Add(this.groupBox21);
            this.gbCmdRegion.Controls.Add(this.btnGetFrequencyRegion);
            this.gbCmdRegion.Controls.Add(this.btnSetFrequencyRegion);
            this.gbCmdRegion.ForeColor = System.Drawing.Color.Black;
            this.gbCmdRegion.Location = new System.Drawing.Point(15, 264);
            this.gbCmdRegion.Name = "gbCmdRegion";
            this.gbCmdRegion.Size = new System.Drawing.Size(967, 144);
            this.gbCmdRegion.TabIndex = 9;
            this.gbCmdRegion.TabStop = false;
            this.gbCmdRegion.Text = "射频频谱 ";
            // 
            // cbUserDefineFreq
            // 
            this.cbUserDefineFreq.AutoSize = true;
            this.cbUserDefineFreq.Location = new System.Drawing.Point(9, 100);
            this.cbUserDefineFreq.Name = "cbUserDefineFreq";
            this.cbUserDefineFreq.Size = new System.Drawing.Size(60, 16);
            this.cbUserDefineFreq.TabIndex = 11;
            this.cbUserDefineFreq.Text = "自定义";
            this.cbUserDefineFreq.UseVisualStyleBackColor = true;
            this.cbUserDefineFreq.CheckedChanged += new System.EventHandler(this.cbUserDefineFreq_CheckedChanged);
            // 
            // groupBox23
            // 
            this.groupBox23.Controls.Add(this.label106);
            this.groupBox23.Controls.Add(this.label105);
            this.groupBox23.Controls.Add(this.label104);
            this.groupBox23.Controls.Add(this.label103);
            this.groupBox23.Controls.Add(this.label86);
            this.groupBox23.Controls.Add(this.label75);
            this.groupBox23.Controls.Add(this.textFreqQuantity);
            this.groupBox23.Controls.Add(this.TextFreqInterval);
            this.groupBox23.Controls.Add(this.textStartFreq);
            this.groupBox23.ForeColor = System.Drawing.Color.Black;
            this.groupBox23.Location = new System.Drawing.Point(75, 77);
            this.groupBox23.Name = "groupBox23";
            this.groupBox23.Size = new System.Drawing.Size(609, 55);
            this.groupBox23.TabIndex = 10;
            this.groupBox23.TabStop = false;
            this.groupBox23.Text = "用户自定义频点";
            // 
            // label106
            // 
            this.label106.AutoSize = true;
            this.label106.Location = new System.Drawing.Point(571, 24);
            this.label106.Name = "label106";
            this.label106.Size = new System.Drawing.Size(17, 12);
            this.label106.TabIndex = 8;
            this.label106.Text = "个";
            // 
            // label105
            // 
            this.label105.AutoSize = true;
            this.label105.Location = new System.Drawing.Point(400, 24);
            this.label105.Name = "label105";
            this.label105.Size = new System.Drawing.Size(23, 12);
            this.label105.TabIndex = 7;
            this.label105.Text = "KHz";
            // 
            // label104
            // 
            this.label104.AutoSize = true;
            this.label104.Location = new System.Drawing.Point(213, 24);
            this.label104.Name = "label104";
            this.label104.Size = new System.Drawing.Size(23, 12);
            this.label104.TabIndex = 6;
            this.label104.Text = "KHz";
            // 
            // label103
            // 
            this.label103.AutoSize = true;
            this.label103.Location = new System.Drawing.Point(429, 24);
            this.label103.Name = "label103";
            this.label103.Size = new System.Drawing.Size(59, 12);
            this.label103.TabIndex = 5;
            this.label103.Text = "频点数量:";
            // 
            // label86
            // 
            this.label86.AutoSize = true;
            this.label86.Location = new System.Drawing.Point(260, 24);
            this.label86.Name = "label86";
            this.label86.Size = new System.Drawing.Size(59, 12);
            this.label86.TabIndex = 4;
            this.label86.Text = "频道间隔:";
            // 
            // label75
            // 
            this.label75.AutoSize = true;
            this.label75.Location = new System.Drawing.Point(76, 24);
            this.label75.Name = "label75";
            this.label75.Size = new System.Drawing.Size(59, 12);
            this.label75.TabIndex = 3;
            this.label75.Text = "起始频率:";
            // 
            // textFreqQuantity
            // 
            this.textFreqQuantity.Location = new System.Drawing.Point(494, 20);
            this.textFreqQuantity.Name = "textFreqQuantity";
            this.textFreqQuantity.Size = new System.Drawing.Size(71, 21);
            this.textFreqQuantity.TabIndex = 2;
            this.textFreqQuantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TextFreqInterval
            // 
            this.TextFreqInterval.Location = new System.Drawing.Point(325, 20);
            this.TextFreqInterval.Name = "TextFreqInterval";
            this.TextFreqInterval.Size = new System.Drawing.Size(71, 21);
            this.TextFreqInterval.TabIndex = 1;
            this.TextFreqInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textStartFreq
            // 
            this.textStartFreq.Location = new System.Drawing.Point(141, 20);
            this.textStartFreq.Name = "textStartFreq";
            this.textStartFreq.Size = new System.Drawing.Size(66, 21);
            this.textStartFreq.TabIndex = 0;
            this.textStartFreq.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox21
            // 
            this.groupBox21.Controls.Add(this.label37);
            this.groupBox21.Controls.Add(this.label36);
            this.groupBox21.Controls.Add(this.cmbFrequencyEnd);
            this.groupBox21.Controls.Add(this.label13);
            this.groupBox21.Controls.Add(this.cmbFrequencyStart);
            this.groupBox21.Controls.Add(this.label12);
            this.groupBox21.Controls.Add(this.rdbRegionChn);
            this.groupBox21.Controls.Add(this.rdbRegionEtsi);
            this.groupBox21.Controls.Add(this.rdbRegionFcc);
            this.groupBox21.ForeColor = System.Drawing.Color.Black;
            this.groupBox21.Location = new System.Drawing.Point(75, 16);
            this.groupBox21.Name = "groupBox21";
            this.groupBox21.Size = new System.Drawing.Size(609, 55);
            this.groupBox21.TabIndex = 9;
            this.groupBox21.TabStop = false;
            this.groupBox21.Text = "系统默认频点";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(570, 23);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(23, 12);
            this.label37.TabIndex = 17;
            this.label37.Text = "MHz";
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(403, 24);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(23, 12);
            this.label36.TabIndex = 16;
            this.label36.Text = "MHz";
            // 
            // cmbFrequencyEnd
            // 
            this.cmbFrequencyEnd.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrequencyEnd.FormattingEnabled = true;
            this.cmbFrequencyEnd.Location = new System.Drawing.Point(494, 19);
            this.cmbFrequencyEnd.Name = "cmbFrequencyEnd";
            this.cmbFrequencyEnd.Size = new System.Drawing.Size(71, 20);
            this.cmbFrequencyEnd.TabIndex = 14;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(454, 24);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 12);
            this.label13.TabIndex = 15;
            this.label13.Text = "—";
            // 
            // cmbFrequencyStart
            // 
            this.cmbFrequencyStart.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFrequencyStart.FormattingEnabled = true;
            this.cmbFrequencyStart.Location = new System.Drawing.Point(325, 19);
            this.cmbFrequencyStart.Name = "cmbFrequencyStart";
            this.cmbFrequencyStart.Size = new System.Drawing.Size(71, 20);
            this.cmbFrequencyStart.TabIndex = 12;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(260, 23);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 13;
            this.label12.Text = "频谱范围:";
            // 
            // rdbRegionChn
            // 
            this.rdbRegionChn.AutoSize = true;
            this.rdbRegionChn.Location = new System.Drawing.Point(195, 22);
            this.rdbRegionChn.Name = "rdbRegionChn";
            this.rdbRegionChn.Size = new System.Drawing.Size(41, 16);
            this.rdbRegionChn.TabIndex = 11;
            this.rdbRegionChn.TabStop = true;
            this.rdbRegionChn.Text = "CHN";
            this.rdbRegionChn.UseVisualStyleBackColor = true;
            this.rdbRegionChn.CheckedChanged += new System.EventHandler(this.rdbRegionChn_CheckedChanged);
            // 
            // rdbRegionEtsi
            // 
            this.rdbRegionEtsi.AutoSize = true;
            this.rdbRegionEtsi.Location = new System.Drawing.Point(116, 22);
            this.rdbRegionEtsi.Name = "rdbRegionEtsi";
            this.rdbRegionEtsi.Size = new System.Drawing.Size(47, 16);
            this.rdbRegionEtsi.TabIndex = 10;
            this.rdbRegionEtsi.TabStop = true;
            this.rdbRegionEtsi.Text = "ETSI";
            this.rdbRegionEtsi.UseVisualStyleBackColor = true;
            this.rdbRegionEtsi.CheckedChanged += new System.EventHandler(this.rdbRegionEtsi_CheckedChanged);
            // 
            // rdbRegionFcc
            // 
            this.rdbRegionFcc.AutoSize = true;
            this.rdbRegionFcc.Location = new System.Drawing.Point(43, 22);
            this.rdbRegionFcc.Name = "rdbRegionFcc";
            this.rdbRegionFcc.Size = new System.Drawing.Size(41, 16);
            this.rdbRegionFcc.TabIndex = 9;
            this.rdbRegionFcc.TabStop = true;
            this.rdbRegionFcc.Text = "FCC";
            this.rdbRegionFcc.UseVisualStyleBackColor = true;
            this.rdbRegionFcc.CheckedChanged += new System.EventHandler(this.rdbRegionFcc_CheckedChanged);
            // 
            // btnGetFrequencyRegion
            // 
            this.btnGetFrequencyRegion.Location = new System.Drawing.Point(734, 66);
            this.btnGetFrequencyRegion.Name = "btnGetFrequencyRegion";
            this.btnGetFrequencyRegion.Size = new System.Drawing.Size(90, 23);
            this.btnGetFrequencyRegion.TabIndex = 6;
            this.btnGetFrequencyRegion.Text = "读取 ";
            this.btnGetFrequencyRegion.UseVisualStyleBackColor = true;
            this.btnGetFrequencyRegion.Click += new System.EventHandler(this.btnGetFrequencyRegion_Click);
            // 
            // btnSetFrequencyRegion
            // 
            this.btnSetFrequencyRegion.Location = new System.Drawing.Point(859, 66);
            this.btnSetFrequencyRegion.Name = "btnSetFrequencyRegion";
            this.btnSetFrequencyRegion.Size = new System.Drawing.Size(90, 23);
            this.btnSetFrequencyRegion.TabIndex = 5;
            this.btnSetFrequencyRegion.Text = "设置 ";
            this.btnSetFrequencyRegion.UseVisualStyleBackColor = true;
            this.btnSetFrequencyRegion.Click += new System.EventHandler(this.btnSetFrequencyRegion_Click);
            // 
            // gbCmdOutputPower
            // 
            this.gbCmdOutputPower.Controls.Add(this.label151);
            this.gbCmdOutputPower.Controls.Add(this.label152);
            this.gbCmdOutputPower.Controls.Add(this.label153);
            this.gbCmdOutputPower.Controls.Add(this.label154);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_16);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_15);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_14);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_13);
            this.gbCmdOutputPower.Controls.Add(this.label147);
            this.gbCmdOutputPower.Controls.Add(this.label148);
            this.gbCmdOutputPower.Controls.Add(this.label149);
            this.gbCmdOutputPower.Controls.Add(this.label150);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_12);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_11);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_10);
            this.gbCmdOutputPower.Controls.Add(this.tb_dbm_9);
            this.gbCmdOutputPower.Controls.Add(this.label115);
            this.gbCmdOutputPower.Controls.Add(this.label114);
            this.gbCmdOutputPower.Controls.Add(this.label113);
            this.gbCmdOutputPower.Controls.Add(this.label112);
            this.gbCmdOutputPower.Controls.Add(this.textBox10);
            this.gbCmdOutputPower.Controls.Add(this.textBox9);
            this.gbCmdOutputPower.Controls.Add(this.textBox8);
            this.gbCmdOutputPower.Controls.Add(this.textBox7);
            this.gbCmdOutputPower.Controls.Add(this.label34);
            this.gbCmdOutputPower.Controls.Add(this.label21);
            this.gbCmdOutputPower.Controls.Add(this.label20);
            this.gbCmdOutputPower.Controls.Add(this.label18);
            this.gbCmdOutputPower.Controls.Add(this.textBox4);
            this.gbCmdOutputPower.Controls.Add(this.textBox3);
            this.gbCmdOutputPower.Controls.Add(this.textBox2);
            this.gbCmdOutputPower.Controls.Add(this.textBox1);
            this.gbCmdOutputPower.Controls.Add(this.label15);
            this.gbCmdOutputPower.Controls.Add(this.btnGetOutputPower);
            this.gbCmdOutputPower.Controls.Add(this.btnSetOutputPower);
            this.gbCmdOutputPower.Controls.Add(this.label9);
            this.gbCmdOutputPower.ForeColor = System.Drawing.Color.Black;
            this.gbCmdOutputPower.Location = new System.Drawing.Point(505, 8);
            this.gbCmdOutputPower.Name = "gbCmdOutputPower";
            this.gbCmdOutputPower.Size = new System.Drawing.Size(477, 107);
            this.gbCmdOutputPower.TabIndex = 8;
            this.gbCmdOutputPower.TabStop = false;
            this.gbCmdOutputPower.Text = "射频输出功率";
            // 
            // label151
            // 
            this.label151.AutoSize = true;
            this.label151.Location = new System.Drawing.Point(389, 20);
            this.label151.Name = "label151";
            this.label151.Size = new System.Drawing.Size(17, 12);
            this.label151.TabIndex = 36;
            this.label151.Text = "16";
            // 
            // label152
            // 
            this.label152.AutoSize = true;
            this.label152.Location = new System.Drawing.Point(364, 20);
            this.label152.Name = "label152";
            this.label152.Size = new System.Drawing.Size(17, 12);
            this.label152.TabIndex = 35;
            this.label152.Text = "15";
            // 
            // label153
            // 
            this.label153.AutoSize = true;
            this.label153.Location = new System.Drawing.Point(342, 20);
            this.label153.Name = "label153";
            this.label153.Size = new System.Drawing.Size(17, 12);
            this.label153.TabIndex = 34;
            this.label153.Text = "14";
            // 
            // label154
            // 
            this.label154.AutoSize = true;
            this.label154.Location = new System.Drawing.Point(316, 20);
            this.label154.Name = "label154";
            this.label154.Size = new System.Drawing.Size(17, 12);
            this.label154.TabIndex = 33;
            this.label154.Text = "13";
            // 
            // tb_dbm_16
            // 
            this.tb_dbm_16.Location = new System.Drawing.Point(388, 35);
            this.tb_dbm_16.Name = "tb_dbm_16";
            this.tb_dbm_16.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_16.TabIndex = 32;
            this.tb_dbm_16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_15
            // 
            this.tb_dbm_15.Location = new System.Drawing.Point(363, 35);
            this.tb_dbm_15.Name = "tb_dbm_15";
            this.tb_dbm_15.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_15.TabIndex = 31;
            this.tb_dbm_15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_14
            // 
            this.tb_dbm_14.Location = new System.Drawing.Point(341, 35);
            this.tb_dbm_14.Name = "tb_dbm_14";
            this.tb_dbm_14.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_14.TabIndex = 30;
            this.tb_dbm_14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_13
            // 
            this.tb_dbm_13.Location = new System.Drawing.Point(316, 35);
            this.tb_dbm_13.Name = "tb_dbm_13";
            this.tb_dbm_13.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_13.TabIndex = 29;
            this.tb_dbm_13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label147
            // 
            this.label147.AutoSize = true;
            this.label147.Location = new System.Drawing.Point(290, 20);
            this.label147.Name = "label147";
            this.label147.Size = new System.Drawing.Size(17, 12);
            this.label147.TabIndex = 28;
            this.label147.Text = "12";
            // 
            // label148
            // 
            this.label148.AutoSize = true;
            this.label148.Location = new System.Drawing.Point(265, 20);
            this.label148.Name = "label148";
            this.label148.Size = new System.Drawing.Size(17, 12);
            this.label148.TabIndex = 27;
            this.label148.Text = "11";
            // 
            // label149
            // 
            this.label149.AutoSize = true;
            this.label149.Location = new System.Drawing.Point(242, 20);
            this.label149.Name = "label149";
            this.label149.Size = new System.Drawing.Size(17, 12);
            this.label149.TabIndex = 26;
            this.label149.Text = "10";
            // 
            // label150
            // 
            this.label150.AutoSize = true;
            this.label150.Location = new System.Drawing.Point(222, 20);
            this.label150.Name = "label150";
            this.label150.Size = new System.Drawing.Size(11, 12);
            this.label150.TabIndex = 25;
            this.label150.Text = "9";
            // 
            // tb_dbm_12
            // 
            this.tb_dbm_12.Location = new System.Drawing.Point(289, 35);
            this.tb_dbm_12.Name = "tb_dbm_12";
            this.tb_dbm_12.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_12.TabIndex = 24;
            this.tb_dbm_12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_11
            // 
            this.tb_dbm_11.Location = new System.Drawing.Point(264, 35);
            this.tb_dbm_11.Name = "tb_dbm_11";
            this.tb_dbm_11.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_11.TabIndex = 23;
            this.tb_dbm_11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_10
            // 
            this.tb_dbm_10.Location = new System.Drawing.Point(242, 35);
            this.tb_dbm_10.Name = "tb_dbm_10";
            this.tb_dbm_10.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_10.TabIndex = 22;
            this.tb_dbm_10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tb_dbm_9
            // 
            this.tb_dbm_9.Location = new System.Drawing.Point(217, 35);
            this.tb_dbm_9.Name = "tb_dbm_9";
            this.tb_dbm_9.Size = new System.Drawing.Size(17, 21);
            this.tb_dbm_9.TabIndex = 21;
            this.tb_dbm_9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label115
            // 
            this.label115.AutoSize = true;
            this.label115.Location = new System.Drawing.Point(198, 20);
            this.label115.Name = "label115";
            this.label115.Size = new System.Drawing.Size(11, 12);
            this.label115.TabIndex = 20;
            this.label115.Text = "8";
            // 
            // label114
            // 
            this.label114.AutoSize = true;
            this.label114.Location = new System.Drawing.Point(173, 20);
            this.label114.Name = "label114";
            this.label114.Size = new System.Drawing.Size(11, 12);
            this.label114.TabIndex = 19;
            this.label114.Text = "7";
            // 
            // label113
            // 
            this.label113.AutoSize = true;
            this.label113.Location = new System.Drawing.Point(151, 20);
            this.label113.Name = "label113";
            this.label113.Size = new System.Drawing.Size(11, 12);
            this.label113.TabIndex = 18;
            this.label113.Text = "6";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Location = new System.Drawing.Point(126, 20);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(11, 12);
            this.label112.TabIndex = 17;
            this.label112.Text = "5";
            // 
            // textBox10
            // 
            this.textBox10.Location = new System.Drawing.Point(193, 35);
            this.textBox10.Name = "textBox10";
            this.textBox10.Size = new System.Drawing.Size(17, 21);
            this.textBox10.TabIndex = 16;
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox10.TextChanged += new System.EventHandler(this.textBox10_TextChanged);
            // 
            // textBox9
            // 
            this.textBox9.Location = new System.Drawing.Point(168, 35);
            this.textBox9.Name = "textBox9";
            this.textBox9.Size = new System.Drawing.Size(17, 21);
            this.textBox9.TabIndex = 15;
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox9.TextChanged += new System.EventHandler(this.textBox9_TextChanged);
            // 
            // textBox8
            // 
            this.textBox8.Location = new System.Drawing.Point(146, 35);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(17, 21);
            this.textBox8.TabIndex = 14;
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox8.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            // 
            // textBox7
            // 
            this.textBox7.Location = new System.Drawing.Point(121, 35);
            this.textBox7.Name = "textBox7";
            this.textBox7.Size = new System.Drawing.Size(17, 21);
            this.textBox7.TabIndex = 13;
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox7.TextChanged += new System.EventHandler(this.textBox7_TextChanged);
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(98, 20);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(11, 12);
            this.label34.TabIndex = 12;
            this.label34.Text = "4";
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(74, 20);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(11, 12);
            this.label21.TabIndex = 11;
            this.label21.Text = "3";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(50, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(11, 12);
            this.label20.TabIndex = 10;
            this.label20.Text = "2";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(24, 20);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(11, 12);
            this.label18.TabIndex = 9;
            this.label18.Text = "1";
            // 
            // textBox4
            // 
            this.textBox4.Location = new System.Drawing.Point(96, 35);
            this.textBox4.Name = "textBox4";
            this.textBox4.Size = new System.Drawing.Size(17, 21);
            this.textBox4.TabIndex = 7;
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.TextChanged += new System.EventHandler(this.textBox4_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(71, 35);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(17, 21);
            this.textBox3.TabIndex = 6;
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(49, 35);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(17, 21);
            this.textBox2.TabIndex = 5;
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(24, 35);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(17, 21);
            this.textBox1.TabIndex = 4;
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(416, 20);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(41, 12);
            this.label15.TabIndex = 3;
            this.label15.Text = "天线号";
            // 
            // btnGetOutputPower
            // 
            this.btnGetOutputPower.Location = new System.Drawing.Point(110, 72);
            this.btnGetOutputPower.Name = "btnGetOutputPower";
            this.btnGetOutputPower.Size = new System.Drawing.Size(90, 23);
            this.btnGetOutputPower.TabIndex = 2;
            this.btnGetOutputPower.Text = "读取 ";
            this.btnGetOutputPower.UseVisualStyleBackColor = true;
            this.btnGetOutputPower.Click += new System.EventHandler(this.btnGetOutputPower_Click);
            // 
            // btnSetOutputPower
            // 
            this.btnSetOutputPower.Location = new System.Drawing.Point(283, 72);
            this.btnSetOutputPower.Name = "btnSetOutputPower";
            this.btnSetOutputPower.Size = new System.Drawing.Size(90, 23);
            this.btnSetOutputPower.TabIndex = 1;
            this.btnSetOutputPower.Text = "设置 ";
            this.btnSetOutputPower.UseVisualStyleBackColor = true;
            this.btnSetOutputPower.Click += new System.EventHandler(this.btnSetOutputPower_Click);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(416, 38);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(23, 12);
            this.label9.TabIndex = 0;
            this.label9.Text = "dBm";
            // 
            // pageEpcTest
            // 
            this.pageEpcTest.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pageEpcTest.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pageEpcTest.Controls.Add(this.tabEpcTest);
            this.pageEpcTest.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageEpcTest.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pageEpcTest.Location = new System.Drawing.Point(4, 22);
            this.pageEpcTest.Name = "pageEpcTest";
            this.pageEpcTest.Size = new System.Drawing.Size(1010, 555);
            this.pageEpcTest.TabIndex = 5;
            this.pageEpcTest.Text = "18000-6C标签测试";
            // 
            // tabEpcTest
            // 
            this.tabEpcTest.Controls.Add(this.pageRealMode);
            this.tabEpcTest.Controls.Add(this.pageBufferedMode);
            this.tabEpcTest.Controls.Add(this.pageFast4AntMode);
            this.tabEpcTest.Controls.Add(this.pageAcessTag);
            this.tabEpcTest.Controls.Add(this.tabPage3);
            this.tabEpcTest.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.tabEpcTest.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tabEpcTest.Location = new System.Drawing.Point(0, 5);
            this.tabEpcTest.Name = "tabEpcTest";
            this.tabEpcTest.SelectedIndex = 0;
            this.tabEpcTest.Size = new System.Drawing.Size(1008, 548);
            this.tabEpcTest.TabIndex = 0;
            this.tabEpcTest.TabStop = false;
            this.tabEpcTest.SelectedIndexChanged += new System.EventHandler(this.tabEpcTest_SelectedIndexChanged);
            // 
            // pageRealMode
            // 
            this.pageRealMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pageRealMode.Controls.Add(this.tableLayoutPanel1);
            this.pageRealMode.Controls.Add(this.excel_format_rb);
            this.pageRealMode.Controls.Add(this.txt_format_rb);
            this.pageRealMode.Controls.Add(this.button8);
            this.pageRealMode.Controls.Add(this.groupBox20);
            this.pageRealMode.Controls.Add(this.tbRealMinRssi);
            this.pageRealMode.Controls.Add(this.tbRealMaxRssi);
            this.pageRealMode.Controls.Add(this.btRealFresh);
            this.pageRealMode.Controls.Add(this.label70);
            this.pageRealMode.Controls.Add(this.label74);
            this.pageRealMode.Controls.Add(this.lbRealTagCount);
            this.pageRealMode.Controls.Add(this.groupBox1);
            this.pageRealMode.Controls.Add(this.lvRealList);
            this.pageRealMode.Font = new System.Drawing.Font("SimSun", 10.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.pageRealMode.Location = new System.Drawing.Point(4, 22);
            this.pageRealMode.Name = "pageRealMode";
            this.pageRealMode.Padding = new System.Windows.Forms.Padding(3);
            this.pageRealMode.Size = new System.Drawing.Size(1000, 522);
            this.pageRealMode.TabIndex = 1;
            this.pageRealMode.Text = "盘存标签(实时模式)";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(1, 4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(992, 56);
            this.tableLayoutPanel1.TabIndex = 64;
            // 
            // panel5
            // 
            this.panel5.Controls.Add(this.customizedExeTime);
            this.panel5.Controls.Add(this.label127);
            this.panel5.Controls.Add(this.Duration);
            this.panel5.Controls.Add(this.mSessionExeTime);
            this.panel5.Controls.Add(this.btRealTimeInventory);
            this.panel5.Controls.Add(this.label84);
            this.panel5.Controls.Add(this.textRealRound);
            this.panel5.Controls.Add(this.sessionInventoryrb);
            this.panel5.Controls.Add(this.autoInventoryrb);
            this.panel5.Controls.Add(this.m_session_q_cb);
            this.panel5.Controls.Add(this.m_session_sl_cb);
            this.panel5.Controls.Add(this.m_session_max_q);
            this.panel5.Controls.Add(this.m_session_min_q);
            this.panel5.Controls.Add(this.m_session_start_q);
            this.panel5.Controls.Add(this.m_max_q_content);
            this.panel5.Controls.Add(this.m_min_q_content);
            this.panel5.Controls.Add(this.m_start_q_content);
            this.panel5.Controls.Add(this.m_session_sl);
            this.panel5.Controls.Add(this.m_sl_content);
            this.panel5.Controls.Add(this.cmbTarget);
            this.panel5.Controls.Add(this.label98);
            this.panel5.Controls.Add(this.cmbSession);
            this.panel5.Controls.Add(this.label97);
            this.panel5.Location = new System.Drawing.Point(4, 4);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(984, 48);
            this.panel5.TabIndex = 1;
            // 
            // customizedExeTime
            // 
            this.customizedExeTime.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.customizedExeTime.Location = new System.Drawing.Point(316, 25);
            this.customizedExeTime.Name = "customizedExeTime";
            this.customizedExeTime.Size = new System.Drawing.Size(52, 21);
            this.customizedExeTime.TabIndex = 71;
            this.customizedExeTime.Text = "0";
            this.customizedExeTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label127
            // 
            this.label127.AutoSize = true;
            this.label127.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label127.Location = new System.Drawing.Point(252, 29);
            this.label127.Name = "label127";
            this.label127.Size = new System.Drawing.Size(59, 12);
            this.label127.TabIndex = 70;
            this.label127.Text = "运行时间:";
            // 
            // Duration
            // 
            this.Duration.AutoSize = true;
            this.Duration.Enabled = false;
            this.Duration.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Duration.Location = new System.Drawing.Point(836, 4);
            this.Duration.Name = "Duration";
            this.Duration.Size = new System.Drawing.Size(53, 12);
            this.Duration.TabIndex = 69;
            this.Duration.Text = "Duration";
            this.Duration.Visible = false;
            // 
            // mSessionExeTime
            // 
            this.mSessionExeTime.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.mSessionExeTime.Enabled = false;
            this.mSessionExeTime.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mSessionExeTime.FormattingEnabled = true;
            this.mSessionExeTime.Location = new System.Drawing.Point(827, 22);
            this.mSessionExeTime.Name = "mSessionExeTime";
            this.mSessionExeTime.Size = new System.Drawing.Size(87, 20);
            this.mSessionExeTime.TabIndex = 68;
            this.mSessionExeTime.Visible = false;
            // 
            // btRealTimeInventory
            // 
            this.btRealTimeInventory.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btRealTimeInventory.ForeColor = System.Drawing.Color.DarkBlue;
            this.btRealTimeInventory.Location = new System.Drawing.Point(9, 5);
            this.btRealTimeInventory.Name = "btRealTimeInventory";
            this.btRealTimeInventory.Size = new System.Drawing.Size(144, 38);
            this.btRealTimeInventory.TabIndex = 1;
            this.btRealTimeInventory.Text = "开始盘存";
            this.btRealTimeInventory.UseVisualStyleBackColor = true;
            this.btRealTimeInventory.Click += new System.EventHandler(this.btRealTimeInventory_Click);
            // 
            // label84
            // 
            this.label84.AutoSize = true;
            this.label84.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label84.Location = new System.Drawing.Point(193, 8);
            this.label84.Name = "label84";
            this.label84.Size = new System.Drawing.Size(119, 12);
            this.label84.TabIndex = 2;
            this.label84.Text = "每条命令的盘存次数:";
            // 
            // textRealRound
            // 
            this.textRealRound.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textRealRound.Location = new System.Drawing.Point(317, 3);
            this.textRealRound.Name = "textRealRound";
            this.textRealRound.Size = new System.Drawing.Size(52, 21);
            this.textRealRound.TabIndex = 48;
            this.textRealRound.Text = "1";
            this.textRealRound.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // sessionInventoryrb
            // 
            this.sessionInventoryrb.AutoSize = true;
            this.sessionInventoryrb.Location = new System.Drawing.Point(392, 28);
            this.sessionInventoryrb.Name = "sessionInventoryrb";
            this.sessionInventoryrb.Size = new System.Drawing.Size(116, 18);
            this.sessionInventoryrb.TabIndex = 67;
            this.sessionInventoryrb.TabStop = true;
            this.sessionInventoryrb.Text = "自定义Session";
            this.sessionInventoryrb.UseVisualStyleBackColor = true;
            this.sessionInventoryrb.CheckedChanged += new System.EventHandler(this.sessionInventoryrb_CheckedChanged);
            // 
            // autoInventoryrb
            // 
            this.autoInventoryrb.AutoSize = true;
            this.autoInventoryrb.Checked = true;
            this.autoInventoryrb.Location = new System.Drawing.Point(393, 4);
            this.autoInventoryrb.Name = "autoInventoryrb";
            this.autoInventoryrb.Size = new System.Drawing.Size(53, 18);
            this.autoInventoryrb.TabIndex = 66;
            this.autoInventoryrb.TabStop = true;
            this.autoInventoryrb.Text = "自动";
            this.autoInventoryrb.UseVisualStyleBackColor = true;
            this.autoInventoryrb.CheckedChanged += new System.EventHandler(this.autoInventoryrb_CheckedChanged);
            // 
            // m_session_q_cb
            // 
            this.m_session_q_cb.AutoSize = true;
            this.m_session_q_cb.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_q_cb.Location = new System.Drawing.Point(803, 24);
            this.m_session_q_cb.Name = "m_session_q_cb";
            this.m_session_q_cb.Size = new System.Drawing.Size(15, 14);
            this.m_session_q_cb.TabIndex = 65;
            this.m_session_q_cb.UseVisualStyleBackColor = true;
            this.m_session_q_cb.Visible = false;
            this.m_session_q_cb.CheckedChanged += new System.EventHandler(this.m_session_q_cb_CheckedChanged);
            // 
            // m_session_sl_cb
            // 
            this.m_session_sl_cb.AutoSize = true;
            this.m_session_sl_cb.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_sl_cb.Location = new System.Drawing.Point(722, 25);
            this.m_session_sl_cb.Name = "m_session_sl_cb";
            this.m_session_sl_cb.Size = new System.Drawing.Size(15, 14);
            this.m_session_sl_cb.TabIndex = 64;
            this.m_session_sl_cb.UseVisualStyleBackColor = true;
            this.m_session_sl_cb.CheckedChanged += new System.EventHandler(this.m_session_sl_cb_CheckedChanged);
            // 
            // m_session_max_q
            // 
            this.m_session_max_q.Enabled = false;
            this.m_session_max_q.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_max_q.Location = new System.Drawing.Point(940, 22);
            this.m_session_max_q.Name = "m_session_max_q";
            this.m_session_max_q.Size = new System.Drawing.Size(28, 21);
            this.m_session_max_q.TabIndex = 63;
            this.m_session_max_q.Text = "0";
            this.m_session_max_q.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_session_max_q.Visible = false;
            // 
            // m_session_min_q
            // 
            this.m_session_min_q.Enabled = false;
            this.m_session_min_q.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_min_q.Location = new System.Drawing.Point(884, 21);
            this.m_session_min_q.Name = "m_session_min_q";
            this.m_session_min_q.Size = new System.Drawing.Size(28, 21);
            this.m_session_min_q.TabIndex = 62;
            this.m_session_min_q.Text = "0";
            this.m_session_min_q.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_session_min_q.Visible = false;
            // 
            // m_session_start_q
            // 
            this.m_session_start_q.Enabled = false;
            this.m_session_start_q.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_start_q.Location = new System.Drawing.Point(827, 20);
            this.m_session_start_q.Name = "m_session_start_q";
            this.m_session_start_q.Size = new System.Drawing.Size(28, 21);
            this.m_session_start_q.TabIndex = 61;
            this.m_session_start_q.Text = "0";
            this.m_session_start_q.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.m_session_start_q.Visible = false;
            // 
            // m_max_q_content
            // 
            this.m_max_q_content.AutoSize = true;
            this.m_max_q_content.Enabled = false;
            this.m_max_q_content.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_max_q_content.Location = new System.Drawing.Point(938, 5);
            this.m_max_q_content.Name = "m_max_q_content";
            this.m_max_q_content.Size = new System.Drawing.Size(35, 12);
            this.m_max_q_content.TabIndex = 60;
            this.m_max_q_content.Text = "Max Q";
            this.m_max_q_content.Visible = false;
            // 
            // m_min_q_content
            // 
            this.m_min_q_content.AutoSize = true;
            this.m_min_q_content.Enabled = false;
            this.m_min_q_content.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_min_q_content.Location = new System.Drawing.Point(882, 5);
            this.m_min_q_content.Name = "m_min_q_content";
            this.m_min_q_content.Size = new System.Drawing.Size(35, 12);
            this.m_min_q_content.TabIndex = 59;
            this.m_min_q_content.Text = "Min Q";
            this.m_min_q_content.Visible = false;
            // 
            // m_start_q_content
            // 
            this.m_start_q_content.AutoSize = true;
            this.m_start_q_content.Enabled = false;
            this.m_start_q_content.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_start_q_content.Location = new System.Drawing.Point(816, 4);
            this.m_start_q_content.Name = "m_start_q_content";
            this.m_start_q_content.Size = new System.Drawing.Size(47, 12);
            this.m_start_q_content.TabIndex = 58;
            this.m_start_q_content.Text = "Start Q";
            this.m_start_q_content.Visible = false;
            // 
            // m_session_sl
            // 
            this.m_session_sl.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_session_sl.Enabled = false;
            this.m_session_sl.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_session_sl.FormattingEnabled = true;
            this.m_session_sl.Items.AddRange(new object[] {
            "00",
            "01",
            "10",
            "11"});
            this.m_session_sl.Location = new System.Drawing.Point(748, 22);
            this.m_session_sl.Name = "m_session_sl";
            this.m_session_sl.Size = new System.Drawing.Size(62, 20);
            this.m_session_sl.TabIndex = 57;
            // 
            // m_sl_content
            // 
            this.m_sl_content.AutoSize = true;
            this.m_sl_content.Enabled = false;
            this.m_sl_content.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_sl_content.Location = new System.Drawing.Point(775, 4);
            this.m_sl_content.Name = "m_sl_content";
            this.m_sl_content.Size = new System.Drawing.Size(17, 12);
            this.m_sl_content.TabIndex = 56;
            this.m_sl_content.Text = "SL";
            this.m_sl_content.Click += new System.EventHandler(this.label125_Click);
            // 
            // cmbTarget
            // 
            this.cmbTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTarget.Enabled = false;
            this.cmbTarget.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbTarget.FormattingEnabled = true;
            this.cmbTarget.Items.AddRange(new object[] {
            "A",
            "B"});
            this.cmbTarget.Location = new System.Drawing.Point(632, 23);
            this.cmbTarget.Name = "cmbTarget";
            this.cmbTarget.Size = new System.Drawing.Size(62, 20);
            this.cmbTarget.TabIndex = 54;
            // 
            // label98
            // 
            this.label98.AutoSize = true;
            this.label98.Enabled = false;
            this.label98.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label98.Location = new System.Drawing.Point(609, 3);
            this.label98.Name = "label98";
            this.label98.Size = new System.Drawing.Size(101, 12);
            this.label98.TabIndex = 53;
            this.label98.Text = "Inventoried Flag";
            // 
            // cmbSession
            // 
            this.cmbSession.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSession.Enabled = false;
            this.cmbSession.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbSession.FormattingEnabled = true;
            this.cmbSession.Items.AddRange(new object[] {
            "S0",
            "S1",
            "S2",
            "S3",
            "SL"});
            this.cmbSession.Location = new System.Drawing.Point(530, 23);
            this.cmbSession.Name = "cmbSession";
            this.cmbSession.Size = new System.Drawing.Size(62, 20);
            this.cmbSession.TabIndex = 52;
            // 
            // label97
            // 
            this.label97.AutoSize = true;
            this.label97.Enabled = false;
            this.label97.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label97.Location = new System.Drawing.Point(530, 4);
            this.label97.Name = "label97";
            this.label97.Size = new System.Drawing.Size(65, 12);
            this.label97.TabIndex = 51;
            this.label97.Text = "Session ID";
            // 
            // excel_format_rb
            // 
            this.excel_format_rb.AutoSize = true;
            this.excel_format_rb.Location = new System.Drawing.Point(838, 289);
            this.excel_format_rb.Name = "excel_format_rb";
            this.excel_format_rb.Size = new System.Drawing.Size(60, 18);
            this.excel_format_rb.TabIndex = 63;
            this.excel_format_rb.Text = "EXCEL";
            this.excel_format_rb.UseVisualStyleBackColor = true;
            // 
            // txt_format_rb
            // 
            this.txt_format_rb.AutoSize = true;
            this.txt_format_rb.Checked = true;
            this.txt_format_rb.Location = new System.Drawing.Point(778, 289);
            this.txt_format_rb.Name = "txt_format_rb";
            this.txt_format_rb.Size = new System.Drawing.Size(46, 18);
            this.txt_format_rb.TabIndex = 62;
            this.txt_format_rb.TabStop = true;
            this.txt_format_rb.Text = "TXT";
            this.txt_format_rb.UseVisualStyleBackColor = true;
            // 
            // button8
            // 
            this.button8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button8.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button8.Location = new System.Drawing.Point(904, 285);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(89, 23);
            this.button8.TabIndex = 61;
            this.button8.Text = "保存标签";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // groupBox20
            // 
            this.groupBox20.Controls.Add(this.cbRealWorkant16);
            this.groupBox20.Controls.Add(this.cbRealWorkant15);
            this.groupBox20.Controls.Add(this.cbRealWorkant14);
            this.groupBox20.Controls.Add(this.cbRealWorkant13);
            this.groupBox20.Controls.Add(this.cbRealWorkant9);
            this.groupBox20.Controls.Add(this.cbRealWorkant12);
            this.groupBox20.Controls.Add(this.cbRealWorkant11);
            this.groupBox20.Controls.Add(this.cbRealWorkant10);
            this.groupBox20.Controls.Add(this.cbRealWorkant8);
            this.groupBox20.Controls.Add(this.cbRealWorkant7);
            this.groupBox20.Controls.Add(this.cbRealWorkant6);
            this.groupBox20.Controls.Add(this.cbRealWorkant5);
            this.groupBox20.Controls.Add(this.cbRealWorkant1);
            this.groupBox20.Controls.Add(this.cbRealWorkant4);
            this.groupBox20.Controls.Add(this.cbRealWorkant3);
            this.groupBox20.Controls.Add(this.cbRealWorkant2);
            this.groupBox20.Controls.Add(this.label19);
            this.groupBox20.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox20.ForeColor = System.Drawing.Color.Black;
            this.groupBox20.Location = new System.Drawing.Point(3, 66);
            this.groupBox20.Name = "groupBox20";
            this.groupBox20.Size = new System.Drawing.Size(994, 53);
            this.groupBox20.TabIndex = 49;
            this.groupBox20.TabStop = false;
            this.groupBox20.Text = "选择天线 ";
            // 
            // cbRealWorkant16
            // 
            this.cbRealWorkant16.AutoSize = true;
            this.cbRealWorkant16.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant16.Location = new System.Drawing.Point(932, 20);
            this.cbRealWorkant16.Name = "cbRealWorkant16";
            this.cbRealWorkant16.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant16.TabIndex = 91;
            this.cbRealWorkant16.Text = "天线16";
            this.cbRealWorkant16.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant15
            // 
            this.cbRealWorkant15.AutoSize = true;
            this.cbRealWorkant15.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant15.Location = new System.Drawing.Point(869, 20);
            this.cbRealWorkant15.Name = "cbRealWorkant15";
            this.cbRealWorkant15.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant15.TabIndex = 90;
            this.cbRealWorkant15.Text = "天线15";
            this.cbRealWorkant15.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant14
            // 
            this.cbRealWorkant14.AutoSize = true;
            this.cbRealWorkant14.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant14.Location = new System.Drawing.Point(805, 20);
            this.cbRealWorkant14.Name = "cbRealWorkant14";
            this.cbRealWorkant14.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant14.TabIndex = 89;
            this.cbRealWorkant14.Text = "天线14";
            this.cbRealWorkant14.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant13
            // 
            this.cbRealWorkant13.AutoSize = true;
            this.cbRealWorkant13.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant13.Location = new System.Drawing.Point(741, 20);
            this.cbRealWorkant13.Name = "cbRealWorkant13";
            this.cbRealWorkant13.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant13.TabIndex = 88;
            this.cbRealWorkant13.Text = "天线13";
            this.cbRealWorkant13.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant9
            // 
            this.cbRealWorkant9.AutoSize = true;
            this.cbRealWorkant9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant9.Location = new System.Drawing.Point(491, 20);
            this.cbRealWorkant9.Name = "cbRealWorkant9";
            this.cbRealWorkant9.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant9.TabIndex = 87;
            this.cbRealWorkant9.Text = "天线9";
            this.cbRealWorkant9.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant12
            // 
            this.cbRealWorkant12.AutoSize = true;
            this.cbRealWorkant12.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant12.Location = new System.Drawing.Point(680, 20);
            this.cbRealWorkant12.Name = "cbRealWorkant12";
            this.cbRealWorkant12.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant12.TabIndex = 86;
            this.cbRealWorkant12.Text = "天线12";
            this.cbRealWorkant12.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant11
            // 
            this.cbRealWorkant11.AutoSize = true;
            this.cbRealWorkant11.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant11.Location = new System.Drawing.Point(617, 20);
            this.cbRealWorkant11.Name = "cbRealWorkant11";
            this.cbRealWorkant11.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant11.TabIndex = 85;
            this.cbRealWorkant11.Text = "天线11";
            this.cbRealWorkant11.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant10
            // 
            this.cbRealWorkant10.AutoSize = true;
            this.cbRealWorkant10.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant10.Location = new System.Drawing.Point(551, 20);
            this.cbRealWorkant10.Name = "cbRealWorkant10";
            this.cbRealWorkant10.Size = new System.Drawing.Size(60, 16);
            this.cbRealWorkant10.TabIndex = 84;
            this.cbRealWorkant10.Text = "天线10";
            this.cbRealWorkant10.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant8
            // 
            this.cbRealWorkant8.AutoSize = true;
            this.cbRealWorkant8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant8.Location = new System.Drawing.Point(431, 20);
            this.cbRealWorkant8.Name = "cbRealWorkant8";
            this.cbRealWorkant8.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant8.TabIndex = 72;
            this.cbRealWorkant8.Text = "天线8";
            this.cbRealWorkant8.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant7
            // 
            this.cbRealWorkant7.AutoSize = true;
            this.cbRealWorkant7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant7.Location = new System.Drawing.Point(370, 20);
            this.cbRealWorkant7.Name = "cbRealWorkant7";
            this.cbRealWorkant7.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant7.TabIndex = 71;
            this.cbRealWorkant7.Text = "天线7";
            this.cbRealWorkant7.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant6
            // 
            this.cbRealWorkant6.AutoSize = true;
            this.cbRealWorkant6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant6.Location = new System.Drawing.Point(309, 20);
            this.cbRealWorkant6.Name = "cbRealWorkant6";
            this.cbRealWorkant6.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant6.TabIndex = 70;
            this.cbRealWorkant6.Text = "天线6";
            this.cbRealWorkant6.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant5
            // 
            this.cbRealWorkant5.AutoSize = true;
            this.cbRealWorkant5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant5.Location = new System.Drawing.Point(248, 20);
            this.cbRealWorkant5.Name = "cbRealWorkant5";
            this.cbRealWorkant5.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant5.TabIndex = 69;
            this.cbRealWorkant5.Text = "天线5";
            this.cbRealWorkant5.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant1
            // 
            this.cbRealWorkant1.AutoSize = true;
            this.cbRealWorkant1.Checked = true;
            this.cbRealWorkant1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbRealWorkant1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant1.Location = new System.Drawing.Point(8, 20);
            this.cbRealWorkant1.Name = "cbRealWorkant1";
            this.cbRealWorkant1.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant1.TabIndex = 68;
            this.cbRealWorkant1.Text = "天线1";
            this.cbRealWorkant1.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant4
            // 
            this.cbRealWorkant4.AutoSize = true;
            this.cbRealWorkant4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant4.Location = new System.Drawing.Point(188, 20);
            this.cbRealWorkant4.Name = "cbRealWorkant4";
            this.cbRealWorkant4.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant4.TabIndex = 67;
            this.cbRealWorkant4.Text = "天线4";
            this.cbRealWorkant4.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant3
            // 
            this.cbRealWorkant3.AutoSize = true;
            this.cbRealWorkant3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant3.Location = new System.Drawing.Point(128, 20);
            this.cbRealWorkant3.Name = "cbRealWorkant3";
            this.cbRealWorkant3.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant3.TabIndex = 66;
            this.cbRealWorkant3.Text = "天线3";
            this.cbRealWorkant3.UseVisualStyleBackColor = true;
            // 
            // cbRealWorkant2
            // 
            this.cbRealWorkant2.AutoSize = true;
            this.cbRealWorkant2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbRealWorkant2.Location = new System.Drawing.Point(68, 20);
            this.cbRealWorkant2.Name = "cbRealWorkant2";
            this.cbRealWorkant2.Size = new System.Drawing.Size(54, 16);
            this.cbRealWorkant2.TabIndex = 65;
            this.cbRealWorkant2.Text = "天线2";
            this.cbRealWorkant2.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Location = new System.Drawing.Point(51, 19);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(0, 12);
            this.label19.TabIndex = 0;
            // 
            // tbRealMinRssi
            // 
            this.tbRealMinRssi.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRealMinRssi.Location = new System.Drawing.Point(365, 287);
            this.tbRealMinRssi.Name = "tbRealMinRssi";
            this.tbRealMinRssi.ReadOnly = true;
            this.tbRealMinRssi.Size = new System.Drawing.Size(62, 21);
            this.tbRealMinRssi.TabIndex = 46;
            this.tbRealMinRssi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tbRealMaxRssi
            // 
            this.tbRealMaxRssi.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.tbRealMaxRssi.Location = new System.Drawing.Point(517, 287);
            this.tbRealMaxRssi.Name = "tbRealMaxRssi";
            this.tbRealMaxRssi.ReadOnly = true;
            this.tbRealMaxRssi.Size = new System.Drawing.Size(62, 21);
            this.tbRealMaxRssi.TabIndex = 47;
            this.tbRealMaxRssi.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btRealFresh
            // 
            this.btRealFresh.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btRealFresh.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btRealFresh.Location = new System.Drawing.Point(627, 285);
            this.btRealFresh.Name = "btRealFresh";
            this.btRealFresh.Size = new System.Drawing.Size(89, 23);
            this.btRealFresh.TabIndex = 45;
            this.btRealFresh.Text = "刷新界面";
            this.btRealFresh.UseVisualStyleBackColor = true;
            this.btRealFresh.Click += new System.EventHandler(this.btRealFresh_Click);
            // 
            // label70
            // 
            this.label70.AutoSize = true;
            this.label70.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label70.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label70.Location = new System.Drawing.Point(448, 293);
            this.label70.Name = "label70";
            this.label70.Size = new System.Drawing.Size(59, 12);
            this.label70.TabIndex = 43;
            this.label70.Text = "Max RSSI:";
            // 
            // label74
            // 
            this.label74.AutoSize = true;
            this.label74.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label74.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label74.Location = new System.Drawing.Point(300, 293);
            this.label74.Name = "label74";
            this.label74.Size = new System.Drawing.Size(59, 12);
            this.label74.TabIndex = 44;
            this.label74.Text = "Min RSSI:";
            // 
            // lbRealTagCount
            // 
            this.lbRealTagCount.AutoSize = true;
            this.lbRealTagCount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lbRealTagCount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.lbRealTagCount.Location = new System.Drawing.Point(7, 290);
            this.lbRealTagCount.Name = "lbRealTagCount";
            this.lbRealTagCount.Size = new System.Drawing.Size(65, 12);
            this.lbRealTagCount.TabIndex = 42;
            this.lbRealTagCount.Text = "标签列表: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ledReal3);
            this.groupBox1.Controls.Add(this.comboBox6);
            this.groupBox1.Controls.Add(this.ledReal5);
            this.groupBox1.Controls.Add(this.ledReal2);
            this.groupBox1.Controls.Add(this.ledReal4);
            this.groupBox1.Controls.Add(this.label53);
            this.groupBox1.Controls.Add(this.label66);
            this.groupBox1.Controls.Add(this.label67);
            this.groupBox1.Controls.Add(this.label68);
            this.groupBox1.Controls.Add(this.label69);
            this.groupBox1.Controls.Add(this.ledReal1);
            this.groupBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox1.Location = new System.Drawing.Point(1, 119);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(996, 162);
            this.groupBox1.TabIndex = 24;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "数据";
            // 
            // ledReal3
            // 
            this.ledReal3.BackColor = System.Drawing.Color.Transparent;
            this.ledReal3.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledReal3.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledReal3.BevelRate = 0.1F;
            this.ledReal3.BorderColor = System.Drawing.Color.Lavender;
            this.ledReal3.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledReal3.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledReal3.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledReal3.HighlightOpaque = ((byte)(20));
            this.ledReal3.Location = new System.Drawing.Point(702, 50);
            this.ledReal3.Name = "ledReal3";
            this.ledReal3.RoundCorner = true;
            this.ledReal3.SegmentIntervalRatio = 50;
            this.ledReal3.ShowHighlight = true;
            this.ledReal3.Size = new System.Drawing.Size(250, 35);
            this.ledReal3.TabIndex = 40;
            this.ledReal3.Text = "0";
            this.ledReal3.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledReal3.TotalCharCount = 14;
            // 
            // comboBox6
            // 
            this.comboBox6.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox6.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox6.FormattingEnabled = true;
            this.comboBox6.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "不选"});
            this.comboBox6.Location = new System.Drawing.Point(-165, 111);
            this.comboBox6.Name = "comboBox6";
            this.comboBox6.Size = new System.Drawing.Size(55, 20);
            this.comboBox6.TabIndex = 39;
            // 
            // ledReal5
            // 
            this.ledReal5.BackColor = System.Drawing.Color.Transparent;
            this.ledReal5.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledReal5.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledReal5.BevelRate = 0.1F;
            this.ledReal5.BorderColor = System.Drawing.Color.Lavender;
            this.ledReal5.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledReal5.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledReal5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledReal5.HighlightOpaque = ((byte)(20));
            this.ledReal5.Location = new System.Drawing.Point(702, 118);
            this.ledReal5.Name = "ledReal5";
            this.ledReal5.RoundCorner = true;
            this.ledReal5.SegmentIntervalRatio = 50;
            this.ledReal5.ShowHighlight = true;
            this.ledReal5.Size = new System.Drawing.Size(250, 35);
            this.ledReal5.TabIndex = 35;
            this.ledReal5.Text = "0";
            this.ledReal5.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledReal5.TotalCharCount = 14;
            // 
            // ledReal2
            // 
            this.ledReal2.BackColor = System.Drawing.Color.Transparent;
            this.ledReal2.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledReal2.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledReal2.BevelRate = 0.1F;
            this.ledReal2.BorderColor = System.Drawing.Color.Lavender;
            this.ledReal2.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledReal2.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledReal2.ForeColor = System.Drawing.Color.Purple;
            this.ledReal2.HighlightOpaque = ((byte)(20));
            this.ledReal2.Location = new System.Drawing.Point(496, 35);
            this.ledReal2.Name = "ledReal2";
            this.ledReal2.RoundCorner = true;
            this.ledReal2.SegmentIntervalRatio = 50;
            this.ledReal2.ShowHighlight = true;
            this.ledReal2.Size = new System.Drawing.Size(162, 50);
            this.ledReal2.TabIndex = 34;
            this.ledReal2.Text = "0";
            this.ledReal2.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledReal2.TotalCharCount = 6;
            // 
            // ledReal4
            // 
            this.ledReal4.BackColor = System.Drawing.Color.Transparent;
            this.ledReal4.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledReal4.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledReal4.BevelRate = 0.1F;
            this.ledReal4.BorderColor = System.Drawing.Color.Lavender;
            this.ledReal4.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledReal4.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledReal4.ForeColor = System.Drawing.Color.Purple;
            this.ledReal4.HighlightOpaque = ((byte)(20));
            this.ledReal4.Location = new System.Drawing.Point(497, 103);
            this.ledReal4.Name = "ledReal4";
            this.ledReal4.RoundCorner = true;
            this.ledReal4.SegmentIntervalRatio = 50;
            this.ledReal4.ShowHighlight = true;
            this.ledReal4.Size = new System.Drawing.Size(161, 50);
            this.ledReal4.TabIndex = 33;
            this.ledReal4.Text = "0";
            this.ledReal4.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledReal4.TotalCharCount = 6;
            // 
            // label53
            // 
            this.label53.AutoSize = true;
            this.label53.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label53.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label53.Location = new System.Drawing.Point(700, 103);
            this.label53.Name = "label53";
            this.label53.Size = new System.Drawing.Size(131, 12);
            this.label53.TabIndex = 30;
            this.label53.Text = "累计运行的时间(毫秒):";
            // 
            // label66
            // 
            this.label66.AutoSize = true;
            this.label66.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label66.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label66.Location = new System.Drawing.Point(495, 17);
            this.label66.Name = "label66";
            this.label66.Size = new System.Drawing.Size(125, 12);
            this.label66.TabIndex = 29;
            this.label66.Text = "命令识别速度(个/秒):";
            // 
            // label67
            // 
            this.label67.AutoSize = true;
            this.label67.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label67.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label67.Location = new System.Drawing.Point(498, 88);
            this.label67.Name = "label67";
            this.label67.Size = new System.Drawing.Size(119, 12);
            this.label67.TabIndex = 28;
            this.label67.Text = "命令执行时间(毫秒):";
            // 
            // label68
            // 
            this.label68.AutoSize = true;
            this.label68.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label68.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label68.Location = new System.Drawing.Point(700, 35);
            this.label68.Name = "label68";
            this.label68.Size = new System.Drawing.Size(107, 12);
            this.label68.TabIndex = 27;
            this.label68.Text = "累计返回数据(条):";
            // 
            // label69
            // 
            this.label69.AutoSize = true;
            this.label69.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label69.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label69.Location = new System.Drawing.Point(104, 17);
            this.label69.Name = "label69";
            this.label69.Size = new System.Drawing.Size(143, 12);
            this.label69.TabIndex = 26;
            this.label69.Text = "已盘存的标签总数量(个):";
            // 
            // ledReal1
            // 
            this.ledReal1.BackColor = System.Drawing.Color.Transparent;
            this.ledReal1.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledReal1.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledReal1.BevelRate = 0.1F;
            this.ledReal1.BorderColor = System.Drawing.Color.Lavender;
            this.ledReal1.BorderWidth = 3;
            this.ledReal1.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledReal1.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledReal1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledReal1.HighlightOpaque = ((byte)(20));
            this.ledReal1.Location = new System.Drawing.Point(106, 35);
            this.ledReal1.Name = "ledReal1";
            this.ledReal1.RoundCorner = true;
            this.ledReal1.SegmentIntervalRatio = 50;
            this.ledReal1.ShowHighlight = true;
            this.ledReal1.Size = new System.Drawing.Size(310, 118);
            this.ledReal1.TabIndex = 21;
            this.ledReal1.Text = "0";
            this.ledReal1.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lvRealList
            // 
            this.lvRealList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader37,
            this.columnHeader38,
            this.columnHeader39,
            this.columnHeader40,
            this.columnHeader41,
            this.columnHeader42});
            this.lvRealList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvRealList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvRealList.GridLines = true;
            this.lvRealList.HideSelection = false;
            this.lvRealList.Location = new System.Drawing.Point(3, 313);
            this.lvRealList.Name = "lvRealList";
            this.lvRealList.Size = new System.Drawing.Size(994, 206);
            this.lvRealList.TabIndex = 23;
            this.lvRealList.UseCompatibleStateImageBehavior = false;
            this.lvRealList.View = System.Windows.Forms.View.Details;
            this.lvRealList.SelectedIndexChanged += new System.EventHandler(this.lvRealList_SelectedIndexChanged);
            // 
            // columnHeader37
            // 
            this.columnHeader37.Text = "ID";
            this.columnHeader37.Width = 59;
            // 
            // columnHeader38
            // 
            this.columnHeader38.Text = "EPC";
            this.columnHeader38.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader38.Width = 446;
            // 
            // columnHeader39
            // 
            this.columnHeader39.Text = "PC";
            this.columnHeader39.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader39.Width = 71;
            // 
            // columnHeader40
            // 
            this.columnHeader40.Text = "识别次数(ANT1/2/3/4/5/6/7/8)";
            this.columnHeader40.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader40.Width = 235;
            // 
            // columnHeader41
            // 
            this.columnHeader41.Text = "RSSI";
            this.columnHeader41.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader41.Width = 97;
            // 
            // columnHeader42
            // 
            this.columnHeader42.Text = "载波频率";
            this.columnHeader42.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader42.Width = 97;
            // 
            // pageBufferedMode
            // 
            this.pageBufferedMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pageBufferedMode.Controls.Add(this.excel_format_buffer_rb);
            this.pageBufferedMode.Controls.Add(this.txt_format_buffer_rb);
            this.pageBufferedMode.Controls.Add(this.button6);
            this.pageBufferedMode.Controls.Add(this.tableLayoutPanel4);
            this.pageBufferedMode.Controls.Add(this.groupBox3);
            this.pageBufferedMode.Controls.Add(this.btBufferFresh);
            this.pageBufferedMode.Controls.Add(this.labelBufferTagCount);
            this.pageBufferedMode.Controls.Add(this.lvBufferList);
            this.pageBufferedMode.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pageBufferedMode.Location = new System.Drawing.Point(4, 22);
            this.pageBufferedMode.Name = "pageBufferedMode";
            this.pageBufferedMode.Size = new System.Drawing.Size(1000, 522);
            this.pageBufferedMode.TabIndex = 2;
            this.pageBufferedMode.Text = "盘存标签(缓存模式)";
            // 
            // excel_format_buffer_rb
            // 
            this.excel_format_buffer_rb.AutoSize = true;
            this.excel_format_buffer_rb.Location = new System.Drawing.Point(832, 259);
            this.excel_format_buffer_rb.Name = "excel_format_buffer_rb";
            this.excel_format_buffer_rb.Size = new System.Drawing.Size(53, 16);
            this.excel_format_buffer_rb.TabIndex = 64;
            this.excel_format_buffer_rb.Text = "EXCEL";
            this.excel_format_buffer_rb.UseVisualStyleBackColor = true;
            // 
            // txt_format_buffer_rb
            // 
            this.txt_format_buffer_rb.AutoSize = true;
            this.txt_format_buffer_rb.Checked = true;
            this.txt_format_buffer_rb.Location = new System.Drawing.Point(766, 259);
            this.txt_format_buffer_rb.Name = "txt_format_buffer_rb";
            this.txt_format_buffer_rb.Size = new System.Drawing.Size(41, 16);
            this.txt_format_buffer_rb.TabIndex = 63;
            this.txt_format_buffer_rb.TabStop = true;
            this.txt_format_buffer_rb.Text = "TXT";
            this.txt_format_buffer_rb.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button6.Location = new System.Drawing.Point(907, 255);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(89, 23);
            this.button6.TabIndex = 62;
            this.button6.Text = "保存标签";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click_1);
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 3;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.22422F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.77578F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 432F));
            this.tableLayoutPanel4.Controls.Add(this.panel9, 2, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel10, 0, 0);
            this.tableLayoutPanel4.Controls.Add(this.panel8, 1, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 1;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(1000, 82);
            this.tableLayoutPanel4.TabIndex = 58;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.btClearBuffer);
            this.panel9.Controls.Add(this.btQueryBuffer);
            this.panel9.Controls.Add(this.btGetClearBuffer);
            this.panel9.Controls.Add(this.btGetBuffer);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(569, 4);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(427, 74);
            this.panel9.TabIndex = 1;
            // 
            // btClearBuffer
            // 
            this.btClearBuffer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btClearBuffer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btClearBuffer.Location = new System.Drawing.Point(15, 41);
            this.btClearBuffer.Name = "btClearBuffer";
            this.btClearBuffer.Size = new System.Drawing.Size(135, 25);
            this.btClearBuffer.TabIndex = 8;
            this.btClearBuffer.Text = "清空缓存";
            this.btClearBuffer.UseVisualStyleBackColor = true;
            this.btClearBuffer.Click += new System.EventHandler(this.btClearBuffer_Click);
            // 
            // btQueryBuffer
            // 
            this.btQueryBuffer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btQueryBuffer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btQueryBuffer.Location = new System.Drawing.Point(167, 41);
            this.btQueryBuffer.Name = "btQueryBuffer";
            this.btQueryBuffer.Size = new System.Drawing.Size(135, 25);
            this.btQueryBuffer.TabIndex = 7;
            this.btQueryBuffer.Text = "查询缓存中标签数量";
            this.btQueryBuffer.UseVisualStyleBackColor = true;
            this.btQueryBuffer.Click += new System.EventHandler(this.btQueryBuffer_Click);
            // 
            // btGetClearBuffer
            // 
            this.btGetClearBuffer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btGetClearBuffer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btGetClearBuffer.Location = new System.Drawing.Point(167, 10);
            this.btGetClearBuffer.Name = "btGetClearBuffer";
            this.btGetClearBuffer.Size = new System.Drawing.Size(135, 25);
            this.btGetClearBuffer.TabIndex = 6;
            this.btGetClearBuffer.Text = "读取并清空缓存";
            this.btGetClearBuffer.UseVisualStyleBackColor = true;
            this.btGetClearBuffer.Click += new System.EventHandler(this.btGetClearBuffer_Click);
            // 
            // btGetBuffer
            // 
            this.btGetBuffer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btGetBuffer.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btGetBuffer.Location = new System.Drawing.Point(15, 10);
            this.btGetBuffer.Name = "btGetBuffer";
            this.btGetBuffer.Size = new System.Drawing.Size(135, 25);
            this.btGetBuffer.TabIndex = 5;
            this.btGetBuffer.Text = "读取缓存";
            this.btGetBuffer.UseVisualStyleBackColor = true;
            this.btGetBuffer.Click += new System.EventHandler(this.btGetBuffer_Click);
            // 
            // panel10
            // 
            this.panel10.Controls.Add(this.btBufferInventory);
            this.panel10.Controls.Add(this.label85);
            this.panel10.Controls.Add(this.textReadRoundBuffer);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(4, 4);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(277, 74);
            this.panel10.TabIndex = 0;
            // 
            // btBufferInventory
            // 
            this.btBufferInventory.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btBufferInventory.ForeColor = System.Drawing.Color.DarkBlue;
            this.btBufferInventory.Location = new System.Drawing.Point(7, 14);
            this.btBufferInventory.Name = "btBufferInventory";
            this.btBufferInventory.Size = new System.Drawing.Size(144, 38);
            this.btBufferInventory.TabIndex = 51;
            this.btBufferInventory.Text = "开始盘存";
            this.btBufferInventory.UseVisualStyleBackColor = true;
            this.btBufferInventory.Click += new System.EventHandler(this.btBufferInventory_Click);
            // 
            // label85
            // 
            this.label85.AutoSize = true;
            this.label85.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label85.Location = new System.Drawing.Point(154, 29);
            this.label85.Name = "label85";
            this.label85.Size = new System.Drawing.Size(119, 12);
            this.label85.TabIndex = 49;
            this.label85.Text = "每条命令的盘存次数:";
            // 
            // textReadRoundBuffer
            // 
            this.textReadRoundBuffer.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textReadRoundBuffer.Location = new System.Drawing.Point(273, 26);
            this.textReadRoundBuffer.Name = "textReadRoundBuffer";
            this.textReadRoundBuffer.Size = new System.Drawing.Size(28, 21);
            this.textReadRoundBuffer.TabIndex = 50;
            this.textReadRoundBuffer.Text = "1";
            this.textReadRoundBuffer.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // panel8
            // 
            this.panel8.Controls.Add(this.checkBox4);
            this.panel8.Controls.Add(this.checkBox3);
            this.panel8.Controls.Add(this.checkBox2);
            this.panel8.Controls.Add(this.checkBox1);
            this.panel8.Controls.Add(this.cbBufferWorkant1);
            this.panel8.Controls.Add(this.cbBufferWorkant4);
            this.panel8.Controls.Add(this.cbBufferWorkant2);
            this.panel8.Controls.Add(this.cbBufferWorkant3);
            this.panel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel8.Location = new System.Drawing.Point(288, 4);
            this.panel8.Name = "panel8";
            this.panel8.Size = new System.Drawing.Size(274, 74);
            this.panel8.TabIndex = 0;
            // 
            // checkBox4
            // 
            this.checkBox4.AutoSize = true;
            this.checkBox4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox4.Location = new System.Drawing.Point(237, 37);
            this.checkBox4.Name = "checkBox4";
            this.checkBox4.Size = new System.Drawing.Size(54, 16);
            this.checkBox4.TabIndex = 15;
            this.checkBox4.Text = "天线8";
            this.checkBox4.UseVisualStyleBackColor = true;
            // 
            // checkBox3
            // 
            this.checkBox3.AutoSize = true;
            this.checkBox3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox3.Location = new System.Drawing.Point(171, 37);
            this.checkBox3.Name = "checkBox3";
            this.checkBox3.Size = new System.Drawing.Size(54, 16);
            this.checkBox3.TabIndex = 14;
            this.checkBox3.Text = "天线7";
            this.checkBox3.UseVisualStyleBackColor = true;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox2.Location = new System.Drawing.Point(104, 36);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(54, 16);
            this.checkBox2.TabIndex = 13;
            this.checkBox2.Text = "天线6";
            this.checkBox2.UseVisualStyleBackColor = true;
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox1.Location = new System.Drawing.Point(33, 37);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(54, 16);
            this.checkBox1.TabIndex = 12;
            this.checkBox1.Text = "天线5";
            this.checkBox1.UseVisualStyleBackColor = true;
            // 
            // cbBufferWorkant1
            // 
            this.cbBufferWorkant1.AutoSize = true;
            this.cbBufferWorkant1.Checked = true;
            this.cbBufferWorkant1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbBufferWorkant1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBufferWorkant1.Location = new System.Drawing.Point(33, 10);
            this.cbBufferWorkant1.Name = "cbBufferWorkant1";
            this.cbBufferWorkant1.Size = new System.Drawing.Size(54, 16);
            this.cbBufferWorkant1.TabIndex = 11;
            this.cbBufferWorkant1.Text = "天线1";
            this.cbBufferWorkant1.UseVisualStyleBackColor = true;
            // 
            // cbBufferWorkant4
            // 
            this.cbBufferWorkant4.AutoSize = true;
            this.cbBufferWorkant4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBufferWorkant4.Location = new System.Drawing.Point(237, 10);
            this.cbBufferWorkant4.Name = "cbBufferWorkant4";
            this.cbBufferWorkant4.Size = new System.Drawing.Size(54, 16);
            this.cbBufferWorkant4.TabIndex = 10;
            this.cbBufferWorkant4.Text = "天线4";
            this.cbBufferWorkant4.UseVisualStyleBackColor = true;
            // 
            // cbBufferWorkant2
            // 
            this.cbBufferWorkant2.AutoSize = true;
            this.cbBufferWorkant2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBufferWorkant2.Location = new System.Drawing.Point(104, 10);
            this.cbBufferWorkant2.Name = "cbBufferWorkant2";
            this.cbBufferWorkant2.Size = new System.Drawing.Size(54, 16);
            this.cbBufferWorkant2.TabIndex = 8;
            this.cbBufferWorkant2.Text = "天线2";
            this.cbBufferWorkant2.UseVisualStyleBackColor = true;
            // 
            // cbBufferWorkant3
            // 
            this.cbBufferWorkant3.AutoSize = true;
            this.cbBufferWorkant3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cbBufferWorkant3.Location = new System.Drawing.Point(171, 10);
            this.cbBufferWorkant3.Name = "cbBufferWorkant3";
            this.cbBufferWorkant3.Size = new System.Drawing.Size(54, 16);
            this.cbBufferWorkant3.TabIndex = 9;
            this.cbBufferWorkant3.Text = "天线3";
            this.cbBufferWorkant3.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ledBuffer4);
            this.groupBox3.Controls.Add(this.comboBox11);
            this.groupBox3.Controls.Add(this.ledBuffer5);
            this.groupBox3.Controls.Add(this.ledBuffer2);
            this.groupBox3.Controls.Add(this.ledBuffer3);
            this.groupBox3.Controls.Add(this.label92);
            this.groupBox3.Controls.Add(this.label93);
            this.groupBox3.Controls.Add(this.label94);
            this.groupBox3.Controls.Add(this.label95);
            this.groupBox3.Controls.Add(this.label96);
            this.groupBox3.Controls.Add(this.ledBuffer1);
            this.groupBox3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox3.Location = new System.Drawing.Point(0, 88);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(996, 162);
            this.groupBox3.TabIndex = 57;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据";
            // 
            // ledBuffer4
            // 
            this.ledBuffer4.BackColor = System.Drawing.Color.Transparent;
            this.ledBuffer4.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledBuffer4.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledBuffer4.BevelRate = 0.1F;
            this.ledBuffer4.BorderColor = System.Drawing.Color.Lavender;
            this.ledBuffer4.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledBuffer4.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledBuffer4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledBuffer4.HighlightOpaque = ((byte)(20));
            this.ledBuffer4.Location = new System.Drawing.Point(702, 50);
            this.ledBuffer4.Name = "ledBuffer4";
            this.ledBuffer4.RoundCorner = true;
            this.ledBuffer4.SegmentIntervalRatio = 50;
            this.ledBuffer4.ShowHighlight = true;
            this.ledBuffer4.Size = new System.Drawing.Size(250, 35);
            this.ledBuffer4.TabIndex = 40;
            this.ledBuffer4.Text = "0";
            this.ledBuffer4.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledBuffer4.TotalCharCount = 14;
            // 
            // comboBox11
            // 
            this.comboBox11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox11.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox11.FormattingEnabled = true;
            this.comboBox11.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "不选"});
            this.comboBox11.Location = new System.Drawing.Point(-165, 111);
            this.comboBox11.Name = "comboBox11";
            this.comboBox11.Size = new System.Drawing.Size(55, 20);
            this.comboBox11.TabIndex = 39;
            // 
            // ledBuffer5
            // 
            this.ledBuffer5.BackColor = System.Drawing.Color.Transparent;
            this.ledBuffer5.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledBuffer5.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledBuffer5.BevelRate = 0.1F;
            this.ledBuffer5.BorderColor = System.Drawing.Color.Lavender;
            this.ledBuffer5.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledBuffer5.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledBuffer5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledBuffer5.HighlightOpaque = ((byte)(20));
            this.ledBuffer5.Location = new System.Drawing.Point(702, 118);
            this.ledBuffer5.Name = "ledBuffer5";
            this.ledBuffer5.RoundCorner = true;
            this.ledBuffer5.SegmentIntervalRatio = 50;
            this.ledBuffer5.ShowHighlight = true;
            this.ledBuffer5.Size = new System.Drawing.Size(250, 35);
            this.ledBuffer5.TabIndex = 35;
            this.ledBuffer5.Text = "0";
            this.ledBuffer5.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledBuffer5.TotalCharCount = 14;
            // 
            // ledBuffer2
            // 
            this.ledBuffer2.BackColor = System.Drawing.Color.Transparent;
            this.ledBuffer2.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledBuffer2.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledBuffer2.BevelRate = 0.1F;
            this.ledBuffer2.BorderColor = System.Drawing.Color.Lavender;
            this.ledBuffer2.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledBuffer2.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledBuffer2.ForeColor = System.Drawing.Color.Purple;
            this.ledBuffer2.HighlightOpaque = ((byte)(20));
            this.ledBuffer2.Location = new System.Drawing.Point(496, 35);
            this.ledBuffer2.Name = "ledBuffer2";
            this.ledBuffer2.RoundCorner = true;
            this.ledBuffer2.SegmentIntervalRatio = 50;
            this.ledBuffer2.ShowHighlight = true;
            this.ledBuffer2.Size = new System.Drawing.Size(162, 50);
            this.ledBuffer2.TabIndex = 34;
            this.ledBuffer2.Text = "0";
            this.ledBuffer2.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledBuffer2.TotalCharCount = 6;
            // 
            // ledBuffer3
            // 
            this.ledBuffer3.BackColor = System.Drawing.Color.Transparent;
            this.ledBuffer3.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledBuffer3.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledBuffer3.BevelRate = 0.1F;
            this.ledBuffer3.BorderColor = System.Drawing.Color.Lavender;
            this.ledBuffer3.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledBuffer3.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledBuffer3.ForeColor = System.Drawing.Color.Purple;
            this.ledBuffer3.HighlightOpaque = ((byte)(20));
            this.ledBuffer3.Location = new System.Drawing.Point(497, 103);
            this.ledBuffer3.Name = "ledBuffer3";
            this.ledBuffer3.RoundCorner = true;
            this.ledBuffer3.SegmentIntervalRatio = 50;
            this.ledBuffer3.ShowHighlight = true;
            this.ledBuffer3.Size = new System.Drawing.Size(161, 50);
            this.ledBuffer3.TabIndex = 33;
            this.ledBuffer3.Text = "0";
            this.ledBuffer3.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledBuffer3.TotalCharCount = 6;
            // 
            // label92
            // 
            this.label92.AutoSize = true;
            this.label92.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label92.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label92.Location = new System.Drawing.Point(700, 103);
            this.label92.Name = "label92";
            this.label92.Size = new System.Drawing.Size(131, 12);
            this.label92.TabIndex = 30;
            this.label92.Text = "累计运行的时间(毫秒):";
            // 
            // label93
            // 
            this.label93.AutoSize = true;
            this.label93.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label93.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label93.Location = new System.Drawing.Point(495, 17);
            this.label93.Name = "label93";
            this.label93.Size = new System.Drawing.Size(125, 12);
            this.label93.TabIndex = 29;
            this.label93.Text = "命令识别速度(个/秒):";
            // 
            // label94
            // 
            this.label94.AutoSize = true;
            this.label94.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label94.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label94.Location = new System.Drawing.Point(498, 88);
            this.label94.Name = "label94";
            this.label94.Size = new System.Drawing.Size(119, 12);
            this.label94.TabIndex = 28;
            this.label94.Text = "命令执行时间(毫秒):";
            // 
            // label95
            // 
            this.label95.AutoSize = true;
            this.label95.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label95.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label95.Location = new System.Drawing.Point(700, 35);
            this.label95.Name = "label95";
            this.label95.Size = new System.Drawing.Size(107, 12);
            this.label95.TabIndex = 27;
            this.label95.Text = "累计读标签的次数:";
            // 
            // label96
            // 
            this.label96.AutoSize = true;
            this.label96.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label96.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label96.Location = new System.Drawing.Point(104, 17);
            this.label96.Name = "label96";
            this.label96.Size = new System.Drawing.Size(143, 12);
            this.label96.TabIndex = 26;
            this.label96.Text = "已盘存的标签总数量(个):";
            // 
            // ledBuffer1
            // 
            this.ledBuffer1.BackColor = System.Drawing.Color.Transparent;
            this.ledBuffer1.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledBuffer1.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledBuffer1.BevelRate = 0.1F;
            this.ledBuffer1.BorderColor = System.Drawing.Color.Lavender;
            this.ledBuffer1.BorderWidth = 3;
            this.ledBuffer1.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledBuffer1.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledBuffer1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledBuffer1.HighlightOpaque = ((byte)(20));
            this.ledBuffer1.Location = new System.Drawing.Point(106, 35);
            this.ledBuffer1.Name = "ledBuffer1";
            this.ledBuffer1.RoundCorner = true;
            this.ledBuffer1.SegmentIntervalRatio = 50;
            this.ledBuffer1.ShowHighlight = true;
            this.ledBuffer1.Size = new System.Drawing.Size(310, 118);
            this.ledBuffer1.TabIndex = 21;
            this.ledBuffer1.Text = "0";
            this.ledBuffer1.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // btBufferFresh
            // 
            this.btBufferFresh.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btBufferFresh.ForeColor = System.Drawing.SystemColors.Desktop;
            this.btBufferFresh.Location = new System.Drawing.Point(623, 255);
            this.btBufferFresh.Name = "btBufferFresh";
            this.btBufferFresh.Size = new System.Drawing.Size(89, 23);
            this.btBufferFresh.TabIndex = 52;
            this.btBufferFresh.Text = "刷新界面";
            this.btBufferFresh.UseVisualStyleBackColor = true;
            this.btBufferFresh.Click += new System.EventHandler(this.btBufferFresh_Click);
            // 
            // labelBufferTagCount
            // 
            this.labelBufferTagCount.AutoSize = true;
            this.labelBufferTagCount.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelBufferTagCount.ForeColor = System.Drawing.SystemColors.Desktop;
            this.labelBufferTagCount.Location = new System.Drawing.Point(6, 261);
            this.labelBufferTagCount.Name = "labelBufferTagCount";
            this.labelBufferTagCount.Size = new System.Drawing.Size(65, 12);
            this.labelBufferTagCount.TabIndex = 49;
            this.labelBufferTagCount.Text = "标签列表: ";
            // 
            // lvBufferList
            // 
            this.lvBufferList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader49,
            this.columnHeader50,
            this.columnHeader51,
            this.columnHeader52,
            this.columnHeader53,
            this.columnHeader54,
            this.columnHeader16});
            this.lvBufferList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvBufferList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvBufferList.GridLines = true;
            this.lvBufferList.HideSelection = false;
            this.lvBufferList.Location = new System.Drawing.Point(0, 284);
            this.lvBufferList.Name = "lvBufferList";
            this.lvBufferList.Size = new System.Drawing.Size(1000, 238);
            this.lvBufferList.TabIndex = 48;
            this.lvBufferList.UseCompatibleStateImageBehavior = false;
            this.lvBufferList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader49
            // 
            this.columnHeader49.Text = "ID";
            this.columnHeader49.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader49.Width = 56;
            // 
            // columnHeader50
            // 
            this.columnHeader50.Text = "PC";
            this.columnHeader50.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader50.Width = 64;
            // 
            // columnHeader51
            // 
            this.columnHeader51.Text = "CRC";
            this.columnHeader51.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader51.Width = 74;
            // 
            // columnHeader52
            // 
            this.columnHeader52.Text = "EPC";
            this.columnHeader52.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader52.Width = 492;
            // 
            // columnHeader53
            // 
            this.columnHeader53.Text = "天线号";
            this.columnHeader53.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader53.Width = 95;
            // 
            // columnHeader54
            // 
            this.columnHeader54.Text = "RSSI";
            this.columnHeader54.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader54.Width = 90;
            // 
            // columnHeader16
            // 
            this.columnHeader16.Text = "读取次数";
            this.columnHeader16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader16.Width = 139;
            // 
            // pageFast4AntMode
            // 
            this.pageFast4AntMode.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pageFast4AntMode.Controls.Add(this.txt_format_fast_rb);
            this.pageFast4AntMode.Controls.Add(this.excel_format_fast_rb);
            this.pageFast4AntMode.Controls.Add(this.button7);
            this.pageFast4AntMode.Controls.Add(this.groupBox2);
            this.pageFast4AntMode.Controls.Add(this.txtFastMaxRssi);
            this.pageFast4AntMode.Controls.Add(this.txtFastMinRssi);
            this.pageFast4AntMode.Controls.Add(this.buttonFastFresh);
            this.pageFast4AntMode.Controls.Add(this.label22);
            this.pageFast4AntMode.Controls.Add(this.lvFastList);
            this.pageFast4AntMode.Controls.Add(this.label49);
            this.pageFast4AntMode.Controls.Add(this.txtFastTagList);
            this.pageFast4AntMode.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pageFast4AntMode.Location = new System.Drawing.Point(4, 22);
            this.pageFast4AntMode.Name = "pageFast4AntMode";
            this.pageFast4AntMode.Padding = new System.Windows.Forms.Padding(3);
            this.pageFast4AntMode.Size = new System.Drawing.Size(1000, 522);
            this.pageFast4AntMode.TabIndex = 0;
            this.pageFast4AntMode.Text = "快速天线盘存";
            this.pageFast4AntMode.Enter += new System.EventHandler(this.pageFast4AntMode_Enter);
            // 
            // txt_format_fast_rb
            // 
            this.txt_format_fast_rb.AutoSize = true;
            this.txt_format_fast_rb.Checked = true;
            this.txt_format_fast_rb.Location = new System.Drawing.Point(738, 331);
            this.txt_format_fast_rb.Name = "txt_format_fast_rb";
            this.txt_format_fast_rb.Size = new System.Drawing.Size(41, 16);
            this.txt_format_fast_rb.TabIndex = 65;
            this.txt_format_fast_rb.TabStop = true;
            this.txt_format_fast_rb.Text = "TXT";
            this.txt_format_fast_rb.UseVisualStyleBackColor = true;
            // 
            // excel_format_fast_rb
            // 
            this.excel_format_fast_rb.AutoSize = true;
            this.excel_format_fast_rb.Location = new System.Drawing.Point(810, 331);
            this.excel_format_fast_rb.Name = "excel_format_fast_rb";
            this.excel_format_fast_rb.Size = new System.Drawing.Size(53, 16);
            this.excel_format_fast_rb.TabIndex = 64;
            this.excel_format_fast_rb.Text = "EXCEL";
            this.excel_format_fast_rb.UseVisualStyleBackColor = true;
            // 
            // button7
            // 
            this.button7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button7.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button7.Location = new System.Drawing.Point(885, 327);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(89, 23);
            this.button7.TabIndex = 62;
            this.button7.Text = "保存标签";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click_1);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox25);
            this.groupBox2.Controls.Add(this.groupBox28);
            this.groupBox2.Controls.Add(this.groupBox27);
            this.groupBox2.Controls.Add(this.groupBox26);
            this.groupBox2.Controls.Add(this.splitContainer1);
            this.groupBox2.Controls.Add(this.comboBox7);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBox2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(994, 318);
            this.groupBox2.TabIndex = 42;
            this.groupBox2.TabStop = false;
            // 
            // groupBox25
            // 
            this.groupBox25.Controls.Add(this.ledFast1);
            this.groupBox25.Controls.Add(this.label58);
            this.groupBox25.Controls.Add(this.ledFast4);
            this.groupBox25.Controls.Add(this.ledFast2);
            this.groupBox25.Controls.Add(this.label55);
            this.groupBox25.Controls.Add(this.label56);
            this.groupBox25.Controls.Add(this.ledFast3);
            this.groupBox25.Controls.Add(this.label57);
            this.groupBox25.Controls.Add(this.label54);
            this.groupBox25.Controls.Add(this.ledFast5);
            this.groupBox25.Location = new System.Drawing.Point(3, 168);
            this.groupBox25.Name = "groupBox25";
            this.groupBox25.Size = new System.Drawing.Size(709, 147);
            this.groupBox25.TabIndex = 66;
            this.groupBox25.TabStop = false;
            this.groupBox25.Text = "数据";
            // 
            // ledFast1
            // 
            this.ledFast1.BackColor = System.Drawing.Color.Transparent;
            this.ledFast1.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledFast1.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledFast1.BevelRate = 0.1F;
            this.ledFast1.BorderColor = System.Drawing.Color.Lavender;
            this.ledFast1.BorderWidth = 3;
            this.ledFast1.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledFast1.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledFast1.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledFast1.HighlightOpaque = ((byte)(20));
            this.ledFast1.Location = new System.Drawing.Point(6, 34);
            this.ledFast1.Name = "ledFast1";
            this.ledFast1.RoundCorner = true;
            this.ledFast1.SegmentIntervalRatio = 50;
            this.ledFast1.ShowHighlight = true;
            this.ledFast1.Size = new System.Drawing.Size(281, 106);
            this.ledFast1.TabIndex = 21;
            this.ledFast1.Text = "0";
            this.ledFast1.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // label58
            // 
            this.label58.AutoSize = true;
            this.label58.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label58.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label58.Location = new System.Drawing.Point(6, 17);
            this.label58.Name = "label58";
            this.label58.Size = new System.Drawing.Size(143, 12);
            this.label58.TabIndex = 26;
            this.label58.Text = "已盘存的标签总数量(个):";
            // 
            // ledFast4
            // 
            this.ledFast4.BackColor = System.Drawing.Color.Transparent;
            this.ledFast4.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledFast4.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledFast4.BevelRate = 0.1F;
            this.ledFast4.BorderColor = System.Drawing.Color.Lavender;
            this.ledFast4.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledFast4.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledFast4.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledFast4.HighlightOpaque = ((byte)(20));
            this.ledFast4.Location = new System.Drawing.Point(451, 37);
            this.ledFast4.Name = "ledFast4";
            this.ledFast4.RoundCorner = true;
            this.ledFast4.SegmentIntervalRatio = 50;
            this.ledFast4.ShowHighlight = true;
            this.ledFast4.Size = new System.Drawing.Size(252, 35);
            this.ledFast4.TabIndex = 40;
            this.ledFast4.Text = "0";
            this.ledFast4.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledFast4.TotalCharCount = 14;
            // 
            // ledFast2
            // 
            this.ledFast2.BackColor = System.Drawing.Color.Transparent;
            this.ledFast2.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledFast2.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledFast2.BevelRate = 0.1F;
            this.ledFast2.BorderColor = System.Drawing.Color.Lavender;
            this.ledFast2.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledFast2.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledFast2.ForeColor = System.Drawing.Color.Purple;
            this.ledFast2.HighlightOpaque = ((byte)(20));
            this.ledFast2.Location = new System.Drawing.Point(302, 37);
            this.ledFast2.Name = "ledFast2";
            this.ledFast2.RoundCorner = true;
            this.ledFast2.SegmentIntervalRatio = 50;
            this.ledFast2.ShowHighlight = true;
            this.ledFast2.Size = new System.Drawing.Size(124, 37);
            this.ledFast2.TabIndex = 34;
            this.ledFast2.Text = "0";
            this.ledFast2.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledFast2.TotalCharCount = 6;
            // 
            // label55
            // 
            this.label55.AutoSize = true;
            this.label55.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label55.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label55.Location = new System.Drawing.Point(300, 19);
            this.label55.Name = "label55";
            this.label55.Size = new System.Drawing.Size(125, 12);
            this.label55.TabIndex = 29;
            this.label55.Text = "命令识别速度(个/秒):";
            // 
            // label56
            // 
            this.label56.AutoSize = true;
            this.label56.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label56.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label56.Location = new System.Drawing.Point(300, 83);
            this.label56.Name = "label56";
            this.label56.Size = new System.Drawing.Size(119, 12);
            this.label56.TabIndex = 28;
            this.label56.Text = "命令执行时间(毫秒):";
            // 
            // ledFast3
            // 
            this.ledFast3.BackColor = System.Drawing.Color.Transparent;
            this.ledFast3.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledFast3.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledFast3.BevelRate = 0.1F;
            this.ledFast3.BorderColor = System.Drawing.Color.Lavender;
            this.ledFast3.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledFast3.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledFast3.ForeColor = System.Drawing.Color.Purple;
            this.ledFast3.HighlightOpaque = ((byte)(20));
            this.ledFast3.Location = new System.Drawing.Point(302, 101);
            this.ledFast3.Name = "ledFast3";
            this.ledFast3.RoundCorner = true;
            this.ledFast3.SegmentIntervalRatio = 50;
            this.ledFast3.ShowHighlight = true;
            this.ledFast3.Size = new System.Drawing.Size(121, 34);
            this.ledFast3.TabIndex = 33;
            this.ledFast3.Text = "0";
            this.ledFast3.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledFast3.TotalCharCount = 6;
            // 
            // label57
            // 
            this.label57.AutoSize = true;
            this.label57.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label57.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label57.Location = new System.Drawing.Point(449, 19);
            this.label57.Name = "label57";
            this.label57.Size = new System.Drawing.Size(107, 12);
            this.label57.TabIndex = 27;
            this.label57.Text = "累计返回数据(条):";
            // 
            // label54
            // 
            this.label54.AutoSize = true;
            this.label54.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label54.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label54.Location = new System.Drawing.Point(449, 83);
            this.label54.Name = "label54";
            this.label54.Size = new System.Drawing.Size(131, 12);
            this.label54.TabIndex = 30;
            this.label54.Text = "累计运行的时间(毫秒):";
            // 
            // ledFast5
            // 
            this.ledFast5.BackColor = System.Drawing.Color.Transparent;
            this.ledFast5.BackColor_1 = System.Drawing.Color.Transparent;
            this.ledFast5.BackColor_2 = System.Drawing.Color.DarkRed;
            this.ledFast5.BevelRate = 0.1F;
            this.ledFast5.BorderColor = System.Drawing.Color.Lavender;
            this.ledFast5.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.ledFast5.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.ledFast5.ForeColor = System.Drawing.Color.MidnightBlue;
            this.ledFast5.HighlightOpaque = ((byte)(20));
            this.ledFast5.Location = new System.Drawing.Point(451, 101);
            this.ledFast5.Name = "ledFast5";
            this.ledFast5.RoundCorner = true;
            this.ledFast5.SegmentIntervalRatio = 50;
            this.ledFast5.ShowHighlight = true;
            this.ledFast5.Size = new System.Drawing.Size(252, 35);
            this.ledFast5.TabIndex = 35;
            this.ledFast5.Text = "0";
            this.ledFast5.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.ledFast5.TotalCharCount = 14;
            // 
            // groupBox28
            // 
            this.groupBox28.Controls.Add(this.m_new_fast_inventory_power2);
            this.groupBox28.Controls.Add(this.m_new_fast_inventory_power1);
            this.groupBox28.Controls.Add(this.m_new_fast_inventory_repeat2);
            this.groupBox28.Controls.Add(this.m_new_fast_inventory_repeat1);
            this.groupBox28.Controls.Add(this.mRepeatPower1);
            this.groupBox28.Controls.Add(this.mRepeatPower2);
            this.groupBox28.Controls.Add(this.mRepeat2);
            this.groupBox28.Controls.Add(this.mRepeat1);
            this.groupBox28.Controls.Add(this.mDynamicPoll);
            this.groupBox28.Controls.Add(this.label132);
            this.groupBox28.Controls.Add(this.mFastExeCount);
            this.groupBox28.Controls.Add(this.label131);
            this.groupBox28.Controls.Add(this.mFastIntervalTime);
            this.groupBox28.Location = new System.Drawing.Point(715, 147);
            this.groupBox28.Name = "groupBox28";
            this.groupBox28.Size = new System.Drawing.Size(273, 140);
            this.groupBox28.TabIndex = 76;
            this.groupBox28.TabStop = false;
            // 
            // m_new_fast_inventory_power2
            // 
            this.m_new_fast_inventory_power2.Enabled = false;
            this.m_new_fast_inventory_power2.Location = new System.Drawing.Point(196, 93);
            this.m_new_fast_inventory_power2.Name = "m_new_fast_inventory_power2";
            this.m_new_fast_inventory_power2.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_power2.TabIndex = 81;
            this.m_new_fast_inventory_power2.Text = "28";
            this.m_new_fast_inventory_power2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_new_fast_inventory_power1
            // 
            this.m_new_fast_inventory_power1.Enabled = false;
            this.m_new_fast_inventory_power1.Location = new System.Drawing.Point(71, 94);
            this.m_new_fast_inventory_power1.Name = "m_new_fast_inventory_power1";
            this.m_new_fast_inventory_power1.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_power1.TabIndex = 80;
            this.m_new_fast_inventory_power1.Text = "26";
            this.m_new_fast_inventory_power1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_new_fast_inventory_repeat2
            // 
            this.m_new_fast_inventory_repeat2.Enabled = false;
            this.m_new_fast_inventory_repeat2.Location = new System.Drawing.Point(196, 61);
            this.m_new_fast_inventory_repeat2.Name = "m_new_fast_inventory_repeat2";
            this.m_new_fast_inventory_repeat2.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_repeat2.TabIndex = 79;
            this.m_new_fast_inventory_repeat2.Text = "1";
            this.m_new_fast_inventory_repeat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // m_new_fast_inventory_repeat1
            // 
            this.m_new_fast_inventory_repeat1.Enabled = false;
            this.m_new_fast_inventory_repeat1.Location = new System.Drawing.Point(71, 62);
            this.m_new_fast_inventory_repeat1.Name = "m_new_fast_inventory_repeat1";
            this.m_new_fast_inventory_repeat1.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_repeat1.TabIndex = 78;
            this.m_new_fast_inventory_repeat1.Text = "1";
            this.m_new_fast_inventory_repeat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mRepeatPower1
            // 
            this.mRepeatPower1.AutoSize = true;
            this.mRepeatPower1.Enabled = false;
            this.mRepeatPower1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mRepeatPower1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mRepeatPower1.Location = new System.Drawing.Point(18, 99);
            this.mRepeatPower1.Name = "mRepeatPower1";
            this.mRepeatPower1.Size = new System.Drawing.Size(41, 12);
            this.mRepeatPower1.TabIndex = 74;
            this.mRepeatPower1.Text = "Power1";
            // 
            // mRepeatPower2
            // 
            this.mRepeatPower2.AutoSize = true;
            this.mRepeatPower2.Enabled = false;
            this.mRepeatPower2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mRepeatPower2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mRepeatPower2.Location = new System.Drawing.Point(136, 98);
            this.mRepeatPower2.Name = "mRepeatPower2";
            this.mRepeatPower2.Size = new System.Drawing.Size(41, 12);
            this.mRepeatPower2.TabIndex = 73;
            this.mRepeatPower2.Text = "Power2";
            // 
            // mRepeat2
            // 
            this.mRepeat2.AutoSize = true;
            this.mRepeat2.Enabled = false;
            this.mRepeat2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mRepeat2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mRepeat2.Location = new System.Drawing.Point(132, 67);
            this.mRepeat2.Name = "mRepeat2";
            this.mRepeat2.Size = new System.Drawing.Size(47, 12);
            this.mRepeat2.TabIndex = 72;
            this.mRepeat2.Text = "Repeat2";
            // 
            // mRepeat1
            // 
            this.mRepeat1.AutoSize = true;
            this.mRepeat1.Enabled = false;
            this.mRepeat1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mRepeat1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mRepeat1.Location = new System.Drawing.Point(15, 67);
            this.mRepeat1.Name = "mRepeat1";
            this.mRepeat1.Size = new System.Drawing.Size(47, 12);
            this.mRepeat1.TabIndex = 71;
            this.mRepeat1.Text = "Repeat1";
            // 
            // mDynamicPoll
            // 
            this.mDynamicPoll.AutoSize = true;
            this.mDynamicPoll.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mDynamicPoll.Location = new System.Drawing.Point(10, 39);
            this.mDynamicPoll.Name = "mDynamicPoll";
            this.mDynamicPoll.Size = new System.Drawing.Size(72, 16);
            this.mDynamicPoll.TabIndex = 70;
            this.mDynamicPoll.Text = "动态轮询";
            this.mDynamicPoll.UseVisualStyleBackColor = true;
            this.mDynamicPoll.CheckedChanged += new System.EventHandler(this.mDynamicPoll_CheckedChanged);
            // 
            // label132
            // 
            this.label132.AutoSize = true;
            this.label132.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label132.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label132.Location = new System.Drawing.Point(6, 16);
            this.label132.Name = "label132";
            this.label132.Size = new System.Drawing.Size(53, 12);
            this.label132.TabIndex = 66;
            this.label132.Text = "运行次数";
            // 
            // mFastExeCount
            // 
            this.mFastExeCount.Location = new System.Drawing.Point(63, 12);
            this.mFastExeCount.Name = "mFastExeCount";
            this.mFastExeCount.Size = new System.Drawing.Size(51, 21);
            this.mFastExeCount.TabIndex = 68;
            this.mFastExeCount.Text = "1";
            this.mFastExeCount.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label131
            // 
            this.label131.AutoSize = true;
            this.label131.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label131.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label131.Location = new System.Drawing.Point(132, 16);
            this.label131.Name = "label131";
            this.label131.Size = new System.Drawing.Size(53, 12);
            this.label131.TabIndex = 65;
            this.label131.Text = "时间间隔";
            // 
            // mFastIntervalTime
            // 
            this.mFastIntervalTime.Location = new System.Drawing.Point(191, 13);
            this.mFastIntervalTime.Name = "mFastIntervalTime";
            this.mFastIntervalTime.Size = new System.Drawing.Size(51, 21);
            this.mFastIntervalTime.TabIndex = 67;
            this.mFastIntervalTime.Text = "0";
            this.mFastIntervalTime.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox27
            // 
            this.groupBox27.Controls.Add(this.m_new_fast_inventory_target_count);
            this.groupBox27.Controls.Add(this.mTargetQuantity);
            this.groupBox27.Controls.Add(this.m_new_fast_inventory_continue);
            this.groupBox27.Controls.Add(this.mContiue);
            this.groupBox27.Controls.Add(this.m_new_fast_inventory_optimized);
            this.groupBox27.Controls.Add(this.mOpitimized);
            this.groupBox27.Controls.Add(this.m_phase_value);
            this.groupBox27.Controls.Add(this.m_new_fast_inventory_flag);
            this.groupBox27.Controls.Add(this.m_new_fast_inventory_session);
            this.groupBox27.Controls.Add(this.m_new_fast_inventory);
            this.groupBox27.Controls.Add(this.label73);
            this.groupBox27.Controls.Add(this.txtInterval);
            this.groupBox27.Controls.Add(this.label72);
            this.groupBox27.Controls.Add(this.mReserve);
            this.groupBox27.Controls.Add(this.txtRepeat);
            this.groupBox27.Controls.Add(this.mPower1);
            this.groupBox27.Controls.Add(this.mPower2);
            this.groupBox27.Controls.Add(this.mPower5);
            this.groupBox27.Controls.Add(this.mPower4);
            this.groupBox27.Controls.Add(this.mPower3);
            this.groupBox27.Location = new System.Drawing.Point(715, 0);
            this.groupBox27.Name = "groupBox27";
            this.groupBox27.Size = new System.Drawing.Size(276, 148);
            this.groupBox27.TabIndex = 75;
            this.groupBox27.TabStop = false;
            // 
            // m_new_fast_inventory_target_count
            // 
            this.m_new_fast_inventory_target_count.Enabled = false;
            this.m_new_fast_inventory_target_count.Location = new System.Drawing.Point(134, 122);
            this.m_new_fast_inventory_target_count.Name = "m_new_fast_inventory_target_count";
            this.m_new_fast_inventory_target_count.Size = new System.Drawing.Size(59, 21);
            this.m_new_fast_inventory_target_count.TabIndex = 81;
            this.m_new_fast_inventory_target_count.Text = "0";
            this.m_new_fast_inventory_target_count.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mTargetQuantity
            // 
            this.mTargetQuantity.AutoSize = true;
            this.mTargetQuantity.Enabled = false;
            this.mTargetQuantity.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mTargetQuantity.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mTargetQuantity.Location = new System.Drawing.Point(9, 127);
            this.mTargetQuantity.Name = "mTargetQuantity";
            this.mTargetQuantity.Size = new System.Drawing.Size(95, 12);
            this.mTargetQuantity.TabIndex = 80;
            this.mTargetQuantity.Text = "Target Quantity";
            // 
            // m_new_fast_inventory_continue
            // 
            this.m_new_fast_inventory_continue.Enabled = false;
            this.m_new_fast_inventory_continue.Location = new System.Drawing.Point(167, 98);
            this.m_new_fast_inventory_continue.Name = "m_new_fast_inventory_continue";
            this.m_new_fast_inventory_continue.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_continue.TabIndex = 79;
            this.m_new_fast_inventory_continue.Text = "00";
            this.m_new_fast_inventory_continue.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mContiue
            // 
            this.mContiue.AutoSize = true;
            this.mContiue.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mContiue.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mContiue.Location = new System.Drawing.Point(116, 104);
            this.mContiue.Name = "mContiue";
            this.mContiue.Size = new System.Drawing.Size(29, 12);
            this.mContiue.TabIndex = 78;
            this.mContiue.Text = "连续";
            // 
            // m_new_fast_inventory_optimized
            // 
            this.m_new_fast_inventory_optimized.Enabled = false;
            this.m_new_fast_inventory_optimized.Location = new System.Drawing.Point(58, 101);
            this.m_new_fast_inventory_optimized.Name = "m_new_fast_inventory_optimized";
            this.m_new_fast_inventory_optimized.Size = new System.Drawing.Size(26, 21);
            this.m_new_fast_inventory_optimized.TabIndex = 77;
            this.m_new_fast_inventory_optimized.Text = "00";
            this.m_new_fast_inventory_optimized.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mOpitimized
            // 
            this.mOpitimized.AutoSize = true;
            this.mOpitimized.Enabled = false;
            this.mOpitimized.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mOpitimized.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mOpitimized.Location = new System.Drawing.Point(15, 104);
            this.mOpitimized.Name = "mOpitimized";
            this.mOpitimized.Size = new System.Drawing.Size(29, 12);
            this.mOpitimized.TabIndex = 76;
            this.mOpitimized.Text = "优化";
            // 
            // m_phase_value
            // 
            this.m_phase_value.AutoSize = true;
            this.m_phase_value.Enabled = false;
            this.m_phase_value.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_phase_value.Location = new System.Drawing.Point(210, 46);
            this.m_phase_value.Name = "m_phase_value";
            this.m_phase_value.Size = new System.Drawing.Size(54, 16);
            this.m_phase_value.TabIndex = 75;
            this.m_phase_value.Text = "Phase";
            this.m_phase_value.UseVisualStyleBackColor = true;
            this.m_phase_value.CheckedChanged += new System.EventHandler(this.m_phase_value_CheckedChanged);
            // 
            // m_new_fast_inventory_flag
            // 
            this.m_new_fast_inventory_flag.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_new_fast_inventory_flag.Enabled = false;
            this.m_new_fast_inventory_flag.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_new_fast_inventory_flag.ForeColor = System.Drawing.SystemColors.InfoText;
            this.m_new_fast_inventory_flag.FormattingEnabled = true;
            this.m_new_fast_inventory_flag.Items.AddRange(new object[] {
            "A",
            "B"});
            this.m_new_fast_inventory_flag.Location = new System.Drawing.Point(143, 44);
            this.m_new_fast_inventory_flag.Name = "m_new_fast_inventory_flag";
            this.m_new_fast_inventory_flag.Size = new System.Drawing.Size(55, 20);
            this.m_new_fast_inventory_flag.TabIndex = 74;
            // 
            // m_new_fast_inventory_session
            // 
            this.m_new_fast_inventory_session.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.m_new_fast_inventory_session.Enabled = false;
            this.m_new_fast_inventory_session.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_new_fast_inventory_session.ForeColor = System.Drawing.SystemColors.InfoText;
            this.m_new_fast_inventory_session.FormattingEnabled = true;
            this.m_new_fast_inventory_session.Items.AddRange(new object[] {
            "S0",
            "S1",
            "S2",
            "S3"});
            this.m_new_fast_inventory_session.Location = new System.Drawing.Point(78, 45);
            this.m_new_fast_inventory_session.Name = "m_new_fast_inventory_session";
            this.m_new_fast_inventory_session.Size = new System.Drawing.Size(55, 20);
            this.m_new_fast_inventory_session.TabIndex = 73;
            // 
            // m_new_fast_inventory
            // 
            this.m_new_fast_inventory.AutoSize = true;
            this.m_new_fast_inventory.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.m_new_fast_inventory.Location = new System.Drawing.Point(10, 47);
            this.m_new_fast_inventory.Name = "m_new_fast_inventory";
            this.m_new_fast_inventory.Size = new System.Drawing.Size(66, 16);
            this.m_new_fast_inventory.TabIndex = 72;
            this.m_new_fast_inventory.Text = "Session";
            this.m_new_fast_inventory.UseVisualStyleBackColor = true;
            this.m_new_fast_inventory.CheckedChanged += new System.EventHandler(this.m_new_fast_inventory_CheckedChanged);
            // 
            // label73
            // 
            this.label73.AutoSize = true;
            this.label73.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label73.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label73.Location = new System.Drawing.Point(8, 20);
            this.label73.Name = "label73";
            this.label73.Size = new System.Drawing.Size(89, 12);
            this.label73.TabIndex = 36;
            this.label73.Text = "天线间延时(mS)";
            // 
            // txtInterval
            // 
            this.txtInterval.Location = new System.Drawing.Point(103, 17);
            this.txtInterval.Name = "txtInterval";
            this.txtInterval.Size = new System.Drawing.Size(42, 21);
            this.txtInterval.TabIndex = 57;
            this.txtInterval.Text = "0";
            this.txtInterval.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label72
            // 
            this.label72.AutoSize = true;
            this.label72.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label72.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label72.Location = new System.Drawing.Point(169, 21);
            this.label72.Name = "label72";
            this.label72.Size = new System.Drawing.Size(53, 12);
            this.label72.TabIndex = 37;
            this.label72.Text = "循环次数";
            // 
            // mReserve
            // 
            this.mReserve.AutoSize = true;
            this.mReserve.Enabled = false;
            this.mReserve.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.mReserve.ForeColor = System.Drawing.SystemColors.ControlText;
            this.mReserve.Location = new System.Drawing.Point(6, 76);
            this.mReserve.Name = "mReserve";
            this.mReserve.Size = new System.Drawing.Size(47, 12);
            this.mReserve.TabIndex = 71;
            this.mReserve.Text = "Reserve";
            // 
            // txtRepeat
            // 
            this.txtRepeat.Location = new System.Drawing.Point(230, 15);
            this.txtRepeat.Name = "txtRepeat";
            this.txtRepeat.Size = new System.Drawing.Size(42, 21);
            this.txtRepeat.TabIndex = 58;
            this.txtRepeat.Text = "1";
            this.txtRepeat.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mPower1
            // 
            this.mPower1.Enabled = false;
            this.mPower1.Location = new System.Drawing.Point(58, 74);
            this.mPower1.Name = "mPower1";
            this.mPower1.Size = new System.Drawing.Size(26, 21);
            this.mPower1.TabIndex = 59;
            this.mPower1.Text = "0";
            this.mPower1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mPower2
            // 
            this.mPower2.Location = new System.Drawing.Point(95, 73);
            this.mPower2.Name = "mPower2";
            this.mPower2.Size = new System.Drawing.Size(26, 21);
            this.mPower2.TabIndex = 60;
            this.mPower2.Text = "0";
            this.mPower2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mPower5
            // 
            this.mPower5.Enabled = false;
            this.mPower5.Location = new System.Drawing.Point(222, 71);
            this.mPower5.Name = "mPower5";
            this.mPower5.Size = new System.Drawing.Size(26, 21);
            this.mPower5.TabIndex = 63;
            this.mPower5.Text = "0";
            this.mPower5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // mPower4
            // 
            this.mPower4.Enabled = false;
            this.mPower4.Location = new System.Drawing.Point(179, 71);
            this.mPower4.Name = "mPower4";
            this.mPower4.Size = new System.Drawing.Size(26, 21);
            this.mPower4.TabIndex = 62;
            this.mPower4.Text = "0";
            this.mPower4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.mPower4.TextChanged += new System.EventHandler(this.mPower4_TextChanged);
            // 
            // mPower3
            // 
            this.mPower3.Enabled = false;
            this.mPower3.Location = new System.Drawing.Point(135, 72);
            this.mPower3.Name = "mPower3";
            this.mPower3.Size = new System.Drawing.Size(26, 21);
            this.mPower3.TabIndex = 61;
            this.mPower3.Text = "0";
            this.mPower3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // groupBox26
            // 
            this.groupBox26.Location = new System.Drawing.Point(-30, -93);
            this.groupBox26.Name = "groupBox26";
            this.groupBox26.Size = new System.Drawing.Size(200, 100);
            this.groupBox26.TabIndex = 74;
            this.groupBox26.TabStop = false;
            this.groupBox26.Text = "groupBox26";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(4, 13);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btFastInventory);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label122);
            this.splitContainer1.Panel2.Controls.Add(this.txtPStay);
            this.splitContainer1.Panel2.Controls.Add(this.label130);
            this.splitContainer1.Panel2.Controls.Add(this.label133);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect16);
            this.splitContainer1.Panel2.Controls.Add(this.txtLStay);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect13);
            this.splitContainer1.Panel2.Controls.Add(this.label134);
            this.splitContainer1.Panel2.Controls.Add(this.label135);
            this.splitContainer1.Panel2.Controls.Add(this.txtOStay);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect9);
            this.splitContainer1.Panel2.Controls.Add(this.label136);
            this.splitContainer1.Panel2.Controls.Add(this.label137);
            this.splitContainer1.Panel2.Controls.Add(this.label138);
            this.splitContainer1.Panel2.Controls.Add(this.label139);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect12);
            this.splitContainer1.Panel2.Controls.Add(this.txtNStay);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect15);
            this.splitContainer1.Panel2.Controls.Add(this.txtKStay);
            this.splitContainer1.Panel2.Controls.Add(this.label140);
            this.splitContainer1.Panel2.Controls.Add(this.txtIStay);
            this.splitContainer1.Panel2.Controls.Add(this.label141);
            this.splitContainer1.Panel2.Controls.Add(this.label142);
            this.splitContainer1.Panel2.Controls.Add(this.txtMStay);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect10);
            this.splitContainer1.Panel2.Controls.Add(this.label143);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect14);
            this.splitContainer1.Panel2.Controls.Add(this.label144);
            this.splitContainer1.Panel2.Controls.Add(this.label145);
            this.splitContainer1.Panel2.Controls.Add(this.label146);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect11);
            this.splitContainer1.Panel2.Controls.Add(this.txtJStay);
            this.splitContainer1.Panel2.Controls.Add(this.label123);
            this.splitContainer1.Panel2.Controls.Add(this.textBox16);
            this.splitContainer1.Panel2.Controls.Add(this.label124);
            this.splitContainer1.Panel2.Controls.Add(this.label120);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox4);
            this.splitContainer1.Panel2.Controls.Add(this.txtDStay);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox1);
            this.splitContainer1.Panel2.Controls.Add(this.label64);
            this.splitContainer1.Panel2.Controls.Add(this.label116);
            this.splitContainer1.Panel2.Controls.Add(this.textBox15);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect1);
            this.splitContainer1.Panel2.Controls.Add(this.label121);
            this.splitContainer1.Panel2.Controls.Add(this.label48);
            this.splitContainer1.Panel2.Controls.Add(this.label65);
            this.splitContainer1.Panel2.Controls.Add(this.label119);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect4);
            this.splitContainer1.Panel2.Controls.Add(this.textBox14);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox3);
            this.splitContainer1.Panel2.Controls.Add(this.txtCStay);
            this.splitContainer1.Panel2.Controls.Add(this.label59);
            this.splitContainer1.Panel2.Controls.Add(this.txtAStay);
            this.splitContainer1.Panel2.Controls.Add(this.label117);
            this.splitContainer1.Panel2.Controls.Add(this.label62);
            this.splitContainer1.Panel2.Controls.Add(this.textBox13);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect2);
            this.splitContainer1.Panel2.Controls.Add(this.label118);
            this.splitContainer1.Panel2.Controls.Add(this.comboBox2);
            this.splitContainer1.Panel2.Controls.Add(this.label61);
            this.splitContainer1.Panel2.Controls.Add(this.label63);
            this.splitContainer1.Panel2.Controls.Add(this.label60);
            this.splitContainer1.Panel2.Controls.Add(this.cmbAntSelect3);
            this.splitContainer1.Panel2.Controls.Add(this.txtBStay);
            this.splitContainer1.Size = new System.Drawing.Size(705, 152);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.TabIndex = 73;
            // 
            // btFastInventory
            // 
            this.btFastInventory.Font = new System.Drawing.Font("SimSun", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btFastInventory.ForeColor = System.Drawing.Color.DarkBlue;
            this.btFastInventory.Location = new System.Drawing.Point(6, 22);
            this.btFastInventory.Name = "btFastInventory";
            this.btFastInventory.Size = new System.Drawing.Size(123, 38);
            this.btFastInventory.TabIndex = 52;
            this.btFastInventory.Text = "开始盘存";
            this.btFastInventory.UseVisualStyleBackColor = true;
            this.btFastInventory.Click += new System.EventHandler(this.btFastInventory_Click);
            // 
            // label122
            // 
            this.label122.AutoSize = true;
            this.label122.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label122.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label122.Location = new System.Drawing.Point(456, 115);
            this.label122.Name = "label122";
            this.label122.Size = new System.Drawing.Size(11, 12);
            this.label122.TabIndex = 107;
            this.label122.Text = "P";
            // 
            // txtPStay
            // 
            this.txtPStay.Location = new System.Drawing.Point(503, 130);
            this.txtPStay.Name = "txtPStay";
            this.txtPStay.Size = new System.Drawing.Size(42, 21);
            this.txtPStay.TabIndex = 105;
            this.txtPStay.Text = "1";
            this.txtPStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label130
            // 
            this.label130.AutoSize = true;
            this.label130.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label130.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label130.Location = new System.Drawing.Point(319, 116);
            this.label130.Name = "label130";
            this.label130.Size = new System.Drawing.Size(11, 12);
            this.label130.TabIndex = 108;
            this.label130.Text = "O";
            // 
            // label133
            // 
            this.label133.AutoSize = true;
            this.label133.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label133.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label133.Location = new System.Drawing.Point(497, 116);
            this.label133.Name = "label133";
            this.label133.Size = new System.Drawing.Size(53, 12);
            this.label133.TabIndex = 97;
            this.label133.Text = "轮询次数";
            // 
            // cmbAntSelect16
            // 
            this.cmbAntSelect16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect16.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect16.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect16.FormattingEnabled = true;
            this.cmbAntSelect16.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect16.Location = new System.Drawing.Point(436, 130);
            this.cmbAntSelect16.Name = "cmbAntSelect16";
            this.cmbAntSelect16.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect16.TabIndex = 101;
            // 
            // txtLStay
            // 
            this.txtLStay.AcceptsReturn = true;
            this.txtLStay.Location = new System.Drawing.Point(504, 92);
            this.txtLStay.Name = "txtLStay";
            this.txtLStay.Size = new System.Drawing.Size(37, 21);
            this.txtLStay.TabIndex = 92;
            this.txtLStay.Text = "1";
            this.txtLStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect13
            // 
            this.cmbAntSelect13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect13.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect13.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect13.FormattingEnabled = true;
            this.cmbAntSelect13.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect13.Location = new System.Drawing.Point(8, 130);
            this.cmbAntSelect13.Name = "cmbAntSelect13";
            this.cmbAntSelect13.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect13.TabIndex = 98;
            // 
            // label134
            // 
            this.label134.AutoSize = true;
            this.label134.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label134.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label134.Location = new System.Drawing.Point(498, 79);
            this.label134.Name = "label134";
            this.label134.Size = new System.Drawing.Size(53, 12);
            this.label134.TabIndex = 88;
            this.label134.Text = "轮询次数";
            // 
            // label135
            // 
            this.label135.AutoSize = true;
            this.label135.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label135.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label135.Location = new System.Drawing.Point(28, 116);
            this.label135.Name = "label135";
            this.label135.Size = new System.Drawing.Size(11, 12);
            this.label135.TabIndex = 93;
            this.label135.Text = "M";
            // 
            // txtOStay
            // 
            this.txtOStay.Location = new System.Drawing.Point(366, 130);
            this.txtOStay.Name = "txtOStay";
            this.txtOStay.Size = new System.Drawing.Size(42, 21);
            this.txtOStay.TabIndex = 104;
            this.txtOStay.Text = "1";
            this.txtOStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect9
            // 
            this.cmbAntSelect9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect9.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect9.FormattingEnabled = true;
            this.cmbAntSelect9.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect9.Location = new System.Drawing.Point(8, 92);
            this.cmbAntSelect9.Name = "cmbAntSelect9";
            this.cmbAntSelect9.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect9.TabIndex = 77;
            // 
            // label136
            // 
            this.label136.AutoSize = true;
            this.label136.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label136.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label136.Location = new System.Drawing.Point(171, 116);
            this.label136.Name = "label136";
            this.label136.Size = new System.Drawing.Size(11, 12);
            this.label136.TabIndex = 106;
            this.label136.Text = "N";
            // 
            // label137
            // 
            this.label137.AutoSize = true;
            this.label137.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label137.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label137.Location = new System.Drawing.Point(30, 78);
            this.label137.Name = "label137";
            this.label137.Size = new System.Drawing.Size(11, 12);
            this.label137.TabIndex = 81;
            this.label137.Text = "I";
            // 
            // label138
            // 
            this.label138.AutoSize = true;
            this.label138.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label138.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label138.Location = new System.Drawing.Point(456, 78);
            this.label138.Name = "label138";
            this.label138.Size = new System.Drawing.Size(11, 12);
            this.label138.TabIndex = 87;
            this.label138.Text = "L";
            // 
            // label139
            // 
            this.label139.AutoSize = true;
            this.label139.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label139.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label139.Location = new System.Drawing.Point(364, 116);
            this.label139.Name = "label139";
            this.label139.Size = new System.Drawing.Size(53, 12);
            this.label139.TabIndex = 96;
            this.label139.Text = "轮询次数";
            // 
            // cmbAntSelect12
            // 
            this.cmbAntSelect12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect12.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect12.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect12.FormattingEnabled = true;
            this.cmbAntSelect12.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect12.Location = new System.Drawing.Point(437, 92);
            this.cmbAntSelect12.Name = "cmbAntSelect12";
            this.cmbAntSelect12.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect12.TabIndex = 80;
            // 
            // txtNStay
            // 
            this.txtNStay.Location = new System.Drawing.Point(222, 130);
            this.txtNStay.Name = "txtNStay";
            this.txtNStay.Size = new System.Drawing.Size(42, 21);
            this.txtNStay.TabIndex = 103;
            this.txtNStay.Text = "1";
            this.txtNStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect15
            // 
            this.cmbAntSelect15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect15.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect15.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect15.FormattingEnabled = true;
            this.cmbAntSelect15.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect15.Location = new System.Drawing.Point(297, 130);
            this.cmbAntSelect15.Name = "cmbAntSelect15";
            this.cmbAntSelect15.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect15.TabIndex = 100;
            // 
            // txtKStay
            // 
            this.txtKStay.Location = new System.Drawing.Point(366, 92);
            this.txtKStay.Name = "txtKStay";
            this.txtKStay.Size = new System.Drawing.Size(42, 21);
            this.txtKStay.TabIndex = 91;
            this.txtKStay.Text = "1";
            this.txtKStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label140
            // 
            this.label140.AutoSize = true;
            this.label140.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label140.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label140.Location = new System.Drawing.Point(74, 79);
            this.label140.Name = "label140";
            this.label140.Size = new System.Drawing.Size(53, 12);
            this.label140.TabIndex = 82;
            this.label140.Text = "轮询次数";
            // 
            // txtIStay
            // 
            this.txtIStay.Location = new System.Drawing.Point(76, 92);
            this.txtIStay.Name = "txtIStay";
            this.txtIStay.Size = new System.Drawing.Size(42, 21);
            this.txtIStay.TabIndex = 89;
            this.txtIStay.Text = "1";
            this.txtIStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label141
            // 
            this.label141.AutoSize = true;
            this.label141.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label141.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label141.Location = new System.Drawing.Point(74, 116);
            this.label141.Name = "label141";
            this.label141.Size = new System.Drawing.Size(53, 12);
            this.label141.TabIndex = 94;
            this.label141.Text = "轮询次数";
            // 
            // label142
            // 
            this.label142.AutoSize = true;
            this.label142.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label142.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label142.Location = new System.Drawing.Point(362, 79);
            this.label142.Name = "label142";
            this.label142.Size = new System.Drawing.Size(53, 12);
            this.label142.TabIndex = 86;
            this.label142.Text = "轮询次数";
            // 
            // txtMStay
            // 
            this.txtMStay.Location = new System.Drawing.Point(76, 130);
            this.txtMStay.Name = "txtMStay";
            this.txtMStay.Size = new System.Drawing.Size(42, 21);
            this.txtMStay.TabIndex = 102;
            this.txtMStay.Text = "1";
            this.txtMStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect10
            // 
            this.cmbAntSelect10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect10.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect10.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect10.FormattingEnabled = true;
            this.cmbAntSelect10.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect10.Location = new System.Drawing.Point(147, 92);
            this.cmbAntSelect10.Name = "cmbAntSelect10";
            this.cmbAntSelect10.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect10.TabIndex = 78;
            // 
            // label143
            // 
            this.label143.AutoSize = true;
            this.label143.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label143.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label143.Location = new System.Drawing.Point(218, 116);
            this.label143.Name = "label143";
            this.label143.Size = new System.Drawing.Size(53, 12);
            this.label143.TabIndex = 95;
            this.label143.Text = "轮询次数";
            // 
            // cmbAntSelect14
            // 
            this.cmbAntSelect14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect14.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect14.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect14.FormattingEnabled = true;
            this.cmbAntSelect14.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect14.Location = new System.Drawing.Point(147, 130);
            this.cmbAntSelect14.Name = "cmbAntSelect14";
            this.cmbAntSelect14.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect14.TabIndex = 99;
            // 
            // label144
            // 
            this.label144.AutoSize = true;
            this.label144.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label144.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label144.Location = new System.Drawing.Point(171, 78);
            this.label144.Name = "label144";
            this.label144.Size = new System.Drawing.Size(11, 12);
            this.label144.TabIndex = 83;
            this.label144.Text = "J";
            // 
            // label145
            // 
            this.label145.AutoSize = true;
            this.label145.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label145.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label145.Location = new System.Drawing.Point(319, 78);
            this.label145.Name = "label145";
            this.label145.Size = new System.Drawing.Size(11, 12);
            this.label145.TabIndex = 85;
            this.label145.Text = "K";
            // 
            // label146
            // 
            this.label146.AutoSize = true;
            this.label146.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label146.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label146.Location = new System.Drawing.Point(218, 79);
            this.label146.Name = "label146";
            this.label146.Size = new System.Drawing.Size(53, 12);
            this.label146.TabIndex = 84;
            this.label146.Text = "轮询次数";
            // 
            // cmbAntSelect11
            // 
            this.cmbAntSelect11.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect11.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect11.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect11.FormattingEnabled = true;
            this.cmbAntSelect11.Items.AddRange(new object[] {
            "天线9",
            "天线10",
            "天线11",
            "天线12",
            "天线13",
            "天线14",
            "天线15",
            "天线16",
            "不选"});
            this.cmbAntSelect11.Location = new System.Drawing.Point(297, 92);
            this.cmbAntSelect11.Name = "cmbAntSelect11";
            this.cmbAntSelect11.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect11.TabIndex = 79;
            // 
            // txtJStay
            // 
            this.txtJStay.Location = new System.Drawing.Point(220, 92);
            this.txtJStay.Name = "txtJStay";
            this.txtJStay.Size = new System.Drawing.Size(42, 21);
            this.txtJStay.TabIndex = 90;
            this.txtJStay.Text = "1";
            this.txtJStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label123
            // 
            this.label123.AutoSize = true;
            this.label123.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label123.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label123.Location = new System.Drawing.Point(456, 40);
            this.label123.Name = "label123";
            this.label123.Size = new System.Drawing.Size(11, 12);
            this.label123.TabIndex = 75;
            this.label123.Text = "H";
            // 
            // textBox16
            // 
            this.textBox16.Location = new System.Drawing.Point(503, 55);
            this.textBox16.Name = "textBox16";
            this.textBox16.Size = new System.Drawing.Size(42, 21);
            this.textBox16.TabIndex = 72;
            this.textBox16.Text = "1";
            this.textBox16.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label124
            // 
            this.label124.AutoSize = true;
            this.label124.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label124.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label124.Location = new System.Drawing.Point(319, 41);
            this.label124.Name = "label124";
            this.label124.Size = new System.Drawing.Size(11, 12);
            this.label124.TabIndex = 76;
            this.label124.Text = "G";
            // 
            // label120
            // 
            this.label120.AutoSize = true;
            this.label120.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label120.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label120.Location = new System.Drawing.Point(497, 41);
            this.label120.Name = "label120";
            this.label120.Size = new System.Drawing.Size(53, 12);
            this.label120.TabIndex = 64;
            this.label120.Text = "轮询次数";
            // 
            // comboBox4
            // 
            this.comboBox4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.comboBox4.Location = new System.Drawing.Point(436, 55);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(55, 20);
            this.comboBox4.TabIndex = 68;
            // 
            // txtDStay
            // 
            this.txtDStay.AcceptsReturn = true;
            this.txtDStay.Location = new System.Drawing.Point(504, 17);
            this.txtDStay.Name = "txtDStay";
            this.txtDStay.Size = new System.Drawing.Size(37, 21);
            this.txtDStay.TabIndex = 59;
            this.txtDStay.Text = "1";
            this.txtDStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox1
            // 
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.comboBox1.Location = new System.Drawing.Point(8, 55);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(55, 20);
            this.comboBox1.TabIndex = 65;
            // 
            // label64
            // 
            this.label64.AutoSize = true;
            this.label64.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label64.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label64.Location = new System.Drawing.Point(498, 4);
            this.label64.Name = "label64";
            this.label64.Size = new System.Drawing.Size(53, 12);
            this.label64.TabIndex = 39;
            this.label64.Text = "轮询次数";
            // 
            // label116
            // 
            this.label116.AutoSize = true;
            this.label116.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label116.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label116.Location = new System.Drawing.Point(28, 41);
            this.label116.Name = "label116";
            this.label116.Size = new System.Drawing.Size(11, 12);
            this.label116.TabIndex = 60;
            this.label116.Text = "E";
            // 
            // textBox15
            // 
            this.textBox15.Location = new System.Drawing.Point(366, 55);
            this.textBox15.Name = "textBox15";
            this.textBox15.Size = new System.Drawing.Size(42, 21);
            this.textBox15.TabIndex = 71;
            this.textBox15.Text = "1";
            this.textBox15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect1
            // 
            this.cmbAntSelect1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect1.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect1.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect1.FormattingEnabled = true;
            this.cmbAntSelect1.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.cmbAntSelect1.Location = new System.Drawing.Point(8, 17);
            this.cmbAntSelect1.Name = "cmbAntSelect1";
            this.cmbAntSelect1.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect1.TabIndex = 13;
            // 
            // label121
            // 
            this.label121.AutoSize = true;
            this.label121.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label121.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label121.Location = new System.Drawing.Point(171, 41);
            this.label121.Name = "label121";
            this.label121.Size = new System.Drawing.Size(11, 12);
            this.label121.TabIndex = 73;
            this.label121.Text = "F";
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label48.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label48.Location = new System.Drawing.Point(30, 3);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(11, 12);
            this.label48.TabIndex = 32;
            this.label48.Text = "A";
            // 
            // label65
            // 
            this.label65.AutoSize = true;
            this.label65.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label65.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label65.Location = new System.Drawing.Point(456, 3);
            this.label65.Name = "label65";
            this.label65.Size = new System.Drawing.Size(11, 12);
            this.label65.TabIndex = 38;
            this.label65.Text = "D";
            // 
            // label119
            // 
            this.label119.AutoSize = true;
            this.label119.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label119.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label119.Location = new System.Drawing.Point(364, 41);
            this.label119.Name = "label119";
            this.label119.Size = new System.Drawing.Size(53, 12);
            this.label119.TabIndex = 63;
            this.label119.Text = "轮询次数";
            // 
            // cmbAntSelect4
            // 
            this.cmbAntSelect4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect4.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect4.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect4.FormattingEnabled = true;
            this.cmbAntSelect4.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.cmbAntSelect4.Location = new System.Drawing.Point(437, 17);
            this.cmbAntSelect4.Name = "cmbAntSelect4";
            this.cmbAntSelect4.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect4.TabIndex = 16;
            // 
            // textBox14
            // 
            this.textBox14.Location = new System.Drawing.Point(222, 55);
            this.textBox14.Name = "textBox14";
            this.textBox14.Size = new System.Drawing.Size(42, 21);
            this.textBox14.TabIndex = 70;
            this.textBox14.Text = "1";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox3
            // 
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.comboBox3.Location = new System.Drawing.Point(297, 55);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(55, 20);
            this.comboBox3.TabIndex = 67;
            // 
            // txtCStay
            // 
            this.txtCStay.Location = new System.Drawing.Point(366, 17);
            this.txtCStay.Name = "txtCStay";
            this.txtCStay.Size = new System.Drawing.Size(42, 21);
            this.txtCStay.TabIndex = 58;
            this.txtCStay.Text = "1";
            this.txtCStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label59
            // 
            this.label59.AutoSize = true;
            this.label59.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label59.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label59.Location = new System.Drawing.Point(74, 4);
            this.label59.Name = "label59";
            this.label59.Size = new System.Drawing.Size(53, 12);
            this.label59.TabIndex = 33;
            this.label59.Text = "轮询次数";
            // 
            // txtAStay
            // 
            this.txtAStay.Location = new System.Drawing.Point(76, 17);
            this.txtAStay.Name = "txtAStay";
            this.txtAStay.Size = new System.Drawing.Size(42, 21);
            this.txtAStay.TabIndex = 56;
            this.txtAStay.Text = "1";
            this.txtAStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label117
            // 
            this.label117.AutoSize = true;
            this.label117.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label117.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label117.Location = new System.Drawing.Point(74, 41);
            this.label117.Name = "label117";
            this.label117.Size = new System.Drawing.Size(53, 12);
            this.label117.TabIndex = 61;
            this.label117.Text = "轮询次数";
            // 
            // label62
            // 
            this.label62.AutoSize = true;
            this.label62.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label62.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label62.Location = new System.Drawing.Point(362, 4);
            this.label62.Name = "label62";
            this.label62.Size = new System.Drawing.Size(53, 12);
            this.label62.TabIndex = 37;
            this.label62.Text = "轮询次数";
            // 
            // textBox13
            // 
            this.textBox13.Location = new System.Drawing.Point(76, 55);
            this.textBox13.Name = "textBox13";
            this.textBox13.Size = new System.Drawing.Size(42, 21);
            this.textBox13.TabIndex = 69;
            this.textBox13.Text = "1";
            this.textBox13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // cmbAntSelect2
            // 
            this.cmbAntSelect2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect2.FormattingEnabled = true;
            this.cmbAntSelect2.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.cmbAntSelect2.Location = new System.Drawing.Point(147, 17);
            this.cmbAntSelect2.Name = "cmbAntSelect2";
            this.cmbAntSelect2.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect2.TabIndex = 14;
            // 
            // label118
            // 
            this.label118.AutoSize = true;
            this.label118.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label118.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label118.Location = new System.Drawing.Point(218, 41);
            this.label118.Name = "label118";
            this.label118.Size = new System.Drawing.Size(53, 12);
            this.label118.TabIndex = 62;
            this.label118.Text = "轮询次数";
            // 
            // comboBox2
            // 
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comboBox2.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.comboBox2.Location = new System.Drawing.Point(147, 55);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(55, 20);
            this.comboBox2.TabIndex = 66;
            // 
            // label61
            // 
            this.label61.AutoSize = true;
            this.label61.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label61.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label61.Location = new System.Drawing.Point(171, 3);
            this.label61.Name = "label61";
            this.label61.Size = new System.Drawing.Size(11, 12);
            this.label61.TabIndex = 34;
            this.label61.Text = "B";
            this.label61.Click += new System.EventHandler(this.label61_Click);
            // 
            // label63
            // 
            this.label63.AutoSize = true;
            this.label63.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label63.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label63.Location = new System.Drawing.Point(319, 3);
            this.label63.Name = "label63";
            this.label63.Size = new System.Drawing.Size(11, 12);
            this.label63.TabIndex = 36;
            this.label63.Text = "C";
            // 
            // label60
            // 
            this.label60.AutoSize = true;
            this.label60.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label60.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label60.Location = new System.Drawing.Point(218, 4);
            this.label60.Name = "label60";
            this.label60.Size = new System.Drawing.Size(53, 12);
            this.label60.TabIndex = 35;
            this.label60.Text = "轮询次数";
            // 
            // cmbAntSelect3
            // 
            this.cmbAntSelect3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAntSelect3.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cmbAntSelect3.ForeColor = System.Drawing.SystemColors.InfoText;
            this.cmbAntSelect3.FormattingEnabled = true;
            this.cmbAntSelect3.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "天线5",
            "天线6",
            "天线7",
            "天线8",
            "不选"});
            this.cmbAntSelect3.Location = new System.Drawing.Point(297, 17);
            this.cmbAntSelect3.Name = "cmbAntSelect3";
            this.cmbAntSelect3.Size = new System.Drawing.Size(55, 20);
            this.cmbAntSelect3.TabIndex = 15;
            // 
            // txtBStay
            // 
            this.txtBStay.Location = new System.Drawing.Point(220, 17);
            this.txtBStay.Name = "txtBStay";
            this.txtBStay.Size = new System.Drawing.Size(42, 21);
            this.txtBStay.TabIndex = 57;
            this.txtBStay.Text = "1";
            this.txtBStay.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // comboBox7
            // 
            this.comboBox7.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox7.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox7.FormattingEnabled = true;
            this.comboBox7.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "不选"});
            this.comboBox7.Location = new System.Drawing.Point(-165, 111);
            this.comboBox7.Name = "comboBox7";
            this.comboBox7.Size = new System.Drawing.Size(55, 20);
            this.comboBox7.TabIndex = 39;
            // 
            // txtFastMaxRssi
            // 
            this.txtFastMaxRssi.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFastMaxRssi.Location = new System.Drawing.Point(503, 329);
            this.txtFastMaxRssi.Name = "txtFastMaxRssi";
            this.txtFastMaxRssi.Size = new System.Drawing.Size(62, 21);
            this.txtFastMaxRssi.TabIndex = 40;
            // 
            // txtFastMinRssi
            // 
            this.txtFastMinRssi.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFastMinRssi.Location = new System.Drawing.Point(339, 330);
            this.txtFastMinRssi.Name = "txtFastMinRssi";
            this.txtFastMinRssi.Size = new System.Drawing.Size(62, 21);
            this.txtFastMinRssi.TabIndex = 41;
            // 
            // buttonFastFresh
            // 
            this.buttonFastFresh.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.buttonFastFresh.ForeColor = System.Drawing.SystemColors.Desktop;
            this.buttonFastFresh.Location = new System.Drawing.Point(600, 327);
            this.buttonFastFresh.Name = "buttonFastFresh";
            this.buttonFastFresh.Size = new System.Drawing.Size(89, 23);
            this.buttonFastFresh.TabIndex = 28;
            this.buttonFastFresh.Text = "刷新界面";
            this.buttonFastFresh.UseVisualStyleBackColor = true;
            this.buttonFastFresh.Click += new System.EventHandler(this.buttonFastFresh_Click);
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(438, 334);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(59, 12);
            this.label22.TabIndex = 26;
            this.label22.Text = "Max RSSI:";
            // 
            // lvFastList
            // 
            this.lvFastList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader31,
            this.columnHeader32,
            this.columnHeader33,
            this.columnHeader34,
            this.columnHeader35,
            this.columnHeader356,
            this.columnHeader36});
            this.lvFastList.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lvFastList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvFastList.GridLines = true;
            this.lvFastList.HideSelection = false;
            this.lvFastList.Location = new System.Drawing.Point(3, 356);
            this.lvFastList.Name = "lvFastList";
            this.lvFastList.Size = new System.Drawing.Size(994, 163);
            this.lvFastList.TabIndex = 24;
            this.lvFastList.UseCompatibleStateImageBehavior = false;
            this.lvFastList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader31
            // 
            this.columnHeader31.Text = "ID";
            this.columnHeader31.Width = 56;
            // 
            // columnHeader32
            // 
            this.columnHeader32.Text = "EPC";
            this.columnHeader32.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader32.Width = 428;
            // 
            // columnHeader33
            // 
            this.columnHeader33.Text = "PC";
            this.columnHeader33.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader33.Width = 65;
            // 
            // columnHeader34
            // 
            this.columnHeader34.Text = "识别次数(ANT1/2/3/4/5/6/7/8)";
            this.columnHeader34.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader34.Width = 226;
            // 
            // columnHeader35
            // 
            this.columnHeader35.Text = "RSSI";
            this.columnHeader35.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader35.Width = 96;
            // 
            // columnHeader356
            // 
            this.columnHeader356.Text = "Phase";
            this.columnHeader356.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader356.Width = 56;
            // 
            // columnHeader36
            // 
            this.columnHeader36.Text = "载波频率";
            this.columnHeader36.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader36.Width = 125;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label49.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label49.Location = new System.Drawing.Point(274, 336);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(59, 12);
            this.label49.TabIndex = 27;
            this.label49.Text = "Min RSSI:";
            // 
            // txtFastTagList
            // 
            this.txtFastTagList.AutoSize = true;
            this.txtFastTagList.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtFastTagList.ForeColor = System.Drawing.SystemColors.Desktop;
            this.txtFastTagList.Location = new System.Drawing.Point(7, 342);
            this.txtFastTagList.Name = "txtFastTagList";
            this.txtFastTagList.Size = new System.Drawing.Size(65, 12);
            this.txtFastTagList.TabIndex = 23;
            this.txtFastTagList.Text = "标签列表: ";
            // 
            // pageAcessTag
            // 
            this.pageAcessTag.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pageAcessTag.Controls.Add(this.ltvOperate);
            this.pageAcessTag.Controls.Add(this.gbCmdOperateTag);
            this.pageAcessTag.Location = new System.Drawing.Point(4, 22);
            this.pageAcessTag.Name = "pageAcessTag";
            this.pageAcessTag.Size = new System.Drawing.Size(1000, 522);
            this.pageAcessTag.TabIndex = 3;
            this.pageAcessTag.Text = "存取标签";
            this.pageAcessTag.UseVisualStyleBackColor = true;
            // 
            // ltvOperate
            // 
            this.ltvOperate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader5,
            this.columnHeader9,
            this.columnHeader10,
            this.columnHeader11,
            this.columnHeader12,
            this.columnHeader13,
            this.columnHeader14,
            this.columnHeader15});
            this.ltvOperate.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ltvOperate.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltvOperate.GridLines = true;
            this.ltvOperate.HideSelection = false;
            this.ltvOperate.Location = new System.Drawing.Point(0, 325);
            this.ltvOperate.Name = "ltvOperate";
            this.ltvOperate.Size = new System.Drawing.Size(1000, 197);
            this.ltvOperate.TabIndex = 10;
            this.ltvOperate.UseCompatibleStateImageBehavior = false;
            this.ltvOperate.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "序号";
            // 
            // columnHeader9
            // 
            this.columnHeader9.Text = "PC";
            this.columnHeader9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader10
            // 
            this.columnHeader10.Text = "CRC";
            this.columnHeader10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // columnHeader11
            // 
            this.columnHeader11.Text = "EPC";
            this.columnHeader11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader11.Width = 260;
            // 
            // columnHeader12
            // 
            this.columnHeader12.Text = "数据";
            this.columnHeader12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader12.Width = 341;
            // 
            // columnHeader13
            // 
            this.columnHeader13.Text = "数据长度";
            this.columnHeader13.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader13.Width = 73;
            // 
            // columnHeader14
            // 
            this.columnHeader14.Text = "天线";
            this.columnHeader14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader14.Width = 49;
            // 
            // columnHeader15
            // 
            this.columnHeader15.Text = "操作次数";
            this.columnHeader15.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader15.Width = 79;
            // 
            // gbCmdOperateTag
            // 
            this.gbCmdOperateTag.Controls.Add(this.groupBox16);
            this.gbCmdOperateTag.Controls.Add(this.groupBox15);
            this.gbCmdOperateTag.Controls.Add(this.groupBox14);
            this.gbCmdOperateTag.Controls.Add(this.groupBox13);
            this.gbCmdOperateTag.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbCmdOperateTag.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.gbCmdOperateTag.Location = new System.Drawing.Point(0, 0);
            this.gbCmdOperateTag.Name = "gbCmdOperateTag";
            this.gbCmdOperateTag.Size = new System.Drawing.Size(1000, 325);
            this.gbCmdOperateTag.TabIndex = 8;
            this.gbCmdOperateTag.TabStop = false;
            this.gbCmdOperateTag.Text = "标签操作";
            // 
            // groupBox16
            // 
            this.groupBox16.Controls.Add(this.btnKillTag);
            this.groupBox16.Controls.Add(this.htxtKillPwd);
            this.groupBox16.Controls.Add(this.label29);
            this.groupBox16.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox16.Location = new System.Drawing.Point(3, 270);
            this.groupBox16.Name = "groupBox16";
            this.groupBox16.Size = new System.Drawing.Size(994, 45);
            this.groupBox16.TabIndex = 4;
            this.groupBox16.TabStop = false;
            this.groupBox16.Text = "销毁标签";
            // 
            // btnKillTag
            // 
            this.btnKillTag.Location = new System.Drawing.Point(888, 19);
            this.btnKillTag.Name = "btnKillTag";
            this.btnKillTag.Size = new System.Drawing.Size(90, 23);
            this.btnKillTag.TabIndex = 14;
            this.btnKillTag.Text = "销毁标签";
            this.btnKillTag.UseVisualStyleBackColor = true;
            this.btnKillTag.Click += new System.EventHandler(this.btnKillTag_Click);
            // 
            // htxtKillPwd
            // 
            this.htxtKillPwd.Location = new System.Drawing.Point(402, 21);
            this.htxtKillPwd.Name = "htxtKillPwd";
            this.htxtKillPwd.Size = new System.Drawing.Size(120, 21);
            this.htxtKillPwd.TabIndex = 13;
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(307, 25);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(89, 12);
            this.label29.TabIndex = 13;
            this.label29.Text = "销毁密码(HEX):";
            // 
            // groupBox15
            // 
            this.groupBox15.Controls.Add(this.htxtLockPwd);
            this.groupBox15.Controls.Add(this.label28);
            this.groupBox15.Controls.Add(this.groupBox19);
            this.groupBox15.Controls.Add(this.groupBox18);
            this.groupBox15.Controls.Add(this.btnLockTag);
            this.groupBox15.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox15.Location = new System.Drawing.Point(3, 162);
            this.groupBox15.Name = "groupBox15";
            this.groupBox15.Size = new System.Drawing.Size(994, 108);
            this.groupBox15.TabIndex = 3;
            this.groupBox15.TabStop = false;
            this.groupBox15.Text = "锁定标签";
            // 
            // htxtLockPwd
            // 
            this.htxtLockPwd.Location = new System.Drawing.Point(736, 45);
            this.htxtLockPwd.Name = "htxtLockPwd";
            this.htxtLockPwd.Size = new System.Drawing.Size(120, 21);
            this.htxtLockPwd.TabIndex = 12;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(641, 49);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(89, 12);
            this.label28.TabIndex = 12;
            this.label28.Text = "访问密码(HEX):";
            // 
            // groupBox19
            // 
            this.groupBox19.Controls.Add(this.rdbUserMemory);
            this.groupBox19.Controls.Add(this.rdbTidMemory);
            this.groupBox19.Controls.Add(this.rdbEpcMermory);
            this.groupBox19.Controls.Add(this.rdbKillPwd);
            this.groupBox19.Controls.Add(this.rdbAccessPwd);
            this.groupBox19.Location = new System.Drawing.Point(16, 14);
            this.groupBox19.Name = "groupBox19";
            this.groupBox19.Size = new System.Drawing.Size(584, 40);
            this.groupBox19.TabIndex = 2;
            this.groupBox19.TabStop = false;
            // 
            // rdbUserMemory
            // 
            this.rdbUserMemory.AutoSize = true;
            this.rdbUserMemory.Location = new System.Drawing.Point(483, 15);
            this.rdbUserMemory.Name = "rdbUserMemory";
            this.rdbUserMemory.Size = new System.Drawing.Size(59, 16);
            this.rdbUserMemory.TabIndex = 4;
            this.rdbUserMemory.TabStop = true;
            this.rdbUserMemory.Text = "USER区";
            this.rdbUserMemory.UseVisualStyleBackColor = true;
            // 
            // rdbTidMemory
            // 
            this.rdbTidMemory.AutoSize = true;
            this.rdbTidMemory.Location = new System.Drawing.Point(378, 15);
            this.rdbTidMemory.Name = "rdbTidMemory";
            this.rdbTidMemory.Size = new System.Drawing.Size(53, 16);
            this.rdbTidMemory.TabIndex = 3;
            this.rdbTidMemory.TabStop = true;
            this.rdbTidMemory.Text = "TID区";
            this.rdbTidMemory.UseVisualStyleBackColor = true;
            // 
            // rdbEpcMermory
            // 
            this.rdbEpcMermory.AutoSize = true;
            this.rdbEpcMermory.Location = new System.Drawing.Point(275, 15);
            this.rdbEpcMermory.Name = "rdbEpcMermory";
            this.rdbEpcMermory.Size = new System.Drawing.Size(53, 16);
            this.rdbEpcMermory.TabIndex = 2;
            this.rdbEpcMermory.TabStop = true;
            this.rdbEpcMermory.Text = "EPC区";
            this.rdbEpcMermory.UseVisualStyleBackColor = true;
            // 
            // rdbKillPwd
            // 
            this.rdbKillPwd.AutoSize = true;
            this.rdbKillPwd.Location = new System.Drawing.Point(142, 15);
            this.rdbKillPwd.Name = "rdbKillPwd";
            this.rdbKillPwd.Size = new System.Drawing.Size(83, 16);
            this.rdbKillPwd.TabIndex = 1;
            this.rdbKillPwd.TabStop = true;
            this.rdbKillPwd.Text = "销毁密码区";
            this.rdbKillPwd.UseVisualStyleBackColor = true;
            // 
            // rdbAccessPwd
            // 
            this.rdbAccessPwd.AutoSize = true;
            this.rdbAccessPwd.Location = new System.Drawing.Point(9, 15);
            this.rdbAccessPwd.Name = "rdbAccessPwd";
            this.rdbAccessPwd.Size = new System.Drawing.Size(83, 16);
            this.rdbAccessPwd.TabIndex = 0;
            this.rdbAccessPwd.TabStop = true;
            this.rdbAccessPwd.Text = "访问密码区";
            this.rdbAccessPwd.UseVisualStyleBackColor = true;
            // 
            // groupBox18
            // 
            this.groupBox18.Controls.Add(this.rdbLockEver);
            this.groupBox18.Controls.Add(this.rdbFreeEver);
            this.groupBox18.Controls.Add(this.rdbLock);
            this.groupBox18.Controls.Add(this.rdbFree);
            this.groupBox18.Location = new System.Drawing.Point(16, 51);
            this.groupBox18.Name = "groupBox18";
            this.groupBox18.Size = new System.Drawing.Size(584, 40);
            this.groupBox18.TabIndex = 1;
            this.groupBox18.TabStop = false;
            // 
            // rdbLockEver
            // 
            this.rdbLockEver.AutoSize = true;
            this.rdbLockEver.Location = new System.Drawing.Point(483, 14);
            this.rdbLockEver.Name = "rdbLockEver";
            this.rdbLockEver.Size = new System.Drawing.Size(71, 16);
            this.rdbLockEver.TabIndex = 3;
            this.rdbLockEver.TabStop = true;
            this.rdbLockEver.Text = "永久锁定";
            this.rdbLockEver.UseVisualStyleBackColor = true;
            // 
            // rdbFreeEver
            // 
            this.rdbFreeEver.AutoSize = true;
            this.rdbFreeEver.Location = new System.Drawing.Point(309, 14);
            this.rdbFreeEver.Name = "rdbFreeEver";
            this.rdbFreeEver.Size = new System.Drawing.Size(71, 16);
            this.rdbFreeEver.TabIndex = 2;
            this.rdbFreeEver.TabStop = true;
            this.rdbFreeEver.Text = "永久开放";
            this.rdbFreeEver.UseVisualStyleBackColor = true;
            // 
            // rdbLock
            // 
            this.rdbLock.AutoSize = true;
            this.rdbLock.Location = new System.Drawing.Point(159, 14);
            this.rdbLock.Name = "rdbLock";
            this.rdbLock.Size = new System.Drawing.Size(47, 16);
            this.rdbLock.TabIndex = 1;
            this.rdbLock.TabStop = true;
            this.rdbLock.Text = "锁定";
            this.rdbLock.UseVisualStyleBackColor = true;
            // 
            // rdbFree
            // 
            this.rdbFree.AutoSize = true;
            this.rdbFree.Location = new System.Drawing.Point(9, 14);
            this.rdbFree.Name = "rdbFree";
            this.rdbFree.Size = new System.Drawing.Size(47, 16);
            this.rdbFree.TabIndex = 0;
            this.rdbFree.TabStop = true;
            this.rdbFree.Text = "开放";
            this.rdbFree.UseVisualStyleBackColor = true;
            // 
            // btnLockTag
            // 
            this.btnLockTag.Location = new System.Drawing.Point(888, 44);
            this.btnLockTag.Name = "btnLockTag";
            this.btnLockTag.Size = new System.Drawing.Size(90, 23);
            this.btnLockTag.TabIndex = 0;
            this.btnLockTag.Text = "锁定标签";
            this.btnLockTag.UseVisualStyleBackColor = true;
            this.btnLockTag.Click += new System.EventHandler(this.btnLockTag_Click);
            // 
            // groupBox14
            // 
            this.groupBox14.Controls.Add(this.radioButton2);
            this.groupBox14.Controls.Add(this.radioButton1);
            this.groupBox14.Controls.Add(this.htxtWriteData);
            this.groupBox14.Controls.Add(this.txtWordCnt);
            this.groupBox14.Controls.Add(this.label27);
            this.groupBox14.Controls.Add(this.btnWriteTag);
            this.groupBox14.Controls.Add(this.btnReadTag);
            this.groupBox14.Controls.Add(this.txtWordAdd);
            this.groupBox14.Controls.Add(this.label26);
            this.groupBox14.Controls.Add(this.htxtReadAndWritePwd);
            this.groupBox14.Controls.Add(this.label25);
            this.groupBox14.Controls.Add(this.groupBox17);
            this.groupBox14.Controls.Add(this.label24);
            this.groupBox14.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox14.Location = new System.Drawing.Point(3, 67);
            this.groupBox14.Name = "groupBox14";
            this.groupBox14.Size = new System.Drawing.Size(994, 95);
            this.groupBox14.TabIndex = 2;
            this.groupBox14.TabStop = false;
            this.groupBox14.Text = "读写标签";
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Checked = true;
            this.radioButton2.Location = new System.Drawing.Point(729, 62);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(83, 16);
            this.radioButton2.TabIndex = 12;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "BlockWrite";
            this.radioButton2.UseVisualStyleBackColor = true;
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Location = new System.Drawing.Point(829, 62);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(53, 16);
            this.radioButton1.TabIndex = 11;
            this.radioButton1.Text = "Write";
            this.radioButton1.UseVisualStyleBackColor = true;
            // 
            // htxtWriteData
            // 
            this.htxtWriteData.Location = new System.Drawing.Point(106, 58);
            this.htxtWriteData.Name = "htxtWriteData";
            this.htxtWriteData.Size = new System.Drawing.Size(617, 21);
            this.htxtWriteData.TabIndex = 10;
            // 
            // txtWordCnt
            // 
            this.txtWordCnt.Location = new System.Drawing.Point(808, 23);
            this.txtWordCnt.Name = "txtWordCnt";
            this.txtWordCnt.Size = new System.Drawing.Size(48, 21);
            this.txtWordCnt.TabIndex = 9;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(707, 27);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(95, 12);
            this.label27.TabIndex = 8;
            this.label27.Text = "数据长度(WORD):";
            // 
            // btnWriteTag
            // 
            this.btnWriteTag.Location = new System.Drawing.Point(888, 58);
            this.btnWriteTag.Name = "btnWriteTag";
            this.btnWriteTag.Size = new System.Drawing.Size(90, 23);
            this.btnWriteTag.TabIndex = 7;
            this.btnWriteTag.Text = "写标签";
            this.btnWriteTag.UseVisualStyleBackColor = true;
            this.btnWriteTag.Click += new System.EventHandler(this.btnWriteTag_Click);
            // 
            // btnReadTag
            // 
            this.btnReadTag.Location = new System.Drawing.Point(888, 22);
            this.btnReadTag.Name = "btnReadTag";
            this.btnReadTag.Size = new System.Drawing.Size(90, 23);
            this.btnReadTag.TabIndex = 6;
            this.btnReadTag.Text = "读标签";
            this.btnReadTag.UseVisualStyleBackColor = true;
            this.btnReadTag.Click += new System.EventHandler(this.btnReadTag_Click);
            // 
            // txtWordAdd
            // 
            this.txtWordAdd.Location = new System.Drawing.Point(641, 23);
            this.txtWordAdd.Name = "txtWordAdd";
            this.txtWordAdd.Size = new System.Drawing.Size(48, 21);
            this.txtWordAdd.TabIndex = 5;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(540, 27);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(95, 12);
            this.label26.TabIndex = 4;
            this.label26.Text = "起始地址(WORD):";
            // 
            // htxtReadAndWritePwd
            // 
            this.htxtReadAndWritePwd.Location = new System.Drawing.Point(402, 23);
            this.htxtReadAndWritePwd.Name = "htxtReadAndWritePwd";
            this.htxtReadAndWritePwd.Size = new System.Drawing.Size(120, 21);
            this.htxtReadAndWritePwd.TabIndex = 3;
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(295, 27);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(101, 12);
            this.label25.TabIndex = 2;
            this.label25.Text = "访问密码（HEX）:";
            // 
            // groupBox17
            // 
            this.groupBox17.Controls.Add(this.rdbUser);
            this.groupBox17.Controls.Add(this.rdbTid);
            this.groupBox17.Controls.Add(this.rdbEpc);
            this.groupBox17.Controls.Add(this.rdbReserved);
            this.groupBox17.Location = new System.Drawing.Point(18, 12);
            this.groupBox17.Name = "groupBox17";
            this.groupBox17.Size = new System.Drawing.Size(273, 40);
            this.groupBox17.TabIndex = 1;
            this.groupBox17.TabStop = false;
            // 
            // rdbUser
            // 
            this.rdbUser.AutoSize = true;
            this.rdbUser.Location = new System.Drawing.Point(189, 13);
            this.rdbUser.Name = "rdbUser";
            this.rdbUser.Size = new System.Drawing.Size(59, 16);
            this.rdbUser.TabIndex = 3;
            this.rdbUser.TabStop = true;
            this.rdbUser.Text = "USER区";
            this.rdbUser.UseVisualStyleBackColor = true;
            // 
            // rdbTid
            // 
            this.rdbTid.AutoSize = true;
            this.rdbTid.Location = new System.Drawing.Point(130, 13);
            this.rdbTid.Name = "rdbTid";
            this.rdbTid.Size = new System.Drawing.Size(53, 16);
            this.rdbTid.TabIndex = 2;
            this.rdbTid.TabStop = true;
            this.rdbTid.Text = "TID区";
            this.rdbTid.UseVisualStyleBackColor = true;
            // 
            // rdbEpc
            // 
            this.rdbEpc.AutoSize = true;
            this.rdbEpc.Location = new System.Drawing.Point(71, 13);
            this.rdbEpc.Name = "rdbEpc";
            this.rdbEpc.Size = new System.Drawing.Size(53, 16);
            this.rdbEpc.TabIndex = 1;
            this.rdbEpc.TabStop = true;
            this.rdbEpc.Text = "EPC区";
            this.rdbEpc.UseVisualStyleBackColor = true;
            // 
            // rdbReserved
            // 
            this.rdbReserved.AutoSize = true;
            this.rdbReserved.Location = new System.Drawing.Point(6, 13);
            this.rdbReserved.Name = "rdbReserved";
            this.rdbReserved.Size = new System.Drawing.Size(59, 16);
            this.rdbReserved.TabIndex = 0;
            this.rdbReserved.TabStop = true;
            this.rdbReserved.Text = "密码区";
            this.rdbReserved.UseVisualStyleBackColor = true;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(19, 64);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(89, 12);
            this.label24.TabIndex = 0;
            this.label24.Text = "写入数据(HEX):";
            // 
            // groupBox13
            // 
            this.groupBox13.Controls.Add(this.label23);
            this.groupBox13.Controls.Add(this.btnSetAccessEpcMatch);
            this.groupBox13.Controls.Add(this.cmbSetAccessEpcMatch);
            this.groupBox13.Controls.Add(this.txtAccessEpcMatch);
            this.groupBox13.Controls.Add(this.ckAccessEpcMatch);
            this.groupBox13.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox13.Location = new System.Drawing.Point(3, 17);
            this.groupBox13.Name = "groupBox13";
            this.groupBox13.Size = new System.Drawing.Size(994, 50);
            this.groupBox13.TabIndex = 1;
            this.groupBox13.TabStop = false;
            this.groupBox13.Text = "选定操作标签";
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(468, 22);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(59, 12);
            this.label23.TabIndex = 4;
            this.label23.Text = "标签列表:";
            // 
            // btnSetAccessEpcMatch
            // 
            this.btnSetAccessEpcMatch.Location = new System.Drawing.Point(888, 17);
            this.btnSetAccessEpcMatch.Name = "btnSetAccessEpcMatch";
            this.btnSetAccessEpcMatch.Size = new System.Drawing.Size(90, 23);
            this.btnSetAccessEpcMatch.TabIndex = 3;
            this.btnSetAccessEpcMatch.Text = "选定标签";
            this.btnSetAccessEpcMatch.UseVisualStyleBackColor = true;
            this.btnSetAccessEpcMatch.Click += new System.EventHandler(this.btnSetAccessEpcMatch_Click);
            // 
            // cmbSetAccessEpcMatch
            // 
            this.cmbSetAccessEpcMatch.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetAccessEpcMatch.FormattingEnabled = true;
            this.cmbSetAccessEpcMatch.Location = new System.Drawing.Point(533, 18);
            this.cmbSetAccessEpcMatch.Name = "cmbSetAccessEpcMatch";
            this.cmbSetAccessEpcMatch.Size = new System.Drawing.Size(323, 20);
            this.cmbSetAccessEpcMatch.TabIndex = 2;
            this.cmbSetAccessEpcMatch.DropDown += new System.EventHandler(this.cmbSetAccessEpcMatch_DropDown);
            // 
            // txtAccessEpcMatch
            // 
            this.txtAccessEpcMatch.Location = new System.Drawing.Point(106, 18);
            this.txtAccessEpcMatch.Name = "txtAccessEpcMatch";
            this.txtAccessEpcMatch.ReadOnly = true;
            this.txtAccessEpcMatch.Size = new System.Drawing.Size(320, 21);
            this.txtAccessEpcMatch.TabIndex = 1;
            // 
            // ckAccessEpcMatch
            // 
            this.ckAccessEpcMatch.AutoSize = true;
            this.ckAccessEpcMatch.Location = new System.Drawing.Point(16, 20);
            this.ckAccessEpcMatch.Name = "ckAccessEpcMatch";
            this.ckAccessEpcMatch.Size = new System.Drawing.Size(90, 16);
            this.ckAccessEpcMatch.TabIndex = 0;
            this.ckAccessEpcMatch.Text = "已选定标签:";
            this.ckAccessEpcMatch.UseVisualStyleBackColor = true;
            this.ckAccessEpcMatch.CheckedChanged += new System.EventHandler(this.cbAccessEpcMatch_CheckedChanged);
            // 
            // tabPage3
            // 
            this.tabPage3.BackColor = System.Drawing.Color.WhiteSmoke;
            this.tabPage3.Controls.Add(this.listView2);
            this.tabPage3.Controls.Add(this.groupBox22);
            this.tabPage3.Controls.Add(this.groupBox12);
            this.tabPage3.Controls.Add(this.groupBox9);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(1000, 522);
            this.tabPage3.TabIndex = 4;
            this.tabPage3.Text = "设置标签过滤";
            // 
            // listView2
            // 
            this.listView2.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader6,
            this.columnHeader7,
            this.columnHeader8});
            this.listView2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView2.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.listView2.GridLines = true;
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(3, 285);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(994, 234);
            this.listView2.TabIndex = 30;
            this.listView2.UseCompatibleStateImageBehavior = false;
            this.listView2.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "过滤ID";
            this.columnHeader1.Width = 56;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Session ID";
            this.columnHeader2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader2.Width = 118;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "过滤行为";
            this.columnHeader3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader3.Width = 96;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "过滤区域";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 71;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "起始地址(Hex bit)";
            this.columnHeader6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader6.Width = 134;
            // 
            // columnHeader7
            // 
            this.columnHeader7.Text = "过滤长度(Hex bit)";
            this.columnHeader7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader7.Width = 138;
            // 
            // columnHeader8
            // 
            this.columnHeader8.Text = "过滤值";
            this.columnHeader8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader8.Width = 376;
            // 
            // groupBox22
            // 
            this.groupBox22.Controls.Add(this.button3);
            this.groupBox22.ForeColor = System.Drawing.Color.Black;
            this.groupBox22.Location = new System.Drawing.Point(6, 203);
            this.groupBox22.Name = "groupBox22";
            this.groupBox22.Size = new System.Drawing.Size(988, 49);
            this.groupBox22.TabIndex = 29;
            this.groupBox22.TabStop = false;
            this.groupBox22.Text = "查询过滤设置";
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(626, 14);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(202, 23);
            this.button3.TabIndex = 0;
            this.button3.Text = "查询过滤";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // groupBox12
            // 
            this.groupBox12.Controls.Add(this.label111);
            this.groupBox12.Controls.Add(this.comboBox16);
            this.groupBox12.Controls.Add(this.button2);
            this.groupBox12.ForeColor = System.Drawing.Color.Black;
            this.groupBox12.Location = new System.Drawing.Point(6, 137);
            this.groupBox12.Name = "groupBox12";
            this.groupBox12.Size = new System.Drawing.Size(988, 49);
            this.groupBox12.TabIndex = 28;
            this.groupBox12.TabStop = false;
            this.groupBox12.Text = "清除过滤设置";
            // 
            // label111
            // 
            this.label111.AutoSize = true;
            this.label111.Location = new System.Drawing.Point(106, 25);
            this.label111.Name = "label111";
            this.label111.Size = new System.Drawing.Size(47, 12);
            this.label111.TabIndex = 16;
            this.label111.Text = "过滤ID:";
            // 
            // comboBox16
            // 
            this.comboBox16.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox16.FormattingEnabled = true;
            this.comboBox16.Items.AddRange(new object[] {
            "Mask ALL",
            "Mask No.1",
            "Mask No.2",
            "Mask No.3",
            "Mask No.4",
            "Mask No.5"});
            this.comboBox16.Location = new System.Drawing.Point(198, 17);
            this.comboBox16.Name = "comboBox16";
            this.comboBox16.Size = new System.Drawing.Size(91, 20);
            this.comboBox16.TabIndex = 15;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(626, 20);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(202, 23);
            this.button2.TabIndex = 0;
            this.button2.Text = "清除过滤";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // groupBox9
            // 
            this.groupBox9.Controls.Add(this.textBox12);
            this.groupBox9.Controls.Add(this.textBox11);
            this.groupBox9.Controls.Add(this.hexTextBox9);
            this.groupBox9.Controls.Add(this.label38);
            this.groupBox9.Controls.Add(this.comboBox12);
            this.groupBox9.Controls.Add(this.label39);
            this.groupBox9.Controls.Add(this.label71);
            this.groupBox9.Controls.Add(this.label99);
            this.groupBox9.Controls.Add(this.label100);
            this.groupBox9.Controls.Add(this.label101);
            this.groupBox9.Controls.Add(this.label102);
            this.groupBox9.Controls.Add(this.comboBox13);
            this.groupBox9.Controls.Add(this.comboBox14);
            this.groupBox9.Controls.Add(this.comboBox15);
            this.groupBox9.Controls.Add(this.button1);
            this.groupBox9.ForeColor = System.Drawing.Color.Black;
            this.groupBox9.Location = new System.Drawing.Point(6, 6);
            this.groupBox9.Name = "groupBox9";
            this.groupBox9.Size = new System.Drawing.Size(988, 125);
            this.groupBox9.TabIndex = 27;
            this.groupBox9.TabStop = false;
            this.groupBox9.Text = "设置过滤";
            // 
            // textBox12
            // 
            this.textBox12.Location = new System.Drawing.Point(431, 55);
            this.textBox12.Name = "textBox12";
            this.textBox12.Size = new System.Drawing.Size(123, 21);
            this.textBox12.TabIndex = 21;
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox11
            // 
            this.textBox11.Location = new System.Drawing.Point(149, 61);
            this.textBox11.Name = "textBox11";
            this.textBox11.Size = new System.Drawing.Size(94, 21);
            this.textBox11.TabIndex = 20;
            this.textBox11.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // hexTextBox9
            // 
            this.hexTextBox9.Location = new System.Drawing.Point(132, 95);
            this.hexTextBox9.Name = "hexTextBox9";
            this.hexTextBox9.Size = new System.Drawing.Size(460, 21);
            this.hexTextBox9.TabIndex = 18;
            this.hexTextBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(5, 23);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(47, 12);
            this.label38.TabIndex = 15;
            this.label38.Text = "过滤ID:";
            // 
            // comboBox12
            // 
            this.comboBox12.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox12.DropDownWidth = 70;
            this.comboBox12.FormattingEnabled = true;
            this.comboBox12.Items.AddRange(new object[] {
            "Mask No.1",
            "Mask No.2",
            "Mask No.3",
            "Mask No.4",
            "Mask No.5"});
            this.comboBox12.Location = new System.Drawing.Point(58, 20);
            this.comboBox12.Name = "comboBox12";
            this.comboBox12.Size = new System.Drawing.Size(83, 20);
            this.comboBox12.TabIndex = 14;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(29, 98);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(47, 12);
            this.label39.TabIndex = 11;
            this.label39.Text = "过滤值:";
            // 
            // label71
            // 
            this.label71.AutoSize = true;
            this.label71.Location = new System.Drawing.Point(312, 61);
            this.label71.Name = "label71";
            this.label71.Size = new System.Drawing.Size(89, 12);
            this.label71.TabIndex = 10;
            this.label71.Text = "过滤长度(bit):";
            // 
            // label99
            // 
            this.label99.AutoSize = true;
            this.label99.Location = new System.Drawing.Point(29, 64);
            this.label99.Name = "label99";
            this.label99.Size = new System.Drawing.Size(89, 12);
            this.label99.TabIndex = 9;
            this.label99.Text = "起始地址(bit):";
            // 
            // label100
            // 
            this.label100.AutoSize = true;
            this.label100.Location = new System.Drawing.Point(439, 23);
            this.label100.Name = "label100";
            this.label100.Size = new System.Drawing.Size(65, 12);
            this.label100.TabIndex = 6;
            this.label100.Text = "过滤区域：";
            // 
            // label101
            // 
            this.label101.AutoSize = true;
            this.label101.Location = new System.Drawing.Point(296, 24);
            this.label101.Name = "label101";
            this.label101.Size = new System.Drawing.Size(59, 12);
            this.label101.TabIndex = 5;
            this.label101.Text = "过滤行为:";
            // 
            // label102
            // 
            this.label102.AutoSize = true;
            this.label102.Location = new System.Drawing.Point(147, 26);
            this.label102.Name = "label102";
            this.label102.Size = new System.Drawing.Size(71, 12);
            this.label102.TabIndex = 4;
            this.label102.Text = "Session ID:";
            // 
            // comboBox13
            // 
            this.comboBox13.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox13.FormattingEnabled = true;
            this.comboBox13.Items.AddRange(new object[] {
            "Reserve",
            "EPC",
            "TID",
            "USER"});
            this.comboBox13.Location = new System.Drawing.Point(510, 20);
            this.comboBox13.Name = "comboBox13";
            this.comboBox13.Size = new System.Drawing.Size(82, 20);
            this.comboBox13.TabIndex = 3;
            // 
            // comboBox14
            // 
            this.comboBox14.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox14.FormattingEnabled = true;
            this.comboBox14.Items.AddRange(new object[] {
            "00",
            "01",
            "02",
            "03",
            "04",
            "05",
            "06",
            "07"});
            this.comboBox14.Location = new System.Drawing.Point(361, 20);
            this.comboBox14.Name = "comboBox14";
            this.comboBox14.Size = new System.Drawing.Size(72, 20);
            this.comboBox14.TabIndex = 2;
            // 
            // comboBox15
            // 
            this.comboBox15.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox15.FormattingEnabled = true;
            this.comboBox15.Items.AddRange(new object[] {
            "S0",
            "S1",
            "S2",
            "S3",
            "SL"});
            this.comboBox15.Location = new System.Drawing.Point(218, 23);
            this.comboBox15.Name = "comboBox15";
            this.comboBox15.Size = new System.Drawing.Size(72, 20);
            this.comboBox15.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Corbel", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(626, 53);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(202, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "设置过滤";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // PagISO18000
            // 
            this.PagISO18000.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.PagISO18000.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PagISO18000.Controls.Add(this.btnClear);
            this.PagISO18000.Controls.Add(this.btnInventoryISO18000);
            this.PagISO18000.Controls.Add(this.ltvTagISO18000);
            this.PagISO18000.Controls.Add(this.gbISO1800LockQuery);
            this.PagISO18000.Controls.Add(this.gbISO1800ReadWrite);
            this.PagISO18000.Controls.Add(this.label41);
            this.PagISO18000.Controls.Add(this.htxtReadUID);
            this.PagISO18000.Location = new System.Drawing.Point(4, 22);
            this.PagISO18000.Name = "PagISO18000";
            this.PagISO18000.Size = new System.Drawing.Size(1010, 555);
            this.PagISO18000.TabIndex = 4;
            this.PagISO18000.Text = "ISO 18000-6B标签测试";
            // 
            // btnClear
            // 
            this.btnClear.Location = new System.Drawing.Point(887, 22);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(100, 28);
            this.btnClear.TabIndex = 10;
            this.btnClear.Text = "刷新界面";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // btnInventoryISO18000
            // 
            this.btnInventoryISO18000.Font = new System.Drawing.Font("SimSun", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnInventoryISO18000.ForeColor = System.Drawing.Color.Indigo;
            this.btnInventoryISO18000.Location = new System.Drawing.Point(8, 10);
            this.btnInventoryISO18000.Name = "btnInventoryISO18000";
            this.btnInventoryISO18000.Size = new System.Drawing.Size(120, 35);
            this.btnInventoryISO18000.TabIndex = 3;
            this.btnInventoryISO18000.Text = "开始盘存";
            this.btnInventoryISO18000.UseVisualStyleBackColor = true;
            this.btnInventoryISO18000.Click += new System.EventHandler(this.btnInventoryISO18000_Click);
            // 
            // ltvTagISO18000
            // 
            this.ltvTagISO18000.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader27,
            this.columnHeader25,
            this.columnHeader26,
            this.columnHeader28});
            this.ltvTagISO18000.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ltvTagISO18000.FullRowSelect = true;
            this.ltvTagISO18000.GridLines = true;
            this.ltvTagISO18000.HideSelection = false;
            this.ltvTagISO18000.Location = new System.Drawing.Point(3, 51);
            this.ltvTagISO18000.Name = "ltvTagISO18000";
            this.ltvTagISO18000.Size = new System.Drawing.Size(458, 502);
            this.ltvTagISO18000.TabIndex = 9;
            this.ltvTagISO18000.UseCompatibleStateImageBehavior = false;
            this.ltvTagISO18000.View = System.Windows.Forms.View.Details;
            this.ltvTagISO18000.Click += new System.EventHandler(this.ltvTagISO18000_Click);
            this.ltvTagISO18000.DoubleClick += new System.EventHandler(this.ltvTagISO18000_DoubleClick);
            // 
            // columnHeader27
            // 
            this.columnHeader27.Text = "序号";
            // 
            // columnHeader25
            // 
            this.columnHeader25.Text = "UID";
            this.columnHeader25.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader25.Width = 227;
            // 
            // columnHeader26
            // 
            this.columnHeader26.Text = "天线号";
            this.columnHeader26.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader26.Width = 77;
            // 
            // columnHeader28
            // 
            this.columnHeader28.Text = "次数";
            this.columnHeader28.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader28.Width = 75;
            // 
            // gbISO1800LockQuery
            // 
            this.gbISO1800LockQuery.Controls.Add(this.txtStatus);
            this.gbISO1800LockQuery.Controls.Add(this.htxtQueryAdd);
            this.gbISO1800LockQuery.Controls.Add(this.label46);
            this.gbISO1800LockQuery.Controls.Add(this.htxtLockAdd);
            this.gbISO1800LockQuery.Controls.Add(this.label47);
            this.gbISO1800LockQuery.Controls.Add(this.btnQueryTagISO18000);
            this.gbISO1800LockQuery.Controls.Add(this.btnLockTagISO18000);
            this.gbISO1800LockQuery.Location = new System.Drawing.Point(487, 467);
            this.gbISO1800LockQuery.Name = "gbISO1800LockQuery";
            this.gbISO1800LockQuery.Size = new System.Drawing.Size(515, 86);
            this.gbISO1800LockQuery.TabIndex = 7;
            this.gbISO1800LockQuery.TabStop = false;
            // 
            // txtStatus
            // 
            this.txtStatus.Location = new System.Drawing.Point(263, 58);
            this.txtStatus.Name = "txtStatus";
            this.txtStatus.ReadOnly = true;
            this.txtStatus.Size = new System.Drawing.Size(128, 21);
            this.txtStatus.TabIndex = 9;
            this.txtStatus.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // htxtQueryAdd
            // 
            this.htxtQueryAdd.Location = new System.Drawing.Point(210, 58);
            this.htxtQueryAdd.MaxLength = 2;
            this.htxtQueryAdd.Name = "htxtQueryAdd";
            this.htxtQueryAdd.Size = new System.Drawing.Size(39, 21);
            this.htxtQueryAdd.TabIndex = 8;
            this.htxtQueryAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(25, 62);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(173, 12);
            this.label46.TabIndex = 5;
            this.label46.Text = "查询永久写保护状态(HEX地址):";
            // 
            // htxtLockAdd
            // 
            this.htxtLockAdd.Location = new System.Drawing.Point(210, 25);
            this.htxtLockAdd.MaxLength = 2;
            this.htxtLockAdd.Name = "htxtLockAdd";
            this.htxtLockAdd.Size = new System.Drawing.Size(39, 21);
            this.htxtLockAdd.TabIndex = 8;
            this.htxtLockAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(73, 29);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(125, 12);
            this.label47.TabIndex = 5;
            this.label47.Text = "永久写保护(HEX地址):";
            // 
            // btnQueryTagISO18000
            // 
            this.btnQueryTagISO18000.Location = new System.Drawing.Point(400, 54);
            this.btnQueryTagISO18000.Name = "btnQueryTagISO18000";
            this.btnQueryTagISO18000.Size = new System.Drawing.Size(100, 28);
            this.btnQueryTagISO18000.TabIndex = 3;
            this.btnQueryTagISO18000.Text = "查询状态";
            this.btnQueryTagISO18000.UseVisualStyleBackColor = true;
            this.btnQueryTagISO18000.Click += new System.EventHandler(this.btnQueryTagISO18000_Click);
            // 
            // btnLockTagISO18000
            // 
            this.btnLockTagISO18000.Location = new System.Drawing.Point(400, 21);
            this.btnLockTagISO18000.Name = "btnLockTagISO18000";
            this.btnLockTagISO18000.Size = new System.Drawing.Size(100, 28);
            this.btnLockTagISO18000.TabIndex = 3;
            this.btnLockTagISO18000.Text = "永久写保护";
            this.btnLockTagISO18000.UseVisualStyleBackColor = true;
            this.btnLockTagISO18000.Click += new System.EventHandler(this.btnLockTagISO18000_Click);
            // 
            // gbISO1800ReadWrite
            // 
            this.gbISO1800ReadWrite.Controls.Add(this.txtLoopTimes);
            this.gbISO1800ReadWrite.Controls.Add(this.label44);
            this.gbISO1800ReadWrite.Controls.Add(this.txtLoop);
            this.gbISO1800ReadWrite.Controls.Add(this.label40);
            this.gbISO1800ReadWrite.Controls.Add(this.htxtWriteData18000);
            this.gbISO1800ReadWrite.Controls.Add(this.txtWriteLength);
            this.gbISO1800ReadWrite.Controls.Add(this.htxtReadData18000);
            this.gbISO1800ReadWrite.Controls.Add(this.label45);
            this.gbISO1800ReadWrite.Controls.Add(this.btnWriteTagISO18000);
            this.gbISO1800ReadWrite.Controls.Add(this.label51);
            this.gbISO1800ReadWrite.Controls.Add(this.label52);
            this.gbISO1800ReadWrite.Controls.Add(this.txtReadLength);
            this.gbISO1800ReadWrite.Controls.Add(this.htxtWriteStartAdd);
            this.gbISO1800ReadWrite.Controls.Add(this.label50);
            this.gbISO1800ReadWrite.Controls.Add(this.htxtReadStartAdd);
            this.gbISO1800ReadWrite.Controls.Add(this.label42);
            this.gbISO1800ReadWrite.Controls.Add(this.label43);
            this.gbISO1800ReadWrite.Controls.Add(this.btnReadTagISO18000);
            this.gbISO1800ReadWrite.Location = new System.Drawing.Point(487, 56);
            this.gbISO1800ReadWrite.Name = "gbISO1800ReadWrite";
            this.gbISO1800ReadWrite.Size = new System.Drawing.Size(515, 411);
            this.gbISO1800ReadWrite.TabIndex = 4;
            this.gbISO1800ReadWrite.TabStop = false;
            this.gbISO1800ReadWrite.Text = "任意长度读写数据";
            // 
            // txtLoopTimes
            // 
            this.txtLoopTimes.Location = new System.Drawing.Point(274, 219);
            this.txtLoopTimes.Name = "txtLoopTimes";
            this.txtLoopTimes.ReadOnly = true;
            this.txtLoopTimes.Size = new System.Drawing.Size(41, 21);
            this.txtLoopTimes.TabIndex = 15;
            this.txtLoopTimes.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(179, 223);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(83, 12);
            this.label44.TabIndex = 14;
            this.label44.Text = "成功写入(次):";
            // 
            // txtLoop
            // 
            this.txtLoop.Location = new System.Drawing.Point(117, 219);
            this.txtLoop.Name = "txtLoop";
            this.txtLoop.Size = new System.Drawing.Size(39, 21);
            this.txtLoop.TabIndex = 13;
            this.txtLoop.Text = "1";
            this.txtLoop.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(22, 223);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(83, 12);
            this.label40.TabIndex = 12;
            this.label40.Text = "循环写入(次):";
            // 
            // htxtWriteData18000
            // 
            this.htxtWriteData18000.Location = new System.Drawing.Point(117, 247);
            this.htxtWriteData18000.Multiline = true;
            this.htxtWriteData18000.Name = "htxtWriteData18000";
            this.htxtWriteData18000.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.htxtWriteData18000.Size = new System.Drawing.Size(383, 158);
            this.htxtWriteData18000.TabIndex = 9;
            // 
            // txtWriteLength
            // 
            this.txtWriteLength.Location = new System.Drawing.Point(274, 192);
            this.txtWriteLength.MaxLength = 2;
            this.txtWriteLength.Name = "txtWriteLength";
            this.txtWriteLength.ReadOnly = true;
            this.txtWriteLength.Size = new System.Drawing.Size(41, 21);
            this.txtWriteLength.TabIndex = 11;
            this.txtWriteLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // htxtReadData18000
            // 
            this.htxtReadData18000.Location = new System.Drawing.Point(117, 49);
            this.htxtReadData18000.Multiline = true;
            this.htxtReadData18000.Name = "htxtReadData18000";
            this.htxtReadData18000.ReadOnly = true;
            this.htxtReadData18000.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.htxtReadData18000.Size = new System.Drawing.Size(383, 133);
            this.htxtReadData18000.TabIndex = 11;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(16, 250);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(89, 12);
            this.label45.TabIndex = 6;
            this.label45.Text = "写入数据(HEX):";
            // 
            // btnWriteTagISO18000
            // 
            this.btnWriteTagISO18000.Location = new System.Drawing.Point(400, 215);
            this.btnWriteTagISO18000.Name = "btnWriteTagISO18000";
            this.btnWriteTagISO18000.Size = new System.Drawing.Size(100, 28);
            this.btnWriteTagISO18000.TabIndex = 3;
            this.btnWriteTagISO18000.Text = "写数据";
            this.btnWriteTagISO18000.UseVisualStyleBackColor = true;
            this.btnWriteTagISO18000.Click += new System.EventHandler(this.btnWriteTagISO18000_Click);
            // 
            // label51
            // 
            this.label51.AutoSize = true;
            this.label51.Location = new System.Drawing.Point(173, 196);
            this.label51.Name = "label51";
            this.label51.Size = new System.Drawing.Size(89, 12);
            this.label51.TabIndex = 10;
            this.label51.Text = "写入长度(HEX):";
            // 
            // label52
            // 
            this.label52.AutoSize = true;
            this.label52.Location = new System.Drawing.Point(16, 52);
            this.label52.Name = "label52";
            this.label52.Size = new System.Drawing.Size(89, 12);
            this.label52.TabIndex = 10;
            this.label52.Text = "读取数据(HEX):";
            // 
            // txtReadLength
            // 
            this.txtReadLength.Location = new System.Drawing.Point(274, 19);
            this.txtReadLength.MaxLength = 2;
            this.txtReadLength.Name = "txtReadLength";
            this.txtReadLength.Size = new System.Drawing.Size(41, 21);
            this.txtReadLength.TabIndex = 9;
            this.txtReadLength.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // htxtWriteStartAdd
            // 
            this.htxtWriteStartAdd.Location = new System.Drawing.Point(117, 192);
            this.htxtWriteStartAdd.MaxLength = 2;
            this.htxtWriteStartAdd.Name = "htxtWriteStartAdd";
            this.htxtWriteStartAdd.Size = new System.Drawing.Size(39, 21);
            this.htxtWriteStartAdd.TabIndex = 8;
            this.htxtWriteStartAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label50
            // 
            this.label50.AutoSize = true;
            this.label50.Location = new System.Drawing.Point(173, 23);
            this.label50.Name = "label50";
            this.label50.Size = new System.Drawing.Size(89, 12);
            this.label50.TabIndex = 8;
            this.label50.Text = "读取长度(HEX):";
            // 
            // htxtReadStartAdd
            // 
            this.htxtReadStartAdd.Location = new System.Drawing.Point(117, 19);
            this.htxtReadStartAdd.MaxLength = 2;
            this.htxtReadStartAdd.Name = "htxtReadStartAdd";
            this.htxtReadStartAdd.Size = new System.Drawing.Size(39, 21);
            this.htxtReadStartAdd.TabIndex = 7;
            this.htxtReadStartAdd.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(16, 23);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(89, 12);
            this.label42.TabIndex = 5;
            this.label42.Text = "起始地址(HEX):";
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(16, 196);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(89, 12);
            this.label43.TabIndex = 5;
            this.label43.Text = "起始地址(HEX):";
            // 
            // btnReadTagISO18000
            // 
            this.btnReadTagISO18000.Location = new System.Drawing.Point(400, 15);
            this.btnReadTagISO18000.Name = "btnReadTagISO18000";
            this.btnReadTagISO18000.Size = new System.Drawing.Size(100, 28);
            this.btnReadTagISO18000.TabIndex = 3;
            this.btnReadTagISO18000.Text = "读数据";
            this.btnReadTagISO18000.UseVisualStyleBackColor = true;
            this.btnReadTagISO18000.Click += new System.EventHandler(this.btnReadTagISO18000_Click);
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(503, 30);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(89, 12);
            this.label41.TabIndex = 4;
            this.label41.Text = "当前选择的UID:";
            // 
            // htxtReadUID
            // 
            this.htxtReadUID.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.htxtReadUID.Location = new System.Drawing.Point(604, 27);
            this.htxtReadUID.Name = "htxtReadUID";
            this.htxtReadUID.ReadOnly = true;
            this.htxtReadUID.Size = new System.Drawing.Size(195, 21);
            this.htxtReadUID.TabIndex = 6;
            this.htxtReadUID.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PagTranDataLog
            // 
            this.PagTranDataLog.BackColor = System.Drawing.Color.WhiteSmoke;
            this.PagTranDataLog.Controls.Add(this.btnSaveData);
            this.PagTranDataLog.Controls.Add(this.gbCmdManual);
            this.PagTranDataLog.Controls.Add(this.lrtxtDataTran);
            this.PagTranDataLog.Location = new System.Drawing.Point(4, 22);
            this.PagTranDataLog.Name = "PagTranDataLog";
            this.PagTranDataLog.Size = new System.Drawing.Size(1010, 555);
            this.PagTranDataLog.TabIndex = 2;
            this.PagTranDataLog.Text = "串口监控数据";
            this.PagTranDataLog.UseVisualStyleBackColor = true;
            // 
            // btnSaveData
            // 
            this.btnSaveData.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnSaveData.Location = new System.Drawing.Point(11, 473);
            this.btnSaveData.Name = "btnSaveData";
            this.btnSaveData.Size = new System.Drawing.Size(140, 23);
            this.btnSaveData.TabIndex = 10;
            this.btnSaveData.Text = "保存数据到txt文件";
            this.btnSaveData.UseVisualStyleBackColor = true;
            this.btnSaveData.Click += new System.EventHandler(this.btnSaveData_Click);
            // 
            // gbCmdManual
            // 
            this.gbCmdManual.Controls.Add(this.label16);
            this.gbCmdManual.Controls.Add(this.htxtSendData);
            this.gbCmdManual.Controls.Add(this.btnClearData);
            this.gbCmdManual.Controls.Add(this.label17);
            this.gbCmdManual.Controls.Add(this.btnSendData);
            this.gbCmdManual.Controls.Add(this.htxtCheckData);
            this.gbCmdManual.Location = new System.Drawing.Point(3, 501);
            this.gbCmdManual.Name = "gbCmdManual";
            this.gbCmdManual.Size = new System.Drawing.Size(1002, 51);
            this.gbCmdManual.TabIndex = 8;
            this.gbCmdManual.TabStop = false;
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(6, 19);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(83, 12);
            this.label16.TabIndex = 1;
            this.label16.Text = "手工输入数据:";
            // 
            // htxtSendData
            // 
            this.htxtSendData.Location = new System.Drawing.Point(95, 16);
            this.htxtSendData.Name = "htxtSendData";
            this.htxtSendData.Size = new System.Drawing.Size(515, 21);
            this.htxtSendData.TabIndex = 2;
            this.htxtSendData.Leave += new System.EventHandler(this.htxtSendData_Leave);
            // 
            // btnClearData
            // 
            this.btnClearData.Location = new System.Drawing.Point(906, 14);
            this.btnClearData.Name = "btnClearData";
            this.btnClearData.Size = new System.Drawing.Size(90, 23);
            this.btnClearData.TabIndex = 6;
            this.btnClearData.Text = "清空数据";
            this.btnClearData.UseVisualStyleBackColor = true;
            this.btnClearData.Click += new System.EventHandler(this.btnClearData_Click);
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(632, 19);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(47, 12);
            this.label17.TabIndex = 3;
            this.label17.Text = "校验位:";
            // 
            // btnSendData
            // 
            this.btnSendData.Location = new System.Drawing.Point(810, 14);
            this.btnSendData.Name = "btnSendData";
            this.btnSendData.Size = new System.Drawing.Size(90, 23);
            this.btnSendData.TabIndex = 5;
            this.btnSendData.Text = "发送数据";
            this.btnSendData.UseVisualStyleBackColor = true;
            this.btnSendData.Click += new System.EventHandler(this.btnSendData_Click);
            // 
            // htxtCheckData
            // 
            this.htxtCheckData.Location = new System.Drawing.Point(685, 16);
            this.htxtCheckData.Name = "htxtCheckData";
            this.htxtCheckData.ReadOnly = true;
            this.htxtCheckData.Size = new System.Drawing.Size(47, 21);
            this.htxtCheckData.TabIndex = 4;
            // 
            // lrtxtDataTran
            // 
            this.lrtxtDataTran.Dock = System.Windows.Forms.DockStyle.Top;
            this.lrtxtDataTran.Location = new System.Drawing.Point(0, 0);
            this.lrtxtDataTran.Name = "lrtxtDataTran";
            this.lrtxtDataTran.Size = new System.Drawing.Size(1010, 471);
            this.lrtxtDataTran.TabIndex = 0;
            this.lrtxtDataTran.Text = "";
            this.lrtxtDataTran.DoubleClick += new System.EventHandler(this.lrtxtDataTran_DoubleClick);
            // 
            // net_configure_tabPage
            // 
            this.net_configure_tabPage.Controls.Add(this.port_setting_tabcontrol);
            this.net_configure_tabPage.Controls.Add(this.net_port_config_tool_linkLabel);
            this.net_configure_tabPage.Controls.Add(this.label165);
            this.net_configure_tabPage.Controls.Add(this.label164);
            this.net_configure_tabPage.Controls.Add(this.old_net_port_link);
            this.net_configure_tabPage.Controls.Add(this.label163);
            this.net_configure_tabPage.Controls.Add(this.net_clear_btn);
            this.net_configure_tabPage.Controls.Add(this.net_base_settings_gb);
            this.net_configure_tabPage.Controls.Add(this.dev_dgv);
            this.net_configure_tabPage.Controls.Add(this.label159);
            this.net_configure_tabPage.Controls.Add(this.net_card_combox);
            this.net_configure_tabPage.Controls.Add(this.groupBox30);
            this.net_configure_tabPage.Controls.Add(this.net_reset_btn);
            this.net_configure_tabPage.Controls.Add(this.net_setCfg_btn);
            this.net_configure_tabPage.Controls.Add(this.net_getCfg_btn);
            this.net_configure_tabPage.Controls.Add(this.net_search_btn);
            this.net_configure_tabPage.Controls.Add(this.net_refresh_netcard_btn);
            this.net_configure_tabPage.Location = new System.Drawing.Point(4, 22);
            this.net_configure_tabPage.Name = "net_configure_tabPage";
            this.net_configure_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.net_configure_tabPage.Size = new System.Drawing.Size(1010, 555);
            this.net_configure_tabPage.TabIndex = 6;
            this.net_configure_tabPage.Text = "网口配置";
            this.net_configure_tabPage.UseVisualStyleBackColor = true;
            // 
            // port_setting_tabcontrol
            // 
            this.port_setting_tabcontrol.Controls.Add(this.net_port_0_tabPage);
            this.port_setting_tabcontrol.Controls.Add(this.net_port_1_tabPage);
            this.port_setting_tabcontrol.Location = new System.Drawing.Point(781, 44);
            this.port_setting_tabcontrol.Name = "port_setting_tabcontrol";
            this.port_setting_tabcontrol.SelectedIndex = 0;
            this.port_setting_tabcontrol.Size = new System.Drawing.Size(226, 318);
            this.port_setting_tabcontrol.TabIndex = 35;
            // 
            // net_port_0_tabPage
            // 
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_enable_cb);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_rand_port_flag_cb);
            this.net_port_0_tabPage.Controls.Add(this.label169);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_parity_bit_cbo);
            this.net_port_0_tabPage.Controls.Add(this.label168);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_stopbits_cbo);
            this.net_port_0_tabPage.Controls.Add(this.label167);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_databits_cbo);
            this.net_port_0_tabPage.Controls.Add(this.label166);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_baudrate_cbo);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_dest_port_tb);
            this.net_port_0_tabPage.Controls.Add(this.label155);
            this.net_port_0_tabPage.Controls.Add(this.label129);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_dest_ip_tb);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_ip_domain_select_cbo);
            this.net_port_0_tabPage.Controls.Add(this.label128);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_local_net_port_tb);
            this.net_port_0_tabPage.Controls.Add(this.label126);
            this.net_port_0_tabPage.Controls.Add(this.label125);
            this.net_port_0_tabPage.Controls.Add(this.net_port_1_net_mode_cbo);
            this.net_port_0_tabPage.Location = new System.Drawing.Point(4, 22);
            this.net_port_0_tabPage.Name = "net_port_0_tabPage";
            this.net_port_0_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.net_port_0_tabPage.Size = new System.Drawing.Size(218, 292);
            this.net_port_0_tabPage.TabIndex = 0;
            this.net_port_0_tabPage.Text = "端口1";
            this.net_port_0_tabPage.UseVisualStyleBackColor = true;
            // 
            // net_port_1_enable_cb
            // 
            this.net_port_1_enable_cb.AutoSize = true;
            this.net_port_1_enable_cb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.net_port_1_enable_cb.Location = new System.Drawing.Point(4, 4);
            this.net_port_1_enable_cb.Name = "net_port_1_enable_cb";
            this.net_port_1_enable_cb.Size = new System.Drawing.Size(78, 16);
            this.net_port_1_enable_cb.TabIndex = 72;
            this.net_port_1_enable_cb.Text = "启用端口1";
            this.net_port_1_enable_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.net_port_1_enable_cb.UseVisualStyleBackColor = true;
            // 
            // net_port_1_rand_port_flag_cb
            // 
            this.net_port_1_rand_port_flag_cb.AutoSize = true;
            this.net_port_1_rand_port_flag_cb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.net_port_1_rand_port_flag_cb.Location = new System.Drawing.Point(77, 51);
            this.net_port_1_rand_port_flag_cb.Name = "net_port_1_rand_port_flag_cb";
            this.net_port_1_rand_port_flag_cb.Size = new System.Drawing.Size(48, 16);
            this.net_port_1_rand_port_flag_cb.TabIndex = 70;
            this.net_port_1_rand_port_flag_cb.Text = "随机";
            this.net_port_1_rand_port_flag_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.net_port_1_rand_port_flag_cb.UseVisualStyleBackColor = true;
            // 
            // label169
            // 
            this.label169.AutoSize = true;
            this.label169.Location = new System.Drawing.Point(6, 267);
            this.label169.Name = "label169";
            this.label169.Size = new System.Drawing.Size(77, 12);
            this.label169.TabIndex = 68;
            this.label169.Text = "串口校验位：";
            // 
            // net_port_1_parity_bit_cbo
            // 
            this.net_port_1_parity_bit_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_parity_bit_cbo.FormattingEnabled = true;
            this.net_port_1_parity_bit_cbo.Location = new System.Drawing.Point(88, 264);
            this.net_port_1_parity_bit_cbo.Name = "net_port_1_parity_bit_cbo";
            this.net_port_1_parity_bit_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_1_parity_bit_cbo.TabIndex = 69;
            // 
            // label168
            // 
            this.label168.AutoSize = true;
            this.label168.Location = new System.Drawing.Point(6, 242);
            this.label168.Name = "label168";
            this.label168.Size = new System.Drawing.Size(77, 12);
            this.label168.TabIndex = 66;
            this.label168.Text = "串口停止位：";
            // 
            // net_port_1_stopbits_cbo
            // 
            this.net_port_1_stopbits_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_stopbits_cbo.FormattingEnabled = true;
            this.net_port_1_stopbits_cbo.Location = new System.Drawing.Point(88, 239);
            this.net_port_1_stopbits_cbo.Name = "net_port_1_stopbits_cbo";
            this.net_port_1_stopbits_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_1_stopbits_cbo.TabIndex = 67;
            // 
            // label167
            // 
            this.label167.AutoSize = true;
            this.label167.Location = new System.Drawing.Point(6, 213);
            this.label167.Name = "label167";
            this.label167.Size = new System.Drawing.Size(77, 12);
            this.label167.TabIndex = 64;
            this.label167.Text = "串口数据位：";
            // 
            // net_port_1_databits_cbo
            // 
            this.net_port_1_databits_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_databits_cbo.FormattingEnabled = true;
            this.net_port_1_databits_cbo.Location = new System.Drawing.Point(88, 210);
            this.net_port_1_databits_cbo.Name = "net_port_1_databits_cbo";
            this.net_port_1_databits_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_1_databits_cbo.TabIndex = 65;
            // 
            // label166
            // 
            this.label166.AutoSize = true;
            this.label166.Location = new System.Drawing.Point(6, 186);
            this.label166.Name = "label166";
            this.label166.Size = new System.Drawing.Size(77, 12);
            this.label166.TabIndex = 62;
            this.label166.Text = "串口波特率：";
            // 
            // net_port_1_baudrate_cbo
            // 
            this.net_port_1_baudrate_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_baudrate_cbo.FormattingEnabled = true;
            this.net_port_1_baudrate_cbo.Location = new System.Drawing.Point(88, 183);
            this.net_port_1_baudrate_cbo.Name = "net_port_1_baudrate_cbo";
            this.net_port_1_baudrate_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_1_baudrate_cbo.TabIndex = 63;
            // 
            // net_port_1_dest_port_tb
            // 
            this.net_port_1_dest_port_tb.Location = new System.Drawing.Point(77, 141);
            this.net_port_1_dest_port_tb.Name = "net_port_1_dest_port_tb";
            this.net_port_1_dest_port_tb.Size = new System.Drawing.Size(132, 21);
            this.net_port_1_dest_port_tb.TabIndex = 61;
            // 
            // label155
            // 
            this.label155.AutoSize = true;
            this.label155.Location = new System.Drawing.Point(6, 144);
            this.label155.Name = "label155";
            this.label155.Size = new System.Drawing.Size(65, 12);
            this.label155.TabIndex = 60;
            this.label155.Text = "目标端口：";
            // 
            // label129
            // 
            this.label129.AutoSize = true;
            this.label129.Location = new System.Drawing.Point(6, 111);
            this.label129.Name = "label129";
            this.label129.Size = new System.Drawing.Size(53, 12);
            this.label129.TabIndex = 59;
            this.label129.Text = "目的IP：";
            // 
            // net_port_1_dest_ip_tb
            // 
            this.net_port_1_dest_ip_tb.Location = new System.Drawing.Point(77, 108);
            this.net_port_1_dest_ip_tb.Name = "net_port_1_dest_ip_tb";
            this.net_port_1_dest_ip_tb.Size = new System.Drawing.Size(132, 21);
            this.net_port_1_dest_ip_tb.TabIndex = 58;
            // 
            // net_port_1_ip_domain_select_cbo
            // 
            this.net_port_1_ip_domain_select_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_ip_domain_select_cbo.FormattingEnabled = true;
            this.net_port_1_ip_domain_select_cbo.Items.AddRange(new object[] {
            "IP"});
            this.net_port_1_ip_domain_select_cbo.Location = new System.Drawing.Point(88, 76);
            this.net_port_1_ip_domain_select_cbo.Name = "net_port_1_ip_domain_select_cbo";
            this.net_port_1_ip_domain_select_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_1_ip_domain_select_cbo.TabIndex = 57;
            // 
            // label128
            // 
            this.label128.AutoSize = true;
            this.label128.Location = new System.Drawing.Point(6, 82);
            this.label128.Name = "label128";
            this.label128.Size = new System.Drawing.Size(83, 12);
            this.label128.TabIndex = 56;
            this.label128.Text = "目的IP/域名：";
            // 
            // net_port_1_local_net_port_tb
            // 
            this.net_port_1_local_net_port_tb.Location = new System.Drawing.Point(143, 49);
            this.net_port_1_local_net_port_tb.Name = "net_port_1_local_net_port_tb";
            this.net_port_1_local_net_port_tb.Size = new System.Drawing.Size(66, 21);
            this.net_port_1_local_net_port_tb.TabIndex = 52;
            this.net_port_1_local_net_port_tb.Text = "0";
            // 
            // label126
            // 
            this.label126.AutoSize = true;
            this.label126.Location = new System.Drawing.Point(6, 52);
            this.label126.Name = "label126";
            this.label126.Size = new System.Drawing.Size(65, 12);
            this.label126.TabIndex = 55;
            this.label126.Text = "本地端口：";
            // 
            // label125
            // 
            this.label125.AutoSize = true;
            this.label125.Location = new System.Drawing.Point(6, 28);
            this.label125.Name = "label125";
            this.label125.Size = new System.Drawing.Size(65, 12);
            this.label125.TabIndex = 53;
            this.label125.Text = "网络模式：";
            // 
            // net_port_1_net_mode_cbo
            // 
            this.net_port_1_net_mode_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_1_net_mode_cbo.FormattingEnabled = true;
            this.net_port_1_net_mode_cbo.Location = new System.Drawing.Point(77, 25);
            this.net_port_1_net_mode_cbo.Name = "net_port_1_net_mode_cbo";
            this.net_port_1_net_mode_cbo.Size = new System.Drawing.Size(132, 20);
            this.net_port_1_net_mode_cbo.TabIndex = 54;
            this.net_port_1_net_mode_cbo.SelectedIndexChanged += new System.EventHandler(this.net_port_net_mode_cb_SelectedIndexChanged);
            // 
            // net_port_1_tabPage
            // 
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_enable_cb);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_rand_port_flag_cb);
            this.net_port_1_tabPage.Controls.Add(this.label170);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_parity_bit_cbo);
            this.net_port_1_tabPage.Controls.Add(this.label171);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_stopbits_cbo);
            this.net_port_1_tabPage.Controls.Add(this.label172);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_databits_cbo);
            this.net_port_1_tabPage.Controls.Add(this.label173);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_baudrate_cbo);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_dest_port_tb);
            this.net_port_1_tabPage.Controls.Add(this.label174);
            this.net_port_1_tabPage.Controls.Add(this.label175);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_dest_ip_tb);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_ip_domain_select_cbo);
            this.net_port_1_tabPage.Controls.Add(this.label176);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_local_net_port_tb);
            this.net_port_1_tabPage.Controls.Add(this.label177);
            this.net_port_1_tabPage.Controls.Add(this.label178);
            this.net_port_1_tabPage.Controls.Add(this.net_port_2_net_mode_cbo);
            this.net_port_1_tabPage.Location = new System.Drawing.Point(4, 22);
            this.net_port_1_tabPage.Name = "net_port_1_tabPage";
            this.net_port_1_tabPage.Padding = new System.Windows.Forms.Padding(3);
            this.net_port_1_tabPage.Size = new System.Drawing.Size(218, 292);
            this.net_port_1_tabPage.TabIndex = 1;
            this.net_port_1_tabPage.Text = "端口2";
            this.net_port_1_tabPage.UseVisualStyleBackColor = true;
            // 
            // net_port_2_enable_cb
            // 
            this.net_port_2_enable_cb.AutoSize = true;
            this.net_port_2_enable_cb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.net_port_2_enable_cb.Location = new System.Drawing.Point(4, 4);
            this.net_port_2_enable_cb.Name = "net_port_2_enable_cb";
            this.net_port_2_enable_cb.Size = new System.Drawing.Size(78, 16);
            this.net_port_2_enable_cb.TabIndex = 71;
            this.net_port_2_enable_cb.Text = "启用端口2";
            this.net_port_2_enable_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.net_port_2_enable_cb.UseVisualStyleBackColor = true;
            // 
            // net_port_2_rand_port_flag_cb
            // 
            this.net_port_2_rand_port_flag_cb.AutoSize = true;
            this.net_port_2_rand_port_flag_cb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.net_port_2_rand_port_flag_cb.Location = new System.Drawing.Point(77, 51);
            this.net_port_2_rand_port_flag_cb.Name = "net_port_2_rand_port_flag_cb";
            this.net_port_2_rand_port_flag_cb.Size = new System.Drawing.Size(48, 16);
            this.net_port_2_rand_port_flag_cb.TabIndex = 70;
            this.net_port_2_rand_port_flag_cb.Text = "随机";
            this.net_port_2_rand_port_flag_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.net_port_2_rand_port_flag_cb.UseVisualStyleBackColor = true;
            // 
            // label170
            // 
            this.label170.AutoSize = true;
            this.label170.Location = new System.Drawing.Point(6, 267);
            this.label170.Name = "label170";
            this.label170.Size = new System.Drawing.Size(77, 12);
            this.label170.TabIndex = 68;
            this.label170.Text = "串口校验位：";
            // 
            // net_port_2_parity_bit_cbo
            // 
            this.net_port_2_parity_bit_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_parity_bit_cbo.FormattingEnabled = true;
            this.net_port_2_parity_bit_cbo.Location = new System.Drawing.Point(88, 264);
            this.net_port_2_parity_bit_cbo.Name = "net_port_2_parity_bit_cbo";
            this.net_port_2_parity_bit_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_2_parity_bit_cbo.TabIndex = 69;
            // 
            // label171
            // 
            this.label171.AutoSize = true;
            this.label171.Location = new System.Drawing.Point(6, 242);
            this.label171.Name = "label171";
            this.label171.Size = new System.Drawing.Size(77, 12);
            this.label171.TabIndex = 66;
            this.label171.Text = "串口停止位：";
            // 
            // net_port_2_stopbits_cbo
            // 
            this.net_port_2_stopbits_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_stopbits_cbo.FormattingEnabled = true;
            this.net_port_2_stopbits_cbo.Location = new System.Drawing.Point(88, 239);
            this.net_port_2_stopbits_cbo.Name = "net_port_2_stopbits_cbo";
            this.net_port_2_stopbits_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_2_stopbits_cbo.TabIndex = 67;
            // 
            // label172
            // 
            this.label172.AutoSize = true;
            this.label172.Location = new System.Drawing.Point(6, 213);
            this.label172.Name = "label172";
            this.label172.Size = new System.Drawing.Size(77, 12);
            this.label172.TabIndex = 64;
            this.label172.Text = "串口数据位：";
            // 
            // net_port_2_databits_cbo
            // 
            this.net_port_2_databits_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_databits_cbo.FormattingEnabled = true;
            this.net_port_2_databits_cbo.Location = new System.Drawing.Point(88, 210);
            this.net_port_2_databits_cbo.Name = "net_port_2_databits_cbo";
            this.net_port_2_databits_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_2_databits_cbo.TabIndex = 65;
            // 
            // label173
            // 
            this.label173.AutoSize = true;
            this.label173.Location = new System.Drawing.Point(6, 186);
            this.label173.Name = "label173";
            this.label173.Size = new System.Drawing.Size(77, 12);
            this.label173.TabIndex = 62;
            this.label173.Text = "串口波特率：";
            // 
            // net_port_2_baudrate_cbo
            // 
            this.net_port_2_baudrate_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_baudrate_cbo.FormattingEnabled = true;
            this.net_port_2_baudrate_cbo.Location = new System.Drawing.Point(88, 183);
            this.net_port_2_baudrate_cbo.Name = "net_port_2_baudrate_cbo";
            this.net_port_2_baudrate_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_2_baudrate_cbo.TabIndex = 63;
            // 
            // net_port_2_dest_port_tb
            // 
            this.net_port_2_dest_port_tb.Location = new System.Drawing.Point(77, 141);
            this.net_port_2_dest_port_tb.Name = "net_port_2_dest_port_tb";
            this.net_port_2_dest_port_tb.Size = new System.Drawing.Size(132, 21);
            this.net_port_2_dest_port_tb.TabIndex = 61;
            // 
            // label174
            // 
            this.label174.AutoSize = true;
            this.label174.Location = new System.Drawing.Point(6, 144);
            this.label174.Name = "label174";
            this.label174.Size = new System.Drawing.Size(65, 12);
            this.label174.TabIndex = 60;
            this.label174.Text = "目标端口：";
            // 
            // label175
            // 
            this.label175.AutoSize = true;
            this.label175.Location = new System.Drawing.Point(6, 111);
            this.label175.Name = "label175";
            this.label175.Size = new System.Drawing.Size(53, 12);
            this.label175.TabIndex = 59;
            this.label175.Text = "目的IP：";
            // 
            // net_port_2_dest_ip_tb
            // 
            this.net_port_2_dest_ip_tb.Location = new System.Drawing.Point(77, 108);
            this.net_port_2_dest_ip_tb.Name = "net_port_2_dest_ip_tb";
            this.net_port_2_dest_ip_tb.Size = new System.Drawing.Size(132, 21);
            this.net_port_2_dest_ip_tb.TabIndex = 58;
            // 
            // net_port_2_ip_domain_select_cbo
            // 
            this.net_port_2_ip_domain_select_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_ip_domain_select_cbo.FormattingEnabled = true;
            this.net_port_2_ip_domain_select_cbo.Items.AddRange(new object[] {
            "IP"});
            this.net_port_2_ip_domain_select_cbo.Location = new System.Drawing.Point(88, 76);
            this.net_port_2_ip_domain_select_cbo.Name = "net_port_2_ip_domain_select_cbo";
            this.net_port_2_ip_domain_select_cbo.Size = new System.Drawing.Size(121, 20);
            this.net_port_2_ip_domain_select_cbo.TabIndex = 57;
            // 
            // label176
            // 
            this.label176.AutoSize = true;
            this.label176.Location = new System.Drawing.Point(6, 82);
            this.label176.Name = "label176";
            this.label176.Size = new System.Drawing.Size(83, 12);
            this.label176.TabIndex = 56;
            this.label176.Text = "目的IP/域名：";
            // 
            // net_port_2_local_net_port_tb
            // 
            this.net_port_2_local_net_port_tb.Location = new System.Drawing.Point(143, 49);
            this.net_port_2_local_net_port_tb.Name = "net_port_2_local_net_port_tb";
            this.net_port_2_local_net_port_tb.Size = new System.Drawing.Size(66, 21);
            this.net_port_2_local_net_port_tb.TabIndex = 52;
            this.net_port_2_local_net_port_tb.Text = "0";
            // 
            // label177
            // 
            this.label177.AutoSize = true;
            this.label177.Location = new System.Drawing.Point(6, 52);
            this.label177.Name = "label177";
            this.label177.Size = new System.Drawing.Size(65, 12);
            this.label177.TabIndex = 55;
            this.label177.Text = "本地端口：";
            // 
            // label178
            // 
            this.label178.AutoSize = true;
            this.label178.Location = new System.Drawing.Point(6, 28);
            this.label178.Name = "label178";
            this.label178.Size = new System.Drawing.Size(65, 12);
            this.label178.TabIndex = 53;
            this.label178.Text = "网络模式：";
            // 
            // net_port_2_net_mode_cbo
            // 
            this.net_port_2_net_mode_cbo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_port_2_net_mode_cbo.FormattingEnabled = true;
            this.net_port_2_net_mode_cbo.Location = new System.Drawing.Point(77, 25);
            this.net_port_2_net_mode_cbo.Name = "net_port_2_net_mode_cbo";
            this.net_port_2_net_mode_cbo.Size = new System.Drawing.Size(132, 20);
            this.net_port_2_net_mode_cbo.TabIndex = 54;
            this.net_port_2_net_mode_cbo.SelectedIndexChanged += new System.EventHandler(this.net_port_1_net_mode_cb_SelectedIndexChanged);
            // 
            // net_port_config_tool_linkLabel
            // 
            this.net_port_config_tool_linkLabel.AutoSize = true;
            this.net_port_config_tool_linkLabel.Location = new System.Drawing.Point(286, 391);
            this.net_port_config_tool_linkLabel.Name = "net_port_config_tool_linkLabel";
            this.net_port_config_tool_linkLabel.Size = new System.Drawing.Size(125, 12);
            this.net_port_config_tool_linkLabel.TabIndex = 33;
            this.net_port_config_tool_linkLabel.TabStop = true;
            this.net_port_config_tool_linkLabel.Text = "专用网络配置工具链接";
            this.net_port_config_tool_linkLabel.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.net_port_config_tool_linkLabel_LinkClicked);
            // 
            // label165
            // 
            this.label165.AutoSize = true;
            this.label165.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label165.Location = new System.Drawing.Point(41, 391);
            this.label165.Name = "label165";
            this.label165.Size = new System.Drawing.Size(239, 12);
            this.label165.TabIndex = 32;
            this.label165.Text = "（1）如需配置Client，请使用专用网络工具";
            // 
            // label164
            // 
            this.label164.AutoSize = true;
            this.label164.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label164.Location = new System.Drawing.Point(41, 418);
            this.label164.Name = "label164";
            this.label164.Size = new System.Drawing.Size(407, 12);
            this.label164.TabIndex = 31;
            this.label164.Text = "（2）旧版Neport网口此界面暂时无法搜索，模块配置请使用专用配置工具。";
            // 
            // old_net_port_link
            // 
            this.old_net_port_link.AutoSize = true;
            this.old_net_port_link.Location = new System.Drawing.Point(461, 418);
            this.old_net_port_link.Name = "old_net_port_link";
            this.old_net_port_link.Size = new System.Drawing.Size(95, 12);
            this.old_net_port_link.TabIndex = 30;
            this.old_net_port_link.TabStop = true;
            this.old_net_port_link.Text = "旧版NetPort链接";
            this.old_net_port_link.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.old_net_port_link_LinkClicked);
            // 
            // label163
            // 
            this.label163.AutoSize = true;
            this.label163.ForeColor = System.Drawing.Color.DodgerBlue;
            this.label163.Location = new System.Drawing.Point(23, 367);
            this.label163.Name = "label163";
            this.label163.Size = new System.Drawing.Size(47, 12);
            this.label163.TabIndex = 29;
            this.label163.Text = "说明： ";
            // 
            // net_clear_btn
            // 
            this.net_clear_btn.Location = new System.Drawing.Point(537, 21);
            this.net_clear_btn.Name = "net_clear_btn";
            this.net_clear_btn.Size = new System.Drawing.Size(75, 23);
            this.net_clear_btn.TabIndex = 28;
            this.net_clear_btn.Text = "清空";
            this.net_clear_btn.UseVisualStyleBackColor = true;
            this.net_clear_btn.Click += new System.EventHandler(this.net_clear_btn_Click);
            // 
            // net_base_settings_gb
            // 
            this.net_base_settings_gb.Controls.Add(this.net_base_mod_mac_tb);
            this.net_base_settings_gb.Controls.Add(this.label157);
            this.net_base_settings_gb.Controls.Add(this.net_base_dhcp_enable_cb);
            this.net_base_settings_gb.Controls.Add(this.net_base_mod_gateway_tb);
            this.net_base_settings_gb.Controls.Add(this.net_base_mod_mask_tb);
            this.net_base_settings_gb.Controls.Add(this.net_base_mod_ip_tb);
            this.net_base_settings_gb.Controls.Add(this.net_base_mod_name_tb);
            this.net_base_settings_gb.Controls.Add(this.label161);
            this.net_base_settings_gb.Controls.Add(this.label160);
            this.net_base_settings_gb.Controls.Add(this.label158);
            this.net_base_settings_gb.Controls.Add(this.label156);
            this.net_base_settings_gb.Location = new System.Drawing.Point(529, 95);
            this.net_base_settings_gb.Name = "net_base_settings_gb";
            this.net_base_settings_gb.Size = new System.Drawing.Size(241, 220);
            this.net_base_settings_gb.TabIndex = 27;
            this.net_base_settings_gb.TabStop = false;
            this.net_base_settings_gb.Text = "基础设置";
            // 
            // net_base_mod_mac_tb
            // 
            this.net_base_mod_mac_tb.Enabled = false;
            this.net_base_mod_mac_tb.Location = new System.Drawing.Point(78, 27);
            this.net_base_mod_mac_tb.Name = "net_base_mod_mac_tb";
            this.net_base_mod_mac_tb.Size = new System.Drawing.Size(152, 21);
            this.net_base_mod_mac_tb.TabIndex = 11;
            // 
            // label157
            // 
            this.label157.AutoSize = true;
            this.label157.Location = new System.Drawing.Point(13, 27);
            this.label157.Name = "label157";
            this.label157.Size = new System.Drawing.Size(59, 12);
            this.label157.TabIndex = 10;
            this.label157.Text = "设备Mac：";
            // 
            // net_base_dhcp_enable_cb
            // 
            this.net_base_dhcp_enable_cb.AutoSize = true;
            this.net_base_dhcp_enable_cb.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.net_base_dhcp_enable_cb.Location = new System.Drawing.Point(14, 91);
            this.net_base_dhcp_enable_cb.Name = "net_base_dhcp_enable_cb";
            this.net_base_dhcp_enable_cb.Size = new System.Drawing.Size(84, 16);
            this.net_base_dhcp_enable_cb.TabIndex = 9;
            this.net_base_dhcp_enable_cb.Text = "DHCP开启：";
            this.net_base_dhcp_enable_cb.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.net_base_dhcp_enable_cb.UseVisualStyleBackColor = true;
            // 
            // net_base_mod_gateway_tb
            // 
            this.net_base_mod_gateway_tb.Location = new System.Drawing.Point(78, 172);
            this.net_base_mod_gateway_tb.Name = "net_base_mod_gateway_tb";
            this.net_base_mod_gateway_tb.Size = new System.Drawing.Size(152, 21);
            this.net_base_mod_gateway_tb.TabIndex = 8;
            // 
            // net_base_mod_mask_tb
            // 
            this.net_base_mod_mask_tb.Location = new System.Drawing.Point(78, 143);
            this.net_base_mod_mask_tb.Name = "net_base_mod_mask_tb";
            this.net_base_mod_mask_tb.Size = new System.Drawing.Size(152, 21);
            this.net_base_mod_mask_tb.TabIndex = 7;
            // 
            // net_base_mod_ip_tb
            // 
            this.net_base_mod_ip_tb.Location = new System.Drawing.Point(78, 113);
            this.net_base_mod_ip_tb.Name = "net_base_mod_ip_tb";
            this.net_base_mod_ip_tb.Size = new System.Drawing.Size(152, 21);
            this.net_base_mod_ip_tb.TabIndex = 6;
            // 
            // net_base_mod_name_tb
            // 
            this.net_base_mod_name_tb.Location = new System.Drawing.Point(78, 56);
            this.net_base_mod_name_tb.Name = "net_base_mod_name_tb";
            this.net_base_mod_name_tb.Size = new System.Drawing.Size(152, 21);
            this.net_base_mod_name_tb.TabIndex = 5;
            // 
            // label161
            // 
            this.label161.AutoSize = true;
            this.label161.Location = new System.Drawing.Point(31, 175);
            this.label161.Name = "label161";
            this.label161.Size = new System.Drawing.Size(41, 12);
            this.label161.TabIndex = 4;
            this.label161.Text = "网关：";
            // 
            // label160
            // 
            this.label160.AutoSize = true;
            this.label160.Location = new System.Drawing.Point(6, 146);
            this.label160.Name = "label160";
            this.label160.Size = new System.Drawing.Size(65, 12);
            this.label160.TabIndex = 3;
            this.label160.Text = "子网掩码：";
            // 
            // label158
            // 
            this.label158.AutoSize = true;
            this.label158.Location = new System.Drawing.Point(19, 116);
            this.label158.Name = "label158";
            this.label158.Size = new System.Drawing.Size(53, 12);
            this.label158.TabIndex = 2;
            this.label158.Text = "设备Ip：";
            // 
            // label156
            // 
            this.label156.AutoSize = true;
            this.label156.Location = new System.Drawing.Point(19, 59);
            this.label156.Name = "label156";
            this.label156.Size = new System.Drawing.Size(53, 12);
            this.label156.TabIndex = 0;
            this.label156.Text = "设备名：";
            // 
            // dev_dgv
            // 
            this.dev_dgv.AllowUserToAddRows = false;
            this.dev_dgv.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dev_dgv.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.mod_check_Column,
            this.ModName,
            this.ModIp,
            this.ModMac,
            this.ModVer,
            this.PcMac});
            this.dev_dgv.Location = new System.Drawing.Point(25, 95);
            this.dev_dgv.Name = "dev_dgv";
            this.dev_dgv.RowTemplate.Height = 23;
            this.dev_dgv.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dev_dgv.Size = new System.Drawing.Size(498, 249);
            this.dev_dgv.TabIndex = 26;
            this.dev_dgv.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dev_dgv_CellContentClick);
            // 
            // mod_check_Column
            // 
            this.mod_check_Column.HeaderText = "#";
            this.mod_check_Column.Name = "mod_check_Column";
            this.mod_check_Column.Width = 30;
            // 
            // ModName
            // 
            this.ModName.HeaderText = "设备名";
            this.ModName.Name = "ModName";
            this.ModName.ReadOnly = true;
            this.ModName.Width = 70;
            // 
            // ModIp
            // 
            this.ModIp.HeaderText = "设备Ip";
            this.ModIp.Name = "ModIp";
            this.ModIp.ReadOnly = true;
            // 
            // ModMac
            // 
            this.ModMac.HeaderText = "设备Mac";
            this.ModMac.Name = "ModMac";
            this.ModMac.Width = 120;
            // 
            // ModVer
            // 
            this.ModVer.HeaderText = "芯片版本";
            this.ModVer.Name = "ModVer";
            this.ModVer.ReadOnly = true;
            this.ModVer.Width = 80;
            // 
            // PcMac
            // 
            this.PcMac.HeaderText = "主机Mac";
            this.PcMac.Name = "PcMac";
            this.PcMac.Width = 120;
            // 
            // label159
            // 
            this.label159.AutoSize = true;
            this.label159.Location = new System.Drawing.Point(112, 26);
            this.label159.Name = "label159";
            this.label159.Size = new System.Drawing.Size(41, 12);
            this.label159.TabIndex = 25;
            this.label159.Text = "网卡：";
            // 
            // net_card_combox
            // 
            this.net_card_combox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.net_card_combox.FormattingEnabled = true;
            this.net_card_combox.Location = new System.Drawing.Point(178, 23);
            this.net_card_combox.Name = "net_card_combox";
            this.net_card_combox.Size = new System.Drawing.Size(345, 20);
            this.net_card_combox.TabIndex = 24;
            this.net_card_combox.SelectedIndexChanged += new System.EventHandler(this.net_card_combox_SelectedIndexChanged);
            // 
            // groupBox30
            // 
            this.groupBox30.Controls.Add(this.label162);
            this.groupBox30.Controls.Add(this.net_pc_mask_label);
            this.groupBox30.Controls.Add(this.net_pc_mac_label);
            this.groupBox30.Controls.Add(this.net_pc_ip_label);
            this.groupBox30.Location = new System.Drawing.Point(106, 52);
            this.groupBox30.Name = "groupBox30";
            this.groupBox30.Size = new System.Drawing.Size(417, 37);
            this.groupBox30.TabIndex = 23;
            this.groupBox30.TabStop = false;
            this.groupBox30.Text = "主机信息";
            // 
            // label162
            // 
            this.label162.AutoSize = true;
            this.label162.Location = new System.Drawing.Point(124, 17);
            this.label162.Name = "label162";
            this.label162.Size = new System.Drawing.Size(35, 12);
            this.label162.TabIndex = 9;
            this.label162.Text = "Mac: ";
            // 
            // net_pc_mask_label
            // 
            this.net_pc_mask_label.AutoSize = true;
            this.net_pc_mask_label.Location = new System.Drawing.Point(274, 17);
            this.net_pc_mask_label.Name = "net_pc_mask_label";
            this.net_pc_mask_label.Size = new System.Drawing.Size(41, 12);
            this.net_pc_mask_label.TabIndex = 8;
            this.net_pc_mask_label.Text = "Mask: ";
            // 
            // net_pc_mac_label
            // 
            this.net_pc_mac_label.AutoSize = true;
            this.net_pc_mac_label.Location = new System.Drawing.Point(161, 17);
            this.net_pc_mac_label.Name = "net_pc_mac_label";
            this.net_pc_mac_label.Size = new System.Drawing.Size(35, 12);
            this.net_pc_mac_label.TabIndex = 7;
            this.net_pc_mac_label.Text = "     ";
            // 
            // net_pc_ip_label
            // 
            this.net_pc_ip_label.AutoSize = true;
            this.net_pc_ip_label.Location = new System.Drawing.Point(6, 17);
            this.net_pc_ip_label.Name = "net_pc_ip_label";
            this.net_pc_ip_label.Size = new System.Drawing.Size(29, 12);
            this.net_pc_ip_label.TabIndex = 6;
            this.net_pc_ip_label.Text = "Ip: ";
            // 
            // net_reset_btn
            // 
            this.net_reset_btn.Location = new System.Drawing.Point(695, 321);
            this.net_reset_btn.Name = "net_reset_btn";
            this.net_reset_btn.Size = new System.Drawing.Size(75, 23);
            this.net_reset_btn.TabIndex = 22;
            this.net_reset_btn.Text = "恢复出厂";
            this.net_reset_btn.UseVisualStyleBackColor = true;
            this.net_reset_btn.Click += new System.EventHandler(this.net_reset_btn_Click);
            // 
            // net_setCfg_btn
            // 
            this.net_setCfg_btn.Location = new System.Drawing.Point(610, 321);
            this.net_setCfg_btn.Name = "net_setCfg_btn";
            this.net_setCfg_btn.Size = new System.Drawing.Size(75, 23);
            this.net_setCfg_btn.TabIndex = 21;
            this.net_setCfg_btn.Text = "保存配置";
            this.net_setCfg_btn.UseVisualStyleBackColor = true;
            this.net_setCfg_btn.Click += new System.EventHandler(this.net_setCfg_btn_Click);
            // 
            // net_getCfg_btn
            // 
            this.net_getCfg_btn.Location = new System.Drawing.Point(529, 321);
            this.net_getCfg_btn.Name = "net_getCfg_btn";
            this.net_getCfg_btn.Size = new System.Drawing.Size(75, 23);
            this.net_getCfg_btn.TabIndex = 20;
            this.net_getCfg_btn.Text = "获取配置";
            this.net_getCfg_btn.UseVisualStyleBackColor = true;
            this.net_getCfg_btn.Click += new System.EventHandler(this.net_getCfg_btn_Click);
            // 
            // net_search_btn
            // 
            this.net_search_btn.Location = new System.Drawing.Point(25, 58);
            this.net_search_btn.Name = "net_search_btn";
            this.net_search_btn.Size = new System.Drawing.Size(75, 23);
            this.net_search_btn.TabIndex = 19;
            this.net_search_btn.Text = "搜索设备";
            this.net_search_btn.UseVisualStyleBackColor = true;
            this.net_search_btn.Click += new System.EventHandler(this.net_search_btn_Click);
            // 
            // net_refresh_netcard_btn
            // 
            this.net_refresh_netcard_btn.Location = new System.Drawing.Point(25, 15);
            this.net_refresh_netcard_btn.Name = "net_refresh_netcard_btn";
            this.net_refresh_netcard_btn.Size = new System.Drawing.Size(75, 23);
            this.net_refresh_netcard_btn.TabIndex = 18;
            this.net_refresh_netcard_btn.Text = "刷新网卡";
            this.net_refresh_netcard_btn.UseVisualStyleBackColor = true;
            this.net_refresh_netcard_btn.Click += new System.EventHandler(this.net_refresh_netcard_btn_Click);
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(2, 588);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(59, 12);
            this.label35.TabIndex = 13;
            this.label35.Text = "操作记录:";
            // 
            // ckDisplayLog
            // 
            this.ckDisplayLog.AutoSize = true;
            this.ckDisplayLog.ForeColor = System.Drawing.Color.Indigo;
            this.ckDisplayLog.Location = new System.Drawing.Point(879, 586);
            this.ckDisplayLog.Name = "ckDisplayLog";
            this.ckDisplayLog.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.ckDisplayLog.Size = new System.Drawing.Size(96, 16);
            this.ckDisplayLog.TabIndex = 16;
            this.ckDisplayLog.Text = "打开串口监控";
            this.ckDisplayLog.UseVisualStyleBackColor = true;
            this.ckDisplayLog.CheckedChanged += new System.EventHandler(this.ckDisplayLog_CheckedChanged);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 40.66265F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 59.33735F));
            this.tableLayoutPanel3.Controls.Add(this.panel6, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel7, 1, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(996, 55);
            this.tableLayoutPanel3.TabIndex = 48;
            // 
            // panel6
            // 
            this.panel6.Controls.Add(this.button4);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(4, 4);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(397, 47);
            this.panel6.TabIndex = 0;
            // 
            // button4
            // 
            this.button4.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button4.Location = new System.Drawing.Point(126, 4);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(144, 38);
            this.button4.TabIndex = 1;
            this.button4.Text = "开始盘存";
            this.button4.UseVisualStyleBackColor = true;
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.checkBox5);
            this.panel7.Controls.Add(this.checkBox6);
            this.panel7.Controls.Add(this.checkBox7);
            this.panel7.Controls.Add(this.checkBox8);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(408, 4);
            this.panel7.Name = "panel7";
            this.panel7.Size = new System.Drawing.Size(584, 47);
            this.panel7.TabIndex = 1;
            // 
            // checkBox5
            // 
            this.checkBox5.AutoSize = true;
            this.checkBox5.Checked = true;
            this.checkBox5.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBox5.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox5.Location = new System.Drawing.Point(64, 17);
            this.checkBox5.Name = "checkBox5";
            this.checkBox5.Size = new System.Drawing.Size(57, 16);
            this.checkBox5.TabIndex = 3;
            this.checkBox5.Text = "天线1";
            this.checkBox5.UseVisualStyleBackColor = true;
            // 
            // checkBox6
            // 
            this.checkBox6.AutoSize = true;
            this.checkBox6.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox6.Location = new System.Drawing.Point(436, 17);
            this.checkBox6.Name = "checkBox6";
            this.checkBox6.Size = new System.Drawing.Size(57, 16);
            this.checkBox6.TabIndex = 2;
            this.checkBox6.Text = "天线4";
            this.checkBox6.UseVisualStyleBackColor = true;
            // 
            // checkBox7
            // 
            this.checkBox7.AutoSize = true;
            this.checkBox7.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox7.Location = new System.Drawing.Point(312, 17);
            this.checkBox7.Name = "checkBox7";
            this.checkBox7.Size = new System.Drawing.Size(57, 16);
            this.checkBox7.TabIndex = 1;
            this.checkBox7.Text = "天线3";
            this.checkBox7.UseVisualStyleBackColor = true;
            // 
            // checkBox8
            // 
            this.checkBox8.AutoSize = true;
            this.checkBox8.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.checkBox8.Location = new System.Drawing.Point(188, 17);
            this.checkBox8.Name = "checkBox8";
            this.checkBox8.Size = new System.Drawing.Size(57, 16);
            this.checkBox8.TabIndex = 0;
            this.checkBox8.Text = "天线2";
            this.checkBox8.UseVisualStyleBackColor = true;
            // 
            // textBox5
            // 
            this.textBox5.Location = new System.Drawing.Point(704, 234);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(89, 21);
            this.textBox5.TabIndex = 46;
            // 
            // textBox6
            // 
            this.textBox6.Location = new System.Drawing.Point(502, 234);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(89, 21);
            this.textBox6.TabIndex = 47;
            // 
            // button5
            // 
            this.button5.ForeColor = System.Drawing.SystemColors.Desktop;
            this.button5.Location = new System.Drawing.Point(907, 232);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(89, 23);
            this.button5.TabIndex = 45;
            this.button5.Text = "刷新界面";
            this.button5.UseVisualStyleBackColor = true;
            // 
            // label76
            // 
            this.label76.AutoSize = true;
            this.label76.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label76.Location = new System.Drawing.Point(633, 237);
            this.label76.Name = "label76";
            this.label76.Size = new System.Drawing.Size(65, 12);
            this.label76.TabIndex = 43;
            this.label76.Text = "Max RSSI：";
            // 
            // label77
            // 
            this.label77.AutoSize = true;
            this.label77.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label77.Location = new System.Drawing.Point(431, 238);
            this.label77.Name = "label77";
            this.label77.Size = new System.Drawing.Size(65, 12);
            this.label77.TabIndex = 44;
            this.label77.Text = "Min RSSI：";
            // 
            // label78
            // 
            this.label78.AutoSize = true;
            this.label78.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label78.Location = new System.Drawing.Point(6, 237);
            this.label78.Name = "label78";
            this.label78.Size = new System.Drawing.Size(71, 12);
            this.label78.TabIndex = 42;
            this.label78.Text = "标签列表： ";
            // 
            // groupBox8
            // 
            this.groupBox8.Controls.Add(this.comboBox9);
            this.groupBox8.Controls.Add(this.lxLedControl9);
            this.groupBox8.Controls.Add(this.lxLedControl10);
            this.groupBox8.Controls.Add(this.lxLedControl11);
            this.groupBox8.Controls.Add(this.lxLedControl12);
            this.groupBox8.Controls.Add(this.label79);
            this.groupBox8.Controls.Add(this.label80);
            this.groupBox8.Controls.Add(this.label81);
            this.groupBox8.Controls.Add(this.label82);
            this.groupBox8.Controls.Add(this.label83);
            this.groupBox8.Controls.Add(this.lxLedControl13);
            this.groupBox8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.groupBox8.Location = new System.Drawing.Point(2, 64);
            this.groupBox8.Name = "groupBox8";
            this.groupBox8.Size = new System.Drawing.Size(996, 162);
            this.groupBox8.TabIndex = 24;
            this.groupBox8.TabStop = false;
            this.groupBox8.Text = "数据";
            // 
            // comboBox9
            // 
            this.comboBox9.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox9.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox9.FormattingEnabled = true;
            this.comboBox9.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "不选"});
            this.comboBox9.Location = new System.Drawing.Point(-165, 111);
            this.comboBox9.Name = "comboBox9";
            this.comboBox9.Size = new System.Drawing.Size(55, 20);
            this.comboBox9.TabIndex = 39;
            // 
            // lxLedControl9
            // 
            this.lxLedControl9.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl9.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl9.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl9.BevelRate = 0.1F;
            this.lxLedControl9.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl9.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl9.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl9.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl9.HighlightOpaque = ((byte)(20));
            this.lxLedControl9.Location = new System.Drawing.Point(702, 118);
            this.lxLedControl9.Name = "lxLedControl9";
            this.lxLedControl9.RoundCorner = true;
            this.lxLedControl9.SegmentIntervalRatio = 50;
            this.lxLedControl9.ShowHighlight = true;
            this.lxLedControl9.Size = new System.Drawing.Size(183, 35);
            this.lxLedControl9.TabIndex = 35;
            this.lxLedControl9.Text = "0";
            this.lxLedControl9.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.lxLedControl9.TotalCharCount = 10;
            // 
            // lxLedControl10
            // 
            this.lxLedControl10.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl10.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl10.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl10.BevelRate = 0.1F;
            this.lxLedControl10.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl10.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl10.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl10.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl10.HighlightOpaque = ((byte)(20));
            this.lxLedControl10.Location = new System.Drawing.Point(496, 35);
            this.lxLedControl10.Name = "lxLedControl10";
            this.lxLedControl10.RoundCorner = true;
            this.lxLedControl10.SegmentIntervalRatio = 50;
            this.lxLedControl10.ShowHighlight = true;
            this.lxLedControl10.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl10.TabIndex = 34;
            this.lxLedControl10.Text = "0";
            this.lxLedControl10.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lxLedControl11
            // 
            this.lxLedControl11.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl11.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl11.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl11.BevelRate = 0.1F;
            this.lxLedControl11.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl11.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl11.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl11.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl11.HighlightOpaque = ((byte)(20));
            this.lxLedControl11.Location = new System.Drawing.Point(497, 103);
            this.lxLedControl11.Name = "lxLedControl11";
            this.lxLedControl11.RoundCorner = true;
            this.lxLedControl11.SegmentIntervalRatio = 50;
            this.lxLedControl11.ShowHighlight = true;
            this.lxLedControl11.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl11.TabIndex = 33;
            this.lxLedControl11.Text = "0";
            this.lxLedControl11.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lxLedControl12
            // 
            this.lxLedControl12.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl12.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl12.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl12.BevelRate = 0.1F;
            this.lxLedControl12.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl12.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl12.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl12.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl12.HighlightOpaque = ((byte)(20));
            this.lxLedControl12.Location = new System.Drawing.Point(702, 35);
            this.lxLedControl12.Name = "lxLedControl12";
            this.lxLedControl12.RoundCorner = true;
            this.lxLedControl12.SegmentIntervalRatio = 50;
            this.lxLedControl12.ShowHighlight = true;
            this.lxLedControl12.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl12.TabIndex = 32;
            this.lxLedControl12.Text = "0";
            this.lxLedControl12.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // label79
            // 
            this.label79.AutoSize = true;
            this.label79.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label79.Location = new System.Drawing.Point(700, 103);
            this.label79.Name = "label79";
            this.label79.Size = new System.Drawing.Size(137, 12);
            this.label79.TabIndex = 30;
            this.label79.Text = "累计运行的时间(毫秒)：";
            // 
            // label80
            // 
            this.label80.AutoSize = true;
            this.label80.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label80.Location = new System.Drawing.Point(495, 17);
            this.label80.Name = "label80";
            this.label80.Size = new System.Drawing.Size(131, 12);
            this.label80.TabIndex = 29;
            this.label80.Text = "命令识别速度(个/秒)：";
            // 
            // label81
            // 
            this.label81.AutoSize = true;
            this.label81.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label81.Location = new System.Drawing.Point(498, 88);
            this.label81.Name = "label81";
            this.label81.Size = new System.Drawing.Size(125, 12);
            this.label81.TabIndex = 28;
            this.label81.Text = "命令执行时间(毫秒)：";
            // 
            // label82
            // 
            this.label82.AutoSize = true;
            this.label82.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label82.Location = new System.Drawing.Point(700, 17);
            this.label82.Name = "label82";
            this.label82.Size = new System.Drawing.Size(113, 12);
            this.label82.TabIndex = 27;
            this.label82.Text = "命令返回数据(条)：";
            // 
            // label83
            // 
            this.label83.AutoSize = true;
            this.label83.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label83.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label83.Location = new System.Drawing.Point(104, 17);
            this.label83.Name = "label83";
            this.label83.Size = new System.Drawing.Size(149, 12);
            this.label83.TabIndex = 26;
            this.label83.Text = "已盘存的标签总数量(个)：";
            // 
            // lxLedControl13
            // 
            this.lxLedControl13.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl13.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl13.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl13.BevelRate = 0.1F;
            this.lxLedControl13.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl13.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl13.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl13.ForeColor = System.Drawing.Color.Purple;
            this.lxLedControl13.HighlightOpaque = ((byte)(20));
            this.lxLedControl13.Location = new System.Drawing.Point(106, 35);
            this.lxLedControl13.Name = "lxLedControl13";
            this.lxLedControl13.RoundCorner = true;
            this.lxLedControl13.SegmentIntervalRatio = 50;
            this.lxLedControl13.ShowHighlight = true;
            this.lxLedControl13.Size = new System.Drawing.Size(310, 118);
            this.lxLedControl13.TabIndex = 21;
            this.lxLedControl13.Text = "0";
            this.lxLedControl13.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // listView1
            // 
            this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader43,
            this.columnHeader44,
            this.columnHeader45,
            this.columnHeader46,
            this.columnHeader47,
            this.columnHeader48});
            this.listView1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(3, 261);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(996, 267);
            this.listView1.TabIndex = 23;
            this.listView1.UseCompatibleStateImageBehavior = false;
            this.listView1.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader43
            // 
            this.columnHeader43.Text = "ID";
            this.columnHeader43.Width = 56;
            // 
            // columnHeader44
            // 
            this.columnHeader44.Text = "EPC";
            this.columnHeader44.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader44.Width = 486;
            // 
            // columnHeader45
            // 
            this.columnHeader45.Text = "PC";
            this.columnHeader45.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader45.Width = 83;
            // 
            // columnHeader46
            // 
            this.columnHeader46.Text = "识别次数";
            this.columnHeader46.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader46.Width = 129;
            // 
            // columnHeader47
            // 
            this.columnHeader47.Text = "RSSI";
            this.columnHeader47.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader47.Width = 95;
            // 
            // columnHeader48
            // 
            this.columnHeader48.Text = "载波频率";
            this.columnHeader48.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader48.Width = 92;
            // 
            // comboBox10
            // 
            this.comboBox10.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox10.ForeColor = System.Drawing.SystemColors.InfoText;
            this.comboBox10.FormattingEnabled = true;
            this.comboBox10.Items.AddRange(new object[] {
            "天线1",
            "天线2",
            "天线3",
            "天线4",
            "不选"});
            this.comboBox10.Location = new System.Drawing.Point(-165, 111);
            this.comboBox10.Name = "comboBox10";
            this.comboBox10.Size = new System.Drawing.Size(55, 20);
            this.comboBox10.TabIndex = 39;
            // 
            // label87
            // 
            this.label87.AutoSize = true;
            this.label87.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label87.Location = new System.Drawing.Point(700, 103);
            this.label87.Name = "label87";
            this.label87.Size = new System.Drawing.Size(137, 12);
            this.label87.TabIndex = 30;
            this.label87.Text = "累计运行的时间(毫秒)：";
            // 
            // label88
            // 
            this.label88.AutoSize = true;
            this.label88.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label88.Location = new System.Drawing.Point(495, 17);
            this.label88.Name = "label88";
            this.label88.Size = new System.Drawing.Size(131, 12);
            this.label88.TabIndex = 29;
            this.label88.Text = "命令识别速度(个/秒)：";
            // 
            // label89
            // 
            this.label89.AutoSize = true;
            this.label89.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label89.Location = new System.Drawing.Point(498, 88);
            this.label89.Name = "label89";
            this.label89.Size = new System.Drawing.Size(125, 12);
            this.label89.TabIndex = 28;
            this.label89.Text = "命令执行时间(毫秒)：";
            // 
            // label90
            // 
            this.label90.AutoSize = true;
            this.label90.ForeColor = System.Drawing.SystemColors.Desktop;
            this.label90.Location = new System.Drawing.Point(700, 17);
            this.label90.Name = "label90";
            this.label90.Size = new System.Drawing.Size(113, 12);
            this.label90.TabIndex = 27;
            this.label90.Text = "命令返回数据(条)：";
            // 
            // label91
            // 
            this.label91.AutoSize = true;
            this.label91.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.label91.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label91.Location = new System.Drawing.Point(104, 17);
            this.label91.Name = "label91";
            this.label91.Size = new System.Drawing.Size(149, 12);
            this.label91.TabIndex = 26;
            this.label91.Text = "已盘存的标签总数量(个)：";
            // 
            // ckClearOperationRec
            // 
            this.ckClearOperationRec.AutoSize = true;
            this.ckClearOperationRec.Checked = true;
            this.ckClearOperationRec.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ckClearOperationRec.Location = new System.Drawing.Point(71, 587);
            this.ckClearOperationRec.Name = "ckClearOperationRec";
            this.ckClearOperationRec.Size = new System.Drawing.Size(72, 16);
            this.ckClearOperationRec.TabIndex = 17;
            this.ckClearOperationRec.Text = "自动清空";
            this.ckClearOperationRec.UseVisualStyleBackColor = true;
            // 
            // lrtxtLog
            // 
            this.lrtxtLog.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lrtxtLog.Location = new System.Drawing.Point(0, 608);
            this.lrtxtLog.Name = "lrtxtLog";
            this.lrtxtLog.Size = new System.Drawing.Size(1018, 132);
            this.lrtxtLog.TabIndex = 1;
            this.lrtxtLog.Text = "";
            this.lrtxtLog.TextChanged += new System.EventHandler(this.lrtxtLog_TextChanged);
            this.lrtxtLog.DoubleClick += new System.EventHandler(this.lrtxtLog_DoubleClick);
            // 
            // lxLedControl14
            // 
            this.lxLedControl14.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl14.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl14.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl14.BevelRate = 0.1F;
            this.lxLedControl14.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl14.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl14.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl14.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl14.HighlightOpaque = ((byte)(20));
            this.lxLedControl14.Location = new System.Drawing.Point(702, 118);
            this.lxLedControl14.Name = "lxLedControl14";
            this.lxLedControl14.RoundCorner = true;
            this.lxLedControl14.SegmentIntervalRatio = 50;
            this.lxLedControl14.ShowHighlight = true;
            this.lxLedControl14.Size = new System.Drawing.Size(183, 35);
            this.lxLedControl14.TabIndex = 35;
            this.lxLedControl14.Text = "0";
            this.lxLedControl14.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            this.lxLedControl14.TotalCharCount = 10;
            // 
            // lxLedControl15
            // 
            this.lxLedControl15.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl15.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl15.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl15.BevelRate = 0.1F;
            this.lxLedControl15.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl15.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl15.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl15.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl15.HighlightOpaque = ((byte)(20));
            this.lxLedControl15.Location = new System.Drawing.Point(496, 35);
            this.lxLedControl15.Name = "lxLedControl15";
            this.lxLedControl15.RoundCorner = true;
            this.lxLedControl15.SegmentIntervalRatio = 50;
            this.lxLedControl15.ShowHighlight = true;
            this.lxLedControl15.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl15.TabIndex = 34;
            this.lxLedControl15.Text = "0";
            this.lxLedControl15.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lxLedControl16
            // 
            this.lxLedControl16.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl16.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl16.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl16.BevelRate = 0.1F;
            this.lxLedControl16.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl16.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl16.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl16.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl16.HighlightOpaque = ((byte)(20));
            this.lxLedControl16.Location = new System.Drawing.Point(497, 103);
            this.lxLedControl16.Name = "lxLedControl16";
            this.lxLedControl16.RoundCorner = true;
            this.lxLedControl16.SegmentIntervalRatio = 50;
            this.lxLedControl16.ShowHighlight = true;
            this.lxLedControl16.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl16.TabIndex = 33;
            this.lxLedControl16.Text = "0";
            this.lxLedControl16.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lxLedControl17
            // 
            this.lxLedControl17.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl17.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl17.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl17.BevelRate = 0.1F;
            this.lxLedControl17.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl17.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl17.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl17.ForeColor = System.Drawing.Color.Black;
            this.lxLedControl17.HighlightOpaque = ((byte)(20));
            this.lxLedControl17.Location = new System.Drawing.Point(702, 35);
            this.lxLedControl17.Name = "lxLedControl17";
            this.lxLedControl17.RoundCorner = true;
            this.lxLedControl17.SegmentIntervalRatio = 50;
            this.lxLedControl17.ShowHighlight = true;
            this.lxLedControl17.Size = new System.Drawing.Size(140, 50);
            this.lxLedControl17.TabIndex = 32;
            this.lxLedControl17.Text = "0";
            this.lxLedControl17.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // lxLedControl18
            // 
            this.lxLedControl18.BackColor = System.Drawing.Color.Transparent;
            this.lxLedControl18.BackColor_1 = System.Drawing.Color.Transparent;
            this.lxLedControl18.BackColor_2 = System.Drawing.Color.DarkRed;
            this.lxLedControl18.BevelRate = 0.1F;
            this.lxLedControl18.BorderColor = System.Drawing.Color.Lavender;
            this.lxLedControl18.FadedColor = System.Drawing.SystemColors.ControlLight;
            this.lxLedControl18.FocusedBorderColor = System.Drawing.Color.LightCoral;
            this.lxLedControl18.ForeColor = System.Drawing.Color.Purple;
            this.lxLedControl18.HighlightOpaque = ((byte)(20));
            this.lxLedControl18.Location = new System.Drawing.Point(106, 35);
            this.lxLedControl18.Name = "lxLedControl18";
            this.lxLedControl18.RoundCorner = true;
            this.lxLedControl18.SegmentIntervalRatio = 50;
            this.lxLedControl18.ShowHighlight = true;
            this.lxLedControl18.Size = new System.Drawing.Size(310, 118);
            this.lxLedControl18.TabIndex = 21;
            this.lxLedControl18.Text = "0";
            this.lxLedControl18.TextAlignment = LxControl.LxLedControl.Alignment.Right;
            // 
            // timerInventory
            // 
            this.timerInventory.Interval = 500;
            this.timerInventory.Tick += new System.EventHandler(this.timerInventory_Tick);
            // 
            // totalTime
            // 
            this.totalTime.Interval = 50;
            this.totalTime.Tick += new System.EventHandler(this.totalTimeDisplay);
            // 
            // sortImageList
            // 
            this.sortImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("sortImageList.ImageStream")));
            this.sortImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.sortImageList.Images.SetKeyName(0, "sort.png");
            // 
            // R2000UartDemo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(1018, 740);
            this.Controls.Add(this.ckClearOperationRec);
            this.Controls.Add(this.ckDisplayLog);
            this.Controls.Add(this.label35);
            this.Controls.Add(this.lrtxtLog);
            this.Controls.Add(this.tabCtrMain);
            this.ForeColor = System.Drawing.Color.Black;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "R2000UartDemo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "UHF RFID Reader Demo v4.0.3";
            this.Load += new System.EventHandler(this.R2000UartDemo_Load);
            this.tabCtrMain.ResumeLayout(false);
            this.PagReaderSetting.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.groupBox24.ResumeLayout(false);
            this.groupBox24.PerformLayout();
            this.gbCmdReadGpio.ResumeLayout(false);
            this.groupBox11.ResumeLayout(false);
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.groupBox7.ResumeLayout(false);
            this.groupBox7.PerformLayout();
            this.groupBox10.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.gbCmdBeeper.ResumeLayout(false);
            this.gbCmdBeeper.PerformLayout();
            this.gbCmdTemperature.ResumeLayout(false);
            this.gbCmdTemperature.PerformLayout();
            this.gbCmdVersion.ResumeLayout(false);
            this.gbCmdVersion.PerformLayout();
            this.gbCmdBaudrate.ResumeLayout(false);
            this.gbCmdBaudrate.PerformLayout();
            this.gbCmdReaderAddress.ResumeLayout(false);
            this.gbCmdReaderAddress.PerformLayout();
            this.gbTcpIp.ResumeLayout(false);
            this.gbTcpIp.PerformLayout();
            this.gbRS232.ResumeLayout(false);
            this.gbRS232.PerformLayout();
            this.gbConnectType.ResumeLayout(false);
            this.gbConnectType.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.gbReturnLoss.ResumeLayout(false);
            this.gbReturnLoss.PerformLayout();
            this.gbProfile.ResumeLayout(false);
            this.gbProfile.PerformLayout();
            this.gbMonza.ResumeLayout(false);
            this.gbMonza.PerformLayout();
            this.gbCmdAntDetector.ResumeLayout(false);
            this.gbCmdAntDetector.PerformLayout();
            this.gbCmdAntenna.ResumeLayout(false);
            this.gbCmdAntenna.PerformLayout();
            this.gbCmdRegion.ResumeLayout(false);
            this.gbCmdRegion.PerformLayout();
            this.groupBox23.ResumeLayout(false);
            this.groupBox23.PerformLayout();
            this.groupBox21.ResumeLayout(false);
            this.groupBox21.PerformLayout();
            this.gbCmdOutputPower.ResumeLayout(false);
            this.gbCmdOutputPower.PerformLayout();
            this.pageEpcTest.ResumeLayout(false);
            this.tabEpcTest.ResumeLayout(false);
            this.pageRealMode.ResumeLayout(false);
            this.pageRealMode.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.groupBox20.ResumeLayout(false);
            this.groupBox20.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledReal1)).EndInit();
            this.pageBufferedMode.ResumeLayout(false);
            this.pageBufferedMode.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            this.panel8.ResumeLayout(false);
            this.panel8.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledBuffer1)).EndInit();
            this.pageFast4AntMode.ResumeLayout(false);
            this.pageFast4AntMode.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox25.ResumeLayout(false);
            this.groupBox25.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ledFast5)).EndInit();
            this.groupBox28.ResumeLayout(false);
            this.groupBox28.PerformLayout();
            this.groupBox27.ResumeLayout(false);
            this.groupBox27.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            this.splitContainer1.ResumeLayout(false);
            this.pageAcessTag.ResumeLayout(false);
            this.gbCmdOperateTag.ResumeLayout(false);
            this.groupBox16.ResumeLayout(false);
            this.groupBox16.PerformLayout();
            this.groupBox15.ResumeLayout(false);
            this.groupBox15.PerformLayout();
            this.groupBox19.ResumeLayout(false);
            this.groupBox19.PerformLayout();
            this.groupBox18.ResumeLayout(false);
            this.groupBox18.PerformLayout();
            this.groupBox14.ResumeLayout(false);
            this.groupBox14.PerformLayout();
            this.groupBox17.ResumeLayout(false);
            this.groupBox17.PerformLayout();
            this.groupBox13.ResumeLayout(false);
            this.groupBox13.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.groupBox22.ResumeLayout(false);
            this.groupBox12.ResumeLayout(false);
            this.groupBox12.PerformLayout();
            this.groupBox9.ResumeLayout(false);
            this.groupBox9.PerformLayout();
            this.PagISO18000.ResumeLayout(false);
            this.PagISO18000.PerformLayout();
            this.gbISO1800LockQuery.ResumeLayout(false);
            this.gbISO1800LockQuery.PerformLayout();
            this.gbISO1800ReadWrite.ResumeLayout(false);
            this.gbISO1800ReadWrite.PerformLayout();
            this.PagTranDataLog.ResumeLayout(false);
            this.gbCmdManual.ResumeLayout(false);
            this.gbCmdManual.PerformLayout();
            this.net_configure_tabPage.ResumeLayout(false);
            this.net_configure_tabPage.PerformLayout();
            this.port_setting_tabcontrol.ResumeLayout(false);
            this.net_port_0_tabPage.ResumeLayout(false);
            this.net_port_0_tabPage.PerformLayout();
            this.net_port_1_tabPage.ResumeLayout(false);
            this.net_port_1_tabPage.PerformLayout();
            this.net_base_settings_gb.ResumeLayout(false);
            this.net_base_settings_gb.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dev_dgv)).EndInit();
            this.groupBox30.ResumeLayout(false);
            this.groupBox30.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.groupBox8.ResumeLayout(false);
            this.groupBox8.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl17)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lxLedControl18)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl tabCtrMain;
        private System.Windows.Forms.TabPage PagReaderSetting;
        private CustomControl.LogRichTextBox lrtxtLog;
        private System.Windows.Forms.TabPage PagTranDataLog;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.Button btnSendData;
        private System.Windows.Forms.Label label17;
        private CustomControl.HexTextBox htxtSendData;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.GroupBox gbCmdManual;
        private CustomControl.LogRichTextBox lrtxtDataTran;
        private CustomControl.HexTextBox htxtCheckData;
        private System.Windows.Forms.Button btnClearData;
        private System.Windows.Forms.TabPage PagISO18000;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.Button btnWriteTagISO18000;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.ColumnHeader columnHeader25;
        private System.Windows.Forms.ColumnHeader columnHeader26;
        private CustomControl.HexTextBox htxtReadUID;
        private CustomControl.HexTextBox htxtQueryAdd;
        private CustomControl.HexTextBox htxtWriteStartAdd;
        private System.Windows.Forms.Button btnInventoryISO18000;
        private System.Windows.Forms.Button btnReadTagISO18000;
        private System.Windows.Forms.Button btnLockTagISO18000;
        private System.Windows.Forms.Button btnQueryTagISO18000;
        private System.Windows.Forms.Label label50;
        private CustomControl.HexTextBox htxtReadStartAdd;
        private CustomControl.HexTextBox htxtWriteData18000;
        private CustomControl.HexTextBox htxtLockAdd;
        private System.Windows.Forms.ListView ltvTagISO18000;
        private System.Windows.Forms.Label label51;
        private System.Windows.Forms.Label label52;
        private System.Windows.Forms.TextBox txtReadLength;
        private System.Windows.Forms.TextBox txtWriteLength;
        private CustomControl.HexTextBox htxtReadData18000;
        private System.Windows.Forms.ColumnHeader columnHeader27;
        private System.Windows.Forms.ColumnHeader columnHeader28;
        private System.Windows.Forms.TextBox txtStatus;
        private System.Windows.Forms.GroupBox gbISO1800ReadWrite;
        private System.Windows.Forms.GroupBox gbISO1800LockQuery;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.CheckBox ckDisplayLog;
        private System.Windows.Forms.TextBox txtLoopTimes;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.TextBox txtLoop;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TabPage pageEpcTest;
        private System.Windows.Forms.TabControl tabEpcTest;
        private System.Windows.Forms.TabPage pageFast4AntMode;
        private System.Windows.Forms.TabPage pageRealMode;
        private System.Windows.Forms.ColumnHeader columnHeader37;
        private System.Windows.Forms.ColumnHeader columnHeader38;
        private System.Windows.Forms.ColumnHeader columnHeader39;
        private System.Windows.Forms.ColumnHeader columnHeader40;
        private System.Windows.Forms.ColumnHeader columnHeader41;
        private System.Windows.Forms.ColumnHeader columnHeader42;
        private System.Windows.Forms.ComboBox cmbAntSelect4;
        private System.Windows.Forms.ComboBox cmbAntSelect3;
        private System.Windows.Forms.ComboBox cmbAntSelect2;
        private System.Windows.Forms.ComboBox cmbAntSelect1;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.Label label59;
        private System.Windows.Forms.Label label64;
        private System.Windows.Forms.Label label65;
        private System.Windows.Forms.Label label62;
        private System.Windows.Forms.Label label63;
        private System.Windows.Forms.Label label60;
        private System.Windows.Forms.Label label61;
        private System.Windows.Forms.TabPage pageBufferedMode;
        private System.Windows.Forms.TabPage pageAcessTag;
        private System.Windows.Forms.Label txtFastTagList;
        private System.Windows.Forms.ColumnHeader columnHeader31;
        private System.Windows.Forms.ColumnHeader columnHeader32;
        private System.Windows.Forms.ColumnHeader columnHeader33;
        private System.Windows.Forms.ColumnHeader columnHeader34;
        private System.Windows.Forms.ColumnHeader columnHeader35;
        private System.Windows.Forms.ColumnHeader columnHeader356;
        private System.Windows.Forms.ColumnHeader columnHeader36;
        private System.Windows.Forms.Label label72;
        private System.Windows.Forms.Label label73;
        private System.Windows.Forms.Button buttonFastFresh;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox txtFastMinRssi;
        private System.Windows.Forms.TextBox txtFastMaxRssi;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.TextBox tbRealMinRssi;
        private System.Windows.Forms.TextBox tbRealMaxRssi;
        private System.Windows.Forms.Button btRealFresh;
        private System.Windows.Forms.Label label70;
        private System.Windows.Forms.Label label74;
        private System.Windows.Forms.Label lbRealTagCount;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ComboBox comboBox6;
        private LxControl.LxLedControl ledReal5;
        private LxControl.LxLedControl ledReal2;
        private LxControl.LxLedControl ledReal4;
        private System.Windows.Forms.Label label53;
        private System.Windows.Forms.Label label66;
        private System.Windows.Forms.Label label67;
        private System.Windows.Forms.Label label68;
        private System.Windows.Forms.Label label69;
        private LxControl.LxLedControl ledReal1;
        private System.Windows.Forms.Button btRealTimeInventory;
        private System.Windows.Forms.Button btGetBuffer;
        private System.Windows.Forms.Panel panel8;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Button btBufferFresh;
        private System.Windows.Forms.Label labelBufferTagCount;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.CheckBox checkBox5;
        private System.Windows.Forms.CheckBox checkBox6;
        private System.Windows.Forms.CheckBox checkBox7;
        private System.Windows.Forms.CheckBox checkBox8;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Label label76;
        private System.Windows.Forms.Label label77;
        private System.Windows.Forms.Label label78;
        private System.Windows.Forms.GroupBox groupBox8;
        private System.Windows.Forms.ComboBox comboBox9;
        private LxControl.LxLedControl lxLedControl9;
        private LxControl.LxLedControl lxLedControl10;
        private LxControl.LxLedControl lxLedControl11;
        private LxControl.LxLedControl lxLedControl12;
        private System.Windows.Forms.Label label79;
        private System.Windows.Forms.Label label80;
        private System.Windows.Forms.Label label81;
        private System.Windows.Forms.Label label82;
        private System.Windows.Forms.Label label83;
        private LxControl.LxLedControl lxLedControl13;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.ColumnHeader columnHeader43;
        private System.Windows.Forms.ColumnHeader columnHeader44;
        private System.Windows.Forms.ColumnHeader columnHeader45;
        private System.Windows.Forms.ColumnHeader columnHeader46;
        private System.Windows.Forms.ColumnHeader columnHeader47;
        private System.Windows.Forms.ColumnHeader columnHeader48;
        private System.Windows.Forms.ComboBox comboBox10;
        private LxControl.LxLedControl lxLedControl14;
        private LxControl.LxLedControl lxLedControl15;
        private LxControl.LxLedControl lxLedControl16;
        private LxControl.LxLedControl lxLedControl17;
        private System.Windows.Forms.Label label87;
        private System.Windows.Forms.Label label88;
        private System.Windows.Forms.Label label89;
        private System.Windows.Forms.Label label90;
        private System.Windows.Forms.Label label91;
        private LxControl.LxLedControl lxLedControl18;
        private System.Windows.Forms.Button btQueryBuffer;
        private System.Windows.Forms.Button btGetClearBuffer;
        private System.Windows.Forms.Button btClearBuffer;
        private System.Windows.Forms.ListView lvBufferList;
        private System.Windows.Forms.ColumnHeader columnHeader49;
        private System.Windows.Forms.ColumnHeader columnHeader50;
        private System.Windows.Forms.ColumnHeader columnHeader51;
        private System.Windows.Forms.ColumnHeader columnHeader52;
        private System.Windows.Forms.ColumnHeader columnHeader53;
        private System.Windows.Forms.ColumnHeader columnHeader54;
        private System.Windows.Forms.GroupBox gbCmdOperateTag;
        private System.Windows.Forms.GroupBox groupBox16;
        private System.Windows.Forms.Button btnKillTag;
        private CustomControl.HexTextBox htxtKillPwd;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.GroupBox groupBox15;
        private CustomControl.HexTextBox htxtLockPwd;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.GroupBox groupBox19;
        private System.Windows.Forms.RadioButton rdbUserMemory;
        private System.Windows.Forms.RadioButton rdbTidMemory;
        private System.Windows.Forms.RadioButton rdbEpcMermory;
        private System.Windows.Forms.RadioButton rdbKillPwd;
        private System.Windows.Forms.RadioButton rdbAccessPwd;
        private System.Windows.Forms.GroupBox groupBox18;
        private System.Windows.Forms.RadioButton rdbLockEver;
        private System.Windows.Forms.RadioButton rdbFreeEver;
        private System.Windows.Forms.RadioButton rdbLock;
        private System.Windows.Forms.RadioButton rdbFree;
        private System.Windows.Forms.Button btnLockTag;
        private System.Windows.Forms.GroupBox groupBox14;
        private CustomControl.HexTextBox htxtWriteData;
        private System.Windows.Forms.TextBox txtWordCnt;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Button btnWriteTag;
        private System.Windows.Forms.Button btnReadTag;
        private System.Windows.Forms.TextBox txtWordAdd;
        private System.Windows.Forms.Label label26;
        private CustomControl.HexTextBox htxtReadAndWritePwd;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.GroupBox groupBox17;
        private System.Windows.Forms.RadioButton rdbUser;
        private System.Windows.Forms.RadioButton rdbTid;
        private System.Windows.Forms.RadioButton rdbEpc;
        private System.Windows.Forms.RadioButton rdbReserved;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.GroupBox groupBox13;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Button btnSetAccessEpcMatch;
        private System.Windows.Forms.ComboBox cmbSetAccessEpcMatch;
        private System.Windows.Forms.TextBox txtAccessEpcMatch;
        private System.Windows.Forms.CheckBox ckAccessEpcMatch;
        private System.Windows.Forms.TextBox textRealRound;
        private System.Windows.Forms.Label label84;
        private System.Windows.Forms.Label label85;
        private System.Windows.Forms.TextBox textReadRoundBuffer;
        private System.Windows.Forms.ListView ltvOperate;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader9;
        private System.Windows.Forms.ColumnHeader columnHeader10;
        private System.Windows.Forms.ColumnHeader columnHeader11;
        private System.Windows.Forms.ColumnHeader columnHeader12;
        private System.Windows.Forms.ColumnHeader columnHeader13;
        private System.Windows.Forms.ColumnHeader columnHeader14;
        private System.Windows.Forms.ColumnHeader columnHeader15;
        private System.Windows.Forms.ListView lvRealList;
        private LxControl.LxLedControl ledReal3;
        private System.Windows.Forms.GroupBox groupBox3;
        private LxControl.LxLedControl ledBuffer4;
        private System.Windows.Forms.ComboBox comboBox11;
        private LxControl.LxLedControl ledBuffer5;
        private LxControl.LxLedControl ledBuffer2;
        private LxControl.LxLedControl ledBuffer3;
        private System.Windows.Forms.Label label92;
        private System.Windows.Forms.Label label93;
        private System.Windows.Forms.Label label94;
        private System.Windows.Forms.Label label95;
        private System.Windows.Forms.Label label96;
        private LxControl.LxLedControl ledBuffer1;
        private System.Windows.Forms.Button btBufferInventory;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.ColumnHeader columnHeader16;
        private System.Windows.Forms.Button btFastInventory;
        private System.Windows.Forms.GroupBox groupBox2;
        private LxControl.LxLedControl ledFast4;
        private System.Windows.Forms.ComboBox comboBox7;
        private LxControl.LxLedControl ledFast5;
        private LxControl.LxLedControl ledFast2;
        private LxControl.LxLedControl ledFast3;
        private System.Windows.Forms.Label label54;
        private System.Windows.Forms.Label label55;
        private System.Windows.Forms.Label label56;
        private System.Windows.Forms.Label label57;
        private System.Windows.Forms.Label label58;
        private LxControl.LxLedControl ledFast1;
        private System.Windows.Forms.TextBox txtDStay;
        private System.Windows.Forms.TextBox txtCStay;
        private System.Windows.Forms.TextBox txtBStay;
        private System.Windows.Forms.TextBox txtAStay;
        private System.Windows.Forms.TextBox txtRepeat;
        private System.Windows.Forms.TextBox txtInterval;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.GroupBox gbCmdReadGpio;
        private System.Windows.Forms.Button btnWriteGpio4Value;
        private System.Windows.Forms.Button btnWriteGpio3Value;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.RadioButton rdbGpio4High;
        private System.Windows.Forms.RadioButton rdbGpio4Low;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.RadioButton rdbGpio3High;
        private System.Windows.Forms.RadioButton rdbGpio3Low;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.RadioButton rdbGpio2High;
        private System.Windows.Forms.RadioButton rdbGpio2Low;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.RadioButton rdbGpio1High;
        private System.Windows.Forms.RadioButton rdbGpio1Low;
        private System.Windows.Forms.Button btnReadGpioValue;
        private System.Windows.Forms.GroupBox gbCmdBeeper;
        private System.Windows.Forms.Button btnSetBeeperMode;
        private System.Windows.Forms.RadioButton rdbBeeperModeTag;
        private System.Windows.Forms.RadioButton rdbBeeperModeInventory;
        private System.Windows.Forms.RadioButton rdbBeeperModeSlient;
        private System.Windows.Forms.GroupBox gbCmdTemperature;
        private System.Windows.Forms.Button btnGetReaderTemperature;
        private System.Windows.Forms.TextBox txtReaderTemperature;
        private System.Windows.Forms.GroupBox gbCmdVersion;
        private System.Windows.Forms.Button btnGetFirmwareVersion;
        private System.Windows.Forms.TextBox txtFirmwareVersion;
        private System.Windows.Forms.Button btnResetReader;
        public System.Windows.Forms.GroupBox gbCmdBaudrate;
        private CustomControl.HexTextBox htbGetIdentifier;
        private CustomControl.HexTextBox htbSetIdentifier;
        private System.Windows.Forms.Button btSetIdentifier;
        private System.Windows.Forms.Button btGetIdentifier;
        private System.Windows.Forms.GroupBox gbCmdReaderAddress;
        private CustomControl.HexTextBox htxtReadId;
        private System.Windows.Forms.Button btnSetReadAddress;
        private System.Windows.Forms.GroupBox gbTcpIp;
        private System.Windows.Forms.Button btnDisconnectTcp;
        private System.Windows.Forms.TextBox txtTcpPort;
        private System.Windows.Forms.Button btnConnectTcp;
        private CustomControl.IpAddressTextBox ipIpServer;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox gbRS232;
        private System.Windows.Forms.Button btnSetUartBaudrate;
        private System.Windows.Forms.Button btnDisconnectRs232;
        private System.Windows.Forms.ComboBox cmbSetBaudrate;
        private System.Windows.Forms.Label lbChangeBaudrate;
        private System.Windows.Forms.Button btnConnectRs232;
        private System.Windows.Forms.ComboBox cmbBaudrate;
        private System.Windows.Forms.ComboBox cmbComPort;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox gbConnectType;
        private System.Windows.Forms.RadioButton rdbTcpIp;
        private System.Windows.Forms.RadioButton rdbRS232;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.GroupBox gbMonza;
        private System.Windows.Forms.RadioButton rdbMonzaOff;
        private System.Windows.Forms.Button btSetMonzaStatus;
        private System.Windows.Forms.Button btGetMonzaStatus;
        private System.Windows.Forms.RadioButton rdbMonzaOn;
        private System.Windows.Forms.GroupBox gbCmdAntenna;
        private System.Windows.Forms.Button btnGetWorkAntenna;
        private System.Windows.Forms.Button btnSetWorkAntenna;
        private System.Windows.Forms.GroupBox gbCmdAntDetector;
        private System.Windows.Forms.Button btnGetAntDetector;
        private System.Windows.Forms.Button btnSetAntDetector;
        private System.Windows.Forms.GroupBox gbCmdRegion;
        private System.Windows.Forms.Button btnGetFrequencyRegion;
        private System.Windows.Forms.Button btnSetFrequencyRegion;
        private System.Windows.Forms.GroupBox gbCmdOutputPower;
        private System.Windows.Forms.Button btnGetOutputPower;
        private System.Windows.Forms.Button btnSetOutputPower;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Button btReaderSetupRefresh;
        private System.Windows.Forms.Button btRfSetup;
        private System.Windows.Forms.GroupBox groupBox10;
        private System.Windows.Forms.GroupBox groupBox11;
        private System.Windows.Forms.TextBox tbAntDectector;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox20;
        private System.Windows.Forms.CheckBox cbRealWorkant1;
        private System.Windows.Forms.CheckBox cbRealWorkant4;
        private System.Windows.Forms.CheckBox cbRealWorkant3;
        private System.Windows.Forms.CheckBox cbRealWorkant2;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.ComboBox cmbTarget;
        private System.Windows.Forms.Label label98;
        private System.Windows.Forms.ComboBox cmbSession;
        private System.Windows.Forms.Label label97;
        private System.Windows.Forms.GroupBox gbProfile;
        private System.Windows.Forms.Button btGetProfile;
        private System.Windows.Forms.Button btSetProfile;
        private System.Windows.Forms.RadioButton rdbProfile3;
        private System.Windows.Forms.RadioButton rdbProfile2;
        private System.Windows.Forms.RadioButton rdbProfile1;
        private System.Windows.Forms.RadioButton rdbProfile0;
        private System.Windows.Forms.CheckBox cbBufferWorkant1;
        private System.Windows.Forms.CheckBox cbBufferWorkant4;
        private System.Windows.Forms.CheckBox cbBufferWorkant2;
        private System.Windows.Forms.CheckBox cbBufferWorkant3;
        private System.Windows.Forms.GroupBox groupBox23;
        private System.Windows.Forms.TextBox textFreqQuantity;
        private System.Windows.Forms.TextBox TextFreqInterval;
        private System.Windows.Forms.TextBox textStartFreq;
        private System.Windows.Forms.GroupBox groupBox21;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.ComboBox cmbFrequencyEnd;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.ComboBox cmbFrequencyStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.RadioButton rdbRegionChn;
        private System.Windows.Forms.RadioButton rdbRegionEtsi;
        private System.Windows.Forms.RadioButton rdbRegionFcc;
        private System.Windows.Forms.Label label106;
        private System.Windows.Forms.Label label105;
        private System.Windows.Forms.Label label104;
        private System.Windows.Forms.Label label103;
        private System.Windows.Forms.Label label86;
        private System.Windows.Forms.Label label75;
        private System.Windows.Forms.GroupBox gbReturnLoss;
        private System.Windows.Forms.Label label108;
        private System.Windows.Forms.TextBox textReturnLoss;
        private System.Windows.Forms.Button btReturnLoss;
        private System.Windows.Forms.Label label107;
        private System.Windows.Forms.ComboBox cmbWorkAnt;
        private System.Windows.Forms.Label label110;
        private System.Windows.Forms.Label label109;
        private System.Windows.Forms.ComboBox cmbReturnLossFreq;
        private System.Windows.Forms.CheckBox ckClearOperationRec;
        private System.Windows.Forms.CheckBox cbUserDefineFreq;
        private System.Windows.Forms.Timer timerInventory;

        private System.Windows.Forms.Timer totalTime;

        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.ColumnHeader columnHeader7;
        private System.Windows.Forms.ColumnHeader columnHeader8;
        private System.Windows.Forms.GroupBox groupBox22;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.GroupBox groupBox12;
        private System.Windows.Forms.Label label111;
        private System.Windows.Forms.ComboBox comboBox16;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.GroupBox groupBox9;
        private CustomControl.HexTextBox hexTextBox9;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.ComboBox comboBox12;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label71;
        private System.Windows.Forms.Label label99;
        private System.Windows.Forms.Label label100;
        private System.Windows.Forms.Label label101;
        private System.Windows.Forms.Label label102;
        private System.Windows.Forms.ComboBox comboBox13;
        private System.Windows.Forms.ComboBox comboBox14;
        private System.Windows.Forms.ComboBox comboBox15;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label115;
        private System.Windows.Forms.Label label114;
        private System.Windows.Forms.Label label113;
        private System.Windows.Forms.Label label112;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.CheckBox cbRealWorkant8;
        private System.Windows.Forms.CheckBox cbRealWorkant7;
        private System.Windows.Forms.CheckBox cbRealWorkant6;
        private System.Windows.Forms.CheckBox cbRealWorkant5;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox textBox11;
        private System.Windows.Forms.Label label124;
        private System.Windows.Forms.Label label123;
        private System.Windows.Forms.Label label121;
        private System.Windows.Forms.TextBox textBox16;
        private System.Windows.Forms.TextBox textBox15;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox textBox13;
        private System.Windows.Forms.ComboBox comboBox4;
        private System.Windows.Forms.ComboBox comboBox3;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.Label label120;
        private System.Windows.Forms.Label label119;
        private System.Windows.Forms.Label label118;
        private System.Windows.Forms.Label label117;
        private System.Windows.Forms.Label label116;
        private System.Windows.Forms.CheckBox checkBox4;
        private System.Windows.Forms.CheckBox checkBox3;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.GroupBox groupBox24;
        private System.Windows.Forms.RadioButton antType8;
        private System.Windows.Forms.RadioButton antType4;
        private System.Windows.Forms.RadioButton antType1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.Label m_sl_content;
        private System.Windows.Forms.TextBox m_session_max_q;
        private System.Windows.Forms.TextBox m_session_min_q;
        private System.Windows.Forms.TextBox m_session_start_q;
        private System.Windows.Forms.Label m_max_q_content;
        private System.Windows.Forms.Label m_min_q_content;
        private System.Windows.Forms.Label m_start_q_content;
        private System.Windows.Forms.ComboBox m_session_sl;
        private System.Windows.Forms.CheckBox m_session_q_cb;
        private System.Windows.Forms.CheckBox m_session_sl_cb;
        private System.Windows.Forms.RadioButton excel_format_rb;
        private System.Windows.Forms.RadioButton txt_format_rb;
        private RadioButton excel_format_buffer_rb;
        private RadioButton txt_format_buffer_rb;
        private RadioButton excel_format_fast_rb;
        private System.Windows.Forms.ListView lvFastList;
        private RadioButton txt_format_fast_rb;
        private Timer mFastSessionTimer;
        private ImageList sortImageList;
        private RadioButton sessionInventoryrb;
        private RadioButton autoInventoryrb;
        private TableLayoutPanel tableLayoutPanel1;
        private ComboBox mSessionExeTime;
        private Label Duration;
        private TextBox customizedExeTime;
        private Label label127;
        private TextBox mFastExeCount;
        private TextBox mFastIntervalTime;
        private Label label132;
        private Label label131;
        private TextBox mPower5;
        private TextBox mPower4;
        private TextBox mPower3;
        private TextBox mPower2;
        private TextBox mPower1;
        private Label mReserve;
        private Timer mSendFastSwitchTimer;
        private SplitContainer splitContainer1;
        private GroupBox groupBox25;
        private GroupBox groupBox27;
        private GroupBox groupBox26;
        private GroupBox groupBox28;
        private TextBox m_new_fast_inventory_power2;
        private TextBox m_new_fast_inventory_power1;
        private TextBox m_new_fast_inventory_repeat2;
        private TextBox m_new_fast_inventory_repeat1;
        private Label mRepeatPower1;
        private Label mRepeatPower2;
        private Label mRepeat2;
        private Label mRepeat1;
        private CheckBox mDynamicPoll;
        private TextBox m_new_fast_inventory_target_count;
        private Label mTargetQuantity;
        private TextBox m_new_fast_inventory_continue;
        private Label mContiue;
        private TextBox m_new_fast_inventory_optimized;
        private Label mOpitimized;
        private CheckBox m_phase_value;
        private ComboBox m_new_fast_inventory_flag;
        private ComboBox m_new_fast_inventory_session;
        private CheckBox m_new_fast_inventory;
        private Button btnSaveData;
        private Label label122;
        private TextBox txtPStay;
        private Label label130;
        private Label label133;
        private ComboBox cmbAntSelect16;
        private TextBox txtLStay;
        private ComboBox cmbAntSelect13;
        private Label label134;
        private Label label135;
        private TextBox txtOStay;
        private ComboBox cmbAntSelect9;
        private Label label136;
        private Label label137;
        private Label label138;
        private Label label139;
        private ComboBox cmbAntSelect12;
        private TextBox txtNStay;
        private ComboBox cmbAntSelect15;
        private TextBox txtKStay;
        private Label label140;
        private TextBox txtIStay;
        private Label label141;
        private Label label142;
        private TextBox txtMStay;
        private ComboBox cmbAntSelect10;
        private Label label143;
        private ComboBox cmbAntSelect14;
        private Label label144;
        private Label label145;
        private Label label146;
        private ComboBox cmbAntSelect11;
        private TextBox txtJStay;
        private RadioButton antType16;
        private Label label151;
        private Label label152;
        private Label label153;
        private Label label154;
        private TextBox tb_dbm_16;
        private TextBox tb_dbm_15;
        private TextBox tb_dbm_14;
        private TextBox tb_dbm_13;
        private Label label147;
        private Label label148;
        private Label label149;
        private Label label150;
        private TextBox tb_dbm_12;
        private TextBox tb_dbm_11;
        private TextBox tb_dbm_10;
        private TextBox tb_dbm_9;
        private CheckBox cbRealWorkant16;
        private CheckBox cbRealWorkant15;
        private CheckBox cbRealWorkant14;
        private CheckBox cbRealWorkant13;
        private CheckBox cbRealWorkant9;
        private CheckBox cbRealWorkant12;
        private CheckBox cbRealWorkant11;
        private CheckBox cbRealWorkant10;
        private TabPage net_configure_tabPage;
        private LinkLabel net_port_config_tool_linkLabel;
        private Label label165;
        private Label label164;
        private LinkLabel old_net_port_link;
        private Label label163;
        private Button net_clear_btn;
        private GroupBox net_base_settings_gb;
        private TextBox net_base_mod_mac_tb;
        private Label label157;
        private CheckBox net_base_dhcp_enable_cb;
        private TextBox net_base_mod_gateway_tb;
        private TextBox net_base_mod_mask_tb;
        private TextBox net_base_mod_ip_tb;
        private TextBox net_base_mod_name_tb;
        private Label label161;
        private Label label160;
        private Label label158;
        private Label label156;
        private DataGridView dev_dgv;
        private DataGridViewCheckBoxColumn mod_check_Column;
        private DataGridViewTextBoxColumn ModName;
        private DataGridViewTextBoxColumn ModIp;
        private DataGridViewTextBoxColumn ModMac;
        private DataGridViewTextBoxColumn ModVer;
        private DataGridViewTextBoxColumn PcMac;
        private Label label159;
        private ComboBox net_card_combox;
        private GroupBox groupBox30;
        private Label label162;
        private Label net_pc_mask_label;
        private Label net_pc_mac_label;
        private Label net_pc_ip_label;
        private Button net_reset_btn;
        private Button net_setCfg_btn;
        private Button net_getCfg_btn;
        private Button net_search_btn;
        private Button net_refresh_netcard_btn;
        private TabControl port_setting_tabcontrol;
        private TabPage net_port_0_tabPage;
        private CheckBox net_port_1_rand_port_flag_cb;
        private Label label169;
        private ComboBox net_port_1_parity_bit_cbo;
        private Label label168;
        private ComboBox net_port_1_stopbits_cbo;
        private Label label167;
        private ComboBox net_port_1_databits_cbo;
        private Label label166;
        private ComboBox net_port_1_baudrate_cbo;
        private TextBox net_port_1_dest_port_tb;
        private Label label155;
        private Label label129;
        private TextBox net_port_1_dest_ip_tb;
        private ComboBox net_port_1_ip_domain_select_cbo;
        private Label label128;
        private TextBox net_port_1_local_net_port_tb;
        private Label label126;
        private Label label125;
        private ComboBox net_port_1_net_mode_cbo;
        private TabPage net_port_1_tabPage;
        private CheckBox net_port_2_rand_port_flag_cb;
        private Label label170;
        private ComboBox net_port_2_parity_bit_cbo;
        private Label label171;
        private ComboBox net_port_2_stopbits_cbo;
        private Label label172;
        private ComboBox net_port_2_databits_cbo;
        private Label label173;
        private ComboBox net_port_2_baudrate_cbo;
        private TextBox net_port_2_dest_port_tb;
        private Label label174;
        private Label label175;
        private TextBox net_port_2_dest_ip_tb;
        private ComboBox net_port_2_ip_domain_select_cbo;
        private Label label176;
        private TextBox net_port_2_local_net_port_tb;
        private Label label177;
        private Label label178;
        private ComboBox net_port_2_net_mode_cbo;
        private CheckBox net_port_1_enable_cb;
        private CheckBox net_port_2_enable_cb;
    }
}

