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
        protected HandlerEvents EventHandler;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, Facade facade, EventBus bus, HandlerEvents eventHandler)
        {
            UI = ui;
            Facade = facade;
            EventBus = bus;
            EventHandler = eventHandler;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}