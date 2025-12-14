using Catan.Communication.Signals;
using Catan.Core;
using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class FirstRoundsBuilding : GamePhase
    {
        public HandlerFirstRoundsBuilding _handler;
        public BinderFirstRoundBuildings _binder;

        public FirstRoundsBuilding() { }

        public override void OnEnter()
        {
            _handler = new HandlerFirstRoundsBuilding(Game, EventBus);
            _binder = new BinderFirstRoundBuildings(UI, EventBus);

            _handler.Activate();
            _binder.Bind();

            UI.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);

            VisualsUI.MakeAllChildrenVisible(UI.MainUIPanel.ButtonsContainer, false);
            UI.UpdateTurnCounter(Game.Turn);

            EventBus.Subscribe<VertexHighlightedSignal>(OnVertexClicked);
            EventBus.Subscribe<EdgeHighlightedSignal>(OnEdgeClicked);
            EventBus.Subscribe<BuildOptionsShownSignal>(OnPositionClicked);

            EventBus.Subscribe<VillagePlacedSignal>(OnVillageBought);
            EventBus.Subscribe<RoadPlacedSignal>(OnRoadBought);

            EventBus.Subscribe<TurnEndedSignal>(OnNextTurnClicked);
        }

        private void OnVertexClicked(VertexHighlightedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var vertexObj = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Manager.BoardVisuals.SetVertexVisual(vertexObj, Color.yellow);
        }

        private void OnEdgeClicked(EdgeHighlightedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(signal.EdgeId);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);
        }

        private void OnPositionClicked(BuildOptionsShownSignal signal)
        {
            UI.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(signal.CanBuildVillage);
            UI.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(signal.CanBuildRoad);
        }

        private void OnVillageBought(VillagePlacedSignal signal)
        {
            var vertexObject = Manager.BoardVisuals.GetVertexObject(signal.VertexId);
            Vector3 pos = vertexObject.transform.position;

            var villageObject = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, CurrentPlayer.PlayerColor, Manager.Board);

            UI.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
        }

        private void OnRoadBought(RoadPlacedSignal signal)
        {
            Edge edge = Game.Map.GetEdgeById(signal.EdgeId);
            var (start, end, mid) = Manager.BoardVisuals.GetEdgePositions(edge);
            Quaternion rotation = Manager.BoardVisuals.GetEdgeRotation(edge);

            var roadObject = Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, CurrentPlayer.PlayerColor, Manager.Board);

            UI.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
            UI.MainUIPanel.NextTurnButton.gameObject.SetActive(true);
        }

        private void OnNextTurnClicked(TurnEndedSignal signal)
        {
            Manager.BoardVisuals.ResetMarkedPositions();
            UI.UpdateTurnCounter(Game.Turn);

            if (Game.FirstRoundsIndices.Count != 0)
            {
                Handler.TransitionTo(new FirstRoundsBuilding());
            }

            else
            {
                Handler.TransitionTo(new BeforeRoll());
            }
        }

        public override void OnExit()
        {
            _handler.Dispose();
            _binder.Unbind();

            EventBus.Unsubscribe<VertexHighlightedSignal>(OnVertexClicked);
            EventBus.Unsubscribe<EdgeHighlightedSignal>(OnEdgeClicked);
            EventBus.Unsubscribe<BuildOptionsShownSignal>(OnPositionClicked);

            EventBus.Unsubscribe<VillagePlacedSignal>(OnVillageBought);
            EventBus.Unsubscribe<RoadPlacedSignal>(OnRoadBought);

            EventBus.Unsubscribe<TurnEndedSignal>(OnNextTurnClicked);
        }
    }
}