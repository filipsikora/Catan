using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class StealCardLogic : BaseLogic
    {
        public StealCardLogic(GameSession session) : base(session) { }

        public  ResultStealResource Handle(int victimId, EnumResourceTypes resource)
        {
            var thief = Session.GetCurrentPlayer();
            var victim = Session.GetPlayerById(victimId);

            var (exists, context) = Session.TryGetCardStealingContext();

            if (context == null)
                return ResultStealResource.Fail(default, default, ConditionFailureReason.DoesNotExist);

            var result = RulesRobber.CanSteal(victim, context);

            if (!result.Success)
            {
                return ResultStealResource.Fail(thief.ID, victimId, result.Reason);
            }

            Session.CardStolenMutation(victim, resource);
            
            return ResultStealResource.Ok(thief.ID, victimId, resource);
        }
    }
}