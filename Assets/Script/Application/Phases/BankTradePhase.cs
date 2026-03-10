using Catan.Application.Controllers;
using Catan.Application.UIMessages;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public sealed class BankTradePhase : BasePhase
    {
        private EnumResourceTypes? _offered;

        public BankTradePhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case BankTradeOfferedResourceSelected c:
                    return HandleOfferedResourceSelected(c);

                case BankTradeCanceledCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case BankTradeDesiredResourceSelected c:
                    return HandleBankTrade(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleOfferedResourceSelected(BankTradeOfferedResourceSelected signal)
        {
            _offered = signal.Type;
            var ratio = Facade.GetCurrentPlayerTradeRatio(signal.Type);

            int amount = Facade.GetCurrentPlayerResourceAmount(signal.Type);
            bool possibleForPlayer = Facade.PlayerHasEnoughResources(amount, ratio);

            return GameResult.Ok().AddUIMessage(new BankTradeRatioChangedMessage(ratio, possibleForPlayer, _offered));
        }

        private GameResult HandleBankTrade(BankTradeDesiredResourceSelected signal)
        {
            var desired = signal.Type;

            if (_offered == null || desired == null)
                return GameResult.Fail();

            var result = Facade.UseBankTrade(_offered.Value, desired.Value);
            
            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.PlayerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, $"player{result.PlayerId} trade {result.Ratio} {result.Offered} for 1 {result.Desired}"));
        }
    }
}