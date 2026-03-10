using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultCondition : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        private ResultCondition(bool success, ConditionFailureReason reason, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
        }

        public static ResultCondition Ok(EnumGamePhases? nextPhase = null)
        {
            return new(true, ConditionFailureReason.None, nextPhase);
        }

        public static ResultCondition Fail(ConditionFailureReason reason, EnumGamePhases? nextPhase = null)
        {
            return new(false, reason, nextPhase);
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