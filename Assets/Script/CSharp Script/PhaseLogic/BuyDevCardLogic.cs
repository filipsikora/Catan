using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using System.Collections.Generic;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuyDevCardLogic : BaseLogic
    {
        public BuyDevCardLogic(GameSession session) : base(session) { }

        public ResultBuyDevCard Handle(List<DevelopmentCard> devCardsLeftList)
        {
            var player = Session.GetCurrentPlayer();
            var devCard = Session.GetFirstDevCard();
            var devCardId = devCard.ID;

            var result = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList);

            if (!result.Success)
            {
                return ResultBuyDevCard.Fail(result.Reason, player.ID, devCardId);
            }

            Session.BuyDevCardMutation(devCard);

            return ResultBuyDevCard.Ok(player.ID, devCardId);
        }
    }
}
