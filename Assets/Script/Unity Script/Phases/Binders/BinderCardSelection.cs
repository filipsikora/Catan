using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;

namespace Catan.Unity.Phases.Binders
{
    public class BinderCardSelection : BaseBinder
    {
        public BinderCardSelection(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            UI.CardSelectorPanel.Bind(EnumCardSelectorDevelopmentUIButtons.AcceptCards, () =>
            {
                Bus.Publish(new CardSelectionAcceptedCommand());
            });
        }

        public override void Unbind()
        {
            UI.CardSelectorPanel.Unbind(EnumCardSelectorDevelopmentUIButtons.AcceptCards);
        }
    }
}