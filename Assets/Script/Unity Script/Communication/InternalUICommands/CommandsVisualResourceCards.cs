using Catan.Shared.Data;
using Catan.Unity.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUICommands
{
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
        public EnumResourceTypes? Type { get; }
        public EnumResourceCardVisualState State { get; }

        public ResourceCardTypeVisualStateChangedUIEvent(
            EnumResourceTypes? type,
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