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
}