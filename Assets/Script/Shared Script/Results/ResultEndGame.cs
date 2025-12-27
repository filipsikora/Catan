using System.Collections.Generic;
using System.Data;

namespace Catan.Shared.Results
{
    public class ResultEndGame
    {
        public Dictionary<int, int> PlayerScoresToIds;
        public int WinnderId;
        public ResultEndGame(Dictionary<int, int> playerScoresToIds, int winnerId)
        {
            PlayerScoresToIds = playerScoresToIds;
            WinnderId = winnerId;
        }
    }
}