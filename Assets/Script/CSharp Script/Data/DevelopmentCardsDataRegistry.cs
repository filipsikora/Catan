using System.Collections.Generic;

namespace Catan
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
    }
}