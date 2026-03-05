using Catan.Application.Interfaces;

namespace Catan.Application.UIMessages
{
    public sealed class PlayerSelectedToDiscardMessage : IUIMessages
    {
        public int PlayerId;
        public PlayerSelectedToDiscardMessage(int playerId)
        {
            PlayerId = playerId;
        }
    }
}
