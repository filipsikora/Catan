using Catan.Application.Queries.Players;
using Catan.Shared.Communication;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;

namespace Vatan.Unity.Visuals.Controllers
{
    public sealed class ControllerPlayerUI
    {
        private readonly IPlayersQueryService _playersQuery;
        private readonly PlayerUI _playerUI;

        public ControllerPlayerUI(IPlayersQueryService playersQuery, PlayerUI playerUI, EventBus bus)
        {
            _playersQuery = playersQuery;
            _playerUI = playerUI;

            bus.Subscribe<PlayerStateChangedUIEvent>(UpdatePlayerUI);
        }

        public void UpdatePlayerUI(PlayerStateChangedUIEvent signal)
        {
            var data = _playersQuery.GetPlayersData(signal.PlayerId);
            var resources = _playersQuery.GetPlayersCards(signal.PlayerId);

            _playerUI.UpdatePlayerInfo(data, resources);
        }
    }
}