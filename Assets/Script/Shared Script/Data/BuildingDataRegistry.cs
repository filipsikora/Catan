using System;
using System.Collections.Generic;
using Catan.Core.Models;

namespace Catan.Shared.Data
{
    public static class BuildingDataRegistry
    {
        public static readonly Dictionary<Type, ResourceCostOrStock> Cost = new()
        {
            { typeof(BuildingVillage), new ResourceCostOrStock(Wheat:1, Wood:1, Clay:1, Wool:1) },
            { typeof(BuildingTown), new ResourceCostOrStock(Wheat:2, Stone:3) },
            { typeof(BuildingRoad), new ResourceCostOrStock(Wood:1, Clay:1) }
        };

        public static readonly Dictionary<Type, int> MaxPerPlayer = new()
        {
            { typeof(BuildingVillage), 5 },
            { typeof(BuildingTown), 4 },
            { typeof(BuildingRoad), 15 }
        };

        public static readonly Dictionary<Type, string> Name = new()
        {
            { typeof(BuildingVillage), "Village" },
            { typeof(BuildingTown), "Town" },
            { typeof(BuildingRoad), "Road" }
        };

        public static readonly Dictionary<Type, int> Worth = new()
        {
            { typeof(BuildingVillage), 1 },
            { typeof(BuildingTown), 2 },
            { typeof(BuildingRoad), 0 } 
        };
    }
}