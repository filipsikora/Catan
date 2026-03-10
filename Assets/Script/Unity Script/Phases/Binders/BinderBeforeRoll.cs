using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Panels;
using Catan.Unity.Data;

namespace Catan.Unity.Phases.Binders
{
    public class BinderBeforeRoll : BaseBinder
    {
        public BinderBeforeRoll(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.RollDice, () =>
            {
                Bus.Publish(new RollDiceCommand());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                Bus.Publish(new ShowDevelopmentCardsCommand());
            });
        }

        public override void Unbind()
        {
            UI.MainUIPanel.Unbind(EnumMainUIButtons.RollDice);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.DevelopmentCards);
        }
    }
}