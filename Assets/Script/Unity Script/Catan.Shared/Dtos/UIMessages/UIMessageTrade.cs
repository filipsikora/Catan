using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class BankTradeRatioChangedDto : IUiMessageDto
    {
        public int Ratio { get; set; }
        public bool PossibleForPlayer { get; set; }
        public string? Resource { get; set; }

        public BankTradeRatioChangedDto(int ratio, bool possibleForPlayer, string? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}