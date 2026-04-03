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
        private int _thiefData;

        public AdapterCardStealing(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            ShowVictimsCards();
        }

        public void ShowVictimsCards()
        {
            var victimResources = Facade.GetVictimsCards();

            UI.CardTheftPanel.Show(victimResources);
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
