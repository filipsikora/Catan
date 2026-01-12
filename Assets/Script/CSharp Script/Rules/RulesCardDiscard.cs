using Catan.Core.Conditions;
using Catan.Core.Models;
using Catan.Core.Results;
using System;
using System.Linq;

namespace Catan.Core.Rules
{
    public static class RulesCardDiscard
    {
        public static int RequiredDiscardCount(Player player)
        {
            int total = player.Resources.Total();
            int required = (int)Math.Ceiling(total / 2.0);

            return required;
        }

        public static ResultCondition CanDiscard(Player player, ResourceCostOrStock selectedCards)
        {
            int required = RequiredDiscardCount(player);

            return ConditionsResources.HasExactResourcesNumber(selectedCards, required);
        }
    }
}