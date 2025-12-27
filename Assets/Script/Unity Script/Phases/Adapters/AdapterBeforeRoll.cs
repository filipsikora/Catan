using Catan.Shared.Communication.Events;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBeforeRoll : BasePhaseAdapter
    {
        private BinderBeforeRoll _binder;

        public override void OnEnter()
        {
            _binder = new BinderBeforeRoll(UI, Manager.EventBus);
            _binder.Bind();

            UI.UpdatePlayerInfo(Manager.Game.GetCurrentPlayer());

            EventBus.Subscribe<LogMessageEvent>(OnLogMessageReceived);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowRollDiceUI(UI.MainUIPanel);
            UI.MainUIPanel.DevelopmentCardsButton.gameObject.SetActive(true);
        }

        private void OnLogMessageReceived(LogMessageEvent signal)
        {
            Debug.Log($"{signal.Type}: {signal.Message}");
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<LogMessageEvent>(OnLogMessageReceived);
        }
    }
}
