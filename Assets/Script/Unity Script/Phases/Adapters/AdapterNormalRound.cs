using Catan.Shared.Data;
using Catan.Unity.Data;
using Catan.Unity.Helpers;
using Catan.Unity.InternalUIEvents;
using Catan.Unity.Networking;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using System;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterNormalRound : BasePhaseAdapter
    {
        private BinderNormalRound _binder;

        public AdapterNormalRound(ManagerUI ui, EventBus bus, HandlerEvents eventHandler, GameClient client, Guid gameId) : base(ui, bus, eventHandler, client, gameId) { }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus, EventsHandler);
            _binder.Bind();            

            EventBus.Subscribe<SelectionChangedUIEvent>(OnTradePossible);

            EventBus.Subscribe<VertexClickedUIEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.HideTradeOfferButton();

            VisualsUI.ResetResourceCardsInParent(UI.PlayerUIPanel.ResourceCardsPanel);
            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);
        }

        private void OnVertexClicked(VertexClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.VertexClickedCommand, signal.VertexId);
        }

        private void OnEdgeClicked(EdgeClickedUIEvent signal)
        {
            EventsHandler.Execute(EnumCommandType.EdgeClickedCommand, signal.EdgeId);
        }

        private void OnTradePossible(SelectionChangedUIEvent signal)
        {
            if (signal.ActionAvailable)
            {
                UI.ShowTradeOfferButton();
            }
            else
            {
                UI.HideTradeOfferButton();
            }
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(EnumCommandType.ResourceCardSelectedCommand, new { IsToggled = !signal.IsToggled, signal.Type });

            if (signal.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUIEvent(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            EventBus.Publish(new ResourceCardToggledUIEvent(signal.VisualResourceCardId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PositionsResetUIEvent());

            UI.HideTradeOfferButton();

            EventBus.Unsubscribe<SelectionChangedUIEvent>(OnTradePossible);

            EventBus.Unsubscribe<VertexClickedUIEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeClickedUIEvent>(OnEdgeClicked);

            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }
    }
}