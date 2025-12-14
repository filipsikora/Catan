using Catan.Catan;
using Catan.Communication;
using Catan.Communication.Signals;
using Catan.Core;
using System.Diagnostics;
using System.Linq;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

namespace Catan.Core
{
    public class HandlerFirstRoundsBuilding : BaseBuildHandler
    {
        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public HandlerFirstRoundsBuilding(GameState game, EventBus bus) : base(game, bus) { }

        public void Activate()
        {
            Bus.Subscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Subscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Subscribe<RequestBuildVillageSignal>(OnVillageRequested);
            Bus.Subscribe<RequestBuildRoadSignal>(OnRoadRequested);

            Bus.Subscribe<RequestEndTurnSignal>(OnEndTurnRequested);
        }

        private void OnVertexClicked(VertexClickedSignal signal)
        {
            UnityEngine.Debug.Log($"OnVertexClicked fired: villagePlaced={villagePlaced}, vertex={signal.VertexId}");

            if (villagePlaced)
                return;

            SelectedVertexId = signal.VertexId;

            bool village = true;
            bool road = false;
            bool town = false;

            Bus.Publish(new VertexHighlightedSignal(signal.VertexId));
            Bus.Publish(new BuildOptionsShownSignal(village, road, town));
        }

        private void OnEdgeClicked(EdgeClickedSignal signal)
        {
            if (!villagePlaced)
                return;

            SelectedEdgeId = signal.EdgeId;

            bool village = false;
            bool road = true;
            bool town = false;

            Bus.Publish(new EdgeHighlightedSignal(signal.EdgeId));
            Bus.Publish(new BuildOptionsShownSignal(village, road, town));
        }

        private void OnVillageRequested(RequestBuildVillageSignal signal)
        {
            if (SelectedVertexId == null)
            {
                UnityEngine.Debug.Log("chuj");
            }   
            
            int id = SelectedVertexId.Value;
            var vertex = Game.Map.GetVertexById(id);

            bool success = Game.BuildFreeVillage(Game.CurrentPlayer, vertex);

            if (!success)
            {
                ResetSelection();
                return;
            }

            if (Game.CurrentPlayer.Points == 2)
            {
                GiveResourcesForSecondVillage(Game.CurrentPlayer, vertex);
            }

            villagePlaced = true;
            Bus.Publish(new VillagePlacedSignal(id));
            ResetSelection();
        }

        private void OnRoadRequested(RequestBuildRoadSignal signal)
        {
            int id = SelectedEdgeId.Value;
            var edge = Game.Map.GetEdgeById(id);

            if (!edge.IsNextToVertex(Game.LastPlacedVillagePosition))
            {
                Bus.Publish(new LogMessageSignal("Need to place this road next to a village placed in this turn"));
                return;
            }

            bool success = Game.BuildFreeRoad(Game.CurrentPlayer, edge);

            if (!success)
            {
                ResetSelection();
                return;
            }

            roadPlaced = true;
            Bus.Publish(new RoadPlacedSignal(id));
            ResetSelection();
        }

        private void GiveResourcesForSecondVillage(Player player, Vertex vertex)
        {
            foreach (HexTile hex in vertex.AdjacentHexTiles)
            {
                var resourceType = hex.GetResourceType();

                if (resourceType.HasValue)
                {
                    vertex.Owner?.Resources.AddCardsFromTheBank(Game, resourceType.Value, 1);
                }
            }
        }

        private void OnEndTurnRequested(RequestEndTurnSignal signal)
        {
            Game.EndTurn();

            Bus.Publish(new TurnEndedSignal());
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<VertexClickedSignal>(OnVertexClicked);
            Bus.Unsubscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Unsubscribe<RequestBuildVillageSignal>(OnVillageRequested);
            Bus.Unsubscribe<RequestBuildRoadSignal>(OnRoadRequested);

            Bus.Unsubscribe<RequestEndTurnSignal>(OnEndTurnRequested);
        }
    }
}