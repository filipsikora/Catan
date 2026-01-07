using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Adapters;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        public override void OnEnter()
        {
            Manager.EventBus.Subscribe<VictimSelectedEvent>(OnVictimSelected);

            Manager.EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            Manager.EventBus.Publish(new RequestCardStealingStartCommand());
        }

        public void OnVictimSelected(VictimSelectedEvent signal)
        {
            var playerResources = Manager.PlayersQueryService.GetPlayersCards(signal.VictimId);
            UI.CardTheftPanel.Show(playerResources);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventBus.Publish(new StolenCardSelectedCommand(signal.Type));
        }

        public override void OnExit()
        {
            Manager.EventBus.Unsubscribe<VictimSelectedEvent>(OnVictimSelected);

            Manager.EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
