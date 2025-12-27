using Catan.Core.Engine;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Phases.Handlers
{
    public sealed class LogicBankTrade : BasePhaseLogic
    {
        private EnumResourceTypes? _chosenResource = null;
        private int _ratio = 0;
        private Dictionary<EnumResourceTypes, bool> resourcesAvailability = new();

        public LogicBankTrade(GameState game, EventBus bus) : base(game, bus) { }

        public override void Enter()
        {
            resourcesAvailability = Game.CheckResourcesAvailability();
        }

        public override void Exit() { }

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
                    HandleDesiredResourceSelected(c);
                    break;

                case RequestBankTradeAvailabilityCommand c:
                    HandleBankTradeAvailabilityRequested(c);
                    break;
            }
        }

        private void HandleOfferedResourceSelected(BankTradeOfferedResourceSelected signal)
        {
            var player = Game.GetCurrentPlayer();

            _chosenResource = signal.Type;
            _ratio = Game.FindTradeRatio(signal.Type);

            int amount = player.Resources.ResourceDictionary[signal.Type];
            bool possibleForPlayer = amount >= _ratio;

            Bus.Publish(new BankTradeRatioChangedEvent(_ratio, possibleForPlayer, _chosenResource));

            return;
        }

        private void HandleBankTradeAvailabilityRequested(RequestBankTradeAvailabilityCommand signal)
        {
            Bus.Publish(new ResourcesAvailabilityEvent(resourcesAvailability));
        }

        private void HandleDesiredResourceSelected(BankTradeDesiredResourceSelected signal)
        {
            var player = Game.GetCurrentPlayer();

            if (_chosenResource == null)
                return;

            var result = Game.PerformBankTrade(_chosenResource.Value, signal.Type);

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
            }

            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, $"{player.ID} traded {_ratio} {_chosenResource} for 1 {signal.Type}"));

            FinishPhase();
        }

        private void FinishPhase()
        {
            Bus.Publish(new ReturnToNormalRoundEvent());
        }
    }
}