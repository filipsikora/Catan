using Catan.Core.Models;
using System;
using System.Linq;

namespace Catan.Core.Rules
{
    public static class RulesCardDiscard
    {
        public static int RequiredDiscardCount(Player player)
        {
            int total = player.Resources.ResourceDictionary.Values.Sum();
            int required = (int)Math.Ceiling(total / 2.0);

            return required;
        }

        public static bool IsValidSelection(ResourceCostOrStock cards, int required)
        {
            int selectedCount = cards.ResourceDictionary.Values.Sum();

            return required == selectedCount;
        }
    }
}