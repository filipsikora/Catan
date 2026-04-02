using Catan.Shared.Data;
using System.Text.Json;

namespace Catan.Shared.Dtos
{
    public class CommandRequestDto
    {
        public EnumCommandType Type { get; set; }
        public JsonElement Data { get; set; }
    }
}