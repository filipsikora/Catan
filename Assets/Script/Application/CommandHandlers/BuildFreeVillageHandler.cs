using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class BuildFreeVillageHandler
    {
        private GameState _game;

        public BuildFreeVillageHandler(GameState game)
        {
            _game = game;
        }

        public ResultBuildFreeVillage Handle(int playerId, Vertex vertex)
        {
            var player = _game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildFreeVillage(player, vertex, _game);

            if (!result.Success)
            {
                return ResultBuildFreeVillage.Fail(result.Reason, playerId, vertex);
            }

            var secondVillage = player.Points == 2;

            _game.VillageBuiltMutation(player, vertex, secondVillage);

            if (secondVillage)
            {
                _game.GiveResourcesForSecondVillageMutation(player, vertex);
            }

            return ResultBuildFreeVillage.Ok(playerId, vertex);
        }
    }
}