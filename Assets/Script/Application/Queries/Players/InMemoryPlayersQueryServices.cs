using Catan.Application.Snapshots;
using Catan.Core.Engine;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Queries.Players
{
    public sealed class InMemoryPlayersQueryServices : IPlayersQueryService
    {
        private readonly GameState _game;

        public InMemoryPlayersQueryServices(GameState game)
        {
            _game = game;
        }

        public PlayerResourcesSnapshot GetPlayersCards(int playerId)
        {
            var player = _game.GetPlayerById(playerId);

            return new PlayerResourcesSnapshot(player.Resources.ToDictionary());
        }

        public PlayerDataSnapshot GetPlayersData(int playerId)
        {
            var player = _game.GetPlayerById(playerId);
            var playerBuildingsLeft = new Dictionary<string, int>();

            foreach (var buildingType in BuildingDataRegistry.MaxPerPlayer.Keys)
            {
                int maxAvailable = BuildingDataRegistry.MaxPerPlayer[buildingType];
                int playerUsed = player.BuildingCount(buildingType);
                int playerLeft = maxAvailable - playerUsed;

                playerBuildingsLeft.Add(BuildingDataRegistry.Name[buildingType], playerLeft);
            }

            return new PlayerDataSnapshot(player.Name, playerBuildingsLeft, player.Points, player.KnightsUsed, player.VictoryPointsCardsUsed, player.ExtraPoints);
        }

        public CurrentPlayerIdSnapshot GetCurrentPlayerId()
        {
            var currentPlayerId = _game.GetCurrentPlayer().ID;

            return new CurrentPlayerIdSnapshot(currentPlayerId);
        }

        public List<PlayerNameSnapshot> GetAllPlayersNames()
        {
            var allPlayersNamesList = new List<PlayerNameSnapshot>();

            foreach (var player in _game.PlayerList)
            {
                var playerNameData = new PlayerNameSnapshot(player.ID, player.Name);

                allPlayersNamesList.Add(playerNameData);
            }

            return allPlayersNamesList;
        }

        public List<PlayerNameSnapshot> GetSomePlayersNames(List<int> playersIds)
        {
            var playersData = new List<PlayerNameSnapshot>();

            foreach (var playerId in playersIds)
            {
                var player = _game.GetPlayerById(playerId);
                var playerNameData = new PlayerNameSnapshot(player.ID, player.Name);

                playersData.Add(playerNameData);
            }

            return playersData;
        }

        public List<PlayerNameSnapshot> GetNotCurrentPlayersNames()
        {
            var playersData = new List<PlayerNameSnapshot>();
            var currentPlayer = _game.GetCurrentPlayer();

            foreach (var player in _game.PlayerList)
            {
                if (player == currentPlayer)
                    continue;

                var playerData = new PlayerNameSnapshot(player.ID, player.Name);
                playersData.Add(playerData);
            }

            return playersData;
        }
    }
}