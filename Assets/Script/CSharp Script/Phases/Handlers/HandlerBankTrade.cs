using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;


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
            var cardVisual = signal.Card;
            var cardModel = cardVisual.LinkedCard;
            var type = cardModel.Type;
            var player = Game.CurrentPlayer;

            if (cardModel.Location == EnumResourceCardLocation.OfferedTrade)
            {
                _chosenResource = type;
                _ratio = Game.FindTradeRatio(type);

                int amount = player.Resources.ResourceDictionary[type];
                bool possible = amount >= _ratio;

                Bus.Publish(new BankTradeRatioChangedSignal(_ratio, possible, _chosenResource));

                return;
            }

            else if (cardModel.Location == EnumResourceCardLocation.DesiredTrade)
            {
                if (_chosenResource == null) 
                    return;

                var give = _chosenResource.Value;

                if (player.Resources.ResourceDictionary[give] < _ratio)
                    return;

                player.Resources.SubtractSingleType(give, _ratio);
                Game.Bank.AddSingleType(give, _ratio);

                player.Resources.AddSingleType(type, 1);
                Game.Bank.SubtractSingleType(type, 1);

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