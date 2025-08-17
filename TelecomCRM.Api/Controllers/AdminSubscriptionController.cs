using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
    [Route("api/admin/subscription")]
    public class AdminSubscriptionController : TelecomBaseController
    {
        private readonly IMediator _mediator;
        public AdminSubscriptionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("user/{userId}")]
        public async Task<ActionResult<Unit>> UserSubscriptions(int userId)
        {
            throw new NotImplementedException();
        }
        [HttpGet("services/{serviceId}")]
        public async Task<ActionResult<Unit>> ServiceSubscribers(int serviceId)
        {
            throw new NotImplementedException();
        }
        [HttpPost("subscribe")]
        public async Task<ActionResult<Unit>> SubscribeUser()
        {
            throw new NotImplementedException();
        }
        [HttpPost("unsubscribe")]
        public async Task<ActionResult<Unit>> UnsubscribeUser()
        {
            throw new NotImplementedException();
        }
    }
}
