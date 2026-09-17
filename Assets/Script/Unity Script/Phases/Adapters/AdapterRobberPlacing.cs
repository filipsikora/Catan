using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Visuals;
using System.Linq;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterRobberPlacing : BasePhaseAdapter
    {
        public AdapterRobberPlacing(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<HexClickedUIEvent>(OnHexClicked);

            EventBus.Subscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            EventBus.Subscribe<PlayerClickedUIEvent>(OnPlayerChosen);
        }

        private void OnHexClicked(HexClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.HexClickedCommand, new { hexId = signal.HexId });
        }

        private void OnPotentialVictimsFound(PotentialVictimsFoundUIEvent signal)
        {
            var potentialVictims = signal.VictimsIds.Select(x => GameCache.OtherPlayers.FirstOrDefault(p => p.Id == x)).Where(x => x != null).ToList();

            UI.VictimSelectorPanel.Show(potentialVictims, EventBus);
        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.VictimChosenCommand, new { victimId = signal.PlayerId });
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<HexClickedUIEvent>(OnHexClicked);

            EventBus.Unsubscribe<PotentialVictimsFoundUIEvent>(OnPotentialVictimsFound);

            EventBus.Unsubscribe<PlayerClickedUIEvent>(OnPlayerChosen);
        }
    }
}