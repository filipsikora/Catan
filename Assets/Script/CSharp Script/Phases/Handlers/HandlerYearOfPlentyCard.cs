using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using System.Linq;


namespace Catan.Core
{
    public class HandlerYearOfPlentyCard : BaseHandler
    {
        private ResourceCostOrStock _cardsDesired = new ResourceCostOrStock();

        public HandlerYearOfPlentyCard(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<ResourceCardClickedSignal>(OnResourceCardClicked);

            Bus.Subscribe<CardSelectionAcceptedSignal>(OnResourceSelected);
        }

        private void OnResourceCardClicked(ResourceCardClickedSignal signal)
        {
            EnumResourceTypes type = signal.Type;

            if (!signal.IsLeftClicked)
            {
                if (_cardsDesired.ResourceDictionary[type] > 0)
                {
                    _cardsDesired.SubtractSingleType(type, 1);
                }
            }

            if (signal.IsLeftClicked)
            {
                _cardsDesired.AddSingleType(type, 1);
            }

            bool canAccept = _cardsDesired.ResourceDictionary.Values.Sum() == 2;    
            Bus.Publish(new ReviewDesiredCardsChangedSignal(type, signal.Location, canAccept));
        }

        private void OnResourceSelected(CardSelectionAcceptedSignal signal)
        {
            Game.OnYearOfPlentyUsed(Game.CurrentPlayer, _cardsDesired);
            Bus.Publish(new YearOfPlentyFinishedSignal());
        }

        public ResourceCostOrStock GetDesiredCards()
        {
            return _cardsDesired;
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);

            Bus.Unsubscribe<CardSelectionAcceptedSignal>(OnResourceSelected);
        }
    }
}