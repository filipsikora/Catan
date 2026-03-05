using Catan.Core.Interfaces;
using Catan.Shared.Data;

namespace Catan.Core.DomainEvents
{
    public sealed class BankTradeRatioChangedMessage : IDomainEvent
    {
        public int Ratio { get; }
        public bool PossibleForPlayer { get; }
        public EnumResourceTypes? Resource { get; }

        public BankTradeRatioChangedMessage(int ratio, bool possibleForPlayer, EnumResourceTypes? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}