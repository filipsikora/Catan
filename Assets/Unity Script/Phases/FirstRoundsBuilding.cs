using NUnit.Framework;
using UnityEngine;

namespace Catan
{
    public class FirstRoundsBuilding : GamePhase
    {
        private Vertex selectedVertex;

        private Edge selectedEdge;

        public bool villagePlaced = false;

        public bool roadPlaced = false;


        public override void OnEnter()
        {
            Manager.PlayerUIPanel.UpdatePlayerInfo(Game.currentPlayer);

            Manager.MainUIPanel.HideLaterBuildings();
            Manager.MainUIPanel.HideStartingBuildings();
            Manager.MainUIPanel.UpdateTurnCounter(Game.Turn);
            Manager.MainUIPanel.TurnCounterText.gameObject.SetActive(true);
        }

        public override void OnVertexClicked(Vertex vertex)
        {
            OnPositionClickedCommon();

            vertex.IsMarked = true;
            selectedVertex = vertex;

            var vertexObj = Manager.BoardVisuals.GetVertexObject(vertex.Id);
            Manager.BoardVisuals.SetVertexVisual(vertexObj, Color.yellow);

            if (!villagePlaced)
            {
                Manager.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(true);
            }

            Debug.Log($"{vertex.Owner}, {vertex.HasVillage}");
        }

        public override void OnEdgeClicked(Edge edge)
        {
            OnPositionClickedCommon();

            edge.IsMarked = true;
            selectedEdge = edge;

            var edgeObj = Manager.BoardVisuals.GetEdgeObject(edge.Id);
            Manager.BoardVisuals.SetEdgeVisual(edgeObj, Color.yellow);

            if (!roadPlaced)
            {
                Manager.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(true);
            }

            Debug.Log($"{edge}");
        }

        public override void OnBuildFreeVillageClicked()
        {
            bool success = Game.BuildFreeVillage(CurrentPlayer, selectedVertex);

            if (success)
            {
                villagePlaced = true;
                var vertexObj = Manager.BoardVisuals.GetVertexObject(selectedVertex.Id);
                Vector3 pos = vertexObj.transform.position;

                var villageObj = Manager.BoardVisuals.PlaceObject(Manager.CubeVillagePrefab, pos, null, Game.currentPlayer.PlayerColor, Manager.Board);

                selectedVertex.VillageObject = villageObj;

                Manager.MainUIPanel.BuildFreeVillageButton.gameObject.SetActive(false);
                Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
            }
        }

        public override void OnBuildFreeRoadClicked()
        {

            if (!selectedEdge.IsNextToVertex(Game.lastPlacedVillagePosition))
            {
                UnityEngine.Debug.Log("Need to place this road next to a village placed in this turn");
                return;
            }

            bool success = Game.BuildFreeRoad(CurrentPlayer, selectedEdge);

            if (success)
            {
                roadPlaced = true;
                var (_, _, mid) = Manager.BoardVisuals.GetEdgePositions(selectedEdge);
                Quaternion rotation = Manager.BoardVisuals.GetEdgeRotation(selectedEdge);

                var roadObj = Manager.BoardVisuals.PlaceObject(Manager.CubeRoadPrefab, mid, rotation, CurrentPlayer.PlayerColor, Manager.Board);

                selectedEdge.RoadObject = roadObj;

                Manager.MainUIPanel.BuildFreeRoadButton.gameObject.SetActive(false);
                Manager.MainUIPanel.NextTurnButton.gameObject.SetActive(true);
                Manager.PlayerUIPanel.UpdatePlayerInfo(CurrentPlayer);
            }

        }

        public override void OnNextTurnClicked()
        {
            roadPlaced = false;
            villagePlaced = false;

            OnNextTurnClickedCommon();
        }

    }
}