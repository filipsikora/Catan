using Catan.Unity.Interfaces;
using System.Collections.Generic;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class BankInformationChangedUIEvent : IInternalUIEvents { }

    public sealed class GameWonUIEvent : IInternalUIEvents
    {
        public int PlayerId { get; }
        public Dictionary<int, int> PlayerScoresToIds;

        public GameWonUIEvent(int playerId, Dictionary<int, int> playerScoresToIds)
        {
            PlayerId = playerId;
            PlayerScoresToIds = playerScoresToIds;
        }
    }

    public sealed class PlayersToMoveChangedUIEvent : IInternalUIEvents
    {
        public List<int> PlayersToMove;

        public PlayersToMoveChangedUIEvent(List<int> playersToMove)
        {
            PlayersToMove = playersToMove;
        }
    }

    public sealed class PlayerInformationTableChangedUIEvent : IInternalUIEvents { }

    public sealed class TurnNumberChangedUIEvent : IInternalUIEvents { }

    public sealed class RolledNumberChangedUIEvent : IInternalUIEvents { }
}