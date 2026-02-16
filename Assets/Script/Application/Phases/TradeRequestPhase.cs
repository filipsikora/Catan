using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Application.Controllers;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class TradeRequestPhase : BasePhase
    {
        public TradeRequestPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case RefuseTradeRequestCommand c:
                    PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
                    break;

                case AcceptTradeRequestCommand c:
                    HandleTradeAccepted(c);
                    break;
            }
        }

        private void HandleTradeAccepted(AcceptTradeRequestCommand signal)
        {
            var result = Facade.UseReactToTrade();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.BuyerId, result.Reason));
                return;
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Trade accepted"));

            TransitionPhase(result);
        }
    }
}