using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;
using TelecomCRM.Application.Queries.Customers;

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

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            throw new NotImplementedException();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<CustomerDTO>>> Create()
        {
            throw new NotImplementedException();
        }
    }
}
