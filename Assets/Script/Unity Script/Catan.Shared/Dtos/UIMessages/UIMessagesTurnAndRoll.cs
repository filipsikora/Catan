using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class TurnNumberChangedDto : IUiMessageDto
    {
        public int NewTurnNumber;
        public TurnNumberChangedDto(int newTurnNumber)
        {
            NewTurnNumber = newTurnNumber;
        }
    }

    public sealed class DiceRollChangedDto : IUiMessageDto
    {
        public int RolledNumber;
        public DiceRollChangedDto(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }
}
