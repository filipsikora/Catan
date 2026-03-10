using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Panels;
using Catan.Unity.Data;

namespace Catan.Unity.Phases.Binders
{
    public class BinderBankTrade : BaseBinder
    {
        public BinderBankTrade(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

        public override void Bind()
        {
            UI.BankTradePanel.Bind(EnumBankTradeUIButtons.CancelBankTrade, () =>
            {
                EventsHandler.Execute(new BankTradeCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.BankTradePanel.Unbind(EnumBankTradeUIButtons.CancelBankTrade);
        }
    }
}