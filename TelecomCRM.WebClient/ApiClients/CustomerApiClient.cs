using System.Net.Http.Json;
using TelecomCRM.WebClient.ApiClients.Interfaces;

namespace TelecomCRM.WebClient.ApiClients
{
    public class CustomerApiClient(HttpClient _httpClient) : ICustomerApiClient
    {
        public async Task<List<CustomerDto>> GetCustomersAsync()
        {
            var customers = await _httpClient.GetFromJsonAsync<List<CustomerDto>>("api/customers");
            return customers ?? [];
        }

    }
}
