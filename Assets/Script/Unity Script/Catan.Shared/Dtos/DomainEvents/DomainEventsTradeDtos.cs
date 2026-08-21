using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class BankTradeDoneEventPrivateDto : IDomainEventDto
    {
        public int PlayerId;
        public EnumResourceType Offered;
        public EnumResourceType Desired;
        public int Ratio;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<EnumResourceType, int> PlayerResources;

        public BankTradeDoneEventPrivateDto(
            int playerId,
            EnumResourceType offered,
            EnumResourceType desired,
            int ratio,
            Dictionary<EnumResourceType, int> bank,
            Dictionary<EnumResourceType, int> playerResources)
        {
            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
            Bank = bank;
            PlayerResources = playerResources;
        }
    }

    public sealed class BankTradeDoneEventPublicDto : IDomainEventDto
    {
        public int PlayerId;
        public EnumResourceType Offered;
        public EnumResourceType Desired;
        public int Ratio;
        public Dictionary<EnumResourceType, int> Bank;
        public int PlayerResourcesCount;

        public BankTradeDoneEventPublicDto(
            int playerId,
            EnumResourceType offered,
            EnumResourceType desired,
            int ratio,
            Dictionary<EnumResourceType, int> bank,
            int playerResourcesCount)
        {
            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
            Bank = bank;
            PlayerResourcesCount = playerResourcesCount;
        }
    }

    public sealed class BankTradeDoneEventDto : IDomainEventDto
    {
        public int PlayerId;
        public EnumResourceType Offered;
        public EnumResourceType Desired;
        public int Ratio;
        public Dictionary<EnumResourceType, int> Bank;
        public Dictionary<EnumResourceType, int> PlayerResources;

        public BankTradeDoneEventDto(
            int playerId,
            EnumResourceType offered,
            EnumResourceType desired,
            int ratio,
            Dictionary<EnumResourceType, int> bank,
            Dictionary<EnumResourceType, int> playerResources)
        {
            PlayerId = playerId;
            Offered = offered;
            Desired = desired;
            Ratio = ratio;
            Bank = bank;
            PlayerResources = playerResources;
        }
    }

    public sealed class TradeDoneEventSellerDto : IDomainEventDto
    {
        public int SellerId;
        public int BuyerId;
        public Dictionary<EnumResourceType, int> SellerResources;
        public int BuyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered;
        public Dictionary<EnumResourceType, int> Desired;

        public TradeDoneEventSellerDto(
            int sellerId,
            int buyerId,
            Dictionary<EnumResourceType, int> sellerResources,
            int buyerResourcesCount,
            Dictionary<EnumResourceType, int> offered,
            Dictionary<EnumResourceType, int> desired)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerResources = sellerResources;
            BuyerResourcesCount = buyerResourcesCount;
            Offered = offered;
            Desired = desired;
        }
    }

    public sealed class TradeDoneEventBuyerDto : IDomainEventDto
    {
        public int SellerId;
        public int BuyerId;
        public int SellerResourcesCount;
        public Dictionary<EnumResourceType, int> BuyerResources;
        public Dictionary<EnumResourceType, int> Offered;
        public Dictionary<EnumResourceType, int> Desired;

        public TradeDoneEventBuyerDto(
            int sellerId,
            int buyerId,
            int sellerResourcesCount,
            Dictionary<EnumResourceType, int> buyerResources,
            Dictionary<EnumResourceType, int> offered,
            Dictionary<EnumResourceType, int> desired)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerResourcesCount = sellerResourcesCount;
            BuyerResources = buyerResources;
            Offered = offered;
            Desired = desired;
        }
    }

    public sealed class TradeDoneEventPublicDto : IDomainEventDto
    {
        public int SellerId;
        public int BuyerId;
        public int SellerResourcesCount;
        public int BuyerResourcesCount;
        public Dictionary<EnumResourceType, int> Offered;
        public Dictionary<EnumResourceType, int> Desired;

        public TradeDoneEventPublicDto(
            int sellerId,
            int buyerId,
            int sellerResourcesCount,
            int buyerResourcesCount,
            Dictionary<EnumResourceType, int> offered,
            Dictionary<EnumResourceType, int> desired)
        {
            SellerId = sellerId;
            BuyerId = buyerId;
            SellerResourcesCount = sellerResourcesCount;
            BuyerResourcesCount = buyerResourcesCount;
            Offered = offered;
            Desired = desired;
        }
    }
}