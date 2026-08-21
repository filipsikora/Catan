using System;

namespace BGS.Shared.Dtos
{
    public class JoinGameRequestDto
    {
        public Guid GameId { get; set; }
        public Guid? PlayerToken { get; set; }
        public string PlayerName { get; set; }
    }
}