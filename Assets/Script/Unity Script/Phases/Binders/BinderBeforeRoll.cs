using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderBeforeRoll : BaseBinder
    {
        public BinderBeforeRoll(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.MainUIPanel.Bind(EnumMainUIButtons.RollDice, () =>
            {
                Bus.Publish(new RequestDiceRollSignal());
            });

            UI.MainUIPanel.Bind(EnumMainUIButtons.DevelopmentCards, () =>
            {
                Bus.Publish(new RequestShowDevelopmentCardsSignal());
            });
        }

        public override void Unbind()
        {
            UI.MainUIPanel.Unbind(EnumMainUIButtons.RollDice);
            UI.MainUIPanel.Unbind(EnumMainUIButtons.DevelopmentCards);
        }
    }
}