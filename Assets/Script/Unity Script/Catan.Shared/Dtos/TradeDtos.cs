using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
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
