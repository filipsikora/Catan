using Catan.Core.Models;
using Catan.Shared.Data;
using Catan.Core.Results;
using System;

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

        public static ResultCondition HasAnyResources(Player player)
        {
            if (player.Resources.Total() == 0)
            {
                return ResultCondition.Fail(ConditionFailureReason.NoResourceCardsLeft);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasSpecificResource(Player player, EnumResourceTypes type)
        {
            if (player.Resources.Get(type) == 0)
            {
                return ResultCondition.Fail(ConditionFailureReason.ResourceNotOwned);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasExactResourcesNumber(ResourceCostOrStock cards, int required)
        {
            if (cards.Total() == required)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.InvalidSelection);
        }

        public static ResultCondition HasAtLeastResourcesNumber(ResourceCostOrStock bank, int number)
        {
            if (bank.Total() < number)
            {
                return ResultCondition.Fail(ConditionFailureReason.NotEnoughResourcesInBank);
            }

            return ResultCondition.Ok(null)
        }

        public static ResultCondition ResourceExists(EnumResourceTypes resource)
        {
            if (Enum.IsDefined(typeof(EnumResourceTypes), resource))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
        }
    }
}