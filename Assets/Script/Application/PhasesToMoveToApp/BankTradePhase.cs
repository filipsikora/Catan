using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public sealed class BankTradePhase : BasePhase
    {
        private EnumResourceTypes? _offered;

        public BankTradePhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter() { }

        public override void Handle(object command)
        {
            switch (command)
            {
                case BankTradeOfferedResourceSelected c:
                    HandleOfferedResourceSelected(c);
                    break;

                case BankTradeCanceledCommand c:
                    FinishPhase();
                    break;

                case BankTradeDesiredResourceSelected c:
                    HandleBankTrade(c);
                    break;
            }
        }

        private void HandleOfferedResourceSelected(BankTradeOfferedResourceSelected signal)
        {
            _offered = signal.Type;
            var ratio = Facade.GetTradeRatioForCurrentPlayer(signal.Type);

            int amount = Facade.GetCurrentPlayerResourceAmount(signal.Type);
            bool possibleForPlayer = amount >= ratio;

            Bus.Publish(new BankTradeRatioChangedEvent(ratio, possibleForPlayer, _offered));
        }

        private void HandleBankTrade(BankTradeDesiredResourceSelected signal)
        {
            var desired = signal.Type;

            if (_offered == null || desired == null)
                return;

            var result = Facade.BankTrade(_offered.Value, desired.Value);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(Facade.GetCurrentPlayerId(), result.Reason));
                FinishPhase();
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"player{Facade.GetCurrentPlayerId()} trade {result.Ratio} {result.Offered} for 1 {result.Desired}"));

            FinishPhase();
        }

        private void FinishPhase()
        {
            PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
        }
    }
}