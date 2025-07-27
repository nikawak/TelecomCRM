using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TelecomCRM.Application.Commands;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers;

public class LoginCommandHandler(
    UserManager<IdentityUser> _userManager,
    TelecomDbContext _context,
    JwtService _jwtService,
    ILogger<LoginCommandHandler> _logger
) : IRequestHandler<LoginCommand, Result<UserResponse>>
{
    public async Task<Result<UserResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                _logger.LogWarning($"Пользователь с email {request.Email} не найден", request.Email);
                return Result<UserResponse>.Failure(Errors.Unauthorized);
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, request.Password);
            if (!passwordValid)
            {
                _logger.LogWarning($"Неверный пароль для пользователя {request.Email}", request.Email);
                return Result<UserResponse>.Failure(Errors.Unauthorized);
            }

            var customer = _context.Customers.FirstOrDefault(c => c.IdentityId == user.Id && !c.IsDeleted);
            if (customer == null)
            {
                _logger.LogWarning($"Клиент для пользователя {request.Email} не найден", request.Email);
                return Result<UserResponse>.Failure(Errors.NotFound("Customer"));
            }

            var token = _jwtService.GenerateToken(user);
            var response = new UserResponse
            {
                Id = customer.Id,
                Token = token
            };

            _logger.LogInformation($"Успешная аутентификация для {request.Email}", request.Email);
            return Result<UserResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при входе пользователя");
            return Result<UserResponse>.Failure(Errors.Unknown);
        }
    }
}
