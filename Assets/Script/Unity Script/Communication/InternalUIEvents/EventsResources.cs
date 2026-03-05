using Catan.Shared.Data;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
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
        public EnumResourceTypes? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedUIEvent(bool selected, EnumResourceTypes type)
        {
            Type = type;
            Selected = selected;
        }
    }
}
