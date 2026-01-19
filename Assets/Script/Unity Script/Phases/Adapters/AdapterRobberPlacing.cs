using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            Manager.EventBus.Subscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Subscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);
        }

        private void OnRobberPlaced(RobberPlacedEvent signal)
        {
            EventBus.Publish(new RobberMovedUIEvent(signal.HexId));
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundEvent signal)
        {
            var potentialVictimsData = Manager.PlayersQueryService.GetSomePlayersNames(signal.VictimsIds);
            UI.VictimSelectorPanel.Show(potentialVictimsData);
        }

        public override void OnExit()
        {
            Manager.EventBus.Unsubscribe<RobberPlacedEvent>(OnRobberPlaced);
            Manager.EventBus.Unsubscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}