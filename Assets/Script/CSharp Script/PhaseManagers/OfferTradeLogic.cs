using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class OfferTradeLogic
    {
        public static ResultPlayerTrade Handle(GameState game, int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            var seller = game.GetPlayerById(sellerId);
            var buyer = game.GetPlayerById(buyerId);

            var result = RulesTrade.CanOfferTrade(seller, buyer, offered, desired);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, sellerId, buyerId);
            }

            game.CreatePlayerTradeOfferedContext(seller.ID, buyer.ID, seller.Name, buyer.Name, offered, desired);

            return ResultPlayerTrade.Ok(sellerId, buyerId, offered, desired);
        }
    }
}