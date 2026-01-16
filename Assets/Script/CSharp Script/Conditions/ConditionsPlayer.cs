using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Conditions
{
    public static class ConditionsPlayer
    {
        public static ResultCondition PlayerExists(Player player)
        {
            if (player == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }
    }
}