using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public class LogMessageEvent
    {
        public EnumLogTypes Type { get; }
        public string Message { get; }
        public LogMessageEvent(EnumLogTypes type, string message)
        {
            Type = type;
            Message = message;
        }
    }
}