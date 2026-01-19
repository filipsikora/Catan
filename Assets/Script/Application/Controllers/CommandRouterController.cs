using Catan.Shared.Communication;
using System.Windows.Input;

namespace Catan.Application.Controllers
{
    public sealed class CommandRouterController
    {
        private PhaseTransitionController _phaseTransition;

        public CommandRouterController(PhaseTransitionController phaseTransition, EventBus bus)
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
