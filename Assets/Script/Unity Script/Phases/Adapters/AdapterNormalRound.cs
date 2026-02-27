using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUICommands;
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

        public AdapterNormalRound(ManagerUI ui, EventBus bus, Facade facade, VisualsBoard board) : base(ui, bus, facade)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            _binder = new BinderNormalRound(UI, EventBus);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();            

            EventBus.Subscribe<SelectionChangedEvent>(OnTradePossible);

            EventBus.Subscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);
            EventBus.Subscribe<TownPlacedEvent>(OnTownPlaced);

            EventBus.Subscribe<DevelopmentCardBoughtEvent>(OnDevelopmentCardBought);

            EventBus.Subscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);

            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

            UI.HideTradeOfferButton();
            VisualsUI.ResetResourceCardsInParent(UI.PlayerUIPanel.ResourceCardsPanel);

            VisualsUI.SetMainAndPlayerUIVisibility(true, UI.MainUIPanel, UI.PlayerUIPanel);
            VisualsUI.ShowNextTurnUI(UI.MainUIPanel);
            UI.MainUIPanel.UpdateRolledDice(_turnDataSnapshot.RolledNumber);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnTradePossible(SelectionChangedEvent signal)
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

        private void OnVertexClicked(VertexHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var vertexObject = _board.GetVertexObject(signal.VertexId);
            _board.SetVertexVisual(vertexObject, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObject = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObject, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentEvent signal)
        {
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildVillage, signal.CanBuildVillage);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.BuildRoad, signal.CanBuildRoad);
            UI.MainUIPanel.SetButtonVisibility(EnumMainUIButtons.UpgradeVillage, signal.CanUpgradeVillage);
        }

        private void OnVillagePlaced(VillagePlacedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnTownPlaced(TownPlacedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new TownPlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnDevelopmentCardBought(DevelopmentCardBoughtEvent signal)
        {
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnResourceCardClicked(ResourceCardClickedUIEvent signal)
        {
            if (!signal.IsLeftClicked)
                return;

            EventBus.Publish(new ResourceCardSelectedCommand(!signal.IsToggled, signal.Type));

            if (signal.IsToggled)
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.None));
            }

            else
            {
                EventBus.Publish(new ResourceCardVisualStateChangedUICommand(signal.VisualResourceCardId, signal.Location, Data.EnumResourceCardVisualState.Lifted));
            }

            EventBus.Publish(new ResourceCardToggledUICommand(signal.VisualResourceCardId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PositionsResetUIEvent());

            UI.HideTradeOfferButton();

            EventBus.Unsubscribe<SelectionChangedEvent>(OnTradePossible);

            EventBus.Unsubscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);
            EventBus.Unsubscribe<TownPlacedEvent>(OnTownPlaced);

            EventBus.Unsubscribe<DevelopmentCardBoughtEvent>(OnDevelopmentCardBought);

            EventBus.Unsubscribe<ResourceCardClickedUIEvent>(OnResourceCardClicked);
        }
    }
}