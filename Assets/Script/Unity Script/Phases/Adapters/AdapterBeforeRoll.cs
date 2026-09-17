using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBeforeRoll : BasePhaseAdapter
    {
        private BinderBeforeRoll _binder;

        public AdapterBeforeRoll(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            _binder = new BinderBeforeRoll(UI, EventBus, EventsHandler);
            _binder.Bind();
        }

        public override void OnExit()
        {
            _binder.Unbind();
        }
    }
}