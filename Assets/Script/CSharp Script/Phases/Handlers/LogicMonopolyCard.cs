using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public class LogicMonopolyCard : BasePhaseLogic
    {
        private EnumResourceTypes? _type;
        private UseMonopolyHandler _handler;

        public LogicMonopolyCard(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new UseMonopolyHandler(game);
        }

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
            var result = _handler.Handle(_type.Value);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Game.CurrentPlayer.ID, result.Reason));
                return;
            }

            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}