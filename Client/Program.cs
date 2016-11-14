using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to SimpleChat, where you can keep in touch with friends and family across the network!");
            Console.WriteLine("Pick a username, type some stuff, then hit enter to send messages.");
            try { 
            Client chatClient = new Client(8000);
            }
            catch(SocketException e)
            {
                Console.WriteLine("The server is down.  Try starting the server and reopening this window.");
                Console.ReadLine();
            }
        }
    }
}
