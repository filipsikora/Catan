using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Unity.Data;
using Catan.Unity.Phases.Binders;
using Catan.Unity.Visuals;
using UnityEngine;

namespace Catan.Unity.Phases.Adapters
{
    public class AdapterFirstRoundsBuilding : BasePhaseAdapter
    {
        public BinderFirstRoundBuildings _binder;

        public override void OnEnter()
        {
            _binder = new BinderFirstRoundBuildings(UI, EventBus);
            _binder.Bind();

            UI.PlayerUIPanel.UpdatePlayerInfo(Manager.Game.GetCurrentPlayer());

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);
            UI.UpdateTurnCounter(Manager.Game.Turn);

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
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, playerColor, Manager.Board);

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        private void OnRoadPlaced(RoadPlacedEvent signal)
        {
            var edge = Manager.Game.Map.GetEdgeById(signal.EdgeId);
            var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(edge);
            var rotation = Manager.BoardVisuals.GetEdgeRotation(edge);
            var playerColor = RegistryPlayerColor.GetColor(Manager.Game.CurrentPlayer.ID);

            Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, playerColor, Manager.Board);

            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);
            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdatePlayerInfo(Manager.Game.CurrentPlayer);
        }

        public override void OnExit()
        {
            _binder.Unbind();

            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdateTurnCounter(Manager.Game.Turn);

            EventBus.Unsubscribe<VertexHighlightedEvent>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedEvent>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsSentEvent>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedEvent>(OnVillagePlaced);
            EventBus.Unsubscribe<RoadPlacedEvent>(OnRoadPlaced);
        }
    }
}