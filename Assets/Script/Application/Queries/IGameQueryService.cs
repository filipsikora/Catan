using System;
using System.Collections.Generic;
using Catan.Application.Snapshots;

namespace Catan.Application.Queries
{
    public interface IGameQueryService
    {
        IReadOnlyList<DevelopmentCardSnapshot>
            GetCurrentPlayerDevelopmentCards();
    }
}
