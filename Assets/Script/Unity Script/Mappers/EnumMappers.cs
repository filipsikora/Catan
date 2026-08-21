using Catan.Shared.Data;
using System;

namespace Catan.Unity.Mappers
{
    public static class EnumMappers
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
                EnumQueryName.SomePlayersNames => "some-players-names",
                _ => throw new Exception($"Unknown query: {queryName}")
            };
        }

        public static EnumFieldTypes MapResourceToField(EnumResourceType resource)
        {
            return resource switch
            {
                EnumResourceType.Wood => EnumFieldTypes.Wood,
                EnumResourceType.Clay => EnumFieldTypes.Clay,
                EnumResourceType.Wool => EnumFieldTypes.Wool,
                EnumResourceType.Wheat => EnumFieldTypes.Wheat,
                EnumResourceType.Stone => EnumFieldTypes.Stone,
                _ => throw new ArgumentOutOfRangeException(nameof(resource))
            };
        }
    }
}
