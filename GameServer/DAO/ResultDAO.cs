using GameServer.Model;
using MySql.Data.MySqlClient;
using System;

namespace GameServer.DAO
{
    internal class ResultDAO
    {
        public Result GetResultByUserId(MySqlConnection connection, int userId)
        {
            MySqlDataReader reader = null;
            try
            {
                MySqlCommand cmd =
                    new MySqlCommand("select * from result where userid=@userid ", connection);
                cmd.Parameters.AddWithValue("userid", userId);
                reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    int id = reader.GetInt32("id");
                    int totalcount = reader.GetInt32("totalcount");
                    int wincount = reader.GetInt32("wincount");
                    Result result = new Result(id, userId, totalcount, wincount);
                    return result;
                }
                else
                {
                    Result result = new Result(-1, userId, 0, 0);
                    return result;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                if (reader != null)
                    reader.Close();
            }

            return null;
        }
    }
}
