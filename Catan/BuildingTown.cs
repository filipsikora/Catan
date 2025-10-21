using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class BuildingTown(Player owner, float x, float y, Vertex vertex) : Building(x, y, owner), IBuildingData
    {
        public static ResourceCostOrStock Cost { get; } = new ResourceCostOrStock(Wheat: 2, Stone: 3);

        public static int MaxPerPlayer { get; } = 4;

        public static string Name { get; } = "Town";

        public Vertex Vertex { get; set; } = vertex;

        public int Worth { get; } = 2;
    }
}
