using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos
{
    public class DomainEventDto
    {
        public EnumDomainEvents Type { get; set; }
        public IDomainEventDto Data { get; set; }
    }
}