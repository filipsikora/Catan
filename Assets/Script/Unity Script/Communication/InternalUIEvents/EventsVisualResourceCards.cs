using Catan.Shared.Data;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public class ResourceCardClickedUIEvent
    {
        public int VisualResourceCardId { get; }
        public EnumResourceTypes Type { get; }
        public EnumResourceCardLocation Location { get; }
        public bool IsLeftClicked { get; }
        public ResourceCardClickedUIEvent(int visualResourceCardId, EnumResourceTypes type, EnumResourceCardLocation location, bool isLeftClicked)
        {
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            Location = location;
            IsLeftClicked = isLeftClicked;
        }
    }
}