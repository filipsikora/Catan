using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using System.Threading.Tasks;

namespace Catan.Unity.Visuals.Controllers
{
    public sealed class ControllerPlayerUI
    {
        private readonly PlayerUI _playerUI;
        private readonly HandlerEvents _eventsHandler;

        public ControllerPlayerUI(HandlerEvents eventsHandler, PlayerUI playerUI, EventBus bus)
        {
            _eventsHandler = eventsHandler;
            _playerUI = playerUI;

            bus.Subscribe<PlayerStateChangedUIEvent>(UpdatePlayerUI);
        }

        public async void UpdatePlayerUI(PlayerStateChangedUIEvent signal)
        {
            var data = await LoadPlayerData(signal.PlayerId);
            var resources = await LoadPlayerCards(signal.PlayerId);

            _playerUI.UpdatePlayerInfo(data, resources);
        }

        private async Task<PlayerDataDto> LoadPlayerData(int playerId)
        {
            return await _eventsHandler.Query<PlayerDataDto>(EnumQueryName.PlayerData);
        }

        private async Task<PlayerCardsDto> LoadPlayerCards(int playerId)
        {
            return await _eventsHandler.Query<PlayerCardsDto>(EnumQueryName.PlayerCards);
        }
    }
}