using MediatR;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Application.Queries;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CustomersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDto>>> Get()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }

    }
}
