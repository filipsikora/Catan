using Catan.Core.Interfaces;

namespace Catan.Core.Models
{
    public class BuildingRoad : Building, IBuildingData
    {
        public Edge Edge { get; set; }

        public static readonly ResourceCostOrStock Cost = new ResourceCostOrStock(0, 1, 0, 0, 1);

        public BuildingRoad(Player owner, float x, float y, Edge edge)
            : base(x, y, owner)
        {
            Edge = edge;
        }
    }
}