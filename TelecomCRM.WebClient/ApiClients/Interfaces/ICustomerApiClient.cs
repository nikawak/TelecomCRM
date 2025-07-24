namespace TelecomCRM.WebClient.ApiClients.Interfaces
{
    public interface ICustomerApiClient
    {
        Task<List<CustomerDto>> GetCustomersAsync();
    }
}
