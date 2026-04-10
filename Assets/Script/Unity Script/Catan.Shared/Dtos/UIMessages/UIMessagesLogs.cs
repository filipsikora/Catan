using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class LogMessageDto : IUiMessageDto
    {
        public string Type { get; }
        public string Message { get; }
        public int Time { get; }
        public LogMessageDto(string type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }

    public sealed class ActionRejectedDto : IUiMessageDto
    {
        public int PlayerId { get; }
        public string Reason { get; }
        public ActionRejectedDto(int playerId, string reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}
