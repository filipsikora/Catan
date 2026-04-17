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
}