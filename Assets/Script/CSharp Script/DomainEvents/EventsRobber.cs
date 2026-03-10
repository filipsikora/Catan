using Catan.Core.Interfaces;

namespace Catan.Core.DomainEvents
{
    public sealed class RobberPlacedEvent : IDomainEvent
    {
        public int HexId { get; }
        public RobberPlacedEvent(int hexId)
        {
            HexId = hexId;
        }
    }
}