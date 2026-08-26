using Catan.Shared.Data;
using Catan.Unity.Models;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Caches
{
    public class GameCache
    {
        public BoardModel Board { get; set; }
        public MyPlayerModel MyPlayer { get; set; }
        public List<OtherPlayerModel> OtherPlayers { get; set; }
        public GameFlowModel GameFlow { get; set; }


        public GameCache(BoardModel board, MyPlayerModel myPlayer, List<OtherPlayerModel> otherPlayers, GameFlowModel gameFlow)
        {
            Board = board;
            MyPlayer = myPlayer;
            OtherPlayers = otherPlayers;
            GameFlow = gameFlow;
        }

        public Dictionary<int, int> GetOtherPlayersResourceCounts()
        {
            return OtherPlayers.ToDictionary(
                player => player.Id,
                player => player.ResourceCardsNumber
            );
        }

        public int GetPlayerPoints(int playerId)
        {
            if (MyPlayer.PlayerId == playerId)
            {
                return MyPlayer.Points;
            }

            var otherPlayer = OtherPlayers.FirstOrDefault(player => player.Id == playerId);

            return otherPlayer?.Points ?? 0;
        }

        public Dictionary<string, int> GetPlayerBuildingsCount(int playerId)
        {
            if (MyPlayer.PlayerId == playerId)
            {
                return MyPlayer.BuildingsLeft;
            }
            var otherPlayer = OtherPlayers.FirstOrDefault(player => player.Id == playerId);
            return otherPlayer?. ?? new Dictionary<string, int>();
        }
    }
}