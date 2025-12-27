using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderTradeOffer : BaseBinder
    {
        public BinderTradeOffer(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.TradeOfferPanel.Bind(EnumTradeOfferUIButtons.CancelTradeOffer, () =>
            {
                Bus.Publish(new TradeOfferCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.TradeOfferPanel.Unbind(EnumTradeOfferUIButtons.CancelTradeOffer);
        }
    }
}