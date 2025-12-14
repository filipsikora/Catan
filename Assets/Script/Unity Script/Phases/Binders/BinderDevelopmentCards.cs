using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderDevelopmentCards : BaseBinder
    {
        public BinderDevelopmentCards(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.DevelopmentCardsPanel.Bind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, () =>
            {
                Bus.Publish(new DevelopmentCardsCanceledSignal());
            });
        }

        public override void Unbind()
        {
            UI.DevelopmentCardsPanel.Unbind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards);
        }
    }
}