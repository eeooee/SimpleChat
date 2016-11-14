using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Client
{
    class Client 
    {
        TcpClient chatClient = new TcpClient();
        NetworkStream chatStream;
        StreamReader reader;
        StreamWriter writer;
        Thread typeMessages;
        Thread messages;
        bool running;
        IPAddress ip = IPAddress.Parse("127.0.0.1");

        public Client(int port)
        {
            chatClient.Connect(ip, port);
            Console.WriteLine("Connected to server!");
            chatStream = chatClient.GetStream();
            reader = new StreamReader(chatStream, Encoding.ASCII);
            writer = new StreamWriter(chatStream, Encoding.ASCII);
            GetName();
            StartChat();

        }
        
        private void GetName()
        {
            Console.WriteLine("Please enter a name.");
            string name = Console.ReadLine();
            writer.WriteLine(name);
            writer.Flush();
            Console.WriteLine("Thanks, {0}!", name);
        }

        private void StartChat()
        {

            running = true;
            typeMessages = new Thread(new ThreadStart(GetMessage));
            typeMessages.Start();
            messages = new Thread(new ThreadStart(seekMessage));
            messages.Start();
            
        }
        public void GetMessage()
        {
            while (running)
            {
                string input = Console.ReadLine();
                if (input.Contains("!quit"))
                {
                Environment.Exit(0);
                }
                writer.WriteLine(input);
                Console.WriteLine("^Sent^");
                writer.Flush();

            }

        }

        private void seekMessage()
        {
            while (running)
            {
                try
                {
                        string stream = reader.ReadLine();
                        if(stream!=null) Console.WriteLine(stream);
                }
                catch(Exception E)
                {
                    Console.WriteLine(E.ToString());
                }
            }

        }
    }
}
