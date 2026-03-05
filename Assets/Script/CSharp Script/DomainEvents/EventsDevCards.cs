using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class DevelopmentCardBoughtEvent : IDomainEvent
    {
        public int CardId;
        public DevelopmentCardBoughtEvent(int cardId)
        {
            CardId = cardId;
        }
    }
}