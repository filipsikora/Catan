using Catan.Shared.Data;
using Newtonsoft.Json.Linq;

public class CommandRequestDto
{
    public EnumCommandType Type { get; set; }
    public JObject Data { get; set; }
}