using TelecomCRM.WebServer.DTOs;

namespace TelecomCRM.WebServer.ApiClients.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<List<CustomerDTO>> GetCustomersAsync();
    }
}
