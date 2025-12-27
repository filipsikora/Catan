using Catan.Shared.Data;

namespace Catan.Shared.Results
{
    public sealed class ResultCondition
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        private ResultCondition(bool success, ConditionFailureReason reason)
        {
            Success = success;
            Reason = reason;
        }

        public static ResultCondition Ok()
        {
            return new(true, ConditionFailureReason.None);
        }

        public static ResultCondition Fail(ConditionFailureReason reason)
        {
            return new(false, reason);
        }
    }
}