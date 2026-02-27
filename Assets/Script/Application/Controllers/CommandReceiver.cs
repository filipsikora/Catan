using Catan.Shared.Communication;
using Catan.Shared.Interfaces;

namespace Catan.Application.Controllers
{
    public sealed class CommandReceiver
    {
        private readonly PhaseTransitionController _phaseTransition;

        public CommandReceiver(PhaseTransitionController phaseTransition, EventBus bus)
        {
            _phaseTransition = phaseTransition;
            bus.Subscribe<ICommand>(Handle);
        }

        private void Handle(ICommand command)
        {
            _phaseTransition.Current.Handle(command);
        }
    }
}