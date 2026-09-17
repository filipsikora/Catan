using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class PlayerClickedUIEvent : IInternalUIEvents
    {
        public int PlayerId;
        public PlayerClickedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class MyResourcesChangedUIEvent : IInternalUIEvents { }
}