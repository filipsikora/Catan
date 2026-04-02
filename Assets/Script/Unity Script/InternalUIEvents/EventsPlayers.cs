using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class PlayerStateChangedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerStateChangedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PlayerClickedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerClickedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}