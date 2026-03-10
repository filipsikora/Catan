using Catan.Shared.Communication.Commands;
using Catan.Application.Controllers;
using Catan.Shared.Data;
using Catan.Application.UIMessages;

namespace Catan.Application.Phases
{
    public class TradeRequestPhase : BasePhase
    {
        public TradeRequestPhase(Facade facade) : base(facade) { }

        public override GameResult Handle(object command)
        {
            switch (command)
            {
                case RefuseTradeRequestCommand c:
                    return GameResult.Ok(EnumGamePhases.NormalRound);

                case AcceptTradeRequestCommand c:
                    return HandleTradeAccepted(c);

                default:
                    return GameResult.Fail();
            }
        }

        private GameResult HandleTradeAccepted(AcceptTradeRequestCommand signal)
        {
            var result = Facade.UseReactToTrade();

            if (!result.Success)
            {
                return GameResult.Fail().AddUIMessage(new ActionRejectedMessage(result.BuyerId, result.Reason));
            }

            return GameResult.Ok(result.NextPhase).AddUIMessage(new LogMessageMessage(EnumLogTypes.Info, "Trade accepted"));
        }
    }
}