using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using System.Linq;
using static Catan.Shared.Communication.Events.ResourcesAvailabilityEvent;

namespace Catan.Core.Phases.Handlers
{
    public class LogicYearOfPlentyCard : BasePhaseLogic
    {
        private ResourceCostOrStock _cardsDesired = new();
        private readonly int _cardsToReceive = 2;

        public LogicYearOfPlentyCard(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

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
            var availability = Game.CheckResourcesAvailabilityAfterChange(_cardsDesired);

            if (!signal.IsSelected)
            {
                if (_cardsDesired.ResourceDictionary[type] > 0)
                {
                    _cardsDesired.SubtractExactAmount(type, 1);
                }
            }

            if (signal.IsSelected)
            {
                if (availability[type])
                {
                    _cardsDesired.AddExactAmount(type, 1);
                }

                else
                {
                    Bus.Publish(new LogMessageEvent(EnumLogTypes.Error, "Not enough resources in the bank"));
                }
            }

            bool canAccept = _cardsDesired.Total() == _cardsToReceive;

            Bus.Publish(new SelectionChangedEvent(canAccept));
        }

        private void HandleResourcesSelected(CardSelectionAcceptedCommand signal)
        {
            var results = Game.UseYearOfPlenty(_cardsDesired);

            foreach (var r in results)
            {
                if (r.Granted <= 0)
                    continue;

                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"Player{r.PlayerId} received {r.Requested} {r.Type} from Year Of Plenty card"));
            }

            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}