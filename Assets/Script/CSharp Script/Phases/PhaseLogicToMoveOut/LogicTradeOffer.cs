using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Application.CommandHandlers;

namespace Catan.Core.Phases.Handlers
{
    public class LogicTradeOffer : BasePhaseLogic
    {
        private ResourceCostOrStock _cardsDesired = new();
        private ResourceCostOrStock _cardsOffered;

        private OfferTradeLogic _handler;

        public LogicTradeOffer(GameState game, EventBus bus, ResourceCostOrStock cardsOffered) : base(game, bus)
        {
            _cardsOffered = cardsOffered.Clone();
            _handler = new OfferTradeLogic(game);
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

        private void HandleTradePartnerChosen(TradePartnerChosenCommand signal)
        {
            var seller = Game.GetCurrentPlayer();
            var result = _handler.Handle(seller.ID, signal.PlayerId, _cardsOffered, _cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(seller.ID, result.Reason));

                return;
            }

            Bus.Publish(new TradeOfferToTradeRequestEvent(_cardsOffered, _cardsDesired, signal.PlayerId));
        }
    }
}