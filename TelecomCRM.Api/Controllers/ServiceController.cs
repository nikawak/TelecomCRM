using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;
using TelecomCRM.Application.Commands.Services;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.Handlers.Services;
using TelecomCRM.Application.Queries.Customers;
using TelecomCRM.Application.Queries.Services;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("api/services")]
    public class ServiceController : TelecomBaseController
    {
        private readonly IMediator _mediator;
        public ServiceController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("")]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllServicesQuery());
            return HandleResult(result);
        }
        [HttpPost("")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CustomerDTO>>> Create(CreateServiceDTO serviceDTO)
        {
            var command = new AddServiceCommand()
            {
                Description = serviceDTO.Description,
                MonthlyFee = serviceDTO.MonthlyFee,
                Name = serviceDTO.Name,
            };

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
