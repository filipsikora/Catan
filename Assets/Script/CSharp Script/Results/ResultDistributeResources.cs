using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultDistributeResources
    {
        public int PlayerId { get; }
        public string PlayerName { get; }
        public EnumResourceTypes Type { get; }
        public int Requested { get; }
        public int Granted { get; }
        public ResultDistributeResources(int playerId, string playerName, EnumResourceTypes type, int requested, int granted)
        {
            PlayerId = playerId;
            PlayerName = playerName;
            Type = type;
            Requested = requested;
            Granted = granted;
        }
    }
}