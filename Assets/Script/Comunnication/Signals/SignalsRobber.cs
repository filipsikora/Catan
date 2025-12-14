using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

namespace Catan.Communication.Signals
{
    public class RobberPlacedSignal
    {
        public List<int> VictimsIds { get; }
        public int HexId { get; }
        public RobberPlacedSignal(List<int> victimsIds, int hexId)
        {
            VictimsIds = victimsIds;
            HexId = hexId;
        }
    }

    public class VictimChosenSignal
    {
        public int VictimId { get; }
        public VictimChosenSignal(int victimId)
        {
            VictimId = victimId;
        }
    }

    public class StealingFinishedSignal { }

    public class DiscardingAcceptedSignal { }

    public class AllDiscardingCompleteSignal { }

    public class DiscardShownForPlayerSignal
    {
        public int PlayerId;
        public DiscardShownForPlayerSignal(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class AcceptedDiscardVisibilitySignal
    {
        public bool CanDiscard;
        public AcceptedDiscardVisibilitySignal(bool canDiscard)
        {
            CanDiscard = canDiscard;
        }
    }
}
