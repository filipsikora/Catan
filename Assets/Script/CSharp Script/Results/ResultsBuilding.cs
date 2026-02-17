using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultBuildInitialVillage : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int VertexId { get; }
        public bool SecondVillage { get; }

        private ResultBuildInitialVillage(bool success, ConditionFailureReason reason, int playerId, int vertexId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            VertexId = vertexId;
        }

        public static ResultBuildInitialVillage Ok(int playerId, int vertexId, EnumGamePhases? nextPhase)
        {
            return new ResultBuildInitialVillage(true, ConditionFailureReason.None, playerId, vertexId, nextPhase);
        }

        public static ResultBuildInitialVillage Fail(ConditionFailureReason reason, int playerId, int vertexId)
        {
            return new ResultBuildInitialVillage(false, reason, playerId, vertexId, null);
        }
    }

    public sealed class ResultBuildInitialRoad : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int EdgeId { get; }

        private ResultBuildInitialRoad(bool success, ConditionFailureReason reason, int playerId, int edgeId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            EdgeId = edgeId;
        }

        public static ResultBuildInitialRoad Ok(int playerId, int edgeId, EnumGamePhases? nextPhase)
        {
            return new ResultBuildInitialRoad(true, ConditionFailureReason.None, playerId, edgeId, nextPhase);
        }

        public static ResultBuildInitialRoad Fail(ConditionFailureReason reason, int playerId, int edgeId)
        {
            return new ResultBuildInitialRoad(false, reason, playerId, edgeId, null);
        }
    }

    public sealed class ResultBuildVillage : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int VertexId { get; }

        private ResultBuildVillage(bool success, ConditionFailureReason reason, int playerId, int vertexId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            VertexId = vertexId;
        }

        public static ResultBuildVillage Ok(int playerId, int vertexId, EnumGamePhases? nextPhase)
        {
            return new ResultBuildVillage(true, ConditionFailureReason.None, playerId, vertexId, nextPhase);
        }

        public static ResultBuildVillage Fail(ConditionFailureReason reason, int playerId, int vertexId)
        {
            return new ResultBuildVillage(false, reason, playerId, vertexId, null);
        }
    }

    public sealed class ResultBuildRoad : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int EdgeId { get; }

        private ResultBuildRoad(bool success, ConditionFailureReason reason, int playerId, int edgeId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            EdgeId = edgeId;
        }

        public static ResultBuildRoad Ok(int playerId, int edgeId, EnumGamePhases? nextPhase)
        {
            return new ResultBuildRoad(true, ConditionFailureReason.None, playerId, edgeId, nextPhase);
        }

        public static ResultBuildRoad Fail(ConditionFailureReason reason, int playerId, int edgeId)
        {
            return new ResultBuildRoad(false, reason, playerId, edgeId, null);
        }
    }

    public sealed class ResultUpgradeVillage : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int VertexId { get; }

        private ResultUpgradeVillage(bool success, ConditionFailureReason reason, int playerId, int vertexId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            VertexId = vertexId;
        }

        public static ResultUpgradeVillage Ok(int playerId, int vertexId, EnumGamePhases? nextPhase)
        {
            return new ResultUpgradeVillage(true, ConditionFailureReason.None, playerId, vertexId, nextPhase);
        }

        public static ResultUpgradeVillage Fail(ConditionFailureReason reason, int playerId, int vertexId)
        {
            return new ResultUpgradeVillage(false, reason, playerId, vertexId, null);
        }
    }

    public sealed class ResultBuildFreeRoad : ResultBase
    {
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public int EdgeId { get; }

        private ResultBuildFreeRoad(bool success, ConditionFailureReason reason, int playerId, int edgeId, EnumGamePhases? nextPhase) : base(success, nextPhase)
        {
            Reason = reason;
            PlayerId = playerId;
            EdgeId = edgeId;
        }

        public static ResultBuildFreeRoad Ok(int playerId, int edgeId, EnumGamePhases? nextPhase)
        {
            return new ResultBuildFreeRoad(true, ConditionFailureReason.None, playerId, edgeId, nextPhase);
        }

        public static ResultBuildFreeRoad Fail(ConditionFailureReason reason, int playerId, int edgeId)
        {
            return new ResultBuildFreeRoad(false, reason, playerId, edgeId, null);
        }
    }
}