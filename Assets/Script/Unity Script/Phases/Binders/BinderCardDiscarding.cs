using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using UnityEngine;

namespace Catan.Unity.Phases.Binders
{
    public class BinderCardDiscarding : BaseBinder
    {
        public BinderCardDiscarding(ManagerUI ui, EventBus bus) : base(ui, bus) { }

        public override void Bind()
        {
            Debug.Log("chuj bind");

            UI.CardDiscardPanel.Bind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard, () =>
            {
                Debug.Log("chuj zbindowane");
                Bus.Publish(new DiscardingAcceptedCommand());
            });
        }

        public override void Unbind()
        {
            UI.CardDiscardPanel.Unbind(EnumCardSelectorDiscardUIButtons.ConfirmDiscard);
        }
    }
}