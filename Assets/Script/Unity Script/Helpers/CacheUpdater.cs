#nullable enable

using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Interfaces;
using Catan.Unity.Caches;
using Catan.Unity.Mappers;
using Catan.Unity.Models;
using System;
using System.Linq;

namespace Unity.Helpers
{
    public class CacheUpdater
    {
        private readonly GameCache _gameCache;

        public CacheUpdater(GameCache gameCache)
        {
            _gameCache = gameCache;
        }

        public void UpdateCache(IDomainEventDto update)
        {
            switch (update)
            {
                case VillagePlacedEventPrivateDto domainEventDto:
                    HandleVillagePlacedPrivate(domainEventDto);
                    break;

                case VillagePlacedEventPublicDto domainEventDto:
                    HandleVillagePlacedPublic(domainEventDto);
                    break;

                case RoadPlacedEventPrivateDto domainEventDto:
                    HandleRoadPlacedPrivate(domainEventDto);
                    break;

                case RoadPlacedEventPublicDto domainEventDto:
                    HandleRoadPlacedPublic(domainEventDto);
                    break;

                case TownPlacedEventPrivateDto domainEventDto:
                    HandleTownPlacedPrivate(domainEventDto);
                    break;

                case TownPlacedEventPublicDto domainEventDto:
                    HandleTownPlacedPublic(domainEventDto);
                    break;

                case DevCardUsedEventPrivateDto domainEventDto:
                    HandleDevCardUsedPrivate(domainEventDto);
                    break;

                case DevCardUsedEventPublicDto domainEventDto:
                    HandleDevCardUsedPublic(domainEventDto);
                    break;

                case DevCardBoughtEventPrivateDto domainEventDto:
                    HandleDevCardBoughtPrivate(domainEventDto);
                    break;

                case DevCardBoughtEventPublicDto domainEventDto:
                    HandleDevCardBoughtPublic(domainEventDto);
                    break;

                case VictoryCardUsedEventDto domainEventDto:
                    HandleVictoryCardUsed(domainEventDto);
                    break;

                case KnightCardUsedEventDto domainEventDto:
                    HandleKnightCardUsed(domainEventDto);
                    break;

                case CardsStolenEventThiefDto domainEventDto:
                    HandleCardsStolenThief(domainEventDto);
                    break;

                case CardsStolenEventVictimDto domainEventDto:
                    HandleCardsStolenVictim(domainEventDto);
                    break;

                case CardsStolenEventPublicDto domainEventDto:
                    HandleCardsStolenPublic(domainEventDto);
                    break;

                case CardStolenEventThiefDto domainEventDto:
                    HandleCardStolenThief(domainEventDto);
                    break;

                case CardStolenEventVictimDto domainEventDto:
                    HandleCardStolenVictim(domainEventDto);
                    break;

                case CardStolenEventPublicDto domainEventDto:
                    HandleCardStolenPublic(domainEventDto);
                    break;

                case CardsDiscardedEventPrivateDto domainEventDto:
                    HandleCardsDiscardedPrivate(domainEventDto);
                    break;

                case CardsDiscardedPublicEventDto domainEventDto:
                    HandleCardsDiscardedPublic(domainEventDto);
                    break;

                case PlayerResourcesReceivedEventPrivateDto domainEventDto:
                    HandlePlayerResourcesReceivedPrivate(domainEventDto);
                    break;

                case PlayerResourcesReceivedEventPublicDto domainEventDto:
                    HandlePlayerResourcesReceivedPublic(domainEventDto);
                    break;

                case RoadChampionChangedEventDto domainEventDto:
                    HandleRoadChampionChanged(domainEventDto);
                    break;

                case KnightChampionChangedEventDto domainEventDto:
                    HandleKnightChampionChanged(domainEventDto);
                    break;

                case RolledNumberChangedEventDto domainEventDto:
                    HandleRolledNumberChanged(domainEventDto);
                    break;

                case PhaseChangedEventDto domainEventDto:
                    HandlePhaseChanged(domainEventDto);
                    break;

                case PlayersToMoveChangedEventDto domainEventDto:
                    HandlePlayersToMoveChanged(domainEventDto);
                    break;

                case GameWonEventDto domainEventDto:
                    HandleGameWon(domainEventDto);
                    break;

                case BankTradeDoneEventPrivateDto domainEventDto:
                    HandleBankTradeDonePrivate(domainEventDto);
                    break;

                case BankTradeDoneEventPublicDto domainEventDto:
                    HandleBankTradeDonePublic(domainEventDto);
                    break;

                case TradeDoneEventSellerDto domainEventDto:
                    HandleTradeDoneSeller(domainEventDto);
                    break;

                case TradeDoneEventBuyerDto domainEventDto:
                    HandleTradeDoneBuyer(domainEventDto);
                    break;

                case TradeDoneEventPublicDto domainEventDto:
                    HandleTradeDonePublic(domainEventDto);
                    break;

                case RobberPlacedEventDto domainEventDto:
                    HandleRobberPlaced(domainEventDto);
                    break;

                case DevCardPlayabilityChangedEventPrivateDto domainEventDto:
                    HandleDevCardPlayabilityChangedPrivate(domainEventDto);
                    break;

                case DevCardPlayabilityChangedEventPublicDto domainEventDto:
                    HandleDevCardPlayabilityChangedPublic(domainEventDto);
                    break;

                default:
                    throw new Exception($"Unknown domain event: {update.GetType().Name}");
            }
        }

        private void HandleVillagePlacedPrivate(VillagePlacedEventPrivateDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Village;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.Points = dto.Points;
            _gameCache.MyPlayer.Resources = dto.Resources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ResourcesCount;
            _gameCache.MyPlayer.BuildingsLeft = dto.BuildingsLeft;
        }

        private void HandleVillagePlacedPublic(VillagePlacedEventPublicDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Village;

            _gameCache.GameFlow.Bank = dto.Bank;

            OtherPlayerModel? player = FindOtherPlayer(dto.OwnerId);

            if (player != null)
            {
                player.Points = dto.Points;
                player.ResourceCardsNumber = dto.ResourcesCount;
                player.BuildingsLeft = dto.BuildingsLeft;
            }
        }

        private void HandleRoadPlacedPrivate(RoadPlacedEventPrivateDto dto)
        {
            var edge = _gameCache.Board.Edges[dto.EdgeId];

            edge.OwnerId = dto.OwnerId;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.Resources = dto.Resources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ResourcesCount;
            _gameCache.MyPlayer.BuildingsLeft = dto.BuildingsLeft;
        }

        private void HandleRoadPlacedPublic(RoadPlacedEventPublicDto dto)
        {
            var edge = _gameCache.Board.Edges[dto.EdgeId];

            edge.OwnerId = dto.OwnerId;

            _gameCache.GameFlow.Bank = dto.Bank;

            OtherPlayerModel? player = FindOtherPlayer(dto.OwnerId);

            if (player != null)
            {
                player.ResourceCardsNumber = dto.ResourcesCount;
                player.BuildingsLeft = dto.BuildingsLeft;
            }
        }

        private void HandleTownPlacedPrivate(TownPlacedEventPrivateDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Town;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.Points = dto.Points;
            _gameCache.MyPlayer.Resources = dto.Resources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ResourcesCount;
            _gameCache.MyPlayer.BuildingsLeft = dto.BuildingsLeft;
        }

        private void HandleTownPlacedPublic(TownPlacedEventPublicDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Town;

            _gameCache.GameFlow.Bank = dto.Bank;

            OtherPlayerModel? player = FindOtherPlayer(dto.OwnerId);

            if (player != null)
            {
                player.Points = dto.Points;
                player.ResourceCardsNumber = dto.ResourcesCount;
                player.BuildingsLeft = dto.BuildingsLeft;
            }
        }

        private void HandleDevCardUsedPrivate(DevCardUsedEventPrivateDto dto)
        {
            _gameCache.MyPlayer.DevCards = PlayerMappers.MapDevCardDtoToModel(dto.DevCards);
            _gameCache.MyPlayer.DevCardNumber = dto.DevCardNumber;
        }

        private void HandleDevCardUsedPublic(DevCardUsedEventPublicDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
                player.DevCardsNumber = dto.DevCardNumber;
        }

        private void HandleDevCardBoughtPrivate(DevCardBoughtEventPrivateDto dto)
        {
            var devCard = new DevCardModel(dto.CardId, dto.DevCardType, dto.IsPlayable);

            _gameCache.MyPlayer.DevCardNumber = dto.DevCardNumber;
            _gameCache.MyPlayer.DevCards.Add(devCard);
            _gameCache.MyPlayer.Resources = dto.Resources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ResourceCardsCount;
        }

        private void HandleDevCardBoughtPublic(DevCardBoughtEventPublicDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
            {
                player.DevCardsNumber = dto.DevCardNumber;
                player.ResourceCardsNumber = dto.ResourcesNumber;
            }
        }

        private void HandleVictoryCardUsed(VictoryCardUsedEventDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
            {
                player.Points = dto.ExtraPoints;
                player.VictoryCardsPlayed = dto.VictoryCardsUsed;
            }
        }

        private void HandleKnightCardUsed(KnightCardUsedEventDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
            {
                player.KnightCardsPlayed = dto.KnightCardsUsed;
            }
        }

        private void HandleCardsStolenThief(CardsStolenEventThiefDto dto)
        {
            _gameCache.MyPlayer.Resources = dto.ThiefResources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ThiefResourcesCount;

            OtherPlayerModel? victim = FindOtherPlayer(dto.VictimId);

            if (victim != null)
            {
                victim.ResourceCardsNumber = dto.VictimResourcesCount;
            }
        }

        private void HandleCardsStolenVictim(CardsStolenEventVictimDto dto)
        {
            _gameCache.MyPlayer.Resources = dto.VictimResources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.VictimResourcesCount;

            OtherPlayerModel? thief = FindOtherPlayer(dto.ThiefId);

            if (thief != null)
            {
                thief.ResourceCardsNumber = dto.ThiefResourcesCount;
            }
        }

        private void HandleCardsStolenPublic(CardsStolenEventPublicDto dto)
        {
            OtherPlayerModel? thief = FindOtherPlayer(dto.ThiefId);

            if (thief != null)
            {
                thief.ResourceCardsNumber = dto.ThiefResourcesCount;
            }

            OtherPlayerModel? victim = FindOtherPlayer(dto.VictimId);

            if (victim != null)
            {
                victim.ResourceCardsNumber = dto.VictimResourcesCount;
            }
        }

        private void HandleCardStolenThief(CardStolenEventThiefDto dto)
        {
            _gameCache.MyPlayer.Resources = dto.ThiefResources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.ThiefResourcesCount;

            OtherPlayerModel? victim = FindOtherPlayer(dto.VictimId);

            if (victim != null)
            {
                victim.ResourceCardsNumber = dto.VictimResourcesCount;
            }
        }

        private void HandleCardStolenVictim(CardStolenEventVictimDto dto)
        {
            _gameCache.MyPlayer.Resources = dto.VictimResources;
            _gameCache.MyPlayer.ResourceCardsNumber = dto.VictimResourcesCount;

            OtherPlayerModel? thief = FindOtherPlayer(dto.ThiefId);

            if (thief != null)
            {
                thief.ResourceCardsNumber = dto.ThiefResourcesCount;
            }
        }

        private void HandleCardStolenPublic(CardStolenEventPublicDto dto)
        {
            OtherPlayerModel? thief = FindOtherPlayer(dto.ThiefId);

            if (thief != null)
            {
                thief.ResourceCardsNumber = dto.ThiefResourcesCount;
            }

            OtherPlayerModel? victim = FindOtherPlayer(dto.VictimId);

            if (victim != null)
            {
                victim.ResourceCardsNumber = dto.VictimResourcesCount;
            }
        }

        private void HandleCardsDiscardedPrivate(CardsDiscardedEventPrivateDto dto)
        {
            _gameCache.MyPlayer.Resources = dto.Resources;
        }

        private void HandleCardsDiscardedPublic(CardsDiscardedPublicEventDto dto)
        {
        }

        private void HandlePlayerResourcesReceivedPrivate(PlayerResourcesReceivedEventPrivateDto dto)
        {
        }

        private void HandlePlayerResourcesReceivedPublic(PlayerResourcesReceivedEventPublicDto dto)
        {
        }

        private void HandleRoadChampionChanged(RoadChampionChangedEventDto dto)
        {
        }

        private void HandleKnightChampionChanged(KnightChampionChangedEventDto dto)
        {
        }

        private void HandleRolledNumberChanged(RolledNumberChangedEventDto dto)
        {
        }

        private void HandlePhaseChanged(PhaseChangedEventDto dto)
        {
        }

        private void HandlePlayersToMoveChanged(PlayersToMoveChangedEventDto dto)
        {
        }

        private void HandleGameWon(GameWonEventDto dto)
        {
        }

        private void HandleBankTradeDonePrivate(BankTradeDoneEventPrivateDto dto)
        {
        }

        private void HandleBankTradeDonePublic(BankTradeDoneEventPublicDto dto)
        {
        }

        private void HandleTradeDoneSeller(TradeDoneEventSellerDto dto)
        {
        }

        private void HandleTradeDoneBuyer(TradeDoneEventBuyerDto dto)
        {
        }

        private void HandleTradeDonePublic(TradeDoneEventPublicDto dto)
        {
        }

        private void HandleRobberPlaced(RobberPlacedEventDto dto)
        {
        }

        private void HandleDevCardPlayabilityChangedPrivate(DevCardPlayabilityChangedEventPrivateDto dto)
        {
        }

        private void HandleDevCardPlayabilityChangedPublic(DevCardPlayabilityChangedEventPublicDto dto)
        {
        }

        private OtherPlayerModel? FindOtherPlayer(int playerId)
        {
            return _gameCache.OtherPlayers.FirstOrDefault(
                x => x.Id == playerId);
        }

        private void UpdateOtherPlayerResourceCount(int playerId, int resourceCount)
        {
            OtherPlayerModel? player = FindOtherPlayer(playerId);

            if (player != null)
                player.ResourceCardsNumber = resourceCount;
        }
    }
}