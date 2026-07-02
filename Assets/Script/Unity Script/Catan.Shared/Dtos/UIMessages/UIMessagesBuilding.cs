using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class BuildOptionsSentDto : IUiMessageDto
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentDto(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }

    public sealed class VillagePlacedDto : IUiMessageDto
    {
        public int VertexId;
        public int OwnerId;
        public VillagePlacedDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedDto : IUiMessageDto
    {
        public int EdgeId;
        public int OwnerId;
        public RoadPlacedDto(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedDto : IUiMessageDto
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