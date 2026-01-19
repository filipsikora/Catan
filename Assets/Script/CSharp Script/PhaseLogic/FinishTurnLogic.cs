using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.PhaseLogic
{
    public static class FinishTurnLogic
    {
        public static ResultFinishTurn Handle(GameState game, Player player)
        {
            var currentPlayer = game.GetCurrentPlayer();

            int nextIndex;
            bool initialRoundsRemaining;

            game.MarkDevCardsAsOldMutation(currentPlayer);

            if (game.FirstRoundsIndices.Count > 0)
            {
                game.FirstRoundsIndices.Dequeue();

                initialRoundsRemaining = game.FirstRoundsIndices.Count > 0;
                nextIndex = initialRoundsRemaining ? game.FirstRoundsIndices.Peek() : 0;
            }

            else
            {
                initialRoundsRemaining = false;
                nextIndex = (game.CurrentPlayerIndex + 1) % game.PlayerList.Count;
            }

            game.AdvanceToNextPlayerMutation(nextIndex);
            game.WinCheck();

            return new ResultFinishTurn(game.CurrentPlayer.ID, initialRoundsRemaining);
        }
    }
}
