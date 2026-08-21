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
        public int VillagesLeft;
        public Dictionary<EnumResourceType, int> Resources;
        public Dictionary<EnumResourceType, int> Bank;

        public VillagePlacedEventPrivateDto(
            int vertexId,
            int ownerId,
            int points,
            int villagesLeft,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            VillagesLeft = villagesLeft;
            Resources = resources;
            Bank = bank;
        }
    }

    public sealed class VillagePlacedEventPublicDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public int VillagesLeft;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;

        public VillagePlacedEventPublicDto(
            int vertexId,
            int ownerId,
            int points,
            int villagesLeft,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            VillagesLeft = villagesLeft;
            ResourcesCount = resourcesCount;
            Bank = bank;
        }
    }

    public sealed class RoadPlacedEventPrivateDto : IDomainEventDto
    {
        public int EdgeId;
        public int OwnerId;
        public int RoadsLeft;
        public Dictionary<EnumResourceType, int> Resources;
        public Dictionary<EnumResourceType, int> Bank;

        public RoadPlacedEventPrivateDto(
            int edgeId,
            int ownerId,
            int roadsLeft,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
            RoadsLeft = roadsLeft;
            Resources = resources;
            Bank = bank;
        }
    }

    public sealed class RoadPlacedEventPublicDto : IDomainEventDto
    {
        public int EdgeId;
        public int OwnerId;
        public int RoadsLeft;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;

        public RoadPlacedEventPublicDto(
            int edgeId,
            int ownerId,
            int roadsLeft,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
            RoadsLeft = roadsLeft;
            ResourcesCount = resourcesCount;
            Bank = bank;
        }
    }

    public sealed class TownPlacedEventPrivateDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public int TownsLeft;
        public int VillagesLeft;
        public Dictionary<EnumResourceType, int> Resources;
        public Dictionary<EnumResourceType, int> Bank;

        public TownPlacedEventPrivateDto(
            int vertexId,
            int ownerId,
            int points,
            int townsLeft,
            int villagesLeft,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> bank)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            TownsLeft = townsLeft;
            VillagesLeft = villagesLeft;
            Resources = resources;
            Bank = bank;
        }
    }

    public sealed class TownPlacedEventPublicDto : IDomainEventDto
    {
        public int VertexId;
        public int OwnerId;
        public int Points;
        public int TownsLeft;
        public int VillagesLeft;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;

        public TownPlacedEventPublicDto(
            int vertexId,
            int ownerId,
            int points,
            int townsLeft,
            int villagesLeft,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
            Points = points;
            TownsLeft = townsLeft;
            VillagesLeft = villagesLeft;
            ResourcesCount = resourcesCount;
            Bank = bank;
        }
    }
}