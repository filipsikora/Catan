using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using Catan.Core.PhaseLogic;

namespace Catan.Application.Phases
{
    public class TradeOfferPhase : BasePhase
    {
        private ResourceCostOrStock _cardsDesired = new();
        private ResourceCostOrStock _cardsOffered;

        public TradeOfferPhase(GameState game, EventBus bus, ResourceCostOrStock cardsOffered, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition)
        {
            _cardsOffered = cardsOffered.Clone();
        }

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

            bool hasDesired = _cardsDesired.Total() > 0;

            Bus.Publish(new DesiredCardsChangedEvent(hasDesired));
        }

        private void HandleTradePartnerChosen(TradePartnerChosenCommand signal)
        {
            var seller = Game.GetCurrentPlayer();
            var result = OfferTradeLogic.Handle(Game, seller.ID, signal.PlayerId, _cardsOffered, _cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(seller.ID, result.Reason));

                return;
            }

            PhaseTransition.ChangePhase(EnumGamePhases.TradeRequest);
        }
    }
}