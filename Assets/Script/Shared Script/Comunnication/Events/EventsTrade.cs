using Catan.Core.Models;
using System.Collections.Generic;

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

    public class PotentialVictimsFoundEvent
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundEvent(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }


}