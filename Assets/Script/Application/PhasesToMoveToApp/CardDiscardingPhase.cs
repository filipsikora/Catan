using Catan.Application.Controllers;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class CardDiscardingPhase : BasePhase
    {
        private ResourceCostOrStock _resourcesSelected = new();
        private int _currentDiscardingPlayerId;

        public CardDiscardingPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceSelectionChanged(c);
                    break;

                case DiscardingAcceptedCommand c:
                    HandleDiscardingAccepted(c);
                    break;

                case RequestCardDiscardingStartCommand c:
                    ProceedToNextPlayer();
                    break;
            }
        }

        private void ProceedToNextPlayer()
        {
            var playersLeft = Facade.GetPlayersToDiscardCount();

            if (playersLeft == 0)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.RobberPlacing);

                return;
            }

            _currentDiscardingPlayerId = Facade.GetNextToDiscardId();
            _resourcesSelected = new ResourceCostOrStock();

            Bus.Publish(new PlayerSelectedToDiscardEvent(_currentDiscardingPlayerId));
        }

        private void HandleResourceSelectionChanged(ResourceCardSelectedCommand signal)
        {
            if (!signal.IsSelected)
            {
                _resourcesSelected.AddExactAmount(signal.Type, 1);
            }

            else
            {
                _resourcesSelected.SubtractExactAmount(signal.Type, 1);
            }

            var canDiscard = Facade.CanPlayerDiscard(_resourcesSelected, _currentDiscardingPlayerId);

            Bus.Publish(new SelectionChangedEvent(canDiscard));
        }

        private void HandleDiscardingAccepted(DiscardingAcceptedCommand signal)
        {
            var result = Facade.UseDiscard(_currentDiscardingPlayerId, _resourcesSelected);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(_currentDiscardingPlayerId, result.Reason));

                return;
            }

            ProceedToNextPlayer();
        }
    }
}