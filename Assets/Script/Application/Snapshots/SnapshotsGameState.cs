namespace Catan.Application.Snapshots
{
    public sealed class TurnDataSnapshot
    {
        public int PlayerId { get; }
        public string PlayerName { get; }
        public int TurnNumber { get; }
        public bool InitialRoundsRemaining { get; }

        public TurnDataSnapshot(int playerdId, string playerName, int turnNumber, bool initialRoundsRemaining)
        {
            PlayerId = playerdId;
            PlayerName = playerName;
            TurnNumber = turnNumber;
            InitialRoundsRemaining = initialRoundsRemaining;
        }
    }
}