using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuyDevCardHandler
    {
        private GameState _game;

        public BuyDevCardHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuyDevCard Handle(int playerId, List<DevelopmentCard> devCardsLeftList)
        {
            var player = _game.GetCurrentPlayer();
            var devCard = _game.DevelopmentCardsDeckAvailable[0];
            var devCardId = devCard.ID;

            var result = RulesDevCards.CanBuyDevCard(player, devCard, devCardsLeftList);

            if (!result.Success)
            {
                return ResultBuyDevCard.Fail(result.Reason, playerId, devCardId);
            }

            _game.BuyDevCardMutation(player, devCard);

            return ResultBuyDevCard.Ok(playerId, devCardId);
        }
    }
}
