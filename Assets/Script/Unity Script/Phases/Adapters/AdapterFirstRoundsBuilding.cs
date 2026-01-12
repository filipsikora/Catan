using Catan.Application.Snapshots;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Unity.Communication.InternalUIEvents;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using Core.Unity.Communication.InternalUIEvents;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;
        private TurnDataSnapshot turnDataSnapshot;

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus);
            _binder.Bind();

            turnDataSnapshot = Manager.TurnsQueryService.GetTurnData();

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);
            UI.UpdateTurnCounter(turnDataSnapshot.TurnNumber);

            EventBus.Subscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Subscribe<RoadPlacedEvent>(OnRoadPlaced);
        }

        private void OnVertexClicked(VertexHighlightedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var vertexObj = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Manager.BoardVisuals.SetVertexVisual(vertexObj, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsSentEvent signal)
        {
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnVillagePlaced(VillagePlacedEvent signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Publish(new VillagePlacedUIEvent(signal.VertexId, turnDataSnapshot.PlayerId));
            EventBus.Publish(new PlayerStateChangedUIEvent(turnDataSnapshot.PlayerId));
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);
            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Publish(new PlayerStateChangedUIEvent(turnDataSnapshot.PlayerId));
            EventBus.Publish(new RoadPlacedUIEvent(signal.EdgeId, turnDataSnapshot.PlayerId));
        }

        public override void OnExit()
        {
            _binder.Unbind();

            Manager.BoardVisuals.ResetMarkedPositions();

            EventBus.Unsubscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);
        }
    }
}