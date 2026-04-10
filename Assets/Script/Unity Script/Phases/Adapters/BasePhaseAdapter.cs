#nullable enable
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerUI UI;
        protected EventBus EventBus;
        protected HandlerEvents EventsHandler;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, EventBus bus, HandlerEvents eventHandler)
        {
            UI = ui;
            EventBus = bus;
            EventsHandler = eventHandler;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}