namespace Catan.Shared.Communication.Events
{
    public class VillagePlacedEvent
    {
        public int VertexId;
        public VillagePlacedEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class RoadPlacedEvent
    {
        public int EdgeId;
        public RoadPlacedEvent(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public class TownPlacedEvent
    {
        public int VertexId;
        public TownPlacedEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public class BuildOptionsSentEvent
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentEvent(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }
}