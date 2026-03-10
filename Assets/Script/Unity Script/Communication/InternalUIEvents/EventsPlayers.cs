using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class PlayerStateChangedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerStateChangedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
