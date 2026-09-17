using Catan.Shared.Data;
using Catan.Unity.Caches;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterTradeOffer : BasePhaseAdapter
    {
        private BinderTradeOffer _binder;

        public AdapterTradeOffer(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameCache gameCache) : base(ui, bus, eventHandler, gameCache) { }

        public override void OnEnter()
        {
            _binder = new BinderTradeOffer(UI, EventBus, EventsHandler);
            _binder.Bind();

            EventBus.Subscribe<DesiredCardsChangedUIEvent>(OnDesiredCardsChanged);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            EventBus.Subscribe<PlayerClickedUIEvent>(OnPlayerChosen);

            if (GameCache.GameFlow.PlayersToMove.Contains(GameCache.MyPlayer.PlayerId))
            {
                UI.TradeOfferPanel.Show(GameCache.OtherPlayers, EventBus);
            }
        }

        private void OnDesiredCardsChanged(DesiredCardsChangedUIEvent signal)
        {
            UI.TradeOfferPanel.PlayersButtonsContainer.gameObject.SetActive(signal.HasDesired);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            if (signal.Location == EnumResourceCardLocation.DesiredTrade)
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { isSelected = true, type = signal.Type });

                UI.TradeOfferPanel.DrawVisualResourceCardInReview(signal.Type);
            }

            else
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { isSelected = false , type = signal.Type });

                UI.TradeOfferPanel.DestroyVisualResourceCardInReview(signal.Type);
            }
        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.TradePartnerChosenCommand, new { playerId = signal.PlayerId });
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Unsubscribe<DesiredCardsChangedUIEvent>(OnDesiredCardsChanged);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            EventBus.Unsubscribe<PlayerClickedUIEvent>(OnPlayerChosen);

            UI.TradeOfferPanel.gameObject.SetActive(false);
        }
    }
}