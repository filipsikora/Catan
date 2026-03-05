using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Helpers;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using EventBus = Catan.Unity.Helpers.EventBus;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterCardDiscarding : BasePhaseAdapter
    {
        private BinderCardDiscarding _binder;
        private TurnDataSnapshot _turnData;

        public AdapterCardDiscarding(ManagerUI ui, EventBus bus, Facade facade, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler) { }

        public override void OnEnter()
        {
            UI.CardDiscardPanel.gameObject.SetActive(true);

            _binder = new BinderCardDiscarding(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardDiscardPanel.Show();

            EventBus.Subscribe<SelectionChangedUIEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Subscribe<PlayerSelectedToDiscardUIEvent>(OnPlayerChosen);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            _turnData = Facade.GetTurnData();
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(new ResourceCardSelectedCommand(signal.IsToggled, signal.Type));

            if (signal.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            EventBus.Publish(new ResourceCardToggledUIEvent(signal.VisualResourceCardId));
        }

        private void OnPlayerChosen(PlayerSelectedToDiscardUIEvent signal)
        {
            var currentPlayerResources = Facade.GetPlayersCards(signal.PlayerId);
            UI.CardDiscardPanel.ShowForPlayer(currentPlayerResources);
        }

        private void OnAcceptedDiscardVisibilityChanged(SelectionChangedUIEvent signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.ActionAvailable);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnData.PlayerId));

            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Unsubscribe<PlayerSelectedToDiscardUIEvent>(OnPlayerChosen);
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardDiscardPanel.gameObject.SetActive(false);
        }
    }
}