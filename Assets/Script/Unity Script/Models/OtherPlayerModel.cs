namespace Catan.Unity.Models
{
    public class OtherPlayerModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ResourceCardsNumber { get; set; }
        public int DevCardsNumber { get; set; }
        public int VictoryCardsPlayed { get; set; }
        public int KnightCardsPlayed { get; set; }

        public OtherPlayerModel(int id, string name, int resourceCardsNumber, int devCardsNumber, int victoryCardsPlayed, int knightCardsPlayed)
        {
            Id = id;
            Name = name;
            ResourceCardsNumber = resourceCardsNumber;
            DevCardsNumber = devCardsNumber;
            VictoryCardsPlayed = victoryCardsPlayed;
            KnightCardsPlayed = knightCardsPlayed;
        }
    }
}
