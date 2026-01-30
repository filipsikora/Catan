using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BankTradeLogic
    {
        private readonly GameSession _session;

        public BankTradeLogic(GameSession session)
        {
            _session = session;
        }

        public ResultBankTrade Handle(EnumResourceTypes offered, EnumResourceTypes desired)
        {
            var player = _session.GetCurrentPlayer();
            var ratio = _session.GetTradeRatioForCurrentPlayer(offered);

            var result = RulesTrade.CanTradeWithBank(player, _session.GetBank(), offered, desired, ratio);
            
            if (!result.Success)
            {
                return ResultBankTrade.Fail(player.ID, result.Reason);
            }

            _session.BankTradeMutation(player, offered, desired, ratio);

            return ResultBankTrade.Ok(player.ID, offered, desired, ratio);
        }
    }
}