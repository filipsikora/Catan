using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class PlayerResourcesReceivedEventPrivateDto : IDomainEventDto
    {
        public int PlayerId;
        public Dictionary<EnumResourceType, int> ResourcesChange;
        public Dictionary<EnumResourceType, int> PlayerResources;
        public Dictionary<EnumResourceType, int> Bank;
        public int PlayerResourcesCount;

        public PlayerResourcesReceivedEventPrivateDto(
            int playerId,
            Dictionary<EnumResourceType, int> resourcesChange,
            Dictionary<EnumResourceType, int> playerResources,
            Dictionary<EnumResourceType, int> bank,
            int playerResourcesCount)
        {
            PlayerId = playerId;
            ResourcesChange = resourcesChange;
            PlayerResources = playerResources;
            Bank = bank;
            PlayerResourcesCount = playerResourcesCount;
        }
    }

    public sealed class PlayerResourcesReceivedEventPublicDto : IDomainEventDto
    {
        public int PlayerId;
        public Dictionary<EnumResourceType, int> ResourcesChange;
        public int PlayerResourcesCount;
        public Dictionary<EnumResourceType, int> Bank;

        public PlayerResourcesReceivedEventPublicDto(
            int playerId,
            Dictionary<EnumResourceType, int> resourcesChange,
            int playerResourcesCount,
            Dictionary<EnumResourceType, int> bank)
        {
            PlayerId = playerId;
            ResourcesChange = resourcesChange;
            PlayerResourcesCount = playerResourcesCount;
            Bank = bank;
        }
    }

    public sealed class RoadChampionChangedEventDto : IDomainEventDto
    {
        public int? OldChampionId;
        public int? NewChampionId;
        public int? OldChampionExtraPoints;
        public int? NewChampionExtraPoints;
        public int? OldChampionPoints;
        public int? NewChampionPoints;

        public RoadChampionChangedEventDto(
            int? oldChampionId,
            int? newChampionId,
            int? oldChampionExtraPoints,
            int? newChampionExtraPoints,
            int? oldChampionPoints,
            int? newChampionPoints)
        {
            OldChampionId = oldChampionId;
            NewChampionId = newChampionId;
            OldChampionExtraPoints = oldChampionExtraPoints;
            NewChampionExtraPoints = newChampionExtraPoints;
            OldChampionPoints = oldChampionPoints;
            NewChampionPoints = newChampionPoints;
        }
    }

    public sealed class KnightChampionChangedEventDto : IDomainEventDto
    {
        public int? OldChampionId;
        public int? NewChampionId;
        public int? OldChampionExtraPoints;
        public int? NewChampionExtraPoints;
        public int? OldChampionPoints;
        public int? NewChampionPoints;

        public KnightChampionChangedEventDto(
            int? oldChampionId,
            int? newChampionId,
            int? oldChampionExtraPoints,
            int? newChampionExtraPoints,
            int? oldChampionPoints,
            int? newChampionPoints)
        {
            OldChampionId = oldChampionId;
            NewChampionId = newChampionId;
            OldChampionExtraPoints = oldChampionExtraPoints;
            NewChampionExtraPoints = newChampionExtraPoints;
            OldChampionPoints = oldChampionPoints;
            NewChampionPoints = newChampionPoints;
        }
    }

    public sealed class ResourcesDistributionDonePrivateEventDto : IDomainEventDto
    {
        public Dictionary<EnumResourceType, int> PlayerResources { get; }
        public int PlayerResourcesCount { get; }
        public Dictionary<int, int> PlayersIdsToResourcesCount { get; }
        public Dictionary<int, Dictionary<EnumResourceType, int>> PlayersIdstoResourceChange { get; }
        public Dictionary<EnumResourceType, int> Bank { get; }

        public ResourcesDistributionDonePrivateEventDto(
            Dictionary<EnumResourceType, int> playerResources,
            int playerResourcesCount,
            Dictionary<int, int> otherPlayersIdsToResourcesCount,
            Dictionary<int, Dictionary<EnumResourceType, int>> playersIdstoResourceChange,
            Dictionary<EnumResourceType, int> bank)
        {
            PlayerResources = playerResources;
            PlayerResourcesCount = playerResourcesCount;
            PlayersIdsToResourcesCount = otherPlayersIdsToResourcesCount;
            PlayersIdstoResourceChange = playersIdstoResourceChange;
            Bank = bank;
        }
    }
}