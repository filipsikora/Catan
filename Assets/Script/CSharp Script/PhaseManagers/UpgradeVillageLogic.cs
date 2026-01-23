using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class UpgradeVillageLogic
    {
        public static ResultUpgradeVillage Handle(GameState game, int playerId, Vertex vertex)
        {
            var player = game.GetCurrentPlayer();

            var result = RulesBuilding.CanUpgradeVillage(player, vertex, game);

            if (!result.Success)
            {
                return ResultUpgradeVillage.Fail(result.Reason, playerId, vertex);
            }

            game.TownPaidAndBuiltMutation(player, vertex);

            return ResultUpgradeVillage.Ok(playerId, vertex);
        }
    }
}