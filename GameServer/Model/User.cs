using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    internal class User
    {
        private int Id { get; set; }
        private string Username { get; set; }
        private string Password { get; set; }

        public User(int id, string username, string password)
        {
            this.Id = id;
            this.Username = username;
            this.Password = password;
        }
    }
}
