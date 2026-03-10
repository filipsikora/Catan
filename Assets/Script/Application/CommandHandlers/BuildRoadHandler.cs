using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildRoadHandler
    {
        private GameState _game;

        public BuildRoadHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildRoad Handle(int playerId, Edge edge)
        {
            var player = _game.GetCurrentPlayer();

            var result = RulesBuilding.CanBuildRoad(player, edge, _game);

            if (!result.Success)
            {
                return ResultBuildRoad.Fail(result.Reason, playerId, edge);
            }

            _game.RoadPaidAndBuiltMutation(player, edge);

            return ResultBuildRoad.Ok(playerId, edge);
        }
    }
}