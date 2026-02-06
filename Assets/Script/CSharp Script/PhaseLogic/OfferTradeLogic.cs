using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class OfferTradeLogic : BaseLogic
    {
        public OfferTradeLogic(GameSession session) : base(session) { }

        public ResultPlayerTrade Handle(int buyerId, ResourceCostOrStock desired)
        {
            var seller = Session.GetCurrentPlayer();
            var buyer = Session.GetPlayerById(buyerId);
            var offered = Session.GetOfferedResources();

            var result = RulesTrade.CanOfferTrade(seller, buyer, offered, desired);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, seller.ID, buyerId);
            }

            Session.CreatePlayerTradeOfferedContext(seller.ID, buyer.ID, seller.Name, buyer.Name, offered, desired);

            return ResultPlayerTrade.Ok(seller.ID, buyerId, offered, desired, EnumGamePhases.TradeRequest);
        }
    }
}