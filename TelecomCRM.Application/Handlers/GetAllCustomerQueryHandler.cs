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
    public class GetAllCustomerQueryHandler(TelecomDbContext _context
        , ILogger<GetAllCustomerQueryHandler> _logger) 
        : IRequestHandler<GetAllCustomersQuery, Result<List<CustomerDTO>>>
    {
        public async Task<Result<List<CustomerDTO>>> Handle(GetAllCustomersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Начато получение списка клиентов");

                var customers = await _context.Customers.Include(x=>x.UserInfo)
                    .Select(c => new CustomerDTO
                    {
                        Id = c.Id,
                        Name = c.FullName,
                    }).ToListAsync(cancellationToken);

                if (customers == null || !customers.Any())
                {
                    _logger.LogWarning("Список клиентов пустой");
                    var list = new List<CustomerDTO>();
                    return Result<List<CustomerDTO>>.Success(list);
                }

                _logger.LogInformation("Получено {Count} клиентов", customers.Count);
                return Result<List<CustomerDTO>>.Success(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка клиентов");
                return Result<List<CustomerDTO>>.Failure(Errors.Unknown);
            }
        }
    }
}
