using Catan.Application.Snapshots;
using Catan.Core.Engine;
using Catan.Core.Rules;

namespace Catan.Application.Queries.Players
{
    public sealed class InMemoryTradeQueryServices : ITradeQueryService
    {
        private readonly GameState _game;

        public InMemoryTradeQueryServices(GameState game)
        {
            _game = game;
        }

        public TradeOfferedSnapshot GetTradeOfferData()
        {
            var data = _game.LastPlayerTradeOffered;
            var canTrade = RulesTrade.CanAcceptTrade(_game.GetPlayerById(data.SellerId), _game.GetPlayerById(data.BuyerId), data.Offered, data.Desired, _game.LastPlayerTradeOffered).Success;

            return new TradeOfferedSnapshot(data.SellerId, data.BuyerId, data.SellerName, data.BuyerName, data.Offered.ToDictionary(), data.Desired.ToDictionary(), canTrade);
        }
    }
}