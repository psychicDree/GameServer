using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    internal class ConnectionHelper
    {
        private const string CONNECTIONTODB = "datasource=database-1.c3eqnx4aavdb.ap-south-1.rds.amazonaws.com;port=3306;database=gameserver;UserID=admin;Password=rootadmin;";

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
