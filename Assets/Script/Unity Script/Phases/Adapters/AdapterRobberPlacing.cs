using Catan.Shared.Communication.Events;
using Catan.Core.Models;
using Catan.Unity.Visuals;
using System.Linq;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            Manager.EventBus.Subscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Subscribe<PotentialVictimsSelectedEvent>(OnPotentialVictimsSelected);

            Manager.EventBus.Subscribe<LogMessageEvent>(OnLogMessageReceived);
        }

        private void OnRobberPlaced(RobberPlacedEvent signal)
        {
            HexTile hex = Manager.Game.Map.GetHexById(signal.HexId);

            Manager.BoardVisuals.MoveRobberObject(hex);
        }

        private void OnPotentialVictimsSelected(PotentialVictimsSelectedEvent signal)
        {
            var victimsList = signal.VictimsIds.Select(id => Manager.Game.GetPlayerById(id)).ToList();
            UI.VictimSelectorPanel.Show(victimsList);
        }

        private void OnLogMessageReceived(LogMessageEvent signal)
        {
            Debug.Log($"{signal.Type}: {signal.Message}");
        }

        public override void OnExit()
        {

            Manager.EventBus.Unsubscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Unsubscribe<PotentialVictimsSelectedEvent>(OnPotentialVictimsSelected);

            Manager.EventBus.Unsubscribe<LogMessageEvent>(OnLogMessageReceived);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}