using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.Rules
{
    public static class RulesTrade
    {
        public static ResultCondition CanTradeWithBank(Player player, ResourceCostOrStock bank, EnumResourceTypes offered, EnumResourceTypes desired, int ratio)
        {
            return ResultCondition.Combine(
                ConditionsTrade.PlayerHasEnoughResources(player, offered, ratio),
                ConditionsTrade.BankHasEnoughResources(bank, desired));
        }
    }
}