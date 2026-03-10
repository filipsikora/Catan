using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class ShowDevelopmentCardsCommand : ICommand { }

    public class BuyDevelopmentCardCommand : ICommand { }

    public class DevelopmentCardsCanceledCommand : ICommand { }

    public class CardSelectionAcceptedCommand : ICommand { }

    public class DevelopmentCardClickedCommand : ICommand
    {
        public int DevelopmentCardId;
        public DevelopmentCardClickedCommand(int devCardId)
        {
            DevelopmentCardId = devCardId;
        }

    }
}