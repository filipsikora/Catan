using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;

namespace Catan.Communication.Signals
{
    public class LogMessageSignal
    {
        public string Message { get; }
        public LogMessageSignal(string message)
        {
            Message = message;
        }
    }
}