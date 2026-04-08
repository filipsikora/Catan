using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class LogMessageDto : IUiMessageDto
    {
        public EnumLogTypes Type { get; }
        public string Message { get; }
        public int Time { get; }
        public LogMessageDto(EnumLogTypes type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }

    public sealed class ActionRejectedDto : IUiMessageDto
    {
        public int PlayerId { get; }
        public ConditionFailureReason Reason { get; }
        public ActionRejectedDto(int playerId, ConditionFailureReason reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}
