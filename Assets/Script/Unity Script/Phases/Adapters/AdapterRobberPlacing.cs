using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Visuals;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public AdapterRobberPlacing(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void OnEnter()
        {
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<HexClickedUIEvent>(OnHexClicked);

            EventBus.Subscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            EventBus.Subscribe<PlayerClickedUIEvent>(OnPlayerChosen);
        }

        private void OnHexClicked(HexClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.HexClickedCommand, signal.HexId);
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundUIEvent signal)
        {
            _ = LoadData();

        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.VictimChosenCommand, signal.PlayerId);
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<List<PlayerNameDto>>(EnumQueryName.SomePlayersNames);
            UI.VictimSelectorPanel.Show(snapshot);

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