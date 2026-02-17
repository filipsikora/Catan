using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildVillageLogic : BaseLogic
    {
        public BuildVillageLogic(GameSession session) : base(session) { }

        public ResultBuildVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var vertex = Session.GetVertexById(vertexId);

            var result = RulesBuilding.CanBuildVillage(player, vertex, Session);

            if (!result.Success)
            {
                return ResultBuildVillage.Fail(result.Reason, player.ID, vertexId);
            }

            Session.VillagePaidAndBuiltMutation(vertex);

            return ResultBuildVillage.Ok(player.ID, vertexId, null);
        }
    }
}