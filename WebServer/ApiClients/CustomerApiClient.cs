using Blazored.LocalStorage;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using TelecomCRM.WebServer.ApiClients.Interfaces;
using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients
{
    public class CustomerApiClient : ICustomerApiClient
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public CustomerApiClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var token = await _localStorage.GetItemAsStringAsync("authToken");
            var cleanToken = token?.Trim('\"');
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", cleanToken);

            var customers = await _httpClient.GetFromJsonAsync<List<CustomerDTO>>("api/customers");
            return customers ?? new();
        }

    }
}
