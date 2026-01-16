using Catan.Application.Snapshots;
using System.Collections.Generic;

namespace Catan.Application.Queries.Players
{
    public interface IPlayersQueryService
    {
        PlayerResourcesSnapshot GetPlayersCards(int playerId);

        PlayerDataSnapshot GetPlayersData(int playerId);

        CurrentPlayerIdSnapshot GetCurrentPlayerId();

        List<PlayerNameSnapshot> GetAllPlayersNames();

        List<PlayerNameSnapshot> GetSomePlayersNames(List<int> potentialVictimsIds);

        List<PlayerNameSnapshot> GetNotCurrentPlayersNames();
    }
}