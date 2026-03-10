using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class LogMessageMessage : IUIMessages
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

    public sealed class ActionRejectedMessage : IUIMessages
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
