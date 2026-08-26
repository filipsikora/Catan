using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class CardsDiscardedEventPrivateDto : IDomainEventDto
    {
        public int PlayerId;
        public Dictionary<EnumResourceType, int> Resources;
        public Dictionary<EnumResourceType, int> PlayerResources;
        public Dictionary<EnumResourceType, int> Bank;
        public int PlayerResourcesCount;

        public CardsDiscardedEventPrivateDto(
            int playerId,
            Dictionary<EnumResourceType, int> resources,
            Dictionary<EnumResourceType, int> playerResources,
            Dictionary<EnumResourceType, int> bank,
            int playerResourcesCount)
        {
            PlayerId = playerId;
            Resources = resources;
            PlayerResources = playerResources;
            Bank = bank;
            PlayerResourcesCount = playerResourcesCount;
        }
    }

    public sealed class CardsDiscardedPublicEventDto : IDomainEventDto
    {
        public int PlayerId;
        public Dictionary<EnumResourceType, int> Resources;
        public int ResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;

        public CardsDiscardedPublicEventDto(
            int playerId,
            Dictionary<EnumResourceType, int> resources,
            int resourcesCount,
            Dictionary<EnumResourceType, int> bank)
        {
            PlayerId = playerId;
            Resources = resources;
            ResourcesCount = resourcesCount;
            Bank = bank;
        }
    }

    public sealed class CardStolenEventThiefDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int ThiefId;
        public int VictimId;
        public Dictionary<EnumResourceType, int> ThiefResources;
        public int VictimResourcesCount;

        public CardStolenEventThiefDto(
            EnumResourceType resource,
            int thiefId,
            int victimId,
            Dictionary<EnumResourceType, int> thiefResources,
            int victimResourcesCount)
        {
            Resource = resource;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResources = thiefResources;
            VictimResourcesCount = victimResourcesCount;
        }
    }

    public sealed class CardStolenEventVictimDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int ThiefId;
        public int VictimId;
        public int ThiefResourcesCount;
        public Dictionary<EnumResourceType, int> VictimResources;

        public CardStolenEventVictimDto(
            EnumResourceType resource,
            int thiefId,
            int victimId,
            int thiefResourcesCount,
            Dictionary<EnumResourceType, int> victimResources)
        {
            Resource = resource;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResourcesCount = thiefResourcesCount;
            VictimResources = victimResources;
        }
    }

    public sealed class CardStolenEventPublicDto : IDomainEventDto
    {
        public EnumResourceType Resource;
        public int ThiefId;
        public int VictimId;
        public int ThiefResourcesCount;
        public int VictimResourcesCount;

        public CardStolenEventPublicDto(
            EnumResourceType resource,
            int thiefId,
            int victimId,
            int thiefResourcesCount,
            int victimResourcesCount)
        {
            Resource = resource;
            ThiefId = thiefId;
            VictimId = victimId;
            ThiefResourcesCount = thiefResourcesCount;
            VictimResourcesCount = victimResourcesCount;
        }
    }

    public sealed class RobberPlacedEventDto : IDomainEventDto
    {
        public int HexId;
        public bool CanSteal;

        public RobberPlacedEventDto(
            int hexId,
            bool canSteal)
        {
            HexId = hexId;
            CanSteal = canSteal;
        }
    }
}