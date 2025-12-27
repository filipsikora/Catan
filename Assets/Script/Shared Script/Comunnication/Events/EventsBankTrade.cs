using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Communication.Events
{
    public class BankTradeRatioChangedEvent
    {
        public int Ratio { get; }
        public bool PossibleForPlayer { get; }
        public EnumResourceTypes? Resource { get; }

        public BankTradeRatioChangedEvent(int ratio, bool possibleForPlayer, EnumResourceTypes? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}