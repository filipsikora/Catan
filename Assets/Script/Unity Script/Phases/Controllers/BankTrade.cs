using Catan.Communication;
using Catan.Core;
using Catan.Communication.Signals;

namespace Catan
{
    public class BankTrade : GamePhase
    {
        private HandlerBankTrade _handler;
        private BinderBankTrade _binder;

        public override void OnEnter()
        {
            _handler = new HandlerBankTrade(Game, EventBus);
            _binder = new BinderBankTrade(UI, EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.BankTradePanel.Show();

            EventBus.Subscribe<BankTradeCompletedSignal>(OnTradeFinished);
            EventBus.Subscribe<BankTradeRatioChangedSignal>(OnRatioChanged);
        }

        private void OnRatioChanged(BankTradeRatioChangedSignal signal)
        {
            UI.BankTradePanel.UpdateTradeRatio(signal.Ratio, signal.Possible, signal.Resource);
        }

        private void OnTradeFinished(BankTradeCompletedSignal _)
        {
            Handler.TransitionTo(new NormalRound());
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            EventBus.Unsubscribe<BankTradeCompletedSignal>(OnTradeFinished);
            EventBus.Unsubscribe<BankTradeRatioChangedSignal>(OnRatioChanged);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.BankTradePanel.gameObject.SetActive(false);
        }
    }
}