using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using System.Diagnostics;
using System.Reflection;
using UnityEngine;

namespace Catan
{
    public class DevelopmentCards : GamePhase
    {
        private HandlerDevelopmentCards _handler;
        private BinderDevelopmentCards _binder;

        private bool _afterRoll;

        public DevelopmentCards(bool afterRoll)  
        {
            _afterRoll = afterRoll;
        }

        public override void OnEnter()
        {
            _handler = new HandlerDevelopmentCards(Game, Manager.EventBus);
            _binder = new BinderDevelopmentCards(UI, Manager.EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.DevelopmentCardsPanel.Show(CurrentPlayer, _afterRoll);

            _binder.Bind();

            Manager.EventBus.Subscribe<DevelopmentCardsCanceledSignal>(OnDevelopmentCardsCanceled);

            Manager.EventBus.Subscribe<KnightCardUsedSignal>(OnKnightCardUsed);

            Manager.EventBus.Subscribe<VictoryPointUsedSignal>(OnVictoryPointUsed);

            Manager.EventBus.Subscribe<MonopolyCardUsedSignal>(OnMonopolyCardUsed);
            Manager.EventBus.Subscribe<YearOfPlentyUsedSignal>(OnYearOfPlentyCardUsed);
            Manager.EventBus.Subscribe<RoadBuildingCardUsedSignal>(OnRoadBuildingCardUsed);
        }
        
        private void OnVictoryPointUsed(VictoryPointUsedSignal signal)
        {
            Handler.TransitionTo(new NormalRound());
        }

        private void OnDevelopmentCardsCanceled(DevelopmentCardsCanceledSignal signal)
        {
            UnityEngine.Debug.Log("chuj");
            UnityEngine.Debug.Log($"{_afterRoll}");

            Handler.TransitionTo(_afterRoll ? new NormalRound() : new BeforeRoll());
        }

        private void OnKnightCardUsed(KnightCardUsedSignal signal)
        {
            Handler.TransitionTo(new RobberPlacing(_afterRoll));
        }

        private void OnMonopolyCardUsed(MonopolyCardUsedSignal signal)
        {
            Handler.TransitionTo(new MonopolyCard());
        }

        private void OnYearOfPlentyCardUsed(YearOfPlentyUsedSignal signal)
        {
            Handler.TransitionTo(new YearOfPlentyCard());
        }

        private void OnRoadBuildingCardUsed(RoadBuildingCardUsedSignal signal)
        {
            Handler.TransitionTo(new RoadBuilding());
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<DevelopmentCardsCanceledSignal>(OnDevelopmentCardsCanceled);

            Manager.EventBus.Unsubscribe<KnightCardUsedSignal>(OnKnightCardUsed);

            Manager.EventBus.Unsubscribe<VictoryPointUsedSignal>(OnVictoryPointUsed);

            Manager.EventBus.Unsubscribe<MonopolyCardUsedSignal>(OnMonopolyCardUsed);
            Manager.EventBus.Unsubscribe<YearOfPlentyUsedSignal>(OnYearOfPlentyCardUsed);
            Manager.EventBus.Unsubscribe<RoadBuildingCardUsedSignal>(OnRoadBuildingCardUsed);

            UI.DevelopmentCardsPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
        }
    }
}