using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using System;
using System.Threading.Tasks;

namespace Catan.Unity.Visuals.Controllers
{
    public sealed class ControllerPlayerUI
    {
        private readonly PlayerUI _playerUI;
        private readonly GameClient _client;

        private readonly Guid _gameId;

        public ControllerPlayerUI(GameClient client, PlayerUI playerUI, Guid gameId, EventBus bus)
        {
            _client = client;
            _playerUI = playerUI;
            _gameId = gameId;

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
            return await _client.SendQuery<PlayerDataDto>(_gameId, EnumQueryName.PlayerData);
        }

        private async Task<PlayerCardsDto> LoadPlayerCards(int playerId)
        {
            return await _client.SendQuery<PlayerCardsDto>(_gameId, EnumQueryName.PlayerCards);
        }
    }
}