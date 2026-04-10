using Catan.Shared.Data;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using BGS.Shared.Dtos;
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

        public async Task Execute(EnumCommandType type, object? data = null)
        {
            var dto = new CommandRequestDto
            {
                Type = type.ToString(),
                Data = data != null ? JObject.FromObject(data) : new JObject()
            };

            CommandResponseDto response;

            try
            {
                response = await _client.SendCommand(_gameId, dto);
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.LogError($"HTTP error: {ex.Message}");
                return;
            }

            if (response == null)
            {
                UnityEngine.Debug.LogError($"GameResult: {response} is null");
                return;
            }

            if (!response.Success)
            {
                UnityEngine.Debug.LogWarning("Command failed");
                return;
            }

            if (response.NextPhase != null)
            {
                if (!Enum.TryParse<EnumGamePhases>(response.NextPhase, out var nextPhase))
                    throw new Exception($"Failed to parse NextPhase: {response.NextPhase}");

                _gameFlow.ChangePhase(nextPhase);
            }

            foreach (var message in response.UiMessages)
            {
                var uiMessage = _translator.TranslateUIMessage(message);

                _bus.Publish(uiMessage);
            }

            foreach (var message in response.DomainMessages)
            {
                var domainEvent = _translator.TranslateDomainEvent(message);

                _bus.Publish(domainEvent);
            }
        }

        public async Task<T> Query<T>(EnumQueryName queryName, object? data = null)
        {
            try
            {
                return await _client.SendQuery<T>(_gameId, queryName, data);
            }

            catch (Exception ex)
            {
                UnityEngine.Debug.Log($"Query error: {queryName}");
                return default;
            }
        }
    }
}