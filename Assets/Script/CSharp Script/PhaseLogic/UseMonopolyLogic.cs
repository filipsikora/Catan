using Catan.Core.Conditions;
using Catan.Core.Results;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class UseMonopolyLogic : BaseLogic
    {
        public UseMonopolyLogic(GameSession session) : base(session) { }

        public ResultMonopolyCard Handle(EnumResourceTypes resource)
        {
            var player = Session.GetCurrentPlayer();

            var result = ConditionsResources.ResourceExists(resource);

            if (!result.Success)
            {
                return ResultMonopolyCard.Fail(result.Reason, player.ID, resource);
            }

            var victimsIdsAndAmounts = Session.UseMonopolyMutation(resource);

            return ResultMonopolyCard.Ok(player.ID, victimsIdsAndAmounts, resource, EnumGamePhases.NormalRound);
        }
    }
}