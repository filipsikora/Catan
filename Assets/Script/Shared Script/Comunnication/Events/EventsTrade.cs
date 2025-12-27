using Catan.Core.Models;
using System.Collections.Generic;

namespace Catan.Shared.Communication.Events
{
    public class TradeOfferPossibleEvent
    {
        public bool CanTrade { get; }
        public TradeOfferPossibleEvent(bool canTrade)
        {
            CanTrade = canTrade;
        }
    }

    public class DesiredCardsChangedEvent
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedEvent(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }

    public class TradeRequestSentEvent
    {
        public int PlayerId;
        public bool CanTrade { get; }
        public ResourceCostOrStock CardsOffered;
        public ResourceCostOrStock CardsDesired;
        public TradeRequestSentEvent(int playerId, bool canTrade, ResourceCostOrStock cardsOffered, ResourceCostOrStock cardsDesired)
        {
            PlayerId = playerId;
            CanTrade = canTrade;
            CardsOffered = cardsOffered;
            CardsDesired = cardsDesired;
        }
    }

    public class RobberPlacedEvent
    {
        public int HexId { get; }
        public RobberPlacedEvent(int hexId)
        {
            HexId = hexId;
        }
    }

    public class PotentialVictimsSelectedEvent
    {
        public List<int> VictimsIds { get; }
        public PotentialVictimsSelectedEvent(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }

    public class PlayerSelectedToDiscardEvent
    {
        public int PlayerId;
        public PlayerSelectedToDiscardEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class VictimSelectedEvent
    {
        public ResourceCostOrStock VictimsCards;
        public int VictimId;
        public VictimSelectedEvent(ResourceCostOrStock victimCards, int victimId)
        {
            VictimsCards = victimCards;
            VictimId = victimId;
        }
    }
}