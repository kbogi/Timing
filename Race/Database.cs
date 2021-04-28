using System;
using MySql.Data.MySqlClient;

namespace Race {
    class Database {
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

        public void saveValue(string value){
            Console.WriteLine("tag: " + value);
            this.exec( "INSERT INTO record (code) VALUES ('" + value + "')");
        }
    }
}
