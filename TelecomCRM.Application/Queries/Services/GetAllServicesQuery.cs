using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.ResponseModels;

namespace TelecomCRM.Application.Queries.Services
{
    public record GetAllServicesQuery : IRequest<Result<List<ServiceDTO>>>;
}
