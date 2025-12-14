using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;


namespace Catan.Core
{
    public class HandlerTradeRequest : BaseHandler
    {
        private Player _player;
        private ResourceCostOrStock _cardsRequested;
        private ResourceCostOrStock _cardsOffered;

        public HandlerTradeRequest(GameState game, EventBus bus, Player player, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsRequested) : base(game, bus)
        {
            _player = player;
            _cardsOffered = cardsOffered;
            _cardsRequested = cardsRequested;

            Bus.Subscribe<TradeRequestAcceptedSignal>(OnTradeRequestAccepted);
            Bus.Subscribe<TradeRequestRefusedSignal>(OnTradeRequestRejected);

            bool canTrade = Conditions.CanAfford(_player.Resources, _cardsRequested);

            Bus.Publish(new TradeRequestSentSignal(canTrade));
        }

        private void OnTradeRequestAccepted(TradeRequestAcceptedSignal signal)
        {
            Game.CurrentPlayer.Resources.AddCards(_cardsRequested);
            _player.Resources.SubtractCards(_cardsRequested);

            Game.CurrentPlayer.Resources.SubtractCards(_cardsOffered);
            _player.Resources.AddCards(_cardsOffered);

            Bus.Publish(new TradeFinishedSignal());
        }

        private void OnTradeRequestRejected(TradeRequestRefusedSignal signal)
        {
            Bus.Publish(new TradeFinishedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<TradeRequestAcceptedSignal>(OnTradeRequestAccepted);
            Bus.Unsubscribe<TradeRequestRefusedSignal>(OnTradeRequestRejected);
        }
    }
}
