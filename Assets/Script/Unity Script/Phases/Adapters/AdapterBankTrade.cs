using EventBus = Catan.Shared.Communication.EventBus;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Application.Controllers;
using Catan.Unity.Panels;
using Catan.Unity.Data;
using Catan.Shared.Data;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBankTrade : BasePhaseAdapter
    {
        private BinderBankTrade _binder;

        public AdapterBankTrade(ManagerUI ui, EventBus bus, Facade facade) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            _binder = new BinderBankTrade(UI, EventBus);
            _binder.Bind();

            UI.BankTradePanel.gameObject.SetActive(true);
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<BankTradeRatioChangedEvent>(OnRatioChanged);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            var resourcesAvailabilitySnapshot = Facade.GetResourcesAvailability();

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

            if (signal.Location == EnumResourceCardLocation.OfferedTrade)
            {
                EventBus.Publish(new BankTradeOfferedResourceSelected(signal.Type));
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Highlighted));
            }

            else
            {
                EventBus.Publish(new BankTradeDesiredResourceSelected(signal.Type));
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