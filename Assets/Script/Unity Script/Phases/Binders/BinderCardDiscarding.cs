using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderCardDiscarding : BaseBinder
    {
        public BinderCardDiscarding(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.CardDiscardPanel.Bind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, () =>
            {
                Bus.Publish(new DiscardingAcceptedSignal());
            });
        }

        public override void Unbind()
        {
            UI.CardDiscardPanel.Unbind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard);
        }
    }
}