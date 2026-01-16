using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildVillageHandler
    {
        private GameState _game;

        public BuildVillageHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildVillage Handle(int playerId, Vertex vertex)
        {
            var player = _game.GetCurrentPlayer();

            var result = RulesBuilding.CanBuildVillage(player, vertex, _game);

            if (!result.Success)
            {
                return ResultBuildVillage.Fail(result.Reason, playerId, vertex);
            }

            _game.VillagePaidAndBuiltMutation(player, vertex);

            return ResultBuildVillage.Ok(playerId, vertex);
        }
    }
}