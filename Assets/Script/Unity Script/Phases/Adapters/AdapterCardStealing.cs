using Catan.Shared.Commands;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Phases.Adapters;
using Catan.Core.Snapshots;
using Catan.Unity.Panels;
using Catan.Unity.Helpers;
using Catan.Application.Controllers;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        private PlayerNameSnapshot _victimName;
        private PlayerResourcesSnapshot _victimResources;
        private int _thiefData;

        public AdapterCardStealing(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            _victimName = Facade.GetVictimsName();

            ShowVictimsCards(_victimName.Id);
        }

        public void ShowVictimsCards(int victimId)
        {
            _victimResources = Facade.GetPlayersCards(victimId);

            UI.CardTheftPanel.Show(_victimResources);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(new StolenCardSelectedCommand(signal.Type));
        }

        public override void OnExit()
        {
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
