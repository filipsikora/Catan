using Catan.Application.Snapshots;

namespace Catan.Application.Queries.Resources
{
    public interface IResourcesQueryService
    {
        ResourcesAvailabilitySnapshot GetResourcesAvailability();
    }
}