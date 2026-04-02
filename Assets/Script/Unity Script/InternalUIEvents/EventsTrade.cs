using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class BankTradeRatioChangedUIEvent : IInternalUIEvents
    {
        public int Ratio { get; }
        public bool PossibleForPlayer { get; }
        public EnumResourceType? Resource { get; }

        public BankTradeRatioChangedUIEvent(int ratio, bool possibleForPlayer, EnumResourceType? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}
