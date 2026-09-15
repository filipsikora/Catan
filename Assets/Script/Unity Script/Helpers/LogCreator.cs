using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Interfaces;
using Catan.Unity.Caches;
using Catan.Unity.InternalUIEvents;
using System;
using System.Linq;

namespace Unity.Catan.Helpers
{
    public sealed class LogCreator
    {
        private GameCache _cache { get; }
        public LogCreator(GameCache cache)
        {
            _cache = cache;
        }

        public LogMessageUIEvent CreateLog(IDomainEventDto domainEvent)
        {
            var type = EnumLogTypes.Info;

            switch (domainEvent)
            {
                case VillagePlacedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You placed a village");

                case VillagePlacedEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.OwnerId} placed a village");

                case RoadPlacedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You placed a road");

                case RoadPlacedEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.OwnerId} placed a road");

                case TownPlacedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You placed a town");

                case TownPlacedEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.OwnerId} placed a town");

                case DevCardUsedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You used a {dto.CardType} development card");

                case DevCardUsedEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} used a {dto.CardType} development card");

                case DevCardBoughtEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You bought a {dto.DevCardType} development card");

                case DevCardBoughtEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} bought a development card");

                case CardsStolenEventDto dto:
                    {
                        if (dto.ThiefId == _cache.MyPlayer.PlayerId)
                        {
                            return new LogMessageUIEvent(type, $"You stole {dto.VictimIdsToAmounts.Count} {dto.Resource} cards from other players");
                        }

                        return new LogMessageUIEvent(type, $"Player {dto.ThiefId} stole {dto.VictimIdsToAmounts.Values.Sum()} {dto.Resource} cards from other players");
                    }

                case CardStolenEventThiefDto dto:
                    return new LogMessageUIEvent(type, $"You stole a {dto.Resource} card from Player {dto.VictimId}");

                case CardStolenEventVictimDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.ThiefId} stole a {dto.Resource} card from you");

                case CardStolenEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.ThiefId} stole a {dto.Resource} card from Player {dto.VictimId}");

                case CardsDiscardedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You discarded cards");

                case CardsDiscardedPublicEventDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} discarded cards");

                case PlayerResourcesReceivedEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You received resources {dto.PlayerResources}");

                case PlayerResourcesReceivedEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} received resources");

                case RoadChampionChangedEventDto dto:
                    return new LogMessageUIEvent(type, $"The longest road champion changed to {dto.NewChampionId}");

                case KnightChampionChangedEventDto dto:
                    return new LogMessageUIEvent(type, $"The largest army champion changed to {dto.NewChampionId}");

                case RolledNumberChangedEventDto dto:
                    return new LogMessageUIEvent(type, $"Rolled {dto.NewRolledNumber}");

                case GameWonEventDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} won the game");

                case BankTradeDoneEventPrivateDto dto:
                    return new LogMessageUIEvent(type, $"You traded {dto.Ratio} {dto.Offered} for a {dto.Desired} with the bank");

                case BankTradeDoneEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.PlayerId} traded {dto.Ratio} {dto.Offered} for a {dto.Desired} with the bank");

                case TradeDoneEventSellerDto dto:
                    return new LogMessageUIEvent(type, $"You traded {dto.Offered} with Player {dto.BuyerId} for {dto.Desired}");

                case TradeDoneEventBuyerDto dto:
                    return new LogMessageUIEvent(type, $"You traded {dto.Desired} for {dto.Offered} with Player {dto.SellerId}");

                case TradeDoneEventPublicDto dto:
                    return new LogMessageUIEvent(type, $"Player {dto.SellerId} traded {dto.Offered} for {dto.Desired} with Player {dto.BuyerId}");

                case RobberPlacedEventDto dto:
                    return new LogMessageUIEvent(type, $"The robber was moved");

                case ResourcesDistributionDonePrivateEventDto dto:
                    {
                        var text = "";

                        foreach (var entry in dto.PlayersIdstoResourceChange)
                        {
                            if (entry.Key == _cache.MyPlayer.PlayerId)
                            {
                                text += $"You received {entry.Value} resources\n";
                            }
                            else
                            {
                                text += $"Player {entry.Key} received {entry.Value} resources\n";
                            }
                        }

                        return new LogMessageUIEvent(type, text);
                    }

                case TurnNumberChangedEventDto dto:
                    return new LogMessageUIEvent(type, $"Turn number changed to {dto.NewTurnNumber}");
            }

            throw new Exception($"Unknown domain event: {domainEvent.GetType().Name}");
        }
    }
}