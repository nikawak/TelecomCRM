using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Application.Validators;
using TelecomCRM.Application.Validators.Customers;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Customers
{
    public class UpdateCustomerCommandHandler(
        TelecomDbContext _context,
        ILogger<UpdateCustomerCommandHandler> _logger,
        UserManager<IdentityUser> _userManager,
        JwtService _jwtService)
        : IRequestHandler<UpdateCustomerCommand, Result<UserResponse>>
    {
        public async Task<Result<UserResponse>> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new UpdateCustomerCommandValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    _logger.LogWarning("Валидация не пройдена: {Errors}", string.Join("; ", validationResult.Errors.Select(e => e.ErrorMessage)));
                    return Result<UserResponse>.Failure(Errors.Validation);
                }

                var customer = await _context.Customers
                    .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted, cancellationToken);

                if (customer == null)
                {
                    _logger.LogWarning($"Клиент с customerId {customer.Id} не найден", customer.Id);
                    return Result<UserResponse>.Failure(Errors.NotFound("Customer"));
                }

                var user = await _userManager.FindByIdAsync(customer.IdentityId);
                if (user == null)
                {
                    _logger.LogWarning($"Identity-пользователь с Id {customer.IdentityId} не найден", customer.IdentityId);
                    return Result<UserResponse>.Failure(Errors.NotFound("User"));
                }

                // Обновление Identity-полей
                user.PhoneNumber = request.PhoneNumber;

                var updateResult = await _userManager.UpdateAsync(user);
                if (!updateResult.Succeeded)
                {
                    var identityErrors = string.Join("; ", updateResult.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    _logger.LogWarning("Ошибка при обновлении Identity: {Errors}", identityErrors);
                    return Result<UserResponse>.Failure(Errors.Validation);
                }
                _logger.LogInformation("Пользователь успешно обновлены (Id: {Id})", customer.IdentityId);

                // Обновление Customer-полей
                customer.FullName = request.FullName;
                customer.Address = request.Address;

                _context.Customers.Update(customer);
                await _context.SaveChangesAsync(cancellationToken);

                _logger.LogInformation("Клиент успешно обновлены (Id: {Id})", customer.Id);

                var response = new UserResponse
                {
                    Id = customer.Id,
                    Token = _jwtService.GenerateToken(user)
                };

                return Result<UserResponse>.Success(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при обновлении клиента");
                return Result<UserResponse>.Failure(Errors.Unknown);
            }
        }
    }
}
