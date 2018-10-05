using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationLogger
{
    public class MessageDO
    {
        public string ErrorMessage { get; set; }
        public string Layer { get; set; }
        public string DateTime { get; set; }
        public string ErrorType { get; set; }
    }
}
