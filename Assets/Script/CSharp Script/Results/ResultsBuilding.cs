using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Core.Results
{
    public sealed class ResultBuildFreeVillage
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public Vertex Vertex { get; }
        public bool SecondVillage { get; }

        public ResultBuildFreeVillage(bool success, ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            Success = success;
            Reason = reason;
            PlayerId = playerId;
            Vertex = vertex;
        }

        public static ResultBuildFreeVillage Ok(int playerId, Vertex vertex)
        {
            return new ResultBuildFreeVillage(true, ConditionFailureReason.None, playerId, vertex);
        }

        public static ResultBuildFreeVillage Fail(ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            return new ResultBuildFreeVillage(false, reason, playerId, vertex);
        }
    }

    public sealed class ResultBuildFreeRoad
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public Edge Edge { get; }

        public ResultBuildFreeRoad(bool success, ConditionFailureReason reason, int playerId, Edge edge)
        {
            Success = success;
            Reason = reason;
            PlayerId = playerId;
            Edge = edge;
        }

        public static ResultBuildFreeRoad Ok(int playerId, Edge edge)
        {
            return new ResultBuildFreeRoad(true, ConditionFailureReason.None, playerId, edge);
        }

        public static ResultBuildFreeRoad Fail(ConditionFailureReason reason, int playerId, Edge edge)
        {
            return new ResultBuildFreeRoad(false, reason, playerId, edge);
        }
    }

    public sealed class ResultBuildVillage
    {
        public bool Success { get;}
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public Vertex Vertex { get; }

        public ResultBuildVillage(bool success, ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            Success = success;
            Reason = reason;
            PlayerId = playerId;
            Vertex = vertex;
        }

        public static ResultBuildVillage Ok(int playerId, Vertex vertex)
        {
            return new ResultBuildVillage(true, ConditionFailureReason.None, playerId, vertex);
        }

        public static ResultBuildVillage Fail(ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            return new ResultBuildVillage(false, reason, playerId, vertex);
        }
    }

    public sealed class ResultBuildRoad
    {
        public bool Success { get; }
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public Edge Edge { get; }

        public ResultBuildRoad(bool success, ConditionFailureReason reason, int playerId, Edge edge)
        {
            Success = success;
            Reason = reason;
            PlayerId = playerId;
            Edge = edge;
        }

        public static ResultBuildRoad Ok(int playerId, Edge edge)
        {
            return new ResultBuildRoad(true, ConditionFailureReason.None, playerId, edge);
        }

        public static ResultBuildRoad Fail(ConditionFailureReason reason, int playerId, Edge edge)
        {
            return new ResultBuildRoad(false, reason, playerId, edge);
        }
    }

    public sealed class ResultUpgradeVillage
    {
        public bool Success { get;}
        public ConditionFailureReason Reason { get; }

        public int PlayerId { get; }
        public Vertex Vertex { get; }

        public ResultUpgradeVillage(bool success, ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            Success = success;
            Reason = reason;
            PlayerId = playerId;
            Vertex = vertex;
        }

        public static ResultUpgradeVillage Ok(int playerId, Vertex vertex)
        {
            return new ResultUpgradeVillage(true, ConditionFailureReason.None, playerId, vertex);
        }

        public static ResultUpgradeVillage Fail(ConditionFailureReason reason, int playerId, Vertex vertex)
        {
            return new ResultUpgradeVillage(false, reason, playerId, vertex);
        }
    }
}