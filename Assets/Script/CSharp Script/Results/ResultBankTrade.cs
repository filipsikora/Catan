using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultBankTrade
    {
        public bool Success { get; }
        public ConditionFailureReason? Reason { get; }
        public int PlayerId;
        public EnumResourceTypes? Offered;
        public EnumResourceTypes? Desired;
        public int Ratio;

        public ResultBankTrade(bool success, ConditionFailureReason? reason, int playerId, EnumResourceTypes? offered, EnumResourceTypes? desired, int ratio)
        {
            Success = success;
            Reason = reason;

            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
        }

        public static ResultBankTrade Fail(int playerId, ConditionFailureReason reason)
        {
            return new ResultBankTrade(false, reason, playerId, null, null, 0);
        }

        public static ResultBankTrade Ok(int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio)
        {
            return new ResultBankTrade(true, null, playerId, offered, desired, ratio);
        }
    }
}