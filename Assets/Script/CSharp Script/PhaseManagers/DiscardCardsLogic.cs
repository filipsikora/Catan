using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class DiscardCardsLogic
    {
        public static ResultCondition Handle(GameState game, Player player, ResourceCostOrStock selectedCards)
        {
            var canDiscard = RulesCardDiscard.CanDiscard(player, selectedCards);

            if (!canDiscard.Success)
            {
                return canDiscard;
            }

            game.CardsDiscardedMutation(player, selectedCards);
            game.CardsDiscardedContextMutation();

            return ResultCondition.Ok();
        }
    }
}