using Catan.Catan;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Catan
{
    public class Player
    {
        public string? Name { get; set; } = null;

        public int Points { get; set; } = 0;

        public int VillagesLeft { get; set; } = 5;

        public int TownsLeft { get; set; } = 4;

        public int RoadsLeft { get; set; } = 15;

        public Dictionary<EnumFieldTypes, int> Resources { get; set; } = new Dictionary<EnumFieldTypes, int>()
        {
            { EnumFieldTypes.Wheat, 0 },
            { EnumFieldTypes.Wood, 0 },
            { EnumFieldTypes.Wool, 0 },
            { EnumFieldTypes.Stone, 0 },
            { EnumFieldTypes.Clay, 0 }
        };


        public Player(string? name)
        {
            Name = name;
        }


        public bool CanAfford(Building building)
        {
            var resourceValues = Enum.GetValues(typeof(EnumResourceTypes)).ToL

            for (int i = 0; i < resourceValues.Count; i++)
            {
                if (resourceValues[i] < building.Cost[i])
                    return false;
            }
            return true;
        } ZAMIENIC BUILDING COST NA SLOWNIK I TUTAJ POROWNAC PO KISACH
            pozmywac naczynia

        public int CountPoints()
        {
            int townPoints = (4 - TownsLeft) * 2;
            int villagesPoints = (5 - VillagesLeft) * 1;

            return (villagesPoints + townPoints);
        }
   }
}
