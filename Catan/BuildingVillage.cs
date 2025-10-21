using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class BuildingVillage(Player owner, float x, float y, Vertex vertex) : Building(x, y, owner), IBuildingData
    {
        public static ResourceCostOrStock Cost { get; } = new ResourceCostOrStock(1, 1, 1, 1, 1);

        public static int MaxPerPlayer { get; } = 5;

        public static string Name { get; } = "Village";

        public Vertex? Vertex { get; set; } = vertex;

        public int Worth { get; } = 1;
    }
}