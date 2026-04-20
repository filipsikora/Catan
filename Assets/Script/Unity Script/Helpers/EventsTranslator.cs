using BGS.Shared.Dtos;
using Catan.Shared.Data;
using Catan.Shared.Dtos.UiMessages;
using Catan.Shared.Dtos.UIMessages;
using Catan.Unity.Interfaces;
using Catan.Unity.InternalUIEvents;
using Newtonsoft.Json.Linq;
using System;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        public EventsTranslator() { }

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

                case EnumUiMessages.LogMessageMessage:
                    {
                        var dto = data.ToObject<LogMessageDto>();
                        return new LogMessageUIEvent(Mappers.MapStringLogTypeToEnum(dto.Type), dto.Message);
                    }

                case EnumUiMessages.ActionRejectedMessage:
                    {
                        var dto = data.ToObject<ActionRejectedDto>();
                        return new ActionRejectedUIEvent(dto.PlayerId, Mappers.MapStringFailureReasonToEnum(dto.Reason));
                    }

                case EnumUiMessages.ResourceSelectedMessage:
                    {
                        var dto = data.ToObject<ResourceSelectedDto>();
                        return new ResourceSelectedUIEvent(dto.Selected, Mappers.MapStringResourcesToEnum(dto.Type));
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

                case EnumUiMessages.VillagePlacedMessage:
                {
                        var dto = data.ToObject<VillagePlacedDto>();
                        return new VillagePlacedUIEvent(dto.VertexId, dto.OwnerId);
                    }

                case EnumUiMessages.RoadPlacedMessage:
                    {
                        var dto = data.ToObject<RoadPlacedDto>();
                        return new RoadPlacedUIEvent(dto.EdgeId, dto.OwnerId);
                    }

                case EnumUiMessages.TownPlacedMessage:
                    {
                        var dto = data.ToObject<TownPlacedDto>();
                        return new TownPlacedUIEvent(dto.VertexId, dto.OwnerId);
                    }

                case EnumUiMessages.DevelopmentCardBoughtMessage:
                    {
                        var dto = data.ToObject<DevelopmentCardBoughtDto>();
                        return new DevelopmentCardBoughtUIEvent(dto.CardId);
                    }

                case EnumUiMessages.RobberPlacedMessage:
                    {
                        var dto = data.ToObject<RobberPlacedDto>();
                        return new RobberMovedUIEvent(dto.HexId);
                    }

                case EnumUiMessages.PlayerStateChangedMessage:
                    {
                        var dto = data.ToObject<PlayerStateChangedDto>();
                        return new PlayerStateChangedUIEvent(dto.PlayerId);
                    }

                default:
                    throw new Exception($"Unknown UI message: {message.Type}");
            }
        }
    }
}