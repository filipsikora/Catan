using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class PrepareTradeOfferLogic : BaseLogic
    {
        public PrepareTradeOfferLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(ResourceCostOrStock offered)
        {
            var player = Session.GetCurrentPlayer();
            var result = RulesTrade.CanDraftTrade(player, offered);

            if (!result.Success)
            {
                return ResultCondition.Fail(result.Reason);
            }

            Session.CreateTradeDraftContext(offered);

            return ResultCondition.Ok();
        }
    }
}
