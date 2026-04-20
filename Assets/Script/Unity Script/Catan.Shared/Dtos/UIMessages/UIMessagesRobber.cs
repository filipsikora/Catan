using Catan.Shared.Interfaces;
using System.Collections.Generic;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class PlayerSelectedToDiscardDto : IUiMessageDto
    {
        public int PlayerId;
        public PlayerSelectedToDiscardDto(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public sealed class PotentialVictimsFoundDto : IUiMessageDto
    {
        public List<int> VictimsIds { get; }

        public PotentialVictimsFoundDto(List<int> victimsIds)
        {
            VictimsIds = victimsIds;
        }
    }

    public sealed class RobberPlacedDto : IUiMessageDto
    {
        public int HexId { get; }
        public RobberPlacedDto(int hexId)
        {
            HexId = hexId;
        }
    }
}
