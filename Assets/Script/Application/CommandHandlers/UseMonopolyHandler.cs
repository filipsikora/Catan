using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Application.CommandHandlers
{
    public sealed class UseMonopolyHandler
    {
        private GameState _game;

        public UseMonopolyHandler(GameState game)
        {
            _game = game;
        }

        public ResultMonopolyCard Handle(EnumResourceTypes resource)
        {
            var player = _game.GetCurrentPlayer();

            var result = ConditionsResources.ResourceExists(resource);

            if (!result.Success)
            {
                return ResultMonopolyCard.Fail(result.Reason, player.ID, resource);
            }

            var victimsIdsAndAmounts = _game.UseMonopolyMutation(resource);

            return ResultMonopolyCard.Ok(player.ID, victimsIdsAndAmounts, resource);
        }
    }
}