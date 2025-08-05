using MediatR;
using Microsoft.EntityFrameworkCore;
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
    public class UserUnsubscribeCommandHandler(
        TelecomDbContext _dbContext,
        ILogger<UserSubscribeCommandHandler> _logger)
        : IRequestHandler<UserUnsubscribeCommand, Result<Unit>>
    {
        public async Task<Result<Unit>> Handle(UserUnsubscribeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Удаление подписки начато");

                var res = await _dbContext.Subscriptions.FirstOrDefaultAsync(x=>x.Id == request.Id);
                if (res == null)
                {
                    _logger.LogWarning("Подписки не существует");
                    return Result<Unit>.Failure(new Error("Subscribe.DoesntExist", "Не существует", 400));
                }
                if (res.IsDeleted)
                {
                    _logger.LogWarning("Подписка уже удалена");
                    return Result<Unit>.Failure(new Error("Subscribe.Deleted", "Уже удалена", 400));
                }

                res.IsActive = true;
                _dbContext.Subscriptions.Update(res);

                _logger.LogInformation($"Подписка остановлена (не активна)");
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
