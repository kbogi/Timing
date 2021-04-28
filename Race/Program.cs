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
        static GateConfig[] gateConfigs;

        static List<Thread> readerThreads = new List<Thread>();
        static int port;
        static string connectionString;

        static void Read(GateConfig gateConfig){
            string ipAddress = gateConfig.ipAddress;
            int[] antenas = gateConfig.antenas;
            DeviceReader deviceReader;
            while(true){
                try{
                    deviceReader = new DeviceReader(ipAddress, port);

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
                    deviceReader.Connect();


                    deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                    Thread.Sleep(50);
                    
                    deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                    Thread.Sleep(50);
                    deviceReader.reader.SetBeeperMode(device.settings.btReadId, 0x00);
                    
                    Console.WriteLine("Reading {0}, {1}", ipAddress, port);
                    while (true) {
                        abortAfter = DateTime.Now.ToFileTime() + 10000000;
                    
                        //Console.WriteLine(DateTime.Now.ToLongTimeString() + ": Loop started");
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
                        Thread.Sleep(50);
                    }


                } catch (Exception e) {
                    Console.WriteLine("Error occured: " + e.Message);
                }
            } 
        }

        static void runReaders() {
            foreach (GateConfig gateConfig in gateConfigs) {
                Thread thr = new Thread(() => Read(gateConfig));
                thr.Start();
                readerThreads.Add(thr);
            }
        }


        static void Main(string[] args)
        {
            var configurationBuilder = new ConfigurationBuilder()
                .AddJsonFile("config.json")
                .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();
            connectionString = configuration["Database"];

            // Application code should start here.
            port = 4001;
            db = new Database(connectionString);
            try{
                List<GateConfig> gateConfigList = new List<GateConfig>();
                using (MySqlDataReader rdr = db.reader("SELECT * FROM gate")){
                    while (rdr.Read())
                    {
                        string ipAddress = rdr.GetString(1);
                        string[] antenaStrings = rdr.GetString(2).Split(',');
                        int[] antenas = new int[antenaStrings.Length];
                        for(int i = 0; i < antenaStrings.Length; i++){
                            antenas[i] = Int32.Parse(antenaStrings[i]) - 1;
                        }
                        gateConfigList.Add(new GateConfig(ipAddress, antenas));

                        Console.WriteLine("{0} {1} {2}", rdr.GetString(0), rdr.GetString(1), rdr.GetString(2));
                    }
                }
                gateConfigs = gateConfigList.ToArray();
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
