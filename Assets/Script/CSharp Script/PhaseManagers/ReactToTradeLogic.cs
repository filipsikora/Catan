using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class ReactToTradeLogic
    {
        public static ResultPlayerTrade Handle(GameState game, int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired, PlayerTradeContext context)
        {
            var seller = game.GetPlayerById(sellerId);
            var buyer = game.GetPlayerById(buyerId);

            var result = RulesTrade.CanAcceptTrade(seller, buyer, offered, desired, context);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, sellerId, buyerId);
            }

            game.PlayerTradeDoneMutation(seller, buyer, offered, desired);

            return ResultPlayerTrade.Ok(sellerId, buyerId, offered, desired);
        }
    }
}