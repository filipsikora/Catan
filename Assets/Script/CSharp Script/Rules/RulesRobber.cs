using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesRobber
    {
        public static ResultCondition CanSteal(Player victim, CardStealingContext context)
        {
            return ResultCondition.Combine(
                ConditionsRobber.StealContextIsValid(context, victim.ID),
                ConditionsResources.HasAnyResources(victim),
                ConditionsPlayer.PlayerExists(victim));
        }

        public static ResultCondition CanBlock(HexTile hex)
        {
            return ResultCondition.Combine(
                ConditionsMap.HexExists(hex),
                ConditionsMap.IsNotBlocked(hex));
        }
    }
}