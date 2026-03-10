using Catan.Unity.Interfaces;

namespace Catan.Unity.Communication.InternalUIEvents
{
    public sealed class DevelopmentCardBoughtUIEvent : IInternalUIEvents
    {
        public int CardId;
        public DevelopmentCardBoughtUIEvent(int cardId)
        {
            CardId = cardId;
        }
    }
}