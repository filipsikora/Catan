using Catan.Application.Controllers;
using Catan.Unity.Helpers;
using Catan.Shared.Commands;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterTradeOffer : BasePhaseAdapter
    {
        private BinderTradeOffer _binder;

        public AdapterTradeOffer(ManagerUI ui, EventBus bus, HandlerEvents eventsHandler) : base(ui, bus, eventsHandler) { }

        public override void OnEnter()
        {
            UI.TradeOfferPanel.gameObject.SetActive(true);

            _binder = new BinderTradeOffer(UI, EventBus, EventsHandler);
            _binder.Bind();

            var potentialPartnersData = Facade.GetNotCurrentPlayersNames();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.Show(potentialPartnersData);

            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(false);

            EventBus.Subscribe<DesiredCardsChangedUIEvent>(OnDesiredCardsChanged);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            EventBus.Subscribe<PlayerClickedUIEvent>(OnPlayerChosen);
        }

        private void OnDesiredCardsChanged(DesiredCardsChangedUIEvent signal)
        {
            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(signal.HasDesired);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            if (signal.Location == Shared.Data.EnumResourceCardLocation.DesiredTrade)
            {
                EventsHandler.Execute(new ResourceCardSelectedCommand(true, signal.Type));

                UI.TradeOfferPanel.DrawVisualResourceCardInReview(signal.Type);
            }

            else
            {
                EventsHandler.Execute(new ResourceCardSelectedCommand(false, signal.Type));

                UI.TradeOfferPanel.DestroyVisualResourceCardInReview(signal.Type);
            }
        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(new TradePartnerChosenCommand(signal.PlayerId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<DesiredCardsChangedUIEvent>(OnDesiredCardsChanged);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            EventBus.Unsubscribe<PlayerClickedUIEvent>(OnPlayerChosen);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.gameObject.SetActive(false);
        }
    }
}