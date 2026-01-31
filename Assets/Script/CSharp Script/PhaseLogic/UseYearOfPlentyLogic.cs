using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class UseYearOfPlentyLogic : BaseLogic
    {
        public UseYearOfPlentyLogic(GameSession session) : base(session) { }

        public ResultYearOfPlenty Handle(ResourceCostOrStock requested)
        {
            var result = RulesDevCards.YearOfPlentyPlayedRight(Session.GetBank(), requested);

            if (!result.Success)
            {
                return ResultYearOfPlenty.Fail(result.Reason);
            }

            Session.UseYearOfPlentyMutation(requested);

            return ResultYearOfPlenty.Ok(requested);
        }
    }
}