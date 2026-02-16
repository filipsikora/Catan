using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Models;
using Catan.Application.Controllers;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class TradeOfferPhase : BasePhase
    {
        private readonly ResourceCostOrStock _cardsDesired = new();

        public TradeOfferPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceCardClicked(c);
                    break;

                case TradeOfferCanceledCommand c:
                    PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
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

            bool hasDesired = Facade.CheckIfCardsSelected(_cardsDesired);

            Bus.Publish(new DesiredCardsChangedEvent(hasDesired));
        }

        private void HandleTradePartnerChosen(TradePartnerChosenCommand signal)
        {
            var buyerId = signal.PlayerId;
            var result = Facade.UseOfferTrade(buyerId, _cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.SellerId, result.Reason));
                return;
            }

            TransitionPhase(result);
        }
    }
}