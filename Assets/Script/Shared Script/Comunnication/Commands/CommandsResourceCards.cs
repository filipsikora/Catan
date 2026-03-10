using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class ResourceCardClickedCommand : ICommand 
    {
        public int VisualResourceCardId;
        public EnumResourceTypes Type;
        public EnumResourceCardLocation Location;
        public bool IsLeftClicked { get; }

        public ResourceCardClickedCommand(int visualResourceCardId, EnumResourceTypes type, EnumResourceCardLocation location, bool isLeftClicked)
        {
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            Location = location;
            IsLeftClicked = isLeftClicked;
        }
    }

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