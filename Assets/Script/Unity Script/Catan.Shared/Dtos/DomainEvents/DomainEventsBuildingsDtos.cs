using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class VillagePlacedEventPrivateDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public Dictionary<EnumResourceType, int> Resources;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public VillagePlacedEventPrivateDto(
            int vertexId,
            int ownerId,
            int points,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank,
            int resourcesCount,
            Dictionary<string, int> buildingsLeft)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            Resources = resources;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft;
        }
    }

    public sealed class VillagePlacedEventPublicDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public VillagePlacedEventPublicDto(
            int vertexId,
            int ownerId,
            int points,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank,
            Dictionary<string, int> buildingsLeft)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft;
        }
    }

    public sealed class RoadPlacedEventPrivateDto : IDomainEventDto
    {
        public int EdgeId;
        public int OwnerId;
        public Dictionary<EnumResourceType, int> Resources;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public RoadPlacedEventPrivateDto(
            int edgeId,
            int ownerId,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank,
            int resourcesCount,
            Dictionary<string, int> buildingsLeft)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
            Resources = resources;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft; 
        }
    }

    public sealed class RoadPlacedEventPublicDto : IDomainEventDto
    {
        public int EdgeId;
        public int OwnerId;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public RoadPlacedEventPublicDto(
            int edgeId,
            int ownerId,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank,
            Dictionary<string, int> buildingsLeft)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft;
        }
    }

    public sealed class TownPlacedEventPrivateDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public Dictionary<EnumResourceType, int> Resources;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public TownPlacedEventPrivateDto(
            int vertexId,
            int ownerId,
            int points,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank,
            int resourcesCount,
            Dictionary<string, int> buildingsLeft)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            Resources = resources;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft;
        }
    }

    public sealed class TownPlacedEventPublicDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<string, int> BuildingsLeft;

        public TownPlacedEventPublicDto(
            int vertexId,
            int ownerId,
            int points,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank,
            Dictionary<string, int> buildingsLeft)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            ResourcesCount = resourcesCount;
            Bank = bank;
            BuildingsLeft = buildingsLeft;
        }
    }
}