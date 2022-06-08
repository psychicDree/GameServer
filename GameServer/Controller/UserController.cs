using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.DAO;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    internal class UserController : BaseController
    {
        private UserDAO userDAO = new UserDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }

        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser(client.MySqlConnection, strs[0], strs[1]);
            if (user == null)
            {
                return ((int)ReturnCode.Failed).ToString();
            }
            else
            {
                return ((int)ReturnCode.Success).ToString();
            }
        }

        public string Register(string data, Client client, Server server)
        {
            string[] str = data.Split(',');
            var username = str[0];
            var password = str[1];
            var response = userDAO.GetUserByUsername(client.MySqlConnection, username);
            if(response) return ((int)ReturnCode.Failed).ToString();
            userDAO.AddUser(client.MySqlConnection,username,password);
            return ((int)ReturnCode.Success).ToString();
        }
    }
}
