using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public class ResultResourceTheft
    {
        public bool Success { get; }
        public ConditionFailureReason? Reason { get; }
        public int ThiefId { get; }
        public int VictimId { get; }
        public EnumResourceTypes? Resource { get; }

        public ResultResourceTheft(bool success, ConditionFailureReason? reason, int thiefId, int victimId, EnumResourceTypes? resource)
        {
            Success = success;
            Reason = reason;
            ThiefId = thiefId;
            VictimId = victimId;
            Resource = resource;
        }

        public static ResultResourceTheft Ok(int thiefId, int victimId, EnumResourceTypes resource)
        {
            return new ResultResourceTheft(true, null, thiefId, victimId, resource);
        }

        public static ResultResourceTheft Fail(int thiefId, int victimId, ConditionFailureReason reason)
        {
            return new ResultResourceTheft(false, reason, thiefId, victimId, null);
        }
    }
}
