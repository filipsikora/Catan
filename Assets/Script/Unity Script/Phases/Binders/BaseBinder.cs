using Catan.Unity.Helpers;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public abstract class BaseBinder
    {
        protected ManagerUI UI;
        protected EventBus Bus;
        protected HandlerEvents EventHandler;

        public BaseBinder(ManagerUI ui, EventBus bus, HandlerEvents eventHandler)
        {
            UI = ui;
            Bus = bus;
            EventHandler = eventHandler;
        }

        public abstract void Bind();

        public abstract void Unbind();
    }
}