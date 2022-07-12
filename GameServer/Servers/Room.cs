using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    internal class Room
    {
        enum RoomState{ WaitingJoin, WaitingBattle, Battle, End }
        private List<Client> clientsInRoom = new List<Client>();
        private RoomState state = RoomState.WaitingJoin;
        private Server server;
        public Room(Server server)
        {
            this.server = server;
        }

        public void AddClient(Client client)
        {
            clientsInRoom.Add(client);
            client.Room = this;
            if (clientsInRoom.Count >= 2) state = RoomState.WaitingBattle;
        }

        public void QuitRoom(Client client)
        {
            server.RemoveRoom(this);
            if(client == clientsInRoom[0])
            {
                CloseRoom();
            }
            else
            {
                clientsInRoom.Remove(client);
            }
                
        }

        private void CloseRoom()
        {
            
        }

        public bool IsWaitingJoin()
        {
            return state == RoomState.WaitingJoin;
        }
        public string GetHouseOwnerData()
        {
            return clientsInRoom[0].GetUserData();
        }

        public int GetRoomId()
        {
            if(clientsInRoom.Count > 0)
                return clientsInRoom[0].GetUserId();
            return -1;
        }

        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var client in clientsInRoom)
            {
                sb.Append(client.GetUserData() + "|");
            }

            if (sb.Length > 0) sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public void BroadCastMessage(Client otherClient, ActionCode actionCode, string data)
        {
            foreach(var client in clientsInRoom)
            {
                if(otherClient !=client) server.SendResponse(client, actionCode, data);
            }
        }
        public bool IsHouseOwner(Client client)
        {
            return client == clientsInRoom[0];
        }
        Thread timerThread = null;
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }
        private void RunTimer()
        {
            Thread.Sleep(1000);
            for (int i = 3; i > 0; i--)
            {
                BroadCastMessage(null, ActionCode.StartTimer, i.ToString());
                Thread.Sleep(1000);
            }
            BroadCastMessage(null, ActionCode.StartPlay, "");
        }
    }
}
