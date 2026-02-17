using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterPlayerSetup
    {
        private readonly ManagerUI _ui;
        private readonly EventBus _bus;

        public AdapterPlayerSetup(ManagerUI ui, EventBus bus)
        {
            _ui = ui;
            _bus = bus;
        }

        public void OnEnter()
        {
            _ui.PlayerSelectorPanel.SetActive(true);

            _bus.Subscribe<PlayerCountSelectedCommand>(OnPlayerCountSelected);
        }

        public void OnPlayerCountSelected(PlayerCountSelectedCommand signal)
        {
            _bus.Publish(new StartGameRequestedEvent(signal.PlayerCount));

            _ui.PlayerSelectorPanel.SetActive(false);
            _ui.MainUIPanel.gameObject.SetActive(true);
            _ui.PlayerUIPanel.gameObject.SetActive(true);

            _bus.Unsubscribe<PlayerCountSelectedCommand>(OnPlayerCountSelected);
        }
    }
}
