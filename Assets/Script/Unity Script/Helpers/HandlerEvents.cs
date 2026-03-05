using Catan.Application;
using Catan.Application.Interfaces;
using Catan.Application.UIMessages;
using Catan.Core.Interfaces;
using Catan.Shared.Interfaces;
using Catan.Unity.Phases.Controllers;
using System.Collections.Generic;

namespace Catan.Unity.Helpers
{
    public class HandlerEvents
    {
        private GameApplication _gameApplication;
        private AdapterGameFlow _gameFlow;
        private EventsTranslator _translator;

        public HandlerEvents(GameApplication gameApplication, AdapterGameFlow gameFlow, EventsTranslator translator)
        {
            _gameApplication = gameApplication;
            _gameFlow = gameFlow;
            _translator = translator;
        }

        public void Execute(ICommand command)
        {
            var result = _gameApplication.Execute(command);

            var uiMessagesList = result.GetUIMessagesList();
            var domainEventsList = result.GetDomainEventsList();

            if (result.NextPhase != null)
                _gameFlow.ChangePhase(result.NextPhase.Value);

                PassDomainEvents(domainEventsList);
                PassUIMessages(uiMessagesList);
        }

        private void PassUIMessages(IReadOnlyList<IUIMessages> uiMessagesList)
        {
            foreach (var uiMessage in uiMessagesList)
            {
                var internalUIEvent = _translator.TranslateUIMessage(uiMessage);

                // once eventbus is moved to unity publish here //
            }
        }

        private void PassDomainEvents(IReadOnlyList<IDomainEvent> domainEventsList)
        {
            foreach (var domainEvent in domainEventsList)
            {
                var internalUIEvent = _translator.TranslateDomainEvent(domainEvent);

                // once eventbus is moved to unity publish here //
            }
        }
    }
}
