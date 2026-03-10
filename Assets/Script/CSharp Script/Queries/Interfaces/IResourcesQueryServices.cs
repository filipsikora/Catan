using Catan.Core.Snapshots;

namespace Catan.Core.Queries.Interfaces
{
    public interface IResourcesQueryService
    {
        ResourcesAvailabilitySnapshot GetResourcesAvailability();
    }
}