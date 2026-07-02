using Catan.Shared.Data;

namespace Catan.Shared.Dtos
{
    public sealed class FullGameFlowDto
    {
        public int TurnNumber;
        public int RolledNumber;
        public int? CurrentPlayerId;

        public int? KnightChampionId;
        public int? RoadChampionId;

        public EnumGamePhases CurrentPhase;
    }
}