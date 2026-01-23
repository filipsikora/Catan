using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BuildRoadLogic
    {
        public static ResultBuildRoad Handle(GameState game, int playerId, Edge edge)
        {
            var player = game.GetCurrentPlayer();

            var result = RulesBuilding.CanBuildRoad(player, edge, game);

            if (!result.Success)
            {
                return ResultBuildRoad.Fail(result.Reason, playerId, edge);
            }

            game.RoadPaidAndBuiltMutation(player, edge);

            return ResultBuildRoad.Ok(playerId, edge);
        }
    }
}