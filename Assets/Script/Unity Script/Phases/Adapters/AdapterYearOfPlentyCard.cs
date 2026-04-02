using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterYearOfPlentyCard : BasePhaseAdapter
    {
        BinderCardSelection _binder;

        public AdapterYearOfPlentyCard(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            UI.CardSelectorPanel.gameObject.SetActive(true);

            _binder = new BinderCardSelection(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show("Choose two resources to get for free");

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Subscribe<SelectionChangedUIEvent>(OnDesiredCardsChanged);
        }

        private void OnDesiredCardsChanged(SelectionChangedUIEvent signal)
        {
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.ActionAvailable);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (signal.IsLeftClicked)
            {
                EventsHandler.Execute(new ResourceCardSelectedCommand(true, signal.Type));
            }

            else
            {
                EventsHandler.Execute(new ResourceCardSelectedCommand(false, signal.Type));
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnDesiredCardsChanged);
        }
    }
}
