using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class BankTradeRatioChangedUIEvent : IInternalUIEvents
    {
        public int Ratio { get; }
        public bool PossibleForPlayer { get; }
        public EnumResourceTypes? Resource { get; }

        public BankTradeRatioChangedUIEvent(int ratio, bool possibleForPlayer, EnumResourceTypes? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}
