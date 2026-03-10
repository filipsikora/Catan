using Catan.Core.Snapshots;
using Catan.Core.Queries.Interfaces;
using System.Collections.Generic;
using Catan.Shared.Data;

namespace Catan.Core.Queries.InMemory
{
    public sealed class InMemoryResourcesQueryService : IResourcesQueryService
    {
        private readonly GameSession _session;

        public InMemoryResourcesQueryService(GameSession session)
        {
            _session = session;
        }

        public ResourcesAvailabilitySnapshot GetResourcesAvailability()
        {
            var resourcesAvailability = new Dictionary<EnumResourceTypes, bool>();

            foreach (var (type, amount) in _session.GetBank().ResourceDictionary)
            {
                bool available = amount > 0;

                resourcesAvailability.Add(type, available);
            }

            return new ResourcesAvailabilitySnapshot(resourcesAvailability);
        }
    }
}