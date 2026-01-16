using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class RequestBankTradeAvailabilityCommand : ICommand { }

    public class RequestCardDiscardingStartCommand : ICommand { }

    public class RequestCardStealingStartCommand : ICommand { }

    public class AcceptTradeRequestCommand : ICommand { }

    public class RequestRolledNumberCommand : ICommand { }
}