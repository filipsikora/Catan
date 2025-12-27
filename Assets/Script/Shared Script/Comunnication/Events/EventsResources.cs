using System.Collections.Generic;
using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public class ResourcesAvailabilityEvent
    {
        public Dictionary<EnumResourceTypes, bool> ResourcesAvailability { get; }
        public ResourcesAvailabilityEvent(Dictionary<EnumResourceTypes, bool> resourcesAvailability)
        {
            ResourcesAvailability = resourcesAvailability;
        }
    }

    public class ResourceSelectedEvent
    {
        public EnumResourceTypes? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedEvent(bool selected, EnumResourceTypes type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public class SelectionChangedEvent
    {
        public bool ActionAvailable;
        public SelectionChangedEvent(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }
}