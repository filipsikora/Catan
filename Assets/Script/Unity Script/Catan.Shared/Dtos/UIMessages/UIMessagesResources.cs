using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
 {
    public sealed class ResourceSelectedDto : IUiMessageDto
    {
        public string? Type { get; set; }
        public bool Selected { get; set; }
        public ResourceSelectedDto(bool selected, string? type)
        {
            Type = type;
            Selected = selected;
        }
    }

    public sealed class SelectionChangedDto : IUiMessageDto
    {
        public bool ActionAvailable { get; set; }
        public SelectionChangedDto(bool actionAvailable)
        {
            ActionAvailable = actionAvailable;
        }
    }

    public sealed class DesiredCardsChangedDto : IUiMessageDto
    {
        public bool HasDesired { get; set; }
        public DesiredCardsChangedDto(bool hasDesired)
        {
            HasDesired = hasDesired;
        }
    }
}
