using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using System.Linq;


namespace Catan.Core
{
    public class HandlerTradeOffer : BaseHandler
    {
        private ResourceCostOrStock _cardsDesired = new();

        public HandlerTradeOffer(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnResourceCardClicked);
        }

        private void OnResourceCardClicked(ResourceCardClickedSignal signal)
        {
            var cardModel = signal.Card.LinkedCard;

            if (cardModel.Location == EnumResourceCardLocation.OfferedTrade)
            {
                _cardsDesired.AddSingleType(cardModel.Type, 1);
            }

            if (cardModel.Location == EnumResourceCardLocation.ReviewTrade)
            {
                _cardsDesired.SubtractSingleType(cardModel.Type, 1);
            }

            bool hasDesired = _cardsDesired.ResourceDictionary.Values.Sum() > 0;

            Bus.Publish(new ReviewDesiredCardsChangedSignal(_cardsDesired, hasDesired));
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);
        }
    }
}
