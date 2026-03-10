#nullable enable
using System;
using Catan.Shared.Data;
using Catan.Core.Results;
using Catan.Core.Interfaces;
using Catan.Core.Models;

namespace Catan.Core.Conditions
{
    public static class ConditionsBuildings
    {
        public static ResultCondition NoSettlementsInRange(Vertex vertex)
        {
            foreach (var position in vertex.NeighbourVertices)
            {
                if (position.IsOwned)
                {
                    return ResultCondition.Fail(ConditionFailureReason.TooCloseToSettlement);
                }
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition HasVillage(Player player, Vertex vertex)
        {
            if (vertex.Owner == player && vertex.HasVillage)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NotOwner);
        }

        public static ResultCondition HasAvailable<T>(Player player)
            where T : Building, IBuildingData
        {
            int max = BuildingDataRegistry.MaxPerPlayer[typeof(T)];

            if (player.BuildingCount<T>() < max)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NoBuildingsAvailable);
        }

        public static ResultCondition AdjacentToLastVillage(Edge edge, Vertex vertex)
        {
            if (edge.VertexA == vertex || edge.VertexB == vertex)
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.NotNextToLastVillage);
        }
    }
}
 