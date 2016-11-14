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
            ILogger logger;
            Console.WriteLine("Would you like the server log to be saved to a file?  \n\t y or n");
            string input = Console.ReadLine();
            if (input.Contains("y")) {
                logger = new LogToFile();
            }
            else {
                logger = new LogToConsole();
            }
            Server simpleChatServer = new Server(8000, logger);
            simpleChatServer.AcceptClients();
        }
    }
}
