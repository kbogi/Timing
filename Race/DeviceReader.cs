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

        public DeviceReader(string ipAddress, int port){
            this.ipAddress = ipAddress;
            this.port = port;
            this.reader = new Reader.ReaderMethod();
            this.device = new Device();
            this.mutator = new DeviceMutator(this.device, reader);
            this.reader.AnalyCallback = this.mutator.ApplyData;
            this.reader.ReceiveCallback = this.ReceiveData;
            this.reader.SendCallback = this.SendData;
        }

        public void Connect() {
            //Processing Tcp to connect reader.
            IPAddress ipAddress = IPAddress.Parse(this.ipAddress);
            string fuck = "";
            this.reader.ConnectServer(ipAddress, this.port, out fuck);
            string message = "Connected " + this.ipAddress + ":" + this.port;
            this.log(message);
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

        private void log(string message){
            Console.WriteLine(message);
        }
    }
}