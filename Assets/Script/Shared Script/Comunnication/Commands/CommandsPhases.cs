using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class RequestBankTradeAvailabilityCommand : ICommand { }

    public class RequestDevelopmentCardsViewCommand : ICommand { }

    public class RequestCardDiscardingStartCommand : ICommand { }

    public class RequestCardStealingStartCommand : ICommand { }

    public class RequestTradeRequestValidatedCommand : ICommand { }

    public class RequestRolledNumberCommand : ICommand { }
}