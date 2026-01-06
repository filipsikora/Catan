using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultResourceDistribution
    {
        public int PlayerId { get; }
        public EnumResourceTypes Type { get; }
        public int Requested { get; }
        public int Granted { get; }
        public ResultResourceDistribution(int playerId, EnumResourceTypes type, int requested, int granted)
        {
            PlayerId = playerId;
            Type = type;
            Requested = requested;
            Granted = granted;
        }
    }
}