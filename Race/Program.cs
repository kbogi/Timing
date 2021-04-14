using System;
using MySql.Data.MySqlClient;
using System.Threading;

namespace Race
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string connectionString = @"server=localhost;user=root;password=heslo;database=timing";

            var db = new Database(connectionString);

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

            try{
                var deviceReader = new DeviceReader("192.168.100.103", 4001);

                deviceReader.Connect();

                var device = deviceReader.device;

                deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                
                deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                
                for (int i = 0; i< 100; i++) {
                    deviceReader.reader.GetInventoryBufferTagCount(device.settings.btReadId);
                    //deviceReader.reader.GetInventoryBuffer(device.settings.btReadId);
                        
                    deviceReader.reader.GetAndResetInventoryBuffer(device.settings.btReadId);
                    
                    Thread.Sleep(2000);
                }


            } catch (Exception e) {
                Console.WriteLine("Unable to connect to device: " + e.Message);
            }
        }
    }
}
