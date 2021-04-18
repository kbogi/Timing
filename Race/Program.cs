using System;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Race
{
    class Program
    {
        static Database db;
        static DeviceReader deviceReader;

        static Int64 abortAfter = Int64.MaxValue; 

        static void Read(){
            try{
                deviceReader = new DeviceReader("192.168.100.103", 4001);

                deviceReader.Connect();

                var device = deviceReader.device;

                deviceReader.setCallback((string code) => {
                    db.exec("INSERT INTO chip (code, count) VALUES (\"" + code + "\", 1) ON DUPLICATE KEY UPDATE count = count + 1");
                });


                deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                Thread.Sleep(50);
                
                deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                Thread.Sleep(50);
                deviceReader.reader.SetBeeperMode(device.settings.btReadId, 0x01);
                
                for (int i = 0; i< 100; i++) {
                    abortAfter = DateTime.Now.ToFileTime() + 10000000;
                
                    Console.WriteLine(DateTime.Now.ToLongTimeString() + ": Loop started");
                    deviceReader.reader.Inventory(device.settings.btReadId, 0xFF);
                    Thread.Sleep(50);
                    deviceReader.reader.GetInventoryBufferTagCount(device.settings.btReadId);
                    Thread.Sleep(50);
                    //deviceReader.reader.GetInventoryBuffer(device.settings.btReadId);
                    Thread.Sleep(50);
                    deviceReader.reader.GetAndResetInventoryBuffer(device.settings.btReadId);
                    Thread.Sleep(100);
                }


            } catch (Exception e) {
                Console.WriteLine("Error occured: " + e.Message);
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string connectionString = @"server=localhost;user=root;password=heslo;database=timing";

            db = new Database(connectionString);

            try{
                db.saveValue("Test func");

                using (MySqlDataReader rdr = db.reader("SELECT * FROM record")){
                    while (rdr.Read())
                    {
                        Console.WriteLine("{0} {1} {2}", rdr.GetInt32(0), rdr.GetString(1),
                                rdr.GetString(2));
                    }
                }
            } catch (MySqlException e){
                Console.WriteLine("Unable to connect to db: " + e.Message);
            }

            // Creating and initializing threads
            Thread thr = new Thread(new ThreadStart(Read));
            thr.Start();
    
            while(true){
                Int64 now =  DateTime.Now.ToFileTime();
                if (now.CompareTo(abortAfter) > 0) {
                    Thread.Sleep(1000);
                    Console.WriteLine("Not responding, aborting");                    
                    thr.Abort();

                    Thread.Sleep(100);
                    deviceReader.Disconnect();

                    thr = new Thread(new ThreadStart(Read));
                    thr.Start();
    
                }
            }
            // Abort thr thread
            // Using Abort() method
        }
    }
}
