using Catan.Shared.Communication;
using Catan.Core.Engine;
using Catan.Core.Interfaces;

namespace Catan.Core.Phases.Handlers
{
    public abstract class BasePhaseLogic : IPhaseLogic
    {
        protected GameState Game { get; }
        protected EventBus Bus { get; }

        protected BasePhaseLogic(GameState game, EventBus bus)
        {
            Game = game;
            Bus = bus;
        }

        public abstract void Enter();
        public abstract void Exit();
        public abstract void Handle(object command);
    }
}