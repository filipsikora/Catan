using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Events;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class FirstRoundsBuildingPhase : BaseBuildPhase
    {
        private bool villagePlaced = false;
        private bool roadPlaced = false;

        public FirstRoundsBuildingPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter()
        {
            Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "Select a vertex to build your free village, then select an edge to build a free road", 4));
        }

        public override void Handle(object command)
        {
            switch (command)
            {
                case VertexClickedCommand c:
                    HandleVertexClicked(c);
                    break;

                case EdgeClickedCommand c:
                    HandleEdgeClicked(c);
                    break;

                case BuildVillageCommand c:
                    HandleBuildVillage(c);
                    break;

                case BuildRoadCommand c:
                    HandleBuildRoad(c);
                    break;

                case EndTurnCommand c:
                    HandleTurnEnded(c);
                    break;
            }
        }

        private void HandleVertexClicked(VertexClickedCommand signal)
        {
            if (villagePlaced)
                return;

            SelectedVertexId = signal.VertexId;

            bool village = true;
            bool road = false;
            bool town = false;

            Bus.Publish(new VertexHighlightedEvent(signal.VertexId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleEdgeClicked(EdgeClickedCommand signal)
        {
            if (!villagePlaced)
                return;

            if (roadPlaced)
                return;

            SelectedEdgeId = signal.EdgeId;

            bool village = false;
            bool road = true;
            bool town = false;

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleBuildVillage(BuildVillageCommand signal)
        {
            int id = SelectedVertexId.Value;
            var result = Facade.UseBuildInitialVillage(id);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }

            villagePlaced = true;
            Bus.Publish(new VillagePlacedEvent(id));
        }

        private void HandleBuildRoad(BuildRoadCommand signal)
        {
            int id = SelectedEdgeId.Value;
            var vertexId = Facade.GetLastPlacedVillagePositionId();

            var result = Facade.UseBuildInitialRoad(id, vertexId);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(result.PlayerId, result.Reason));
                return;
            }

            roadPlaced = true;
            Bus.Publish(new RoadPlacedEvent(id));
        }

        private void HandleTurnEnded(EndTurnCommand signal)
        {
            var result = Facade.UseFinishTurn();

            if (result.InitialRoundsRemaining)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.FirstRoundsBuilding);
            }

            else
            {
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }
        }
    }
}