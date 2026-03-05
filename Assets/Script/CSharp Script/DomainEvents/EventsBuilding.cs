using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class VillagePlacedEvent : IDomainEvent
    {
        public int VertexId;
        public VillagePlacedEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }

    public sealed class RoadPlacedEvent : IDomainEvent
    {
        public int EdgeId;
        public RoadPlacedEvent(int edgeId)
        {
            EdgeId = edgeId;
        }
    }

    public sealed class TownPlacedEvent : IDomainEvent
    {
        public int VertexId;
        public TownPlacedEvent(int vertexId)
        {
            VertexId = vertexId;
        }
    }
}
