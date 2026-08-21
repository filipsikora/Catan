using System.Collections.Generic;

namespace BGS.Shared.Dtos
{
    public sealed class CommandExecutionResultDto
    {
        public IEnumerable<GameUpdateDto> GameUpdates { get; set; }
        public CommandResponseDto CommandResponse { get; set; }
        public CommandExecutionResultDto(IEnumerable<GameUpdateDto> gameUpdates, CommandResponseDto commandResponse)
        {
            GameUpdates = gameUpdates;
            CommandResponse = commandResponse;
        }
    }
}