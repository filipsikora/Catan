using Catan.Core.Results;
using Catan.Core.Rules;
using System.Collections.Generic;

namespace Catan.Core.PhaseLogic
{
    public sealed class SelectVictimLogic
    {
        private readonly GameSession _session;

        public SelectVictimLogic(GameSession session)
        {
            _session = session;
        }

        public ResultCondition Handle(int victimId, List<int> possibleVictimsIds)
        {
            var victim = _session.GetPlayerById(victimId);
            var result = RulesRobber.ValidVictim(victim, possibleVictimsIds);

            if (!result.Success)
            {
                return ResultCondition.Fail(result.Reason);
            }

            _session.CreateCardsStealingContext(victim.ID);

            return ResultCondition.Ok();
        }
    }
}
