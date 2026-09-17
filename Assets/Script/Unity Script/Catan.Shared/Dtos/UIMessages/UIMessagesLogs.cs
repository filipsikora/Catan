using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
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