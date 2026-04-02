using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;

namespace Catan.Unity.Visuals.Controllers
{
    public sealed class ControllerPlayerUI
    {
        private readonly PlayerUI _playerUI;
        private readonly Facade _facade;

        public ControllerPlayerUI(Facade facade, PlayerUI playerUI, EventBus bus)
        {
            _facade = facade;
            _playerUI = playerUI;

            bus.Subscribe<PlayerStateChangedUIEvent>(UpdatePlayerUI);
        }

        public void UpdatePlayerUI(PlayerStateChangedUIEvent signal)
        {
            var data = _facade.GetPlayersData(signal.PlayerId);
            var resources = _facade.GetPlayersCards(signal.PlayerId);

            _playerUI.UpdatePlayerInfo(data, resources);
        }
    }
}