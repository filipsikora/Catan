using System;
using System.Collections.Generic;

namespace Catan
{
    public class BuildingRoad : Building, IBuildingData
    {
        public Edge Edge { get; set; }

        public BuildingRoad(Player owner, float x, float y, Edge edge)
            : base(x, y, owner)
        {
            Edge = edge;
        }
    }
}