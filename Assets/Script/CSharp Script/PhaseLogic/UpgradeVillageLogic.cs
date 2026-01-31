using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Core.Rules;

namespace Catan.Core.PhaseLogic
{
    public sealed class UpgradeVillageLogic : BaseLogic
    {
        public UpgradeVillageLogic(GameSession session) : base(session) { }

        public  ResultUpgradeVillage Handle(Vertex vertex)
        {
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