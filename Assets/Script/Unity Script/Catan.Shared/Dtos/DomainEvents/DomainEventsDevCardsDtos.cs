using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class DevCardUsedEventPrivateDto : IDomainEventDto
    {
        public int PlayerId;
        public int CardId;
        public EnumDevelopmentCardTypes CardType;
        public int DevCardNumber;
        public List<DevelopmentCardDto> DevCards;

        public DevCardUsedEventPrivateDto(
            int playerId,
            int cardId,
            EnumDevelopmentCardTypes cardType,
            int devCardNumber,
            List<DevelopmentCardDto> devCards)
        {
            PlayerId = playerId;
            CardId = cardId;
            CardType = cardType;
            DevCardNumber = devCardNumber;
            DevCards = devCards;
        }
    }

    public sealed class DevCardUsedEventPublicDto : IDomainEventDto
    {
        public int PlayerId;
        public int DevCardNumber;

        public DevCardUsedEventPublicDto(
            int playerId,
            int devCardNumber)
        {
            PlayerId = playerId;
            DevCardNumber = devCardNumber;
        }
    }

    public sealed class CardsStolenEventThiefDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int Quantity;
        public int ThiefId;
        public int VictimId;
        public Dictionary<EnumResourceType, int> ThiefResources;
        public int VictimResourcesCount;
        public int ThiefResourcesCount;

        public CardsStolenEventThiefDto(
            EnumResourceType resource,
            int quantity,
            int thiefId,
            int victimId,
            Dictionary<EnumResourceType, int> thiefResources,
            int victimResourcesCount,
            int thiefResourcesCount)
        {
            Resource = resource;
            Quantity = quantity;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResources = thiefResources;
            VictimResourcesCount = victimResourcesCount;
            ThiefResourcesCount = thiefResourcesCount;
        }
    }

    public sealed class CardsStolenEventVictimDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int Quantity;
        public int ThiefId;
        public int VictimId;
        public int ThiefResourcesCount;
        public Dictionary<EnumResourceType, int> VictimResources;
        public int VictimResourcesCount;

        public CardsStolenEventVictimDto(
            EnumResourceType resource,
            int quantity,
            int thiefId,
            int victimId,
            int thiefResourcesCount,
            Dictionary<EnumResourceType, int> victimResources,
            int victimResourcesCount)
        {
            Resource = resource;
            Quantity = quantity;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResourcesCount = thiefResourcesCount;
            VictimResources = victimResources;
            VictimResourcesCount = victimResourcesCount;
        }
    }

    public sealed class CardsStolenEventPublicDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int Quantity;
        public int ThiefId;
        public int VictimId;
        public int ThiefResourcesCount;
        public int VictimResourcesCount;

        public CardsStolenEventPublicDto(
            EnumResourceType resource,
            int quantity,
            int thiefId,
            int victimId,
            int thiefResourcesCount,
            int victimResourcesCount)
        {
            Resource = resource;
            Quantity = quantity;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResourcesCount = thiefResourcesCount;
            VictimResourcesCount = victimResourcesCount;
        }
    }

    public sealed class DevCardBoughtEventPrivateDto : IDomainEventDto
    {
        public int PlayerId;
        public int CardId;
        public EnumDevelopmentCardTypes DevCardType;
        public int DevCardNumber;
        public Dictionary<EnumResourceType, int> Resources;
        public bool IsPlayable;
        public int ResourceCardsCount;

        public DevCardBoughtEventPrivateDto(
            int playerId,
            int cardId,
            EnumDevelopmentCardTypes devCardType,
            int devCardNumber,
            Dictionary<EnumResourceType, int> resources,
            bool isPlayable,
            int resourceCardsCount)
        {
            PlayerId = playerId;
            CardId = cardId;
            DevCardType = devCardType;
            DevCardNumber = devCardNumber;
            Resources = resources;
            IsPlayable = isPlayable;
            ResourceCardsCount = resourceCardsCount;
        }
    }

    public sealed class DevCardBoughtEventPublicDto : IDomainEventDto
    {
        public int PlayerId;
        public int DevCardNumber;
        public int ResourcesNumber;

        public DevCardBoughtEventPublicDto(
            int playerId,
            int devCardNumber,
            int resourcesNumber)
        {
            PlayerId = playerId;
            DevCardNumber = devCardNumber;
            ResourcesNumber = resourcesNumber;
        }
    }

    public sealed class VictoryCardUsedEventDto : IDomainEventDto
    {
        public int PlayerId;
        public int ExtraPoints;
        public int VictoryCardsUsed;

        public VictoryCardUsedEventDto(
            int playerId,
            int extraPoints,
            int victoryCardsUsed)
        {
            PlayerId = playerId;
            ExtraPoints = extraPoints;
            VictoryCardsUsed = victoryCardsUsed;
        }
    }

    public sealed class KnightCardUsedEventDto : IDomainEventDto
    {
        public int PlayerId;
        public int KnightCardsUsed;

        public KnightCardUsedEventDto(
            int playerId,
            int knightCardsUsed)
        {
            PlayerId = playerId;
            KnightCardsUsed = knightCardsUsed;
        }
    }

    public sealed class DevCardPlayabilityChangedEventPrivateDto : IDomainEventDto
    {
        public IEnumerable<int> DevCardsPlayable;

        public DevCardPlayabilityChangedEventPrivateDto(
            IEnumerable<int> devCardsPlayable)
        {
            DevCardsPlayable = devCardsPlayable;
        }
    }

    public sealed class DevCardPlayabilityChangedEventPublicDto : IDomainEventDto
    {
        public DevCardPlayabilityChangedEventPublicDto()
        {
        }
    }
}