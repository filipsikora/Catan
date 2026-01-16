using Catan.Application.Snapshots;
using Catan.Core.Engine;

namespace Catan.Application.Queries.Turns
{
    public sealed class InMemoryTurnsQueryService : ITurnsQueryService
    {
        private readonly GameState _game;

        public InMemoryTurnsQueryService(GameState game)
        {
            _game = game;
        }

        public TurnDataSnapshot GetTurnData()
        {
            var currentPlayer = _game.GetCurrentPlayer();
            var turnNumber = _game.Turn;
            var rolledNumber = _game.LastRoll;
            var initialRoundsRemaining = _game.FirstRoundsIndices.Count > 0;

            return new TurnDataSnapshot(currentPlayer.ID, currentPlayer.Name, turnNumber, rolledNumber, initialRoundsRemaining);
        }
    }
}