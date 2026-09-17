using Catan.Unity.Interfaces;

namespace Catan.Unity.InternalUIEvents
{
    public sealed class DevelopmentCardClickedUIEvent : IInternalUIEvents
    {
        public int CardId;
        public DevelopmentCardClickedUIEvent(int cardId)
        {
            CardId = cardId;
        }
    }
}