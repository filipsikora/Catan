using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan.Core
{
    public abstract class BaseHandler
    {
        protected GameState Game;
        protected EventBus Bus;

        protected BaseHandler(GameState game, EventBus bus)
        {
            Game = game;
            Bus = bus;
        }

        public virtual void Dispose() { }
    }
}