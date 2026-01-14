using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterTradeOffer : BasePhaseAdapter
    {
        private BinderTradeOffer _binder;

        public override void OnEnter()
        {
            UI.TradeOfferPanel.gameObject.SetActive(true);

            _binder = new BinderTradeOffer(UI, Manager.EventBus);
            _binder.Bind();

            var potentialPartnersData = Manager.PlayersQueryService.GetNotCurrentPlayersNames();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.Show(potentialPartnersData);

            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(false);

            Manager.EventBus.Subscribe<DesiredCardsChangedEvent>(OnDesiredCardsChanged);

            Manager.EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }

        private void OnDesiredCardsChanged(DesiredCardsChangedEvent signal)
        {
            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(signal.HasDesired);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            var card = Manager.ControllerResourceCardsUI.GetVisualResourceCardById(signal.VisualResourceCardId);

            if (card == null)
                return;

            if (card.Location == Shared.Data.EnumResourceCardLocation.DesiredTrade)
            {
                EventBus.Publish(new ResourceCardSelectedCommand(true, card.Type));

                UI.TradeOfferPanel.DrawVisualResourceCardInReview(card.Type);
            }

            else
            {
                EventBus.Publish(new ResourceCardSelectedCommand(false, card.Type));

                UI.TradeOfferPanel.DestroyVisualResourceCardInReview(card.Type);
            }
        }

        public override void OnExit()
        {
            _binder.Unbind();

            Manager.EventBus.Unsubscribe<DesiredCardsChangedEvent>(OnDesiredCardsChanged);

            Manager.EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            UI.TradeOfferPanel.gameObject.SetActive(false);
        }
    }
}