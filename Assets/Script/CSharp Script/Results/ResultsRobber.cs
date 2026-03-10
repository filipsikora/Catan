#nullable enable

using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Results
{
    public class ResultStealResource : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int ThiefId { get; }
        public int VictimId { get; }
        public EnumResourceTypes Resource { get; }

        private ResultStealResource(bool success, ConditionFailureReason reason, int thiefId, int victimId, EnumResourceTypes resource, EnumGamePhases? nextPhase) :
            base(success, nextPhase)
        {
            Reason = reason;
            ThiefId = thiefId;
            VictimId = victimId;
            Resource = resource;
        }

        public static ResultStealResource Ok(int thiefId, int victimId, EnumResourceTypes resource, EnumGamePhases nextPhase)
        {
            return new ResultStealResource(true, ConditionFailureReason.None, thiefId, victimId, resource, nextPhase);
        }

        public static ResultStealResource Fail(int thiefId, int victimId, ConditionFailureReason reason)
        {
            return new ResultStealResource(false, reason, thiefId, victimId, default, null);
        }
    }

    public class ResultBlockHex : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int? HexId { get; }
        public bool CanSteal { get; }
        public List<int>? PotentialVictimsIds { get; }

        private ResultBlockHex(bool success, ConditionFailureReason reason, int? hexId, bool canSteal, List<int>? potentialVictimsIds, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            HexId = hexId;
            CanSteal = canSteal;
            PotentialVictimsIds = potentialVictimsIds;
        }

        public static ResultBlockHex Ok(int hexId, bool canSteal, List<int> potentialVictimsIds, EnumGamePhases? nextPhase)
        {
            return new ResultBlockHex(true, ConditionFailureReason.None, hexId, canSteal, potentialVictimsIds, nextPhase);
        }

        public static ResultBlockHex Fail(ConditionFailureReason reason)
        {
            return new ResultBlockHex(false, reason, null, false, null, null);
        }
    }
}
