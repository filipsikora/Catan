using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderCardSelection : BaseBinder
    {
        public BinderCardSelection(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.CardSelectorPanel.Bind(EnumCardSelectorDevelopmentUIButtons.AcceptCards, () =>
            {
                EventsHandler.Execute(new CardSelectionAcceptedCommand());
            });
        }

        public override void Unbind()
        {
            UI.CardSelectorPanel.Unbind(EnumCardSelectorDevelopmentUIButtons.AcceptCards);
        }
    }
}