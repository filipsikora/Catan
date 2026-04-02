using Catan.Shared.Data;
using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class RobberMovedUIEvent : IInternalUIEvents
    {
        public int HexId;
        public RobberMovedUIEvent(int hexId)
        {
            HexId = hexId;
        }
    }

    public sealed class PlayerSelectedToDiscardUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerSelectedToDiscardUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class StolenCardSelectedUIEvent : IInternalUIEvents
    {
        public EnumResourceTypes Type;
        public StolenCardSelectedUIEvent(EnumResourceTypes type)
        {
            Type = type;
        }
    }

    public sealed class PotentialVictimsFoundUIEvent : IInternalUIEvents
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundUIEvent(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }
}
