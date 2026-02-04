using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots
{
    public sealed class ResourcesAvailabilitySnapshot
    {
        public Dictionary<EnumResourceTypes, bool> ResourcesAvailability;
        public ResourcesAvailabilitySnapshot(Dictionary<EnumResourceTypes, bool> resourcesAvailability)
        {
            ResourcesAvailability = resourcesAvailability;
        }
    }
}
