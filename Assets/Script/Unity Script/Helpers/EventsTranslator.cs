using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Interfaces;
using Catan.Unity.Caches;
using Catan.Unity.Interfaces;
using Catan.Unity.InternalUIEvents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        private GameCache _gameCache;

        public EventsTranslator(GameCache gameCache)
        {
            _gameCache = gameCache;
        }

        public IInternalUIEvents TranslateUIMessage(UiMessageDto message)
        {
            var data = (JObject)message.Data;

            if (!Enum.TryParse<EnumUiMessages>(message.Type, out var type))
                throw new Exception($"Failed to parse UiMessage: {message.Type}");

            switch (type)
            {
                case EnumUiMessages.VertexHighlightedMessage:
                    {
                        var dto = data.ToObject<VertexHighlightedDto>();
                        return new VertexHighlightedUIEvent(dto.VertexId);
                    }

                case EnumUiMessages.EdgeHighlightedMessage:
                    {
                        var dto = data.ToObject<EdgeHighlightedDto>();
                        return new EdgeHighlightedUIEvent(dto.EdgeId);
                    }

                case EnumUiMessages.BuildOptionsSentMessage:
                    {
                        var dto = data.ToObject<BuildOptionsSentDto>();
                        return new BuildOptionsSentUIEvent(dto.CanBuildVillage, dto.CanBuildRoad, dto.CanUpgradeVillage);
                    }

                case EnumUiMessages.ActionRejectedMessage:
                    {
                        var dto = data.ToObject<ActionRejectedDto>();
                        return new ActionRejectedUIEvent(dto.PlayerId, Mappers.MapStringFailureReasonToEnum(dto.Reason));
                    }

                case EnumUiMessages.PotentialVictimsFoundMessage:
                    {
                        var dto = data.ToObject<PotentialVictimsFoundDto>();
                        return new PotentialVictimsFoundUIEvent(dto.VictimsIds);
                    }

                case EnumUiMessages.BankTradeRatioChangedMessage:
                    {
                        var dto = data.ToObject<BankTradeRatioChangedDto>();
                        var nullable = true;
                        return new BankTradeRatioChangedUIEvent(dto.Ratio, dto.PossibleForPlayer, Mappers.MapStringResourcesToEnum(dto.Resource, nullable));
                    }

                case EnumUiMessages.TurnNumberChangedMessage:
                    {
                        var dto = data.ToObject<TurnNumberChangedDto>();
                        return new TurnNumberChangedUIEvent(dto.NewTurnNumber);
                    }

                case EnumUiMessages.DiceRollChangedMessage:
                    {
                        var dto = data.ToObject<DiceRollChangedDto>();
                        return new DiceRollChangedUIEvent(dto.RolledNumber);
                    }

                default:
                    throw new Exception($"Unknown UI message: {message.Type}");
            }
        }

        public List<IInternalUIEvents> TranslateDomainEvent(IDomainEventDto domainEvent)
        {
            var uiEvents = new List<IInternalUIEvents>();

            switch (domainEvent)
            {
                case VillagePlacedEventPrivateDto dto:
                    {
                        uiEvents.Add(new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId));
                        uiEvents.Add(new MyResourcesChangedUIEvent(_gameCache.MyPlayer.Resources));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        uiEvents.Add(new PlayersPointsChangedUIEvent())
                        return uiEvents;
                    }

                case VillagePlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;
                    }

                case RoadPlacedEventPrivateDto dto:
                                        {
                        uiEvents.Add(new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId));
                        uiEvents.Add(new MyResourcesChangedUIEvent(_gameCache.MyPlayer.Resources));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;
                    }

                case RoadPlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;
                    }

                case TownPlacedEventPrivateDto dto:
                    {
                        uiEvents.Add(new TownPlacedUIEvent(dto.VertexId, dto.OwnerId));
                        uiEvents.Add(new MyResourcesChangedUIEvent(_gameCache.MyPlayer.Resources));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;
                    }

                case TownPlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new TownPlacedUIEvent(dto.VertexId, dto.OwnerId));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;
                    }

                case BankTradeDoneEventPrivateDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent(_gameCache.GameFlow.Bank));
                        uiEvents.Add(new MyResourcesChangedUIEvent(_gameCache.MyPlayer.Resources));
                        return uiEvents;
                    }

                case BankTradeDoneEventPublicDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent(_gameCache.GameFlow.Bank));
                        uiEvents.Add(new AllPlayersResourcesChangedUIEvent(_gameCache.GetOtherPlayersResourceCounts()));
                        return uiEvents;    
                    }

                default:
                    throw new Exception($"Unknown Domain event: {domainEvent}");
            }
        }
    }
}