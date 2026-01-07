namespace Catan.Shared.Communication.Events
{
    public class StartGameRequestedEvent
    {
        public int PlayerCount { get; }
        public StartGameRequestedEvent(int playerCount)
        {
            PlayerCount = playerCount;
        }
    }

    public class GameInitializedEvent { }

    public class PlayerStateChangedEvent
    {
        public int PlayerId;
        public PlayerStateChangedEvent(int playerId)
        {
            PlayerId = playerId;
        }
    }
}