using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderBankTrade : BaseBinder
    {
        public BinderBankTrade(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.BankTradePanel.Bind(EnumBankTradeUIButtons.CancelBankTrade, () =>
            {
                Bus.Publish(new BankTradeCanceledSignal());
            });
        }

        public override void Unbind()
        {
            UI.BankTradePanel.Unbind(EnumBankTradeUIButtons.CancelBankTrade);
        }
    }
}