using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class PlayerStateChangedDto : IDomainEventDto
    {
        public int PlayerId;
        public PlayerStateChangedDto(int playerId)
        {
            PlayerId = playerId;
        }
    }
}