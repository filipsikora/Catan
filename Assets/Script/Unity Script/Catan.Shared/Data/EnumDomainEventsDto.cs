namespace Catan.Shared.Data
{
    public enum EnumDomainEventsDto
    {
        VillagePlacedEventPrivateDto,
        VillagePlacedEventPublicDto,

        RoadPlacedEventPrivateDto,
        RoadPlacedEventPublicDto,

        TownPlacedEventPrivateDto,
        TownPlacedEventPublicDto,

        DevCardUsedEventPrivateDto,
        DevCardUsedEventPublicDto,

        DevCardBoughtEventPrivateDto,
        DevCardBoughtEventPublicDto,

        VictoryCardUsedEventDto,
        KnightCardUsedEventDto,

        CardsStolenEventThiefDto,
        CardsStolenEventVictimDto,
        CardsStolenEventPublicDto,

        CardStolenEventThiefDto,
        CardStolenEventVictimDto,
        CardStolenEventPublicDto,

        CardsDiscardedEventPrivateDto,
        CardsDiscardedPublicEventDto,

        PlayerResourcesReceivedEventPrivateDto,
        PlayerResourcesReceivedEventPublicDto,

        RoadChampionChangedEventDto,
        KnightChampionChangedEventDto,

        RolledNumberChangedEventDto,
        PhaseChangedEventDto,
        PlayersToMoveChangedEventDto,
        GameWonEventDto,

        BankTradeDoneEventPrivateDto,
        BankTradeDoneEventPublicDto,

        TradeDoneEventSellerDto,
        TradeDoneEventBuyerDto,
        TradeDoneEventPublicDto,

        RobberPlacedEventDto,

        DevCardPlayabilityChangedEventPrivateDto,
        DevCardPlayabilityChangedEventPublicDto
    }
}