using MySql.Data.MySqlClient;
using System;

namespace GameServer.Tool
{
    internal class ConnectionHelper
    {
        private const string CONNECTIONTODB = "datasource=db-gamedata.cplkhciexfnk.ap-south-1.rds.amazonaws.com;port=3306;database=gamedata;UserID=admin;Password=adminadmin;";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONTODB);
            conn.Open();
            return conn;
        }
        public static void CloseConnection(MySqlConnection connection)
        {
            if (connection != null)
                connection.Close();
            else
                Console.WriteLine("MysqlConnection is null");
        }
    }
}
