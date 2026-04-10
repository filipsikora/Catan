using Catan.Shared.Data;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Helpers
{
    public static class Mappers
    {
        public static string MapEnumQueryToString(EnumQueryName queryName)
        {
            return queryName switch
            {
                EnumQueryName.Board => "board",
                EnumQueryName.CurrentPlayerDevCards => "current-player-dev-cards",
                EnumQueryName.NotCurrentPlayerNames => "not-current-player-names",
                EnumQueryName.PlayerCards => "player-cards",
                EnumQueryName.PlayerData => "player-data",
                EnumQueryName.ResourcesAvailability => "resources-availability",
                EnumQueryName.TradeOfferData => "trade-offer-data",
                EnumQueryName.VictimCards => "victim-cards",
                _ => throw new Exception($"Unknown query: {queryName}")
            };
        }

        public static Dictionary<EnumResourceType, T> MapStringResourcesToEnumInDictionary<T>(Dictionary<string, T> resourcesString)
        {
            return resourcesString.ToDictionary(
                kvp => MapStringResourcesToEnum(kvp.Key),
                kvp => kvp.Value);
        }

        public static EnumResourceType MapStringResourcesToEnum(string? resourceString, bool nullable = false)
        {
            if (resourceString == null && !nullable)
                throw new Exception($"Resource: {resourceString} is empty");

            if (!Enum.TryParse<EnumResourceType>(resourceString, out var resource))
                throw new Exception($"Failed to parse resource: {resourceString}");

            return resource;
        }

        public static EnumFieldTypes MapStringFieldToEnum(string? fieldString)
        {
            if (fieldString == null)
                throw new Exception($"Field: {fieldString} is empty");

            if (!Enum.TryParse<EnumFieldTypes>(fieldString, out var field))
                throw new Exception($"Failed to parse field: {fieldString}");

            return field;
        }

        public static EnumDevelopmentCardTypes MapStringDevCardToEnum(string? devCardString)
        {
            if (devCardString == null)
                throw new Exception($"DevCard: {devCardString} is empty");

            if (!Enum.TryParse<EnumDevelopmentCardTypes>(devCardString, out var devCard))
                throw new Exception($"Failed to parse field: {devCardString}");

            return devCard;
        }

        public static ConditionFailureReason MapStringFailureReasonToEnum(string? failureReasonString)
        {
            if (failureReasonString == null)
                throw new Exception($"DevCard: {failureReasonString} is empty");

            if (!Enum.TryParse<ConditionFailureReason>(failureReasonString, out var failureReason))
                throw new Exception($"Failed to parse field: {failureReasonString}");

            return failureReason;
        }

        public static EnumLogTypes MapStringLogTypeToEnum(string? logTypeString)
        {
            if (logTypeString == null)
                throw new Exception($"DevCard: {logTypeString} is empty");

            if (!Enum.TryParse<EnumLogTypes>(logTypeString, out var logType))
                throw new Exception($"Failed to parse field: {logTypeString}");

            return logType;
        }
    }
}