using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using static Catan.Shared.Communication.Events.ResourcesAvailabilityEvent;

namespace Catan.Core.Phases.Handlers
{
    public class LogicMonopolyCard : BasePhaseLogic
    {
        private EnumResourceTypes? _type;

        public LogicMonopolyCard(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter() { }

        public override void Exit() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case StolenCardSelectedCommand c:
                    HandleResourceSelected(c);
                    break;

                case CardSelectionAcceptedCommand c:
                    HandleResourceAccepted(c);
                    break;
            }
        }

        private void HandleResourceSelected(StolenCardSelectedCommand signal)
        {
            if (_type == signal.Type)
            {
                _type = null;
            }
            else
            {
                _type = signal.Type;
            }

            bool hasSelected = _type != null;

            Bus.Publish(new ResourceSelectedEvent(hasSelected, signal.Type));
        }

        private void HandleResourceAccepted(CardSelectionAcceptedCommand signal)
        {
            if (_type == null)
                return;

            var resultMonopolyCard = Game.UseMonopoly(_type.Value);

            foreach (var r in resultMonopolyCard)
            {
                if (r.Amount < 1)
                    continue;

                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"{r.PlayerId} steals {r.Amount} {r.Type} from {r.VictimId}"));
            }

            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}