using Catan.Shared.Data;

namespace Catan.Shared.Dtos
{
    public class DomainEventDto
    {
        public EnumDomainEvents Type { get; set; }
        public object Data { get; set; }
    }
}