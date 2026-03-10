using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultBankTrade
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int PlayerId;
        public EnumResourceTypes Offered;
        public EnumResourceTypes Desired;
        public int Ratio;

        public ResultBankTrade(bool success, ConditionFailureReason reason, int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio)
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
            return new ResultBankTrade(false, reason, playerId, default, default, 0);
        }

        public static ResultBankTrade Ok(int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio)
        {
            return new ResultBankTrade(true, ConditionFailureReason.None, playerId, offered, desired, ratio);
        }
    }

    public sealed class ResultPlayerTrade
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int BuyerId { get; }
        public int SellerId { get; }
        public ResourceCostOrStock Offered { get; }
        public ResourceCostOrStock Desired { get; }

        public ResultPlayerTrade(bool success, ConditionFailureReason reason, int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            Success = success;
            Reason = reason;
            SellerId = sellerId;
            BuyerId = buyerId;
            Offered = offered;
            Desired = desired;
        }

        public static ResultPlayerTrade Fail(ConditionFailureReason reason, int sellerId, int buyerId)
        {
            return new ResultPlayerTrade(false, reason, sellerId, buyerId, default, default);
        }

        public static ResultPlayerTrade Ok(int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            return new ResultPlayerTrade(true, ConditionFailureReason.None, sellerId, buyerId, offered, desired);
        }
    }
}