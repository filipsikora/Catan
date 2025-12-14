using NUnit.Framework;
using UnityEngine;
using Catan.Communication.Signals;

namespace Catan
{
    public class PlayerSetup : GamePhase
    {
        public override void OnEnter()
        {
            UI.PlayerSelectorPanel.SetActive(true);

            EventBus.Subscribe<PlayerCountSelectedSignal>(OnPlayerCountSelected);
        }

        public void OnPlayerCountSelected(PlayerCountSelectedSignal signal)
        {
            Manager.InitializeGame(signal.PlayerCount);

            UI.PlayerSelectorPanel.SetActive(false);
            UI.MainUIPanel.gameObject.SetActive(true);
            UI.PlayerUIPanel.gameObject.SetActive(true);

            Handler.TransitionTo(new FirstRoundsBuilding());
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<PlayerCountSelectedSignal>(OnPlayerCountSelected);
        }
    }
}
