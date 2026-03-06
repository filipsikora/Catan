using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class BankTradeRatioChangedMessage : IUIMessages
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