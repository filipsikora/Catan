using Catan.Shared.Data;
using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
 {
    public sealed class ResourceSelectedDto : IUiMessageDto
    {
        public EnumResourceType? Type { get; }
        public bool Selected { get; }
        public ResourceSelectedDto(bool selected, EnumResourceType? type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public sealed class SelectionChangedDto : IUiMessageDto
    {
        public bool ActionAvailable;
        public SelectionChangedDto(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }

    public sealed class DesiredCardsChangedDto : IUiMessageDto
    {
        public bool HasDesired { get; }
        public DesiredCardsChangedDto(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }
}
