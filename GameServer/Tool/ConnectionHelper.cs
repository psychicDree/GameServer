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
        private const string CONNECTIONTODB =
            "datasource=remotemysql.com;port=3306;database=253U0l32wa;UserID=253U0l32wa;Password=WLCv8ca8Bz;";

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
