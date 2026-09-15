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
        public EnumDevelopmentCardTypes CardType;

        public DevCardUsedEventPublicDto(
            int playerId,
            int devCardNumber,
            EnumDevelopmentCardTypes cardType)
        {
            PlayerId = playerId;
            DevCardNumber = devCardNumber;
            CardType = cardType;
        }
    }

    public sealed class CardsStolenEventDto : IDomainEventDto
    {
        public EnumResourceType Resource { get; }
        public int ThiefId { get; }
        public Dictionary<int, int> VictimIdsToAmounts { get; }
        public Dictionary<EnumResourceType, int> MyResources { get; }
        public int MyResourcesCount { get; }
        public Dictionary<int, int> PlayersResourcesCount { get; }

        public CardsStolenEventDto(
            EnumResourceType resource,
            int thiefId,
            Dictionary<int, int> victimIdsToAmounts,
            Dictionary<EnumResourceType, int> myResources,
            int myResourcesCount,
            Dictionary<int, int> playersResourcesCount)
        {
            Resource = resource;
            ThiefId = thiefId;
            VictimIdsToAmounts = victimIdsToAmounts;
            MyResources = myResources;
            MyResourcesCount = myResourcesCount;
            PlayersResourcesCount = playersResourcesCount;
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
}