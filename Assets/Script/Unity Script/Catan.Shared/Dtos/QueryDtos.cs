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

    public sealed class ResourcesAvailabilityDto
    {
        public Dictionary<EnumResourceType, bool> ResourcesAvailability;
    }

    public sealed class DevelopmentCardDto
    {
        public int Id { get; set; }
        public EnumDevelopmentCardTypes Type { get; set; }
        public bool IsNew { get; set; }
        public bool IsPlayable { get; set; }
    }

    public sealed class PlayerNameDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public sealed class TradeOfferedDto
    {
        public int SellerId { get; set; }
        public int BuyerId { get; set; }
        public string SellerName { get; set; }
        public string BuyerName { get; set; }
        public Dictionary<EnumResourceType, int> Offered;
        public Dictionary<EnumResourceType, int> Desired;
        public bool CanTrade { get; set; }
    }
}
