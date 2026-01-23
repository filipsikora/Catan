using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public static class UseYearOfPlentyLogic
    {
        public static ResultYearOfPlenty Handle(GameState game, ResourceCostOrStock requested)
        {
            var result = RulesDevCards.YearOfPlentyPlayedRight(game.Bank, requested);

            if (!result.Success)
            {
                return ResultYearOfPlenty.Fail(result.Reason);
            }

            game.UseYearOfPlentyMutation(requested);

            return ResultYearOfPlenty.Ok(requested);
        }
    }
}
