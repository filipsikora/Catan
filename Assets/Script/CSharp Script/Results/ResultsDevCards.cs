using Catan.Core.Models;
using Catan.Shared.Data;
using NUnit.Framework.Constraints;
using System.Collections.Generic;

namespace Catan.Core.Results
{
    public sealed class ResultBuyDevCard
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int DevCardId { get; }

        public ResultBuyDevCard(bool succcess, ConditionFailureReason reason, int playerId, int devCardId)
        {
            Success = succcess;
            Reason = reason;
            PlayerId = playerId;
            DevCardId = devCardId;
        }

        public static ResultBuyDevCard Ok(int playerId, int devCardId)
        {
            return new ResultBuyDevCard(true, ConditionFailureReason.None, playerId, devCardId);
        }

        public static ResultBuyDevCard Fail(ConditionFailureReason reason, int playerId, int devCardId)
        {
            return new ResultBuyDevCard(false, reason, playerId, devCardId);
        }
    }

    public sealed class ResultMonopolyCard
    {
        public bool Success;
        public ConditionFailureReason Reason;

        public int ThiefId;
        public Dictionary<int, int> VictimsIdsAndAmounts;
        public EnumResourceTypes Resource;

        public ResultMonopolyCard(bool success, ConditionFailureReason reason, int thiefId, Dictionary<int, int> victimsIsdAndAmounts, EnumResourceTypes resource)
        {
            Success = success;
            Reason = reason;
            ThiefId = thiefId;
            VictimsIdsAndAmounts = victimsIsdAndAmounts;
            Resource = resource;
        }

        public static ResultMonopolyCard Fail(ConditionFailureReason reason, int thiefId, EnumResourceTypes resource)
        {
            return new ResultMonopolyCard(false, reason, thiefId, default, resource);
        }

        public static ResultMonopolyCard Ok(int thiefId, Dictionary<int, int> victimsIdsAndAmounts, EnumResourceTypes resource)
        {
            return new ResultMonopolyCard(true, ConditionFailureReason.None, thiefId, victimsIdsAndAmounts, resource);
        }
    }

    public sealed class ResultYearOfPlenty
    {
        public bool Success;
        public ConditionFailureReason Reason;

        public ResourceCostOrStock Requested;

        public ResultYearOfPlenty(bool success, ConditionFailureReason reason, ResourceCostOrStock requested)
        {
            Success = success;
            Reason = reason;
            Requested = requested;
        }

        public static ResultYearOfPlenty Fail(ConditionFailureReason reason)
        {
            return new ResultYearOfPlenty(false, reason, default);
        }

        public static ResultYearOfPlenty Ok(ResourceCostOrStock requested)
        {
            return new ResultYearOfPlenty(true, ConditionFailureReason.None, requested);
        }
    }
}
