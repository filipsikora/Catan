using Catan.Shared.Data;

namespace Catan.Unity.Helpers
{
    public static class MapperQueries
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
                EnumQueryName.VictimCards => "victim-cards"
            };
        }
    }
}