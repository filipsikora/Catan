using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class DiscardCardsHandler
    {
        private readonly GameState _game;

        public DiscardCardsHandler(GameState game)
        {
            _game = game;
        }

        public ResultCondition Handle(Player player, ResourceCostOrStock selectedCards)
        {
            var canDiscard = RulesCardDiscard.CanDiscard(player, selectedCards);

            if (!canDiscard.Success)
            {
                return canDiscard;
            }

            _game.CardsDiscardedMutation(player, selectedCards);

            return ResultCondition.Ok();
        }
    }
}