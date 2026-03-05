using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public class ResourceCardClickedUIEvent : IInternalUIEvents
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

    public class ResourceCardDestroyedUIEvent : IInternalUIEvents
    {
        public int VisualResourceCardId { get; }

        public ResourceCardDestroyedUIEvent(int visualResourceCardId)
        {
            VisualResourceCardId = visualResourceCardId;
        }
    }
}