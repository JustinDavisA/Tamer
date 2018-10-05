using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace ApplicationLogger
{
    public class LoggerIO : ILoggerIO
    {
        private string path = @"C:\Users\OnshoreBootcamper\Documents\Visual Studio 2013\Projects\Tamer\logs.txt";

        public void LogError(string errorType, string message, string location)
        {
            string[] errorMessage = new string[] { DateTime.Now.ToString(), errorType, message, location };
            File.AppendAllLines(path, errorMessage);
        }
        public List<MessageDO> ReadLog()
        {
            List<MessageDO> logs = new List<MessageDO>();
            string[] log = File.ReadAllLines(path);
            for (int i = 0; i < log.Length; i++)
            {
                MessageDO msg = new MessageDO();
                msg.DateTime = log[i];
                i++;
                msg.ErrorType = log[i];
                i++;
                msg.ErrorMessage = log[i];
                i++;
                msg.Layer = log[i];
                logs.Add(msg);
            }
            return logs;
        }
    }
}
