using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class LogMessageMessage : IInternalUIEvents
    {
        public EnumLogTypes Type { get; }
        public string Message { get; }
        public int Time { get; }
        public LogMessageMessage(EnumLogTypes type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }

    public sealed class ActionRejectedMessage : IInternalUIEvents
    {
        public int PlayerId { get; }
        public ConditionFailureReason Reason { get; }
        public ActionRejectedMessage(int playerId, ConditionFailureReason reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}
