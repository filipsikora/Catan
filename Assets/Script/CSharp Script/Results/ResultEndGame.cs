using System.Collections.Generic;

namespace Catan.Core.Results
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