using Catan.Core.Conditions;
using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public static class UseMonopolyLogic
    {
        public static ResultMonopolyCard Handle(GameState game, EnumResourceTypes resource)
        {
            var player = game.GetCurrentPlayer();

            var result = ConditionsResources.ResourceExists(resource);

            if (!result.Success)
            {
                return ResultMonopolyCard.Fail(result.Reason, player.ID, resource);
            }

            var victimsIdsAndAmounts = game.UseMonopolyMutation(resource);

            return ResultMonopolyCard.Ok(player.ID, victimsIdsAndAmounts, resource);
        }
    }
}