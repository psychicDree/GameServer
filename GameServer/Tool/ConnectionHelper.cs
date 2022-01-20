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
            "datasource=db-gameserver01.cc6u8pgmf3md.ap-south-1.rds.amazonaws.com;port=3306;database=sys;user=admin;pwd=admin123;";

        public static MySqlConnection Connect()
        {
            MySqlConnection conn = new MySqlConnection(CONNECTIONTODB);
            conn.Open();
            return conn;
        }
    }
}
