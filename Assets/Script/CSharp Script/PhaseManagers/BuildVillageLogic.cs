using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class BuildVillageLogic
    {
        public static ResultBuildVillage Handle(GameState game, int playerId, Vertex vertex)
        {
            var player = game.GetCurrentPlayer();

            var result = RulesBuilding.CanBuildVillage(player, vertex, game);

            if (!result.Success)
            {
                return ResultBuildVillage.Fail(result.Reason, playerId, vertex);
            }

            game.VillagePaidAndBuiltMutation(player, vertex);

            return ResultBuildVillage.Ok(playerId, vertex);
        }
    }
}