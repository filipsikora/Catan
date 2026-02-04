using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;
using System.Collections.Generic;
using Catan.Shared.Data;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryPlayersQueryServices : IPlayersQueryService
    {
        private readonly GameSession _session;

        public InMemoryPlayersQueryServices(GameSession session)
        {
            _session = session;
        }

        public PlayerResourcesSnapshot GetPlayersCards(int playerId)
        {
            var player = _session.GetPlayerById(playerId);

            return new PlayerResourcesSnapshot(player.Resources.ToDictionary());
        }

        public PlayerDataSnapshot GetPlayersData(int playerId)
        {
            var player = _session.GetPlayerById(playerId);
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
            var currentPlayerId = _session.GetCurrentPlayerId();

            return new CurrentPlayerIdSnapshot(currentPlayerId);
        }

        public List<PlayerNameSnapshot> GetAllPlayersNames()
        {
            var allPlayersNamesList = new List<PlayerNameSnapshot>();

            foreach (var player in _session.GetAllPlayersView())
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
                var player = _session.GetPlayerById(playerId);
                var playerNameData = new PlayerNameSnapshot(player.ID, player.Name);

                playersData.Add(playerNameData);
            }

            return playersData;
        }

        public List<PlayerNameSnapshot> GetNotCurrentPlayersNames()
        {
            var playersData = new List<PlayerNameSnapshot>();
            var currentPlayer = _session.GetCurrentPlayer();

            foreach (var player in _session.GetAllPlayersView())
            {
                if (player == currentPlayer)
                    continue;

                var playerData = new PlayerNameSnapshot(player.ID, player.Name);
                playersData.Add(playerData);
            }

            return playersData;
        }

        public PlayerNameSnapshot GetVictimsName()
        {
            var victimId = _session.GetVictimId();
            var victim = _session.GetPlayerById(victimId);

            return new PlayerNameSnapshot(victim.ID, victim.Name);
        }
    }
}