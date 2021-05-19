using System;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Race
{
    class Program
    {
        static Database db;

        static Int64 abortAfter = Int64.MaxValue; 
        
        static string comPort;

        static List<Thread> readerThreads = new List<Thread>();
        static string connectionString;

        static void Read(){
            DeviceReader deviceReader;
            while(true){
                try{
                    deviceReader = new DeviceReader(comPort);

                    var device = deviceReader.device;

                    deviceReader.setCallback((string code) => {
                        try{
                            db.saveValue(code, comPort);
                        }  catch (Exception e) {
                            Console.WriteLine("Write error occured, message: {0}", e.Message);
                        }
                    });
                    deviceReader.Connect();


                    deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                    Thread.Sleep(50);
                    
                    deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                    Thread.Sleep(50);
                    deviceReader.reader.SetBeeperMode(device.settings.btReadId, 0x00);
                    
                    Console.WriteLine("Reading {0}", comPort);
                    deviceReader.reader.SetWorkAntenna(device.settings.btReadId, (byte) 0);
                    Thread.Sleep(100);

                    deviceReader.setReadFinishedCallback(() => {
                        deviceReader.reader.InventoryReal(device.settings.btReadId, (byte) 255);
                        Console.WriteLine('New read started');
                    });
                    while (true) {
                    
                        deviceReader.reader.InventoryReal(device.settings.btReadId, (byte) 255);
                        //Console.WriteLine(DateTime.Now.ToLongTimeString() + ": Loop started");
                        /*
                        for(int i = 0; i < 19; i++){
                            int antena = antenas[i % antenas.Length];
                            deviceReader.reader.SetWorkAntenna(device.settings.btReadId, (byte) antena);
                            Thread.Sleep(25);
                            deviceReader.reader.Inventory(device.settings.btReadId, 0xFF);
                            Thread.Sleep(25);
                        }
                        //Thread.Sleep(50);
                        //deviceReader.reader.GetInventoryBuffer(device.settings.btReadId);
                        deviceReader.reader.GetAndResetInventoryBuffer(device.settings.btReadId);
                        */
                        Thread.Sleep(5000);
                    }


                } catch (Exception e) {
                    Console.WriteLine("Error occured: " + e.Message);
                }
            } 
        }


        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();
            connectionString = configuration["Database"];
            comPort = configuration["Com"];

            db = new Database(connectionString);
            try{
                using (MySqlDataReader rdr = db.reader("SELECT 1")){
                    while (rdr.Read())
                    {
                        Console.WriteLine("Connected to db");
                    }                    
                }
            } catch (MySqlException e){
                Console.WriteLine("Unable to connect to db: " + e.Message);
                throw e;
            }
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
            
            Read();
    
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
