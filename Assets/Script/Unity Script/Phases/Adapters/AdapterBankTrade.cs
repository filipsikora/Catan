using EventBus = Catan.Unity.Helpers.EventBus;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Communication.InternalUICommands;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Application.Controllers;
using Catan.Unity.Panels;
using Catan.Unity.Data;
using Catan.Shared.Data;
using Catan.Unity.Helpers;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBankTrade : BasePhaseAdapter
    {
        private BinderBankTrade _binder;

        public AdapterBankTrade(ManagerUI ui, Facade facade, EventBus bus, HandlerEvents eventHandler) : base(ui, facade, bus, eventHandler) { }

        public override void OnEnter()
        {
            _binder = new BinderBankTrade(UI, EventBus, EventHandler);
            _binder.Bind();

            UI.BankTradePanel.gameObject.SetActive(true);
            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<BankTradeRatioChangedUIEvent>(OnRatioChanged);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            var resourcesAvailabilitySnapshot = Facade.GetResourcesAvailability();

            UI.BankTradePanel.Show(resourcesAvailabilitySnapshot);
        }

        private void OnRatioChanged(BankTradeRatioChangedUIEvent signal)
        {
            UI.BankTradePanel.UpdateTradeRatio(signal.Ratio, signal.PossibleForPlayer, signal.Resource);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            if (signal.Location == EnumResourceCardLocation.OfferedTrade)
            {
                EventHandler.Execute(new BankTradeOfferedResourceSelected(signal.Type));
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Highlighted));
            }

            else
            {
                EventHandler.Execute(new BankTradeDesiredResourceSelected(signal.Type));
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<BankTradeRatioChangedUIEvent>(OnRatioChanged);
            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.BankTradePanel.gameObject.SetActive(false);
        }
    }
}