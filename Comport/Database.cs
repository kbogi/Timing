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

        public bool keepValue(string code, string ipAddress, DateTime pass){
            string key = ipAddress + ":" + code;
            bool keep = true;
            try{
                DateTime lastPass = passMap[key];
                DateTime moved = lastPass.Add(new TimeSpan(0,0,5));
                keep = moved.CompareTo(pass) < 0;
            } catch {}
            try{
                passMap.Add(key, pass);
            } catch {
                passMap[key] = pass;
            }
            return keep;
        }

        public void saveValue(string code, string ipAddress){
            DateTime now = DateTime.Now;
            if(keepValue(code, ipAddress, now)){
                Console.WriteLine("tag: " + code);
                long unixTime = ((DateTimeOffset)now).ToUnixTimeSeconds();
                this.exec( "INSERT INTO rawdata (hw_ip, rf_tag, rd_time) VALUES ('" + 
                    ipAddress + "', '" + code + "', from_unixtime(" + unixTime + "))");
            }
        }
    }
}
