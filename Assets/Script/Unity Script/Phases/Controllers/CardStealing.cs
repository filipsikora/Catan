using Catan.Catan;
using Catan.Communication.Signals;
using Catan.Core;
using System.Linq;
using UnityEngine;

namespace Catan
{
    public class CardStealing : GamePhase
    {
        private HandlerCardStealing _handler;

        private readonly Player _victim;
        private bool _afterRoll;

        public CardStealing(Player victim, bool afterRoll)
        {
            _victim = victim;
            _afterRoll = afterRoll;
        }

        public override void OnEnter()
        {
            _handler = new HandlerCardStealing(Game, Manager.EventBus, _victim);

            if (_victim.Resources.ResourceDictionary.Values.Sum() == 0)
            {
                Debug.Log("Nothing to steal from this player");

                OnStealingFinished();
                return;
            }

            Manager.EventBus.Subscribe<StealingFinishedSignal>(OnStealingFinished);

            UI.CardTheftPanel.Show(_victim);
        }

        private void OnStealingFinished()
        {
            OnStealingFinished(null);
        }

        private void OnStealingFinished(StealingFinishedSignal _)
        {
            UI.UpdatePlayerInfo(CurrentPlayer);
            UI.UpdatePlayerInfo(_victim);

            Handler.TransitionTo(_afterRoll ? new NormalRound() : new BeforeRoll());
        }

        public override void OnExit()
        {
            _handler.Dispose();

            Manager.EventBus.Unsubscribe<StealingFinishedSignal>(OnStealingFinished);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardTheftPanel.gameObject.SetActive(false);
        }
    }
}
