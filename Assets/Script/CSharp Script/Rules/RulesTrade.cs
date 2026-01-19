using Catan.Core.Conditions;
using Catan.Core.Engine;
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

        public static ResultCondition CanOfferTrade(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            return ResultCondition.Combine(
                ConditionsTrade.CostIsValid(offered),
                ConditionsTrade.CostIsValid(desired),
                ConditionsResources.CanAfford(seller.Resources, offered),
                ConditionsPlayer.PlayerExists(buyer),
                ConditionsTrade.NotSamePLayer(seller.ID, buyer.ID));
        }

        public static ResultCondition CanAcceptTrade(Player seller, Player buyer, ResourceCostOrStock offered, ResourceCostOrStock desired, PlayerTradeContext context)
        {
            return ResultCondition.Combine(
                ConditionsTrade.TradeContextIsValid(context, buyer.ID, seller.ID, desired, offered),
                ConditionsTrade.CostIsValid(offered),
                ConditionsTrade.CostIsValid(desired),
                ConditionsResources.CanAfford(buyer.Resources, desired),
                ConditionsPlayer.PlayerExists(seller),
                ConditionsTrade.NotSamePLayer(seller.ID, buyer.ID));
        }
    }
}