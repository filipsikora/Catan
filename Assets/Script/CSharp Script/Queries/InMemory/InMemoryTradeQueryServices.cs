using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;
using Catan.Core.Rules;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryTradeQueryServices : ITradeQueryService
    {
        private readonly GameSession _session;

        public InMemoryTradeQueryServices(GameSession session)
        {
            _session = session;
        }

        public TradeOfferedSnapshot GetTradeOfferData()
        {
            var data = _session.TryGetPlayerTradeContext().context;
            var canTrade = RulesTrade.CanAcceptTrade(_session.GetPlayerById(data.SellerId), _session.GetPlayerById(data.BuyerId), data.Offered, data.Desired, data).Success;

            return new TradeOfferedSnapshot(data.SellerId, data.BuyerId, data.SellerName, data.BuyerName, data.Offered.ToDictionary(), data.Desired.ToDictionary(), canTrade);
        }
    }
}