using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class LogMessageDto : IUiMessageDto
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public int Time { get; set; }
        public LogMessageDto(string type, string message, int time = 2)
        {
            Type = type;
            Message = message;
            Time = time;
        }
    }

    public sealed class ActionRejectedDto : IUiMessageDto
    {
        public int PlayerId { get; set; }
        public string Reason { get; set; }
        public ActionRejectedDto(int playerId, string reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}
