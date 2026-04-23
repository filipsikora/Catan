using Catan.Shared.Data;
using BGS.Shared.Dtos;
using Catan.Unity.Helpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using BGS.Shared.Data;

namespace Catan.Unity.Networking
{
    public class GameClient
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl = "http://localhost:5000/games";

        public GameClient()
        {
            _http = new HttpClient();
        }

        public async Task<CreateGameResponseDto> CreateGame()
        {
            var dto = new CreateGameRequestDto
            {
                GameType = EnumGames.Catan.ToString()
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

            return result;
        }

        public async Task<CommandResponseDto> SendCommand(Guid gameId, CommandRequestDto dto)
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

        public async Task<T> SendQuery<T>(Guid gameId, EnumQueryName queryName, object? data = null)
        {
            var queryString = Mappers.MapEnumQueryToString(queryName);
            var url = $"{_baseUrl}/{gameId}/queries/{queryString}";

            if (data != null)
            {
                var paramString = BuildQueryString(data);
                url += $"?{paramString}";

            }

            UnityEngine.Debug.Log(url);

            var response = await _http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                var error = await response.Content.ReadAsStringAsync();

                throw new Exception($"HTTP error: {response.StatusCode} - {error}");
            }

            var json = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(json) ?? throw new Exception("Failed to deserialize query");
        }

        private string BuildQueryString(object data)
        {
            var properties = data.GetType().GetProperties();
            var parts = new List<string>();

            foreach (var prop in properties)
            {
                var value = prop.GetValue(data);

                if (value == null)
                    continue;

                if (value is IEnumerable<int> list)
                {
                    foreach (var item in list)
                    {
                        parts.Add($"{prop.Name}={Uri.EscapeDataString(item.ToString())}");
                    }
                }

                else
                {
                    parts.Add($"{prop.Name}={value}");
                }
            }

            return string.Join("&", parts);
        }
    }
}