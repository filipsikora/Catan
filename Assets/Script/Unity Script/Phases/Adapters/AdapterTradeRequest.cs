using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Adapters;
using Catan.Unity.Phases.Binders;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Controllers
{
    public class AdapterTradeRequest : BasePhaseAdapter
    {
        private BinderTradeRequest _binder;

        public AdapterTradeRequest(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            _binder = new BinderTradeRequest(UI, EventBus, EventsHandler);
            _binder.Bind();

            _ = LoadData();
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<TradeOfferedDto>(EnumQueryName.TradeOfferData);

            if (snapshot.BuyerId == GameCache.MyPlayer.PlayerId)
            {
                UI.TradeRequestPanel.Show(snapshot.SellerName, snapshot.BuyerName, snapshot.Offered, snapshot.Desired, snapshot.CanTrade);
            }

            else if (snapshot.SellerId == GameCache.MyPlayer.PlayerId)
            {
                UI.AwaitingPanel.Show();
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.TradeRequestPanel.gameObject.SetActive(false);
            UI.AwaitingPanel.Hide();
        }
    }
}