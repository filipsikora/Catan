using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesCardTheft
    {
        public static ResultCondition CanSteal(Player victim)
        {
            return ConditionsResources.HasAnyResources(victim);
        }
    }
}
