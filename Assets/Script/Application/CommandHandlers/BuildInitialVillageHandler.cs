using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildInitialVillageHandler
    {
        private GameState _game;

        public BuildInitialVillageHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildInitialVillage Handle(int playerId, Vertex vertex)
        {
            var player = _game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildInitialVillage(player, vertex, _game);

            if (!result.Success)
            {
                return ResultBuildInitialVillage.Fail(result.Reason, playerId, vertex);
            }

            var secondVillage = player.Points == 1;

            _game.VillageBuiltMutation(player, vertex, secondVillage);

            return ResultBuildInitialVillage.Ok(playerId, vertex);
        }
    }
}