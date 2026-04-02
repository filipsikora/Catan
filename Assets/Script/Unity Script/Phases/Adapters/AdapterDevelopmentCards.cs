using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Shared.Commands;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterDevelopmentCards : BasePhaseAdapter
    {
        private BinderDevelopmentCards _binder;

        public AdapterDevelopmentCards(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            UI.DevelopmentCardsPanel.gameObject.SetActive(true);

            _binder = new BinderDevelopmentCards(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            var currentPlayerDevCardsSnapshots = Facade.GetCurrentPlayerDevCards();

            EventBus.Subscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);

            UI.DevelopmentCardsPanel.Show(currentPlayerDevCardsSnapshots);
        }

        private void OnDevCardClicked(DevelopmentCardClickedUIEvent signal)
        {
            EventsHandler.Execute(new DevelopmentCardClickedPlayed(signal.CardId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            UI.DevelopmentCardsPanel.gameObject.SetActive(false);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Unsubscribe<DevelopmentCardClickedUIEvent>(OnDevCardClicked);
        }
    }
}