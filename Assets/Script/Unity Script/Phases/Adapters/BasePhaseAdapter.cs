#nullable enable
using Catan.Shared.Communication;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Application.Controllers;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerUI UI;
        protected EventBus EventBus;
        protected Facade Facade;

        internal AdapterPhaseTransition? Handler;

        public BasePhaseAdapter(ManagerUI ui, EventBus eventBus, Facade facade)
        {
            UI = ui;
            EventBus = eventBus;
            Facade = facade;
        }

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}