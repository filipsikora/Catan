using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderTradeOffer : BaseBinder
    {
        public BinderTradeOffer(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.TradeOfferPanel.Bind(EnumTradeOfferUIButtons.CancelTradeOffer, () =>
            {
                Bus.Publish(new TradeOfferCanceledSignal());
            });
        }

        public override void Unbind()
        {
            UI.TradeOfferPanel.Unbind(EnumTradeOfferUIButtons.CancelTradeOffer);
        }
    }
}