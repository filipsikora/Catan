using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class UpgradeVillageLogic : BaseLogic
    {
        public UpgradeVillageLogic(GameSession session) : base(session) { }

        public  ResultUpgradeVillage Handle(int vertexId)
        {
            var vertex = Session.GetVertexById(vertexId);
            var player = Session.GetCurrentPlayer();
            var result = RulesBuilding.CanUpgradeVillage(player, vertex, Session);

            if (!result.Success)
            {
                return ResultUpgradeVillage.Fail(result.Reason, player.ID, vertexId);
            }

            Session.TownPaidAndBuiltMutation(vertex);

            return ResultUpgradeVillage.Ok(player.ID, vertexId, null);
        }
    }
}