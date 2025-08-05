using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.Queries.Subscription;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Subscriptions
{
    public class GetAllUserSubscriptionQueryHandler(
        TelecomDbContext _dbContext,
        ILogger<GetAllUserSubscriptionQueryHandler> _logger,
        JwtService _jwtService)
        : IRequestHandler<GetAllUserSubscriptionsQuery, Result<List<SubscriptionDTO>>>
    {
        public async Task<Result<List<SubscriptionDTO>>> Handle(GetAllUserSubscriptionsQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Начато получение всех подписок пользвателя");
                if (string.IsNullOrEmpty(request.UserToken))
                {
                    _logger.LogWarning("Токен пустой");
                    return Result<List<SubscriptionDTO>>.Failure(Errors.InvalidCredentials);
                }
                var cred = _jwtService.ParseToken(request.UserToken);
                if (cred == null)
                {
                    _logger.LogWarning("Idenity из token не получен");
                    return Result<List<SubscriptionDTO>>.Failure(Errors.InvalidCredentials);
                }

                var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.IdentityId == cred.IdentityId);
                if (cred == null)
                {
                    _logger.LogWarning("customer не найден");
                    return Result<List<SubscriptionDTO>>.Failure(Errors.NotFound("customer"));
                }

                var subscriptions = _dbContext.Subscriptions.Where(x => x.CustomerId == customer.Id);
                if (subscriptions == null || subscriptions.Count() == 0)
                {
                    _logger.LogWarning("У customer подписок нет");
                    return Result<List<SubscriptionDTO>>.Success(new List<SubscriptionDTO>());
                }
                else
                {
                    _logger.LogWarning($"Подписки найдены: {subscriptions.Count()}");
                    return Result<List<SubscriptionDTO>>.Success(new List<SubscriptionDTO>());
                }
            }
            catch (Exception ex) 
            {
                _logger.LogError(ex, "Неизвестная ошибка");
                return Result<List<SubscriptionDTO>>.Failure(Errors.Unknown);
            }
        }
    }
}
