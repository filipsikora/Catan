using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
    public sealed class FullGameFlowDto
    {
        public int TurnNumber;
        public int RolledNumber;
        public int? CurrentPlayerId;
        public List<int> PlayersToMove;

        public int? KnightChampionId;
        public int? RoadChampionId;

        public EnumGamePhases CurrentPhase;
        public Dictionary<EnumResourceType, int> Bank;
    }
}