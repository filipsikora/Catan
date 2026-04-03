using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class TurnNumberChangedUIEvent : IInternalUIEvents
    {
        public int TurnNumber;
        public TurnNumberChangedUIEvent(int turnNumber)
        {
            TurnNumber = turnNumber;
        }
    }

    public sealed class DiceRollChangedUIEvent : IInternalUIEvents
    {
        public int RolledNumber;
        public DiceRollChangedUIEvent(int rolledNumber)
        {
            RolledNumber = rolledNumber;
        }
    }
}