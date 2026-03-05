using Catan.Application.Controllers;
using Catan.Core.Snapshots;
using Catan.Unity.Helpers;
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

        public AdapterFirstRoundsBuilding(ManagerUI ui, EventBus bus, Facade facade, VisualsBoard board, HandlerEvents eventsHandler) : base(ui, bus, facade, eventsHandler)
        {
            _board = board;
        }

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus, EventsHandler);
            _binder.Bind();

            _turnDataSnapshot = Facade.GetTurnData();

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);

            UI.UpdateTurnCounter(_turnDataSnapshot.TurnNumber);

            EventBus.Subscribe<VertexHighlightedUIEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedUIEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedUIEvent>(OnRoadPlaced);

            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnVertexClicked(VertexHighlightedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var vertexObj = _board.GetVertexObject(signal.VertexId);
            _board.SetVertexVisual(vertexObj, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());

            var edgeObj = _board.GetEdgeObject(signal.EdgeId);
            _board.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentUIEvent signal)
        {
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnVillagePlaced(VillagePlacedUIEvent signal)
        {
            EventBus.Publish(new PositionsResetUIEvent());
            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, _turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(_turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedUIEvent signal)
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

            EventBus.Unsubscribe<VertexHighlightedUIEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedUIEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentUIEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedUIEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedUIEvent>(OnRoadPlaced);
        }
    }
}