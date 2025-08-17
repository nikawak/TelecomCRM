using TelecomCRM.WebServer.ApiClients.Interfaces;
using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients
{
    public class CustomerApiClientMock : ICustomerApiClient
    {
        public Task<List<CustomerDTO>> GetCustomersAsync()
        {
            var mockCustomers = new List<CustomerDTO>
            {
                new CustomerDTO { Id = 1, Name = "Test Alice" },
                new CustomerDTO { Id = 2, Name = "Test Bob" }
            };

            return Task.FromResult(mockCustomers);
        }
    }
}
