using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;
using Catan.Application.Controllers;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBeforeRoll : BasePhaseAdapter
    {
        private BinderBeforeRoll _binder;

        public AdapterBeforeRoll(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventHandler) : base(ui, bus, facade, eventHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderBeforeRoll(UI, EventBus, EventsHandler);
            _binder.Bind();
            
            var turnData = Facade.GetTurnData();

            EventBus.Publish(new PlayerStateChangedUIEvent(turnData.PlayerId));

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
