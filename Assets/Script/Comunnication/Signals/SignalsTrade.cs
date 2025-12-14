using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;

namespace Catan.Communication.Signals
{
    public class TradeOfferPossibleSignal
    {
        public bool CanTrade { get; }
        public TradeOfferPossibleSignal(bool canTrade)
        {
            CanTrade = canTrade;
        }
    }

    public class TradeOfferConfirmedSignal
    {
        public ResourceCostOrStock OfferedCards { get; }
        public TradeOfferConfirmedSignal(ResourceCostOrStock cards)
        {
            OfferedCards = cards;
        }
    }

    public class RequestOfferTradeSignal { }

    public class ReviewDesiredCardsChangedSignal
    {
        public ResourceCostOrStock CardsDesired { get; }
        public bool HasDesired { get; }
        public ReviewDesiredCardsChangedSignal(ResourceCostOrStock cardsDesired, bool hasDesired)
        {
            CardsDesired = cardsDesired;
            HasDesired = hasDesired;
        }
    }

    public class TradeOfferCanceledSignal { }

    public class TradePartnerChosenSignal
    {
        public int PlayerId { get; }
        public TradePartnerChosenSignal(int playerId)
        {
            PlayerId = playerId;
        }
    }

    public class TradeRequestAcceptedSignal { }

    public class TradeRequestRefusedSignal { }

    public class TradeFinishedSignal { }

    public class TradeRequestSentSignal
    {
        public bool CanTrade { get; }
        public TradeRequestSentSignal(bool canTrade)
        {
            CanTrade = canTrade;
        }
    }

}