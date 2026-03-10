using Catan.Application.Snapshots;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;
        private TradeOfferedSnapshot _tradeOfferSnapshot;

        public override void OnEnter()
        {
            UI.TradeRequestPanel.gameObject.SetActive(true);

            _binder = new BinderTradeRequest(UI, Manager.EventBus);
            _binder.Bind();

            _tradeOfferSnapshot = ManagerGame.Instance.TradeQueryService.GetTradeOfferData();

            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(_tradeOfferSnapshot.CanTrade);
            UI.TradeRequestPanel.Show(_tradeOfferSnapshot.SellerName, _tradeOfferSnapshot.BuyerName, _tradeOfferSnapshot.Offered, _tradeOfferSnapshot.Desired);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.TradeRequestPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
        }
    }
}
