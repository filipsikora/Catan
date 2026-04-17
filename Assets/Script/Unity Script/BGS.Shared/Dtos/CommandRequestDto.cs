using Newtonsoft.Json.Linq;

namespace BGS.Shared.Dtos
{
    public class CommandRequestDto
    {
        public string Type { get; set; }
        public JObject Data { get; set; }
    }
}