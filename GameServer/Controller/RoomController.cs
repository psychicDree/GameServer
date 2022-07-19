using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Model;
using GameServer.Servers;

namespace GameServer.Controller
{
    internal class RoomController : BaseController
    {
        public RoomController()
        {
            requestCode = RequestCode.Room;
        }

        public string CreateRoom(string data, Client client, Servers.Server server)
        {
            server.CreateRoom(client);
            return ((int)ReturnCode.Success).ToString();
        }

        public string ListRoom(string data, Client client, Servers.Server server)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var room in server.GetRoomList())
            {
                if (room.IsWaitingJoin()) sb.Append(room.GetHouseOwnerData() + "|");
            }

            if (sb.Length == 0) sb.Append("0");
            else sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public string JoinRoom(string data, Client client, Servers.Server server)
        {
            int id = int.Parse(data);
            Room room = server.GetRoomById(id);
            if(room==null) return ((int)ReturnCode.NotFound).ToString();
            else if(!room.IsWaitingJoin()) return ((int)ReturnCode.Failed).ToString();
            else
            {
                room.AddClient(client);
                string roomData = room.GetRoomData();
                room.BroadCastMessage(client, ActionCode.UpdateRoom, roomData);
                return ((int)ReturnCode.Success).ToString() + "-" + roomData;
            }
        }
    }
}
