using System;

namespace Catan.Shared.Dtos
{
    public class GameStatePerPlayerDto
    {
        public Guid GameId { get; set; }
        public Guid PlayerToken { get; set; }

        public FullBoardDto Board { get; set; }
        public FullGameFlowDto GameFlow { get; set; }

        public FullPlayerDto Player { get; set; }
        public OtherPlayersDto OtherPlayers { get; set; }
    }
}