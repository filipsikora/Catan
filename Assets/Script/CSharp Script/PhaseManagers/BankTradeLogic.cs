using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public static class BankTradeLogic
    {
        public static ResultBankTrade Handle(GameState game, EnumResourceTypes offered, EnumResourceTypes desired)
        {
            var player = game.GetCurrentPlayer();
            var ratio = game.GetTradeRatio(offered);

            var result = RulesTrade.CanTradeWithBank(player, game.Bank, offered, desired, ratio);
            
            if (!result.Success)
            {
                return ResultBankTrade.Fail(player.ID, result.Reason);
            }

            game.BankTradeDoneMutation(player, offered, desired, ratio);

            return ResultBankTrade.Ok(player.ID, offered, desired, ratio);
        }
    }
}