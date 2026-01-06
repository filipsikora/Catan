using Catan.Application.Snapshots;
using Catan.Core.Engine;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Application.Queries.Resources
{
    public sealed class InMemoryResourcesQueryService : IResourcesQueryService
    {
        private readonly GameState _game;

        public InMemoryResourcesQueryService(GameState game)
        {
            _game = game;
        }

        public ResourcesAvailabilitySnapshot GetResourcesAvailability()
        {
            var resourcesAvailability = new Dictionary<EnumResourceTypes, bool>();

            foreach (var (type, amount) in _game.Bank.ResourceDictionary)
            {
                bool available = amount > 0;

                resourcesAvailability.Add(type, available);
            }

            return new ResourcesAvailabilitySnapshot(resourcesAvailability);
        }
    }
}