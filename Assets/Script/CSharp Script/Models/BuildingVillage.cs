using Catan.Core.Interfaces;

namespace Catan.Core.Models
{
    public class BuildingVillage : Building, IBuildingData
    {
        public Vertex Vertex { get; set; }

        public int Worth = 1;

        public static readonly ResourceCostOrStock Cost = new ResourceCostOrStock(1, 1, 1, 0, 1);

        public BuildingVillage(Player owner, float x, float y, Vertex vertex)
            : base(x, y, owner)
        {
            Vertex = vertex;
        }

    }
}