#nullable enable
using Catan.Shared.Communication;
using Catan.Unity.Phases.Controllers;
using Catan.Unity.Panels;
using Catan.Unity;

namespace Catan.Unity.Phases.Adapters
{
    public abstract class BasePhaseAdapter
    {
        protected ManagerGame Manager => ManagerGame.Instance;
        protected ManagerUI UI => ManagerGame.Instance.UIManager;
        internal AdapterPhaseTransition? Handler;
        protected EventBus EventBus => Manager.EventBus;

        public virtual void OnEnter() { }
        public virtual void OnExit() { }
    }
}