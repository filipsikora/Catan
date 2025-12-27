using Catan.Shared.Communication;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public abstract class BaseBinder
    {
        protected ManagerUI UI;
        protected EventBus Bus;

        public BaseBinder(ManagerUI ui, EventBus bus)
        {
            UI = ui;
            Bus = bus;
        }

        public abstract void Bind();

        public abstract void Unbind();
    }
}