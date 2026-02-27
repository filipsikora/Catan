using Catan.Application.Controllers;
using Catan.Core.Results;
using Catan.Shared.Communication;

namespace Catan.Application.Phases
{
    public abstract class BasePhase
    {
        protected EventBus Bus;
        protected PhaseTransitionController PhaseTransition;
        protected Facade Facade;

        protected BasePhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition)
        {
            Bus = bus;
            PhaseTransition = phaseTransition;
            Facade = facade;
        }

        public abstract void Handle(object command);

        public abstract void Enter();

        protected void TransitionPhase(ResultBase result)
        {
            if (result.NextPhase != null)
            {
                PhaseTransition.ChangePhase(result.NextPhase.Value);
            }
        }
    }
}