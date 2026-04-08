using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos
{
    public class UiMessageDto
    {
        public EnumUiMessages Type { get; set; }
        public IUiMessageDto Data { get; set; }
    }
}