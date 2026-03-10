using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderTradeRequest : BaseBinder
    {
        public BinderTradeRequest(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.AcceptTradeRequest, () =>
            {
                EventsHandler.Execute(new AcceptTradeRequestCommand());
            });

            UI.TradeRequestPanel.Bind(EnumTradeRequestUIButtons.RefuseTradeRequest, () =>
            {
                EventsHandler.Execute(new RefuseTradeRequestCommand());
            });
        }

        public override void Unbind()
        {
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.AcceptTradeRequest);
            UI.TradeRequestPanel.Unbind(EnumTradeRequestUIButtons.RefuseTradeRequest);
        }
    }
}