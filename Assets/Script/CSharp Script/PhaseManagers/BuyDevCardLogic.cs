using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using System.Collections.Generic;

namespace Catan.Core.PhaseLogic
{
    public static class BuyDevCardLogic
    {
        public static ResultBuyDevCard Handle(GameState game, int playerId, List<DevelopmentCard> devCardsLeftList)
        {
            var player = game.GetCurrentPlayer();
            var devCard = game.DevelopmentCardsDeckAvailable[0];
            var devCardId = devCard.ID;

            var result = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList);

            if (!result.Success)
            {
                return ResultBuyDevCard.Fail(result.Reason, playerId, devCardId);
            }

            game.BuyDevCardMutation(player, devCard);

            return ResultBuyDevCard.Ok(playerId, devCardId);
        }
    }
}
