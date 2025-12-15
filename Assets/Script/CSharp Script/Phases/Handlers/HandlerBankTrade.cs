using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using NUnit.Framework;


namespace Catan.Core
{
    public class HandlerBankTrade : BaseHandler
    {
        private EnumResourceTypes? _chosenResource;
        private int _ratio;

        public HandlerBankTrade(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnCardClicked);
            Bus.Subscribe<BankTradeCanceledSignal>(OnBankTradeCanceled);
        }

        private void OnCardClicked(ResourceCardClickedSignal signal)
        {
            var player = Game.CurrentPlayer;

            if (signal.Location == EnumResourceCardLocation.OfferedTrade)
            {                
                if (_chosenResource != signal.Type)
                {
                    Bus.Publish(new MultipleResourceCardVisualStateChangedResetSignal(EnumResourceCardLocation.OfferedTrade));
                    Bus.Publish(new ResourceCardVisualStateChangedSignal(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Highlighted));
                }

                _chosenResource = signal.Type;
                _ratio = Game.FindTradeRatio(signal.Type);

                int amount = player.Resources.ResourceDictionary[signal.Type];
                bool possible = amount >= _ratio;

                Bus.Publish(new BankTradeRatioChangedSignal(_ratio, possible, _chosenResource));

                return;
            }

            else if (signal.Location == EnumResourceCardLocation.DesiredTrade)
            {
                if (_chosenResource == null) 
                    return;

                var give = _chosenResource.Value;

                if (player.Resources.ResourceDictionary[give] < _ratio)
                    return;

                player.Resources.SubtractSingleType(give, _ratio);
                Game.Bank.AddSingleType(give, _ratio);

                player.Resources.AddSingleType(signal.Type, 1);
                Game.Bank.SubtractSingleType(signal.Type, 1);

                Bus.Publish(new BankTradeCompletedSignal());
            }
        }

        private void OnBankTradeCanceled(BankTradeCanceledSignal _)
        {
            Bus.Publish(new BankTradeCompletedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnCardClicked);
            Bus.Unsubscribe<BankTradeCanceledSignal>(OnBankTradeCanceled);
        }
    }
}