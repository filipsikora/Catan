using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class OfferTradeLogic : BaseLogic
    {
        public OfferTradeLogic(GameSession session) : base(session) { }

        public ResultPlayerTrade Handle(int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            var seller = Session.GetPlayerById(sellerId);
            var buyer = Session.GetPlayerById(buyerId);

            var result = RulesTrade.CanOfferTrade(seller, buyer, offered, desired);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, sellerId, buyerId);
            }

            Session.CreatePlayerTradeOfferedContext(seller.ID, buyer.ID, seller.Name, buyer.Name, offered, desired);

            return ResultPlayerTrade.Ok(sellerId, buyerId, offered, desired);
        }
    }
}