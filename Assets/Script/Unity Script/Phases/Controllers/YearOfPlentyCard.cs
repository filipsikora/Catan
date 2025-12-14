using Catan.Catan;
using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class YearOfPlentyCard : GamePhase
    {
        HandlerYearOfPlentyCard _handler;
        BinderCardSelection _binder;    

        bool yearOfPlenty = true;

        public override void OnEnter()
        {
            _handler = new HandlerYearOfPlentyCard(Game, EventBus);
            _binder = new BinderCardSelection(UI, EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show("Choose two resources to get for free", yearOfPlenty);

            EventBus.Subscribe<ReviewDesiredCardsChangedSignal>(OnDesiredCardsChanged);

            EventBus.Subscribe <YearOfPlentyFinishedSignal>(OnYearOfPlentyFinished);
        }

        private void OnDesiredCardsChanged(ReviewDesiredCardsChangedSignal signal)
        {
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.HasDesired);

            foreach (var entry in Game.Bank.ResourceDictionary)
            {
                EnumResourceTypes type = entry.Key;
                bool available = Game.Bank.ResourceDictionary[type] > signal.CardsDesired.ResourceDictionary[type];

                UI.CardSelectorPanel.SetCardAvailability(type, available);
            }
        }

        private void OnYearOfPlentyFinished(YearOfPlentyFinishedSignal signal)
        {
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);
            UI.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);

            Handler.TransitionTo(new NormalRound());
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            EventBus.Unsubscribe<ReviewDesiredCardsChangedSignal>(OnDesiredCardsChanged);

            EventBus.Unsubscribe<YearOfPlentyFinishedSignal>(OnYearOfPlentyFinished);
        }
    }
}
