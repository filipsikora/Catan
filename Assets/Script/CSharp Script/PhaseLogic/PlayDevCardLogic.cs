using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class PlayDevCardLogic : BaseLogic
    {
        public PlayDevCardLogic(GameSession session) : base(session) { }

        public Result<DevelopmentCard> Handle(int cardId)
        {
            var player = Session.GetCurrentPlayer();
            var card = Session.GetDevCardById(cardId);
            var afterRoll = Session.GetAfterRoll();
            var result = RulesDevCards.CanPlayDevCard(player, card, afterRoll);

            if (!result.Success)
            {
                return Result<DevelopmentCard>.Fail(result.Reason);
            }

            var playedCard = Session.DevCardPlayedMutation(card);

            return Result<DevelopmentCard>.Ok(card);
        }
    }
}