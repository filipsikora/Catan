using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Data;
using Catan.Shared.Data;

namespace Catan.Unity.Phases.Binders
{
    public class BinderBeforeRoll : BaseBinder
    {
        public BinderBeforeRoll(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.RollDice, () =>
            {
                EventsHandler.Execute(EnumCommandType.RollDiceCommand);
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                EventsHandler.Execute(EnumCommandType.ShowDevelopmentCardsCommand);
            });
        }

        public override void Unbind()
        {
            UI.MainUIPanel.Unbind(EnumMainUIButtons.RollDice);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.DevelopmentCards);
        }
    }
}