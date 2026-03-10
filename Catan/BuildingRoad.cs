using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class BuildingRoad(Player owner, float x, float y, Edge edge) : Building(x, y, owner), IBuildingData
    {
        public static ResourceCostOrStock Cost { get; } = new ResourceCostOrStock(Wood: 1, Clay: 1);

        public static int MaxPerPlayer { get; } = 15;

        public static string Name { get; } = "Road";

        public Edge Edge { get; set; } = edge;

        public int Worth { get; } = 0;

    }
}
