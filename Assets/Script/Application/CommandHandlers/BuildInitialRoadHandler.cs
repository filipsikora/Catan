using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildInitialRoadHandler
    {
        private GameState _game;

        public BuildInitialRoadHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildInitialRoad Handle(int playerId, Edge edge, Vertex vertex)
        {
            var player = _game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildInitialRoad(player, edge, vertex, _game);

            if (!result.Success)
            {
                return ResultBuildInitialRoad.Fail(result.Reason, playerId, edge);
            }

            _game.RoadBuiltMutation(player, edge);

            return ResultBuildInitialRoad.Ok(playerId, edge);
        }
    }
}