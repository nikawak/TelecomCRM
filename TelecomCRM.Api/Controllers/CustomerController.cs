using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;
using TelecomCRM.Application;
using TelecomCRM.Application.Commands;
using TelecomCRM.Application.Commands.Customer;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.Queries;
using TelecomCRM.Application.Queries.Customers;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api/customers")]
    public class CustomersController : TelecomBaseController
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

            return HandleResult(result);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Get(int id)
        {
            var query = new GetCustomerByIdQuery() { Id = id};
            var result = await _mediator.Send(query);

            return HandleResult(result);
        }
        [HttpPost]
        public async Task<ActionResult<List<CustomerDTO>>> Create(CreateCustomerDTO custDTO)
        {
            var command = new AddCustomerCommand()
            {
                FullName = custDTO.FullName,
                Address = custDTO.Address,
                Email = custDTO.Email,
                PhoneNumber = custDTO.PhoneNumber
            };
            var result = await _mediator.Send(command);

            return HandleResult(result);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Update(int id, UpdateCustomerDTO custDTO)
        {
            var command = new UpdateCustomerCommand()
            {
                Id = id,
                FullName = custDTO.FullName,
                Address = custDTO.Address,
                PhoneNumber = custDTO.PhoneNumber
            };
            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<List<CustomerDTO>>> Delete(int id)
        {
            var command = new DeleteCustomerCommand() { Id = id };

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
