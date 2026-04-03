using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
    public sealed class PlayerDataDto
    {
        public string Name { get; set; }

        public Dictionary<string, int> BuildingsLeft { get; set; }

        public int Points { get; set; }
        public int Knights { get; set; }
        public int VictoryPoints { get; set; }
        public int ExtraPoints { get; set; }
    }

    public sealed class PlayerCardsDto
    {
        public Dictionary<EnumResourceType, int> PlayerResources;
    }
}
