using MediatR;
using MediatR.Wrappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.Queries.Customers;
using TelecomCRM.Application.Queries.Services;
using TelecomCRM.Application.ResponseModels;
using TelecomCRM.Infrastructure.Data;

namespace TelecomCRM.Application.Handlers.Services
{
    public class GetAllServicesQueryHandler (
        TelecomDbContext _dbContext,
        ILogger<GetAllServicesQueryHandler> _logger)
        : IRequestHandler<GetAllServicesQuery, Result<List<ServiceDTO>>>
    {
        public async Task<Result<List<ServiceDTO>>> Handle(GetAllServicesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                _logger.LogInformation("Начато получение списка сервисов");

                var services = await _dbContext.TelecomServices
                    .Where(x => !x.IsDeleted)
                    .Select(x => new ServiceDTO()
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        MonthlyFee = x.MonthlyFee,
                    }).ToListAsync(cancellationToken);

                if (services == null || services.Count == 0)
                {
                    _logger.LogWarning("Список сервисов пустой");
                    var list = new List<ServiceDTO>();
                    return Result<List<ServiceDTO>>.Success(list);
                }

                _logger.LogInformation($"Получено {services.Count} сервисов", services.Count);
                return Result<List<ServiceDTO>>.Success(services);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Ошибка при получении списка сервисов");
                return Result<List<ServiceDTO>>.Failure(Errors.Unknown);
            }
        }
    }
}
