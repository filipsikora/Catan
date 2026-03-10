using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class ReactToTradeLogic : BaseLogic
    {
        public ReactToTradeLogic(GameSession session) : base(session) { }

        public ResultPlayerTrade Handle()
        {
            var (exists, context) = Session.TryGetPlayerTradeContext();

            if (!exists)
                return ResultPlayerTrade.Fail(ConditionFailureReason.DoesNotExist, default, default);

            var seller = Session.GetPlayerById(context.SellerId);
            var buyer = Session.GetPlayerById(context.BuyerId);

            var result = RulesTrade.CanAcceptTrade(seller, buyer, context.Offered, context.Desired, context);

            if (!result.Success)
                return ResultPlayerTrade.Fail(result.Reason, context.SellerId, context.BuyerId);

            Session.PlayerTradeDoneMutation(seller, buyer, context.Offered, context.Desired);

            return ResultPlayerTrade.Ok(context.SellerId, context.BuyerId, context.Offered, context.Desired, EnumGamePhases.NormalRound);
        }
    }
}