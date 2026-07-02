using Newtonsoft.Json.Linq;
using System;

namespace BGS.Shared.Dtos
{
    public class JoinGameResponseDto
    {
        public Guid GameId { get; set; }
        public Guid PlayerToken { get; set; }
        public JToken Payload { get; set; }
    }
}