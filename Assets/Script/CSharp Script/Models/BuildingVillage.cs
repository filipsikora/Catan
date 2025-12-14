#nullable enable
using System;
using System.Collections.Generic;

namespace Catan
{
    public class BuildingVillage : Building, IBuildingData
    {
        public Vertex Vertex { get; set; }

        public int Worth = 1;

        public BuildingVillage(Player owner, float x, float y, Vertex vertex)
            : base(x, y, owner)
        {
            Vertex = vertex;
        }

    }
}