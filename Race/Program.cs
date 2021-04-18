using System;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Configuration;
using System.Collections.Specialized;
using System.Collections.Generic;

namespace Race
{
    class Program
    {
        static Database db;
        static DeviceReader deviceReader;

        static Int64 abortAfter = Int64.MaxValue; 
        static string[] ipAddresses;

        static List<Thread> readerThreads = new List<Thread>();
        static int port;

        static void Read(string ipAddress){
            try{
                deviceReader = new DeviceReader(ipAddress, port);

                deviceReader.Connect();

                var device = deviceReader.device;

                deviceReader.setCallback((string code) => {
                    int state = 0;
                    try{
                        db.exec("INSERT INTO chip (code, count) VALUES (\"" + code + "\", 1) ON DUPLICATE KEY UPDATE count = count + 1");
                        state++;
                        db.saveValue(code);
                        state++;
                    }  catch (Exception e) {
                        Console.WriteLine("Write error occured, state: {0} message: {1}", state, e.Message);
                    }
                });


                deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                Thread.Sleep(50);
                
                deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                Thread.Sleep(50);
                deviceReader.reader.SetBeeperMode(device.settings.btReadId, 0x00);
                
                while (true) {
                    abortAfter = DateTime.Now.ToFileTime() + 10000000;
                
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + ": Loop started");
                    for(int i = 0; i < 10; i++){
                        deviceReader.reader.Inventory(device.settings.btReadId, 0xFF);
                        Thread.Sleep(50);
                    }
                    //Thread.Sleep(50);
                    //deviceReader.reader.GetInventoryBuffer(device.settings.btReadId);
                    deviceReader.reader.GetAndResetInventoryBuffer(device.settings.btReadId);
                    //Thread.Sleep(50);
                }


            } catch (Exception e) {
                Console.WriteLine("Error occured: " + e.Message);
            }
        }

        static void runReaders() {
            foreach (string ipAddress in ipAddresses) {
                Thread thr = new Thread(() => Read(ipAddress));
                thr.Start();
                readerThreads.Add(thr);
                Console.WriteLine("Reading {0}, {1}", ipAddress, port);
            }
        }

        static void Main(string[] args)
        {
            ipAddresses = ConfigurationManager.AppSettings.Get("ip").Split(',');
            port = Int32.Parse(ConfigurationManager.AppSettings.Get("port"));

            string connectionString = ConfigurationManager.AppSettings.Get("db");

            db = new Database(connectionString);
            /*
            try{
                using (MySqlDataReader rdr = db.reader("SELECT * FROM record")){
                    while (rdr.Read())
                    {
                        Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                                rdr.GetString(2));
                    }
                }
            } catch (MySqlException e){
                Console.WriteLine("Unable to connect to db: " + e.Message);
            }*/

            runReaders();        
    
            /*
            while(true){
                Int64 now =  DateTime.Now.ToFileTime();
                if (now.CompareTo(abortAfter) > 0) {
                    Thread.Sleep(1000);
                    Console.WriteLine("Not responding, aborting");                    
                    thr.Abort();

                    Thread.Sleep(100);
                    deviceReader.Disconnect();

                    thr = new Thread(new ParameterizedThreadStart(Read));
                    thr.Start();
                }
            }*/
        }
    }
}
