using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            Manager.EventBus.Subscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Subscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);

            Manager.EventBus.Subscribe<LogMessageEvent>(OnLogMessageReceived);
        }

        private void OnRobberPlaced(RobberPlacedEvent signal)
        {
            EventBus.Publish(new RobberMovedUIEvent(signal.HexId));
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundEvent signal)
        {
            var potentialVictimsData = Manager.PlayersQueryService.GetNotCurrentPlayersNames();
            UI.VictimSelectorPanel.Show(potentialVictimsData);
        }

        private void OnLogMessageReceived(LogMessageEvent signal)
        {
            Debug.Log($"{signal.Type}: {signal.Message}");
        }

        public override void OnExit()
        {

            Manager.EventBus.Unsubscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Unsubscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);

            Manager.EventBus.Unsubscribe<LogMessageEvent>(OnLogMessageReceived);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}