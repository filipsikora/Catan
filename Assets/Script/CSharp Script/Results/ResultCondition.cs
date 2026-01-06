using Catan.Shared.Data;

namespace Catan.Core.Results
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

        public static ResultCondition Combine(params ResultCondition[] conditions)
        {
            foreach (var condition in conditions)
            {
                if (!condition.Success)
                    return condition;
            }

            return Ok();
        }
    }
}