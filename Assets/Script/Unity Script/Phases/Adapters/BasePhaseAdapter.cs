#nullable enable
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;
using Catan.Unity.Caches;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerUI UI;
        protected EventBus EventBus;
        protected HandlerEvents EventsHandler;
        protected GameCache GameCache;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache)
        {
            UI = ui;
            EventBus = bus;
            EventsHandler = eventHandler;
            GameCache = gameCache;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}