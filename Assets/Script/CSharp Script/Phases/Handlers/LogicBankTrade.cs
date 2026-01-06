using Catan.Application.CommandHandlers;
using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Core.Phases.Handlers
{
    public sealed class LogicBankTrade : BasePhaseLogic
    {
        private EnumResourceTypes? _offered;
        private int _ratio;

        private readonly PerformBankTradeHandler _handler;

        public LogicBankTrade(GameState game, EventBus bus) : base(game, bus)
        {
            _handler = new PerformBankTradeHandler(game);
        }

        public override void Enter() { }

        public override void Exit()
        {
            _offered = null;
            _ratio = 0;
        }

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
            _ratio = Game.FindTradeRatio(signal.Type);

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

            var result = _handler.Handle(_offered.Value, desired.Value);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.FailureReason.Value));
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"player{player.ID} trade {result.Ratio} {result.Offered} for 1 {result.Desired}"));

            FinishPhase();
        }

        private void FinishPhase()
        {
            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}