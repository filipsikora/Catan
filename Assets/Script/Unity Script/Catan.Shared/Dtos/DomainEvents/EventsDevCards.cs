using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class DevelopmentCardBoughtDto : IDomainEventDto
    {
        public int CardId;
        public DevelopmentCardBoughtDto(int cardId)
        {
            CardId = cardId;
        }
    }
}