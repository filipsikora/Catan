using Catan.Application.Interfaces;
using Catan.Core.Interfaces;
using Catan.Shared.Data;
using System.Collections.Generic;
using System.Linq;

namespace Catan.Application
{
    public sealed class GameResult
    {
        public bool Success { get; }
        public EnumGamePhases? NextPhase { get; }
        public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;
        public IReadOnlyList<IUIMessages> UIMessages => _uiMessages;

        private List<IUIMessages> _uiMessages = new();
        private List<IDomainEvent> _domainEvents = new();

        private GameResult(bool success, EnumGamePhases? nextPhase = null)
        {
            Success = success;
            NextPhase = nextPhase;
        }

        public static GameResult Ok(EnumGamePhases? nextPhase = null)
        {
            return new GameResult(true, nextPhase);
        }

        public static GameResult Fail(EnumGamePhases? nextPhase = null)
        {
            return new GameResult(false, nextPhase);
        }

        public GameResult AddDomainEvent(IDomainEvent domainEvent)
        {
            if (domainEvent != null)
                _domainEvents.Add(domainEvent);

            return this;
        }

        public GameResult AddUIMessage(IUIMessages message)
        {
            if (message != null)
                _uiMessages.Add(message);

            return this;
        }

        public GameResult AddDomainEventsList(IReadOnlyList<IDomainEvent> domainEventsList)
        {
            foreach (var domainEvent in domainEventsList)
            {
                this.AddDomainEvent(domainEvent);
            }

            return this;
        }

        public GameResult AddUIMessagesList(IReadOnlyList<IUIMessages> uiMessagesList)
        {
            foreach (var uiMessage in uiMessagesList)
            {
                this.AddUIMessage(uiMessage);
            }

            return this;
        }

        public IReadOnlyList<IDomainEvent> GetDomainEventsList()
        {
            var domainEventsList = this.DomainEvents.ToList();

            return domainEventsList;
        }

        public IReadOnlyList<IUIMessages> GetUIMessagesList()
        {
            var uiMessagesList = this.UIMessages.ToList();

            return uiMessagesList;
        }

        public IUIMessages GetFirstUIMessage()
        {
            var firstUIMessage = _uiMessages[0];

            return firstUIMessage;
        }

        public T? GetUIMessage<T>() where T : IUIMessages
        {
            return _uiMessages.OfType<T>().FirstOrDefault();
        }
    } 
}
