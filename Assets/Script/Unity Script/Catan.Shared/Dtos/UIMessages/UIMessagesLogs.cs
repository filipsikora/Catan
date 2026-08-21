using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
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