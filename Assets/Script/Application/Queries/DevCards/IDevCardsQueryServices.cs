using System.Collections.Generic;
using Catan.Application.Snapshots;

namespace Catan.Application.Queries.DevCards
{
    public interface IDevCardsQueryService
    {
        IReadOnlyList<DevelopmentCardSnapshot> GetCurrentPlayerDevCards();
    }
}