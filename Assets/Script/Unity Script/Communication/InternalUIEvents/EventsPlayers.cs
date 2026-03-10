namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class PlayerStateChangedUIEvent
    {
        public int PlayerId;
        public PlayerStateChangedUIEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
