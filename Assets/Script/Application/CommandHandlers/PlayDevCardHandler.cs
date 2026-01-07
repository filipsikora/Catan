using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public class PlayDevCardHandler
    {
        private readonly GameState _game;

        public PlayDevCardHandler(GameState game)
        {
            _game = game;
        }

        public (ResultCondition, DevelopmentCard) Handle(int cardId)
        {
            var player = _game.GetCurrentPlayer();
            var card = _game.GetDevCardById(cardId);
            var afterRoll = _game.GetAfterRoll();
            var result = RulesDevCards.CanPlayDevCard(player, card, afterRoll);

            if (!result.Success)
            {
                return (result, null);
            }

            var playedCard = _game.DevCardPlayedMutation(player, card);

            return (ResultCondition.Ok(), playedCard);
        }
    }
}
