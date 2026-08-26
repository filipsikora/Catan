using System.Collections.Generic;

namespace Catan.Unity.Models
{
    public class OtherPlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Points { get; set; }
        public int ExtraPoints { get; set; }
        public int ResourceCardsNumber { get; set; }
        public int DevCardsNumber { get; set; }
        public int VictoryCardsPlayed { get; set; }
        public int KnightCardsPlayed { get; set; }

        public Dictionary<string, int> BuildingsLeft { get; set; }


        public OtherPlayerModel(int id, string name, int points, int extraPoints, int resourceCardsNumber, int devCardsNumber, int victoryCardsPlayed, int knightCardsPlayed, Dictionary<string, int> buildingsLeft)
        {
            Id = id;
            Name = name;
            Points = points;
            ExtraPoints = extraPoints;
            ResourceCardsNumber = resourceCardsNumber;
            DevCardsNumber = devCardsNumber;
            VictoryCardsPlayed = victoryCardsPlayed;
            KnightCardsPlayed = knightCardsPlayed;
            BuildingsLeft = buildingsLeft;
        }
    }
}