using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class RobberMovedUIEvent : IInternalUIEvents { }

    public sealed class PotentialVictimsFoundUIEvent : IInternalUIEvents
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundUIEvent(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }
}
