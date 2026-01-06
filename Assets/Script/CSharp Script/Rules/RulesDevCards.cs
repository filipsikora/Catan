using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;

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
    }
}