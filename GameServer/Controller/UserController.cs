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
    }
}
