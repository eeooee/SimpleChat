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
            int portNumber = 8000;
            Console.WriteLine("Would you like the server log to be saved to a file?  \n\t y or n");
            string input = Console.ReadLine();
            if (input.Contains("y")) {
                logger = new LogToFile();
            }
            else {
                logger = new LogToConsole();
            }
            Console.WriteLine("To change the default port, type y.  Otherwise, hit enter. ");
            input = Console.ReadLine();
            if (input.Contains("y"))
            {
                input = Console.ReadLine();
                while (!int.TryParse(input, out portNumber)) {
                input = Console.ReadLine();
                }
                Console.WriteLine("New port number is {0}",portNumber);
            }
            Server simpleChatServer = new Server(portNumber, logger);
            simpleChatServer.AcceptClients();
        }
    }
}
