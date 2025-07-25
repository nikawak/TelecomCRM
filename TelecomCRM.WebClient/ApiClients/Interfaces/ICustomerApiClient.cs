using TelecomCRM.WebClient.DTOs;

namespace TelecomCRM.WebClient.ApiClients.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<List<CustomerDTO>> GetCustomersAsync();
    }
}
