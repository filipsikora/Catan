using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

namespace Catan.Communication.Signals
{
    public class BuildOptionsShownSignal
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsShownSignal(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }

    public class RequestBuildVillageSignal { }

    public class RequestBuildRoadSignal { }

    public class RequestUpgradeVillageSignal { }

    public class VertexHighlightedSignal
    {
        public int VertexId;
        public VertexHighlightedSignal(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class EdgeHighlightedSignal
    {
        public int EdgeId;
        public EdgeHighlightedSignal(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class VillagePlacedSignal
    {
        public int VertexId;
        public VillagePlacedSignal(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class RoadPlacedSignal
    {
        public int EdgeId;
        public RoadPlacedSignal(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class TownPlacedSignal
    {
        public int VertexId;
        public TownPlacedSignal(int vertexId)
        {
            VertexId = vertexId;
        }
    }
}