using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class FinishTurnLogic : BaseLogic
    {
        public FinishTurnLogic(GameSession session) : base(session) { }

        public ResultFinishTurn Handle()
        {
            var player = Session.GetCurrentPlayer();

            Session.MarkDevCardsAsOldMutation();

            (int nextIndex, bool initialRoundsRemaining) = Session.GetNextIndex();

            Session.AdvanceToNextPlayerMutation(nextIndex);
            Session.WinCheck();

            var nextPhase = initialRoundsRemaining ? EnumGamePhases.FirstRoundsBuilding : EnumGamePhases.BeforeRoll;

            return ResultFinishTurn.Ok(player.ID, initialRoundsRemaining, nextPhase);
        }
    }
}