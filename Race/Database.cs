using System;
using MySql.Data.MySqlClient;
using System.Collections.Generic;
namespace Race {

    class Database {
        private Dictionary<string, DateTime> passMap = new();
        private string connectionString;

        public Database(string config)
        {
            connectionString = config;
        }

        public void exec(string query) {
            var con = new MySqlConnection(connectionString);
            con.Open();


            var cmd = new MySqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public MySqlDataReader reader(string query){
            var con = new MySqlConnection(connectionString);
            con.Open();
            var cmd = new MySqlCommand(query, con);
            var reader = cmd.ExecuteReader();
            return reader;
        }

        public bool keepValue(string code, string ipAddress, DateTime pass, int filterDelay){
            string key = ipAddress + ":" + code;
            bool keep = true;
            try{
                DateTime lastPass = passMap[key];
                DateTime moved = lastPass.Add(new TimeSpan(0,0,filterDelay));
                keep = moved.CompareTo(pass) < 0;
            } catch {}
            try{
                passMap.Add(key, pass);
            } catch {
                passMap[key] = pass;
            }
            return keep;
        }

        public void saveValue(string code, string ipAddress, string pointType, int filterDelay){
            DateTime now = DateTime.Now;
            if(keepValue(code, ipAddress, now, filterDelay)){
                Console.WriteLine("{1}: {2} tag: {0}",  code, DateTime.Now, ipAddress);
                long unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();
                this.exec( string.Format(
                    "INSERT INTO rawdata (hw_ip, rf_tag, rd_time) VALUES ('{0}', '{1}', from_unixtime({2}))",
                    ipAddress, code, unixTime
                ));
            }
        }
    }
}
