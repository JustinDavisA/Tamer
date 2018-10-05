using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public interface ILoggerIO
    {
        void LogError(string errorType, string message, string location);
        List<MessageDO> ReadLog();
    }
}
