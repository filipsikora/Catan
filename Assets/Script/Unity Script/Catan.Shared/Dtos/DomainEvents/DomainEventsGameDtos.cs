using Catan.Shared.Data;
using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.DomainEvents
{
    public sealed class RolledNumberChangedEventDto : IDomainEventDto
    {
        public int NewRolledNumber;

        public RolledNumberChangedEventDto(int newRolledNumber)
        {
            NewRolledNumber = newRolledNumber;
        }
    }

    public sealed class PhaseChangedEventDto : IDomainEventDto
    {
        public EnumGamePhases Phase;
        public List<int> PlayersToMove;

        public PhaseChangedEventDto(
            EnumGamePhases phase,
            List<int> playersToMove)
        {
            Phase = phase;
            PlayersToMove = playersToMove;
        }
    }

    public sealed class PlayersToMoveChangedEventDto : IDomainEventDto
    {
        public List<int> PlayersToMove;

        public PlayersToMoveChangedEventDto(List<int> playersToMove)
        {
            PlayersToMove = playersToMove;
        }
    }

    public sealed class GameWonEventDto : IDomainEventDto
    {
        public int PlayerId;
        public Dictionary<int, int> PlayerScoresToIds;

        public GameWonEventDto(
            int playerId,
            Dictionary<int, int> playerScoresToIds)
        {
            PlayerId = playerId;
            PlayerScoresToIds = playerScoresToIds;
        }
    }
}