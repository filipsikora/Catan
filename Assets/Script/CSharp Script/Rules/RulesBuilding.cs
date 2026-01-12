using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;

namespace Catan.Core.Rules
{
    public static class RulesBuilding
    {
        public static ResultCondition CanBuildFreeVillage(Player player, Vertex vertex, GameState game)
        {
            return ResultCondition.Combine(
                ConditionsBuildings.PositionExists(vertex.Id, id => game.Map.GetVertexById(id)),
                ConditionsBuildings.HasAvailable<BuildingVillage>(player),
                ConditionsBuildings.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex));
        }

        public static ResultCondition CanBuildFreeRoad(Player player, Edge edge, Vertex vertex, GameState game)
        {
            return ResultCondition.Combine(
                ConditionsBuildings.PositionExists(edge.Id, id => game.Map.GetEdgeById(id)),
                ConditionsBuildings.HasAvailable<BuildingRoad>(player),
                ConditionsBuildings.IsNotOwned(edge),
                ConditionsBuildings.HasAccessToPosition(player, edge),
                ConditionsBuildings.AdjacentToLastVillage(edge, vertex));
        }

        public static ResultCondition CanBuildVillage(Player player, Vertex vertex, GameState game)
        {
            return ResultCondition.Combine(
                ConditionsBuildings.PositionExists(vertex.Id, id => game.Map.GetVertexById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingVillage.Cost),
                ConditionsBuildings.HasAvailable<BuildingVillage>(player),
                ConditionsBuildings.IsNotOwned(vertex),
                ConditionsBuildings.NoSettlementsInRange(vertex),
                ConditionsBuildings.HasAccessToPosition(player, vertex));
        }

        public static ResultCondition CanBuildRoad(Player player, Edge edge, GameState game)
        {
            return ResultCondition.Combine(
                ConditionsBuildings.PositionExists(edge.Id, id => game.Map.GetEdgeById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingRoad.Cost),
                ConditionsBuildings.HasAvailable<BuildingRoad>(player),
                ConditionsBuildings.IsNotOwned(edge),
                ConditionsBuildings.HasAccessToPosition(player, edge));
        }

        public static ResultCondition CanUpgradeVillage(Player player, Vertex vertex, GameState game)
        {
            return ResultCondition.Combine(
                ConditionsBuildings.PositionExists(vertex.Id, id => game.Map.GetVertexById(id)),
                ConditionsResources.CanAfford(player.Resources, BuildingTown.Cost),
                ConditionsBuildings.HasAvailable<BuildingTown>(player),
                ConditionsBuildings.HasVillage(player, vertex));
        }
    }
}