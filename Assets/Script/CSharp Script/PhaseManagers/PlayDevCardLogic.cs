using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class PlayDevCardLogic
    {
        public static Result<DevelopmentCard> Handle(GameState game, int cardId)
        {
            var player = game.GetCurrentPlayer();
            var card = game.GetDevCardById(cardId);
            var afterRoll = game.GetAfterRoll();
            var result = RulesDevCards.CanPlayDevCard(player, card, afterRoll);

            if (!result.Success)
            {
                return Result<DevelopmentCard>.Fail(result.Reason);
            }

            var playedCard = game.DevCardPlayedMutation(player, card);

            return Result<DevelopmentCard>.Ok(card);
        }
    }
}