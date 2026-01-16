using Catan.Application.Snapshots;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterYearOfPlentyCard : BasePhaseAdapter
    {
        BinderCardSelection _binder;
        TurnDataSnapshot _turnData;

        public override void OnEnter()
        {
            UI.CardSelectorPanel.gameObject.SetActive(true);

            _binder = new BinderCardSelection(UI, EventBus);
            _binder.Bind();

            _turnData = Manager.TurnsQueryService.GetTurnData();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.Show("Choose two resources to get for free");

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Subscribe<SelectionChangedEvent>(OnDesiredCardsChanged);
        }

        private void OnDesiredCardsChanged(SelectionChangedEvent signal)
        {
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(signal.ActionAvailable);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (signal.IsLeftClicked)
            {
                EventBus.Publish(new ResourceCardSelectedCommand(true, signal.Type));
            }

            else
            {
                EventBus.Publish(new ResourceCardSelectedCommand(false, signal.Type));
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.CardSelectorPanel.AcceptCardsButton.gameObject.SetActive(false);
            UI.CardSelectorPanel.gameObject.SetActive(false);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnData.PlayerId));

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
            EventBus.Unsubscribe<SelectionChangedEvent>(OnDesiredCardsChanged);
        }
    }
}
