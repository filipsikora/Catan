using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class BeforeRoll : GamePhase
    {
        private HandlerBeforeRoll _handler;
        private BinderBeforeRoll _binder;

        public override void OnEnter()
        {
            _handler = new HandlerBeforeRoll(Game, Manager.EventBus);
            _binder = new BinderBeforeRoll(UI, Manager.EventBus);
            _binder.Bind();

            UI.UpdatePlayerInfo(CurrentPlayer);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            VisualsUI.ShowRollDiceUI(UI.MainUIPanel);
            UI.MainUIPanel.DevelopmentCardsButton.gameObject.SetActive(true);

            Manager.EventBus.Subscribe<DevelopmentCardsShownSignal>(OnDevelopmentCardsShown);
            Manager.EventBus.Subscribe<DiceRolledSignal>(OnDiceRolled);
        }

        private void OnDevelopmentCardsShown(DevelopmentCardsShownSignal signal)
        {
            Handler.TransitionTo(new DevelopmentCards(signal.AfterRoll));
        }

        private void OnDiceRolled(DiceRolledSignal signal)
        {
            UI.UpdateRolledDice(Game.LastRoll);

            if (Game.LastRoll == 7)
            {
                Handler.TransitionTo(new CardDiscarding());
            }

            else
            {
                Handler.TransitionTo(new NormalRound());
            }
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<DevelopmentCardsShownSignal>(OnDevelopmentCardsShown);
            Manager.EventBus.Unsubscribe<DiceRolledSignal>(OnDiceRolled);
        }
    }
}
