using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultBankTrade : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public EnumResourceTypes Offered { get; }
        public EnumResourceTypes Desired { get; }
        public int Ratio { get; }

        private ResultBankTrade(bool success, ConditionFailureReason reason, int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio, EnumGamePhases? nextPhase) : 
            base(success, nextPhase)
        {
            Reason = reason;

            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
        }

        public static ResultBankTrade Fail(int playerId, ConditionFailureReason reason)
        {
            return new ResultBankTrade(false, reason, playerId, default, default, 0, null);
        }

        public static ResultBankTrade Ok(int playerId, EnumResourceTypes offered, EnumResourceTypes desired, int ratio, EnumGamePhases nextPhase)
        {
            return new ResultBankTrade(true, ConditionFailureReason.None, playerId, offered, desired, ratio, nextPhase);
        }
    }

    public sealed class ResultPlayerTrade : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int BuyerId { get; }
        public int SellerId { get; }
        public ResourceCostOrStock Offered { get; }
        public ResourceCostOrStock Desired { get; }

        private ResultPlayerTrade(bool success, ConditionFailureReason reason, int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired, EnumGamePhases? nextPhase) : 
            base(success, nextPhase)
        {
            Reason = reason;
            SellerId = sellerId;
            BuyerId = buyerId;
            Offered = offered;
            Desired = desired;
        }

        public static ResultPlayerTrade Fail(ConditionFailureReason reason, int sellerId, int buyerId)
        {
            return new ResultPlayerTrade(false, reason, sellerId, buyerId, default, default, null);
        }

        public static ResultPlayerTrade Ok(int sellerId, int buyerId, ResourceCostOrStock offered, ResourceCostOrStock desired, EnumGamePhases nextPhase)
        {
            return new ResultPlayerTrade(true, ConditionFailureReason.None, sellerId, buyerId, offered, desired, nextPhase);
        }
    }
}