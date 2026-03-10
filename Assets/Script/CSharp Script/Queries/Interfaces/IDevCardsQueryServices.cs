using Catan.Core.Snapshots;
using System.Collections.Generic;

namespace Catan.Core.Queries.Interfaces
{
    public interface IDevCardsQueryService
    {
        IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards();
    }
}