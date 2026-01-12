using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Core.Engine;
using Catan.Core.Models;
using System.Linq;

namespace Catan.Core.Phases.Handlers
{
    public class LogicTradeOffer : BasePhaseLogic
    {
        private ResourceCostOrStock _cardsDesired = new();
        private ResourceCostOrStock _cardsOffered;

        public LogicTradeOffer(GameState game, EventBus bus, ResourceCostOrStock cardsOffered) : base(game, bus)
        {
            _cardsOffered = cardsOffered.Clone();
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
                    Bus.Publish(new TradeOfferToTradeRequestEvent(_cardsOffered, _cardsDesired, c.PlayerId));
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
    }
}