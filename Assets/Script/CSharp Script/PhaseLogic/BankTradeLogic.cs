using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BankTradeLogic : BaseLogic
    {
        public BankTradeLogic(GameSession session) : base(session) { }

        public ResultBankTrade Handle(EnumResourceTypes offered, EnumResourceTypes desired)
        {
            var player = Session.GetCurrentPlayer();
            var ratio = Session.GetCurrentPlayerTradeRatio(offered);
            
            var result = RulesTrade.CanTradeWithBank(player, Session.GetBank(), offered, desired, ratio);
            
            if (!result.Success)
            {
                return ResultBankTrade.Fail(player.ID, result.Reason);
            }

            Session.BankTradeMutation(offered, desired, ratio);

            return ResultBankTrade.Ok(player.ID, offered, desired, ratio, EnumGamePhases.NormalRound);
        }
    }
}