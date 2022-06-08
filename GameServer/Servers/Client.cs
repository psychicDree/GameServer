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
            message.ReadMessage(count,OnProcessMessage);
            StartRecieving();
        }

        private void CloseReceiving()
        {
            ConnectionHelper.CloseConnection(mySqlConnection);
            if (clientSocket != null)
            {
                clientSocket.Shutdown(SocketShutdown.Both);
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
