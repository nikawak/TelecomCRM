using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TelecomCRM.WebClient.DTOs;

namespace TelecomCRM.WebClient.ApiClients
{
    public class AuthApiClient(HttpClient _http, ILocalStorageService _localStorage)
    {
        public async Task<bool> Login(LoginDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/login", dto);
            if (!response.IsSuccessStatusCode) return false;

            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
        public async Task<bool> Register(LoginDTO dto)
        {
            var response = await _http.PostAsJsonAsync("api/auth/register", dto);
            if (!response.IsSuccessStatusCode) return false;

            var token = await response.Content.ReadAsStringAsync();
            await _localStorage.SetItemAsync("authToken", token);

            _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            return true;
        }
    }
}
