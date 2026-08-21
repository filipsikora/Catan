using Catan.Unity.Interfaces;
using Catan.Unity.Models;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class GameFlowReceivedUIEvent : IInternalUIEvents
    {
        public GameFlowModel GameFlow;
        public GameFlowReceivedUIEvent(GameFlowModel gameFlow)
        {
            GameFlow = gameFlow;
        }
    }

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