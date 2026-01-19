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
}