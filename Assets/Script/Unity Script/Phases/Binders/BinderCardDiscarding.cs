using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderCardDiscarding : BaseBinder
    {
        public BinderCardDiscarding(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.CardDiscardPanel.Bind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, () =>
            {
                Bus.Publish(new DiscardingAcceptedCommand());
            });
        }

        public override void Unbind()
        {
            UI.CardDiscardPanel.Unbind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard);
        }
    }
}