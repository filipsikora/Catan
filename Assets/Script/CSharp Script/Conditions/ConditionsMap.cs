using Catan.Core.Engine;
using Catan.Core.Results;

namespace Catan.Core.Conditions
{
    public static class ConditionsMap
    {
        public static ResultCondition MapExists(HexMap map)
        {
            if (map == null)
            {
                return ResultCondition.Fail(Shared.Data.ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }
    }
}