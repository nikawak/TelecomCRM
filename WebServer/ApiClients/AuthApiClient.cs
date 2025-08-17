using Blazored.LocalStorage;
using Microsoft.Extensions.Logging;
using System.Net.Http.Headers;
using System.Net.Http.Json; // ОБЯЗАТЕЛЬНО
using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients
{
    public class AuthApiClient
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly ILogger<AuthApiClient> _logger;

        class TOK { public string token { get; set; } }

        public AuthApiClient(HttpClient http, ILocalStorageService localStorage, ILogger<AuthApiClient> logger)
        {
            _http = http;
            _localStorage = localStorage;
            _logger = logger;
        }

        public async Task<bool> Login(LoginDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);
            if (!response.IsSuccessStatusCode) return false;

            var result = await response.Content.ReadFromJsonAsync<TOK>();
            if (string.IsNullOrEmpty(result?.token)) return false;

            await _localStorage.SetItemAsync("authToken", result.token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.token);

            _logger.LogInformation("User logged in, token saved.");
            return true;
        }

        public async Task<bool> Register(RegisterDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", dto);
            if (!response.IsSuccessStatusCode) return false;

            var token = await response.Content.ReadFromJsonAsync<string>();
            if (string.IsNullOrEmpty(token)) return false;

            await _localStorage.SetItemAsync("authToken", token);
            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            _logger.LogInformation("User registered, token saved.");
            return true;
        }
    }
}
