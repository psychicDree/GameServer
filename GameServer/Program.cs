using GameServer.Servers;
using System;

namespace GameServer
{
    public abstract class Program
    {
        private static void Main(string[] args)
        {
            Server server = new Server("127.0.0.1", 6688);
            server.StartServer();
            Console.ReadKey();
        }
    }
}
