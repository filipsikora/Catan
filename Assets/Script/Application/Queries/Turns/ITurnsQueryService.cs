using Catan.Application.Snapshots;

namespace Catan.Application.Queries.Turns
{
    public interface ITurnsQueryService
    {
        TurnDataSnapshot GetTurnData();
    }
}