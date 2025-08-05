using MediatR;
using Microsoft.AspNetCore.DataProtection.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands.Subscriptions;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Subscriptions
{
    public class UserSubscribeCommandHandler(
        TelecomDbContext _dbContext,
        ILogger<UserSubscribeCommandHandler> _logger)
        : IRequestHandler<UserSubscribeCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UserSubscribeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Оформление подписки начато");
                var subscription = new Subscription()
                {
                    StartDate = DateTime.Now,
                    IsActive = true,
                    ServiceId = request.ServiceId,
                    CustomerId = request.UserId,
                    IsDeleted = false
                };
                var res = await _dbContext.Subscriptions.AddAsync(subscription);
                if(res == null || res.Entity.Id < 0)
                {
                    _logger.LogWarning("Создана некорректная модель");
                    return Result<Unit>.Failure(new Error("Subscription.InvalidId", "Невалидный id", 400));
                }

                _logger.LogInformation($"Подписка оформлена успешно {res.Entity.Id}", res.Entity.Id);
                return Result<Unit>.Success(Unit.Value);
            }
            catch (Exception ex)
            {
                _logger.LogInformation(ex, "Неизвестная ошибка при оформлении подписки");
                return Result<Unit>.Failure(Errors.Unknown);
            }
        }
    }
}
