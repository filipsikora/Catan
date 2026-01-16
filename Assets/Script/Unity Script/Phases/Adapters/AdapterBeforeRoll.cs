using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Unity.Communication.InternalUIEvents;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBeforeRoll : BasePhaseAdapter
    {
        private BinderBeforeRoll _binder;

        public override void OnEnter()
        {
            _binder = new BinderBeforeRoll(UI, Manager.EventBus);
            _binder.Bind();

            var turnData = Manager.TurnsQueryService.GetTurnData();

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
