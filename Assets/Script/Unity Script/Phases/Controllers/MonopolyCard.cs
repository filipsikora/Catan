using Catan.Catan;
using Catan.Communication.Signals;
using Catan.Core;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class MonopolyCard : GamePhase
    {
        public HandlerMonopolyCard _handler;
        public BinderCardSelection _binder;

        bool yearOfPlenty = false;

        public override void OnEnter()
        {
            _handler = new HandlerMonopolyCard(Game, EventBus);
            _binder = new BinderCardSelection(UI, EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show("Choose resource to steal from the other players", yearOfPlenty);

            _binder.Bind();

            EventBus.Subscribe<ResourceSelectedSignal>(OnResourceSelected);

            EventBus.Subscribe<MonopolyCardFinishedSignal>(OnMonopolyCardFinished);
        }

        private void OnResourceSelected(ResourceSelectedSignal signal)
        { 
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.Selected);
        }

        private void OnMonopolyCardFinished(MonopolyCardFinishedSignal signal)
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

            EventBus.Unsubscribe<ResourceSelectedSignal>(OnResourceSelected);

            EventBus.Unsubscribe<MonopolyCardFinishedSignal>(OnMonopolyCardFinished);
        }
    }
}
