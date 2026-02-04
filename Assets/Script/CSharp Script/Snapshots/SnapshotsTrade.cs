using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Snapshots
{
    public sealed class TradeOfferedSnapshot
    {
        public int SellerId { get; }
        public int BuyerId { get; }
        public string SellerName { get; }
        public string BuyerName { get; }
        public Dictionary<EnumResourceTypes, int> Offered { get; }
        public Dictionary<EnumResourceTypes, int> Desired { get; }
        public bool CanTrade { get; }

        public TradeOfferedSnapshot(int sellerId, int buyerId, string sellerName, string buyerName, Dictionary<EnumResourceTypes, int> offered, Dictionary<EnumResourceTypes, int> desired, bool canTrade)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerName = sellerName;
            BuyerName = buyerName;
            Offered = offered;
            Desired = desired;
            CanTrade = canTrade;
        }
    }
}
