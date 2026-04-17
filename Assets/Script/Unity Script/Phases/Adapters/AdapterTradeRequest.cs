using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;

        public AdapterTradeRequest(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void OnEnter()
        {
            UI.TradeRequestPanel.gameObject.SetActive(true);

            _binder = new BinderTradeRequest(UI, EventBus, EventsHandler);
            _binder.Bind();

            _ = LoadData();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<TradeOfferedDto>(EnumQueryName.TradeOfferData);

            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(snapshot.CanTrade);
            UI.TradeRequestPanel.Show(snapshot.SellerName, snapshot.BuyerName, Mappers.MapStringResourcesToEnumInDictionary<int>(snapshot.Offered), Mappers.MapStringResourcesToEnumInDictionary<int>(snapshot.Desired));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.TradeRequestPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
        }
    }
}
