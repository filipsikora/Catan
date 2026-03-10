#nullable enable
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Application.Controllers;
using Catan.Unity.Helpers;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerUI UI;
        protected Facade Facade;
        protected EventBus EventBus;
        protected HandlerEvents EventsHandler;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventHandler)
        {
            UI = ui;
            Facade = facade;
            EventBus = bus;
            EventsHandler = eventHandler;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}