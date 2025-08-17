using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TelecomCRM.Api.Services;
using TelecomCRM.Application.Commands.Subscriptions;
using TelecomCRM.Application.DTOs;
using TelecomCRM.Application.Queries.Subscription;

namespace TelecomCRM.Api.Controllers
{
    [ApiController]
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
            string authHeader = Request.Headers["Authorization"];
            string token = authHeader.Substring("Bearer ".Length).Trim();

            var query = new GetAllUserSubscriptionsQuery() { UserToken = token };

            var result = await _mediator.Send(query);
            return HandleResult(result);
            
        }
        [HttpPost]
        public async Task<ActionResult<List<CustomerDTO>>> Create(int serviceId)
        {
            string authHeader = Request.Headers["Authorization"];
            string token = authHeader.Substring("Bearer ".Length).Trim();

            var command = new UserSubscribeCommand()
            {
                ServiceId = serviceId,
                Token = token,
            };

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
        [HttpDelete("{subscriptionId}")]
        public async Task<ActionResult<List<CustomerDTO>>> Delete(int subscriptionId)
        {
            var command = new UserUnsubscribeCommand()
            {
                Id = subscriptionId
            };

            var result = await _mediator.Send(command);
            return HandleResult(result);
        }
    }
}
