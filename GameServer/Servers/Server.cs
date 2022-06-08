using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using Common;
using GameServer.Controller;

namespace GameServer.Servers
{
    internal class Server
    {
        private readonly IPEndPoint ipEndPoint;
        private Socket serverSocket;
        private List<Client> clientList = new List<Client>();
        private ControllerManager controllerManager;
        public Server(string ipString,int port)
        {
            this.ipEndPoint = new IPEndPoint(IPAddress.Parse(ipString),port);
            this.controllerManager = new ControllerManager(this);
        }

        public void StartServer()
        { 
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(0);
            serverSocket.BeginAccept(AcceptCallback, null);
        }

        private void AcceptCallback(IAsyncResult ar)
        {
            Socket clientSocket = serverSocket.EndAccept(ar);
            Client client = new Client(clientSocket, this);
            client.StartRecieving();
            lock (clientList) clientList.Add(client);
            serverSocket.BeginAccept(AcceptCallback, null);
            Console.WriteLine("Client Connected");
        }

        public void RemoveClient(Client client)
        {
            lock (clientList) clientList.Remove(client);
        }

        public void SendResponse(Client client, ActionCode actionCode, string data)
        {
            client.SendFromClient(actionCode, data);
        }

        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data, Client client)
        {
            controllerManager.HandleRequest(requestCode, actionCode, data, client);
        }
    }
}
