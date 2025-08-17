using Blazored.LocalStorage;
using System.Net.Http.Json;
using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients
{
    public class ServiceApiClient
    {
        private HttpClient _httpClient;
        private ILocalStorageService _localStorage;
        public ServiceApiClient(HttpClient httpClient, ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _localStorage = localStorage;
        }
        public async Task<List<ServiceDTO>> GetServicesAsync()
        {
            var services = await _httpClient.GetFromJsonAsync<List<ServiceDTO>>("api/services");
            return services ?? new();
        }
        public async Task<int> CreateServiceAsync(CreateServiceDTO serviceDTO)
            {
            var result = await _httpClient.PostAsJsonAsync("api/services", serviceDTO);
            return await result.Content.ReadFromJsonAsync<int>();
        }
        //public async Task<int> DeleteServiceAsync(ServiceDTO serviceDTO)
        //{
        //    var result = await _httpClient.DeleteFromJsonAsync<int>($"api/services{serviceDTO}");
        //    return await result.Content.ReadFromJsonAsync<int>();
        //}
    }
}
