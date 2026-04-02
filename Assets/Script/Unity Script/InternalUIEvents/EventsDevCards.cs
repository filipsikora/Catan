using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class DevelopmentCardBoughtUIEvent : IInternalUIEvents
    {
        public int CardId;
        public DevelopmentCardBoughtUIEvent(int cardId)
        {
            CardId = cardId;
        }
    }

    public sealed class DevelopmentCardClickedUIEvent : IInternalUIEvents
    {
        public int CardId;
        public DevelopmentCardClickedUIEvent(int cardId)
        {
            CardId = cardId;
        }
    }
}