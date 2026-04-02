using Catan.Unity.Helpers;
using Catan.Shared.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderDevelopmentCards : BaseBinder
    {
        public BinderDevelopmentCards(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.DevelopmentCardsPanel.Bind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, () =>
            {
                EventsHandler.Execute(new DevelopmentCardsCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.DevelopmentCardsPanel.Unbind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards);
        }
    }
}