using Catan.Shared.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Catan.Shared.Dtos;
using Catan.Unity.Networking;
using System;

namespace Catan.Unity.Helpers
{
    public class HandlerEvents
    {
        private EventsTranslator _translator;
        private EventBus _bus;

        private GameClient _client;
        private Guid _gameId;

        public HandlerEvents(EventsTranslator translator, EventBus bus, GameClient client, Guid gameId)
        {
            _translator = translator;
            _bus = bus;
            _client = client;
            _gameId = gameId;
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
                response = await _client.Send(_gameId, dto);
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
