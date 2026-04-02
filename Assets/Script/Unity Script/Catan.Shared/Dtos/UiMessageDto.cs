using Catan.Shared.Data;

namespace Catan.Shared.Dtos
{
    public class UiMessageDto
    {
        public EnumUiMessages Type { get; set; }
        public object Data { get; set; }
    }
}
