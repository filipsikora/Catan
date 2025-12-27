using Catan.Shared.Data;

namespace Catan.Shared.Results
{
    public class ResultCardsStolen
    {
        public int PlayerId { get; }
        public int VictimId { get; }
        public EnumResourceTypes Type { get; }
        public int Amount { get; }
        public ResultCardsStolen(int playerId, int victimId, EnumResourceTypes type, int amount)
        {
            PlayerId = playerId;
            VictimId = victimId;
            Type = type;
            Amount = amount;
        }
    }
}