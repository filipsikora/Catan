using Catan.Core.Interfaces;

namespace Catan.Core.Models
{
    public class BuildingTown : Building, IBuildingData
    {
        public Vertex Vertex { get; set; }

        public int Worth = 2;

        public BuildingTown(Player owner, float x, float y, Vertex vertex)
            : base(x, y, owner)
        {
            Vertex = vertex;
        }
    }
}