using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using System.Runtime.ExceptionServices;

namespace Catan.Application.CommandHandlers
{
    public sealed class FinishTurnHandler
    {
        private GameState _game;

        public FinishTurnHandler(GameState game)
        {
            _game = game;
        }

        public ResultFinishTurn Handle(Player player)
        {
            var currentPlayer = _game.GetCurrentPlayer();

            int nextIndex;
            bool initialRoundsRemaining;

            _game.MarkDevCardsAsOldMutation(currentPlayer);

            if (_game.FirstRoundsIndices.Count > 0)
            {
                _game.FirstRoundsIndices.Dequeue();

                initialRoundsRemaining = _game.FirstRoundsIndices.Count > 0;
                nextIndex = initialRoundsRemaining ? _game.FirstRoundsIndices.Peek() : 0;
            }

            else
            {
                initialRoundsRemaining = false;
                nextIndex = (_game.CurrentPlayerIndex + 1) % _game.PlayerList.Count;
            }

            _game.AdvanceToNextPlayerMutation(nextIndex);
            _game.WinCheck();

                return new ResultFinishTurn(_game.CurrentPlayer.ID, initialRoundsRemaining);
        }
    }
}
