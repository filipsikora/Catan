using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesRobber
    {
        public static ResultCondition CanSteal(Player victim)
        {
            return ConditionsResources.HasAnyResources(victim);
        }

        public static ResultCondition CanBlock(HexTile hex)
        {
            return ResultCondition.Combine(
                ConditionsMap.HexExists(hex),
                ConditionsMap.IsNotBlocked(hex));
        }
    }
}