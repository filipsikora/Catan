using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;

        public override void OnEnter()
        {
            UI.TradeRequestPanel.gameObject.SetActive(true);

            _binder = new BinderTradeRequest(UI, Manager.EventBus);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            _binder.Bind();

            Manager.EventBus.Subscribe<TradeRequestSentEvent>(OnTradeRequestSent);

            Manager.EventBus.Publish(new RequestTradeRequestValidatedCommand());
        }

        private void OnTradeRequestSent(TradeRequestSentEvent signal)
        {
            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(signal.CanTrade);
            var playerOffered = ManagerGame.Instance.Game.GetPlayerById(signal.PlayerId);
            var playerOffering = ManagerGame.Instance.Game.GetCurrentPlayer();
            UI.TradeRequestPanel.Show(playerOffered, playerOffering, signal.CardsOffered, signal.CardsDesired);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeRequestPanel.gameObject.SetActive(false);

            UI.UpdatePlayerInfo(ManagerGame.Instance.Game.CurrentPlayer);

            Manager.EventBus.Unsubscribe<TradeRequestSentEvent>(OnTradeRequestSent);
        }
    }
}
