using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Queries;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers
{
    public class GetAllCustomerQueryHandler(TelecomDbContext _context, ILogger<GetAllCustomerQueryHandler> _logger) : IRequestHandler<GetAllCustomersQuery, List<CustomerDTO>>
    {
        public async Task<List<CustomerDTO>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Начато получение списка клиентов");

            try
            {
                var customers = await _context.Customers
                    .Select(c => new CustomerDTO
                    {
                        Id = c.Id,
                        Name = c.FullName,
                    }).ToListAsync(cancellationToken);

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("Список клиентов пустой");
                    return new List<CustomerDTO>();
                }

                _logger.LogInformation("Получено {Count} клиентов", customers.Count);
                return customers;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка клиентов");
                throw; // пробрасываем дальше или можно вернуть пустой список/ошибку в Result, если используешь
            }
        }
    }
}
