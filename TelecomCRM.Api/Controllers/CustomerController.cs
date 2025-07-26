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
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            var result = await _mediator.Send(new GetAllCustomersQuery());
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Get(int id)
        {
            var result = await _mediator.Send(new GetCustomerByIdQuery());
            return Ok(result);
        }
        [HttpPost]
        public async Task<ActionResult<List<CustomerDTO>>> Create(CustomerDTO custDTO)
        {
            var result = await _mediator.Send(new AddCustomerCommand());
            return Ok(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Update(int id, CustomerDTO custDTO)
        {
            var result = await _mediator.Send(new UpdateCustomerCommand());
            return Ok(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Delete(int id)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand());
            return Ok(result);
        }
    }
}
