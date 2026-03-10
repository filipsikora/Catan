using Catan.Core.Models;
using System.Collections.Generic;

namespace Catan.Shared.Data
{
    public static class DevelopmentCardDataRegistry
    {
        public static readonly Dictionary<EnumDevelopmentCardTypes, int> TotalCount = new()
        {
            { EnumDevelopmentCardTypes.Knight, 14 },
            { EnumDevelopmentCardTypes.VictoryPoint, 5 },
            { EnumDevelopmentCardTypes.Monopoly, 2 },
            { EnumDevelopmentCardTypes.RoadBuilding, 2 },
            { EnumDevelopmentCardTypes.YearOfPlenty, 2 }
        };

        public static readonly ResourceCostOrStock Cost = new(1,0, 1, 1, 0);
    }
}