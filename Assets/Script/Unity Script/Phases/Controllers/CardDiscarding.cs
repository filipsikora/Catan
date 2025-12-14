using Catan.Catan;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Catan.Core;
using Catan.Communication.Signals;

namespace Catan
{
    public class CardDiscarding : GamePhase
    {
        private HandlerCardDiscarding _handler;
        private BinderCardDiscarding _binder;

        private bool _afterRoll = true;

        public override void OnEnter()
        {
            _handler = new HandlerCardDiscarding(Game, Manager.EventBus);
            _binder = new BinderCardDiscarding(UI, Manager.EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardDiscardPanel.Show();

            _binder.Bind();

            Manager.EventBus.Subscribe<AllDiscardingCompleteSignal>(OnAllDone);
            Manager.EventBus.Subscribe<AcceptedDiscardVisibilitySignal>(OnAcceptedDiscardVisibilityChanged);
            Manager.EventBus.Subscribe<DiscardShownForPlayerSignal>(OnPlayerChosen);

            _handler.ProceedToNextPlayer();
        }

        private void OnPlayerChosen(DiscardShownForPlayerSignal signal)
        {
            var player = Game.PlayerList.Find(p => p.ID == signal.PlayerId);
            UI.CardDiscardPanel.ShowForPlayer(player);
        }

        private void OnAcceptedDiscardVisibilityChanged(AcceptedDiscardVisibilitySignal signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.CanDiscard);
        }

        private void OnAllDone(AllDiscardingCompleteSignal signal)
        {
            Handler.TransitionTo(new RobberPlacing(_afterRoll));
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<AllDiscardingCompleteSignal>(OnAllDone);
            Manager.EventBus.Unsubscribe<AcceptedDiscardVisibilitySignal>(OnAcceptedDiscardVisibilityChanged);
            Manager.EventBus.Unsubscribe<DiscardShownForPlayerSignal>(OnPlayerChosen);

            UI.CardDiscardPanel.gameObject.SetActive(false);
        }
    }
}