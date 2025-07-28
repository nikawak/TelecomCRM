using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "User")]
    [Route("api/subscriptions")]
    public class SubscriptionController : TelecomBaseController
    {
        private readonly IMediator _mediator;
        public SubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<CustomerDTO>>> GetAll()
        {
            throw new NotImplementedException();

        }
        [HttpPost]
        public async Task<ActionResult<List<CustomerDTO>>> Create()
        {
            throw new NotImplementedException();
        }
        [HttpDelete("{subscriptionId}")]
        public async Task<ActionResult<List<CustomerDTO>>> Delete(int subscriptionId)
        {
            throw new NotImplementedException();
        }
    }
}
