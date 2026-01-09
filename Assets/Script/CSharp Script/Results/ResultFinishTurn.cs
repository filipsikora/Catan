namespace Catan.Core.Results
{
    public sealed class ResultFinishTurn
    {
        public int NewCurrentPlayerId { get; }
        public bool InitialRoundsRemaining { get; }

        public ResultFinishTurn(int newCurrentPlayerId, bool initialRoundsRemaining)
        {
            NewCurrentPlayerId = newCurrentPlayerId;
            InitialRoundsRemaining = initialRoundsRemaining;
        }
    }
}
