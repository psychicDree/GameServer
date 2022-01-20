using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Tool;
using MySql.Data.MySqlClient;

namespace GameServer.Servers
{
    internal class Client
    {
        private Socket clientSocket;
        private Server server;
        private Message message = new Message();
        private MySqlConnection connection;
        public Client(Socket clientSocket, Server server)
        {
            this.clientSocket = clientSocket;
            this.server = server;
            this.connection = ConnectionHelper.Connect();
        }

        public void SendFromClient(RequestCode requestCode, string data)
        {
            byte[] bytes = Message.PackData(requestCode, data);
            clientSocket.Send(bytes);
        }
        public void StartRecieving()
        {
            clientSocket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, RecieveCallback, null);
        }

        private void RecieveCallback(IAsyncResult ar)
        {
            int count = clientSocket.EndReceive(ar);
            if (count == 0)
            {
                CloseRecieving();
            }
            message.ReadMessage(count,OnProcessMessage);
            StartRecieving();
        }

        private void CloseRecieving()
        {
            ConnectionHelper.CloseConnection(connection);
            if (clientSocket != null)
            {
                clientSocket.Close();
            }
            server.RemoveClient(this);
        }

        public void OnProcessMessage(RequestCode requestCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(requestCode, actionCode, data, this);
        }
    }
}
