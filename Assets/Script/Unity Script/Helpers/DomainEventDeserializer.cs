using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Interfaces;
using System;

namespace Unity.Helpers
{
    public static class DomainEventDeserializer
    {
        public static IDomainEventDto Deserialize(GameUpdateDto update)
        {
            if (!Enum.TryParse<EnumDomainEventsDto>(update.DtoType, out var type))
                throw new Exception($"Unknown domain event: {update.DtoType}");

            switch (type)
            {
                case EnumDomainEventsDto.VillagePlacedEventPrivateDto:
                    return Deserialize<VillagePlacedEventPrivateDto>(update);

                case EnumDomainEventsDto.VillagePlacedEventPublicDto:
                    return Deserialize<VillagePlacedEventPublicDto>(update);

                case EnumDomainEventsDto.RoadPlacedEventPrivateDto:
                    return Deserialize<RoadPlacedEventPrivateDto>(update);

                case EnumDomainEventsDto.RoadPlacedEventPublicDto:
                    return Deserialize<RoadPlacedEventPublicDto>(update);

                case EnumDomainEventsDto.TownPlacedEventPrivateDto:
                    return Deserialize<TownPlacedEventPrivateDto>(update);

                case EnumDomainEventsDto.TownPlacedEventPublicDto:
                    return Deserialize<TownPlacedEventPublicDto>(update);

                case EnumDomainEventsDto.DevCardUsedEventPrivateDto:
                    return Deserialize<DevCardUsedEventPrivateDto>(update);

                case EnumDomainEventsDto.DevCardUsedEventPublicDto:
                    return Deserialize<DevCardUsedEventPublicDto>(update);

                case EnumDomainEventsDto.DevCardBoughtEventPrivateDto:
                    return Deserialize<DevCardBoughtEventPrivateDto>(update);

                case EnumDomainEventsDto.DevCardBoughtEventPublicDto:
                    return Deserialize<DevCardBoughtEventPublicDto>(update);

                case EnumDomainEventsDto.VictoryCardUsedEventDto:
                    return Deserialize<VictoryCardUsedEventDto>(update);

                case EnumDomainEventsDto.KnightCardUsedEventDto:
                    return Deserialize<KnightCardUsedEventDto>(update);

                case EnumDomainEventsDto.CardsStolenEventThiefDto:
                    return Deserialize<CardsStolenEventThiefDto>(update);

                case EnumDomainEventsDto.CardsStolenEventVictimDto:
                    return Deserialize<CardsStolenEventVictimDto>(update);

                case EnumDomainEventsDto.CardsStolenEventPublicDto:
                    return Deserialize<CardsStolenEventPublicDto>(update);

                case EnumDomainEventsDto.CardStolenEventThiefDto:
                    return Deserialize<CardStolenEventThiefDto>(update);

                case EnumDomainEventsDto.CardStolenEventVictimDto:
                    return Deserialize<CardStolenEventVictimDto>(update);

                case EnumDomainEventsDto.CardStolenEventPublicDto:
                    return Deserialize<CardStolenEventPublicDto>(update);

                case EnumDomainEventsDto.CardsDiscardedEventPrivateDto:
                    return Deserialize<CardsDiscardedEventPrivateDto>(update);

                case EnumDomainEventsDto.CardsDiscardedPublicEventDto:
                    return Deserialize<CardsDiscardedPublicEventDto>(update);

                case EnumDomainEventsDto.PlayerResourcesReceivedEventPrivateDto:
                    return Deserialize<PlayerResourcesReceivedEventPrivateDto>(update);

                case EnumDomainEventsDto.PlayerResourcesReceivedEventPublicDto:
                    return Deserialize<PlayerResourcesReceivedEventPublicDto>(update);

                case EnumDomainEventsDto.RoadChampionChangedEventDto:
                    return Deserialize<RoadChampionChangedEventDto>(update);

                case EnumDomainEventsDto.KnightChampionChangedEventDto:
                    return Deserialize<KnightChampionChangedEventDto>(update);

                case EnumDomainEventsDto.RolledNumberChangedEventDto:
                    return Deserialize<RolledNumberChangedEventDto>(update);

                case EnumDomainEventsDto.PhaseChangedEventDto:
                    return Deserialize<PhaseChangedEventDto>(update);

                case EnumDomainEventsDto.PlayersToMoveChangedEventDto:
                    return Deserialize<PlayersToMoveChangedEventDto>(update);

                case EnumDomainEventsDto.GameWonEventDto:
                    return Deserialize<GameWonEventDto>(update);

                case EnumDomainEventsDto.BankTradeDoneEventPrivateDto:
                    return Deserialize<BankTradeDoneEventPrivateDto>(update);

                case EnumDomainEventsDto.BankTradeDoneEventPublicDto:
                    return Deserialize<BankTradeDoneEventPublicDto>(update);

                case EnumDomainEventsDto.TradeDoneEventSellerDto:
                    return Deserialize<TradeDoneEventSellerDto>(update);

                case EnumDomainEventsDto.TradeDoneEventBuyerDto:
                    return Deserialize<TradeDoneEventBuyerDto>(update);

                case EnumDomainEventsDto.TradeDoneEventPublicDto:
                    return Deserialize<TradeDoneEventPublicDto>(update);

                case EnumDomainEventsDto.RobberPlacedEventDto:
                    return Deserialize<RobberPlacedEventDto>(update);

                case EnumDomainEventsDto.DevCardPlayabilityChangedEventPrivateDto:
                    return Deserialize<DevCardPlayabilityChangedEventPrivateDto>(update);

                default:
                    throw new Exception($"Unknown domain event: {update.DtoType}");
            }
        }

        private static T Deserialize<T>(GameUpdateDto update)
            where T : IDomainEventDto
        {
            return update.Payload.ToObject<T>()
                ?? throw new Exception($"Failed to deserialize {typeof(T).Name}");
        }
    }
}