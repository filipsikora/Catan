using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Unity.VisualScripting;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBankTrade : BasePhaseAdapter
    {
        private BinderBankTrade _binder;

        public override void OnEnter()
        {
            _binder = new BinderBankTrade(UI, EventBus);
            _binder.Bind();

            UI.BankTradePanel.gameObject.SetActive(true);
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<BankTradeRatioChangedEvent>(OnRatioChanged);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            var resourcesAvailabilitySnapshot = Manager.ResourcesQueryService.GetResourcesAvailability();

            UI.BankTradePanel.Show(resourcesAvailabilitySnapshot);
        }

        private void OnRatioChanged(BankTradeRatioChangedEvent signal)
        {
            UI.BankTradePanel.UpdateTradeRatio(signal.Ratio, signal.PossibleForPlayer, signal.Resource);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            var card = Manager.ControllerResourceCardsUI.GetVisualResourceCardById(signal.VisualResourceCardId);

            if (card == null)
                return;

            if (card.Location == Shared.Data.EnumResourceCardLocation.OfferedTrade)
            {
                EventBus.Publish(new BankTradeOfferedResourceSelected(card.Type));
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(card.VisualResourceCardId, card.Location, Data.EnumResourceCardVisualState.Highlighted));
            }

            else
            {
                EventBus.Publish(new BankTradeDesiredResourceSelected(card.Type));
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<BankTradeRatioChangedEvent>(OnRatioChanged);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.BankTradePanel.gameObject.SetActive(false);
        }
    }
}