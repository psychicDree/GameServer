using System;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    internal class GameController : BaseController
    {
        public GameController()
        {
            requestCode = RequestCode.Game;
        }
        public string StartGame(string data, Client client, Server server)
        {
            bool isHouseOwner = client.IsHouseOwner();
            if (isHouseOwner)
            {
                Room room = client.Room;
                room.BroadCastMessage(client, ActionCode.StartGame, ((int)ReturnCode.Success).ToString());
                room.StartTimer();
                return ((int)ReturnCode.Success).ToString();
            }
            else return ((int)ReturnCode.Failed).ToString();
        }

        public string Move(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadCastMessage(client, ActionCode.Move, data);
            return null;
        }

        public string Shoot(string data, Client client, Server server)
        {
            Room room = client.Room;
            room.BroadCastMessage(client,ActionCode.Shoot,data);
            return null;
        }

        public string Attack(string data, Client client, Server server)
        {
            int damage = int.Parse(data);
            Console.WriteLine(damage);
            Room room = client.Room;
            room.Takedamage(damage, client);
            return null;
        }
    }
}
