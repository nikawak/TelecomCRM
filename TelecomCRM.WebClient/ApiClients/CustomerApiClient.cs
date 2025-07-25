using System.Net.Http.Json;
using TelecomCRM.WebClient.ApiClients.Interfaces;
using TelecomCRM.WebClient.DTOs;

namespace TelecomCRM.WebClient.ApiClients
{
    public class CustomerApiClient(HttpClient _httpClient) : ICustomerApiClient
    {
        public async Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<CustomerDTO>>("api/customers");
            return customers ?? [];
        }

    }
}
