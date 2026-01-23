using Catan.Application.Controllers;
using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.PhaseLogic;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;
using System;

namespace Catan.Application.Phases
{
    public class RoadBuildingPhase : BaseBuildPhase
    {
        private int RoadsBuilt = 0;
        private int RoadsToBuild = 2;
        private int RoadsLeft = 0;

        private Edge _selectedEdge;

        public RoadBuildingPhase(GameState game, EventBus bus, PhaseTransitionController phaseTransition) : base(game, bus, phaseTransition) { }

        public override void Enter()
        {
            RoadsLeft = Game.CurrentPlayer.BuildingCount<BuildingRoad>();
            RoadsToBuild = Math.Min(RoadsToBuild, RoadsLeft);
        }

        public override void Handle(object command)
        {
            switch (command)
            {
                case EdgeClickedCommand c:
                    HandleEdgeClicked(c);
                    break;

                case BuildRoadCommand c:
                    HandleRoadRequested(c);
                    break;
            }
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            SelectedEdgeId = signal.EdgeId;
            _selectedEdge = Game.Map.GetEdgeById(signal.EdgeId);

            (bool village, bool road, bool town) = Game.CheckBuildOptions(_selectedEdge);

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleRoadRequested(BuildRoadCommand signal)
        {
            var player = Game.GetCurrentPlayer();
            var result = BuildFreeRoadLogic.Handle(Game, player.ID, _selectedEdge);
            int id = result.Edge.Id;

            ResetSelection();

            if (!result.Success)
            {
                _selectedEdge = null;


                Bus.Publish(new ActionRejectedEvent(player.ID, result.Reason));
                return;
            }

            Bus.Publish(new RoadPlacedEvent(id));

            RoadsToBuild--;
            RoadsBuilt++;

            if (RoadsToBuild == 0)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }
        }
    }
}
