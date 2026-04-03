using Catan.Unity.InternalUIEvents;
using Catan.Unity.Interfaces;
using Newtonsoft.Json.Linq;
using Catan.Shared.Dtos;
using Catan.Shared.Data;
using System;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        public EventsTranslator() { }

        public IInternalUIEvents TranslateUIMessage(UiMessageDto message)
        {
            var data = JToken.FromObject(message.Data);

            return message.Type switch
            {
                EnumUiMessages.VertexHighlightedMessage => new VertexHighlightedUIEvent(JsonHelper.GetInt(data, "vertexId")),
                EnumUiMessages.EdgeHighlightedMessage => new EdgeHighlightedUIEvent(JsonHelper.GetInt(data, "edgeId")),
                EnumUiMessages.BuildOptionsSentMessage => new BuildOptionsSentUIEvent
                (JsonHelper.GetBool(data, "canBuildVillage"), JsonHelper.GetBool(data, "canBuildRoad"), JsonHelper.GetBool(data, "canUpgradeVillage")),
                EnumUiMessages.LogMessageMessage => new LogMessageUIEvent(JsonHelper.GetEnum<EnumLogTypes>(data, "type"), JsonHelper.GetString(data, "message")),
                EnumUiMessages.ActionRejectedMessage => new ActionRejectedUIEvent(JsonHelper.GetInt(data, "playerId"), JsonHelper.GetEnum<ConditionFailureReason>(data, "reason")),
                EnumUiMessages.ResourceSelectedMessage => new ResourceSelectedUIEvent(JsonHelper.GetBool(data, "selected"), JsonHelper.GetNullableEnum<EnumResourceType>(data, "type")),
                EnumUiMessages.SelectionChangedMessage => new SelectionChangedUIEvent(JsonHelper.GetBool(data, "actionAvailable")),
                EnumUiMessages.DesiredCardsChangedMessage => new DesiredCardsChangedUIEvent(JsonHelper.GetBool(data, "hasDesired")),
                EnumUiMessages.PlayerSelectedToDiscardMessage => new PlayerSelectedToDiscardUIEvent(JsonHelper.GetInt(data, "playerId")),
                EnumUiMessages.PotentialVictimsFoundMessage => new PotentialVictimsFoundUIEvent(JsonHelper.GetIntList(data, "victimsIds")),
                EnumUiMessages.BankTradeRatioChangedMessage => new BankTradeRatioChangedUIEvent(JsonHelper.GetInt
                (data, "ratio"), JsonHelper.GetBool(data, "possibleForPlayer"), JsonHelper.GetNullableEnum<EnumResourceType>(data, "resource")),
                EnumUiMessages.TurnNumberChangedMessage => new TurnNumberChangedUIEvent(JsonHelper.GetInt(data, "turnNumber")),
                EnumUiMessages.DiceRollChangedMessage => new DiceRollChangedUIEvent(JsonHelper.GetInt(data, "rolledNumber")),
                _ => throw new Exception($"Unknown UI message: {message.Type}")
            };
        }

        public IInternalUIEvents TranslateDomainEvent(DomainEventDto message)
        {
            var data = JToken.FromObject(message.Data);

            return message.Type switch
            {
                EnumDomainEvents.VillagePlacedEvent => new VillagePlacedUIEvent(JsonHelper.GetInt(data, "vertexId"), JsonHelper.GetInt(data, "ownerId")),
                EnumDomainEvents.RoadPlacedEvent => new RoadPlacedUIEvent(JsonHelper.GetInt(data, "edgeId"), JsonHelper.GetInt(data, "ownerId")),
                EnumDomainEvents.TownPlacedEvent => new TownPlacedUIEvent(JsonHelper.GetInt(data, "vertexId"), JsonHelper.GetInt(data, "ownerId")),
                EnumDomainEvents.DevelopmentCardBoughtEvent => new DevelopmentCardBoughtUIEvent(JsonHelper.GetInt(data, "cardId")),
                EnumDomainEvents.RobberPlacedEvent => new RobberMovedUIEvent(JsonHelper.GetInt(data, "hexId")),
                EnumDomainEvents.PlayerStateChangedEvent => new PlayerStateChangedUIEvent(JsonHelper.GetInt(data, "playerId")),
                _ => throw new Exception($"Unknown UI message: {message.Type}")
            };
        }
    }
}
