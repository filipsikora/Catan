using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using EventBus = Catan.Shared.Communication.EventBus;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterCardDiscarding : BasePhaseAdapter
    {
        private BinderCardDiscarding _binder;
        private TurnDataSnapshot _turnData;

        public AdapterCardDiscarding(ManagerUI ui, EventBus bus, Facade facade) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            UI.CardDiscardPanel.gameObject.SetActive(true);

            _binder = new BinderCardDiscarding(UI, EventBus);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardDiscardPanel.Show();

            EventBus.Subscribe<SelectionChangedEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Subscribe<PlayerSelectedToDiscardEvent>(OnPlayerChosen);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            EventBus.Publish(new RequestCardDiscardingStartCommand());

            _turnData = Facade.GetTurnData();
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventBus.Publish(new ResourceCardSelectedCommand(signal.IsToggled, signal.Type));

            if (signal.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            EventBus.Publish(new ResourceCardToggledUICommand(signal.VisualResourceCardId));
        }

        private void OnPlayerChosen(PlayerSelectedToDiscardEvent signal)
        {
            var currentPlayerResources = Facade.GetPlayersCards(signal.PlayerId);
            UI.CardDiscardPanel.ShowForPlayer(currentPlayerResources);
        }

        private void OnAcceptedDiscardVisibilityChanged(SelectionChangedEvent signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.ActionAvailable);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnData.PlayerId));

            EventBus.Unsubscribe<SelectionChangedEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Unsubscribe<PlayerSelectedToDiscardEvent>(OnPlayerChosen);
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardDiscardPanel.gameObject.SetActive(false);
        }
    }
}