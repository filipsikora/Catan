using Catan.Core.Models;
using Catan.Shared.Data;
using Catan.Core.Results;

namespace Catan.Core.Conditions
{
    public class ConditionsDevCards
    {
        public static ResultCondition DevCardExists(DevelopmentCard? card)
        {
            if (card == null)
            {
                return ResultCondition.Fail(ConditionFailureReason.DoesNotExist);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsOwner(DevelopmentCard card, Player player)
        {
            if (card.Owner != player)
            {
                return ResultCondition.Fail(ConditionFailureReason.NotOwner);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition CanBePlayedNow(DevelopmentCard card, bool afterRoll)
        {
            if (!(card.Type == EnumDevelopmentCardTypes.Knight || afterRoll))
            {
                return ResultCondition.Fail(ConditionFailureReason.NotKnightOrAfterRoll);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsNotUsed(DevelopmentCard card)
        {
            if (card.IsUsed)
            {
                return ResultCondition.Fail(ConditionFailureReason.AlreadyUsed);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsNotNew(DevelopmentCard card)
        {
            if (card.IsNew)
            {
                return ResultCondition.Fail(ConditionFailureReason.CardIsNew);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition IsNotOwned(DevelopmentCard card)
        {
            if (card.Owner != null)
            {
                return ResultCondition.Fail(ConditionFailureReason.IsAlreadyOwned);
            }

            return ResultCondition.Ok();
        }

        public static ResultCondition DevCardsLeft(int devCardsLeft)
        {
            if (devCardsLeft <= 0)
            {
                return ResultCondition.Fail(ConditionFailureReason.NoDevelopmentCardsLeft);
            }

            return ResultCondition.Ok();
        }
    }
}