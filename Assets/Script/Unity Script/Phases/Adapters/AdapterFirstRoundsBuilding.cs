using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Panels;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;
        private TurnDataSnapshot _turnDataSnapshot;
        private VisualsBoard _board;

        public AdapterFirstRoundsBuilding(ManagerUI ui, EventBus bus, Facade facade, VisualsBoard board) : base(ui, bus, facade)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

            EventBus.Subscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnVertexClicked(VertexHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var vertexObj = _board.GetVertexObject(signal.VertexId);
            _board.SetVertexVisual(vertexObj, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObj = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentEvent signal)
        {
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnVillagePlaced(VillagePlacedEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);

            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, _turnDataSnapshot.PlayerId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            EventBus.Publish(new PositionsResetUIEvent());

            EventBus.Unsubscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);
        }
    }
}