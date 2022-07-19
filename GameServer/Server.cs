using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using GameServer.Servers;

namespace GameServer
{
    internal class Server
    {
        static void Main(string[] args)
        {
            Servers.Server server = new Servers.Server("127.0.0.1", 6688);
            server.StartServer();
            Console.ReadKey();
        }
    }
}
