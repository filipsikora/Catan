using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class BuildOptionsSentMessage : IUIMessages
    {
        public bool CanBuildVillage { get; }
        public bool CanBuildRoad { get; }
        public bool CanUpgradeVillage { get; }

        public BuildOptionsSentMessage(bool canVillage, bool canRoad, bool canTown)
        {
            CanBuildVillage = canVillage;
            CanBuildRoad = canRoad;
            CanUpgradeVillage = canTown;
        }
    }
}