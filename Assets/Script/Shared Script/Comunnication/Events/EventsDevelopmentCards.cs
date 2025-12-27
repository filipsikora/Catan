using System.Collections.Generic;

namespace Catan.Shared.Communication.Events
{
    public class DevelopmentCardBoughtEvent
    {
        public int CardId;
        public DevelopmentCardBoughtEvent(int cardId)
        {
            CardId = cardId;
        }
    }

    public class ProceedToDevelopmentCardsEvent
    {
        public List<int> PlayerCardsByID { get; }
        public bool AfterRoll;
        public ProceedToDevelopmentCardsEvent(List<int> playerCardsByID, bool afterRoll)
        {
            PlayerCardsByID = playerCardsByID;
            AfterRoll = afterRoll;
        }
    }

    public class DevelopmentCardsShownEvent
    {
        public List<int> PlayerCardsByID { get; }
        public bool AfterRoll;
        public DevelopmentCardsShownEvent(List<int> playerCardsByID, bool afterRoll)
        {
            PlayerCardsByID = playerCardsByID;
            AfterRoll = afterRoll;
        }
    }
}