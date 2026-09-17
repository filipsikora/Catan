using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;

namespace Catan.Unity.Visuals.Controllers
{
    public sealed class ControllerPlayerUI
    {
        private readonly PlayerUI _playerUI;
        private readonly GameCache _gameCache;

        public ControllerPlayerUI(PlayerUI playerUI, EventBus bus, GameCache gameCache)
        {
            _playerUI = playerUI;
            _gameCache = gameCache;

            bus.Subscribe<MyResourcesChangedUIEvent>(UpdateMyResources);
        }

        private void UpdateMyResources(MyResourcesChangedUIEvent signal)
        {
            _playerUI.UpdateResources(_gameCache.MyPlayer.Resources);
        }
    }
}