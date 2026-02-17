using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterTradeOffer : BasePhaseAdapter
    {
        private BinderTradeOffer _binder;

        public AdapterTradeOffer(ManagerUI ui, EventBus bus, Facade facade) : base(ui, bus, facade) { }

        public override void OnEnter()
        {
            UI.TradeOfferPanel.gameObject.SetActive(true);

            _binder = new BinderTradeOffer(UI, EventBus);
            _binder.Bind();

            var potentialPartnersData = Facade.GetNotCurrentPlayersNames();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.Show(potentialPartnersData);

            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(false);

            EventBus.Subscribe<DesiredCardsChangedEvent>(OnDesiredCardsChanged);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }

        private void OnDesiredCardsChanged(DesiredCardsChangedEvent signal)
        {
            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(signal.HasDesired);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            if (signal.Location == Shared.Data.EnumResourceCardLocation.DesiredTrade)
            {
                EventBus.Publish(new ResourceCardSelectedCommand(true, signal.Type));

                UI.TradeOfferPanel.DrawVisualResourceCardInReview(signal.Type);
            }

            else
            {
                EventBus.Publish(new ResourceCardSelectedCommand(false, signal.Type));

                UI.TradeOfferPanel.DestroyVisualResourceCardInReview(signal.Type);
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<DesiredCardsChangedEvent>(OnDesiredCardsChanged);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.gameObject.SetActive(false);
        }
    }
}