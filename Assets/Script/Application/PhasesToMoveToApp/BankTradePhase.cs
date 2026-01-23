using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using Catan.Core.PhaseLogic;

namespace Catan.Application.Phases
{
    public sealed class BankTradePhase : BasePhase
    {
        private EnumResourceTypes? _offered;
        private int _ratio;

        public BankTradePhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

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
            var player = Game.GetCurrentPlayer();

            _offered = signal.Type;
            _ratio = Game.GetTradeRatio(signal.Type);

            int amount = player.Resources.ResourceDictionary[signal.Type];
            bool possibleForPlayer = amount >= _ratio;

            Bus.Publish(new BankTradeRatioChangedEvent(_ratio, possibleForPlayer, _offered));
        }

        private void HandleBankTrade(BankTradeDesiredResourceSelected signal)
        {
            var player = Game.GetCurrentPlayer();
            var desired = signal.Type;

            if (_offered == null || desired == null)
                return;

            var result = BankTradeLogic.Handle(Game, _offered.Value, desired.Value);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
                FinishPhase();
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"player{player.ID} trade {result.Ratio} {result.Offered} for 1 {result.Desired}"));

            FinishPhase();
        }

        private void FinishPhase()
        {
            PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
        }
    }
}