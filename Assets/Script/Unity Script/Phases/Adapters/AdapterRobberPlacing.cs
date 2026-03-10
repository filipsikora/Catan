using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public AdapterRobberPlacing(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler) { }

        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundUIEvent signal)
        {
            var potentialVictimsData = Facade.GetSomePlayersNames(signal.VictimsIds);
            UI.VictimSelectorPanel.Show(potentialVictimsData);
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}