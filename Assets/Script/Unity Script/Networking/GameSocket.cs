using BGS.Shared.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Unity.Helpers;

namespace Catan.Unity.Networking
{
    public sealed class GameSocket
    {
        private ClientWebSocket _socket;
        private DomainEventDispatcher _dispatcher;

        public async Task Connect(Guid gameId, Guid playerToken, DomainEventDispatcher dispatcher)
        {
            _socket = new ClientWebSocket();
            _dispatcher = dispatcher;

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
            var domainEventDto = _dispatcher.Handle(update);
            // get uievents list + log
            // publish it
        }
    }
}