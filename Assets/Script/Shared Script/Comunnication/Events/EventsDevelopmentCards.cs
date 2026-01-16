namespace Catan.Shared.Communication.Events
{
    public class DevelopmentCardBoughtEvent
    {
        public int CardId;
        public DevelopmentCardBoughtEvent(int cardId)
        {
            CardId = cardId;
        }
    }
}