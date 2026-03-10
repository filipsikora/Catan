using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Panels;
using Catan.Unity.Data;

namespace Catan.Unity.Phases.Binders
{
    public class BinderBankTrade : BaseBinder
    {
        public BinderBankTrade(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.BankTradePanel.Bind(EnumBankTradeUIButtons.CancelBankTrade, () =>
            {
                Bus.Publish(new BankTradeCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.BankTradePanel.Unbind(EnumBankTradeUIButtons.CancelBankTrade);
        }
    }
}