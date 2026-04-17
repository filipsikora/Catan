using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class SelectionChangedUIEvent : IInternalUIEvents
    {
        public bool ActionAvailable;
        public SelectionChangedUIEvent(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }

    public sealed class ResourceSelectedUIEvent : IInternalUIEvents
    {
        public EnumResourceType? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedUIEvent(bool selected, EnumResourceType? type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public sealed class DesiredCardsChangedUIEvent : IInternalUIEvents
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedUIEvent(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }
}
