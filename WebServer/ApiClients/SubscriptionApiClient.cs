using Blazored.LocalStorage;
using System.Net.Http.Json;
using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients
{
    public class SubscriptionApiClient
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public SubscriptionApiClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task UserSubscribeAsync(int serviceId)
        {
            await _httpClient.PostAsJsonAsync<int>("api/subscriptions", serviceId);
        }
        public async Task UserUnscubscribeAsync(int subscriptionId)
        {
            await _httpClient.DeleteFromJsonAsync<int>($"api/subscriptions/{subscriptionId}");
        }
    }
}
