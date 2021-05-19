namespace Race {
    class Device {        
        public ReaderSetting settings = new ReaderSetting();
        public OperateTagISO18000Buffer operateTagISO18000Buffer = new OperateTagISO18000Buffer();
        
        //Before inventory, you need to set working antenna to identify whether the inventory operation is executing.
        public bool inventory = false;
        public bool isReading = false;
        //Identify whether reckon the command execution time, and the current inventory command needs to reckon time.
        public bool reckonTime = false;
        //Real time inventory locking operation.
        public bool lockTab = false;
        //ISO18000 tag continuously inventory mark.
        public bool m_bContinue = false;
        //Whether display the serial monitoring data.
        public bool displayLog = false;
        //Record the number of ISO18000 tag written loop time.
        public int loopTimes = 0;
        //Record the number of ISO18000 tag's written characters.
        public int bytes = 0;
        //Record the number of ISO18000 tag have been written loop time.
        public int loopedTimes = 0;
        //Real time inventory times.
        public int total = 0;
        //Frequency of list updating.
        public int realRate = 20;
        //Record quick poll antenna parameter.
        public byte[] aryData = new byte[10];
        //Record the total number of quick poll times.
        public int switchTotal = 0;
        public int switchTime = 0;

        public int receiveFlag = 0;

        public int fastExeCount = 0;

        public bool getOutputPower = false;
        public bool setOutputPower = false;
        public bool setWorkAnt = false;
        public bool getWorkAnt = false;
    }
}