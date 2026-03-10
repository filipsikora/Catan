using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public sealed class ActionRejectedEvent
    {
        public int PlayerId { get; }
        public ConditionFailureReason Reason { get; }
        public ActionRejectedEvent(int playerId, ConditionFailureReason reason)
        {
            PlayerId = playerId;
            Reason = reason;
        }
    }
}