using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using System.Collections.Generic;

namespace Catan.Core.Rules
{
    public static class RulesDevCards
    {
        public static ResultCondition CanPlayDevCard(Player player, DevelopmentCard? card, bool afterRoll)
        {
            return ResultCondition.Combine(
                ConditionsDevCards.DevCardExists(card),
                ConditionsDevCards.IsNotNew(card),
                ConditionsDevCards.IsNotUsed(card),
                ConditionsDevCards.IsOwner(card, player),
                ConditionsDevCards.CanBePlayedNow(card, afterRoll));
        }

        public static ResultCondition CanBuyDevCard(Player player, DevelopmentCard? card, List<DevelopmentCard> devCardsLeft)
        {
            return ResultCondition.Combine(
                ConditionsDevCards.DevCardsLeft(devCardsLeft.Count),
                ConditionsDevCards.DevCardExists(card),
                ConditionsDevCards.IsNotOwned(card),
                ConditionsResources.CanAfford(player.Resources, DevelopmentCard.Cost));
        }

        public static ResultCondition CanPlayYearOfPlenty(ResourceCostOrStock bank, int number)
        {
            return ConditionsResources.HasAtLeastResourcesNumber(bank, number);
        }

        public static ResultCondition CanPlayRoadBuilding(Player player)
        {
            return ConditionsBuildings.HasAvailable<BuildingRoad>(player);
        }

        public static ResultCondition YearOfPlentyPlayedRight(ResourceCostOrStock bank, ResourceCostOrStock requested)
        {
            return ResultCondition.Combine(
                ConditionsResources.CanAfford(bank, requested),
                ConditionsResources.HasExactResourcesNumber(requested, 2),
                ConditionsTrade.CostIsValid(requested));
        }
    }
}