using Catan.Shared.Data;
using Catan.Shared.Dtos;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace Catan.Unity.Networking
{
    public class GameClient
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl = "http://localhost:5235/games";

        public GameClient()
        {
            _http = new HttpClient();
        }

        public async Task<Guid> CreateGame()
        {
            var dto = new CreateGameRequestDto
            {
                GameType = EnumGames.Catan
            };

            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{_baseUrl}/create", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new Exception($"HHTP error: {response.StatusCode} - {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CreateGameResponseDto>(responseJson) ?? throw new Exception("Failed to deserialize CreateGameResponseDto");

            return result.GameId;
        }

        public async Task<CommandResponseDto> Send(Guid gameId, CommandRequestDto dto)
        {
            var json = JsonConvert.SerializeObject(dto);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{_baseUrl}/{gameId}/command", content);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new Exception($"HHTP error: {response.StatusCode} - {error}");
            }

            var responseJson = await response.Content.ReadAsStringAsync();

            var result = JsonConvert.DeserializeObject<CommandResponseDto>(responseJson) ?? throw new Exception("Failed to deserialize CommandResponseDto");

            return result;
        }
    }
}
