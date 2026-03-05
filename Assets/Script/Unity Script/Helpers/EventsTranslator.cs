using Catan.Application.Interfaces;
using Catan.Core.Interfaces;
using Catan.Unity.Interfaces;

namespace Catan.Unity.Helpers
{
    public class EventsTranslator
    {
        public EventsTranslator() { }

        public IInternalUIEvents TranslateUIMessage(IUIMessages uiMessage)
        {
            return uiMessage switch
            {

            };
        }

        public IInternalUIEvents TranslateDomainEvent(IDomainEvent domainEvent)
        {
            return domainEvent switch
            { 

            };
        }
    }
}
