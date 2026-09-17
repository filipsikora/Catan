using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using EventBus = Catan.Unity.Helpers.EventBus;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterCardDiscarding : BasePhaseAdapter
    {
        private BinderCardDiscarding _binder;

        public AdapterCardDiscarding(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            _binder = new BinderCardDiscarding(UI, EventBus, EventsHandler);
            _binder.Bind();

            EventBus.Subscribe<SelectionChangedUIEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Subscribe<PlayersToMoveChangedUIEvent>(OnPlayersToMoveChanged);

            UpdateParticipationUI();
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { isSelected = signal.IsSelected, type = signal.Type });

            if (signal.IsSelected)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            EventBus.Publish(new ResourceCardToggledUIEvent(signal.VisualResourceCardId));
        }

        private void OnAcceptedDiscardVisibilityChanged(SelectionChangedUIEvent signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.ActionAvailable);
        }

        private void OnPlayersToMoveChanged(PlayersToMoveChangedUIEvent signal)
        {
            UpdateParticipationUI();
        }

        private void UpdateParticipationUI()
        {
            bool canAct = GameCache.GameFlow.PlayersToMove.Contains(GameCache.MyPlayer.PlayerId);

            if (canAct)
            {
                UI.AwaitingPanel.Hide();
                UI.CardDiscardPanel.Show(GameCache.MyPlayer.Resources);
            }

            else
            {
                UI.AwaitingPanel.Show();
                UI.CardDiscardPanel.Hide();
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Unsubscribe<PlayersToMoveChangedUIEvent>(OnPlayersToMoveChanged);

            UI.CardDiscardPanel.Hide();
            UI.AwaitingPanel.Hide();
        }
    }
}