using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildRoadLogic : BaseLogic
    {
        public BuildRoadLogic(GameSession session) : base(session) { }

        public ResultBuildRoad Handle(int edgeId)
        {
            var player = Session.GetCurrentPlayer();
            var edge = Session.GetEdgeById(edgeId);

            var result = RulesBuilding.CanBuildRoad(player, edge, Session);

            if (!result.Success)
            {
                return ResultBuildRoad.Fail(result.Reason, player.ID, edge);
            }

            Session.RoadPaidAndBuiltMutation(edge);

            return ResultBuildRoad.Ok(player.ID, edge);
        }
    }
}