using Catan.Shared.Data;
using Catan.Unity.Models;
using System.Collections.Generic;

namespace Catan.Unity.Caches
{
    public class GameCache
    {
        public BoardModel Board { get; set; }
        public MyPlayerModel MyPlayer { get; set; }
        public List<OtherPlayerModel> OtherPlayers { get; set; }

        public int TurnNumber { get; set; }
        public int RolledNumber { get; set; }
        public int? CurrentPlayerId { get; set; }

        public int? KnightChampionId { get; set; }
        public int? RoadChampionId { get; set; }

        public EnumGamePhases CurrentPhase { get; set; }

        public GameCache(BoardModel board, MyPlayerModel myPlayer, List<OtherPlayerModel> otherPlayers, int turnNumber, int rolledNumber, int? currentPlayerId, int? knightChampionId, int? roadChampionId, EnumGamePhases currentPhase)
        {
            Board = board;
            MyPlayer = myPlayer;
            OtherPlayers = otherPlayers;
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            CurrentPlayerId = currentPlayerId;
            KnightChampionId = knightChampionId;
            RoadChampionId = roadChampionId;
            CurrentPhase = currentPhase;
        }
    }
}
