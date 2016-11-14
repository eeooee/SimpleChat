using Server;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server
{
    class Server
    {
        private bool running = true;
        TcpListener serverListener;
        TcpClient connection;
        NetworkStream stream;
        StreamReader reader;
        ILogger logger;
        StreamWriter writer;
        ConcurrentQueue<Message> messageQueue = new ConcurrentQueue<Message>();
        public Dictionary<NetworkStream, User> clients;



        public Server(int port, ILogger logger)
        {
            this.logger = logger;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            logger.LogMessage("Attempting to start server...");
            serverListener = new TcpListener(ip, port);
            try
            {
                serverListener.Start();
                running = true;
                logger.LogMessage("Connected!");
                clients = new Dictionary<NetworkStream, User>();
            }
            catch (Exception E)
            {
                logger.LogMessage("Failed to connect to server.");
                logger.LogMessage(E.ToString());
            }

        }

        public void AcceptClients()
        {
            startChat();
            while (running)
            {
                if (serverListener.Pending())
                {
                    connection = serverListener.AcceptTcpClient();
                    logger.LogMessage("New client connected!");
                    stream = connection.GetStream();
                    reader = new StreamReader(stream, Encoding.ASCII);
                    writer = new StreamWriter(stream, Encoding.ASCII);
                    clients.Add(stream, new User(GetUserName()));
                    Thread inputThread = new Thread(() => AcceptMessage(clients[stream], stream));
                    inputThread.Start();


                }
                else
                {
                    Thread.Sleep(100);
                }

            }

        }

        public void startChat()
        {
            //starts a thread for dispatch queue
            Thread dispatchThread = new Thread(() => DispatchQueue());
            dispatchThread.Start();
            logger.LogMessage("The server can now receive messages from users.");

        }

        private void AcceptMessage(User user, NetworkStream stream)
        {
            while (running)
            {
                try
                {

                    string input = reader.ReadLine();

                    //could not get 'whisper' (SendPrivateMessage) to function properly 
                    //if (input.Contains("!whisper"))
                    //{
                    //    string whisperer = PickWhisperer(input);
                    //    input = reader.ReadLine();
                    //    Message message = new Message(user, input);
                    //    SendPrivateMessage(message, whisperer);
                    //}
                    //else {

                    Message message = new Message(user, input);
                    messageQueue.Enqueue(message);
                    logger.LogMessage("Message from " + user.Name + " added to queue");
                    //}

                }
                catch (IOException E)
                {
                    logger.LogMessage(E.ToString());
                    clients.Remove(stream);
                    SendSystemMessage(user.Name + " has disconnected.");
                    reader.Close();
                    running = false;
                    break;
                }
                catch (Exception E)
                {

                    logger.LogMessage(E.ToString());
                }
            }
        }

        private void DispatchQueue()
        {
            Message message;
            while (true)
            {
                if (messageQueue.TryDequeue(out message))
                {
                    SendChatMessage(message);
                    logger.LogMessage("Sent message from " + message.userName + " to users");
                }
            }
        }

        private string GetUserName()
        {
            string name = reader.ReadLine();
            reader.DiscardBufferedData();
            SendSystemMessage("New user " + name + " has connected!");
            return name;
        }

        //private string PickWhisperer(string input)
        //{
        //  return input.Replace("!whisper ", "");
        //}

        private void SendChatMessage(Message message)
        {
            logger.LogMessage(message.ToString());

            foreach (KeyValuePair<NetworkStream, User> pair in clients)
            {
                if (!(pair.Value == message.userReference))
                {

                    writer = new StreamWriter(pair.Key, Encoding.ASCII);
                    writer.WriteLine(message.ToString());
                    writer.Flush();
                }
            }
        }

        //private void SendPrivateMessage(Message message, string user)
        //{
        //    logger.LogMessage(message.ToString());
        //    foreach (KeyValuePair<NetworkStream, User> pair in clients)
        //    {
        //        if (pair.Value.Name.Contains(user))
        //        {
        //            pair.Value.Notify(pair.Key, "Whisper from " + message.userName);
        //            pair.Value.PrivateMessage(pair.Key, message.ToString());
        //        }
        //    }

        //}

        private void SendSystemMessage(string message)
        {
            logger.LogMessage(message.ToString());

            foreach (KeyValuePair<NetworkStream, User> pair in clients)
            {
                pair.Value.Notify(pair.Key, message);

            }
        }
    }
}
