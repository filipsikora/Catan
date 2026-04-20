using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UIMessages
{
    public sealed class PlayerStateChangedDto : IUiMessageDto
    {
        public int PlayerId;
        public PlayerStateChangedDto(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
