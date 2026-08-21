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
        public GameFlowModel GameFlow { get; set; }


        public GameCache(BoardModel board, MyPlayerModel myPlayer, List<OtherPlayerModel> otherPlayers, GameFlowModel gameFlow)
        {
            Board = board;
            MyPlayer = myPlayer;
            OtherPlayers = otherPlayers;
            GameFlow = gameFlow;
        }
    }
}