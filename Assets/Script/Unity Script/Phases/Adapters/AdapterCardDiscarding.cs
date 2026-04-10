using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System.Threading.Tasks;
using EventBus = Catan.Unity.Helpers.EventBus;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterCardDiscarding : BasePhaseAdapter
    {
        private BinderCardDiscarding _binder;

        public AdapterCardDiscarding(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui, bus, eventHandler) { }

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
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { signal.IsToggled, signal.Type });

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
            _ = LoadData(signal.PlayerId);
        }

        private void OnAcceptedDiscardVisibilityChanged(SelectionChangedUIEvent signal)
        {
            UI.CardDiscardPanel.ConfirmDiscardButton.gameObject.SetActive(signal.ActionAvailable);
        }
       
        private async Task LoadData(int playerId)
        {
            var snapshot = await EventsHandler.Query<PlayerCardsDto>(EnumQueryName.PlayerCards, playerId);
            UI.CardDiscardPanel.ShowForPlayer(snapshot);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnAcceptedDiscardVisibilityChanged);
            EventBus.Unsubscribe<PlayerSelectedToDiscardUIEvent>(OnPlayerChosen);
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.CardDiscardPanel.gameObject.SetActive(false);
        }
    }
}