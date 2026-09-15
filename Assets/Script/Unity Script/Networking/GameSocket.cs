using BGS.Shared.Dtos;
using Catan.Shared.Dtos.DomainEvents;
using Catan.Unity.Helpers;
using Catan.Unity.Phases.Controllers;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Catan.Helpers;
using Unity.Helpers;

namespace Catan.Unity.Networking
{
    public sealed class GameSocket
    {
        private ClientWebSocket _socket;
        private CacheUpdater _updater;
        private AdapterGameFlow _gameFlow;
        private LogCreator _logCreator;
        private EventBus _bus;

        public async Task Connect(Guid gameId, Guid playerToken, CacheUpdater dispatcher, AdapterGameFlow gameFlow, LogCreator logCreator, EventBus bus)
        {
            _socket = new ClientWebSocket();
            _updater = dispatcher;
            _bus = bus;
            _gameFlow = gameFlow;
            _logCreator = logCreator;

            var uri = new Uri($"ws://localhost:5000/games/{gameId}/{playerToken}/socket");

            await _socket.ConnectAsync(uri, CancellationToken.None);

            _ = ReceiveLoop();
        }

        private async Task ReceiveLoop()
        {
            var buffer = new byte[8192];

            while (_socket.State == WebSocketState.Open)
            {
                var result = await _socket.ReceiveAsync(
                    new ArraySegment<byte>(buffer),
                    CancellationToken.None);

                var json = Encoding.UTF8.GetString(
                    buffer,
                    0,
                    result.Count);

                HandleMessage(json);
            }
        }

        private void HandleMessage(string json)
        {
            var update = JsonConvert.DeserializeObject<GameUpdateDto>(json);
            var domainEventDto = DomainEventDeserializer.Deserialize(update);
            _updater.UpdateCache(domainEventDto);

            if (domainEventDto is PhaseChangedEventDto phaseChanged)
            {
                _gameFlow.ChangePhase(phaseChanged.Phase);
            }

            var uievents = EventsTranslator.TranslateDomainEvent(domainEventDto);

            foreach ( var uievent in uievents)
            {
                _bus.Publish(uievent);
            }

            var log = _logCreator.CreateLog(domainEventDto);

            // publish it
        }


    }
}