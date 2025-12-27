using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderTradeRequest : BaseBinder
    {
        public BinderTradeRequest(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.AcceptTradeRequest, () =>
            {
                Bus.Publish(new TradeRequestAcceptedCommand());
            });

            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.RefuseTradeRequest, () =>
            {
                Bus.Publish(new TradeRequestRefusedCommand());
            });
        }

        public override void Unbind()
        {
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.AcceptTradeRequest);
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.RefuseTradeRequest);
        }
    }
}