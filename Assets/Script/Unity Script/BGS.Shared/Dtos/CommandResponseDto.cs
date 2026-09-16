#nullable enable

using System.Collections.Generic;

namespace BGS.Shared.Dtos
{
    public class CommandResponseDto
    {
        public bool Success { get; set; }
        public List<UiMessageDto> UiMessages { get; set; }
    }
}