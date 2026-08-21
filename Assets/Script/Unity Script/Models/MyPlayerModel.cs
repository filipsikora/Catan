using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Models
{
    public class MyPlayerModel
    {
        public string Name { get; set; }
        public int PlayerId { get; set; }

        public Dictionary<string, int> BuildingsLeft { get; set; }

        public int Points { get; set; }
        public int Knights { get; set; }
        public int VictoryPoints { get; set; }
        public int ExtraPoints { get; set; }

        public List<DevCardModel> DevCards { get; set; }
        public Dictionary<EnumResourceType, int> Resources { get; set; }
        public int DevCardNumber { get; set; }

        public MyPlayerModel(int playerId, string name, Dictionary<string, int> buildingsLeft, int points, int knights, int victoryPoints, int extraPoints, IReadOnlyList<DevCardModel> devCards, Dictionary<EnumResourceType, int> resources)
        {
            PlayerId = playerId;
            Name = name;
            BuildingsLeft = buildingsLeft;
            Points = points;
            Knights = knights;
            VictoryPoints = victoryPoints;
            ExtraPoints = extraPoints;
            DevCards = devCards.ToList();
            Resources = resources;
            DevCardNumber = 0;
        }
    }
}
