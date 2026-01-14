using Catan.Application.Snapshots;

namespace Catan.Application.Queries.Players
{
    public interface ITradeQueryService
    {
        TradeOfferedSnapshot GetTradeOfferData();
    }
}