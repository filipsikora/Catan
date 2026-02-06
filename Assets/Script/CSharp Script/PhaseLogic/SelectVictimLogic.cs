using Catan.Core.Results;
using Catan.Core.Rules;
using Catan.Shared.Data;

namespace Catan.Core.PhaseLogic
{
    public sealed class SelectVictimLogic : BaseLogic
    {
        public SelectVictimLogic(GameSession session) : base(session) { }

        public ResultCondition Handle(int victimId)
        {
            var victim = Session.GetPlayerById(victimId);
            var possibleVictimsIds = Session.GetPossibleVictimsIds();
            var result = RulesRobber.ValidVictim(victim, possibleVictimsIds);

            if (!result.Success)
            {
                return ResultCondition.Fail(result.Reason);
            }

            Session.CreateCardsStealingContext(victim.ID);

            return ResultCondition.Ok(EnumGamePhases.CardStealing);
        }
    }
}