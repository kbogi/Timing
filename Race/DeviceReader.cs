using System;
using System.Net;
using Util;

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
            this.mutator = new DeviceMutator(this.device);
            this.reader.AnalyCallback = this.mutator.ApplyData;
            this.reader.ReceiveCallback = this.ReceiveData;
            this.reader.SendCallback = this.SendData;
        }

        public void Connect() {
            //Processing Tcp to connect reader.
            IPAddress ipAddress = IPAddress.Parse(this.ipAddress);
            this.reader.ConnectServer(ipAddress, this.port);
            string message = "Connected " + this.ipAddress + ":" + this.port;
            this.log(message);
        }
        
        private void ReceiveData(byte[] btAryReceiveData)
        {

            string message = CCommondMethod.ByteArrayToString(btAryReceiveData, 0, btAryReceiveData.Length);

            this.log("Received data: " + message);
        }
        
        private void SendData(byte[] btArySendData)
        {
            string message = CCommondMethod.ByteArrayToString(btArySendData, 0, btArySendData.Length);

            this.log("Sent data: " + message);      
        }

        private void log(string message){
            Console.WriteLine(message);
        }
    }
}