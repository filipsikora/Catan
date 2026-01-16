using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Application.CommandHandlers
{
    public sealed class UseYearOfPlentyHandler
    {
        private GameState _game;

        public UseYearOfPlentyHandler(GameState game)
        {
            _game = game;
        }

        public ResultYearOfPlenty Handle(ResourceCostOrStock requested)
        {
            var result = RulesDevCards.YearOfPlentyPlayedRight(_game.Bank, requested);

            if (!result.Success)
            {
                return ResultYearOfPlenty.Fail(result.Reason);
            }

            _game.UseYearOfPlentyMutation(requested);

            return ResultYearOfPlenty.Ok(requested);
        }
    }
}
