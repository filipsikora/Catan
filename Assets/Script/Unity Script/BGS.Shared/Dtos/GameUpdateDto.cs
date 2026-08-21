using Newtonsoft.Json.Linq;
using System;

namespace BGS.Shared.Dtos
{
    public sealed class GameUpdateDto
    {
        public string DtoType { get; set; }
        public Guid PlayerToken { get; set; }
        public JToken Payload { get; set; }

        public GameUpdateDto(string dtoType, Guid playerToken,JToken payload)
        {
            DtoType = dtoType;
            PlayerToken = playerToken;
            Payload = payload;
        }
    }
}