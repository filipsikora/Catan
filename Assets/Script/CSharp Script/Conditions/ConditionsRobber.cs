using Catan.Core.Engine;
using Catan.Core.Models;
using Catan.Core.Results;
using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Core.Conditions
{
    public class ConditionsRobber
    {
        public static ResultCondition DiscardContextIsValid(CardDiscardContext context, Queue<int> playersToDiscard)
        {
            if (context == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            if (context.PlayersToDiscard != playersToDiscard)
            {
                return ResultCondition.Fail(ConditionFailureReason.DiscardContextInvalid);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition StealContextIsValid(CardStealingContext context, int victimId)
        {
            if (context == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            if (context.VictimId != victimId)
            {
                return ResultCondition.Fail(ConditionFailureReason.VictimInvalid);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition VictimPossible(List<int> possibleVictimsIds, Player victim)
        {
            if (possibleVictimsIds.Contains(victim.ID))
            {
                return ResultCondition.Ok();
            }

            return ResultCondition.Fail(ConditionFailureReason.VictimInvalid);
        }
    }
}