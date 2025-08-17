using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace TelecomCRM.WebServer.Services
{
    public class TokenHTTPClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalStorageService _localStorage;

        public TokenHTTPClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }

        public async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request)
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (!string.IsNullOrEmpty(token))
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await _httpClient.SendAsync(request);
        }
    }
}
