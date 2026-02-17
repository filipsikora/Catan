using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Adapters;
using Catan.Core.Snapshots;
using Catan.Unity.Panels;
using Catan.Shared.Communication;
using Catan.Application.Controllers;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        private PlayerNameSnapshot _victimName;
        private PlayerResourcesSnapshot _victimResources;
        private int _thiefData;

        public AdapterCardStealing(ManagerUI ui, EventBus bus, Facade facade) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            _victimName = Facade.GetVictimsName();
            OnVictimSelected(_victimName.Id);
        }

        public void OnVictimSelected(int victimId)
        {
            _thiefData = Facade.GetCurrentPlayerId();
            _victimResources = Facade.GetPlayersCards(victimId);
            UI.CardTheftPanel.Show(_victimResources);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventBus.Publish(new StolenCardSelectedCommand(signal.Type));
        }

        public override void OnExit()
        {
            EventBus.Publish(new PlayerStateChangedUIEvent(_thiefData));

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
