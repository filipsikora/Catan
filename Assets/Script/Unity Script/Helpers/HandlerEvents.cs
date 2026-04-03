using Catan.Shared.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Catan.Shared.Dtos;
using Catan.Unity.Networking;
using System;
using Catan.Unity.Phases.Controllers;

namespace Catan.Unity.Helpers
{
    public class HandlerEvents
    {
        private EventsTranslator _translator;
        private EventBus _bus;

        private GameClient _client;
        private Guid _gameId;

        private AdapterGameFlow _gameFlow;

        public HandlerEvents(EventsTranslator translator, EventBus bus, GameClient client, Guid gameId, AdapterGameFlow gameFlow)
        {
            _translator = translator;
            _bus = bus;
            _client = client;
            _gameId = gameId;
            _gameFlow = gameFlow;
        }

        public async Task Execute(EnumCommandType type, object data)
        {
            var dto = new CommandRequestDto
            {
                Type = type,
                Data = JObject.FromObject(data)
            };

            CommandResponseDto response;

            try
            {
                response = await _client.SendCommand(_gameId, dto);
            }

            catch(Exception ex)
            {
                UnityEngine.Debug.LogError($"HTTP error: {ex.Message}");
                return;
            }

            if (!response.Success)
            {
                UnityEngine.Debug.LogWarning("Command failed");
                return;
            }

            if (response.NextPhase != null)
                _gameFlow.ChangePhase(response.NextPhase);

            foreach (var message in response.UiMessages)
            {
                var jToken = message as JToken ?? JToken.FromObject(message);
                var uiMessage = _translator.TranslateDomainEvent(jToken);

                _bus.Publish(uiMessage);
            }

            foreach (var message in response.DomainMessages)
            {
                var jToken = message as JToken ?? JToken.FromObject(message);
                var domainEvent = _translator.TranslateDomainEvent(jToken);

                _bus.Publish(domainEvent);
            }
        }
    }
}
