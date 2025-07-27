using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Application.Validators;
using TelecomCRM.Application.Validators.Customers;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Customers
{
    public class AddCustomerCommandHandler(TelecomDbContext _context
        , ILogger<AddCustomerCommandHandler> _logger
        , UserManager<IdentityUser> _userManager
        , JwtService _jwtService)
        : IRequestHandler<AddCustomerCommand, Result<UserResponse>>
    {
        public async Task<Result<UserResponse>> Handle(AddCustomerCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddCustomerCommandValidator();
                var validationResult = validator.Validate(request);
                if (!validationResult.IsValid)
                {
                    var errors = validationResult.Errors.Select(e => e.ErrorMessage);
                    _logger.LogWarning($"Валидация не пройдена: {errors}", errors);
                    //var errorsMessage = string.Join("; ", errors);
                    return Result<UserResponse>.Failure(Errors.Validation);
                }
                var user = new IdentityUser { UserName = request.Email.Split('@')[0], Email = request.Email, PhoneNumber = request.PhoneNumber };
                var result = await _userManager.CreateAsync(user, request.Password);

                if(!result.Succeeded)
                {
                    var errors = string.Join("; ", result.Errors.Select(e => $"{e.Code}: {e.Description}"));
                    _logger.LogWarning($"Ошибка при создании Identity {errors}");
                    return Result<UserResponse>.Failure(Errors.Validation);
                }
                var token = _jwtService.GenerateToken(user);

                var customer = new Customer
                {
                    Address = request.Address,
                    IdentityId = user.Id, 
                    FullName = request.FullName,
                };

                var res = await _context.AddAsync(customer, cancellationToken);
                await _context.SaveChangesAsync(cancellationToken);

                if (res.Entity.Id <= 0)
                {
                    _logger.LogWarning("Созданный клиент имеет невалидный Id: {Id}", res.Entity.Id);
                    return Result<UserResponse>.Failure(new Error("User.InvalidId", "Невалидный id", 400));
                }

                _logger.LogInformation("Клиент успешно добавлен с Id {Id}", res.Entity.Id);
                var userResponse = new UserResponse() { Id = res.Entity.Id, Token = token };
                return Result<UserResponse>.Success(userResponse);

            }
            catch(Exception ex) 
            {
                _logger.LogError(ex, "Ошибка при добавлении клиента");
                return Result<UserResponse>.Failure(Errors.Unknown);
            }
        }
    }
}
