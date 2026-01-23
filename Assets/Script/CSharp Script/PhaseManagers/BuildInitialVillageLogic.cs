using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BuildInitialVillageLogic
    {
        public static ResultBuildInitialVillage Handle(GameState game, int playerId, Vertex vertex)
        {
            var player = game.GetPlayerById(playerId);

            var result = RulesBuilding.CanBuildInitialVillage(player, vertex, game);

            if (!result.Success)
            {
                return ResultBuildInitialVillage.Fail(result.Reason, playerId, vertex);
            }

            var secondVillage = player.Points == 1;

            game.VillageBuiltMutation(player, vertex, secondVillage);

            return ResultBuildInitialVillage.Ok(playerId, vertex);
        }
    }
}