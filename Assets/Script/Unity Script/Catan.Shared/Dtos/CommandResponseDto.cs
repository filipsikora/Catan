using Catan.Shared.Data;
using System.Collections.Generic;

namespace Catan.Shared.Dtos
{
    public class CommandResponseDto
    {
        public bool Success { get; set; }
        public EnumGamePhases? NextPhase { get; set; }
        public List<UiMessageDto> UiMessages { get; set; } = new();
        public List<DomainEventDto> DomainMessages { get; set; } = new();
    }
}