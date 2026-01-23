using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BuildInitialRoadLogic
    {
        public static ResultBuildInitialRoad Handle(GameState game, int playerId, Edge edge, Vertex vertex)
        {
            var player = game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildInitialRoad(player, edge, vertex, game);

            if (!result.Success)
            {
                return ResultBuildInitialRoad.Fail(result.Reason, playerId, edge);
            }

            game.RoadBuiltMutation(player, edge);

            return ResultBuildInitialRoad.Ok(playerId, edge);
        }
    }
}