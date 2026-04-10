using Catan.Shared.Interfaces;

namespace Catan.Shared.Dtos.UiMessages
{
    public sealed class BankTradeRatioChangedDto : IUiMessageDto
    {
        public int Ratio { get; }
        public bool PossibleForPlayer { get; }
        public string? Resource { get; }

        public BankTradeRatioChangedDto(int ratio, bool possibleForPlayer, string? resource)
        {
            Ratio = ratio;
            PossibleForPlayer = possibleForPlayer;
            Resource = resource;
        }
    }
}