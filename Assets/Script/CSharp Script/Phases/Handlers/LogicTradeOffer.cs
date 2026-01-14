using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Application.CommandHandlers;
using Catan.Core.Results;

namespace Catan.Core.Phases.Handlers
{
    public class LogicTradeOffer : BasePhaseLogic
    {
        private ResourceCostOrStock _cardsDesired = new();
        private ResourceCostOrStock _cardsOffered;

        private OfferTradeHandler _handler;

        public LogicTradeOffer(GameState game, EventBus bus, ResourceCostOrStock cardsOffered) : base(game, bus)
        {
            _cardsOffered = cardsOffered.Clone();
            _handler = new OfferTradeHandler(game);
        }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceCardClicked(c);
                    break;

                case TradeOfferCanceledCommand c:
                    Bus.Publish(new ReturnToNormalRoundEvent());
                    break;

                case TradePartnerChosenCommand c:
                    HandleTradePartnerChosen(c);
                    break;
            }
        }

        private void HandleResourceCardClicked(ResourceCardSelectedCommand signal)
        {
            if (signal.IsSelected)
            { 
                _cardsDesired.AddExactAmount(signal.Type, 1);
            }

            if (!signal.IsSelected)
            {
                _cardsDesired.SubtractExactAmount(signal.Type, 1);
            }

            bool hasDesired = _cardsDesired.Total() > 0;

            Bus.Publish(new DesiredCardsChangedEvent(hasDesired));
        }

        private ResultPlayerTrade HandleTradePartnerChosen(TradePartnerChosenCommand signal)
        {
            var seller = Game.GetCurrentPlayer();
            var result = _handler.Handle(seller.ID, signal.PlayerId, _cardsOffered, _cardsDesired);

            if (!result.Success)
            {
                return ResultPlayerTrade.Fail(result.Reason, seller.ID, signal.PlayerId);
            }

            Bus.Publish(new TradeOfferToTradeRequestEvent(_cardsOffered, _cardsDesired, signal.PlayerId));

            return ResultPlayerTrade.Ok(seller.ID, signal.PlayerId, _cardsOffered, _cardsDesired);
        }
    }
}