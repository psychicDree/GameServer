using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Model;
using GameServer.Tool;
using MySql.Data.MySqlClient;

namespace GameServer.Servers
{
    internal class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message message = new Message();
        private User user;
        private Result result;
        private Room room;
        public Room Room { set => room = value; get => room; }

        private MySqlConnection mySqlConnection;
        public MySqlConnection MySqlConnection =>mySqlConnection;
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            this.mySqlConnection = ConnectionHelper.Connect();
        }

        public void SendFromClient(ActionCode actionCode, string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            clientSocket.Send(bytes);
        }
        public void StartRecieving()
        {
            if (clientSocket == null || clientSocket.Connected == false) return;
            clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallback, null);
        }

        private void ReceiveCallback(IAsyncResult ar)
        {
            int count = clientSocket.EndReceive(ar);
            if (count == 0)
            {
                CloseReceiving();
            }
            message.ReadMessage(count, OnProcessMessage);
            StartRecieving();
        }

        private void CloseReceiving()
        {
            ConnectionHelper.CloseConnection(mySqlConnection);
            clientSocket?.Close();
            server.RemoveClient(this);
            room?.QuitRoom(this);
        }

        public void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }

        public void SetUserData(User user, Result result)
        {
            this.user = user;
            this.result = result;
        }

        public string GetUserData()
        {
            return user.Id +"," + user.Username + "," + result.TotalCount + "," + result.WinCount;
        }

        public int GetUserId()
        {
            return user.Id;
        }
        public bool IsHouseOwner()
        {
            return Room.IsHouseOwner(this);
        }
    }
}
