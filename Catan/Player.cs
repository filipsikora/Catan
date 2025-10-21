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

        public List<Building> Buildings = new();

        public ResourceCostOrStock Resources { get; set; } = new ResourceCostOrStock();


        public Player(string? name)
        {
            Name = name;
            Resources = new ResourceCostOrStock() { Name = name };
        }


        public int BuildingCount<T>()
            where T : Building
        {
            return Buildings.Count(b => b is T);
        }

        public bool HasAvailable<T>()
            where T : Building, IBuildingData
        {
            return BuildingCount<T>() < T.MaxPerPlayer;
        }

        public int CountPoints()
        {
            int villagesPoints = BuildingCount<BuildingVillage>();
            int townPoints = BuildingCount<BuildingTown>() * 2;
            int points = villagesPoints + townPoints;
            Points = points;

            return points;
        }
   }
}
