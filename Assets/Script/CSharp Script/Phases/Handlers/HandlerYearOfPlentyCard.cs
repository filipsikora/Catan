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
            EnumResourceTypes type = signal.Card.LinkedCard.Type;

            if (signal.IsLeftClick)
            { 
                _cardsDesired.AddSingleType(type, 1);
            }

            if (!signal.IsLeftClick)
            {
                _cardsDesired.SubtractSingleType(type, 1);
            }

            bool canAccept = _cardsDesired.ResourceDictionary.Values.Sum() == 2;
            Bus.Publish(new ReviewDesiredCardsChangedSignal(_cardsDesired, canAccept));
        }

        private void OnResourceSelected(CardSelectionAcceptedSignal signal)
        {
            Game.OnYearOfPlentyUsed(Game.CurrentPlayer, _cardsDesired);
            Bus.Publish(new YearOfPlentyFinishedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<ResourceCardClickedSignal>(OnResourceCardClicked);

            Bus.Unsubscribe<CardSelectionAcceptedSignal>(OnResourceSelected);
        }
    }
}