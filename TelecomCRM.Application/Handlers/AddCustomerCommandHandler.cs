using MediatR;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands;
using TelecomCRM.Application.Validators;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers
{
    public class AddCustomerCommandHandler(TelecomDbContext _context
        , ILogger<AddCustomerCommandHandler> _logger)
        : IRequestHandler<AddCustomerCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddCustomerCommandValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    _logger.LogWarning($"Валидация не пройдена: {errors}", errors);
                    return Result<int>.Failure(string.Join("; ", errors));
                }

                var customer = new Customer
                {
                    FullName = request.FullName,
                    Email = request.Email,
                    PhoneNumber = request.PhoneNumber,
                    Address = request.Address
                };
                var res = await _context.AddAsync(customer, cancellationToken);
                if (res.Entity.Id <= 0)
                {
                    _logger.LogWarning("Созданный клиент имеет невалидный Id: {Id}", res.Entity.Id);
                    return Result<int>.Failure("Id <= 0");
                }

                await _context.SaveChangesAsync(cancellationToken);
                _logger.LogInformation("Клиент успешно добавлен с Id {Id}", res.Entity.Id);
                return Result<int>.Success(res.Entity.Id);

            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Ошибка при добавлении клиента");
                return Result<int>.Failure(ex.Message);
            }
        }
    }
}
