using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public AdapterRobberPlacing(ManagerUI ui, EventBus bus, Facade facade) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<RobberPlacedEvent>(OnRobberPlaced);
            EventBus.Subscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);
        }

        private void OnRobberPlaced(RobberPlacedEvent signal)
        {
            EventBus.Publish(new RobberMovedUIEvent(signal.HexId));
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundEvent signal)
        {
            var potentialVictimsData = Facade.GetSomePlayersNames(signal.VictimsIds);
            UI.VictimSelectorPanel.Show(potentialVictimsData);
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<RobberPlacedEvent>(OnRobberPlaced);
            EventBus.Unsubscribe<PotentialVictimsFoundEvent>(OnPotentialVictimsFound);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}