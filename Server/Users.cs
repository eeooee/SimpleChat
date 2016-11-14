using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class User : IObserver
    {

        public string Name;

        public User(string name)
        {
            Name = name;

        }

        public void Notify(NetworkStream stream, string message)
        {
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
            writer.WriteLine("**{0}**", message);
            writer.Flush();
        }

        public void PrivateMessage(NetworkStream stream, string message)
        {
            StreamWriter writer = new StreamWriter(stream, Encoding.ASCII);
            writer.WriteLine("*{0}*");
            writer.Flush();
        }


    }
}
