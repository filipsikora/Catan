using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class VillagePlacedDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public VillagePlacedDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedDto : IDomainEventDto
    {
        public int EdgeId;
        public int OwnerId;
        public RoadPlacedDto(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public TownPlacedDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}
