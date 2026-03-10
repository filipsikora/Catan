using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildFreeRoadLogic : BaseLogic
    {
        public BuildFreeRoadLogic(GameSession session) : base(session) { }

        public ResultBuildFreeRoad Handle(int edgeId)
        {
            var player = Session.GetCurrentPlayer();
            var edge = Session.GetEdgeById(edgeId);
            var result = RulesBuilding.CanBuildFreeRoad(player, edge, Session);

            if (!result.Success)
            {
                return ResultBuildFreeRoad.Fail(result.Reason, player.ID, edgeId);
            }

            Session.RoadBuiltMutation(edge);
            Session.RoadBuildingContextMutation();

            EnumGamePhases? nextPhase = Session.GetRoadsLeftToBuild() == 0 ? EnumGamePhases.NormalRound : null;

            return ResultBuildFreeRoad.Ok(player.ID, edgeId, nextPhase);
        }
    }
}