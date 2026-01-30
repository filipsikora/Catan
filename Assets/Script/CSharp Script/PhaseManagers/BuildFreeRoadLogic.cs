using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildFreeRoadLogic
    {
        private readonly GameSession _session;

        public BuildFreeRoadLogic(GameSession session)
        {
            _session = session;
        }

        public ResultBuildFreeRoad Handle(int edgeId)
        {
            var player = _session.GetCurrentPlayer();
            var edge = _session.GetEdgeById(edgeId);
            var result = RulesBuilding.CanBuildFreeRoad(player, edge, _session);

            if (!result.Success)
            {
                return ResultBuildFreeRoad.Fail(result.Reason, player.ID, edge);
            }

            _session.RoadBuiltMutation(player, edge);

            return ResultBuildFreeRoad.Ok(player.ID, edge);
        }
    }
}