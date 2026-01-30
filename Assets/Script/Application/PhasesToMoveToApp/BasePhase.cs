using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Data;

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

        protected void FinishPhase(EnumGamePhases phase)
        {
            PhaseTransition.ChangePhase(phase);
        }
    }
}
