using Catan.Core.Models;

namespace Catan.Shared.Communication.Events
{
    public class DesiredCardsChangedEvent
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedEvent(bool hasDesired)
        {
            HasDesired = hasDesired;
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

    public class PotentialVictimsFoundEvent { }

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