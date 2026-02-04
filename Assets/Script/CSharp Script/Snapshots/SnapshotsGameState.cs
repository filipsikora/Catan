namespace Catan.Core.Snapshots
{
    public sealed class TurnDataSnapshot
    {
        public int PlayerId { get; }
        public string PlayerName { get; }
        public int TurnNumber { get; }
        public int RolledNumber { get; }
        public bool InitialRoundsRemaining { get; }

        public TurnDataSnapshot(int playerdId, string playerName, int turnNumber, int rolledNumber, bool initialRoundsRemaining)
        {
            PlayerId = playerdId;
            PlayerName = playerName;
            TurnNumber = turnNumber;
            RolledNumber = rolledNumber;
            InitialRoundsRemaining = initialRoundsRemaining;
        }
    }
}