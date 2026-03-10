using Catan.Shared.Communication.Commands;
using Catan.Unity.Helpers;
using Catan.Shared.Data;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Application.Controllers;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterMonopolyCard : BasePhaseAdapter
    {
        public BinderCardSelection _binder;

        public AdapterMonopolyCard(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderCardSelection(UI, EventBus, EventsHandler);

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show( "Choose resource to steal from the other players");

            _binder.Bind();

            EventBus.Subscribe<ResourceSelectedUIEvent>(OnResourceSelected);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }

        private void OnResourceSelected(ResourceSelectedUIEvent signal)
        {
            EventBus.Publish(new MultipleResourceCardVisualStateResetUIEvent(EnumResourceCardLocation.DesiredTrade));

            if (signal.Type != null)
            {
                EventBus.Publish(new ResourceCardTypeVisualStateChangedUIEvent(signal.Type, EnumResourceCardVisualState.Highlighted));
            }

            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.Selected);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(new StolenCardSelectedCommand(signal.Type));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<ResourceSelectedUIEvent>(OnResourceSelected);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);
        }
    }
}
