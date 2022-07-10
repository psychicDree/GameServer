using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Model;
using MySql.Data.MySqlClient;

namespace GameServer.DAO
{
    internal class UserDAO
    {
        private MySqlDataReader reader;
        public User VerifyUser(MySqlConnection connection, string username, string password)
        {
            try
            {
                MySqlCommand cmd =
                    new MySqlCommand("select * from user where username=@username and password=@password", connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    User user = new User(id, username, password);
                    return user;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                reader.Close();
            }

            return null;
        }

        public bool GetUserByUsername(MySqlConnection connection, string username)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd = 
                    new MySqlCommand("select * from user where username=@username ", connection);
                cmd.Parameters.AddWithValue("username", username);
                reader = cmd.ExecuteReader();
                if (reader.HasRows) return true;
                else return false;
            }
            catch (Exception e) { Console.WriteLine(e); }
            finally{ reader.Close(); }

            return false;
        }

        public void AddUser(MySqlConnection connection, string username, string password)
        {
            try
            {
                MySqlCommand cmd = 
                    new MySqlCommand("insert into user set username=@username,password=@password", connection);
                cmd.Parameters.AddWithValue("username", username);
                cmd.Parameters.AddWithValue("password", password);
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
