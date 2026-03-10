using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildInitialRoadLogic : BaseLogic
    {
        public BuildInitialRoadLogic(GameSession session) : base(session) { }

        public ResultBuildInitialRoad Handle(int edgeId, int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var edge = Session.GetEdgeById(edgeId);
            var vertex = Session.GetVertexById(vertexId);

            var result = RulesBuilding.CanBuildInitialRoad(player, edge, vertex, Session);

            if (!result.Success)
            {
                return ResultBuildInitialRoad.Fail(result.Reason, player.ID, edgeId);
            }

            Session.RoadBuiltMutation(edge);

            return ResultBuildInitialRoad.Ok(player.ID, edgeId, null);
        }
    }
}