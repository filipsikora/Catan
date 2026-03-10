using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderCardDiscarding : BaseBinder
    {
        public BinderCardDiscarding(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.CardDiscardPanel.Bind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, () =>
            {
                EventsHandler.Execute(new DiscardingAcceptedCommand());
            });
        }

        public override void Unbind()
        {
            UI.CardDiscardPanel.Unbind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard);
        }
    }
}