using EventBus = Catan.Unity.Helpers.EventBus;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Catan.Unity.Panels;
using Catan.Unity.Data;
using Catan.Shared.Data;
using Catan.Unity.Helpers;
using System.Threading.Tasks;
using Catan.Shared.Dtos;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterBankTrade : BasePhaseAdapter
    {
        private BinderBankTrade _binder;

        public AdapterBankTrade(ManagerUI ui, EventBus bus, HandlerEvents eventHandler) : base(ui,bus, eventHandler) { }

        public override void OnEnter()
        {
            UI.BankTradePanel.gameObject.SetActive(true);

            _binder = new BinderBankTrade(UI, EventBus, EventsHandler);
            _binder.Bind();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

            EventBus.Subscribe<BankTradeRatioChangedUIEvent>(OnRatioChanged);
            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            _ = LoadData();
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
                EventsHandler.Execute(EnumCommandType.BankTradeOfferedResourceSelectedCommand, new { type = signal.Type });

                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, EnumResourceCardVisualState.Highlighted));
            }

            else
            {
                EventsHandler.Execute(EnumCommandType.BankTradeDesiredResourceSelectedCommand, new { type = signal.Type });
            }
        }

        private async Task LoadData()
        {
            var snapshot = await EventsHandler.Query<ResourcesAvailabilityDto>(EnumQueryName.ResourcesAvailability);
            UI.BankTradePanel.Show(snapshot);
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