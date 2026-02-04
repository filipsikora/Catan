using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryTurnsQueryService : ITurnsQueryService
    {
        private readonly GameSession _session;

        public InMemoryTurnsQueryService(GameSession session)
        {
            _session = session;
        }

        public TurnDataSnapshot GetTurnData()
        {
            var currentPlayer = _session.GetCurrentPlayer();
            var turnNumber = _session.GetTurn();
            var rolledNumber = _session.GetLastRoll();
            var initialRoundsRemaining = _session.CheckIfInitialRoundsRemaining();

            return new TurnDataSnapshot(currentPlayer.ID, currentPlayer.Name, turnNumber, rolledNumber, initialRoundsRemaining);
        }
    }
}