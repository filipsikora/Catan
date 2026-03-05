using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
using Catan.Shared.Communication.Commands;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Data;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterNormalRound : BasePhaseAdapter
    {
        private BinderNormalRound _binder;
        private TurnDataSnapshot _turnDataSnapshot;

        private VisualsBoard _board;

        public AdapterNormalRound(ManagerUI ui, EventBus bus, Facade facade, VisualsBoard board, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus, EventsHandler);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();            

            EventBus.Subscribe<SelectionChangedUIEvent>(OnTradePossible);

            EventBus.Subscribe<VertexHighlightedUIEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedUIEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);
            EventBus.Subscribe<TownPlacedUIEvent>(OnTownPlaced);

            EventBus.Subscribe<DevelopmentCardBoughtUIEvent>(OnDevelopmentCardBought);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

            UI.HideTradeOfferButton();
            VisualsUI.ResetResourceCardsInParent(UI.PlayerUIPanel.ResourceCardsPanel);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);
            UI.MainUIPanel.UpdateRolledDice(_turnDataSnapshot.RolledNumber);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
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

        private void OnVertexClicked(VertexHighlightedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var vertexObject = _board.GetVertexObject(signal.VertexId);
            _board.SetVertexVisual(vertexObject, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObject = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObject, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnVillagePlaced(VillagePlacedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnTownPlaced(TownPlacedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnDevelopmentCardBought(DevelopmentCardBoughtUIEvent signal)
        {
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventsHandler.Execute(new ResourceCardSelectedCommand(!signal.IsToggled, signal.Type));

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

            EventBus.Unsubscribe<VertexHighlightedUIEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedUIEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedUIEvent>(OnRoadPlaced);
            EventBus.Unsubscribe<TownPlacedUIEvent>(OnTownPlaced);

            EventBus.Unsubscribe<DevelopmentCardBoughtUIEvent>(OnDevelopmentCardBought);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }
    }
}