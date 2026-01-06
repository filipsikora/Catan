#nullable enable
using System;
using Catan.Shared.Data;
using Catan.Core.Results;
using Catan.Core.Interfaces;
using Catan.Core.Models;

namespace Catan.Core.Conditions
{
    public static class ConditionsTrade
    {
        public static ResultCondition PlayerHasEnoughResources(Player player, EnumResourceTypes type, int amount)
        {
            if (player.Resources.Get(type) < amount)
            {
                return ResultCondition.Fail(ConditionFailureReason.CannotAfford);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition BankHasEnoughResources(ResourceCostOrStock bank, EnumResourceTypes type)
        {
            if (bank.Get(type) < 1)
            {
                return ResultCondition.Fail(ConditionFailureReason.NoResourceCardsLeft);
            }

            return ResultCondition.Ok();
        }
    }
}