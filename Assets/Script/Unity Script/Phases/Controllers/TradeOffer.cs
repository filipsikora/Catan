using Catan.Catan;
using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class TradeOffer : GamePhase
    {
        private HandlerTradeOffer _handler;
        private BinderTradeOffer _binder;

        private ResourceCostOrStock _cardsOffered;
        private ResourceCostOrStock _cardsDesired = new();

        public TradeOffer(ResourceCostOrStock cardsOffered)
        {
            _cardsOffered = cardsOffered;
        }

        public override void OnEnter()
        {
            _handler = new HandlerTradeOffer(Game, Manager.EventBus);
            _binder = new BinderTradeOffer(UI, Manager.EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.Show();

            _binder.Bind();

            Manager.EventBus.Subscribe<ReviewDesiredCardsChangedSignal>(OnDesiredCardsChanged);
            Manager.EventBus.Subscribe<TradeOfferCanceledSignal>(OnTradeOfferCanceled);
            Manager.EventBus.Subscribe<TradePartnerChosenSignal>(OnTradePartnerChosen);
        }

        private void OnDesiredCardsChanged(ReviewDesiredCardsChangedSignal signal)
        {
            UI.TradeOfferPanel.OnDesiredUpdated(signal.CardsDesired);
            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(signal.HasDesired);
        }

        private void OnTradeOfferCanceled(TradeOfferCanceledSignal signal)
        {
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.gameObject.SetActive(false);

            Handler.TransitionTo(new NormalRound());
        }

        private void OnTradePartnerChosen(TradePartnerChosenSignal signal)
        {
            var partner = Game.PlayerList.First(p => p.ID == signal.PlayerId);

            UI.TradeOfferPanel.gameObject.SetActive(false);

            Handler.TransitionTo(new TradeRequest(partner, _cardsOffered, _cardsDesired));
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<ReviewDesiredCardsChangedSignal>(OnDesiredCardsChanged);
            Manager.EventBus.Unsubscribe<TradeOfferCanceledSignal>(OnTradeOfferCanceled);
            Manager.EventBus.Unsubscribe<TradePartnerChosenSignal>(OnTradePartnerChosen);
        }
    }
}