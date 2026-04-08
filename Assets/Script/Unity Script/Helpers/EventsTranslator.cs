using Catan.Unity.InternalUIEvents;
using Catan.Unity.Interfaces;
using Newtonsoft.Json.Linq;
using Catan.Shared.Dtos;
using Catan.Shared.Data;
using System;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Dtos.DomainEvents;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        public EventsTranslator() { }

        public IInternalUIEvents TranslateUIMessage(UiMessageDto message)
        {
            var data = (JObject)message.Data;

            switch (message.Type)
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

                case EnumUiMessages.LogMessageMessage:
                    {
                        var dto = data.ToObject<LogMessageDto>();
                        return new LogMessageUIEvent(dto.Type, dto.Message);
                    }

                case EnumUiMessages.ActionRejectedMessage:
                    {
                        var dto = data.ToObject<ActionRejectedDto>();
                        return new ActionRejectedUIEvent(dto.PlayerId, dto.Reason);
                    }

                case EnumUiMessages.ResourceSelectedMessage:
                    {
                        var dto = data.ToObject<ResourceSelectedDto>();
                        return new ResourceSelectedUIEvent(dto.Selected, dto.Type);
                    }

                case EnumUiMessages.SelectionChangedMessage:
                    {
                        var dto = data.ToObject<SelectionChangedDto>();
                        return new SelectionChangedUIEvent(dto.ActionAvailable);
                    }

                case EnumUiMessages.DesiredCardsChangedMessage:
                    {
                        var dto = data.ToObject<DesiredCardsChangedDto>();
                        return new DesiredCardsChangedUIEvent(dto.HasDesired);
                    }

                case EnumUiMessages.PlayerSelectedToDiscardMessage:
                    {
                        var dto = data.ToObject<PlayerSelectedToDiscardDto>();
                        return new PlayerSelectedToDiscardUIEvent(dto.PlayerId);
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

        public IInternalUIEvents TranslateDomainEvent(DomainEventDto message)
        {
            var data = (JObject)message.Data;

            switch (message.Type)
            {
                case EnumDomainEvents.VillagePlacedEvent:
                    {
                        var dto = data.ToObject<VillagePlacedDto>();
                        return new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId);
                    }

                case EnumDomainEvents.RoadPlacedEvent:
                    {
                        var dto = data.ToObject<RoadPlacedDto>();
                        return new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId);
                    }

                case EnumDomainEvents.TownPlacedEvent:
                    {
                        var dto = data.ToObject<TownPlacedDto>();
                        return new TownPlacedUIEvent(dto.VertexId, dto.OwnerId);
                    }

                case EnumDomainEvents.DevelopmentCardBoughtEvent:
                    {
                        var dto = data.ToObject<DevelopmentCardBoughtDto>();
                        return new DevelopmentCardBoughtUIEvent(dto.CardId);
                    }

                case EnumDomainEvents.RobberPlacedEvent:
                    {
                        var dto = data.ToObject<RobberPlacedDto>();
                        return new RobberMovedUIEvent(dto.HexId);
                    }

                case EnumDomainEvents.PlayerStateChangedEvent:
                    {
                        var dto = data.ToObject<PlayerStateChangedDto>();
                        return new PlayerStateChangedUIEvent(dto.PlayerId);
                    }

                default:
                    throw new Exception($"Unknown domain event: {message.Type}");
            }
        }
    }
}
