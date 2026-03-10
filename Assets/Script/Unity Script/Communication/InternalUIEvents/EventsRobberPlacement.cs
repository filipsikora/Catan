namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class RobberMovedUIEvent
    {
        public int HexId;
        public RobberMovedUIEvent(int hexId)
        {
            HexId = hexId;
        }
    }
}
