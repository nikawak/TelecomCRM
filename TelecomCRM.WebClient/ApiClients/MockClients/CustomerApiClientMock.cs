using TelecomCRM.WebClient.ApiClients.Interfaces;

namespace TelecomCRM.WebClient.ApiClients
{
    public class CustomerApiClientMock : ICustomerApiClient
    {
        public Task<List<CustomerDto>> GetCustomersAsync()
        {
            var mockCustomers = new List<CustomerDto>
            {
                new CustomerDto { Id = 1, Name = "Test Alice" },
                new CustomerDto { Id = 2, Name = "Test Bob" }
            };

            return Task.FromResult(mockCustomers);
        }
    }
}
