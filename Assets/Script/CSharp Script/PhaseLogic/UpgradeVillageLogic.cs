using Catan.Core.Results;
using Catan.Core.Rules;

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
                return ResultUpgradeVillage.Fail(result.Reason, player.ID, vertex);
            }

            Session.TownPaidAndBuiltMutation(vertex);

            return ResultUpgradeVillage.Ok(player.ID, vertex);
        }
    }
}