using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            LogToFile logger = new LogToFile();
            Server simpleChatServer = new Server(8000, logger);
            simpleChatServer.AcceptClients();
        }
    }
}
