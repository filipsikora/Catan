using Catan.Shared.Data;
using Catan.Unity.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public class ResourceCardClickedUIEvent : IInternalUIEvents
    {
        public int VisualResourceCardId { get; }
        public EnumResourceType Type { get; }
        public EnumResourceCardLocation Location { get; }
        public bool IsLeftClicked { get; }
        public bool IsToggled { get; }

        public ResourceCardClickedUIEvent(int visualResourceCardId, EnumResourceType type, EnumResourceCardLocation location, bool isLeftClicked, bool isToggled)
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

    public class ResourceCardVisualStateChangedUIEvent : IInternalUIEvents
    {
        public int VisualResourceCardId;
        public EnumResourceCardLocation Location;
        public EnumResourceCardVisualState State { get; }

        public ResourceCardVisualStateChangedUIEvent(int visualResourceCardId, EnumResourceCardLocation location, EnumResourceCardVisualState state)
        {
            VisualResourceCardId = visualResourceCardId;
            Location = location;
            State = state;
        }
    }

    public class ResourceCardTypeVisualStateChangedUIEvent : IInternalUIEvents
    {
        public EnumResourceType? Type { get; }
        public EnumResourceCardVisualState State { get; }

        public ResourceCardTypeVisualStateChangedUIEvent(
            EnumResourceType? type,
            EnumResourceCardVisualState state)
        {
            Type = type;
            State = state;
        }
    }

    public class MultipleResourceCardVisualStateResetUIEvent : IInternalUIEvents
    {
        public EnumResourceCardLocation Location;

        public MultipleResourceCardVisualStateResetUIEvent(EnumResourceCardLocation location)
        {
            Location = location;
        }
    }

    public class ResourceCardToggledUIEvent : IInternalUIEvents
    {
        public int VisualResourceCardId { get; }

        public ResourceCardToggledUIEvent(int visualResourceCardId)
        {
            VisualResourceCardId = visualResourceCardId;
        }
    }
}