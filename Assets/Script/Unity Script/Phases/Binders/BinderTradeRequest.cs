using Catan.Communication;
using Catan.Communication.Signals;
using TMPro.EditorUtilities;

namespace Catan
{
    public class BinderTradeRequest : BaseBinder
    {
        public BinderTradeRequest(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.AcceptTradeRequest, () =>
            {
                Bus.Publish(new TradeRequestAcceptedSignal());
            });

            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.RefuseTradeRequest, () =>
            {
                Bus.Publish(new TradeRequestRefusedSignal());
            });
        }

        public override void Unbind()
        {
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.AcceptTradeRequest);
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.RefuseTradeRequest);
        }
    }
}