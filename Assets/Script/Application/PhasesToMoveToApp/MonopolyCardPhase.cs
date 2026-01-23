using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Core.PhaseLogic;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class MonopolyCardPhase : BasePhase
    {
        private EnumResourceTypes? _type;

        public MonopolyCardPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter() { }

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
            var result = UseMonopolyLogic.Handle(Game, _type.Value);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            PhaseTransition.ChangePhase(EnumGamePhases.MonopolyCard);
        }
    }
}