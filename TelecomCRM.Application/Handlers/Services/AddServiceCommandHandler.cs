using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.Commands.Services;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Application.Validators.Customers;
using TelecomCRM.Application.Validators.Services;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Services
{
    public class AddServiceCommandHandler(
        TelecomDbContext _dbContext,
        ILogger<AddServiceCommandHandler> _logger)
        : IRequestHandler<AddServiceCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(AddServiceCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var validator = new AddServiceCommandValidator();
                var validationResult = validator.Validate(request);

                if (!validationResult.IsValid)
                {
                    var errors = string.Join(";", validationResult.Errors.Select(e => e.ErrorMessage));
                    _logger.LogWarning($"Валидация не пройдена: {errors}", errors);
                    //var errorsMessage = string.Join("; ", errors);
                    return Result<int>.Failure(Errors.Validation);
                }

                var model = new Service()
                {
                    Description = request.Description,
                    MonthlyFee = request.MonthlyFee,
                    Name = request.Name,
                    IsDeleted = false
                };

                var res = await _dbContext.TelecomServices.AddAsync(model);
                await _dbContext.SaveChangesAsync();

                if (res.Entity.Id <= 0)
                {
                    _logger.LogWarning("Созданный сервис имеет невалидный Id: {Id}", res.Entity.Id);
                    return Result<int>.Failure(new Error("Service.InvalidId", "Невалидный id", 400));
                }

                _logger.LogInformation("Сервис успешно добавлен с Id {Id}", res.Entity.Id);
                return Result<int>.Success(res.Entity.Id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при создании сервиса");
                return Result<int>.Failure(Errors.Unknown);
            }
        }
    }
}
