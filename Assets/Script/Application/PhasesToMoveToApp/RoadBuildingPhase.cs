using Catan.Application.Controllers;
using Catan.Shared.Communication;
using Catan.Shared.Communication.Commands;
using Catan.Shared.Communication.Events;
using Catan.Shared.Data;

namespace Catan.Application.Phases
{
    public class RoadBuildingPhase : BaseBuildPhase
    {
        public RoadBuildingPhase(Facade facade, EventBus bus, PhaseTransitionController phaseTransition) : base(facade, bus, phaseTransition) { }

        public override void Enter()
        {
            var result = Facade.UsePrepareRoadBuilding();
            var playerId = Facade.GetCurrentPlayerId();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);

                return;
            }

            if (Facade.GetRoadsLeftToBuild() == 0)
            {
                Bus.Publish(new LogMessageEvent(EnumLogTypes.Info, "No roads left to build"));
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }
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

            var village = false;
            var road = true;
            var town = false;   

            Bus.Publish(new EdgeHighlightedEvent(signal.EdgeId));
            Bus.Publish(new BuildOptionsSentEvent(village, road, town));
        }

        private void HandleRoadRequested(BuildRoadCommand signal)
        {
            var playerId = Facade.GetCurrentPlayerId();
            var id = SelectedEdgeId.Value;
            var result = Facade.UseBuildFreeRoad(id);

            ResetSelection();

            if (!result.Success)
            {
                Bus.Publish(new ActionRejectedEvent(playerId, result.Reason));
                return;
            }

            Bus.Publish(new RoadPlacedEvent(id));

            if (Facade.GetRoadsLeftToBuild() == 0)
            {
                PhaseTransition.ChangePhase(EnumGamePhases.NormalRound);
            }
        }
    }
}