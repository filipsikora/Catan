using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class RobberPlacedDto : IDomainEventDto
    {
        public int HexId { get; }
        public RobberPlacedDto(int hexId)
        {
            HexId = hexId;
        }
    }
}