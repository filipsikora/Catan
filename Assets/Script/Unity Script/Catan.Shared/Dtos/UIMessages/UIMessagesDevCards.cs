using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UIMessages
{
    public sealed class DevelopmentCardBoughtDto : IUiMessageDto
    {
        public int CardId;
        public DevelopmentCardBoughtDto(int cardId)
        {
            CardId = cardId;
        }
    }
}
