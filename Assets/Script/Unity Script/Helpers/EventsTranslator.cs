using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Interfaces;
using Catan.Unity.Interfaces;
using Catan.Unity.InternalUIEvents;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace Catan.Unity.Helpers
{
    public static class EventsTranslator
    {
        public static IInternalUIEvents TranslateUIMessage(UiMessageDto message)
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
                        return new ActionRejectedUIEvent(dto.PlayerId, dto.Reason);
                    }

                case EnumUiMessages.PotentialVictimsFoundMessage:
                    {
                        var dto = data.ToObject<PotentialVictimsFoundDto>();
                        return new PotentialVictimsFoundUIEvent(dto.VictimsIds);
                    }

                case EnumUiMessages.BankTradeRatioChangedMessage:
                    {
                        var dto = data.ToObject<BankTradeRatioChangedDto>();
                        return new BankTradeRatioChangedUIEvent(dto.Ratio, dto.PossibleForPlayer, dto.Resource);
                    }

                default:
                    throw new Exception($"Unknown UI message: {message.Type}");
            }
        }

        public static List<IInternalUIEvents> TranslateDomainEvent(IDomainEventDto domainEvent)
        {
            var uiEvents = new List<IInternalUIEvents>();

            switch (domainEvent)
            {
                case VillagePlacedEventPrivateDto dto:
                    {
                        uiEvents.Add(new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId));

                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case VillagePlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId));

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case RoadPlacedEventPrivateDto dto:
                    {
                        uiEvents.Add(new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId));

                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case RoadPlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId));

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case TownPlacedEventPrivateDto dto:
                    {
                        uiEvents.Add(new TownPlacedUIEvent(dto.VertexId, dto.OwnerId));

                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case TownPlacedEventPublicDto dto:
                    {
                        uiEvents.Add(new TownPlacedUIEvent(dto.VertexId, dto.OwnerId));

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case BankTradeDoneEventPrivateDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case BankTradeDoneEventPublicDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case DevCardUsedEventPrivateDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case DevCardUsedEventPublicDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case DevCardBoughtEventPrivateDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case DevCardBoughtEventPublicDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case VictoryCardUsedEventDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case KnightCardUsedEventDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardsStolenEventDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardStolenEventThiefDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardStolenEventVictimDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardStolenEventPublicDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardsDiscardedEventPrivateDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case CardsDiscardedPublicEventDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case PlayerResourcesReceivedEventPrivateDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case PlayerResourcesReceivedEventPublicDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case RoadChampionChangedEventDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case KnightChampionChangedEventDto:
                    {
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case GameWonEventDto dto:
                    {
                        uiEvents.Add(new GameWonUIEvent(dto.PlayerId, dto.PlayerScoresToIds));
                        return uiEvents;
                    }

                case TradeDoneEventSellerDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case TradeDoneEventBuyerDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case TradeDoneEventPublicDto:
                    {
                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case RobberPlacedEventDto dto:
                    {
                        uiEvents.Add(new RobberMovedUIEvent(dto.HexId));
                        return uiEvents;
                    }

                case ResourcesDistributionDonePrivateEventDto:
                    {
                        uiEvents.Add(new MyResourcesChangedUIEvent());

                        uiEvents.Add(new BankInformationChangedUIEvent());
                        uiEvents.Add(new PlayerInformationTableChangedUIEvent());
                        return uiEvents;
                    }

                case TurnNumberChangedEventDto dto:
                    {
                        uiEvents.Add(new TurnNumberChangedUIEvent());
                        return uiEvents;
                    }

                case RolledNumberChangedEventDto dto:
                    {
                        uiEvents.Add(new RolledNumberChangedUIEvent());
                        return uiEvents;
                    }

                case PlayersToMoveChangedEventDto dto:
                    {
                        uiEvents.Add(new PlayersToMoveChangedUIEvent(dto.PlayersToMove));
                        return uiEvents;
                    }

                default:
                    throw new Exception($"Unknown Domain event: {domainEvent}");
            }
        }
    }
}