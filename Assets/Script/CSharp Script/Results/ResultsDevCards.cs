using Catan.Core.Models;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Results
{
    public sealed class ResultBuyDevCard : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int? DevCardId { get; }
        public EnumDevelopmentCardTypes? Type { get; }

        private ResultBuyDevCard(bool success, ConditionFailureReason reason, int playerId, int? devCardId, EnumDevelopmentCardTypes? type, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            DevCardId = devCardId;
            Type = type;
        }

        public static ResultBuyDevCard Ok(int playerId, int devCardId, EnumDevelopmentCardTypes devCardType, EnumGamePhases? nextPhase)
        {
            return new ResultBuyDevCard(true, ConditionFailureReason.None, playerId, devCardId, devCardType, nextPhase);
        }

        public static ResultBuyDevCard Fail(ConditionFailureReason reason, int playerId)
        {
            return new ResultBuyDevCard(false, reason, playerId, null, null, null);
        }
    }

    public sealed class ResultMonopolyCard : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int ThiefId { get; }
        public Dictionary<int, int> VictimsIdsAndAmounts { get; }
        public EnumResourceTypes Resource { get; }

        private ResultMonopolyCard(bool success, ConditionFailureReason reason, int thiefId, Dictionary<int, int> victimsIsdAndAmounts, EnumResourceTypes resource, EnumGamePhases? nextPhase) : 
            base(success, nextPhase)
        {
            Reason = reason;
            ThiefId = thiefId;
            VictimsIdsAndAmounts = victimsIsdAndAmounts;
            Resource = resource;
        }

        public static ResultMonopolyCard Fail(ConditionFailureReason reason, int thiefId, EnumResourceTypes resource)
        {
            return new ResultMonopolyCard(false, reason, thiefId, default, resource, null);
        }

        public static ResultMonopolyCard Ok(int thiefId, Dictionary<int, int> victimsIdsAndAmounts, EnumResourceTypes resource, EnumGamePhases nextPhase)
        {
            return new ResultMonopolyCard(true, ConditionFailureReason.None, thiefId, victimsIdsAndAmounts, resource, nextPhase);
        }
    }

    public sealed class ResultYearOfPlenty : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public ResourceCostOrStock Requested { get; }

        private ResultYearOfPlenty(bool success, ConditionFailureReason reason, ResourceCostOrStock requested, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            Requested = requested;
        }

        public static ResultYearOfPlenty Fail(ConditionFailureReason reason)
        {
            return new ResultYearOfPlenty(false, reason, default, null);
        }

        public static ResultYearOfPlenty Ok(ResourceCostOrStock requested, EnumGamePhases nextPhase)
        {
            return new ResultYearOfPlenty(true, ConditionFailureReason.None, requested, nextPhase);
        }
    }

    public sealed class ResultPlayDevCard : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int? DevCardId { get; }
        public EnumDevelopmentCardTypes? Type { get; }

        private ResultPlayDevCard(bool success, ConditionFailureReason reason, int playerId, int? devCardId, EnumDevelopmentCardTypes? type, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            DevCardId = devCardId;
            Type = type;
        }

        public static ResultPlayDevCard Ok(int playerId, int devCardId, EnumDevelopmentCardTypes devCardType, EnumGamePhases? nextPhase)
        {
            return new ResultPlayDevCard(true, ConditionFailureReason.None, playerId, devCardId, devCardType, nextPhase);
        }

        public static ResultPlayDevCard Fail(ConditionFailureReason reason, int playerId)
        {
            return new ResultPlayDevCard(false, reason, playerId, null, null, null);
        }
    }
}
