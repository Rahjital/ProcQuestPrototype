using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ProceduralQuestTest
{
    public static class Log
    {
        private static StreamWriter streamWriter;

        public static void LogMessage(string message)
        {
            if (streamWriter == null)
            {
                streamWriter = new StreamWriter("log.txt");
                streamWriter.AutoFlush = true;
            }

            Console.WriteLine((String.Format("[{0}]: {1}", DateTime.Now.TimeOfDay, message)));
            streamWriter.WriteLine(String.Format("[{0}]: {1}", DateTime.Now.TimeOfDay, message));
        }
    }
}
