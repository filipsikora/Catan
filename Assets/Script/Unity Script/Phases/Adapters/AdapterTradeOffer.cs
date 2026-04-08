using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterTradeOffer : BasePhaseAdapter
    {
        private BinderTradeOffer _binder;

        public AdapterTradeOffer(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameClient client, Guid gameId) : base(ui, bus, eventHandler, client, gameId) { }

        public override void OnEnter()
        {
            UI.TradeOfferPanel.gameObject.SetActive(true);

            _binder = new BinderTradeOffer(UI, EventBus, EventsHandler);
            _binder.Bind();

            _ = LoadData();

            VisualsUI.SetMainAndPlayerUIVisibility(false, UI.MainUIPanel, UI.PlayerUIPanel);

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

            if (signal.Location == EnumResourceCardLocation.DesiredTrade)
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { IsToggled = true, Type = signal.Type });

                UI.TradeOfferPanel.DrawVisualResourceCardInReview(signal.Type);
            }

            else
            {
                EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { IsToggled = false , Type = signal.Type });

                UI.TradeOfferPanel.DestroyVisualResourceCardInReview(signal.Type);
            }
        }

        private void OnPlayerChosen(PlayerClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.TradeOfferCanceledCommand, signal.PlayerId);
        }
        private async Task LoadData()
        {
            var snapshot = await Client.SendQuery<List<PlayerNameDto>>(GameId, EnumQueryName.NotCurrentPlayerNames);
            UI.TradeOfferPanel.Show(snapshot);
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