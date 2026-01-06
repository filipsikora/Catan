using Catan.Core.Models;
using Catan.Shared.Data;
using Catan.Core.Results;

namespace Catan.Core.Conditions
{
    public class ConditionsResources
    {
        public static ResultCondition CanAfford(ResourceCostOrStock playerResources, ResourceCostOrStock cost)
        {
            if (!playerResources.HasEnoughCards(cost))
                return ResultCondition.Fail(ConditionFailureReason.CannotAfford);

            return ResultCondition.Ok();
        }
    }
}