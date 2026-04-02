using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Visuals;
using Catan.Shared.Commands;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public AdapterRobberPlacing(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<HexClickedUIEvent>(OnHexClicked);

            EventBus.Subscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            EventBus.Subscribe<PlayerClickedUIEvent>(OnPlayerChosen);
        }

        private void OnHexClicked(HexClickedUIEvent signal)
        {
            EventsHandler.Execute(new HexClickedCommand(signal.HexId));
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundUIEvent signal)
        {
            var potentialVictimsData = Facade.GetSomePlayersNames(signal.VictimsIds);
            UI.VictimSelectorPanel.Show(potentialVictimsData);
        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(new VictimChosenCommand(signal.PlayerId));
        }


        public override void OnExit()
        {
            EventBus.Unsubscribe<HexClickedUIEvent>(OnHexClicked);

            EventBus.Unsubscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            EventBus.Unsubscribe<PlayerClickedUIEvent>(OnPlayerChosen);

            UI.VictimSelectorPanel.gameObject.SetActive(false);
        }
    }
}