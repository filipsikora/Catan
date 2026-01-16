using Catan.Shared.Interfaces;

namespace Catan.Shared.Communication.Commands
{
    public class OfferTradeCommand : ICommand { }

    public class TradeOfferCanceledCommand : ICommand { }

    public class TradePartnerChosenCommand : ICommand
    {
        public int PlayerId { get; }
        public TradePartnerChosenCommand(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class TradeRequestAcceptedCommand : ICommand { }

    public class RefuseTradeRequestCommand : ICommand { }

    public class RequestTradeDataCommand : ICommand { }
}