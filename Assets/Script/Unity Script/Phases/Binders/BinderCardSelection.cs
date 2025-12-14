using Catan.Communication;
using Catan.Communication.Signals;

namespace Catan
{
    public class BinderCardSelection : BaseBinder
    {
        public BinderCardSelection(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.CardSelectorPanel.Bind(EnumCardSelectorDevelopmentUIButtons.AcceptCards, () =>
            {
                Bus.Publish(new CardSelectionAcceptedSignal());
            });
        }

        public override void Unbind()
        {
            UI.CardSelectorPanel.Unbind(EnumCardSelectorDevelopmentUIButtons.AcceptCards);
        }
    }
}