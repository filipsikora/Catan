using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class TurnNumberChangedDto : IUiMessageDto
    {
        public int NewTurnNumber { get; set; }
        public TurnNumberChangedDto(int newTurnNumber)
        {
            NewTurnNumber = newTurnNumber;
        }
    }

    public sealed class DiceRollChangedDto : IUiMessageDto
    {
        public int RolledNumber { get; set; }
        public DiceRollChangedDto(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }
}
