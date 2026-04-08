using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;

        public AdapterTradeRequest(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameClient client, Guid gameId) : base(ui, bus, eventHandler, client, gameId) { }

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
            var snapshot = await Client.SendQuery<TradeOfferedDto>(GameId, EnumQueryName.TradeOfferData);

            UI.TradeRequestPanel.AcceptTradeButton.gameObject.SetActive(snapshot.CanTrade);
            UI.TradeRequestPanel.Show(snapshot.SellerName, snapshot.BuyerName, snapshot.Offered, snapshot.Desired);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.TradeRequestPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
        }
    }
}
