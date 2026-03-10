using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class ResourceCardSelectedCommand : ICommand
    {
        public EnumResourceTypes Type;
        public bool IsSelected { get; }
        public ResourceCardSelectedCommand(bool isSelected, EnumResourceTypes type)
        {
            IsSelected = isSelected;
            Type = type;
        }
    }
}