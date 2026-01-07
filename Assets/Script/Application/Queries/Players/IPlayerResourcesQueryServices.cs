using Catan.Application.Snapshots;

namespace Catan.Application.Queries.Players
{
    public interface IPlayersQueryService
    {
        PlayerResourcesSnapshot GetPlayersCards(int playerId);

        PlayerDataSnapshot GetPlayersData(int playerId);
    }
}