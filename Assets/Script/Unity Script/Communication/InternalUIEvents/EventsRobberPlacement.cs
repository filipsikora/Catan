using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class RobberMovedUIEvent : IInternalUIEvents
    {
        public int HexId;
        public RobberMovedUIEvent(int hexId)
        {
            HexId = hexId;
        }
    }
}
