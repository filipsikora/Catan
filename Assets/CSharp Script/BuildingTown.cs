using System;
using System.Collections.Generic;

namespace Catan
{
    public class BuildingTown : Building, IBuildingData
    {
        public Vertex Vertex { get; set; }

        public BuildingTown(Player owner, float x, float y, Vertex vertex)
            : base(x, y, owner)
        {
            Vertex = vertex;
        }
    }
}