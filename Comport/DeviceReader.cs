using System;
using System.Net;
using Reader;

namespace Race {
    class DeviceReader {
        private string ipAddress;

        public Device device;
        private DeviceMutator mutator;
        private int port;
        public Reader.ReaderMethod reader;
        private string comPort;

        public bool isSerial = false;

        public DeviceReader(string ipAddress, int port){
            this.ipAddress = ipAddress;
            this.port = port;
            this.reader = new Reader.ReaderMethod();
            this.device = new Device();
            this.mutator = new DeviceMutator(this.device, reader);
            this.reader.AnalyCallback = this.mutator.ApplyData;
            this.reader.ReceiveCallback = this.ReceiveData;
            this.reader.SendCallback = this.SendData;
            this.reader.ErrCallback = this.ReceiveError;
        }

        public DeviceReader(string comPort){
            this.comPort = comPort;
            this.isSerial = true;
            this.reader = new Reader.ReaderMethod();
            this.device = new Device();
            this.mutator = new DeviceMutator(this.device, reader);
            this.reader.AnalyCallback = this.mutator.ApplyData;
            this.reader.ReceiveCallback = this.ReceiveData;
            this.reader.SendCallback = this.SendData;
            this.reader.ErrCallback = this.ReceiveError;
        }

        public void Connect() {
            if(isSerial){
                this.reader.OpenCom(this.comPort, 115200);
                string message = "Connected " + this.comPort;
                this.log(message);
            } else {
                //Processing Tcp to connect reader.
                IPAddress ipAddress = IPAddress.Parse(this.ipAddress);
                this.reader.ConnectServer(ipAddress, this.port);
                string message = "Connected " + this.ipAddress + ":" + this.port;
                this.log(message);
            }
        }

        public void Disconnect() {
            this.reader.SignOut();
        }

        public void setCallback(ReadCallback callback) {
            mutator.ReadCallback = callback;
        }
        
        private void ReceiveData(object sender, TransportDataEventArgs e)
        {
                string strLog = e.Tx ? "Send: ":"Recv: " + ReaderUtils.ToHex(e.Data, "", " ");
                //Console.WriteLine("<--  {0}", strLog);
                this.log(strLog);
        }
        
        private void SendData(object sender, byte[] btArySendData)
        {
            string message = ReaderUtils.ByteArrayToString(btArySendData, 0, btArySendData.Length);

            this.log("Sent data: " + message);      
        }
        
        private void ReceiveError(object sender, ErrorReceivedEventArgs e)
        {
            this.log("Error: " + e.ErrStr);      
        }

        private void log(string message){
            //Console.WriteLine(message);
        }
    }
}