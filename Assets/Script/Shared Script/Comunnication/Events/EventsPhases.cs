using Catan.Core.Models;
using Catan.Shared.Data;

namespace Catan.Shared.Communication.Events
{
    public sealed class PhaseChangedEvent
    {
        public EnumGamePhases Phase { get; }

        public PhaseChangedEvent(EnumGamePhases phase)
        {
            Phase = phase;
        }
    }

    public class ReturnToNormalRoundEvent { }

    public class NormalRoundToBeforeRollEvent { }

    public class NormalRoundToOfferTradeEvent
    {
        public ResourceCostOrStock OfferedCards { get; }
        public NormalRoundToOfferTradeEvent(ResourceCostOrStock cards)
        {
            OfferedCards = cards;
        }
    }

    public class NormalRoundToBankTradeEvent { }

    public class TradeOfferToTradeRequestEvent
    {
        public ResourceCostOrStock CardsOffered;
        public ResourceCostOrStock CardsDesired;
        public int PlayerId;
        public TradeOfferToTradeRequestEvent(ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsDesired, int playerId)
        {
            CardsOffered = cardsOffered;
            CardsDesired = cardsDesired;
            PlayerId = playerId;
        }
    }

    public class RobberPlacingToCardStealingEvent
    {
        public int VictimId;
        public RobberPlacingToCardStealingEvent(int victimId)
        {
            VictimId = victimId;
        }
    }

    public class DiceRollCompletedEvent
    {
        public bool RolledSeven { get; }
        public DiceRollCompletedEvent(bool rolledSeven)
        {
            RolledSeven = rolledSeven;
        }
    }

    public class CardStealingCompletedEvent { }

    public class DevelopmentCardsCompletedEvent
    {
        public bool AfterRoll;
        public DevelopmentCardsCompletedEvent(bool afterRoll)
        {
            AfterRoll = afterRoll;
        }
    }

    public class ProceedToDevelopmentCardsEvent { }

    public class DevelopmentCardsToRoadBuildingEvent { }

    public class DevelopmentCardsToMonopolyCardEvent { }

    public class DevelopmentCardsToYearOfPlentyUsedEvent { }

    public class ProceedToRobberPlacingEvent { }

    public class FirstRoundsBuildingCompletedEvent
    {
        public bool FirstTurnsLeft;
        public FirstRoundsBuildingCompletedEvent(bool firstTurnsLeft)
        {
            FirstTurnsLeft = firstTurnsLeft;
        }
    }
}