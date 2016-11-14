using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {

                Client chatClient = new Client();
            }
            catch (SocketException e)
            {
                Console.WriteLine("The server is down.  Try starting the server and reopening this window.");
                Console.ReadLine();
            }
        }


    }
}