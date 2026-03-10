using Catan.Core.Models;

namespace Catan.Core.Engine
{
    public sealed class PlayerTradeContext
    {
        public int SellerId { get; }
        public int BuyerId { get; }
        public string SellerName { get; }
        public string BuyerName { get; }
        public ResourceCostOrStock Offered { get; }
        public ResourceCostOrStock Desired { get; }

        public PlayerTradeContext(int sellerId, int buyerId, string sellerName, string buyerName, ResourceCostOrStock offered, ResourceCostOrStock desired)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerName = sellerName;
            BuyerName = buyerName;
            Offered = offered;
            Desired = desired;
        }
    }
}