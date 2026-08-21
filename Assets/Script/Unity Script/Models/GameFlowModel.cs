using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Unity.Models
{
    public class GameFlowModel
    {
        public int TurnNumber { get; set; }
        public int RolledNumber { get; set; }
        public int? CurrentPlayerId { get; set; }

        public int? KnightChampionId { get; set; }
        public int? RoadChampionId { get; set; }

        public EnumGamePhases CurrentPhase { get; set; }
        public Dictionary<EnumResourceType, int> Bank { get; set; }

        public List<int> PlayersToMove { get; set; }

        public GameFlowModel(int turnNumber, int rolledNumber, int? currentPlayerId, int? knightChampionId, int? roadChampionId, EnumGamePhases currentPhase, Dictionary<EnumResourceType, int> bank,
            List<int> playersToMove)
        {
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            CurrentPlayerId = currentPlayerId;
            KnightChampionId = knightChampionId;
            RoadChampionId = roadChampionId;
            CurrentPhase = currentPhase;
            Bank = bank;
            PlayersToMove = playersToMove;
        }
    }
}