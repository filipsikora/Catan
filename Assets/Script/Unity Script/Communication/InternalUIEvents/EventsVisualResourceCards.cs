using Catan.Shared.Data;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public class ResourceCardClickedUIEvent
    {
        public int VisualResourceCardId { get; }
        public EnumResourceTypes Type { get; }
        public EnumResourceCardLocation Location { get; }
        public bool IsLeftClicked { get; }
        public bool IsToggled { get; }

        public ResourceCardClickedUIEvent(int visualResourceCardId, EnumResourceTypes type, EnumResourceCardLocation location, bool isLeftClicked, bool isToggled)
        {
            VisualResourceCardId = visualResourceCardId;
            Type = type;
            Location = location;
            IsLeftClicked = isLeftClicked;
            IsToggled = isToggled;
        }
    }

    public class ResourceCardDestroyedUIEvent
    {
        public int VisualResourceCardId { get; }

        public ResourceCardDestroyedUIEvent(int visualResourceCardId)
        {
            VisualResourceCardId = visualResourceCardId;
        }
    }
}