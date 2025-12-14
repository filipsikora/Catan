using Catan.Catan;
using Catan.Communication;
using Catan.Core;
using UnityEngine;
using Catan.Communication.Signals;
using System;


namespace Catan.Core
{
    public class HandlerRoadBuilding : BaseBuildHandler
    {
        private int RoadsBuilt = 0;
        private int RoadsToBuild = 2;
        private int RoadsLeft = 0;

        ResourceCostOrStock RoadCost = BuildingDataRegistry.Cost[typeof(BuildingRoad)];

        private Edge _selectedEdge;

        public HandlerRoadBuilding(GameState game, EventBus bus) : base(game, bus)
        {
            Bus.Subscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Subscribe<RequestBuildRoadSignal>(OnBuildRoadRequested);

            RoadsLeft = Game.CurrentPlayer.BuildingCount<BuildingRoad>();
            RoadsToBuild = Math.Min(RoadsToBuild, RoadsLeft);

            for (int i = 0; i < RoadsToBuild; i++)
            {
                Game.CurrentPlayer.Resources.AddCards(RoadCost);
            }
        }

        private void OnEdgeClicked(EdgeClickedSignal signal)
        {
            SelectedEdgeId = signal.EdgeId;
            _selectedEdge = Game.Map.GetEdgeById(signal.EdgeId);

            (bool village, bool road, bool town) = Game.CheckBuildOptions(_selectedEdge);

            Bus.Publish(new EdgeHighlightedSignal(signal.EdgeId));
            Bus.Publish(new BuildOptionsShownSignal(village, road, town));
        }

        private void OnBuildRoadRequested(RequestBuildRoadSignal signal)
        {
            bool roadBuilt = Game.BuildRoad(Game.CurrentPlayer, _selectedEdge);
            int id = SelectedEdgeId.Value;

            if (!roadBuilt)
            {
                _selectedEdge = null;

                ResetSelection();
                return;
            }

            Bus.Publish(new RoadPlacedSignal(id));
            ResetSelection();

            Game.Bank.SubtractCards(RoadCost);

            RoadsToBuild--;
            RoadsBuilt++;

            if (RoadsToBuild == 0)
            {
                Bus.Publish(new RoadBuildingFinishedSignal());
            }
        }

        public override void Dispose()
        {
            Bus.Unsubscribe<EdgeClickedSignal>(OnEdgeClicked);

            Bus.Unsubscribe<RequestBuildRoadSignal>(OnBuildRoadRequested);
        }
    }
}
