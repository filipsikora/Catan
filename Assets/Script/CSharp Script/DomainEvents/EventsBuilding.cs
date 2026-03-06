using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class VillagePlacedEvent : IDomainEvent
    {
        public int VertexId;
        public int OwnerId;
        public VillagePlacedEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedEvent : IDomainEvent
    {
        public int EdgeId;
        public int OwnerId;
        public RoadPlacedEvent(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedEvent : IDomainEvent
    {
        public int VertexId;
        public int OwnerId;
        public TownPlacedEvent(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}
