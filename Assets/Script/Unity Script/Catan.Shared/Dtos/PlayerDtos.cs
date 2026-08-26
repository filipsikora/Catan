using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
    public sealed class PlayerResourcesDto
    {
        public Dictionary<EnumResourceType, int> PlayerResources { get; set; }
    }

    public sealed class FullPlayerDto
    {
        public FullPlayerDataDto Data { get; set; }
        public PlayerResourcesDto Resources { get; set; }
    }

    public sealed class FullPlayerDataDto
    {
        public string Name { get; set; }
        public int PlayerId { get; set; }

        public Dictionary<string, int> BuildingsLeft { get; set; }

        public int Points { get; set; }
        public int Knights { get; set; }
        public int VictoryPoints { get; set; }
        public int ExtraPoints { get; set; }

        public List<DevelopmentCardDto> DevCards { get; set; }
        public int DevCardsNumber { get; set; }
        public int ResourceCardsNumber { get; set; }

        public int VictoryCardsPlayed { get; set; }
        public int KnightCardsPlayed { get; set; }
    }

    public sealed class BasicPlayerDto
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
    }

    public sealed class OtherPlayersDto
    {
        public List<BasicPlayerDto> OtherPlayers { get; set; }
    }
}
