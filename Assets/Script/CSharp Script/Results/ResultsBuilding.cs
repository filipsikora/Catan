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
}