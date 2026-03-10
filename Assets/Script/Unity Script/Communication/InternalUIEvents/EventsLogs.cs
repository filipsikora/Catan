using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class LogMessageUIEvent : IInternalUIEvents
    {
        public EnumLogTypes Type { get; }
        public string Message { get; }
        public int Time { get; }
        public LogMessageUIEvent(EnumLogTypes type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }

    public sealed class ActionRejectedUIEvent : IInternalUIEvents
    {
        public int PlayerId { get; }
        public ConditionFailureReason Reason { get; }
        public ActionRejectedUIEvent(int playerId, ConditionFailureReason reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}
