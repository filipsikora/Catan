using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public abstract class BasePhase
    {
        protected GameState Game;
        protected EventBus Bus;
        protected PhaseTransitionController PhaseTransition;

        protected BasePhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition)
        {
            Game = game;
            Bus = bus;
            PhaseTransition = phaseTransition;
        }

        public abstract void Handle(object command);

        public abstract void Enter();

        protected void FinishPhase(EnumGamePhases phase)
        {
            PhaseTransition.ChangePhase(phase);
        }
    }
}
