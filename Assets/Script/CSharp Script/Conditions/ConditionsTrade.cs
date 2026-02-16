#nullable enable
using Catan.Shared.Data;
using Catan.Core.Results;
using Catan.Core.Models;
using Catan.Core.Engine;

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

        public static ResultCondition PlayerHasEnoughResource(int playerAmount, int neededAmount)
        {
            if (playerAmount >= neededAmount)
                return ResultCondition.Ok();

            return ResultCondition.Fail(ConditionFailureReason.NotEnoughResources);
        }

        public static ResultCondition BankHasEnoughResources(ResourceCostOrStock bank, EnumResourceTypes type)
        {
            if (bank.Get(type) < 1)
            {
                return ResultCondition.Fail(ConditionFailureReason.NoResourceCardsLeft);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition NotSamePLayer(int sellerId, int buyerId)
        {
            if (sellerId == buyerId)
            {
                return ResultCondition.Fail(ConditionFailureReason.SamePlayer);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition CostIsValid(ResourceCostOrStock cost)
        {
            var negativeNumber = false;

            foreach (var (type, amount) in cost.ResourceDictionary)
            {
                if (amount < 0)
                {
                    negativeNumber = true;
                }
            }

            if (!negativeNumber)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.InvalidResourceCost);
        }

        public static ResultCondition TradeContextIsValid(PlayerTradeContext context, int buyerId, int sellerId, ResourceCostOrStock desired, ResourceCostOrStock offered)
        {
            if (context == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            if (context.BuyerId != buyerId)
            {
                return ResultCondition.Fail(ConditionFailureReason.TradeContextInvalid);
            }

            if (context.SellerId != sellerId)
            {
                return ResultCondition.Fail(ConditionFailureReason.TradeContextInvalid);
            }

            if (context.Desired != desired)
            {
                return ResultCondition.Fail(ConditionFailureReason.TradeContextInvalid);
            }

            if (context.Offered != offered)
            {
                return ResultCondition.Fail(ConditionFailureReason.TradeContextInvalid);
            }

            return ResultCondition.Ok();
        }
    }
}