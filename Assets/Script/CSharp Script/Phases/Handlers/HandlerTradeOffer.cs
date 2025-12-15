using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;


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
            if (!signal.IsLeftClicked)
                return;

            if (signal.Location == EnumResourceCardLocation.DesiredTrade)
            {
                _cardsDesired.AddSingleType(signal.Type, 1);
            }

            if (signal.Location == EnumResourceCardLocation.ReviewTrade)
            {
                _cardsDesired.SubtractSingleType(signal.Type, 1);
            }

            bool hasDesired = _cardsDesired.ResourceDictionary.Values.Sum() > 0;

            Bus.Publish(new ReviewDesiredCardsChangedSignal(signal.Type, signal.Location, hasDesired));
        }

        public ResourceCostOrStock GetDesiredCards()
        {
            return _cardsDesired.Clone();
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);
        }
    }
}
