using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class BuildOptionsSentDto : IUiMessageDto
    {
        public bool CanBuildVillage { get; set; }
        public bool CanBuildRoad { get; set; }
        public bool CanUpgradeVillage { get; set; }

        public BuildOptionsSentDto(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }

    public sealed class VillagePlacedDto : IUiMessageDto
    {
        public int VertexId { get; set; }
        public int OwnerId { get; set; }
        public VillagePlacedDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }

    public sealed class RoadPlacedDto : IUiMessageDto
    {
        public int EdgeId { get; set; }
        public int OwnerId { get; set; }
        public RoadPlacedDto(int edgeId, int ownerId)
        {
            EdgeId = edgeId;
            OwnerId = ownerId;
        }
    }

    public sealed class TownPlacedDto : IUiMessageDto
    {
        public int VertexId { get; set; }
        public int OwnerId { get; set; }
        public TownPlacedDto(int vertexId, int ownerId)
        {
            VertexId = vertexId;
            OwnerId = ownerId;
        }
    }
}