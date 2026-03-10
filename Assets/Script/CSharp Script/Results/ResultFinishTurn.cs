using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultFinishTurn : ResultBase
    {
        public int NewCurrentPlayerId { get; }
        public bool InitialRoundsRemaining { get; }

        private ResultFinishTurn(int newCurrentPlayerId, bool initialRoundsRemaining, EnumGamePhases nextPhase) : base(true, nextPhase)
        {
            NewCurrentPlayerId = newCurrentPlayerId;
            InitialRoundsRemaining = initialRoundsRemaining;
        }

        public static ResultFinishTurn Ok(int newCurrentPlayerId, bool initialRoundsRemaining, EnumGamePhases nextPhase)
        {
            return new ResultFinishTurn(newCurrentPlayerId, initialRoundsRemaining, nextPhase);
        }
    }
}
