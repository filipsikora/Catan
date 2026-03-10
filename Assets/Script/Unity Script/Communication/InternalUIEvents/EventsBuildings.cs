using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public class VillagePlacedUIEvent : IInternalUIEvents
    {
        public int VertexId;
        public int OwnerId;

        public VillagePlacedUIEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
    public class RoadPlacedUIEvent : IInternalUIEvents
    {
        public int EdgeId;
        public int OwnerId;

        public RoadPlacedUIEvent(int edgeId, int ownerId)

        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public class TownPlacedUIEvent : IInternalUIEvents
    {
        public int VertexId;
        public int OwnerId;

        public TownPlacedUIEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class BuildOptionsSentUIEvent : IInternalUIEvents
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentUIEvent(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }
}
