using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterPlayerSetup : BasePhaseAdapter
    {
        public override void OnEnter()
        {
            UI.PlayerSelectorPanel.SetActive(true);

            EventBus.Subscribe<PlayerCountSelectedCommand>(OnPlayerCountSelected);
        }

        public void OnPlayerCountSelected(PlayerCountSelectedCommand signal)
        {
            EventBus.Publish(new StartGameRequestedEvent(signal.PlayerCount));
        }

        public override void OnExit()
        {
            UI.PlayerSelectorPanel.SetActive(false);
            UI.MainUIPanel.gameObject.SetActive(true);
            UI.PlayerUIPanel.gameObject.SetActive(true);

            EventBus.Unsubscribe<PlayerCountSelectedCommand>(OnPlayerCountSelected);
        }
    }
}
