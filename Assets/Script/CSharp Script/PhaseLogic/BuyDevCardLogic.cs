using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuyDevCardLogic : BaseLogic
    {
        public BuyDevCardLogic(GameSession session) : base(session) { }

        public ResultBuyDevCard Handle()
        {
            var player = Session.GetCurrentPlayer();
            var devCard = Session.GetFirstDevCard();
            var devCardId = devCard.ID;
            var devCardType = devCard.Type;
            var devCardsLeftList = Session.GetDevCardsLeft();

            var result = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList);

            if (!result.Success)
            {
                return ResultBuyDevCard.Fail(result.Reason, player.ID);
            }

            Session.BuyDevCardMutation(devCard);

            return ResultBuyDevCard.Ok(player.ID, devCardId, devCardType, null);
        }
    }
}
