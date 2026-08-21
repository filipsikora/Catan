#nullable enable

using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Interfaces;
using Catan.Unity.Caches;
using Catan.Unity.Models;
using System;
using System.Linq;

namespace Unity.Helpers
{
    public class DomainEventDispatcher
    {
        private readonly GameCache _gameCache;

        public DomainEventDispatcher(GameCache gameCache)
        {
            _gameCache = gameCache;
        }

        public IDomainEventDto Handle(GameUpdateDto update)
        {
            if (!Enum.TryParse<EnumDomainEventsDto>(update.DtoType, out var type))
                throw new Exception($"Unknown domain event: {update.DtoType}");

            switch (type)
            {
                case EnumDomainEventsDto.VillagePlacedEventPrivateDto:
                    return HandleVillagePlacedPrivate(Deserialize<VillagePlacedEventPrivateDto>(update));

                case EnumDomainEventsDto.VillagePlacedEventPublicDto:
                    return HandleVillagePlacedPublic(Deserialize<VillagePlacedEventPublicDto>(update));

                case EnumDomainEventsDto.RoadPlacedEventPrivateDto:
                    return HandleRoadPlacedPrivate(Deserialize<RoadPlacedEventPrivateDto>(update));

                case EnumDomainEventsDto.RoadPlacedEventPublicDto:
                    return HandleRoadPlacedPublic(Deserialize<RoadPlacedEventPublicDto>(update));

                case EnumDomainEventsDto.TownPlacedEventPrivateDto:
                    return HandleTownPlacedPrivate(Deserialize<TownPlacedEventPrivateDto>(update));

                case EnumDomainEventsDto.TownPlacedEventPublicDto:
                    return HandleTownPlacedPublic(Deserialize<TownPlacedEventPublicDto>(update));

                case EnumDomainEventsDto.DevCardUsedEventPrivateDto:
                    return HandleDevCardUsedPrivate(Deserialize<DevCardUsedEventPrivateDto>(update));

                case EnumDomainEventsDto.DevCardUsedEventPublicDto:
                    return HandleDevCardUsedPublic(Deserialize<DevCardUsedEventPublicDto>(update));

                case EnumDomainEventsDto.DevCardBoughtEventPrivateDto:
                    return HandleDevCardBoughtPrivate(Deserialize<DevCardBoughtEventPrivateDto>(update));

                case EnumDomainEventsDto.DevCardBoughtEventPublicDto:
                    return HandleDevCardBoughtPublic(Deserialize<DevCardBoughtEventPublicDto>(update));

                case EnumDomainEventsDto.VictoryCardUsedEventDto:
                    return HandleVictoryCardUsed(Deserialize<VictoryCardUsedEventDto>(update));

                case EnumDomainEventsDto.KnightCardUsedEventDto:
                    return HandleKnightCardUsed(Deserialize<KnightCardUsedEventDto>(update));

                case EnumDomainEventsDto.CardsStolenEventThiefDto:
                    return HandleCardsStolenThief(Deserialize<CardsStolenEventThiefDto>(update));

                case EnumDomainEventsDto.CardsStolenEventVictimDto:
                    return HandleCardsStolenVictim(Deserialize<CardsStolenEventVictimDto>(update));

                case EnumDomainEventsDto.CardsStolenEventPublicDto:
                    return HandleCardsStolenPublic(Deserialize<CardsStolenEventPublicDto>(update));

                case EnumDomainEventsDto.CardStolenEventThiefDto:
                    return HandleCardStolenThief(Deserialize<CardStolenEventThiefDto>(update));

                case EnumDomainEventsDto.CardStolenEventVictimDto:
                    return HandleCardStolenVictim(Deserialize<CardStolenEventVictimDto>(update));

                case EnumDomainEventsDto.CardStolenEventPublicDto:
                    return HandleCardStolenPublic(Deserialize<CardStolenEventPublicDto>(update));

                case EnumDomainEventsDto.CardsDiscardedEventPrivateDto:
                    return HandleCardsDiscardedPrivate(Deserialize<CardsDiscardedEventPrivateDto>(update));

                case EnumDomainEventsDto.CardsDiscardedPublicEventDto:
                    return HandleCardsDiscardedPublic(Deserialize<CardsDiscardedPublicEventDto>(update));

                case EnumDomainEventsDto.PlayerResourcesReceivedEventPrivateDto:
                    return HandlePlayerResourcesReceivedPrivate(Deserialize<PlayerResourcesReceivedEventPrivateDto>(update));

                case EnumDomainEventsDto.PlayerResourcesReceivedEventPublicDto:
                    return HandlePlayerResourcesReceivedPublic(Deserialize<PlayerResourcesReceivedEventPublicDto>(update));

                case EnumDomainEventsDto.RoadChampionChangedEventDto:
                    return HandleRoadChampionChanged(Deserialize<RoadChampionChangedEventDto>(update));

                case EnumDomainEventsDto.KnightChampionChangedEventDto:
                    return HandleKnightChampionChanged(Deserialize<KnightChampionChangedEventDto>(update));

                case EnumDomainEventsDto.RolledNumberChangedEventDto:
                    return HandleRolledNumberChanged(Deserialize<RolledNumberChangedEventDto>(update));

                case EnumDomainEventsDto.PhaseChangedEventDto:
                    return HandlePhaseChanged(Deserialize<PhaseChangedEventDto>(update));

                case EnumDomainEventsDto.PlayersToMoveChangedEventDto:
                    return HandlePlayersToMoveChanged(Deserialize<PlayersToMoveChangedEventDto>(update));

                case EnumDomainEventsDto.GameWonEventDto:
                    return HandleGameWon(Deserialize<GameWonEventDto>(update));

                case EnumDomainEventsDto.BankTradeDoneEventPrivateDto:
                    return HandleBankTradeDonePrivate(Deserialize<BankTradeDoneEventPrivateDto>(update));

                case EnumDomainEventsDto.BankTradeDoneEventPublicDto:
                    return HandleBankTradeDonePublic(Deserialize<BankTradeDoneEventPublicDto>(update));

                case EnumDomainEventsDto.TradeDoneEventSellerDto:
                    return HandleTradeDoneSeller(Deserialize<TradeDoneEventSellerDto>(update));

                case EnumDomainEventsDto.TradeDoneEventBuyerDto:
                    return HandleTradeDoneBuyer(Deserialize<TradeDoneEventBuyerDto>(update));

                case EnumDomainEventsDto.TradeDoneEventPublicDto:
                    return HandleTradeDonePublic(Deserialize<TradeDoneEventPublicDto>(update));

                case EnumDomainEventsDto.RobberPlacedEventDto:
                    return HandleRobberPlaced(Deserialize<RobberPlacedEventDto>(update));

                case EnumDomainEventsDto.DevCardPlayabilityChangedEventPrivateDto:
                    return HandleDevCardPlayabilityChangedPrivate(Deserialize<DevCardPlayabilityChangedEventPrivateDto>(update));

                case EnumDomainEventsDto.DevCardPlayabilityChangedEventPublicDto:
                    return HandleDevCardPlayabilityChangedPublic(Deserialize<DevCardPlayabilityChangedEventPublicDto>(update));

                default:
                    throw new Exception($"Unknown domain event: {update.DtoType}");
            }
        }

        private VillagePlacedEventPrivateDto HandleVillagePlacedPrivate(VillagePlacedEventPrivateDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Village;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.Points = dto.Points;
            _gameCache.MyPlayer.BuildingsLeft["Village"] = dto.VillagesLeft;
            _gameCache.MyPlayer.Resources = dto.Resources;

            return dto;
        }

        private VillagePlacedEventPublicDto HandleVillagePlacedPublic(VillagePlacedEventPublicDto dto)
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
            }

            return dto;
        }

        private RoadPlacedEventPrivateDto HandleRoadPlacedPrivate(RoadPlacedEventPrivateDto dto)
        {
            var edge = _gameCache.Board.Edges[dto.EdgeId];

            edge.OwnerId = dto.OwnerId;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.BuildingsLeft["Road"] = dto.RoadsLeft;
            _gameCache.MyPlayer.Resources = dto.Resources;

            return dto;
        }

        private RoadPlacedEventPublicDto HandleRoadPlacedPublic(RoadPlacedEventPublicDto dto)
        {
            var edge = _gameCache.Board.Edges[dto.EdgeId];

            edge.OwnerId = dto.OwnerId;

            _gameCache.GameFlow.Bank = dto.Bank;

            UpdateOtherPlayerResourceCount(dto.OwnerId, dto.ResourcesCount);

            return dto;
        }

        private TownPlacedEventPrivateDto HandleTownPlacedPrivate(TownPlacedEventPrivateDto dto)
        {
            var vertex = _gameCache.Board.Vertices[dto.VertexId];

            vertex.OwnerId = dto.OwnerId;
            vertex.Building = EnumBuildings.Town;

            _gameCache.GameFlow.Bank = dto.Bank;

            _gameCache.MyPlayer.Points = dto.Points;
            _gameCache.MyPlayer.BuildingsLeft["Town"] = dto.TownsLeft;
            _gameCache.MyPlayer.BuildingsLeft["Village"] = dto.VillagesLeft;
            _gameCache.MyPlayer.Resources = dto.Resources;

            return dto;
        }

        private TownPlacedEventPublicDto HandleTownPlacedPublic(TownPlacedEventPublicDto dto)
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
            }

            return dto;
        }

        private DevCardUsedEventPrivateDto HandleDevCardUsedPrivate(DevCardUsedEventPrivateDto dto)
        {
            var cards = _gameCache.MyPlayer.DevCards.ToList();

            cards.RemoveAll(x => x.Id == dto.CardId);

            _gameCache.MyPlayer.DevCards = cards;
            _gameCache.MyPlayer.DevCardNumber = dto.DevCardNumber;

            return dto;
        }

        private DevCardUsedEventPublicDto HandleDevCardUsedPublic(DevCardUsedEventPublicDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
                player.DevCardsNumber = dto.DevCardNumber;

            return dto;
        }

        private DevCardBoughtEventPrivateDto HandleDevCardBoughtPrivate(DevCardBoughtEventPrivateDto dto)
        {
            var devCard = new DevCardModel(dto.CardId, dto.DevCardType, dto.IsPlayable);

            _gameCache.MyPlayer.DevCardNumber = dto.DevCardNumber;
            _gameCache.MyPlayer.DevCards.Add(devCard);
            _gameCache.MyPlayer.Resources = dto.Resources;

            return dto;
        }

        private DevCardBoughtEventPublicDto HandleDevCardBoughtPublic(DevCardBoughtEventPublicDto dto)
        {
            OtherPlayerModel? player = FindOtherPlayer(dto.PlayerId);

            if (player != null)
            {
                player.DevCardsNumber = dto.DevCardNumber;
                player.ResourceCardsNumber = dto.ResourcesNumber;
            }

            return dto;
        }

        private VictoryCardUsedEventDto HandleVictoryCardUsed(VictoryCardUsedEventDto dto)
        {
            return dto;
        }

        private KnightCardUsedEventDto HandleKnightCardUsed(KnightCardUsedEventDto dto)
        {
            return dto;
        }

        private CardsStolenEventThiefDto HandleCardsStolenThief(CardsStolenEventThiefDto dto)
        {
            return dto;
        }

        private CardsStolenEventVictimDto HandleCardsStolenVictim(CardsStolenEventVictimDto dto)
        {
            return dto;
        }

        private CardsStolenEventPublicDto HandleCardsStolenPublic(CardsStolenEventPublicDto dto)
        {
            return dto;
        }

        private CardStolenEventThiefDto HandleCardStolenThief(CardStolenEventThiefDto dto)
        {
            return dto;
        }

        private CardStolenEventVictimDto HandleCardStolenVictim(CardStolenEventVictimDto dto)
        {
            return dto;
        }

        private CardStolenEventPublicDto HandleCardStolenPublic(CardStolenEventPublicDto dto)
        {
            return dto;
        }

        private CardsDiscardedEventPrivateDto HandleCardsDiscardedPrivate(CardsDiscardedEventPrivateDto dto)
        {
            return dto;
        }

        private CardsDiscardedPublicEventDto HandleCardsDiscardedPublic(CardsDiscardedPublicEventDto dto)
        {
            return dto;
        }

        private PlayerResourcesReceivedEventPrivateDto HandlePlayerResourcesReceivedPrivate(PlayerResourcesReceivedEventPrivateDto dto)
        {
            return dto;
        }

        private PlayerResourcesReceivedEventPublicDto HandlePlayerResourcesReceivedPublic(PlayerResourcesReceivedEventPublicDto dto)
        {
            return dto;
        }

        private RoadChampionChangedEventDto HandleRoadChampionChanged(RoadChampionChangedEventDto dto)
        {
            return dto;
        }

        private KnightChampionChangedEventDto HandleKnightChampionChanged(KnightChampionChangedEventDto dto)
        {
            return dto;
        }

        private RolledNumberChangedEventDto HandleRolledNumberChanged(RolledNumberChangedEventDto dto)
        {
            return dto;
        }

        private PhaseChangedEventDto HandlePhaseChanged(PhaseChangedEventDto dto)
        {
            return dto;
        }

        private PlayersToMoveChangedEventDto HandlePlayersToMoveChanged(PlayersToMoveChangedEventDto dto)
        {
            return dto;
        }

        private GameWonEventDto HandleGameWon(GameWonEventDto dto)
        {
            return dto;
        }

        private BankTradeDoneEventPrivateDto HandleBankTradeDonePrivate(BankTradeDoneEventPrivateDto dto)
        {
            return dto;
        }

        private BankTradeDoneEventPublicDto HandleBankTradeDonePublic(BankTradeDoneEventPublicDto dto)
        {
            return dto;
        }

        private TradeDoneEventSellerDto HandleTradeDoneSeller(TradeDoneEventSellerDto dto)
        {
            return dto;
        }

        private TradeDoneEventBuyerDto HandleTradeDoneBuyer(TradeDoneEventBuyerDto dto)
        {
            return dto;
        }

        private TradeDoneEventPublicDto HandleTradeDonePublic(TradeDoneEventPublicDto dto)
        {
            return dto;
        }

        private RobberPlacedEventDto HandleRobberPlaced(RobberPlacedEventDto dto)
        {
            return dto;
        }

        private DevCardPlayabilityChangedEventPrivateDto HandleDevCardPlayabilityChangedPrivate(DevCardPlayabilityChangedEventPrivateDto dto)
        {
            return dto;
        }

        private DevCardPlayabilityChangedEventPublicDto HandleDevCardPlayabilityChangedPublic(DevCardPlayabilityChangedEventPublicDto dto)
        {
            return dto;
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

        private T Deserialize<T>(GameUpdateDto update)
            where T : IDomainEventDto
        {
            return update.Payload.ToObject<T>()
                ?? throw new Exception($"Failed to deserialize {typeof(T).Name}");
        }
    }
}