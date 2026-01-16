using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class OfferTradeHandler
    {
        private GameState _game;

        public OfferTradeHandler(GameState game)
        {
            _game = game;
        }

        public ResultPlayerTrade Handle(int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            var seller = _game.GetPlayerById(sellerId);
            var buyer = _game.GetPlayerById(buyerId);

            var result = RulesTrade.CanOfferTrade(seller, buyer, offered, desired);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, sellerId, buyerId);
            }

            _game.PlayerTradeOfferedContextMutation(seller.ID, buyer.ID, seller.Name, buyer.Name, offered, desired);

            return ResultPlayerTrade.Ok(sellerId, buyerId, offered, desired);
        }
    }
}