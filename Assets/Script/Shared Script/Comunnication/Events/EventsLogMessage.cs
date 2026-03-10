using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public class LogMessageEvent
    {
        public EnumLogTypes Type { get; }
        public string Message { get; }
        public int Time { get; }
        public LogMessageEvent(EnumLogTypes type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }
}