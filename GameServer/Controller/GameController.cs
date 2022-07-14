using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    internal class GameController : BaseController
    {
        public GameController()
        {
            requestCode  = RequestCode.Game;
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
            ActionCode actionCode = ActionCode.Move;
            room.BroadCastMessage(client, ActionCode.Move, data);
            return null;
        }
    }
}
