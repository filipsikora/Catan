using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BuildFreeRoadLogic
    {
        public static ResultBuildFreeRoad Handle(GameState game, int playerId, Edge edge)
        {
            var player = game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildFreeRoad(player, edge, game);

            if (!result.Success)
            {
                return ResultBuildFreeRoad.Fail(result.Reason, playerId, edge);
            }

            game.RoadBuiltMutation(player, edge);

            return ResultBuildFreeRoad.Ok(playerId, edge);
        }
    }
}