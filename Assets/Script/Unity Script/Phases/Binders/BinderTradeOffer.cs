using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderTradeOffer : BaseBinder
    {
        public BinderTradeOffer(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.TradeOfferPanel.Bind(EnumTradeOfferUIButtons.CancelTradeOffer, () =>
            {
                EventsHandler.Execute(new TradeOfferCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.TradeOfferPanel.Unbind(EnumTradeOfferUIButtons.CancelTradeOffer);
        }
    }
}