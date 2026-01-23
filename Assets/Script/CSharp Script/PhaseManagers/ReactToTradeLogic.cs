using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class ReactToTradeLogic
    {
        public static ResultPlayerTrade Handle(GameState game)
        {
            var context = game.LastPlayerTradeOffered;
            var seller = game.GetPlayerById(context.SellerId);
            var buyer = game.GetPlayerById(context.BuyerId);

            var result = RulesTrade.CanAcceptTrade(seller, buyer, context.Offered, context.Desired, context);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, context.SellerId, context.BuyerId);
            }

            game.PlayerTradeDoneMutation(seller, buyer, context.Offered, context.Desired);

            return ResultPlayerTrade.Ok(context.SellerId, context.BuyerId, context.Offered, context.Desired);
        }
    }
}