using Catan.Application.Interfaces;
using Catan.Shared.Data;

namespace Catan.Application.UIMessages
{
    public sealed class ResourceSelectedMessage : IUIMessages
    {
        public EnumResourceTypes? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedMessage(bool selected, EnumResourceTypes type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public sealed class SelectionChangedMessage : IUIMessages
    {
        public bool ActionAvailable;
        public SelectionChangedMessage(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }

    public sealed class DesiredCardsChangedMessage : IUIMessages
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedMessage(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }
}
