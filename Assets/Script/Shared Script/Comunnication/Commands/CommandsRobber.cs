using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class VictimChosenCommand : ICommand
    {
        public int VictimId { get; }
        public VictimChosenCommand(int victimId)
        {
            VictimId = victimId;
        }
    }

    public class DiscardingAcceptedCommand : ICommand { }

    public class StolenCardSelectedCommand : ICommand
    {
        public EnumResourceTypes Type;
        public StolenCardSelectedCommand(EnumResourceTypes type)
        {
            Type = type;
        }
    }

    public sealed class TryGetDiscardingVictimCommand : ICommand { }
}