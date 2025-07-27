using MediatR;
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
    public class GetCustomerByIdQueryHandler(TelecomDbContext _context
        , ILogger<GetCustomerByIdQueryHandler> _logger) 
        : IRequestHandler<GetCustomerByIdQuery, Result<CustomerDTO>>
    {
        public async Task<Result<CustomerDTO>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Начато получение данных о клиенте");
                var result = await _context.Customers.FindAsync(request.Id);
                if(result == null || result.IsDeleted)
                {
                    _logger.LogWarning("Пользователя не существует или он удалён");
                    return Result<CustomerDTO>.Failure(Errors.NotFound("Customer"));
                }
                var customerDTO = new CustomerDTO() { Id = request.Id, Name = result.FullName };
                _logger.LogInformation("Данные о клиенте получены и передаются дальше");
                return Result<CustomerDTO>.Success(customerDTO);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при поиске и чтении данных клиента");
                return Result<CustomerDTO>.Failure(Errors.Unknown);
            }
        }
    }
}
