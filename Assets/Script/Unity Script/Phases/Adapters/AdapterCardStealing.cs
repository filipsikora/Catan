using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Adapters;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterCardStealing : BasePhaseAdapter
    {
        private int _victimId;
        public override void OnEnter()
        {
            Manager.EventBus.Subscribe<VictimSelectedEvent>(OnVictimSelected);

            Manager.EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            Manager.EventBus.Publish(new RequestCardStealingStartCommand());
        }

        public void OnVictimSelected(VictimSelectedEvent signal)
        {
            UI.CardTheftPanel.Show(signal.VictimsCards);
            _victimId = signal.VictimId;
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

            var thief = Manager.Game.GetCurrentPlayer();
            var victim = Manager.Game.GetPlayerById(_victimId);

            UI.UpdatePlayerInfo(thief);
            UI.UpdatePlayerInfo(victim);

            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
