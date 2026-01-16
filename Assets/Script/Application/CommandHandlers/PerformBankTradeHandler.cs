using Catan.Core.Engine;
using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Application.CommandHandlers
{
    public class PerformBankTradeHandler
    {
        private readonly GameState _game;

        public PerformBankTradeHandler(GameState game)
        {
            _game = game;
        }

        public ResultBankTrade Handle(EnumResourceTypes offered, EnumResourceTypes desired)
        {
            var player = _game.GetCurrentPlayer();
            var ratio = _game.GetTradeRatio(offered);

            var result = RulesTrade.CanTradeWithBank(player, _game.Bank, offered, desired, ratio);

            if (!result.Success)
            {
                return ResultBankTrade.Fail(player.ID, result.Reason);
            }

            _game.BankTradeDoneMutation(player, offered, desired, ratio);

            return ResultBankTrade.Ok(player.ID, offered, desired, ratio);
        }
    }
}