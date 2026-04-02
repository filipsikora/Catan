using Catan.Shared.Data;

namespace Catan.Shared.Dtos
{
    public class CommandResponseDto
    {
        public bool Success { get; set; }
        public EnumGamePhasesDto? NextPhase { get; set; }
        public List<UiMessageDto> UiMessages { get; set; } = new();
        public List<DomainEventDto> DomainMessages { get; set; } = new();
    }
}