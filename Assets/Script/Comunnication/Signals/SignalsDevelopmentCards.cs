using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Catan;
using Catan.Catan;
using JetBrains.Annotations;
using NUnit.Framework.Internal;

namespace Catan.Communication.Signals
{
    public class RequestShowDevelopmentCardsSignal { }

    public class RequestBuyDevelopmentCardSignal { }

    public class RequestUseDevelopmentCardsSignal
    {
        public int CardID { get; }
        public RequestUseDevelopmentCardsSignal(int cardId)
        {
            CardID = cardId;
        }
    }

    public class DevelopmentCardsShownSignal
    {
        public List<int> PlayerCardsByID { get; }
        public bool AfterRoll;
        public DevelopmentCardsShownSignal(List<int> playerCardsByID, bool afterRoll)
        {
            PlayerCardsByID = playerCardsByID;
            AfterRoll = afterRoll;
        }
    }

    public class DevelopmentCardsUsedSignal { }

    public class DevelopmentCardBoughtSignal
    {
        public int CardID { get; }
        public DevelopmentCardBoughtSignal(int cardId)
        {
            CardID = cardId;
        }
    }

    public class DevelopmentCardClickedSignal
    {
        public VisualDevelopmentCard Card;
        public DevelopmentCardClickedSignal(VisualDevelopmentCard card)
        {
            Card = card;
        }
    }

    public class DevelopmentCardsCanceledSignal { }

    public class KnightCardUsedSignal { }

    public class MonopolyCardUsedSignal { }

    public class MonopolyCardFinishedSignal { }

    public class RoadBuildingCardUsedSignal { }

    public class RoadBuildingFinishedSignal { }

    public class YearOfPlentyUsedSignal { }

    public class YearOfPlentyFinishedSignal { }

    public class VictoryPointUsedSignal { }

    public class CardSelectionAcceptedSignal { }

    public class ResourceSelectedSignal
    {
        public bool Selected { get; }
        public ResourceSelectedSignal(bool selected)
        {
            Selected = selected;
        }
    }
}
