using Catan.Shared.Data;
using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class AllPlayersResourcesChangedUIEvent : IInternalUIEvents
    {
        public Dictionary<int, int> PlayerIdsToResourcesCount { get; }

        public AllPlayersResourcesChangedUIEvent(Dictionary<int, int> playerIdsToResourcesCount)
        {
            PlayerIdsToResourcesCount = playerIdsToResourcesCount;
        }

    }

    public sealed class PlayersPointsChangedUIEvent : IInternalUIEvents
    {
        public int PlayerId { get; }
        public int Points { get; }

        public PlayersPointsChangedUIEvent(int playerId, int points)
        {
            PlayerId = playerId;
            Points = points;
        }
    }

    public sealed class PlayersBuildingsCountChangedUIEvent : IInternalUIEvents
    {
        public int PlayerId { get; }
        public Dictionary<EnumBuildings, int> BuildingsCount { get; }
        public PlayersBuildingsCountChangedUIEvent(int playerId, Dictionary<EnumBuildings, int> buildingsCount)
        {
            PlayerId = playerId;
            BuildingsCount = buildingsCount;
        }
    }
}