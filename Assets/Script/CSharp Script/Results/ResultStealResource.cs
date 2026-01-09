using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public class ResultStealResource
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }
        public int ThiefId { get; }
        public int VictimId { get; }
        public EnumResourceTypes Resource { get; }

        public ResultStealResource(bool success, ConditionFailureReason reason, int thiefId, int victimId, EnumResourceTypes resource)
        {
            Success = success;
            Reason = reason;
            ThiefId = thiefId;
            VictimId = victimId;
            Resource = resource;
        }

        public static ResultStealResource Ok(int thiefId, int victimId, EnumResourceTypes resource)
        {
            return new ResultStealResource(true, ConditionFailureReason.None, thiefId, victimId, resource);
        }

        public static ResultStealResource Fail(int thiefId, int victimId, ConditionFailureReason reason)
        {
            return new ResultStealResource(false, reason, thiefId, victimId, default);
        }
    }
}
