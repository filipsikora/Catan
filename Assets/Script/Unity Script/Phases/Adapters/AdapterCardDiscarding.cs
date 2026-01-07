using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterCardDiscarding : BasePhaseAdapter
    {
        private BinderCardDiscarding _binder;

        public override void OnEnter()
        {
            UI.CardDiscardPanel.gameObject.SetActive(true);

            _binder = new BinderCardDiscarding(UI, Manager.EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardDiscardPanel.Show();

            Manager.EventBus.Subscribe<SelectionChangedEvent>(OnAcceptedDiscardVisibilityChanged);
            Manager.EventBus.Subscribe<PlayerSelectedToDiscardEvent>(OnPlayerChosen);

            Manager.EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            Manager.EventBus.Publish(new RequestCardDiscardingStartCommand());
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            var card = Manager.ControllerResourceCardsUI.GetVisualResourceCardById(signal.VisualResourceCardId);

            if (card == null)
                return;

            EventBus.Publish(new ResourceCardSelectedCommand(card.IsToggled, card.Type));

            if (card.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            card.ToggleCard();
        }

        private void OnPlayerChosen(PlayerSelectedToDiscardEvent signal)
        {
            var currentPlayerResources = Manager.PlayersQueryService.GetPlayersCards(signal.PlayerId);
            UI.CardDiscardPanel.ShowForPlayer(currentPlayerResources);
        }

        private void OnAcceptedDiscardVisibilityChanged(SelectionChangedEvent signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.ActionAvailable);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<SelectionChangedEvent>(OnAcceptedDiscardVisibilityChanged);
            Manager.EventBus.Unsubscribe<PlayerSelectedToDiscardEvent>(OnPlayerChosen);

            Manager.EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardDiscardPanel.gameObject.SetActive(false);
        }
    }
}