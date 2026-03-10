using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public sealed class BankTradeRatioChangedEvent
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

    public sealed class BankTradeExecutedEvent
    {
        public ResultBankTrade Result;
        public BankTradeExecutedEvent(ResultBankTrade result)
        {
            Result = result;
        }
    }
}