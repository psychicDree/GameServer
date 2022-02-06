using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Servers;
namespace GameServer
{
    internal class Program
    {
        private Server server;
        static void Main(string[] args)
        {
            Server server = new Server("192.168.43.95", 88);
            server.StartServer();
            Console.ReadLine();
        }
    }
}
