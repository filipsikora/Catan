using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;

        public AdapterTradeRequest(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            UI.TradeRequestPanel.gameObject.SetActive(true);

            _binder = new BinderTradeRequest(UI, EventBus, EventsHandler);
            _binder.Bind();

            var tradeOfferSnapshot = Facade.GetTradeOfferData();

            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(tradeOfferSnapshot.CanTrade);
            UI.TradeRequestPanel.Show(tradeOfferSnapshot.SellerName, tradeOfferSnapshot.BuyerName, tradeOfferSnapshot.Offered, tradeOfferSnapshot.Desired);

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
