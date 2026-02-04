using Catan.Application.Controllers;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class YearOfPlentyCardPhase : BasePhase
    {
        private ResourceCostOrStock _cardsDesired = new();
        private readonly int _cardsToReceive = 2;

        public YearOfPlentyCardPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case ResourceCardSelectedCommand c:
                    HandleResourceCardClicked(c);
                    break;

                case CardSelectionAcceptedCommand c:
                    HandleResourcesSelected(c);
                    break;
            }
        }

        private void HandleResourceCardClicked(ResourceCardSelectedCommand signal)
        {
            EnumResourceTypes type = signal.Type;

            if (!signal.IsSelected)
            {
                if (_cardsDesired.Get(type) > 0)
                {
                    _cardsDesired.SubtractExactAmount(type, 1);
                }
            }

            if (signal.IsSelected)
            {
                _cardsDesired.AddExactAmount(type, 1);

            }

            bool canAccept = Facade.CheckIfExactCardsAmountSelected(_cardsDesired, _cardsToReceive);

            Bus.Publish(new SelectionChangedEvent(canAccept));
        }

        private void HandleResourcesSelected(CardSelectionAcceptedCommand signal)
        {
            var playerId = Facade.GetCurrentPlayerId();
            var result = Facade.UseYearOfPlenty(_cardsDesired);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));
            }

            foreach (var (key, amount) in result.Requested.ResourceDictionary)
            {
                if (amount <= 0)
                    continue;

                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Player{playerId} received {key} {amount} from Year Of Plenty card"));
            }

            PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
        }
    }
}