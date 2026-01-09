using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildFreeRoadHandler
    {
        private GameState _game;

        public BuildFreeRoadHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildFreeRoad Handle(int playerId, Edge edge, Vertex vertex)
        {
            var player = _game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildFreeRoad(player, edge, vertex, _game);

            if (!result.Success)
            {
                return ResultBuildFreeRoad.Fail(result.Reason, playerId, edge);
            }

            _game.RoadBuiltMutation(player, edge);

            return ResultBuildFreeRoad.Ok(playerId, edge);
        }
    }
}