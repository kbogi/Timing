using System;
using MySql.Data.MySqlClient;
using System.Threading;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Reader;

namespace Race
{
    class Program
    {
        const byte ANTENA_DEFAULT_POWER = 1;
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
                        try{
                            db.saveValue(code, ipAddress, gateConfig.pointType, gateConfig.filterDelay);
                        }  catch (Exception e) {
                            Console.WriteLine("Write error occured, message: {0}", e.Message);
                        }
                    });
                    deviceReader.Connect();

                    deviceReader.reader.GetFirmwareVersion(device.settings.btReadId);
                    Thread.Sleep(50);
                    
                    deviceReader.reader.ResetInventoryBuffer(device.settings.btReadId);
                    Thread.Sleep(50);
                    deviceReader.reader.SetBeeperMode(device.settings.btReadId, 0);
                    Thread.Sleep(50);

                    {
                        byte[] powers = new byte[Device.getAntenaCount(gateConfig.mode)];
                        Array.Clear(powers, 0, powers.Length);
                        for(int i = 0; i< gateConfig.antenas.Length && i< gateConfig.antenaPowers.Length; i++){          
                            byte antena = (byte) gateConfig.antenas[i];
                            byte antenaPower = gateConfig.antenaPowers[i];
                            powers[antena] = antenaPower;
                        }
                        deviceReader.reader.SetOutputPower(device.settings.btReadId, powers);
                    }
                    Thread.Sleep(50);

                    Console.WriteLine("Reading {0}:{1} {2}", ipAddress, port, gateConfig.mode);
                    deviceReader.reader.SetWorkAntenna(device.settings.btReadId, (byte) 0);
                    Thread.Sleep(100);
                    switch(gateConfig.mode){
                        case "multi4":
                        case "multi8":
                            int antenaCount = Device.getAntenaCount(gateConfig.mode);
                            byte[] payload = new byte[(antenaCount * 2) + 2];
                            
                            int index = 0;
                            for(int i = 0; i < antenaCount; i++){
                                byte antena = (byte)(((gateConfig.gates & (1 << i)) != 0) ? 1 : 0);
                                payload[index++] = antena; // antena
                                payload[index++] = antena; // repeats
                            }
                            payload[index++] = 0xFF;
                            payload[index++] = 0xFF;

                            Console.WriteLine(ReaderUtils.ByteArrayToString(payload, 0, index));

                            deviceReader.setReadFinishedCallback(() => {
                                deviceReader.reader.FastSwitchInventory(device.settings.btReadId, payload);
                                Console.WriteLine("{1}: New read started", gateConfig.ipAddress);
                            });
                            while(true){
                                deviceReader.reader.FastSwitchInventory(device.settings.btReadId, payload);
                                Thread.Sleep(1000);
                            }
                            break;
                        case "buffer": 
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
                        break;
                        default:
                            Console.WriteLine("Neznámý mod: {0}", gateConfig.mode);
                        break;
                    }


                } catch (Exception e) {
                    Console.WriteLine("Error occured: " + e.Message);
                }
                Thread.Sleep(100);
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
                .AddJsonFile("hw_query.json")
                .AddEnvironmentVariables();
            var configuration = configurationBuilder.Build();
            connectionString = configuration["Database"];
            string hwQuery = configuration["hwQuery"];

            // Application code should start here.
            port = 4001;
            db = new Database(connectionString);
            try{
                List<GateConfig> gateConfigList = new List<GateConfig>();
                using (MySqlDataReader rdr = db.reader(hwQuery)){
                    while (rdr.Read())
                    {
                        string ipAddress = rdr.GetString(0);
                        List<int> antenaList = new();
                        List<byte> antenaDbList = new();
                        int dbIndex = 0;
                        for(int i = 0; i < 8; i++){
                            dbIndex++;
                            if(!rdr.IsDBNull(dbIndex) && rdr.GetInt32(dbIndex) > 0){
                                antenaList.Add(i);
                                int powerDbIndex = dbIndex+8;
                                if(!rdr.IsDBNull(powerDbIndex)){
                                    antenaDbList.Add(rdr.GetByte(powerDbIndex));
                                } else {
                                    antenaDbList.Add(ANTENA_DEFAULT_POWER);                                    
                                }
                                Console.WriteLine("Adding: " + (i + 1));
                            }
                        }
                        dbIndex+=8;
                        int filterDelay = rdr.GetInt32(++dbIndex);
                        string mode = rdr.GetString(++dbIndex);
                        string pointType = rdr.GetString(++dbIndex);
                        Console.WriteLine("hw_mode: {0}, tp_type: {1}, filter: {2}s",mode, pointType, filterDelay);
                        gateConfigList.Add(new GateConfig(ipAddress, antenaList.ToArray(), antenaDbList.ToArray(), mode, pointType, filterDelay));
                    }
                }
                gateConfigs = gateConfigList.ToArray();
            } catch (MySqlException e){
                Console.WriteLine("Unable to connect to db: " + e.Message);
                throw e;
            }

            runReaders();
        }
    }
}
