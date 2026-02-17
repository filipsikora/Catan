using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class BuildInitialVillageLogic : BaseLogic
    {
        public BuildInitialVillageLogic(GameSession session) : base(session) { }

        public ResultBuildInitialVillage Handle(int vertexId)
        {
            var player = Session.GetCurrentPlayer();
            var vertex = Session.GetVertexById(vertexId);

            var result = RulesBuilding.CanBuildInitialVillage(player, vertex, Session);

            if (!result.Success)
            {
                return ResultBuildInitialVillage.Fail(result.Reason, player.ID, vertexId);
            }

            var secondVillage = player.Points == 1;

            Session.VillageBuiltMutation(vertex, secondVillage);

            return ResultBuildInitialVillage.Ok(player.ID, vertexId, null);
        }
    }
}