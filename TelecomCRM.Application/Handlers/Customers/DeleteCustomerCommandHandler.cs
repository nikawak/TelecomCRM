using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers;

public class DeleteCustomerCommandHandler(
    TelecomDbContext _context,
    ILogger<DeleteCustomerCommandHandler> _logger,
    UserManager<IdentityUser> _userManager
    ) : IRequestHandler<DeleteCustomerCommand, Result<Unit>>
{
    public async Task<Result<Unit>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var customer = await _context.Customers
                .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

            if (customer == null)
            {
                _logger.LogWarning($"Клиент с id {customer.Id} не найден", customer.Id);
                return Result<Unit>.Failure(Errors.NotFound("Customer"));
            }

            customer.IsDeleted = true;
            _context.Customers.Update(customer);
            await _context.SaveChangesAsync(cancellationToken);

            _logger.LogInformation($"Клиент успешно помечен как удаленный (Id: {customer.Id})", customer.Id);
            return Result<Unit>.Success(Unit.Value);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при удалении клиента");
            return Result<Unit>.Failure(Errors.Unknown);
        }
    }
}
