using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class LogToConsole : ILogger
    {
        public void LogMessage(string message)
        {
            Console.WriteLine(message);
        }


    }
}
