using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBeforeRoll : BasePhaseAdapter
    {
        private BinderBeforeRoll _binder;

        public AdapterBeforeRoll(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderBeforeRoll(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowRollDiceUI(UI.MainUIPanel);
            UI.MainUIPanel.DevelopmentCardsButton.gameObject.SetActive(true);
        }

        public override void OnExit()
        {
            _binder.Unbind();
        }
    }
}