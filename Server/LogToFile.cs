using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Server
{
    class LogToFile : ILogger
    {
        private string _File = "ServerLog";
        public void LogMessage(string message)
        {
            try { 
            using(FileStream stream = new FileStream(_File+".txt", FileMode.Create)) {
                    StreamWriter writer = new StreamWriter(stream);
                    writer.WriteLine(message);
                    writer.Flush();
                Console.WriteLine(message);
                }
            }
            catch (IOException)
            {
                Console.WriteLine("Write to file failed. {0}", message);
            }


                }
            }
    }

