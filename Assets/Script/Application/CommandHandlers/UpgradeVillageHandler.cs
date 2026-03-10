using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class UpgradeVillageHandler
    {
        private GameState _game;

        public UpgradeVillageHandler(GameState game)
        {
            _game = game;
        }

        public ResultUpgradeVillage Handle(int playerId, Vertex vertex)
        {
            var player = _game.GetCurrentPlayer();

            var result = RulesBuilding.CanUpgradeVillage(player, vertex, _game);

            if (!result.Success)
            {
                return ResultUpgradeVillage.Fail(result.Reason, playerId, vertex);
            }

            _game.TownPaidAndBuiltMutation(player, vertex);

            return ResultUpgradeVillage.Ok(playerId, vertex);
        }
    }
}