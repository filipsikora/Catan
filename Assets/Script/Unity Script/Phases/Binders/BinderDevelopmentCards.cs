using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using UnityEngine;

namespace Catan.Unity.Phases.Binders
{
    public class BinderDevelopmentCards : BaseBinder
    {
        public BinderDevelopmentCards(ManagerUI ui, EventBus bus) : base(ui, bus)
        {
            Debug.Log("Dev Cards Binder constructor");
        }

        public override void Bind()
        {
            Debug.Log("DevCards Binded");

            UI.DevelopmentCardsPanel.Bind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards, () =>
            {
                Debug.Log("DevCards Binded");
                Bus.Publish(new DevelopmentCardsCanceledCommand());
            });
        }

        public override void Unbind()
        {
            UI.DevelopmentCardsPanel.Unbind(EnumDevelopmentCardsUIButtons.CancelDevelopmentCards);
        }
    }
}